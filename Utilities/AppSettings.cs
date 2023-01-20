using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace CabalsCorner.Utilities
{
	public abstract class AppSettings
	{
		#region Ctor(s)

		public AppSettings(string xmlFilePath)
		{
			_xmlFilePath = xmlFilePath;
			Init();
		}

		#endregion

		#region Operations

		public void WriteKeyValue(string key, string value)
		{
			XmlNodeList keyValueList = _xmlDoc.DocumentElement.ChildNodes[0].ChildNodes;
			foreach (XmlNode xmlNode in keyValueList)
			{
				if (xmlNode.Attributes["key"].Value.Equals(key))
				{
					xmlNode.Attributes["value"].Value = value;
				}
			}

			_xmlDoc.Save(_xmlFilePath);
		}
		public string GetValueAtKey(string key)
		{
			XmlNodeList keyValueList = _xmlDoc.DocumentElement.ChildNodes[0].ChildNodes;
			string keyAttribValue = null;
			foreach (XmlNode xmlNode in keyValueList)
			{
				if (xmlNode.Attributes["key"].Value.Equals(key))
				{
					keyAttribValue = xmlNode.Attributes["value"].Value;
					return keyAttribValue;
				}
			}
			return keyAttribValue;
		}

		#endregion

		#region Abstract Method(s)

		protected abstract void Init(); 
		protected abstract string GetDefaultSettingsFileContent(); 

		#endregion

		#region Utilities

		protected void CreateFactoryDefaultsAppSettings()
		{
			using (StreamWriter sw = File.CreateText(_xmlFilePath))
			{
				sw.Write(GetDefaultSettingsFileContent());
			}
		}

		#endregion

		#region Protected Fields

		protected XmlDocument _xmlDoc = null;
		protected readonly string _xmlFilePath = null;

		#endregion
	}
}
