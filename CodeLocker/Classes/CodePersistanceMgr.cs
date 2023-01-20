using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IdentityModel;
using System.Text;
using System.Configuration;

using Microsoft.Win32;

using CabalsCorner.ErrorHandling;
using CabalsCorner.Utilities;

namespace CabalsCorner.CodeLocker.Classes
{
	internal class CodePersistanceMgr
	{
		internal class State
		{
			#region Ctor(s)

			public State(string code, string duration, string startingTime)
			{
				EncryptedCode = code;
				EncryptedDuration = duration;
				EncryptedStartingTime = startingTime;
			}

			#endregion

			#region Read-Only Fields

			public readonly string EncryptedCode;
			public readonly string EncryptedDuration;
			public readonly string EncryptedStartingTime;

			#endregion

			#region Object Overrides

			public override string ToString()
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(string.Format("Code: {0}", EncryptedCode));
				sb.AppendLine(string.Format("Duration: {0}", EncryptedDuration));
				sb.AppendLine(string.Format("StartingTime: {0}", EncryptedStartingTime));
				return sb.ToString();
			}

			#endregion
		}

		#region Constructor(s)

		public CodePersistanceMgr()
		{
			AppSettingsReader appSettings = new AppSettingsReader();
			DefaultResetValue = (string)appSettings.GetValue("CodePersistanceMgr.DefaultResetValue", typeof(string));
			DefaultTimeServersFilePath = (string)appSettings.GetValue("CodePersistanceMgr.DefaultTimeServersFilePath", typeof(string));
			DefaultLastUnlockedCodeFilePath = (string)appSettings.GetValue("CodePersistanceMgr.DefaultLastUnlockedCodeFilePath", typeof(string));
		}

		#endregion

		#region Operations

		public bool CodeIsLocked(string codeLockFile)
		{
			SaveCodeLockRecordIfNotExists(codeLockFile);
			State state = Load(codeLockFile);

			return (
				state.EncryptedCode != string.Empty && state.EncryptedCode != null && state.EncryptedCode != DefaultResetValue
			&& state.EncryptedDuration != string.Empty && state.EncryptedDuration != null && state.EncryptedDuration != DefaultResetValue
			&& state.EncryptedStartingTime != string.Empty && state.EncryptedStartingTime != null && state.EncryptedStartingTime != DefaultResetValue
			);
		}
		public bool StateExists(string codeLockFile)
		{
			if (!File.Exists(codeLockFile))
				throw new Exception(
					string.Format("CodePersistanceMgr.StateExists: persistance file '{0}' doesn't exist.", codeLockFile)
				);

			State state;
			try
			{
				state = Load(App.Instance.DefaultCodeLockFileName);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return (state.EncryptedCode != string.Empty && state.EncryptedCode != null);

		}
		public void Reset()
		{
			PersistCodeLockRecord(App.Instance.DefaultCodeLockFileName, DefaultResetValue, DefaultResetValue, DefaultResetValue);

		}
		public void CreateDocumentsCodeLockerFolder()
		{
			Directory.CreateDirectory(App.Instance.DocumentsCodeLockerFolder);
		}
		public State Load(string codeLockFile)
		{
			if (!File.Exists(codeLockFile))
				throw new Exception(
					string.Format("CodePersistanceMgr.Load: persistance file '{0}' doesn't exist.", codeLockFile)
				);

			State s = null;

			KeyMgr keyMgr = new KeyMgr();
			if (App.Instance.Key == null)
			{
				App.Instance.Key = keyMgr.ReadKey();
			}

			// Open the file to read from. 
			using (StreamReader sr = AES.DecryptFile(codeLockFile, App.Instance.Key))
			{
				string line = sr.ReadLine();
				if (line == null)
					throw new ArgumentNullException(
						string.Format("CodePersistanceMgr.StateExists: failed to read code line from persistance file '{0}'", codeLockFile)
					);
				string code = line;

				line = sr.ReadLine();
				if (line == null)
					throw new ArgumentNullException(
						string.Format("CodePersistanceMgr.StateExists: failed to read duration line from persistance file '{0}'", codeLockFile)
					);
				string duration = line;

				line = sr.ReadLine();
				if (line == null)
					throw new ArgumentNullException(
						string.Format("CodePersistanceMgr.StateExists: failed to read starting-time line from persistance file '{0}'", codeLockFile)
					);
				string startingTime = line;

				s = new State(code, duration, startingTime);
			}
			return s;
		}
		public void Save(string codeLockFile, State state)
		{
			if (!File.Exists(codeLockFile))
				throw new Exception(
					string.Format("CodePersistanceMgr.Load: persistance file '{0}' doesn't exist.", codeLockFile)
				);

			PersistCodeLockRecord(App.Instance.DefaultCodeLockFileName, state.EncryptedCode, state.EncryptedDuration, state.EncryptedStartingTime);

			File.Copy(codeLockFile, App.Instance.BackupCodeLockFilePath, true);
		}
		public void Import(string codeLockFile, string importCodeLockFile)
		{
			if (ValidateCodeLockFile(importCodeLockFile) && File.Exists(importCodeLockFile))
         {
				File.Copy(importCodeLockFile, codeLockFile, true);
				File.Copy(importCodeLockFile, App.Instance.BackupCodeLockFilePath, true);
			}
		}
		public void Export(string codeLockFile, string exportCodeLockFile)
		{
			if (File.Exists(codeLockFile))
			{
				File.Copy(codeLockFile, exportCodeLockFile, true);
         }
		}
		public bool ValidateCodeLockFile(string codeLockFile)
		{
			bool valid = true;
			if (!File.Exists(codeLockFile))
			{
				valid = false;
			}
			else
			{
				try
				{
					Load(codeLockFile);
				}
				catch (Exception ex)
				{
					valid = false;
				}
			}
			return valid;
		}
		public void SaveCodeLockRecordIfNotExists(string codeLockFile)
		{
			if (File.Exists(codeLockFile))
				return;

			// Create a file to write to. 
			PersistCodeLockRecord(App.Instance.DefaultCodeLockFileName, DefaultResetValue, DefaultResetValue, DefaultResetValue);
		}
		public void SaveTimeServersFileIfNotExists()
		{
			if (File.Exists(DefaultTimeServersFilePath))
				return;

			// Create a file to write to. 
			PersistTimeServersFile();
		}
		public void SaveLastUnlockedCodeIfNotExists(string lastUnlockedCodeString)
		{
			CodeLockerAppSettings settings = App.Instance.Settings;
			settings.LastUnlockedCode = lastUnlockedCodeString;
		}
		public void DeleteLastUnlockedCode()
		{
			App.Instance.Settings.LastUnlockedCode = string.Empty;
		}

		public string[] GetTimeServers()
		{
			List<string> servers = new List<string>();
			using (StreamReader reader = File.OpenText(DefaultTimeServersFilePath))
			{
				string line = string.Empty;
				while ((line = reader.ReadLine()) != null)
				{
					if (line.Trim().StartsWith("#"))
					{
						continue;
					}
					if (line.Trim() != string.Empty && line != null)
					{
						servers.Add(line);
					}
				}
			}
			return (string[])servers.ToArray();
		}
		public bool LastUnlockedCodeFileExists()
		{
			return App.Instance.Settings.LastUnlockedCode != string.Empty;
		}
		public string GetLastUnlockedCode()
		{
			return App.Instance.Settings.LastUnlockedCode;
		}

		#endregion

		#region Utilities

		private void PersistCodeLockRecord(string codeLockFile, string encryptedCode, string encryptedDuration, string encryptedStartingTime)
		{
			// Create a file to write to. 
			using (StreamWriter sw = File.CreateText(codeLockFile))
			{
				//
				// Encrypt each code a second time
				//
				sw.WriteLine(encryptedCode);
				sw.WriteLine(encryptedDuration);
				sw.WriteLine(encryptedStartingTime);
			}

			KeyMgr keyMgr = new KeyMgr();
			App.Instance.Key = keyMgr.CreateKey();
			AES.EncryptFile(codeLockFile, App.Instance.Key, codeLockFile);
		}
		private void PersistTimeServersFile()
		{
			// Create a file to write to. 
			using (StreamWriter sw = File.CreateText(DefaultTimeServersFilePath))
			{
				// Represents the list of NIST servers
				/*
				string[] servers = {	
					"tick.usno.navy.mil"
				,	"time-a.nist.gov"
				,	"time-b.nist.gov"
				,	"time-a.timefreq.bldrdoc.gov"
				,	"time-b.timefreq.bldrdoc.gov"
				,	"time-c.timefreq.bldrdoc.gov"
				,	"utcnist.colorado.edu"
				,	"time.nist.gov"
				,	"time-nw.nist.gov"
				,	"nist1.datum.com"
				,	"nist1.dc.certifiedtime.com"
				,	"nist1.nyc.certifiedtime.com"
				,	"nist1.sjc.certifiedtime.com"
				};
				*/
				string[] servers = {
					"tick.usno.navy.mil"
				,  "utcnist.colorado.edu"
				,  "time.nist.gov"
				};

				foreach (string server in servers)
				{
					sw.WriteLine(server);
				}
			}
		}
		private void PersistLastUnlockedCodeFile(string lastUnlockedCodeString)
		{
			using (StreamWriter sw = File.CreateText(DefaultLastUnlockedCodeFilePath))
			{
				sw.WriteLine(lastUnlockedCodeString);
			}
		}

		#endregion

		#region Private Fields

		private const string ENCRYPTION_PASSWORD = "yTT4AFY87jZ1o7Se0g";

		private readonly string DefaultResetValue;
		private readonly string DefaultTimeServersFilePath;
		private readonly string DefaultLastUnlockedCodeFilePath;

		#endregion

	} // class CodePersistanceMgr

} // namespace CabalsCorner.CodeLocker.Classes
