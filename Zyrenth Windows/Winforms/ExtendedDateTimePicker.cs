using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Diagnostics;
using System.Windows.Forms.VisualStyles;

namespace Zyrenth.Winforms
{
	// Author:     Nils Jonsson
	// Originated: 10/03/2003

	/// <summary>
	/// Represents an enhanced Windows date-time picker control.
	/// </summary>
	[DefaultEvent("ValueChanged")]
	[DefaultProperty("Value")]
	[ToolboxItemFilter("System.Windows.Forms")]
    [ToolboxBitmap(typeof(ExtendedDateTimePicker))]
	public class ExtendedDateTimePicker : System.Windows.Forms.DateTimePicker
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimePicker" />
		/// class.
		/// </summary>
		public ExtendedDateTimePicker()
			: base()
		{
			// Show the check box because it is most of the reason for
			// this class.
			this.ShowCheckBox = true;
		}


		/// <summary>
		/// Occurs when the value of the <see cref="Checked" /> property
		/// changes.
		/// </summary>
		[Category("Property Changed")]
		[Description("Occurs when the Checked property value is changed.")]
		public event EventHandler CheckedChanged
		{
			add { this.checkedChanged += value; }
			remove { this.checkedChanged -= value; }
		}


		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="Value" />
		/// property has been set with a valid date-time value and the displayed
		/// value is able to be updated.
		/// </summary>
		/// <value><c>true</c> if the <see cref="Value" /> property has been set
		/// with a valid <see cref="DateTime" /> value and the displayed value
		/// is able to be updated; otherwise, <c>false</c>. The default is
		/// <c>true</c>.</value>
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Determines if the check box is checked, indicating that "
		 + "the user has selected a value.")]
		public new bool Checked
		{
			get { return base.Checked; }

			set
			{
				if (value == this.Checked)
					return;

				this.SetCheckBoxState(value);
				this.OnCheckedChanged();
				this.OnValueChanged();
			}
		}


		/// <summary>
		/// Gets or sets the date-time value assigned to the control.
		/// </summary>
		/// <value>The <see cref="DateTime" /> value assigned to the
		/// control.</value>
		/// <exception cref="ArgumentNullException">Argument is a null reference
		/// (<c>Nothing</c> in Visual Basic).</exception>
		/// <exception cref="ArgumentException">Argument is neither a
		/// <see cref="DateTime" /> nor a <see cref="DBNull" />
		/// value.</exception>
		[Category("Behavior")]
		[Description("The date and/or time value (also can be null).")]
		[RefreshProperties(RefreshProperties.All)]
		public new DateTime? Value
		{
			get
			{
				if (this.Checked)
					return base.Value;
				else
					return null;
			}

			set
			{

				if (value == this.Value)
					return;

				if (value == null)
				{
					this.Checked = false;
					this.OnValueChanged();
				}
				else if (value is DateTime)
				{
					// Calls OnValueChanged().

					base.Value = (DateTime)value;
					this.Checked = true;
				}
				else
				{
					throw new ArgumentException(
					 "Argument must be a nullable DateTime.");
				}
			}
		}



		/// <summary>
		/// Raises the <see cref="CheckedChanged" /> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs" /> that contains the event
		/// data.</param>
		/// <exception cref="ArgumentNullException"><paramref name="e" /> is a
		/// null reference (<c>Nothing</c> in Visual Basic).</exception>
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e",
				 "Argument e cannot be null.");
			}

			if (this.checkedChanged != null)
				this.checkedChanged(this, e);
		}

		private void OnCheckedChanged()
		{
			this.OnCheckedChanged(EventArgs.Empty);
		}

		/// <summary>
		/// Raises the <see cref="Control.KeyDown" /> event.
		/// </summary>
		/// <param name="e">A <see cref="KeyEventArgs" /> that contains the event
		/// data.</param>
		/// <exception cref="ArgumentNullException"><paramref name="e" /> is a
		/// null reference (<c>Nothing</c> in Visual Basic).</exception>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (((e.KeyCode == Keys.F4) && !(e.Alt))
			 || (e.KeyCode == Keys.Down) && e.Alt)
			{
				// Check the check box because the user dropped down the
				// calendar via keyboard input.
				this.Checked = true;
			}

			base.OnKeyDown(e);
		}

		private void OnTextChanged()
		{
			this.OnTextChanged(EventArgs.Empty);
		}

		private void OnValueChanged()
		{
			this.OnValueChanged(EventArgs.Empty);
		}

		private void SetCheckBoxState(bool value)
		{
			base.Checked = value;
			if (this.Checked)
			{
				this.showingOrHidingText = true;
				//base.CustomFormat = originalCustFormat;
				//base.Format = orginalFormat;
				this.showingOrHidingText = false;
			}
			else
			{
				// Tweak CustomFormat and Format in order to make the text
				// portion appear empty.

				this.showingOrHidingText = true;
				
				//originalCustFormat = base.CustomFormat;
				//orginalFormat = base.Format;
				//base.CustomFormat = " ";
				//base.Format = DateTimePickerFormat.Custom;
				this.showingOrHidingText = false;
			}
		}
		
		/// <summary>
		/// This member overrides <see cref="Control.WndProc" />.
		/// </summary>
		/// <param name="m">The Windows <see cref="Message" /> to
		/// process.</param>
		/// <exception cref="ArgumentNullException"><paramref name="m" /> is a
		/// null reference (<c>Nothing</c> in Visual Basic).</exception>
		[SecurityPermission(SecurityAction.Demand)]
		[DebuggerHidden]
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
			case 0x102: // WM_CHAR

				int charAsInt32 = m.WParam.ToInt32();
				char charAsChar = (char)charAsInt32;
				if (charAsInt32 == (int)Keys.Space)
				{
					// Toggle the check box because the user pressed the
					// space bar.
					this.OnKeyPress(new KeyPressEventArgs(charAsChar));
					this.Checked = !(this.Checked);
				}
				else
				{
					// Forward the message to DateTimePicker because a key
					// other than the space bar was pressed.
					base.WndProc(ref m);
				}
				break;
			case 0x201: // WM_LBUTTONDOWN

					// The X value of the mouse position is stored in the
					// low-order bits of LParam, and the Y value in the
					// high-order bits.

				int x = (m.LParam.ToInt32() << 16) >> 16;
				int y = m.LParam.ToInt32() >> 16;
				if (x <= this.GetCheckBoxExtent())
				{
					// Toggle the check box because the user clicked (near)
					// the check box.

					this.OnMouseDown(
						 new MouseEventArgs(MouseButtons.Left, 1, x, y, 0));
					this.Checked = !(this.Checked);
					// Grab focus because we are eating the message.

					this.Focus();
				}
				else
				{
					bool checkedChange = !(this.Checked);
					if (checkedChange)
					{
						// Check the check box because the user clicked
						// somewhere within the control while it was not
						// checked.

						this.SetCheckBoxState(true);
					}
					// Forward the message to DateTimePicker so that mouse
					// events will be fired, focus will be taken, etc.
					base.WndProc(ref m);
					if (checkedChange)
						this.OnCheckedChanged();
				}
				break;
			default:
					// Forward the message to DateTimePicker because it pertains
					// to neither the left mouse button nor a pressed key.

				base.WndProc(ref m);
				break;
			}
		}

		// Estimates the right edge of the area in which a mouse click will
		// toggle the check box. This varies with the font used to display the
		// text of the DateTimePicker.
		private int GetCheckBoxExtent()
		{
			// Use the Height property because the check box is square.
			
			return 16;
			//(int)this.CreateGraphics().MeasureString("X", this.Font).Height;
		}

		private bool showingOrHidingText;
		private EventHandler checkedChanged;
		private string originalCustFormat;
		private DateTimePickerFormat orginalFormat;
	}
}
