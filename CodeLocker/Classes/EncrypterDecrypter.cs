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

using CabalsCorner.ErrorHandling;
using CabalsCorner.Utilities;

namespace CabalsCorner.CodeLocker.Classes
{
	internal class EncryptCodeResult
	{
		#region Ctor(s)

		public EncryptCodeResult()
		{

		}

		#endregion
	}

	/// <summary>
	/// TODO
	/// - rewrite CreateEncryptedCode/CreateDecryptedCode
	/// - make main methods return result object
	/// - start working on actual CodeLocker winforms app
	/// - get pseudocode written
	/// </summary>
	internal class EncrypterDecrypter
	{
		#region Readonly Fields

		internal readonly Dictionary<string, string> ENCRYPTION_TABLE1 = new Dictionary<string, string>() 
			{
			  {"a", "}"}
			, {"b", "8"}
			, {"c", "a"}
			, {"d", "H"}
			, {"e", "U"}
			, {"f", "b"}
			, {"g", "="}
			, {"h", "c"}
			, {"i", "'"}
			, {"j", "A"}
			, {"k", "d"}
			, {"l", "M"}
			, {"m", "e"}
			, {"n", "%"}
			, {"o", "^"}
			, {"p", "f"}
			, {"q", "9"}
			, {"r", ")"}
			, {"s", "g"}
			, {"t", "["}
			, {"u", "h"}
			, {"v", "|"}
			, {"w", ","}
			, {"x", "i"}
			, {"y", "\""}
			, {"z", "j"}
			, {"`", "C"}
			, {"~", "F"}
			, {"1", "k"}
			, {"!", "K"}
			, {"2", "l"}
			, {"@", "P"}
			, {"3", "S"}
			, {"#", "m"}
			, {"4", "X"}
			, {"$", "Z"}
			, {"5", "n"}
			, {"%", "6"}
			, {"6", "o"}
			, {"^", "7"}
			, {"7", "&"}
			, {"&", "p"}
			, {"8", "*"}
			, {"*", "q"}
			, {"9", "("}
			, {"(", "0"}
			, {"0", "r"}
			, {")", "-"}
			, {"-", "_"}
			, {"_", "s"}
			, {"=", "+"}
			, {"+", "t"}
			, {"[", "{"}
			, {"{", "]"}
			, {"]", "u"}
			, {"}", "\\"}
			, {"\\", "v"}
			, {"|", ";"}
			, {";", ":"}
			, {":", "w"}
			, {",", "<"}
			, {"<", "x"}
			, {"'", "."}
			, {".", ">"}
			, {">", "y"}
			, {"\"", "/"}
			, {"/", "?"}
			, {"?", "z"}
			, {"A", "B"}
			, {"B", "`"}
			, {"C", "D"}
			, {"D", "E"}
			, {"E", "~"}
			, {"F", "G"}
			, {"G", "1"}
			, {"H", "I"}
			, {"I", "J"}
			, {"J", "!"}
			, {"K", "L"}
			, {"L", "2"}
			, {"M", "N"}
			, {"N", "O"}
			, {"O", "@"}
			, {"P", "Q"}
			, {"Q", "R"}
			, {"R", "3"}
			, {"S", "T"}
			, {"T", "#"}
			, {"U", "V"}
			, {"V", "W"}
			, {"W", "4"}
			, {"X", "Y"}
			, {"Y", "$"}
			, {"Z", "5"}
			};

		internal readonly Dictionary<string, string> ENCRYPTION_TABLE2 = new Dictionary<string, string>() 
			{
			{"a", "J"}
			, {"b", "a"}
			, {"c", "b"}
			, {"d", "G"}
			, {"e", "c"}
			, {"f", "d"}
			, {"g", "e"}
			, {"h", "?"}
			, {"i", "f"}
			, {"j", "g"}
			, {"k", "U"}
			, {"l", "h"}
			, {"m", "i"}
			, {"n", "j"}
			, {"o", "P"}
			, {"p", "k"}
			, {"q", "l"}
			, {"r", "F"}
			, {"s", "m"}
			, {"t", "n"}
			, {"u", "o"}
			, {"v", "T"}
			, {"w", "p"}
			, {"x", "q"}
			, {"y", "B"}
			, {"z", "r"}
			, {"`", "s"}
			, {"~", "t"}
			, {"1", "N"}
			, {"!", "u"}
			, {"2", "v"}
			, {"@", "w"}
			, {"3", "W"}
			, {"#", "x"}
			, {"4", "y"}
			, {"$", "D"}
			, {"5", "z"}
			, {"%", "`"}
			, {"6", "~"}
			, {"^", "K"}
			, {"7", "1"}
			, {"&", "!"}
			, {"8", "Q"}
			, {"*", "2"}
			, {"9", "@"}
			, {"(", "3"}
			, {"0", "Y"}
			, {")", "#"}
			, {"-", "4"}
			, {"_", "C"}
			, {"=", "$"}
			, {"+", "5"}
			, {"[", "%"}
			, {"{", "I"}
			, {"]", "6"}
			, {"}", "^"}
			, {"\\", "M"}
			, {"|", "7"}
			, {";", "&"}
			, {":", "8"}
			, {",", "R"}
			, {"<", "*"}
			, {"'", "9"}
			, {".", "("}
			, {">", "X"}
			, {"\"", "0"}
			, {"/", ")"}
			, {"?", "A"}
			, {"A", "-"}
			, {"B", "_"}
			, {"C", "="}
			, {"D", "E"}
			, {"E", "+"}
			, {"F", "["}
			, {"G", "H"}
			, {"H", "{"}
			, {"I", "]"}
			, {"J", "}"}
			, {"K", "L"}
			, {"L", "\\"}
			, {"M", "|"}
			, {"N", "O"}
			, {"O", ";"}
			, {"P", ":"}
			, {"Q", ","}
			, {"R", "S"}
			, {"S", "<"}
			, {"T", "'"}
			, {"U", "V"}
			, {"V", "."}
			, {"W", ">"}
			, {"X", "\""}
			, {"Y", "Z"}
			, {"Z", "/"}
			};

		internal readonly Dictionary<string, string> ENCRYPTION_TABLE3 = new Dictionary<string, string>() 
			{
			{"a", ":"}
			, {"b", "*"}
			, {"c", "a"}
			, {"d", "M"}
			, {"e", "6"}
			, {"f", "b"}
			, {"g", "+"}
			, {"h", "c"}
			, {"i", ">"}
			, {"j", "E"}
			, {"k", "d"}
			, {"l", "S"}
			, {"m", "$"}
			, {"n", "e"}
			, {"o", "7"}
			, {"p", "f"}
			, {"q", "0"}
			, {"r", "-"}
			, {"s", "g"}
			, {"t", "]"}
			, {"u", "\\"}
			, {"v", "h"}
			, {"w", "'"}
			, {"x", "i"}
			, {"y", "?"}
			, {"z", "C"}
			, {"`", "j"}
			, {"~", "H"}
			, {"1", "K"}
			, {"!", "k"}
			, {"2", "P"}
			, {"@", "l"}
			, {"3", "U"}
			, {"#", "X"}
			, {"4", "m"}
			, {"$", "5"}
			, {"5", "%"}
			, {"%", "n"}
			, {"6", "^"}
			, {"^", "o"}
			, {"7", "&"}
			, {"&", "8"}
			, {"8", "p"}
			, {"*", "9"}
			, {"9", "("}
			, {"(", "q"}
			, {"0", ")"}
			, {")", "r"}
			, {"-", "_"}
			, {"_", "="}
			, {"=", "s"}
			, {"+", "["}
			, {"[", "{"}
			, {"{", "t"}
			, {"]", "}"}
			, {"}", "u"}
			, {"\\", "|"}
			, {"|", ";"}
			, {";", "v"}
			, {":", ","}
			, {",", "<"}
			, {"<", "w"}
			, {"'", "."}
			, {".", "x"}
			, {">", "\""}
			, {"\"", "/"}
			, {"/", "y"}
			, {"?", "A"}
			, {"A", "B"}
			, {"B", "z"}
			, {"C", "D"}
			, {"D", "`"}
			, {"E", "F"}
			, {"F", "G"}
			, {"G", "~"}
			, {"H", "I"}
			, {"I", "J"}
			, {"J", "1"}
			, {"K", "L"}
			, {"L", "!"}
			, {"M", "N"}
			, {"N", "O"}
			, {"O", "2"}
			, {"P", "Q"}
			, {"Q", "R"}
			, {"R", "@"}
			, {"S", "T"}
			, {"T", "3"}
			, {"U", "V"}
			, {"V", "W"}
			, {"W", "#"}
			, {"X", "Y"}
			, {"Y", "Z"}
			, {"Z", "4"}
			};

		internal readonly Dictionary<string, string> ENCRYPTION_TABLE4 = new Dictionary<string, string>() 
		{
		{"a", "L"}
		, {"b", "{"}
		, {"c", "a"}
		, {"d", "$"}
		, {"e", "9"}
		, {"f", "b"}
		, {"g", "<"}
		, {"h", "C"}
		, {"i", "c"}
		, {"j", "Q"}
		, {"k", "Z"}
		, {"l", "d"}
		, {"m", "6"}
		, {"n", "&"}
		, {"o", "e"}
		, {"p", ")"}
		, {"q", "="}
		, {"r", "f"}
		, {"s", "}"}
		, {"t", ";"}
		, {"u", "g"}
		, {"v", ">"}
		, {"w", "?"}
		, {"x", "h"}
		, {"y", "F"}
		, {"z", "I"}
		, {"`", "i"}
		, {"~", "N"}
		, {"1", "j"}
		, {"!", "T"}
		, {"2", "W"}
		, {"@", "k"}
		, {"3", "#"}
		, {"#", "4"}
		, {"4", "l"}
		, {"$", "5"}
		, {"5", "%"}
		, {"%", "m"}
		, {"6", "^"}
		, {"^", "7"}
		, {"7", "n"}
		, {"&", "8"}
		, {"8", "*"}
		, {"*", "o"}
		, {"9", "("}
		, {"(", "0"}
		, {"0", "p"}
		, {")", "-"}
		, {"-", "_"}
		, {"_", "q"}
		, {"=", "+"}
		, {"+", "["}
		, {"[", "r"}
		, {"{", "]"}
		, {"]", "s"}
		, {"}", "\\"}
		, {"\\", "|"}
		, {"|", "t"}
		, {";", ":"}
		, {":", ","}
		, {",", "u"}
		, {"<", "'"}
		, {"'", "."}
		, {".", "v"}
		, {">", "\""}
		, {"\"", "/"}
		, {"/", "w"}
		, {"?", "A"}
		, {"A", "B"}
		, {"B", "x"}
		, {"C", "D"}
		, {"D", "E"}
		, {"E", "y"}
		, {"F", "G"}
		, {"G", "H"}
		, {"H", "z"}
		, {"I", "J"}
		, {"J", "K"}
		, {"K", "`"}
		, {"L", "M"}
		, {"M", "~"}
		, {"N", "O"}
		, {"O", "P"}
		, {"P", "1"}
		, {"Q", "R"}
		, {"R", "S"}
		, {"S", "!"}
		, {"T", "U"}
		, {"U", "V"}
		, {"V", "2"}
		, {"W", "X"}
		, {"X", "Y"}
		, {"Y", "@"}
		, {"Z", "3"}
		};


		#endregion

		#region Enumerated Types

		public enum DurationType { Seconds, Minutes, Hours, Days };

		#endregion

		#region Events

		#region EVENT: RandomNumberChanged

		/// <summary>
		/// ValueObjectAddedArgs is argument supplied when RandomNumberChanged is fired.
		/// </summary>
		public class RandomNumberChangedArgs : EventArgs
		{
			public RandomNumberChangedArgs(int randomNumber)
			{
				RandomNumber = randomNumber;
			}

			public readonly int RandomNumber;
		}

		/// <summary>
		/// RandomNumberChanged event delegate
		/// </summary>
		public delegate void RandomNumberChangedHandler(object sender, RandomNumberChangedArgs e);

		/// <summary>
		/// RandomNumberChanged event
		/// </summary>
		[Category("DataBindings")]
		public event RandomNumberChangedHandler RandomNumberChanged;

		public void FireRandomNumberChanged(int randomNumber)
		{
			EventDispatcher.SyncExecute(RandomNumberChanged, this, new RandomNumberChangedArgs(randomNumber));
		}

		#endregion

		#endregion

		#region Operations

		public string CreateEncryptedDateTime(
		  DateTime dateTime
		, Dictionary<string, string> table
		)
		{
			// MM/dd/yy HH:mm:ss
			//string dtStr = dateTime.ToString("MM/dd/yy HH:mm:ss");
			long encryptedDateTime = dateTime.ToBinary();
			string enc = encryptedDateTime.ToString();

			string encryptedStr = CreateEncryptedString(enc, table);

			return encryptedStr;
		}

		public string CreateEncryptedDuration(
		  EncrypterDecrypter.DurationType durationType
		, int duration
		, Dictionary<string, string> table
		)
		{
			TimeSpan t = new TimeSpan();
			switch (durationType)
			{
				case DurationType.Seconds:
					t = new TimeSpan(0, 0, duration);
					break;
				case DurationType.Minutes:
					t = new TimeSpan(0, duration, 0);
					break;
				case DurationType.Hours:
					t = new TimeSpan(duration, 0, 0);
					break;
				case DurationType.Days:
					t = new TimeSpan(duration, 0, 0, 0);
					break;
			}

			string encryptedStr = CreateEncryptedString(t.TotalSeconds.ToString(), table);

			return encryptedStr;
		}

		#endregion

		#region Operations: Inner Ring

		public string CreateEncryptedString(string s, Dictionary<string, string> table)
		{
			Debug.Assert(s != string.Empty && s != null);
			if (table.Count == 0)
				throw new ArgumentException("EncrypterDecrypter.CreateEncryptedString: argument 'table' must be populated.");

			StringBuilder sb = new StringBuilder();
			foreach (char ch in s.ToCharArray())
			{
				string chStr = new string(new char[] { ch });

				sb.Append(table[chStr]);
			}

			return sb.ToString();
		}
		public string CreateDecryptedString(string encryptedString, Dictionary<string, string> table)
		{
			Debug.Assert(encryptedString != string.Empty && encryptedString != null);
			if (table.Count == 0)
				throw new ArgumentException("EncrypterDecrypter.CreateDecryptedString: argument 'table' must be populated.");

			StringBuilder sb = new StringBuilder();
			foreach (char ch in encryptedString.ToCharArray())
			{
				string chStr = new string(new char[] { ch });

				string key = null;
				foreach (KeyValuePair<string, string> pair in table)
				{
					if (pair.Value == chStr)
					{
						key = pair.Key;
						break;
					}
				}

				sb.Append(key);
			}

			return sb.ToString();
		}

		#endregion
	}
}
