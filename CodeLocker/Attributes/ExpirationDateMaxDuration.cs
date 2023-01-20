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
	internal class ExpirationDateMaxDurationDays : ValidateAttribute
	{
		#region Ctor(s)

		public ExpirationDateMaxDurationDays() : this(9250D)
		{
		}
		public ExpirationDateMaxDurationDays(double maxDays)
		{
			_maxDays = maxDays;
		} 

		#endregion

		#region Override(s): ValidateAttribute

		public override bool Validate(object instance)
		{
			ExpirationDate expDate = (ExpirationDate)instance;
			TimeSpan duration = expDate.RemainingDuration;
			Valid = duration.TotalDays < _maxDays;
			if (!Valid)
			{
				ErrorMessage = string.Format("Expiration date duration cannot exceed {0} days.", _maxDays.ToString()); ;
			}
			return Valid;
		} 

		#endregion

		#region Private Fields

		private double _maxDays = 0; 

		#endregion
	}
}
