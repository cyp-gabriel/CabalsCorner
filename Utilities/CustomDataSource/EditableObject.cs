using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace CabalsCorner.Utilities.CustomDataSource
{
	/// <summary>
	/// Base class for the BindingCollectionBase items.
	/// </summary>
	[Serializable()]
	public class EditableObject : IEditableObject
	{
		#region Properties: Read-Only
		protected BindingCollectionBase Collection
		{
			get { return _Collection; }
		}

		private bool PendingInsert
		{
			get { return (_Collection._PendingInsert == this); }
		}

		private bool IsEdit
		{
			get { return (_OriginalValues != null); }
		}
		#endregion

		#region IEditableObject Impl.

		void IEditableObject.BeginEdit()
		{
			System.Diagnostics.Trace.WriteLine("BeginEdit");
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

		void IEditableObject.CancelEdit()
		{
			System.Diagnostics.Trace.WriteLine("CancelEdit");
			if (IsEdit)
			{
				if (PendingInsert)
					((IList)_Collection).Remove(this);
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

		void IEditableObject.EndEdit()
		{
			System.Diagnostics.Trace.WriteLine("EndEdit");
			if (IsEdit)
			{
				if (PendingInsert)
					_Collection._PendingInsert = null;
				_OriginalValues = null;
			}
		}

		#endregion

		#region Utilities

		// not a contructor since it would fuck up derived classes
		internal void SetCollection(BindingCollectionBase Collection)
		{
			_Collection = Collection;
		}

		#endregion

		#region Fields

		internal BindingCollectionBase _Collection = null;
		private object[] _OriginalValues = null;

		#endregion
	}
}