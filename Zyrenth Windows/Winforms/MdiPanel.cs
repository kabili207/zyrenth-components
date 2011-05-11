using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zyrenth.Winforms
{
	public class MdiPanel: System.Windows.Forms.Panel
	{
		private Form mdiForm;
		private MdiClient ctlClient;

		public MdiPanel()
		{
			ctlClient = new MdiClient();
			ctlClient.Dock = DockStyle.Fill;
			this.Controls.Add(ctlClient);
		}

		public MdiPanel(Form form) :this()
		{
			//this.Text = form.Text;
			//form.MdiChildActivate +=new EventHandler(FormActivated);

			if (this.mdiForm == null)
			{
				this.mdiForm = new Form();
				/// set the hidden ctlClient field which is used to determine if the form is an MDI form
				System.Reflection.FieldInfo field = typeof(Form).GetField("ctlClient", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
				field.SetValue(this.mdiForm, this.ctlClient);
			}
			form.MdiParent = this.mdiForm;
		}

		public void AddForm(Form form)
		{
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
