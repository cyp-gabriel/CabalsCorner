using System;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.Win32;
using System.ComponentModel;

using CabalsCorner.Utilities.CustomDataSource;

namespace CabalsCorner.Utilities
{
	/// <summary>
	/// Manages program settings and persists them in the registry.
	/// </summary>
	public class RegistrySettings : IEditableObject
	{
		#region Events

		public event EventHandler WindowCenteredChanged;

		#endregion

		#region Constructor(s)

		public RegistrySettings()
		{
			this.regBroker = new RegistryBroker(this);
		}

		#endregion

		#region Properties: Read/write

		public bool WindowCentered
		{
			get
			{
				return bool.Parse(_slideShowRunning);
			}
			set
			{
				_slideShowRunning = value.ToString();
				EventDispatcher.SyncExecute(WindowCenteredChanged, this, EventArgs.Empty);
			}
		}
		public int WindowWidth
		{
			get
			{
				return int.Parse(_windowWidth);
			}
			set
			{
				_windowWidth = value.ToString();
				//EventDispatcher.SyncExecute(this.WindowWidthChanged, this, EventArgs.Empty);
			}
		}
		public int WindowHeight
		{
			get
			{
				return int.Parse(_windowHeight);
			}
			set
			{
				_windowHeight = value.ToString();
				//EventDispatcher.SyncExecute(this.WindowHeightChanged, this, EventArgs.Empty);
			}
		}

		#endregion

		#region IEditableObject: Operations

		private bool IsEdit
		{
			get { return (_OriginalValues != null); }
		}
		public void BeginEdit()
		{
			if (!IsEdit)
			{
				PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this, null);
				object[] vals = new object[props.Count];
				for (int i = 0; i < props.Count; ++i)
				{
					vals[i] = NotCopied.Value;
					PropertyDescriptor desc = props[i];
					if (desc.PropertyType.IsSubclassOf(typeof(ValueType)))
					{
						vals[i] = desc.GetValue(this);
					}
					else
					{
						object val = desc.GetValue(this);
						if (val == null)
						{
							vals[i] = null;
						}
						else if (val is IList)
						{
							// if IList, then no copy
						}
						else if (val is ICloneable)
						{
							vals[i] = ((ICloneable)val).Clone();
						}
					}
				}
				_OriginalValues = vals;
			}
		}
		public void CancelEdit()
		{
			if (IsEdit)
			{
				PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this, null);
				for (int i = 0; i < props.Count; ++i)
				{
					if (_OriginalValues[i] is NotCopied)
						continue;
					props[i].SetValue(this, _OriginalValues[i]);
				}
				_OriginalValues = null;
			}
		}
		public void EndEdit()
		{
			if (IsEdit)
			{
				_OriginalValues = null;
			}
		}

		#endregion

		#region Overrides: Object

		public override string ToString()
		{
			return Dump();
		}

		#endregion

		#region Operations

		public void Load()
		{
			defaultValues = new string[]{ 
											"False",
											"889",
											"584"
										};

			regBroker.Load(Registry.LocalMachine, "SOFTWARE\\CabalsCorner", regBroker.GetInstancePropertyNames(), defaultValues);
		}
		public void Save()
		{
			regBroker.Save(Registry.LocalMachine, "SOFTWARE\\CabalsCorner", regBroker.GetInstancePropertyNames(), defaultValues);
		}
		public string Dump()
		{
			string[] propNames = Reflector.GetProperyNames(this);
			object[] propValues = Reflector.GetProperyValues(this);

			if (propNames == null) 
				return string.Empty;
			if (propValues == null) 
				return string.Empty;
			if (propNames.Length != propValues.Length) 
				return "ProgramSettings.Dump: the number of property names is different than the number of property values.";

			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("* {0}\n", this.GetType().Name);
			for (int i = 0; i < propNames.Length; i++)
			{
				if (i >= propNames.Length || i >= propValues.Length) 
					continue;

				string propName = propNames[i];
				object propValue = propValues[i];
				sb.AppendFormat("- {0} = {1}\n", propName, propValue);
			}

			return sb.ToString();
		}

		#endregion

		#region Private Fields

		[NonSerialized]
		private string[] defaultValues = null;

		private string _slideShowRunning;
		private string _windowWidth;
		private string _windowHeight;

		[NonSerialized]
		private object[] _OriginalValues = null;
		[NonSerialized]
		private RegistryBroker regBroker = null;

		#endregion
	}
}
