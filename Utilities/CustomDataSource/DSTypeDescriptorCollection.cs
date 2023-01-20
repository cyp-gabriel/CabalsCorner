using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace CabalsCorner.Utilities.CustomDataSource
{
	 /// <summary>
	 /// Represents a fake collection which has only ONE typedescriptor like DataViewManager.
	 /// </summary>
	 [Serializable()]
	 public class DSTypeDescriptorCollection : CollectionBase, ITypedList
	 {
		  private DSTypeDescriptor _Tables;

		  public DSTypeDescriptorCollection()
		  {
				_Tables = new DSTypeDescriptor();
				List.Add(_Tables);
		  }
		  public void Add(object Target, string PropertyName, Type ElementType)
		  {
				_Tables.Add(new DSPropertyDescriptor(Target, PropertyName, ElementType));
		  }

		  string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		  {
				if (listAccessors == null || listAccessors.Length == 0)
					 return "SUX"; // nevermind, never queryed
				Type elementtype = _Tables.GetElementType(listAccessors);
				if (elementtype == null)
					 return string.Empty;
				return listAccessors[listAccessors.Length - 1].Name; // works as TableName
		  }
		  PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		  {
				if (listAccessors == null || listAccessors.Length == 0)
					 return ((ICustomTypeDescriptor)_Tables).GetProperties(); // get Tables
				Type elementtype = _Tables.GetElementType(listAccessors);
				if (elementtype == null)
					 return new PropertyDescriptorCollection(null);
				return TypeDescriptor.GetProperties(elementtype); // get Table columns
		  }
	 }
}
