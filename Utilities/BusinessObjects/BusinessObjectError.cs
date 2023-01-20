using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabalsCorner.Utilities.BusinessObjects
{
	public class BusinessObjectError
	{
		#region Ctor(s)

		public BusinessObjectError(string propertyName, string errorMessage)
		{
			Property = propertyName;
			ErrorMessage = errorMessage;
		}

		#endregion

		#region Read-Only Fields

		public readonly string Property;
		public readonly string ErrorMessage;

		#endregion
	}
}
