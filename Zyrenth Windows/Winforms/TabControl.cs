using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;

namespace Zyrenth.Winforms
{
	public partial class TabControl : System.Windows.Forms.TabControl
	{
		ContextMenuStrip menu;
		ToolStripMenuItem closeToolStripMenuItem;
		ToolStripMenuItem closeAllToolStripMenuItem;
		ToolStripMenuItem closeOtherToolStripMenuItem;

		public TabControl()
		{
			SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
			this.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.SizeMode = TabSizeMode.Fixed;
			menu = new ContextMenuStrip();
			menu.AllowMerge = true;
			closeOtherToolStripMenuItem = new ToolStripMenuItem();
			closeAllToolStripMenuItem = new ToolStripMenuItem();
			closeToolStripMenuItem = new ToolStripMenuItem();

			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.closeOtherToolStripMenuItem});
			this.closeToolStripMenuItem.Text = "Close Tab";
			this.closeAllToolStripMenuItem.Text = "Close All Tabs";
			this.closeOtherToolStripMenuItem.Text = "Close Other Tabs";
			this.closeToolStripMenuItem.Click += new EventHandler(OnCloseToolStrip_Click);
			this.closeAllToolStripMenuItem.Click += new EventHandler(OnCloseAllToolStrip_Click);
			this.closeOtherToolStripMenuItem.Click += new EventHandler(OnCloseOtherToolStrip_Click);

			//this.Controls.Add(ctlClient);
			//this.ItemSize = new Size(
		}

		private void OnCloseToolStrip_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.TabPage tp = menu.Tag as System.Windows.Forms.TabPage;
			if (tp != null)
			{
				this.TabPages.Remove(tp);
			}
		}

		private void OnCloseAllToolStrip_Click(object sender, EventArgs e)
		{
			this.TabPages.Clear();
		}

		private void OnCloseOtherToolStrip_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.TabPage tp = menu.Tag as System.Windows.Forms.TabPage;
			TabPageCollection tc = this.TabPages;
			if (tp != null)
			{
				foreach (System.Windows.Forms.TabPage tcp in tc)
				{
					if (tcp != tp)
						this.TabPages.Remove(tcp);
				}
			}
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			/*foreach(TabPage tp in TabPages)
			{
				//if(tp.IsMdiContainer)
				//	tp.Controls.Remove(ctlClient);
			}
			if (this.SelectedTab is TabPage && ((TabPage)this.SelectedTab).IsMdiContainer)
			{
				//((TabPage)this.SelectedTab).Controls.Add(ctlClient);
				((TabPage)this.SelectedTab).LinkedForm.Select();
			}*/
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				menu.Show(this, e.Location);
				int i = 0;
				for (; i<  this.TabPages.Count; i++)
				{
					if (GetTabRect(i).Contains(e.Location))
						break;
				}
				if (i < TabPages.Count)
					menu.Tag = TabPages[i];
				else
					menu.Tag = null;
			}
			else
			{
				base.OnMouseClick(e);
			}
		}

		protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
		{
			//base.OnDrawItem(e);
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			Rectangle tabTextArea = Rectangle.Empty;
			for (int nIndex = 0; nIndex < this.TabCount; nIndex++)
			{
				tabTextArea = this.GetTabRect(nIndex);
				if (nIndex != this.SelectedIndex)
				{
					/*if not active draw ,inactive close button*/

					if (VisualStyleRenderer.IsElementDefined(VisualStyleElement.Tab.TabItem.Normal))
					{
						VisualStyleRenderer renderer =
							 new VisualStyleRenderer(VisualStyleElement.Tab.TabItem.Normal);
						renderer.DrawBackground(e.Graphics, tabTextArea);

					}
					//else
					//e.Graphics.DrawString("This element is not defined in the current visual style.",
					//	 this.Font, Brushes.Black, new Point(10, 10));

					/*using(Bitmap bmp = new Bitmap(GetContentFromResource(
						"closeinactive.bmp")))
					{
						e.Graphics.DrawImage(bmp,
							tabTextArea.X+tabTextArea.Width -16, 5, 13, 13);
					}*/
				}
				else
				{
					LinearGradientBrush br = new LinearGradientBrush(tabTextArea,
						SystemColors.ControlLightLight, SystemColors.Control,
						LinearGradientMode.Vertical);
					e.Graphics.FillRectangle(br, tabTextArea);
					if (VisualStyleRenderer.IsElementDefined(VisualStyleElement.Tab.TopTabItem.Pressed))
					{
						VisualStyleRenderer renderer =
							 new VisualStyleRenderer(VisualStyleElement.Tab.TopTabItem.Pressed);
						tabTextArea.Inflate(1, 1);
						renderer.DrawBackground(e.Graphics, tabTextArea);
					}

					/*if active draw ,inactive close button*/
					/*using(Bitmap bmp = new Bitmap(
						GetContentFromResource("close.bmp")))
					{
						e.Graphics.DrawImage(bmp,
							tabTextArea.X+tabTextArea.Width -16, 5, 13, 13);
					}*/
					br.Dispose();
				}

				if (VisualStyleRenderer.IsElementDefined(VisualStyleElement.ToolTip.Close.Normal))
				{
					VisualStyleRenderer renderer =
						 new VisualStyleRenderer(VisualStyleElement.ToolTip.Close.Normal);
					Rectangle rectangle1 = new Rectangle((int)tabTextArea.Right - 15, (int)tabTextArea.Top + 1,
						(int)tabTextArea.Height - 2, (int)tabTextArea.Height - 2);
					renderer.DrawBackground(e.Graphics, rectangle1);
					tabTextArea.Width += 15;
					
				}

				string str = this.TabPages[nIndex].Text;
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = StringAlignment.Center;
				using (SolidBrush brush = new SolidBrush(
					this.TabPages[nIndex].ForeColor))
				{
					//Draw the tab header text
					TextRenderer.DrawText(e.Graphics, str, this.Font,
						new Rectangle(tabTextArea.X, tabTextArea.Y, tabTextArea.Width - 15, tabTextArea.Height),
						this.TabPages[nIndex].ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

				}
			}
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			this.ResumeLayout(false);

		}

	}
}
