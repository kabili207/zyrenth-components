using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Zyrenth;
using System.ComponentModel;

namespace Zyrenth.Components
{

	public partial class ImageTreeView : TreeView
	{

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

				

				// We have to white out the area otherwise images start to overlap.
				e.Graphics.FillRectangle(SystemBrushes.Window, bounds.Right + 1, bounds.Top,
						bounds.Height - 2, bounds.Height - 2);

				if (((ImageTreeNode)e.Node).Image != null)
				{
					if(!TextOnly)
						e.Graphics.DrawImage(node.Image, bounds.Right + 2, bounds.Top,
							bounds.Height - 4, bounds.Height - 4);
					else
						e.Graphics.DrawString("M", nodeFont, Brushes.DarkBlue,
							bounds.Right + 1, bounds.Top);
					
				}
				
			}
		}

		protected override bool ProcessMnemonic(char charCode)
		{
			//return base.ProcessMnemonic(charCode);
			return false;
		}

	}
}
