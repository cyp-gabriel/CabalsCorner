using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CabalsCorner.UIUtilities;
using CabalsCorner.Utilities;
using CabalsCorner.Utilities.BusinessObjects;
using CabalsCorner.CodeLocker.Classes;

namespace CabalsCorner.CodeLocker.UserControls
{
	internal partial class TimeDurationSelectorControl : UserControl
	{
		#region Ctor(s)

		public TimeDurationSelectorControl()
		{
			InitializeComponent();

			if (DesignMode)
				return;

			dtDate.ValueChanged -= dtDate_ValueChanged;
			dtTime.ValueChanged -= dtTime_ValueChanged;
		}

		#endregion

		#region Events/Delegates

		public event EventHandler DurationUnitsChanged;
		public event EventHandler ExpirationDatetimeChanged;
		public event EventHandler ExpirationDateExpired;

		#endregion

		#region Properties: Read/Write

		public string DurationUnits
		{
			get 
			{ 
				return _durationUnits; 
			}
			set
			{
				_durationUnits = value;
				EventDispatcher.SyncExecute(DurationUnitsChanged, this, EventArgs.Empty);
			}
		}
		public string ExpirationDatetime
		{
			get 
			{
				return _expirationDatetime; 
			}
			set
			{
				_expirationDatetime = value;
				//EventDispatcher.SyncExecute(ExpirationDatetimeChanged, this, EventArgs.Empty);
			}
		}

		#endregion

		#region Properties: Read-Only

		public DateTime Now
		{
			get 
			{ 
				return DateTime.Now; 
			}
		}
		public string DurationType
		{
			get
			{
				return cmbDurationType.Items[cmbDurationType.SelectedIndex].ToString();
			}
		}
		public long Ticks
		{
			get
			{
				return _duration.Ticks;
			}
		}

		#endregion

		#region Message-Handlers

		private void TimeDurationSelectorControl_Load(object sender, EventArgs e)
		{
			if (DesignMode)
				return;

			txtDuration.DataBindings.Add("Text", this, "DurationUnits");
			txtExpiration.DataBindings.Add("Text", this, "ExpirationDatetime");

			dtDate.MinDate = Now;

			_expirationDate.ValueChanged += _expirationDate_ValueChanged;

			Disposed += TimeDurationSelectorControl_Disposed;

			txtDuration.ForeColor = Color.DarkRed;
			txtExpiration.ForeColor = Color.DarkBlue;

			//txtNow.Text = App.Instance.MakeTimestamp(DateTime.Now);

			//_nowTimer.Start();
			//_expirationDate.Active = true;
		}
		void TimeDurationSelectorControl_Disposed(object sender, EventArgs e)
		{
			txtDuration.DataBindings.Clear();
			txtExpiration.DataBindings.Clear();
			dtDate.DataBindings.Clear();
			dtTime.DataBindings.Clear();
			_expirationDate.ValueChanged -= _expirationDate_ValueChanged;
			_nowTimer.Dispose();
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			btnReset.Enabled = false;
			DateTime resetDate = new DateTime(dtDate.Value.Year, dtDate.Value.Month, dtDate.Value.Day, 0, 0, 0, 0);
			_expirationDate.Value = resetDate;

			UpdateView();
			HighlightFields(11F, FontStyle.Bold, FontStyle.Underline, 10F, true);

			EventDispatcher.SyncExecute(ExpirationDatetimeChanged, this, EventArgs.Empty);
		}

		private void cmbDurationType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!refresh)
				return;

			//DateTime expirationDt;
			//if (!ValidateDateTimePickers(out expirationDt))
			//{
			//	DialogResult result = MessageBox.Show(
			//	  "Cannot convert to " + cmbDurationType.Items[cmbDurationType.SelectedIndex].ToString() + "."
			//	, "Attention on Location"
			//	, MessageBoxButtons.OK
			//	, MessageBoxIcon.Exclamation
			//	);
			//	refresh = false;
			//	cmbDurationType.SelectedIndex = _lastDt;
			//	refresh = true;
			//}
			//else
			//{
			//	CalcDurationAndExpiration(false);
			//}
			//DurationUnits = _expirationDate.GetRemainingDurationUnits(cmbDurationType.SelectedIndex).ToString();
			if (_highlight)
			{
				HighlightFields(11F, FontStyle.Bold, FontStyle.Underline, 10F, false);
			}
		}

		private void dtDate_ValueChanged(object sender, EventArgs e)
		{

		}
		private void dtDate_CloseUp(object sender, EventArgs e)
		{
			dtDate.ValueChanged += dtDate_ValueChanged;
		}
		private void dtDate_DropDown(object sender, EventArgs e)
		{
			dtDate.ValueChanged -= dtDate_ValueChanged;
		}
		private void dtTime_ValueChanged(object sender, EventArgs e)
		{
			try
			{
			//	if (!refresh)
			//		return;

			//	//_expirationDate.Value = dtTime.Value;
			//	_expirationDate.Validate();
			//	if (_expirationDate.IsDirty)
			//	{
			//		_expirationDate.RollBack();
			//	}
			//	else
			//	{
			//		//CalcDurationAndExpiration();
			//		UpdateView();

			//		if (_highlight)
			//		{
			//			HighlightFields(11F, FontStyle.Bold, FontStyle.Underline, 10F, true);
			//		}
			//		else
			//		{
			//			lblDuration.Text = string.Empty;
			//		}
			//	}

			////	//_lastTime = dtTime.Value;
			}
			catch (ApplicationException ex)
			{
				MessageBox.Show(this, ex.Message, "Attention on Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		private void dtTime_CloseUp(object sender, EventArgs e)
		{
			////dtTime.ValueChanged += dtDate_ValueChanged;
		}
		private void dtTime_DropDown(object sender, EventArgs e)
		{
			//dtTime.ValueChanged -= dtDate_ValueChanged;
		}

		private void _highlightTimer_Tick(object sender, EventArgs e)
		{
			_highlightTimer.Stop();
			_highlightTimer.Tick -= _highlightTimer_Tick;
			HighlightFields(11F, FontStyle.Regular, FontStyle.Regular, 10F, true);
			lblDuration.ForeColor = Color.Black;
			lblExpiration.ForeColor = Color.Black;
		}
		private void _statusTimer_Tick(object sender, EventArgs e)
		{
			_statusTimer.Stop();
			lblStatus.Text = string.Empty;
		}
		private void _nowTimer_Tick(object sender, EventArgs e)
		{
			if (!refresh)
				return;

			if (_expirationDate.Active && _expirationDate.Expired)
			{
				_nowTimer.Stop();
				_statusTimer.Stop();

				_expirationDate.Deactivate();

				ControlsEnumeratingMutator.SetPropertyAllControls(this, "Enabled", false);
				lblStatus.Enabled = true;

				StatusMsg("LOCK EXPIRED.", 100000);

				EventDispatcher.SyncExecute(ExpirationDateExpired, this, EventArgs.Empty);
			}
			else if (_expirationDate.Active && !_expirationDate.Expired)
			{
				UpdateView();
			}
		}

		#endregion

		#region Object Event-Handlers

		void _expirationDate_ValueChanged(object sender, EventArgs e)
		{
			if (!refresh)
				return;

			_expirationDate.Validate();
			if (_expirationDate.IsDirty)
			{
				StatusMsg(_expirationDate.ErrorMessages);
				
				_expirationDate.RollBack();
			}

			dtDate.Value = _expirationDate.Value;
			dtTime.Value = _expirationDate.Value;

			if (! _expirationDate.IsDirty && _highlight)
			{
				_statusTimer.Stop();
				lblStatus.Text = string.Empty;
				HighlightFields(11F, FontStyle.Bold, FontStyle.Underline, 10F, true);
			}
		} 

		#endregion

		#region Operations

		public void Configure(DateTime now, int durationIndex, double duration)
		{
			try
			{
				//refresh = false;
				cmbDurationType.SelectedIndex = durationIndex;

				if (dtDate.DataBindings.Count == 0)
				{
					dtDate.DataBindings.Add("Value", _expirationDate, "Value", true, DataSourceUpdateMode.OnPropertyChanged);
					dtTime.DataBindings.Add("Value", _expirationDate, "Value", true, DataSourceUpdateMode.OnPropertyChanged);
				}

				_highlight = false;
				_expirationDate.Activate(durationIndex, duration);
				_highlight = true;

				_nowTimer.Start();
				//_expirationDate.Value = Now + GetDurationTSFromDurationType(duration);

				//DateTime expirationDt;
				//btnReset.Enabled = ValidateDateTimePickers(0, 0, 0, 0, out expirationDt);

				//TimeSpan durationTs = _expirationDate.RemainingDuration;
				//DurationUnits = _expirationDate.GetRemainingDurationUnits(cmbDurationType.SelectedIndex).ToString();

				//ExpirationDatetime = App.Instance.MakeTimestamp(_expirationDate.Value);
				//refresh = true;

				//txtExpiration.SelectAll();
				//txtExpiration.Focus();
			}
			catch (ApplicationException ex)
			{
				MessageBox.Show(this, ex.Message, "Attention on Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public bool ValidateExpirationDate()
		{
			_expirationDate.Validate();
			if (_expirationDate.IsDirty)
			{
				BusinessObjectError error = _expirationDate.Errors[0];
				DialogResult result = MessageBox.Show(
				  error.ErrorMessage
				, "Attention on Location"
				, MessageBoxButtons.OK
				, MessageBoxIcon.Error
				);
				return false;
			}
			else
			{
				return true;
			}
		}

		#endregion

		#region Utilities

		private void UpdateView()
		{
			DurationUnits = _expirationDate.GetRemainingDurationUnits(cmbDurationType.SelectedIndex).ToString();

			DateTime expirationDt;
			btnReset.Enabled = ValidateDateTimePickers(0, 0, 0, 0, out expirationDt);

			ExpirationDatetime = App.Instance.MakeTimestamp(_expirationDate.Value);

			txtNow.Text = App.Instance.MakeTimestamp(Now);
		}

		private void HighlightFields(
			  float size
			, FontStyle fontStyle
			, FontStyle lblFontStyle
			, float lblSize
			, bool highlightExpiration
		)
		{
			_highlightTimer.Stop();
			_highlightTimer.Tick -= _highlightTimer_Tick;

			txtExpiration.Font = new System.Drawing.Font(
				"Courier New"
				, 11F
				, FontStyle.Regular
				, System.Drawing.GraphicsUnit.Point
				, ((byte)(0))
			);
			lblExpiration.ForeColor = Color.Black;
			lblExpiration.Font = new System.Drawing.Font(
				"Microsoft Sans Serif"
				, 10F
				, FontStyle.Regular
				, System.Drawing.GraphicsUnit.Point
				, ((byte)(0))
			);

			txtDuration.Font = new System.Drawing.Font(
				"Courier New"
				, size
				, fontStyle
				, System.Drawing.GraphicsUnit.Point
				, ((byte)(0))
			);
			lblDuration.Font = new System.Drawing.Font(
				"Microsoft Sans Serif"
				, lblSize
				, lblFontStyle
				, System.Drawing.GraphicsUnit.Point
				, ((byte)(0))
			);
			lblDuration.ForeColor = Color.DarkRed;

			if (highlightExpiration)
			{
				txtExpiration.Font = new System.Drawing.Font(
					"Courier New"
					, size
					, fontStyle
					, System.Drawing.GraphicsUnit.Point
					, ((byte)(0))
				);
				lblExpiration.Font = new System.Drawing.Font(
					"Microsoft Sans Serif"
					, lblSize
					, lblFontStyle
					, System.Drawing.GraphicsUnit.Point
					, ((byte)(0))
				);
				lblExpiration.ForeColor = Color.DarkBlue;
			}

			_highlightTimer.Start();
			_highlightTimer.Tick += _highlightTimer_Tick;
		}

		private void StatusMsg(string msg, int msTimeout)
		{
			_statusTimer.Stop();
			lblStatus.Text = msg;
			_statusTimer.Interval = msTimeout;
			_statusTimer.Start();
		}
		private void StatusMsg(string msg)
		{
			_statusTimer.Stop();
			lblStatus.Text = msg;
			_statusTimer.Interval = 4000;
			_statusTimer.Start();

			txtExpiration.Font = new Font(
				"Courier New"
				, 11F
				, FontStyle.Regular
				, GraphicsUnit.Point
				, ((byte)(0))
			);
			lblExpiration.ForeColor = Color.Black;
			lblExpiration.Font = new Font(
				"Microsoft Sans Serif"
				, 10F
				, FontStyle.Regular
				, System.Drawing.GraphicsUnit.Point
				, ((byte)(0))
			); 
			
			txtDuration.Font = new Font(
				"Courier New"
				, 11F
				, FontStyle.Regular
				, GraphicsUnit.Point
				, ((byte)(0))
			);
			lblDuration.ForeColor = Color.Black;
			lblDuration.Font = new Font(
				"Microsoft Sans Serif"
				, 10F
				, FontStyle.Regular
				, GraphicsUnit.Point
				, ((byte)(0))
			);
		}

		public void CalcDurationAndExpiration()
		{
			CalcDurationAndExpiration(true);
		}
		private void CalcDurationAndExpiration(bool highlightExpiration)
		{
			DateTime expirationDt;
			_duration = GetDurationTSFromDatePickers(out expirationDt);

			TimeSpan durationTs = _expirationDate.RemainingDuration;
			DurationUnits = _expirationDate.GetRemainingDurationUnits(cmbDurationType.SelectedIndex).ToString();

			DateTime expirationDt2;
			btnReset.Enabled = ValidateDateTimePickers(0, 0, 0, 0, out expirationDt2);

			ExpirationDatetime = App.Instance.MakeTimestamp(_expirationDate.Value);

			txtNow.Text = App.Instance.MakeTimestamp(Now);

			HighlightFields(11F, FontStyle.Bold, FontStyle.Underline, 10F, highlightExpiration);
		}

		private TimeSpan GetDurationTSFromDurationType(double duration)
		{
			TimeSpan durationTs = new TimeSpan();
			switch (cmbDurationType.SelectedIndex)
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
		private double GetDurationUnitsFromDurationType(TimeSpan durationTs)
		{
			switch (cmbDurationType.SelectedIndex)
			{
				case App.DAYS_INDEX:
					return durationTs.TotalDays;
				case App.HOURS_INDEX:
					return durationTs.TotalHours;
				case App.MINUTES_INDEX:
					return durationTs.TotalMinutes;
				case App.SECONDS_INDEX:
					return durationTs.TotalSeconds;
				default:
					return -1;
			}
		}
		private void LoadDatePickersFromDurationType(double duration)
		{
			//dtDate.Value = Now;
			//dtTime.Value = Now;
			refresh = false;
			switch (cmbDurationType.SelectedIndex)
			{
				case App.DAYS_INDEX:
					dtDate.Value = Now + GetDurationTSFromDurationType(duration);
					break;
				case App.HOURS_INDEX:
				case App.MINUTES_INDEX:
				case App.SECONDS_INDEX:
					dtTime.Value = Now + GetDurationTSFromDurationType(duration);
					break;
				default:
					break;
			}
			refresh = true;
		}
		private TimeSpan GetDurationTSFromDatePickers(out DateTime expirationDt)
		{
			return GetDurationTSFromDatePickers(dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second, dtTime.Value.Millisecond, out expirationDt);
		}
		private TimeSpan GetDurationTSFromDatePickers(int hour, int min, int sec, int ms, out DateTime expirationDt)
		{
			DateTime date = dtDate.Value;
			expirationDt = new DateTime(date.Year, date.Month, date.Day, hour, min, sec, ms);
			TimeSpan durationTs = expirationDt - Now;
			return durationTs;
      }

		private bool ValidateDateTimePickers(int hour, int min, int sec, int ms, out DateTime expirationDt)
		{
			TimeSpan durationTs = GetDurationTSFromDatePickers(hour, min, sec, ms, out expirationDt);
			//TimeSpan durationTs = _expirationDate.RemainingDuration;
			//expirationDt = _expirationDate.Value;
			if (durationTs.TotalSeconds < 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		private bool ValidateDateTimePickers(out DateTime expirationDt)
		{
         bool valid = ValidateDateTimePickers(dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second, dtTime.Value.Millisecond, out expirationDt);
			return valid;
		}

		#endregion

		#region Private Fields

		private string _durationUnits;
		private TimeSpan _duration;
		private string _expirationDatetime;
		private bool refresh = true;
		private ExpirationDate _expirationDate = App.Instance.ExpirationDate;
		private bool _highlight = false;

		#endregion

	} // class TimeDurationSelectorControl

} // namespace CabalsCorner.CodeLocker.UserControls
