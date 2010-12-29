using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using HRScreening;
using System.ComponentModel;

namespace HRScreening.Components
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
			Font nodeFont = e.Node.NodeFont;
			if (nodeFont == null) nodeFont = ((TreeView)sender).Font;
			
			e.DrawDefault = true;			

			if (e.Node is ImageTreeNode)
			{
				ImageTreeNode node = e.Node as ImageTreeNode;

				if ((e.State & TreeNodeStates.Selected) == 0 && !((ImageTreeNode)e.Node).Active)
				{
					e.Graphics.DrawString(e.Node.Text, nodeFont, SystemBrushes.GrayText,
						e.Bounds.Left +1 , e.Bounds.Top +1);
					e.DrawDefault = false;
				}

				// We have to white out the area otherwise images start to overlap.
				e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds.Right + 1, e.Bounds.Top,
						e.Bounds.Height - 2, e.Bounds.Height - 2);

				if (((ImageTreeNode)e.Node).Image != null)
				{
					if(!TextOnly)
						e.Graphics.DrawImage(node.Image, e.Bounds.Right + 2, e.Bounds.Top,
							e.Bounds.Height - 4, e.Bounds.Height - 4);
					else
						e.Graphics.DrawString("M", nodeFont, Brushes.DarkBlue,
							e.Bounds.Right + 1, e.Bounds.Top);
				}
			}
		}

		// Returns the bounds of the specified node, including the region 
		// occupied by the node label and any node tag displayed.
		private Rectangle NodeBounds(TreeNode node)
		{
			// Set the return value to the normal node bounds.
			Rectangle bounds = node.Bounds;
			if (node.Tag != null)
			{
				// Retrieve a Graphics object from the TreeView handle
				// and use it to calculate the display width of the tag.
				Graphics g = this.CreateGraphics();
				int tagWidth = (int)g.MeasureString
					(node.Tag.ToString(), Font).Width + 6;

				// Adjust the node bounds using the calculated value.
				bounds.Offset(tagWidth / 2, 0);
				bounds = Rectangle.Inflate(bounds, tagWidth / 2, 0);
				g.Dispose();
			}

			return bounds;

		}


	}
}
