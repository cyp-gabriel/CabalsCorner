using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabalsCorner.CodeLocker.Classes
{
	internal class GetTimeAsyncContext
	{
		#region Properties: Read-Only

		public bool IsCancelling
		{
			get
			{
				lock (_sync) { return _isCancelling; }
			}
		}

		#endregion

		#region Operations

		public void Cancel()
		{
			lock (_sync) { _isCancelling = true; }
		}

		#endregion

		#region Private Fields

		private readonly object _sync = new object();
		private bool _isCancelling = false;

		#endregion
	}
}
