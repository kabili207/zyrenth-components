using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zyrenth.Winforms
{
	class TabPage : System.Windows.Forms.TabPage
	{
		private Form mdiForm;
		private MdiClient ctlClient = new MdiClient();
		public bool IsMdiContainer { get; private set; }

		public TabPage()
		{
			this.IsMdiContainer = false;
		}

		public TabPage(Form form) :this()
		{
			this.IsMdiContainer = true;
			this.Text = form.Text;
			base.Controls.Add(this.ctlClient);

			if (this.mdiForm == null)
			{
				this.mdiForm = new Form();
				/// set the hidden ctlClient field which is used to determine if the form is an MDI form
				System.Reflection.FieldInfo field = typeof(Form).GetField("ctlClient", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
				field.SetValue(this.mdiForm, this.ctlClient);
			}
			form.MdiParent = this.mdiForm;
		}

	}
}
