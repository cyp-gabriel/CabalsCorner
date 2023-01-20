using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CabalsCorner.Utilities;
using CabalsCorner.CodeLocker;
using CabalsCorner.Utilities.BusinessObjects;
using CabalsCorner.Utilities.BusinessObjects.Attributes;
using CabalsCorner.CodeLocker.Classes;

namespace CabalsCorner.CodeLocker.Attributes
{
	internal class ExpirationDateValidationAttribute : ValidateAttribute
	{
		#region Overrides: ValidateAttribute

		public override bool Validate(object instance)
		{
			ExpirationDate expDate = (ExpirationDate)instance;
			TimeSpan duration = expDate.RemainingDuration;
			Valid = duration.TotalSeconds > 0;
			if (!Valid)
			{
				ErrorMessage = "Expiration date must be in the future.";
			}
			return Valid;
		} 

		#endregion
	}
}
