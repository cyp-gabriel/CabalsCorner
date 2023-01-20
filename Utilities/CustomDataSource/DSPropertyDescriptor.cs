using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace CabalsCorner.Utilities.CustomDataSource
{
	 /// <summary>
	 /// Represents a Table.
	 /// </summary>
	 [Serializable()]
	 public class DSPropertyDescriptor : PropertyDescriptor
	 {
		  #region PropertyDescriptor stubs
		  public override bool CanResetValue(object Component) { return false; }
		  public override bool IsReadOnly { get { return true; } }
		  public override void ResetValue(object Component) { }
		  public override bool ShouldSerializeValue(object Component) { return false; }
		  public override void SetValue(object component, object value) { }
		  #endregion

		  public DSPropertyDescriptor(object Target, string PropertyName, Type ElementType)
				: base(PropertyName, null)
		  {
				_Target = Target;
				_ElementType = ElementType;
				Type proptype = Target.GetType().GetProperty(PropertyName).PropertyType;
				_Desc = TypeDescriptor.CreateProperty(Target.GetType(), PropertyName, proptype);
		  }

		  /// <summary>
		  /// Gets the type of a Row.
		  /// </summary>
		  public override Type ComponentType
		  {
				get { return _ElementType; }
		  }
		  /// <summary>
		  /// Gets the type of the Table (IList, IBindingList, etc).
		  /// </summary>
		  public override Type PropertyType
		  {
				get { return _Desc.PropertyType; }
		  }
		  /// <summary>
		  /// Gets the Table.
		  /// </summary>
		  public override object GetValue(object component)
		  {
				return _Desc.GetValue(_Target);
		  }

		  private object _Target;
		  private Type _ElementType;
		  private PropertyDescriptor _Desc;
	 }
}
