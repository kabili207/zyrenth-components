using System;
using System.ComponentModel;

namespace Zyrenth.Winforms
{
	public class Panel : System.Windows.Forms.Panel
	{
		public string SelectedValue
		{
			get
			{
				foreach (System.Windows.Forms.Control con in Controls)
				{
					if (con is CheckBox || con is RadioButton)
					{
						try
						{
							bool o = (bool)TypeDescriptor.GetProperties(con)["Checked"].GetValue(con);
							if ((o == true))
							{
								return TypeDescriptor.GetProperties(con)["Value"].GetValue(con) as string;
							}
						}
						catch
						{
						}
					}
				}
				return null;
			}
			set
			{
				foreach (System.Windows.Forms.Control con in Controls)
				{
					if (con is CheckBox || con is RadioButton)
					{
						string o = TypeDescriptor.GetProperties(con)["Value"].GetValue(con) as string;
						TypeDescriptor.GetProperties(con)["Checked"].SetValue(con, o == value);
					}
				}
			}
		}

		public Panel()
			: base()
		{
			if (!DesignMode)
				this.ControlAdded += this.Control_Added;
		}

		private void Control_Added(object sender, System.Windows.Forms.ControlEventArgs e)
		{
			if (e.Control is CheckBox)
			{
				((CheckBox)e.Control).CheckedChanged += this.Checked_Changed;
			}
			else if (e.Control is RadioButton)
			{
				((RadioButton)e.Control).CheckedChanged += this.Checked_Changed;
			}

		}

		private void Checked_Changed(object sender, EventArgs e)
		{
			if ((sender is CheckBox && !((CheckBox)sender).Exclusive) || sender is RadioButton)
			{
				try
				{
					if (!string.IsNullOrEmpty(TypeDescriptor.GetProperties(sender)["Value"].GetValue(sender) as string))
					{
						bool o = (bool)TypeDescriptor.GetProperties(sender)["Checked"].GetValue(sender);
						if ((o == true))
						{
							SelectedValue = TypeDescriptor.GetProperties(sender)["Value"].GetValue(sender) as string;
						}
					}
				}
				catch
				{
				}
			}
		}

	}

	public class RadioButton : System.Windows.Forms.RadioButton
	{

		public string Value { get; set; }

	}
}

