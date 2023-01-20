using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Reflection;

using CabalsCorner.Utilities;

namespace CabalsCorner.CodeLocker.Classes
{
	#region Enumerated Type(s)

	internal enum CodeType
	{
		CreateRandomNumericCode
	,	CreateRandomNumbersAndLettersCode
	,  ManuallyEnterCode
	}; 

	#endregion

	internal class EncryptActionResult
	{
		#region Ctor(s)
		public EncryptActionResult(string origUnencryptedString, string encryptedString)
		{
			OriginalUnencryptedString = origUnencryptedString;
			EncryptedString = encryptedString;
		}

		#endregion

		#region Read-Only Fields
		
		public readonly string OriginalUnencryptedString;
		public readonly string EncryptedString;

		#endregion

	} // class EncryptActionResult

	internal class App
	{
		#region Ctor(s)

		private App() 
		{
			_expirationDate = new ExpirationDate();
		}

		#endregion

		#region Properties: Read-Only

		public static App Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new App();
				}
				return _instance;
			}
		}
		public string DefaultCodeLockFileName
		{
			get
			{
				return "code.clk";
			}
		}
		public string DefaultCodeLockFilePath
		{
			get
			{
				string p = Path.Combine(AppFolder, this.DefaultCodeLockFileName);
				return p;
			}
		}
		/*
		public string CodeLockFileFolder
		{
			get
			{
				string f = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

				return f;
			}
		}
		*/
		public string MyDocuments
		{
			get
			{
				string f = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

				return f;
			}
		}
		public string BackupCodeLockFilePath
		{
			get
			{
				//string path = Path.Combine(CodeLockFileFolder, "zBackup.bak");
				string path = Path.Combine(BackupFolder, @"zBackup.bak");
				return path;
			}
		}
		public string BackupSettingsFilePath
		{
			get
			{
				string path = Path.Combine(BackupFolder, "settings.xml");
				return path;
			}
		}
		public string AppFolder
		{
			get
			{
				string f = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				return f;
			}
		}

		public string DocumentsCodeLockerFolder
		{
			get
			{
				string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string codeLockerFolder = Path.Combine(documentsFolder, @"CodeLocker");

				return codeLockerFolder;
			}
		}
		public string BackupFolder
		{
			get
			{
				string backupFolderPath = Path.Combine(AppFolder, @"Backup");

				Directory.CreateDirectory(backupFolderPath);

				return backupFolderPath;
			}
		}
		public string CodeLockBackupFilePath
		{
			get
			{
				//string path = Path.Combine(CodeLockFileFolder, @"zBackup.bak");
				string path = Path.Combine(BackupFolder, @"zBackup.bak");

				return path;
			}
		}
		public ExpirationDate @ExpirationDate
		{
			get
			{
				return _expirationDate;
			}
		}

		public CodeLockerAppSettings Settings
		{
			get
			{
				var a = ConfigurationManager.AppSettings;
				int i = a.Count;
				string s = ConfigurationManager.AppSettings["CodePersistanceMgr.DefaultAppSettingsFilePath"];
				CodeLockerAppSettings settings = new CodeLockerAppSettings(s);
            return settings;
			}
		}

		public int DaysIndex
		{
			get
			{
				return DAYS_INDEX;
			}
		}
		public int HoursIndex
		{
			get
			{
				return HOURS_INDEX;
			}
		}
		public int MinutesIndex
		{
			get
			{
				return MINUTES_INDEX;
			}
		}
		public int SecondsIndex
		{
			get
			{
				return SECONDS_INDEX;
			}
		}

		public const int DAYS_INDEX = 0;
		public const int HOURS_INDEX = 1;
		public const int MINUTES_INDEX = 2;
		public const int SECONDS_INDEX = 3;

		public readonly TimeSpan MaxDuration = TimeSpan.FromDays(9125);

		#endregion

		#region Properties: Read/Write

		public string Key
		{
			get { return _key; }
			set { _key = value; }
		}

		public string EncryptedDuration
		{
			get { return _encryptedDuration; }
			set { _encryptedDuration = value; }
		}
		public string EncryptedStartingTime
		{
			get { return _encryptedStartingTime; }
			set { _encryptedStartingTime = value; }
		}

		#endregion

		#region Operations

		public EncryptActionResult EncryptCodeDurationAndStartingTime(
		  CodeType codeType
		, DateTime now
		, EncrypterDecrypter.DurationType durationType
		, string manuallyEnteredCode
		, string durationString
		, string codeLengthString
		)
		{
			EncryptActionResult result = null;
			try
			{
				//
				// encrypt code
				//
				if (codeType == CodeType.CreateRandomNumericCode)
				{
					result = EncryptRandomNumericCode(
					  Convert.ToInt64(codeLengthString)
					, durationType
					, double.Parse(durationString)
					);
				}
				else if (codeType == CodeType.CreateRandomNumbersAndLettersCode)
				{
					result = EncryptRandomAlphanumericCode(
					  Convert.ToInt32(codeLengthString)
					, durationType
					, double.Parse(durationString)
					);
				}
				else if (codeType == CodeType.ManuallyEnterCode)
				{
					EncrypterDecrypter ed = new EncrypterDecrypter();
					result = new EncryptActionResult(
					  manuallyEnteredCode
					, ed.CreateEncryptedString(manuallyEnteredCode, ed.ENCRYPTION_TABLE1)
					);
				}

				//
				// encrypt duration
				//
				double duration = double.Parse(durationString);
				EncryptActionResult r2 = EncryptCodeDuration(now, durationType, duration);
				EncryptedDuration = r2.EncryptedString;

				//
				// encrypt starting-time
				//
				EncryptActionResult r3 = EncryptCodeStartingTime(now);
				EncryptedStartingTime = r3.EncryptedString;

				return result;

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public EncryptActionResult EncryptRandomNumericCode(
		  long codeLength
		, EncrypterDecrypter.DurationType durationType
		, double duration
		)
		{
			//
			// create encrypted code
			//

			// create min value 

			long minValue = RandomCodeGenerator.MakeCombinationCodeMinValue(codeLength);

			// create max value
			long maxValue = RandomCodeGenerator.MakeCombinationCodeMaxValue(codeLength);

			string rndNumericCode = RandomCodeGenerator.CreateRandomNumericCode(minValue, maxValue);
			EncrypterDecrypter ed = new EncrypterDecrypter();
			string encryptedNumericCode = ed.CreateEncryptedString(rndNumericCode, ed.ENCRYPTION_TABLE1);

			EncryptActionResult result = new EncryptActionResult(rndNumericCode, encryptedNumericCode);
			return result;
		}
		public EncryptActionResult EncryptRandomAlphanumericCode(
		  int codeLength
		, EncrypterDecrypter.DurationType durationType
		, double duration
		)
		{
			//
			// create encrypted code
			//
			string rndNumericCode = RandomCodeGenerator.CreateRandomAlphaNumericString(Convert.ToInt32(codeLength));
			EncrypterDecrypter ed = new EncrypterDecrypter();
			string encryptedNumericCode = ed.CreateEncryptedString(rndNumericCode, ed.ENCRYPTION_TABLE1);

			EncryptActionResult result = new EncryptActionResult(rndNumericCode, encryptedNumericCode);
			return result;
		}
		public EncryptActionResult EncryptCodeDuration(
		  DateTime now
		, EncrypterDecrypter.DurationType durationType
		, double duration
		) {
			TimeSpan durationTs = GetRemainingDuration(now, durationType, duration);

			EncrypterDecrypter ed = new EncrypterDecrypter();
			string encryptedDurationString = ed.CreateEncryptedString(
			  durationTs.TotalSeconds.ToString()
			, ed.ENCRYPTION_TABLE2
			);

			string formattedDurationString = durationTs.ToString(@"dd\.hh\:mm\:ss");

			return new EncryptActionResult(formattedDurationString, encryptedDurationString);
		}
		public EncryptActionResult EncryptCodeStartingTime(DateTime now)
		{
			EncrypterDecrypter ed = new EncrypterDecrypter();
			TimeOps to = new TimeOps();

			now = GetAdjustedNow(now);

			long binaryStartingTime = now.ToBinary();
			string encryptedStartingTime = ed.CreateEncryptedString(binaryStartingTime.ToString(), ed.ENCRYPTION_TABLE3);

			return new EncryptActionResult(now.ToLongDateString() + " " + now.ToLongTimeString(), encryptedStartingTime);
		}
		public void GetDecryptedDurationAndStartingTime(
		  CodePersistanceMgr.State state
		, out TimeSpan duration
		, out DateTime startingTime
		)
		{
			//
			// retrieve and decrypt duration
			//

			// get duration string, convert to TimeSpan
			EncrypterDecrypter ed = new EncrypterDecrypter();
			string decryptedDuration = ed.CreateDecryptedString(state.EncryptedDuration, ed.ENCRYPTION_TABLE2);
			double seconds = double.Parse(decryptedDuration);
			duration = TimeSpan.FromSeconds(seconds);

			//
			// retrieve and decrypt starting-time
			//
			string decryptedStartingTime = ed.CreateDecryptedString(state.EncryptedStartingTime, ed.ENCRYPTION_TABLE3);
			long binaryStartingTime = long.Parse(decryptedStartingTime);
			startingTime = DateTime.FromBinary(binaryStartingTime);
		}
		
		public bool LockHasExpired(CodePersistanceMgr.State state, DateTime now)
		{
			DateTime expirationTime = GetLockExpirationTime(state);

			bool lockExpired = DateTime.Compare(now, expirationTime) > 0;
			return lockExpired;
		}
		public string GetDecryptedCode(CodePersistanceMgr.State state)
		{
			EncrypterDecrypter ed = new EncrypterDecrypter();
			string decryptedCode = ed.CreateDecryptedString(state.EncryptedCode, ed.ENCRYPTION_TABLE1);
			return decryptedCode;
		}
		public DateTime GetLockExpirationTime(CodePersistanceMgr.State state)
		{
			TimeSpan duration = new TimeSpan();
			DateTime startingTime;
			GetDecryptedDurationAndStartingTime(state, out duration, out startingTime);

			DateTime expirationTime = startingTime + duration;
			return expirationTime;
		}
		public TimeSpan GetRemainingDuration(
			  DateTime now
			, EncrypterDecrypter.DurationType durationType
			, double duration
		)
		{
			TimeSpan durationTs = new TimeSpan();
			if (ExpirationDate.Active)
			{
				durationTs = ExpirationDate.Value - now;
				//ExpirationDate.Deactivate();
			}
			else
			{
				switch (durationType)
				{
					case EncrypterDecrypter.DurationType.Seconds:
						durationTs = TimeSpan.FromSeconds(duration);
						break;
					case EncrypterDecrypter.DurationType.Minutes:
						durationTs = TimeSpan.FromMinutes(duration);
						break;
					case EncrypterDecrypter.DurationType.Hours:
						durationTs = TimeSpan.FromHours(duration);
						break;
					case EncrypterDecrypter.DurationType.Days:
						durationTs = TimeSpan.FromDays(duration);
						break;
				}
			}
			return durationTs;
		}
		public string MakeTimestamp(DateTime dt)
		{
			//string time = (dt.Hour >= 13 && dt.Hour <= 21) ? " " + dt.ToLongTimeString() : dt.ToLongTimeString();
			//string timestamp = dt.ToShortDateString() + ", " + time;
			string time = dt.ToString("HH:mm:ss");
			string date = dt.ToString("MM/dd/yy");
			string timestamp = date + ", " + time;

			return timestamp;
		}

		public void CreateDocumentsCodeLockerFolder()
		{
			Directory.CreateDirectory(App.Instance.DocumentsCodeLockerFolder);
		}

		#endregion

		#region Utilities

		private static DateTime GetAdjustedNow(DateTime now)
		{
			TimeSpan delta = DateTime.Now - now;
			if (delta.TotalSeconds > 0 && delta.TotalSeconds <= 30)
			{
				now = now + delta;
			}
			return now;
		}

		#endregion

		#region Private Fields

		private string _encryptedDuration;
		private string _encryptedStartingTime;

		private static App _instance = null;

		private string _key = null;

		private ExpirationDate _expirationDate = null;

		#endregion

	} // class App

} // namespace CabalsCorner.CodeLocker.Classes
