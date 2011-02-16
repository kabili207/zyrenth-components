using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;


namespace Zyrenth.Components
{
	/// <summary>
	/// Represents a Windows control that displays AppointmentItems
	/// similar to Microsoft Outlook 2010
	/// </summary>
    [ToolboxBitmap(typeof(AppointmentList))]
	public partial class AppointmentList : UserControl
	{
		public const int NoMatches = -1;

		private List<AppointmentItem> _appointments;

		/// <summary>
		/// Gets or sets the Appointment items from the AppointmentList
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public List<AppointmentItem> Appointments
		{
			get { return _appointments; }
			set
			{
				_appointments = value.OrderBy(a => a.Start).ThenBy(a => a.End).ToList();
				OnAppointmentChanged(EventArgs.Empty);
			}
		}

		private int index = NoMatches;
		private KeyValuePair<int, AppointmentItem> _selectedItem;

		/// <summary>
		/// Gets or sets the index of the currently selected item
		/// </summary>
		public int SelectedIndex
		{
			get { return index; }
			set
			{
				bool r = index == value;
				index = value;
				OnSelectedIndexChanged(EventArgs.Empty);
				if (!r)
					this.Refresh();
			}
		}


		/// <summary>
		/// Gets or sets the currently selected item
		/// </summary>
		public AppointmentItem SelectedItem
		{
			get
			{
				if (_selectedItem.Key == index)
				{
					return _selectedItem.Value;
				}

				try
				{
					AppointmentItem a = _appointments.ElementAt(index);
					_selectedItem = new KeyValuePair<int, AppointmentItem>(index, a);
					return a;
				}
				catch (ArgumentOutOfRangeException e)
				{
					return null;
				}
			}
			set
			{
				index = Array.IndexOf(_appointments.ToArray(), value);
				// Helps reduce flicker caused by unneeded refreshing
				if (_selectedItem.Key != index || _selectedItem.Value != value)
				{
					_selectedItem = new KeyValuePair<int, AppointmentItem>(index, value);
					OnSelectedIndexChanged(EventArgs.Empty);
					this.Refresh();
				}
			}
		}

		/// <summary>
		/// Occurs whenever the Appointments are changed.
		/// </summary>
		public event EventHandler AppointmentsChanged;

		/// <summary>
		/// Occurs whenever the SelectedIndex is changed.
		/// </summary>
		public event EventHandler SelectedIndexChanged;

		/// <summary>
		/// Initializes a new <see cref="AppointmentList"/> that contains no Appointment objects
		/// </summary>
		public AppointmentList()
		{
			InitializeComponent();

			this.Appointments = new List<AppointmentItem>();
			_selectedItem = new KeyValuePair<int, AppointmentItem>(NoMatches, null);

			// Reduces flicker by first drawing the new frame off screen and
			// then copying it to the visible area. Causes a minor amount of
			// lag in the draw, but this is far more desireable than flicker.
			this.SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.UserPaint |
				ControlStyles.DoubleBuffer, true);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppointmentList));
			this.SuspendLayout();
			// 
			// AppointmentList
			// 
			this.AutoScroll = true;
			this.Name = "AppointmentList";
			this.Size = new System.Drawing.Size(177, 117);
			this.ResumeLayout(false);

		}

		#endregion

		// Used by Visual Studio designer
		public void ResetAppointments()
		{
			Appointments = new List<AppointmentItem>();
		}

		// Used by Visual Studio designer
		public bool ShouldSerializeAppointments()
		{
			return Appointments.Count != 0;
		}

		/// <summary>
		/// Restricts a value to a certain range
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The initial value</param>
		/// <param name="min">The minimum value</param>
		/// <param name="max">The maximum value</param>
		/// <returns>A clamped value that lies between min and max</returns>
		public static T Clamp<T>(T value, T min, T max)
			where T : System.IComparable<T>
		{
			T result = value;
			if (value.CompareTo(max) > 0)
				result = max;
			if (value.CompareTo(min) < 0)
				result = min;
			return result;
		}

		/// <summary>
		/// Raises the AppointmentChanged event
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnAppointmentChanged(EventArgs e)
		{
			if (AppointmentsChanged != null)
				AppointmentsChanged(this, EventArgs.Empty);
			SelectedIndex = NoMatches;
			_selectedItem = new KeyValuePair<int, AppointmentItem>(NoMatches, null);
			this.Refresh();
		}

		/// <summary>
		/// Raises the SelectedIndexChanged event
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if (SelectedIndexChanged != null)
				SelectedIndexChanged(this, e);
		}

		/// <summary>
		/// Returns the zero-based index from the item at the specified coordinates
		/// </summary>
		/// <param name="p">A <see cref="System.Drawing.Point"/> object containing the
		/// coordinates used to obtain the item index.</param>
		/// <returns>The zero-based index of the item at the specified coordinates</returns>
		public int IndexFromPoint(Point p)
		{
			foreach (AppointmentItem a in _appointments)
			{
				if (a.Bounds.Contains(p))
				{
					return Array.IndexOf(_appointments.ToArray(), a);
				}
			}
			return NoMatches;
		}

		/// <summary>
		/// Returns the zero-based index from the item at the specified coordinates
		/// </summary>
		/// <param name="x">The x-coordinate of the location to search.</param>
		/// <param name="y">The y-coordinate of the location to search.</param>
		/// <returns>The zero-based index of the item at the specified coordinates</returns>
		public int IndexFromPoint(int x, int y)
		{
			return IndexFromPoint(new Point(x, y));
		}

		protected override void OnLeave(EventArgs e)
		{
			SelectedIndex = -1;
			base.OnLeave(e);
		}

		/// <summary>
		/// Paints the control
		/// </summary>
		/// <param name="e">A <see cref="PaintEventArgs"/> that contains event arguments</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;


			// Translate the coordinates to the currently scrolled position
			Matrix m = new Matrix();
			m.Translate(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, MatrixOrder.Append);
			graphics.Transform = m;

			int rightPad = 0;
			if (this.VerticalScroll.Visible)
				rightPad = SystemInformation.VerticalScrollBarWidth;
			// Creates a rectangle that represents the control's visible area
			Rectangle limit = new Rectangle(Padding.Left, Padding.Top,
				Bounds.Width - Padding.Horizontal - rightPad,
				Bounds.Height - Padding.Vertical);

			if (this.DesignMode)
			{
				Pen sysPen = new Pen(SystemBrushes.WindowFrame);
				Rectangle size = new Rectangle(Padding.Left, Padding.Top,
				Bounds.Width - Padding.Horizontal,
				Bounds.Height - Padding.Vertical);
				sysPen.DashStyle = DashStyle.Dash;
				graphics.DrawRectangle(sysPen, limit);
			}

			// Keeps track of the current position
			int X = this.Padding.Left;
			int Y = this.Padding.Top;
			int boxPad = 3;

			// Use system brushes and fonts to maintain a uniform look and feel
			Brush brush = SystemBrushes.WindowText;
			Font fontNorm = this.Parent.Font;
			if (fontNorm == null)
				fontNorm = SystemFonts.DefaultFont;
			Font fontBold = new Font(fontNorm, FontStyle.Bold);

			StringFormat sf = new StringFormat();
			sf.Trimming = StringTrimming.Character;

			if (_appointments.Count == 0)
			{
				if (DesignMode)
					graphics.DrawString(this.Name, fontNorm, brush, new Point(X, Y + boxPad), sf);
				else
					graphics.DrawString("No appointments found", fontBold, brush, new Point(X, Y + boxPad), sf);
				return;
			}


			// Group the appointments together by their start dates
			Lookup<DateTime, AppointmentItem> dates;
			dates = (Lookup<DateTime, AppointmentItem>)_appointments.ToLookup(a => a.Start.Date, a => a);


			foreach (IGrouping<DateTime, AppointmentItem> group in dates)
			{
				Y += boxPad;

				// Calculates the height of the long date and draws it to the control
				string text = group.Key.ToLongDateString();
				int h = (int)graphics.MeasureString(text, fontBold).Height;
				graphics.DrawString(text, fontBold, brush, new Point(X, Y), sf);

				Y += h;

				foreach (AppointmentItem a in group)
				{
					int sub = (int)graphics.MeasureString(a.Subject, fontBold, limit.Width, sf).Height;

					string temp = a.Start.ToShortTimeString() + " - "
						+ a.End.ToShortTimeString();
					if (!String.IsNullOrWhiteSpace(a.Location))
						temp += Environment.NewLine + a.Location;

					int detail = (int)graphics.MeasureString(temp, fontNorm, limit.Width, sf).Height;

					Rectangle box = new Rectangle(X + boxPad, Y + boxPad,
						limit.Width - (boxPad * 2), sub + detail + (boxPad * 2));

					a.Bounds = box;

					Color baseColor;
					Brush baseFont;

					// Sets the "button's" font and color to match the system
					if (SelectedItem == a)
					{
						baseFont = SystemBrushes.HighlightText;
						baseColor = SystemColors.Highlight;
					}
					else
					{
						baseFont = SystemBrushes.ControlText;
						baseColor = SystemColors.Control;
					}

					// Create new colors that are slightly lighter and
					// darker than the system base color
					Color colorL = Color.FromArgb(
						Clamp(baseColor.R + 35, 0, 255),
						Clamp(baseColor.G + 35, 0, 255),
						Clamp(baseColor.B + 35, 0, 255));
					Color colorD = Color.FromArgb(
						Clamp(baseColor.R - 35, 0, 255),
						Clamp(baseColor.G - 35, 0, 255),
						Clamp(baseColor.B - 35, 0, 255));

					// And now use them to create a gradient brush
					Brush gradBrush = new LinearGradientBrush(box, colorL,
						colorD, LinearGradientMode.Vertical);

					// Fill in the appointment's box and draw the appointment info
					graphics.FillRectangle(gradBrush, box);
					graphics.DrawRectangle(SystemPens.ControlDark, box);

					Brush freeBrush = null;
					switch (a.Status)
					{
						case AppointmentStatus.Busy:
							freeBrush = new SolidBrush(Color.Blue);
							break;
						case AppointmentStatus.OutOfOffice:
							freeBrush = new SolidBrush(Color.Red);
							break;
						case AppointmentStatus.Tentative:
							freeBrush = new HatchBrush(
								HatchStyle.DarkUpwardDiagonal,
								Color.White, Color.Blue);
							break;
					}
					Rectangle freeRect = new Rectangle(box.X, box.Y, 5, box.Height);
					if (freeBrush != null)
					{
						graphics.FillRectangle(freeBrush, freeRect);
						graphics.DrawRectangle(SystemPens.ControlDark, box);
					}

					Rectangle head = new Rectangle(box.Left + boxPad, box.Top + boxPad, box.Width - (boxPad * 2), box.Height - (boxPad * 2));
					graphics.DrawString(a.Subject, fontBold, baseFont, head, sf);
					head.Height -= sub;
					head.Y += sub;
					graphics.DrawString(temp, fontNorm, baseFont, head, sf);

					Y = box.Bottom + boxPad;

				}
			}

			AutoScrollMinSize = new Size(0, Y);
			sf.Dispose();
		}


		protected override void OnMouseClick(MouseEventArgs e)
		{
			Point p = new Point(e.Location.X, e.Location.Y);
			p.X -= this.AutoScrollPosition.X;
			p.Y -= this.AutoScrollPosition.Y;
			SelectedIndex = IndexFromPoint(p);
		}

	}
}