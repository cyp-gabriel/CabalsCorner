using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabalsCorner.Utilities.BusinessObjects.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public abstract class ValidateAttribute : Attribute
	{
		#region Properties: Read-Only

		public string ErrorMessage = string.Empty;
		public bool Valid = true;

		#endregion

		#region Abstract Operations

		public abstract bool Validate(object instance);

		#endregion
	}
}