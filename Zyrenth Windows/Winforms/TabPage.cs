using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Zyrenth.Winforms
{
	class TabPage : System.Windows.Forms.TabPage
	{
		public Icon TabIcon { get; set; }

		public TabPage()
		{
		}

		public TabPage(Form form) :this()
		{
			
		}


		private void FormActivated(object sender, EventArgs e)
		{
			TabControl tc = this.Parent as TabControl;
			if (tc != null)
				tc.SelectedTab = this;
		}
	}
}
