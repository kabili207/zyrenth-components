using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Zyrenth.Winforms
{

    [ToolboxBitmap(typeof(ImageTreeView))]
	public partial class ImageTreeView : TreeView
	{

		const int WM_LBUTTONDBLCLK = 0x0203;//client area
        const int WM_NCLBUTTONDBLCLK = 0x00A3;//non-client area
		
		bool _textOnly = false;

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="ImageTreeView"/> should display
		/// only text if there is an <see cref="ImageTreeNode"/>
		/// </summary>
		[Description("Sets if failsafe text should be used if an image is not found"),
		Category("Images"),
		DefaultValue(false)]
		public bool TextOnly {
			get { return _textOnly; }
			set { _textOnly = value; }
		}
		
		bool _doubleClickExpand = true;

		/// <summary>
		/// Gets or sets a value that determines if an <see cref="ImageTreeNode"/> will
		/// expand or collapse when double clicked on.
		/// </summary>
		[Description("Sets if failsafe text should be used if an image is not found"),
		Category("Behavior"),
		DefaultValue(true)]
		public bool DoubleClickExpand {
			get { return _doubleClickExpand; }
			set { _doubleClickExpand = value; }
		}


		public ImageTreeView()
			: base()
		{
			
			DrawMode = TreeViewDrawMode.OwnerDrawText;
			DrawNode += new DrawTreeNodeEventHandler(DrawNodeHandler);

		}

		private void DrawNodeHandler(object sender, DrawTreeNodeEventArgs e)
		{
			if (e.Bounds.Height == 0)
				return;

			Font nodeFont = e.Node.NodeFont;
			if (nodeFont == null) nodeFont = this.Font;
			
			SizeF size = e.Graphics.MeasureString(e.Node.Text, nodeFont);
			Rectangle bounds = new Rectangle(e.Bounds.Location.X, e.Bounds.Location.Y,
				(int)size.Width, e.Bounds.Height);

			if ((e.State & TreeNodeStates.Selected) == 0)
			{
				e.Graphics.FillRectangle(SystemBrushes.Window, bounds);
				if (e.Node is ImageTreeNode && !((ImageTreeNode)e.Node).Active)
				{
					e.Graphics.DrawString(e.Node.Text, nodeFont, SystemBrushes.GrayText,
						bounds.Left + 1, bounds.Top + 1);
				}
				else
				{
					e.Graphics.DrawString(e.Node.Text, nodeFont, SystemBrushes.WindowText,
						bounds.Left + 1, bounds.Top + 1);
				}
			}
			else
			{
				e.Graphics.FillRectangle(SystemBrushes.Highlight, bounds);
				e.Graphics.DrawString(e.Node.Text, nodeFont, SystemBrushes.HighlightText,
						bounds.Left + 1, bounds.Top + 1);
			}

			if (e.Node is ImageTreeNode)
			{
				ImageTreeNode node = e.Node as ImageTreeNode;

				// Make sure the images are drawn in the highest quality
				e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				
				// We have to white out the area otherwise images start to overlap.
				e.Graphics.FillRectangle(SystemBrushes.Window, bounds.Right + 1, bounds.Top,
						bounds.Height - 2, bounds.Height - 2);

				if (((ImageTreeNode)e.Node).Image != null)
				{
                    if (!TextOnly)
                    {

						if (Screen.PrimaryScreen.BitsPerPixel >= 32) // Check if primary screen supports alpha channels
                        {
                            e.Graphics.DrawImage(node.Image, bounds.Right + 2, bounds.Top,
                                bounds.Height - 4, bounds.Height - 4);
                        }
                        else
                        {
							// Alpha blending is not supported by primary screen so we have to draw it
							// off-screen to perform alpha blending then draw it to the screen
                            Bitmap bmp = new Bitmap(bounds.Height - 4, bounds.Height - 4, PixelFormat.Format16bppRgb555);
                            Graphics gBmp = Graphics.FromImage(bmp);

                            gBmp.Clear(SystemColors.Window);
                            gBmp.CompositingMode = CompositingMode.SourceOver;
                            gBmp.DrawImage(node.Image, 0, 0, bounds.Height - 4, bounds.Height - 4);

                            e.Graphics.DrawImage(bmp, bounds.Right , bounds.Top+1);
                        }
                    }
                    else
                    {
                        e.Graphics.DrawString(node.FallbackText, nodeFont, new SolidBrush(node.FallbackTextColor),
                            bounds.Right + 1, bounds.Top);
                    }
				}
				
			}
		}

		protected override bool ProcessMnemonic(char charCode)
		{
			//return base.ProcessMnemonic(charCode);
			return false;
		}

		protected override void DefWndProc(ref Message m) {
	        if ((m.Msg == WM_LBUTTONDBLCLK || m.Msg == WM_NCLBUTTONDBLCLK) && !DoubleClickExpand) /*  */
	        	return;
	        base.DefWndProc(ref m);
	    }

		protected override void WndProc(ref Message m) {
			if ((m.Msg == WM_LBUTTONDBLCLK || m.Msg == WM_NCLBUTTONDBLCLK) && !DoubleClickExpand)
				return;
			base.WndProc (ref m);
		}
		
	}
}
