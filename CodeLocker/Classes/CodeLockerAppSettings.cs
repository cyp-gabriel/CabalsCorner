using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

using CabalsCorner.Utilities;

namespace CabalsCorner.CodeLocker.Classes
{
	internal class CodeLockerAppSettings : AppSettings
	{
		#region Events/Delegates

		public event EventHandler UseMicrosoftTimeChanged;

		#endregion

		#region Ctor(s)

		public CodeLockerAppSettings(string xmlFilePath) : base(xmlFilePath)
		{
		}

		#endregion

		#region Properties: Read/Write

		public int CodeLockCount
		{
			set
			{
				WriteKeyValue("CodeLockCount", value.ToString());
			}
			get
			{
				return int.Parse(GetValueAtKey("CodeLockCount"));
			}
		}
		public string UserEmail
		{
			set
			{
				WriteKeyValue("UserEmail", value.ToString());
			}
			get
			{
				return GetValueAtKey("UserEmail");
			}
		}
		public string LastUnlockedCode
		{
			set
			{
				WriteKeyValue("LastUnlockedCode", value);
			}
			get
			{
				return GetValueAtKey("LastUnlockedCode");
			}
		}
		public bool UseMicrosoftTime
		{
			set
			{
				WriteKeyValue("UseMicrosoftTime", value.ToString());
				EventDispatcher.SyncExecute(UseMicrosoftTimeChanged, this, EventArgs.Empty);
			}
			get
			{
				bool appFailed = bool.Parse(GetValueAtKey("UseMicrosoftTime"));
				return appFailed;
			}
		}
		public bool EmailCodeLockToUser
		{
			set
			{
				WriteKeyValue("EmailCodeLockToUser", value.ToString());
				EventDispatcher.SyncExecute(UseMicrosoftTimeChanged, this, EventArgs.Empty);
			}
			get
			{
				bool appFailed = bool.Parse(GetValueAtKey("EmailCodeLockToUser"));
				return appFailed;
			}
		}

		public int TimeoutMS
		{
			set
			{
				WriteKeyValue("TimeoutMS", value.ToString());
			}
			get
			{
				int timeout = int.Parse(GetValueAtKey("TimeoutMS"));
				return timeout;
			}
		}
		public int CodeLength
		{
			set
			{
				if (CodeLength != value)
				{
					WriteKeyValue("CodeLength", value.ToString());
				}
			}
			get
			{
				int codeLength = int.Parse(GetValueAtKey("CodeLength"));
				return codeLength;
			}
		}
		public bool AppFailedWithInsufficientSecurityCredentials
		{
			set
			{
				WriteKeyValue("AppFailedWithInsufficientSecurityCredentials", value.ToString());
			}
			get
			{
				bool appFailed = bool.Parse(GetValueAtKey("AppFailedWithInsufficientSecurityCredentials"));
				return appFailed;
			}
		}
		public EncrypterDecrypter.DurationType DurationType
		{
			set
			{
				WriteKeyValue("DurationType", value.ToString());
			}
			get
			{
				EncrypterDecrypter.DurationType durationType =
					(EncrypterDecrypter.DurationType)Enum.Parse(
						typeof(EncrypterDecrypter.DurationType)
							, GetValueAtKey("DurationType")
						);
				return durationType;
			}
		}

		#endregion

		#region Abstract Override(s): AppSettings

		protected override void Init()
		{
			if (!File.Exists(_xmlFilePath) && !File.Exists(App.Instance.BackupSettingsFilePath))
			{
				CreateFactoryDefaultsAppSettings();
				File.Copy(_xmlFilePath, App.Instance.BackupSettingsFilePath, true);
			}
			else if (!File.Exists(_xmlFilePath) && File.Exists(App.Instance.BackupSettingsFilePath))
			{
				File.Copy(App.Instance.BackupSettingsFilePath, _xmlFilePath, true);
			}

			_xmlDoc = new XmlDocument();
			_xmlDoc.Load(_xmlFilePath);
		}
		protected override string GetDefaultSettingsFileContent()
		{
			string configFileContent = @"<configuration>
	  <appSettings>
		  <add key=""LastUnlockedCode"" value="""" />
		  <add key=""AppFailedWithInsufficientSecurityCredentials"" value=""false"" />
        <add key=""DurationType"" value=""Days"" />
        <add key=""UseMicrosoftTime"" value=""false"" />
        <add key=""TimeoutMS"" value=""3000"" />
        <add key=""CodeLength"" value=""4"" />
        <add key=""CodeLockCount"" value=""0"" />
        <add key=""UserEmail"" value =""NOEMAIL"" />
        <add key=""EmailCodeLockToUser"" value=""False"" />
	  </appSettings>
  </configuration>";
			return configFileContent;
		}

		#endregion
	}
}