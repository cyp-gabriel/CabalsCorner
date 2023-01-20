using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections;
using System.Reflection;

namespace CabalsCorner.Utilities
{
	public static class Reflector
	{
		#region Class Operations

		public static string[] GetProperyNames(object instance, BindingFlags bindingFlags)
		{
			if (instance == null) 
				throw new NullReferenceException("Reflector.GetProperyNames: argument 'instance' is null.");

			PropertyInfo[] propInfos = instance.GetType().GetProperties(bindingFlags);
			Debug.Assert(propInfos != null);

			ArrayList names = new ArrayList();
			foreach (PropertyInfo prop in propInfos)
			{
				string name = prop.Name;
				Debug.Assert(name != null && name != string.Empty);

				Type i = prop.PropertyType.GetInterface("ICollection");
				if (i != null) continue;

				names.Add(prop.Name);
			}

			return (string[])names.ToArray(typeof(string));
		}
		public static string[] GetProperyNames(object instance)
		{
			return Reflector.GetProperyNames(instance, BindingFlags.Public | BindingFlags.Instance);
		}
		public static object[] GetProperyValues(object instance, BindingFlags bindingFlags)
		{
			if (instance == null) 
				throw new NullReferenceException("Reflector.GetProperyValues: argument 'instance' is null.");

			string[] propNames = Reflector.GetProperyNames(instance, bindingFlags);
			if (propNames == null) 
				return null;

			ArrayList values = new ArrayList();
			foreach (string propName in propNames)
			{
				PropertyInfo propInfo = instance.GetType().GetProperty(propName);
				if (propInfo == null) 
					continue;

				object value = propInfo.GetValue(instance, null);
				if (value == null) 
					continue;

				values.Add(value);
			}

			return (object[])values.ToArray(typeof(object));
		}
		public static object[] GetProperyValues(object instance)
		{
			return Reflector.GetProperyValues(instance, BindingFlags.Public | BindingFlags.Instance);
		}
		public static object GetPropertyValue(object instance, string propName)
		{
			if (instance == null)
				throw new NullReferenceException("Reflector.GetProperyValue: argument 'instance' is null.");

			PropertyInfo propInfo = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
			if (propInfo == null) 
				throw new Exception(string.Format("Reflector.GetProperyValue: failed to extract property '{0}'.", propName));

			object value = propInfo.GetValue(instance, null);
			return value;
		}
		public static object GetPropertyValue(object instance, string propName, BindingFlags bindingFlags)
		{
			if (instance == null)
				throw new NullReferenceException("Reflector.GetProperyValue: argument 'instance' is null.");

			PropertyInfo propInfo = instance.GetType().GetProperty(propName, bindingFlags);
			if (propInfo == null)
				throw new Exception(string.Format("Reflector.GetProperyValue: failed to extract property '{0}'.", propName));

			object value = propInfo.GetValue(instance, null);
			return value;
		}
		public static bool ObjectHasMethod(this object objectToCheck, string methodName)
		{
			var type = objectToCheck.GetType();
			return type.GetMethod(methodName) != null;
		}
		public static bool ObjectHasProperty(object objectToCheck, string propertyName)
		{
			PropertyInfo property = objectToCheck.GetType().GetProperty(propertyName);
			return property != null;
		}
		public static object[] GetPropertyAttributes(object instance,  string propertyName)
		{
			PropertyInfo property = instance.GetType().GetProperty(propertyName);
			object[] attrs = property.GetCustomAttributes(true);
			return attrs;
		}
		public static void SetPropertyValue(object instance, string propertyName, object propertyValue)
		{
			PropertyInfo property = instance.GetType().GetProperty(propertyName);
			if (Reflector.ObjectHasProperty(instance, propertyName)) 
			{
				property.SetValue(instance, propertyValue);
			}
		}

		#endregion
	}
}
