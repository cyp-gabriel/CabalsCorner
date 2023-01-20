using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using CabalsCorner.Utilities;
using CabalsCorner.ErrorHandling;

using System.Configuration;
using Microsoft.IdentityModel.Threading;
using System.Collections.Concurrent;

namespace CabalsCorner.CodeLocker.Classes
{
	internal class GetTimeOp
	{
		#region Events and Delegates

		public event AsyncCompletedEventHandler ExecuteCompleted;
		public event EventHandler<ExecuteProgressChangedEventArgs> ExecuteProgressChanged;

		private delegate void ExecuteDelegate(AsyncOperation async, GetTimeAsyncContext ctx, out bool cancelled);
		protected virtual void FireOnCompleted(AsyncCompletedEventArgs e)
		{
			if (ExecuteCompleted != null)
				ExecuteCompleted(this, e);
		}
		public virtual void FireOnExecuteProgressChanged(ExecuteProgressChangedEventArgs e)
		{
			if (ExecuteProgressChanged != null)
				ExecuteProgressChanged(this, e);
		}

		#endregion

		#region Properties: Read/Write

		public bool TimedOut
		{
			get { return _timedOut; }
			set { _timedOut = value; }
		}
		public SocketException Error
		{
			get { return _error; }
			set { _error = value; }
		}

		#endregion

		#region "Execute" Asynchronous Operations

		public bool IsBusy
		{
			get { return _myTaskIsRunning; }
		}
		public DateTime Result
		{
			get { return _result; }
			set { _result = value; }
		}

		public DateTime ExecuteAsync(CancellationToken ct)
		{
			if (App.Instance.Settings.UseMicrosoftTime)
			{
				int failCount = 0;
				DateTime dt = new DateTime(1979, 2, 28);
				while (true)
				{
					try
					{
						if (ct.IsCancellationRequested)
						{
							//cancelled = true;

							FireOnExecuteProgressChanged(new ExecuteProgressChangedEventArgs(1, "Getting time from time.windows.com...", null));

							ct.ThrowIfCancellationRequested();
						}

						ExecuteProgressChangedEventArgs eArgs = new(1, "Getting time from time.windows.com...", null);
						FireOnExecuteProgressChanged(eArgs);

						Result = dt = GetNetworkTime();
						return dt;
					}
					catch (SocketException ex)
					{
						failCount++;
						Error = ex;
						Console.WriteLine(new ExceptionMessageMaker().MakeExceptionChainMessage(ex));

						if (ex.ErrorCode == WSAETIMEDOUT)
						{
							continue;
						}
						else if (ex.ErrorCode == WSAHOST_NOT_FOUND)
						{
							if (failCount >= 20)
								return DateTime.Now;

							continue;
						}
						else if (ex.ErrorCode == WSATRY_AGAIN)
						{
							continue;
						}
						else
						{
							if (failCount >= 20)
								return DateTime.Now;
						}
					} // catch
				}
			}

			string[] servers = new CodePersistanceMgr().GetTimeServers();

			//	Try	each	server	in	random	order	to	avoid	blocked	requests	due	to	too	frequent	request
			for (int i = 0; i < servers.Length; i++)
			{
				Random r = new();
				int index = r.Next(0, servers.Length - 1);
				string server = servers[index];
				try
				{
					//	if (ctxt.IsCancelling && this.IsBusy)
					//	{
					//		cancelled = true;

					//		ExecuteProgressChangedEventArgs args = new ExecuteProgressChangedEventArgs(1, "Cancelled", null);
					//		async.Post(
					//					delegate (object e)
					//					{
					//						FireOnExecuteProgressChanged((ExecuteProgressChangedEventArgs)e);
					//					}
					//					, args
					//		);
					//		return;
					//	}

					string argString = string.Format("Getting	time	from	{0}...", server);
					ExecuteProgressChangedEventArgs eArgs = new(1, argString, null);
					//async.Post(
					//		delegate (object e)
					//		{
					//			FireOnExecuteProgressChanged((ExecuteProgressChangedEventArgs)e);
					//		}
					//		, eArgs
					//	);
					FireOnExecuteProgressChanged(eArgs);

					SNTPClient client = new SNTPClient(server);
					int timeout = Convert.ToInt32(App.Instance.Settings.TimeoutMS);

					client.Connect(timeout, false);
					this.Result = client.TransmitTimestamp;

				}
				catch (SocketException ex)
				{
					Error = ex;
					Console.WriteLine(new ExceptionMessageMaker().MakeExceptionChainMessage(ex));

					if (ex.ErrorCode == WSAETIMEDOUT)
					{
						_timedOut = true;
						break;
					}
					else if (ex.ErrorCode == WSAHOST_NOT_FOUND)
					{
						Error = ex;
						break;
					}
					else if (ex.ErrorCode == WSATRY_AGAIN)
					{
						string argString = string.Format("Retrying...", server);
						ExecuteProgressChangedEventArgs eArgs = new ExecuteProgressChangedEventArgs(1, argString, null);
						//async.Post(
						//			delegate (object e)
						//			{
						//				FireOnExecuteProgressChanged((ExecuteProgressChangedEventArgs)e);
						//			}
						//			, eArgs
						//);
						FireOnExecuteProgressChanged(eArgs);
						continue;
					}
					else
					{
						int code = ex.ErrorCode;
					}

				} // catch

			} // for
			return this.Result;
		}

		public void Execute(AsyncOperation async, GetTimeAsyncContext ctxt, out bool cancelled)
		{
			cancelled = false;
			TimedOut = false;
			Error = null;

			if (App.Instance.Settings.UseMicrosoftTime)
			{
				int failCount = 0;
				DateTime dt = new DateTime(1979, 2, 28);
				while (true)
				{
					Error = null;
					try
					{
						if (ctxt.IsCancelling && this.IsBusy)
						{
							cancelled = true;

							ExecuteProgressChangedEventArgs args = new ExecuteProgressChangedEventArgs(1, "Operation cancelled.", null);
							async.Post(
								delegate (object e)
								{
									FireOnExecuteProgressChanged((ExecuteProgressChangedEventArgs)e);
								}
								, args
							);
							return;
						}

						ExecuteProgressChangedEventArgs eArgs = new ExecuteProgressChangedEventArgs(1, "Getting time from time.windows.com...", null);
						async.Post(
							delegate (object e)
							{
								FireOnExecuteProgressChanged((ExecuteProgressChangedEventArgs)e);
							}
							, eArgs
						);

						dt = GetNetworkTime();
						this.Result = dt;
						return;
					}
					catch (SocketException ex)
					{
						failCount++;
						Error = ex;
						Console.WriteLine(new ExceptionMessageMaker().MakeExceptionChainMessage(ex));

						if (ex.ErrorCode == WSAETIMEDOUT)
						{
							continue;
						}
						else if (ex.ErrorCode == WSAHOST_NOT_FOUND)
						{
							if (failCount >= 20)
								return;

							continue;
						}
						else if (ex.ErrorCode == WSATRY_AGAIN)
						{
							continue;
						}
						else
						{
							if (failCount >= 20)
								return;
						}
					} // catch

				} // while

			}

			string[] servers = new CodePersistanceMgr().GetTimeServers();

			//	Try	each	server	in	random	order	to	avoid	blocked	requests	due	to	too	frequent	request
			for (int i = 0; i < servers.Length; i++)
			{
				Random r = new Random();
				int index = r.Next(0, servers.Length - 1);
				string server = servers[index];
				try
				{
					if (ctxt.IsCancelling && this.IsBusy)
					{
						cancelled = true;

						ExecuteProgressChangedEventArgs args = new ExecuteProgressChangedEventArgs(1, "Cancelled", null);
						async.Post(
									delegate (object e)
									{
										FireOnExecuteProgressChanged((ExecuteProgressChangedEventArgs)e);
									}
									, args
						);
						return;
					}

					string argString = string.Format("Getting	time	from	{0}...", server);
					ExecuteProgressChangedEventArgs eArgs = new ExecuteProgressChangedEventArgs(1, argString, null);
					async.Post(
							delegate (object e)
							{
								FireOnExecuteProgressChanged((ExecuteProgressChangedEventArgs)e);
							}
							, eArgs
						);
					client = new Utilities.SNTPClient(server);
					int timeout = Convert.ToInt32(App.Instance.Settings.TimeoutMS);
					client.Connect(timeout, false);
					Result = client.TransmitTimestamp;

					return;
				}
				catch (SocketException ex)
				{
					Error = ex;
					Console.WriteLine(new ExceptionMessageMaker().MakeExceptionChainMessage(ex));

					if (ex.ErrorCode == WSAETIMEDOUT)
					{
						_timedOut = true;
						break;
					}
					else if (ex.ErrorCode == WSAHOST_NOT_FOUND)
					{
						Error = ex;
						break;
					}
					else if (ex.ErrorCode == WSATRY_AGAIN)
					{
						string argString = string.Format("Retrying...", server);
						ExecuteProgressChangedEventArgs eArgs = new ExecuteProgressChangedEventArgs(1, argString, null);
						async.Post(
									delegate (object e)
									{
										FireOnExecuteProgressChanged((ExecuteProgressChangedEventArgs)e);
									}
									, eArgs
						);
						continue;
					}
					else
					{
						int code = ex.ErrorCode;
					}

				} // catch

			} // for

		}
		//public void ExecuteAsync()
		//{
			//ExecuteDelegate worker = new ExecuteDelegate(Execute);
			//AsyncCallback completedCallback = new AsyncCallback(ExecuteCompletedCallback);

			//lock (_sync)
			//{
			//	if (_myTaskIsRunning)
			//		throw new InvalidOperationException("The control is currently busy.");

			//	AsyncOperation async = AsyncOperationManager.CreateOperation(null);
			//	GetTimeAsyncContext context = new GetTimeAsyncContext();
			//	_executeContext = context;
			//	bool cancelled;

			//	worker.BeginInvoke(async, context, out cancelled, completedCallback, async);

			//	_myTaskIsRunning = true;
			//}
		//}
		private void ExecuteCompletedCallback(IAsyncResult ar)
		{
			//// get the original worker delegate and the AsyncOperation instance
			//ExecuteDelegate worker = (ExecuteDelegate)((AsyncResult)ar).AsyncDelegate;
			//AsyncOperation async = (AsyncOperation)ar.AsyncState;

			//bool cancelled = false;

			//// finish the asynchronous operation
			//worker.EndInvoke(out cancelled, ar);

			//// clear the running task flag
			//lock (_sync)
			//{
			//	_myTaskIsRunning = false;
			//}

			//// raise the completed event
			//AsyncCompletedEventArgs completedArgs = new AsyncCompletedEventArgs(null, cancelled, null);
			//async.PostOperationCompleted(
			//  delegate (object e) { FireOnCompleted((AsyncCompletedEventArgs)e); },
			//  completedArgs);

			bool cancelled = false;

			// clear running task flag
			lock(_sync)
			{
				_myTaskIsRunning = false;
			}
			AsyncCompletedEventArgs args = TypedAsyncResult<AsyncCompletedEventArgs>.End(ar);
			this.FireOnCompleted(args);

		}
		public void CancelAsync()
		{
			lock (_sync)
			{
				if (_executeContext != null)
				{
					_executeContext.Cancel();
				}
			}
		}

		#endregion

		#region Utilities

		//Get a NTP time from NIST
		//do not request a nist date more than once every 4 seconds, or the connection will be refused.
		//more servers at tf.nist.goc/tf-cgi/servers.cgi
		public static DateTime GetDummyDate()
		{
			return new DateTime(1000, 1, 1); //to check if we have an online date or not.
		}
		public DateTime GetNetworkTime()
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

			return networkTime;
		}

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

		private DateTime _result;
		private readonly object _sync = new object();
		private bool _myTaskIsRunning = false;
		private GetTimeAsyncContext _executeContext = null;
		private DateTime networkTime;
		private CabalsCorner.Utilities.SNTPClient client;
		private bool _timedOut = false;
		private SocketException _error = null;

		private const int WSAETIMEDOUT = 10060;
		private const int WSATRY_AGAIN = 11002;
		private const int WSAHOST_NOT_FOUND = 11001;

		#endregion
	}
}