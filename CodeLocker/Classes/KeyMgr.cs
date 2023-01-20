using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;

using CabalsCorner.Utilities;

namespace CabalsCorner.CodeLocker.Classes
{
	internal class KeyMgr
	{
		#region Ctor(s)

		public KeyMgr()
		{
		}

		#endregion

		#region Properties: Read/Write

		public int CodeLockCount
		{
			get
			{
				CodeLockerAppSettings settings = App.Instance.Settings;
				return settings.CodeLockCount;
			}
			set
			{

				CodeLockerAppSettings settings = App.Instance.Settings;
				settings.CodeLockCount = value;
			}
		}

		#endregion

		#region Operations

		public string CreateKey()
		{
			EncrypterDecrypter ed = new EncrypterDecrypter();
         int doubledCodeLockCount = CodeLockCount * 2;
			string scrambledCodeLockCount = ed.CreateEncryptedString(doubledCodeLockCount.ToString(), ed.ENCRYPTION_TABLE4);
			//string key = string.Format("{0}{1}", ENCRYPTION_PASSWORD, scrambledCodeLockCount);
			string key = "#Fm)se+=)8vf";

			return key;
		}
		public string ReadKey()
		{
			return CreateKey();
		}
		public void ResetKey()
		{
			CodeLockCount = CodeLockCount + 1;
		}

		#endregion

		#region Private Fields

		private const string ENCRYPTION_PASSWORD = "yTT4AFY87jZ1o7Se0g";

		#endregion
	}
}
