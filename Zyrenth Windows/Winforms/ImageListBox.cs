using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Zyrenth.Winforms
{
	/// <summary>
	/// Extends the <see cref="System.Windows.Forms.ListBox"/> control to display an image
	/// to the left of the item.
    /// </summary>
    [ToolboxBitmap(typeof(ImageListBox))]
	public class ImageListBox : ListBox
	{
		[DefaultValue(false)]
		public bool HideImage { get; set; }

		public bool UseCompatibleTextRendering { get; set; }
		internal bool CanUseTextRenderer { get { return true; } }
		internal bool SupportsUseCompatibleTextRendering { get { return true; } }
		internal bool UseCompatibleTextRenderingInt { get; set; }

		private List<Bitmap> _images;

		/// <summary>
		/// Occurs when the control checks for formatting changes
		/// </summary>
		/// <remarks>Hides the ListControl Format event</remarks>
		public new event ImageListControlConvertEventHandler Format;

		public ImageListBox()
		{
			//this.DrawMode = DrawMode.OwnerDrawFixed; // We're using custom drawing.
			_images = new List<Bitmap>();
			
		}

		/// <summary>
		/// Raises the Zyrenth.Winforms.ImageListBox.Format event.
		/// </summary>
		protected override void OnFormat(ListControlConvertEventArgs e)
		{
			ImageListControlConvertEventArgs args =
				new ImageListControlConvertEventArgs(e.Value, e.DesiredType, e.ListItem);
			if(Format != null)
				Format(this, args);
			e.Value = args.Value;
		}
		
		
		
		/// <summary>
		/// Overrides the ListBox DrawItem event handler to custom draw each
		/// item in the Items collection.
		/// </summary>
		/// <remarks>
		/// There WILL be discrepencies between how the text is drawn in a normal
		/// list box and our extended list box. This is because the old listbox
		/// uses GDI to draw text whereas this one uses GDI+.
		/// </remarks>
		protected override void OnDrawItem(DrawItemEventArgs e)
		{

			base.OnDrawItem(e);

			// Make sure we're not trying to draw something that isn't there.
			if (e.Index >= this.Items.Count || e.Index <= -1)
			{
				// The designer draws a single 'fake' item
				// We'll use this to draw the name of the control
				if (this.DesignMode)
					TextRenderer.DrawText(e.Graphics, this.Name, this.Font,
					    new Point(e.Bounds.X, e.Bounds.Y), SystemColors.WindowText);
					//e.Graphics.DrawString(this.Name, this.Font, SystemBrushes.WindowText,
					//	new PointF(e.Bounds.X, e.Bounds.Y));
				
				return;
			}
			
			// Get the item object.
			object item = this.Items[e.Index];
			if (item == null)
				return;

			string text = GetItemText(item);
			
			Point stringLoc;
			if (HideImage)
				stringLoc = new Point(e.Bounds.X, e.Bounds.Y);
			else
				stringLoc = new Point(e.Bounds.X + e.Bounds.Height + 1, e.Bounds.Y);

			Brush back;
			//Brush front;
			Color front;
			ImageListBoxItem ilist = this.Items[e.Index] as ImageListBoxItem;

			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				back = SystemBrushes.Highlight;
				front = SystemColors.HighlightText;
			}
			else
			{
				back = SystemBrushes.Window;
				if(ilist != null && !ilist.Active)
					front = SystemColors.GrayText;
				else
					front = SystemColors.WindowText;
			}

			e.Graphics.FillRectangle(back, e.Bounds);
			TextRenderer.DrawText(e.Graphics, text, this.Font, stringLoc, front);
			//e.Graphics.DrawString(text, this.Font, front, stringLoc, StringFormat.GenericDefault);

			if (ilist == null || HideImage || ilist.Image == null)
				return;
			// Make sure the images are drawn in the highest quality
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

			if (Screen.PrimaryScreen.BitsPerPixel >= 32) // Check if primary screen supports alpha channels
			{
				e.Graphics.DrawImage(ilist.Image, e.Bounds.Left + 1, e.Bounds.Top + 1,
					e.Bounds.Height - 2, e.Bounds.Height - 2);
			}
			else
			{
				// Alpha blending is not supported by primary screen so we have to draw it
				// off-screen to perform alpha blending then draw it to the screen
				Bitmap bmp = new Bitmap(e.Bounds.Height - 2, e.Bounds.Height - 2, PixelFormat.Format16bppRgb555);
				Graphics gBmp = Graphics.FromImage(bmp);

				gBmp.Clear(new Pen(back).Color);
                gBmp.CompositingMode = CompositingMode.SourceOver;
				gBmp.DrawImage(ilist.Image, 0, 0, e.Bounds.Height - 2, e.Bounds.Height - 2);

				e.Graphics.DrawImage(bmp, e.Bounds.Left +1, e.Bounds.Top +1);
			}
		}
	}

    /// <summary>
    /// Represents the method that will handle converting a System.Windows.Forms.ListControl.
    /// </summary>
    /// <param name="sender">Represents the method that will handle converting
    /// a <see cref="System.Windows.Forms.ListControl."/></param>
    /// <param name="e">A <see cref="System.Windows.Forms.ListControlConvertEventArgs"/>
    /// that contains the event data.</param>
    public delegate void ImageListControlConvertEventHandler(object sender, ImageListControlConvertEventArgs e);

    /// <summary>
    /// Extends the System.Windows.Forms.ListControlConvertEventArgs class
    /// to allow transparent access to an Zyrenth.Winforms.ImageListBoxItem's
    /// Item property.
    /// </summary>
    public class ImageListControlConvertEventArgs : ListControlConvertEventArgs
    {
        /// <summary>
        /// Gets a data source item.
        /// </summary>
        public new object ListItem
        {

            get
            {
                ImageListBoxItem i = base.ListItem as ImageListBoxItem;
                if (i != null)
                    return i.Item;
                else
                    return base.ListItem;
            }
        }

        public ImageListControlConvertEventArgs(object value, System.Type desiredType, object listItem)
            : base(value, desiredType, listItem)
        {
        }
    }
}
