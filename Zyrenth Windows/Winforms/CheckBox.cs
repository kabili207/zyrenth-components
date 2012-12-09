using System;
using System.ComponentModel;

namespace Zyrenth.Winforms
{
	public class CheckBox : System.Windows.Forms.CheckBox
	{
		public CheckBox()
		{
		}

		private bool _exclusive = false;

		[DefaultValue(false)]
		public bool Exclusive
		{
			get { return _exclusive; }
			set { _exclusive = value; }
		}

		public String StringValue
		{
			get { return Checked ? "Y" : "N"; }
			set { Checked = value == "Y"; }
		}

		public string Value { get; set; }

		protected override void OnCheckedChanged(System.EventArgs e)
		{
			base.OnCheckedChanged(e);

			if (DesignMode)
				return;

			if (Exclusive && this.Checked /*&& !(Parent is Panel)*/)
			{
				foreach (System.Windows.Forms.Control box in Parent.Controls)
				{
					if (box is CheckBox)
					{
						if (!ReferenceEquals(this, box))
							((CheckBox)box).Checked = false;
					}
				}
			}
			else if (this.Checked)
			{
				foreach (System.Windows.Forms.Control box in Parent.Controls)
				{
					if (box is CheckBox)
					{
						if (((CheckBox)box).Exclusive && !ReferenceEquals(this, box))
						{
							((CheckBox)box).Checked = false;
						}
					}
				}
			}

		}
	}
}

