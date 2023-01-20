using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CabalsCorner.Utilities;
using CabalsCorner.Utilities.CustomDataSource;
using CabalsCorner.Utilities.BusinessObjects.Attributes;

namespace CabalsCorner.Utilities.BusinessObjects
{
	public class BusinessObject : EditableObject
	{
		#region Properties: Read-Only

		public bool IsDirty
		{
			get
			{
				return _errors.Count > 0;
			}
		}

		public List<BusinessObjectError> Errors
		{
			get
			{
				return _errors;
			}
		} 

		#endregion

		#region Virtual Operations

		public virtual string ErrorMessages
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				foreach (BusinessObjectError error in _errors)
				{
					sb.AppendLine(error.ErrorMessage);
				}
				return sb.ToString();
			}
		} 

		#endregion

		#region Operations

		public void Validate()
		{
			_errors.Clear();

			string[] propNames = Reflector.GetProperyNames(this);
			foreach (string propName in propNames)
			{
				object[] attributes = Reflector.GetPropertyAttributes(this, propName);
				if (attributes == null || attributes.Length == 0)
					continue;

				foreach (object attr in attributes)
				{
					if (attr is ValidateAttribute)
					{
						ValidateAttribute attribute = (ValidateAttribute)attr;
						attribute.Validate(this);
						if (!attribute.Valid)
						{
							_errors.Add(new BusinessObjectError(propName, attribute.ErrorMessage));
						}
					}
				}
			}
		} 

		#endregion

		#region Protected Fields

		protected List<BusinessObjectError> _errors = new List<BusinessObjectError>(); 

		#endregion
	}
}
