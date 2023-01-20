using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace CabalsCorner.Utilities.CustomDataSource
{
	 /// <summary>
	 /// Represents a fake object which has all the Tables as properties.
	 /// </summary>
	 [Serializable()]
	 public class DSTypeDescriptor : ICustomTypeDescriptor
	 {
		  #region ICustomTypeDescriptor stubs
		  AttributeCollection ICustomTypeDescriptor.GetAttributes()
		  {
				return new AttributeCollection(null);
		  }
		  string ICustomTypeDescriptor.GetClassName()
		  {
				return null;
		  }
		  string ICustomTypeDescriptor.GetComponentName()
		  {
				return null;
		  }
		  TypeConverter ICustomTypeDescriptor.GetConverter()
		  {
				return null;
		  }
		  EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		  {
				return null;
		  }
		  PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		  {
				return null;
		  }
		  object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		  {
				return null;
		  }
		  EventDescriptorCollection ICustomTypeDescriptor.GetEvents(System.Attribute[] attributes)
		  {
				return new EventDescriptorCollection(null);
		  }
		  EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		  {
				return new EventDescriptorCollection(null);
		  }
		  object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		  {
				return this;
		  }
		  #endregion

		  private ArrayList _TableDesc;

		  public DSTypeDescriptor()
		  {
				_TableDesc = new ArrayList();
		  }
		  public void Add(DSPropertyDescriptor Desc)
		  {
				_TableDesc.Add(Desc);
		  }
		  public Type GetElementType(PropertyDescriptor[] listAccessors)
		  {
				PropertyDescriptor prop = listAccessors[0];
				foreach (DSPropertyDescriptor desc in _TableDesc)
				{
					 if (desc.Name == prop.Name)
					 {
						  if (listAccessors.Length == 1)
								return desc.ComponentType;
						  Type elemtype = desc.ComponentType;
						  for (int i = 1; i < listAccessors.Length; ++i)
						  {
								// get the type of the collection property
								PropertyInfo info = elemtype.GetProperty(listAccessors[1].Name);
								if (info == null)
									 return null;
								// get the typed indexer property
								info = info.PropertyType.GetProperty("Item");
								if (info == null)
									 return null;
								// the type of the indexer property is the element type
								elemtype = info.PropertyType;
						  }
						  return elemtype;
					 }
				}
				return null;
		  }

		  PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		  {
				return ((ICustomTypeDescriptor)this).GetProperties(null);
		  }
		  /// <summary>
		  /// Emulates a fake object.
		  /// </summary>
		  PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(System.Attribute[] attributes)
		  {
				PropertyDescriptor[] props = new PropertyDescriptor[_TableDesc.Count];
				_TableDesc.CopyTo(props);
				return new PropertyDescriptorCollection(props);
		  }
	 }
}
