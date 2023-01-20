using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CabalsCorner.CodeLocker.Classes
{
	internal class ExecuteProgressChangedEventArgs : ProgressChangedEventArgs
	{
		#region Ctor(s)



		public ExecuteProgressChangedEventArgs() : base(0, null)
		{
			_statusMsg = string.Empty;
		}

		public ExecuteProgressChangedEventArgs(int progressPercentage, string statusMsg, object userState)
			: base(progressPercentage, userState)
		{
			_statusMsg = statusMsg.Replace("\t", " ");
		}

		#endregion

		#region Properties: Read/Write

		public string StatusMsg
		{
			get { return _statusMsg; }
			set { _statusMsg = value; }
		}

		public static ExecuteProgressChangedEventArgs Blank { 
			get
			{
				return new ExecuteProgressChangedEventArgs();
			}
		}

		#endregion

		#region Private Fields

		private string _statusMsg; 

		#endregion
	}
}
