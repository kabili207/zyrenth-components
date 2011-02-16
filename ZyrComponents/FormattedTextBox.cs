using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Zyrenth.Components
{

	public enum FormattedTextBoxType { None, CreditCard, Phone, SSN, EIN };

	/// <summary>
	/// Represents a Windows text box control that allows for an input mask.
	/// </summary>
	public class FormattedTextBox : TextBox
	{
		private bool isFormatting;

		[DefaultValue(FormattedTextBoxType.None),
		Description("Indicates the input mask that should be used to format the text box."),
		Category("Behavior")]
		public FormattedTextBoxType InputMask { get; set; }

		public override string Text
		{
			get
			{
				string deformatted = base.Text;

				if(InputMask == FormattedTextBoxType.CreditCard || InputMask == FormattedTextBoxType.EIN ||
					InputMask == FormattedTextBoxType.Phone || InputMask == FormattedTextBoxType.SSN)
					deformatted = Regex.Replace(deformatted, @"\D", string.Empty);

				return deformatted;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Zyrenth.Components.FormattedTextBox"/> class.
		/// </summary>
		public FormattedTextBox() : base()
		{
		}

		protected override void OnTextChanged(EventArgs e)
		{
			if (!isFormatting) // Prevents recursive calls due to formatting changes
			{

				isFormatting = true;
				switch (InputMask)
				{
					case FormattedTextBoxType.Phone:
						MakeDashed(new int[] { 3, 6 }, 10);
						break;
					case FormattedTextBoxType.SSN:
						MakeDashed(new int[] { 3, 5 }, 9);
						break;
					case FormattedTextBoxType.CreditCard:
						MakeDashed(new int[] { 4, 8, 12 }, 16);
						break;
					case FormattedTextBoxType.EIN:
						MakeDashed(new int[] { 2 }, 9);
						break;
						
				}
				isFormatting = false;
				base.OnTextChanged(e);
			}
		}

		private void MakeDashed(int[] dashes, int length)
		{
			int caret = SelectionStart;
			string temp;

			// Strips any non-digits and truncates the string
			temp = Text;
			if (temp.Length > length)
			{
				temp = temp.Substring(0, length);
			}

			// Move the insertion point to compensate for new or missing characters
			if (base.Text.Length > 0 && caret > 0 && !char.IsDigit(base.Text[caret - 1]))
				caret--;
			int count = 0;
			foreach (int dash in dashes)
			{
				if (temp.Length >= dash +count + 1)
					temp = temp.Insert(dash+count, "-");
				if (caret == dash +count+ 1)
					caret++;
				count++;
			}

			Text = temp; // Fires another TextChanged event. We ignore it.
			SelectionStart = caret;
		}

	}
}
