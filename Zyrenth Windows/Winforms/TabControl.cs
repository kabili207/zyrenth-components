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
		public TabControl()
		{
			SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
			this.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.SizeMode = TabSizeMode.Fixed;
			//this.ItemSize = new Size(
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

	}
}
