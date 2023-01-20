using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections;
using Microsoft.Win32;
using System.Reflection;

namespace CabalsCorner.Utilities
{
	/// <summary>
	/// Offers methods that save the public properties of the object 'instance' (see ctor) to the registry, 
	/// and load public properties from the registry.
	/// </summary>
	public sealed class RegistryBroker
	{
		#region Ctor(s)

		public RegistryBroker(object instance)
		{
			this.instance = instance;
		} 

		#endregion

		#region Operations

		public string[] GetInstancePropertyNames()
		{
			return Reflector.GetProperyNames(this.instance);
		}

		/// <summary>
		/// Loads public properties of this object instance  from registry key and its associated key values.  If
		/// each 'keyValueName' is assigned to a property of this instance via reflection.
		/// </summary>
		public void Load(
			RegistryKey parentKey,
			string keyName,
			string[] keyValueNames,
			object[] keyValueDefaultData
			)
		{
			Debug.Assert(keyValueNames.Length == keyValueDefaultData.Length);
			if (this.instance == null)
				throw new NullReferenceException("RegistryBroker.Load: this.instance is null, fiend");
			if (parentKey == null)
				throw new NullReferenceException("RegistryBroker.Load: argument 'parentKey' is null.");

			// Bouncer method
			ValidateDefaultValueData(keyValueDefaultData);

			// Create the registry entries if they don't exist and set default values
			CreateKeyAndValuesIfDoNotExist(parentKey, keyName, keyValueNames, keyValueDefaultData);

			using (RegistryKey key = parentKey.CreateSubKey(keyName))
			{
				if (key == null)
					throw new Exception(string.Format("RegistryBroker.Load: failed to create sub-key '{0}'", keyName));

				for (int i = 0; i < keyValueNames.Length; i++)
				{
					// get the object value
					object value = key.GetValue(keyValueNames[i]);

					if (value == null)
						value = string.Empty;

					// find PropertyDescriptor based on argument 'keyValueNames[i]'
					PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this.instance);
					Debug.Assert(props != null && props.Count > 0);
					PropertyDescriptor prop = props.Find(keyValueNames[i], false);

					// covert object value to string
					string s = TypeDescriptor.GetConverter(value.GetType()).ConvertToString(value);
					if (s == null)
						continue;

					// get actual value of object via PropertyDescriptor's TypeConverter;
					// set it
					object actualValue = prop.Converter.ConvertFromString(s);
					if (actualValue != null)
						prop.SetValue(this.instance, actualValue);
				}
			}

		}
		/// <summary>
		/// Persists public properties of this instance object into the registry.
		/// Argument 'keyValueNames' are loaded from public properties of this object and
		/// persisted in the Windows 95 registry.
		/// </summary>
		public void Save(
			RegistryKey parentKey,
			string keyName,
			string[] keyValueNames,
			object[] keyValueDefaultData
			)
		{
			Debug.Assert(keyValueNames.Length == keyValueDefaultData.Length);
			Debug.Assert(keyValueNames != null && keyValueNames.Length > 0);
			Debug.Assert(keyValueDefaultData != null && keyValueDefaultData.Length > 0);

			// Bouncer method
			ValidateDefaultValueData(keyValueDefaultData);

			if (this.instance == null)
				throw new Exception("RegistryBroker.Save: this.instance is null, fiend");

			// Create the registry entries if they don't exist and set default values
			CreateKeyAndValuesIfDoNotExist(parentKey, keyName, keyValueNames, keyValueDefaultData);

			using (RegistryKey key = parentKey.CreateSubKey(keyName))
			{
				if (key == null)
					throw new Exception(string.Format("RegistryBroker.Save: failed to create sub-key '{0}'", keyName));

				// for each key, load this object from the registry key's values; make the
				// assignment by reflecting over the argument 'keyValueNames' 
				for (int i = 0; i < keyValueNames.Length; i++)
				{
					string keyValueName = keyValueNames[i];
					if (keyValueName == null || keyValueName == string.Empty)
						throw new Exception(string.Format("RegistryBroker.Save: keyValueName element in argument 'keyValueNames' at index {0} is a null/empty string.", i));

					// Use reflection to get property value
					PropertyInfo prop = instance.GetType().GetProperty(keyValueName);
					if (prop == null)
						continue;

					// ...assigned key value to property
					//key.SetValue(keyValueName, prop.GetValue(instance, null).ToString());
					key.SetValue(keyValueName, prop.GetValue(instance, null), GetRegistryValueKind(prop.GetValue(instance, null)));
				}
			}
		}
		/// <summary>
		/// If argument key does not exist, method creates key, in which case default key values are assigned 
		/// from 'keyValueDefaultData' arguments if not already in registry
		/// </summary>
		public void CreateKeyAndValuesIfDoNotExist(
			RegistryKey parentKey,
			string keyName,
			string[] keyValueNames,
			object[] keyValueDefaultData
			)
		{
			Debug.Assert(keyValueNames.Length == keyValueDefaultData.Length);
			Debug.Assert(keyValueNames != null && keyValueNames.Length > 0);
			Debug.Assert(keyValueDefaultData != null && keyValueDefaultData.Length > 0);

			using (RegistryKey key = parentKey.CreateSubKey(keyName, RegistryKeyPermissionCheck.Default))
			{
				if (key == null)
					throw new Exception(string.Format("RegistryBroker.CreateKeyAndValuesIfDoNotExist: failed to create sub-key '{0}'", keyName));


				for (int i = 0; i < keyValueNames.Length; i++)
				{
					string keyValueName = keyValueNames[i];
					if (keyValueName == null)
						throw new Exception(string.Format("RegistryBroker.CreateKeyAndValuesIfDoNotExist: keyValueName element in argument 'keyValueNames' at index {0} is a null/empty string.", i));

					object keyVDefaultDataItem = keyValueDefaultData[i];
					if (keyVDefaultDataItem == null)
						throw new Exception(string.Format("CreateKeyAndValuesIfDoNotExist.CreateKeyAndValuesIfDoNotExist: keyVDefaultDataItem element in argument 'keyValueDefaultData' at index {0} is a null/empty string.", i));

					// fetch property value from registry
					object result = key.GetValue(keyValueNames[i]);

					// if key value does exist for key, assign argument default value
					if (result == null)
						key.SetValue(keyValueName, keyVDefaultDataItem, GetRegistryValueKind(keyVDefaultDataItem));
				}
			}
		} 

		#endregion

		#region Utilities

		private RegistryValueKind GetRegistryValueKind(object regValue)
		{
			RegistryValueKind regValueKind = RegistryValueKind.String;
			if (regValue.GetType() == typeof(int))
			{
				regValueKind = RegistryValueKind.DWord;
			}
			else if (regValue.GetType() == typeof(string))
			{
				regValueKind = RegistryValueKind.String;
			}
			return regValueKind;
		}
		private void ValidateDefaultValueData(object[] keyValueDefaultData)
		{
			foreach (object item in keyValueDefaultData)
			{
				if (item == null)
					throw new NullReferenceException("RegistryBroker.ValidateDefaultValueData: default object[] data array contains a null element.");
			}
		} 

		#endregion

		#region Private Fields

		private object instance; 

		#endregion

	} // class RegistryBroker
} // namespace CabalsCorner.Utilities
