using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CabalsCorner.Utilities;

namespace CabalsCorner.Controls
{
	/// <summary>
	/// Extends vanilla TextBox control by restricting input to numbers.
	/// </summary>
	[DefaultEvent("NumberValidated")]
	public partial class NumberTextbox : TextBox
	{
		#region Events/Delegates

		[Browsable(true)]
		[Category("Cabal's Corner")]
		[Description("Fires after number text has been validated with ValidateText().")]
		public event EventHandler NumberValidated;

		#endregion

		#region Ctor(s)
		public NumberTextbox()
		{
			InitializeComponent();
			OriginalBackColor = this.BackColor;
			//this.Location = new System.Drawing.Point(129, 75);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Text";
			this.Size = new System.Drawing.Size(100, 26);
			this.TabIndex = 2;

			this.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			RequiresValue = true;
			MaxNumberValue = 1.7976931348623158e+308D;
			MinNumberValue = 0.00000000000001D;

			ErrorBackColor = Color.Yellow;

			// Iterate through each property
			foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this))
			{
				// Set default value if DefaultValueAttribute is present
				DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)]
																				 as DefaultValueAttribute;
				if (attr != null)
					prop.SetValue(this, attr.Value);
			}
		}

		[DefaultValue(typeof(Font), "Courier New, 10F, System.Drawing.FontStyle.Regular")]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}
		#endregion

		#region Properties (Designer): Read/Write

		[Browsable(true)]
		[Category("Cabal's Corner")]
		[Description("Represents the textbox background color whenever the input is invalid.  Defaults to yellow.")]
		[DefaultValue(typeof(Color), "Yellow")]
		public Color ErrorBackColor
		{
			get { return _errorBackColor; }
			set { _errorBackColor = value; }
		}

		[Browsable(true)]
		[DefaultValue(1.7976931348623158e+308D)]
		[Category("Cabal's Corner")]
		[Description("Maximum numeric value this control accepts.")]
		public double MaxNumberValue
		{
			get { return _maxNumberValue; }
			set { _maxNumberValue = value; Invalidate(); }
		}

		[Browsable(true)]
		[DefaultValue(.000000000000000001D)]
		[Category("Cabal's Corner")]
		[Description("Minimum numeric value this control accepts.")]
		public double MinNumberValue
		{
			get { return _minNumberValue; }
			set { _minNumberValue = value; Invalidate(); }
		}

		[Browsable(true)]
		[DefaultValue(true)]
		[Category("Cabal's Corner")]
		[Description("Defines whether this control requires a value.")]
		public bool RequiresValue
		{
			get { return _requiresValue; }
			set { _requiresValue = value; Invalidate(); }
		}

		#endregion

		#region Properties: Read/Write

		/// <summary>
		/// Returns true if this control's textbox value is a valid, numeric value.
		/// </summary>
		public bool ValidationSuccessful
		{
			get { return _validationSuccessful; }
			set { _validationSuccessful = value; Invalidate(); }
		}
		/// <summary>
		/// Returns the numeric value of this control's textbox as a double.
		/// </summary>
		public double DoubleValue
		{
			get
			{
				return double.Parse(Text);
			}
		}

		#endregion

		#region Properties (Private): Read/Write

		public Color OriginalBackColor
		{
			get { return _originalBackColor; }
			set { _originalBackColor = value; }
		}
		public bool ValueIsEmpty
		{
			get
			{
				return (Text == null || Text.Equals(string.Empty));
			}
		}
		public bool ValueIsNumber
		{
			get
			{
				double result;
				return (double.TryParse(Text, out result));
			}
		}

		#endregion

		#region Overrides

		protected override void OnTextChanged(EventArgs e)
		{
			ValidateText();
			base.OnTextChanged(e);
		}
		protected override void OnValidated(EventArgs e)
		{
			ValidateText();
			base.OnValidated(e);
		}
		protected override void OnValidating(CancelEventArgs e)
		{
			ValidateText();
			base.OnValidating(e);
		}
		protected override void OnKeyUp(KeyEventArgs e)
		{
			ValidateText();
			base.OnKeyUp(e);
		}

		#endregion

		#region Operations

		public void ValidateText()
		{
			ValidationSuccessful = false;
			this.BackColor = ErrorBackColor;

			if (ValueIsEmpty && RequiresValue)
			{
				_errorProvider.SetError(this, "Value is required.");
			}
			else if (Text.Contains(" "))
			{
				_errorProvider.SetError(this, "Value cannot contain whitespace characters.");
			}
			else if (!ValueIsEmpty && !ValueIsNumber)
			{
				_errorProvider.SetError(this, "Value must be a number..");
			}
			else if (!ValueIsEmpty && ValueIsNumber && (DoubleValue < MinNumberValue || DoubleValue > MaxNumberValue))
			{
				_errorProvider.SetError(this, string.Format("Value must be between {0} and {1}.", MinNumberValue.ToString(), MaxNumberValue.ToString()));
			}
			else
			{
				ValidationSuccessful = true;
				this.BackColor = OriginalBackColor;
				_errorProvider.SetError(this, string.Empty);
			}

			EventDispatcher.SyncExecute(NumberValidated, this, EventArgs.Empty);
		}

		#endregion

		#region Private Fields

		private bool _validationSuccessful;
		private double _maxNumberValue;
		private double _minNumberValue;
		private Color _originalBackColor;
		private Color _errorBackColor;
		private bool _requiresValue;

		#endregion

	} // class NumberTextbox

} // namespace CabalsCorner.Controls