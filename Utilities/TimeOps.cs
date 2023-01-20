using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.IdentityModel;
using Microsoft.IdentityModel.Threading;

namespace CabalsCorner.Utilities
{
	#region Enumerated Types

	public enum GetNetworkTimeResult { Success, Failure };

	#endregion

	public class TimeOps
	{
		#region Delegate(s), Event(s), and EventArgs class

		public delegate DateTime GetNetworkTimeHandler();

		public delegate void GetNetworkTimeCompleteHandler(object sender, GetNetworkTimeFinishedArgs e);

		public event GetNetworkTimeCompleteHandler GetNetworkTimeComplete;

		public class GetNetworkTimeFinishedArgs : EventArgs
		{
			public GetNetworkTimeFinishedArgs(GetNetworkTimeResult result)
			{
				this.Result = result;
			}

			public readonly GetNetworkTimeResult Result;
		}

		#endregion

		#region Asynchronous Operation

		public void BeginGetNetworkTime()
		{
			handler = new GetNetworkTimeHandler(this.GetNetworkTime);
			AsyncCallback callback = new AsyncCallback(this.OnGetNetworkTimeComplete);
			handler.BeginInvoke(callback, null);
		}

		#endregion

		#region Asynchronous Method Completion Callback

		private void OnGetNetworkTimeComplete(IAsyncResult asyncResult)
		{
			GetNetworkTimeResult gntResult = (! Failed) ? GetNetworkTimeResult.Success : GetNetworkTimeResult.Failure;
			try
			{
				//Debug.Assert(result.EndInvokeCalled == false);
				Debug.Assert(asyncResult.CompletedSynchronously == false);

				//GetNetworkTimeHandler del = (GetNetworkTimeHandler)asyncResult.AsyncDelegate;
				GetNetworkTimeFinishedArgs result = TypedAsyncResult<GetNetworkTimeFinishedArgs>.End(asyncResult);
				failed = false;

				//del.EndInvoke(asyncResult);
			}
			catch (Exception ex)
			{
				failed = true;
				gntResult = GetNetworkTimeResult.Failure;
				throw ex;
			}
			finally
			{
				EventDispatcher.SyncExecute(
					this.GetNetworkTimeComplete
				,	this
				,	new GetNetworkTimeFinishedArgs(gntResult)
				);
			}
		}

		#endregion

		#region Properties: Read-Only

		public bool Failed
		{
			get { return failed; }
		} 

		#endregion

		#region Operations

		public DateTime LoopGetNetworkTime()
		{
			bool opFailed = true;
			DateTime dt = new DateTime(1979, 2, 28);
			int count = 0;
			do
			{
				try
				{
					if (count == 3)
						throw new ApplicationException("Failed to get global time.");
					dt = GetNetworkTime();
					opFailed = false;
					count++;
				}
				catch (ApplicationException ex)
				{
					opFailed = true;
					throw ex;
				}
				catch (Exception)
				{
					opFailed = true;
				}
			}
			while (opFailed);
			return dt;
		}
		public DateTime GetNetworkTime()
		{
			try
			{
				//default Windows time server
				const string ntpServer = "time.windows.com";

				// NTP message size - 16 bytes of the digest (RFC 2030)
				var ntpData = new byte[48];

				//Setting the Leap Indicator, Version Number and Mode values
				ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

				var addresses = Dns.GetHostEntry(ntpServer).AddressList;

				//The UDP port number assigned to NTP is 123
				var ipEndPoint = new IPEndPoint(addresses[0], 123);
				//NTP uses UDP
				var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

				socket.Connect(ipEndPoint);

				//Stops code hang if NTP is blocked
				socket.ReceiveTimeout = 3000;

				socket.Send(ntpData);
				socket.Receive(ntpData);
				socket.Close();

				//Offset to get to the "Transmit Timestamp" field (time at which the reply 
				//departed the server for the client, in 64-bit timestamp format."
				const byte serverReplyTime = 40;

				//Get the seconds part
				ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

				//Get the seconds fraction
				ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

				//Convert From big-endian to little-endian
				intPart = SwapEndianness(intPart);
				fractPart = SwapEndianness(fractPart);

				var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

				//**UTC** time
				var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

				networkTime = networkDateTime.ToLocalTime();
				failed = false;

			}
			catch (Exception ex)
			{
				failed = true;
				throw ex;
			}
			return networkTime;
		} 

		#endregion

		#region Utilities

		// stackoverflow.com/a/3294698/162671
		private uint SwapEndianness(ulong x)
		{
			return (uint)(((x & 0x000000ff) << 24) +
								((x & 0x0000ff00) << 8) +
								((x & 0x00ff0000) >> 8) +
								((x & 0xff000000) >> 24));
		} 

		#endregion

		#region Private Fields

		private bool failed = true;
		private DateTime networkTime;
		private GetNetworkTimeHandler handler = null; 

		#endregion
	}
}
