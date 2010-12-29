using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace HRScreening.Components
{

	/// <summary>
	/// ZoomPicBox does what it says on the wrapper.
	/// </summary>
	/// <remarks>
	/// PictureBox doesn't lend itself well to overriding. Why not start with something basic and do the job properly?
	/// </remarks>

	public class ZoomPictureBox : ScrollableControl
	{

		Image _image;
		[
		Category("Appearance"),
		Description("The image to be displayed")
		]
		public Image Image
		{
			get { return _image; }
			set
			{
				_image = value;
				UpdateScaleFactor();
				Invalidate();
			}
		}

		float _zoom = 1.0f;
		[
		Category("Appearance"),
		Description("The zoom factor. Less than 1 to reduce. More than 1 to magnify.")
		]
		public float Zoom
		{
			get { return _zoom; }
			set
			{
				if (value < 0 || value < 0.00001)
					value = 0.00001f;
				_zoom = value;
				UpdateScaleFactor();
				Invalidate();
			}
		}

		public ZoomPictureBox()
		{
			//Double buffer the control
			this.SetStyle(ControlStyles.AllPaintingInWmPaint |
			  ControlStyles.UserPaint |
			  ControlStyles.ResizeRedraw |
			  ControlStyles.UserPaint |
			  ControlStyles.DoubleBuffer, true);

			this.AutoScroll = true;
		}

		/// <summary>
		/// Calculates the effective size of the image
		/// after zooming and updates the AutoScrollSize accordingly
		/// </summary>
		private void UpdateScaleFactor()
		{
			if (_image == null)
				this.AutoScrollMinSize = this.Size;
			else
			{
				this.AutoScrollMinSize = new Size(
				  (int)(this._image.Width * _zoom),
				  (int)(this._image.Height * _zoom)
				  );
			}
		}

		InterpolationMode _interpolationMode = InterpolationMode.High;
		[
		Category("Appearance"),
		Description("The interpolation mode used to smooth the drawing")
		]
		public InterpolationMode InterpolationMode
		{
			get { return _interpolationMode; }
			set { _interpolationMode = value; }
		}


		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			
			if (_image == null)
			{
				if (this.DesignMode)
				{
					Pen sysPen = new Pen(SystemBrushes.WindowFrame);
					Rectangle size = new Rectangle(Padding.Left, Padding.Top,
					Bounds.Width - Padding.Horizontal-1,
					Bounds.Height - Padding.Vertical-1);
					sysPen.DashStyle = DashStyle.Dash;
					e.Graphics.DrawRectangle(sysPen, size);
				}
				return;
			}
			//Set up a zoom matrix
			Matrix mx = new Matrix(_zoom, 0, 0, _zoom, 0, 0);
			//now translate the matrix into position for the scrollbars
			mx.Translate(this.AutoScrollPosition.X / _zoom, this.AutoScrollPosition.Y / _zoom);
			
			e.Graphics.Transform = mx;
			e.Graphics.InterpolationMode = _interpolationMode;

			//Draw the image ignoring the images resolution settings.
			e.Graphics.DrawImage(_image, new Rectangle(0, 0, this._image.Width, this._image.Height), 0, 0, _image.Width, _image.Height, GraphicsUnit.Pixel);
			base.OnPaint(e);
		}
	}

}
