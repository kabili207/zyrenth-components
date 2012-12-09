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
			form.MdiParent = this.MdiForm;
		}

		public void AddForm(Form form)
		{
			
			form.MdiParent = this.MdiForm;
		}

		public Form[] MdiChildren
		{
			get { return MdiForm.MdiChildren; }
		}

		public Form MdiForm
		{
			get
			{
				if (this.mdiForm == null)
				{
					this.mdiForm = new Form();
					/// set the hidden ctlClient field which is used to determine if the form is an MDI form
					System.Reflection.FieldInfo field = typeof(Form).GetField("ctlClient", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
					field.SetValue(this.mdiForm, this.ctlClient);
				}
				return mdiForm;
			}
		}
	}
}
