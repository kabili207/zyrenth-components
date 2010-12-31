using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Zyrenth.Components
{
	/// <summary>
	/// Represents the method that will handle converting a System.Windows.Forms.ListControl.
	/// </summary>
	/// <param name="sender">Represents the method that will handle converting
	/// a <see cref="System.Windows.Forms.ListControl."/></param>
	/// <param name="e">A <see cref="System.Windows.Forms.ListControlConvertEventArgs"/>
	/// that contains the event data.</param>
	public delegate void ImageListControlConvertEventHandler(object sender, ImageListControlConvertEventArgs e);

	/// <summary>
	/// Extends the <see cref="System.Windows.Forms.ListControlConvertEventArgs"/> class
	/// to allow transparent access to an <see cref="Zyrenth.Components.ImageListBoxItem"/>'s
	/// Item property.
	/// </summary>
	public class ImageListControlConvertEventArgs : ListControlConvertEventArgs
	{
		/// <summary>
		/// Gets a data source item.
		/// </summary>
		public new object ListItem
		{
			
			get {
				ImageListBoxItem i = base.ListItem as ImageListBoxItem;
				if (i != null)
					return i.Item;
				else
					return base.ListItem;
			}
		}

		public ImageListControlConvertEventArgs(object value, System.Type desiredType, object listItem)
			:base(value, desiredType, listItem)
		{
		}
	}

	/// <summary>
	/// Extends the <see cref="System.Windows.Forms.ListBox"/> control to display an image
	/// to the left of the item.
	/// </summary>
	public class ImageListBox : ListBox
	{
		[DefaultValue(false)]
		public bool HideImage { get; set; }


		private List<Bitmap> _images;

		/// <summary>
		/// Occurs when the control checks for formatting changes
		/// </summary>
		/// <remarks>Hides the ListControl Format event</remarks>
		public new event ImageListControlConvertEventHandler Format;

		public ImageListBox()
		{
			this.DrawMode = DrawMode.OwnerDrawFixed; // We're using custom drawing.
			_images = new List<Bitmap>();
			
		}

		/// <summary>
		/// Raises the <see cref="Zyrenth.Components.ImageListBox.Format"/> event.
		/// </summary>
		protected override void OnFormat(ListControlConvertEventArgs e)
		{
			ImageListControlConvertEventArgs args =
				new ImageListControlConvertEventArgs(e.Value, e.DesiredType, e.ListItem);
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
			
			// Make sure we're not trying to draw something that isn't there.
			if (e.Index >= this.Items.Count || e.Index <= -1)
			{
				// The designer draws a single 'fake' item
				// We'll use this to draw the name of the control
				if (this.DesignMode)
					e.Graphics.DrawString(this.Name, this.Font, SystemBrushes.WindowText,
						new PointF(e.Bounds.X, e.Bounds.Y));
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
			Brush front;

			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				back = SystemBrushes.Highlight;
				front = SystemBrushes.HighlightText;
			}
			else
			{
				back = SystemBrushes.Window;
				front = SystemBrushes.WindowText;
			}

			e.Graphics.FillRectangle(back, e.Bounds);
			e.Graphics.DrawString(text, this.Font, front, stringLoc, StringFormat.GenericDefault);

			ImageListBoxItem ilist = this.Items[e.Index] as ImageListBoxItem;
			if (ilist == null || HideImage || ilist.Image == null)
				return;

			e.Graphics.DrawImage(ilist.Image, e.Bounds.Left + 1, e.Bounds.Top + 1,
				e.Bounds.Height - 2, e.Bounds.Height - 2);
		}
	}
}
