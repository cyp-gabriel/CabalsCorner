using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;

using CabalsCorner.Utilities;
using CabalsCorner.Utilities.BusinessObjects;
using CabalsCorner.Utilities.CustomDataSource;
using CabalsCorner.CodeLocker.Attributes;
using CabalsCorner.Utilities.BusinessObjects.Attributes;

namespace CabalsCorner.CodeLocker.Classes
{
	[TypeConverter(typeof(ExpirationDateConverter))]
	[DefaultProperty("Value")]
	[Serializable]
	internal class ExpirationDate : BusinessObject
	{
		#region Events/Delegates

		public event EventHandler ValueChanged;
		public event EventHandler Activated;
		public event EventHandler Deactivated;

		#endregion

		#region Properties: Read-Only

		[ExpirationDateValidation()]
		[ExpirationDateMaxDurationDays(9250D)]
		public DateTime Value
		{
			get
			{
				return _value;
			}
			set
			{
				_lastValue = _value;
				_value = value;

				EventDispatcher.SyncExecute(ValueChanged, this, EventArgs.Empty);
			}
		}

		public DateTime Now
		{
			get
			{
				return DateTime.Now;
			}
		}
		public bool Active
		{
			get
			{
				return _active;
			}
			set
			{
				_active = value;

				if (_active)
				{
					EventDispatcher.SyncExecute(Activated, this, EventArgs.Empty);
				}
				else
				{
					EventDispatcher.SyncExecute(Deactivated, this, EventArgs.Empty);
				}
			}
		} 
		public TimeSpan RemainingDuration
		{
			get
			{
				return Value - Now;
			}
		}
		public bool Expired
		{
			get
			{
				return Value < Now;
			}
		}

		#endregion

		#region Operations

		public void Activate(int durationTypeIndex, double duration)
		{
			DateTime now = Now;
			_value = now + GetDurationTSFromDurationType(durationTypeIndex, duration);
			_lastValue = now + GetDurationTSFromDurationType(durationTypeIndex, duration);

			_active = true;

			EventDispatcher.SyncExecute(ValueChanged, this, EventArgs.Empty);
		}
		public void RollBack()
		{
			_value = _lastValue;
			//Value = _lastValue;
		}
		public void Deactivate() 
		{
			_active = false;
		}
		public double GetRemainingDurationUnits(int durationTypeIndex)
		{
			double remainingDuration = 0;
			switch (durationTypeIndex)
			{
				case App.SECONDS_INDEX:
					remainingDuration = RemainingDuration.TotalSeconds;
					break;
				case App.MINUTES_INDEX:
					remainingDuration = RemainingDuration.TotalMinutes;
					break;
				case App.HOURS_INDEX:
					remainingDuration = RemainingDuration.TotalHours;
					break;
				case App.DAYS_INDEX:
					remainingDuration = RemainingDuration.TotalDays;
					break;
			}
			return remainingDuration;
		}

		#endregion

		#region Utilities

		private TimeSpan GetDurationTSFromDurationType(int durationTypeIndex, double duration)
		{
			TimeSpan durationTs = new TimeSpan();
			switch (durationTypeIndex)
			{
				case App.SECONDS_INDEX:
					durationTs = TimeSpan.FromSeconds(duration);
					break;
				case App.MINUTES_INDEX:
					durationTs = TimeSpan.FromMinutes(duration);
					break;
				case App.HOURS_INDEX:
					durationTs = TimeSpan.FromHours(duration);
					break;
				case App.DAYS_INDEX:
					durationTs = TimeSpan.FromDays(duration);
					break;
			}
			return durationTs; 
		}

		#endregion

		#region Private Fields

		private DateTime _value;
		private DateTime _lastValue;
		private bool _active = false;

		#endregion
	}

	[Serializable]
	internal class ExpirationDateConverter : SimpleObjectConverter
	{
		#region Ctor(s)

		ExpirationDateConverter()
			: base(typeof(ExpirationDate))
		{
		}

		#endregion
	}
}
