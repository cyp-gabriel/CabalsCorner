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
	/// The most simple TypeConverter.
	/// Creates code like
	///		ObjectType object = new ObjectType();
	///		object.Property1 = ...;
	///		object.Property2 = ...;
	///		...
	/// </summary>
	[Serializable()]
	public class SimpleObjectConverter : TypeConverter
	{
		#region Ctor(s)

		protected SimpleObjectConverter(Type ObjectType)
		{
			_ObjectType = ObjectType;
		}

		#endregion

		#region Overrides

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
				return true;
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor) && value.GetType() == _ObjectType)
			{
				ConstructorInfo ctor = _ObjectType.GetConstructor(new Type[] { });
				if (ctor != null)
					return new InstanceDescriptor(ctor, new object[] { }, false);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		#endregion

		#region Private Fields

		private Type _ObjectType;

		#endregion
	}
}