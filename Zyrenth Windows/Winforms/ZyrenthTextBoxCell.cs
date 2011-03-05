using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Zyrenth.Winforms
{
	public class ZyrenthTextBoxCell : DataGridViewTextBoxCell {

		/*private int maxInputLength = 32767;
		private static DataGridViewTextBoxEditingControl editingControl;

		static ZyrenthTextBoxCell ()
		{
			editingControl = new DataGridViewTextBoxEditingControl();
			editingControl.Multiline = false;
			editingControl.BorderStyle = BorderStyle.None;
		}

		public ZyrenthTextBoxCell ()
		{
			base.ValueType = typeof (object);
		}

		public override Type FormattedValueType {
			get { return typeof(string); }
		}

		[DefaultValue (32767)]
		public virtual int MaxInputLength {
			get { return maxInputLength; }
			set {
				if (value < 0) {
					throw new ArgumentOutOfRangeException("MaxInputLength coudn't be less than 0.");
				}
				maxInputLength = value;
			}
		}

		public override Type ValueType {
			get { return base.ValueType; }
		}

		public override object Clone ()
		{
			ZyrenthTextBoxCell result = (ZyrenthTextBoxCell) base.Clone();
			result.maxInputLength = maxInputLength;
			return result;
		}

		[EditorBrowsable (EditorBrowsableState.Advanced)]
		public override void DetachEditingControl ()
		{
			if (DataGridView == null) {
				throw new InvalidOperationException("There is no associated DataGridView.");
			}

			//DataGridView.EditingControlInternal = null;
		}

		public override void InitializeEditingControl (int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			if (DataGridView == null) {
				throw new InvalidOperationException("There is no associated DataGridView.");
			}

			//DataGridView.EditingControlInternal = editingControl;

			editingControl.EditingControlDataGridView = DataGridView;
			editingControl.MaxLength = maxInputLength;

			if (initialFormattedValue == null || initialFormattedValue.ToString () == string.Empty)
				editingControl.Text = string.Empty;
			else
				editingControl.Text = initialFormattedValue.ToString ();

			editingControl.ApplyCellStyleToEditingControl(dataGridViewCellStyle);
			editingControl.PrepareEditingControlForEdit(true);
		}

		public override bool KeyEntersEditMode (KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
				return true;
			if ((int)e.KeyCode >= 48 && (int)e.KeyCode <= 90)
				return true;
			if ((int)e.KeyCode >= 96 && (int)e.KeyCode <= 111)
				return true;
			if (e.KeyCode == Keys.BrowserSearch || e.KeyCode == Keys.SelectMedia)
				return true;
			if ((int)e.KeyCode >= 186 && (int)e.KeyCode <= 229)
				return true;
			if (e.KeyCode == Keys.Attn || e.KeyCode == Keys.Packet)
				return true;
			if ((int)e.KeyCode >= 248 && (int)e.KeyCode <= 254)
				return true;

			return false;
		}

		public override void PositionEditingControl (bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			cellBounds.Size = new Size (cellBounds.Width - 5, cellBounds.Height + 2);
			cellBounds.Location = new Point (cellBounds.X + 3, ((cellBounds.Height - editingControl.Height) / 2) + cellBounds.Y - 1);

			base.PositionEditingControl (setLocation, setSize, cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);

			editingControl.Invalidate();
		}

		public override string ToString ()
		{
			return string.Format ("DataGridViewTextBoxCell {{ ColumnIndex={0}, RowIndex={1} }}", ColumnIndex, RowIndex);
		}

		protected override Rectangle GetContentBounds (Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (DataGridView == null)
				return Rectangle.Empty;

			object o = FormattedValue;
			Size s = Size.Empty;

			if (o != null) {
				s = DataGridViewCell.MeasureTextSize (graphics, o.ToString (), cellStyle.Font, TextFormatFlags.Default);
				s.Height += 2;
			}

			return new Rectangle (0, (OwningRow.Height - s.Height) / 2, s.Width, s.Height);
		}

		protected override Rectangle GetErrorIconBounds (Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (DataGridView == null || string.IsNullOrEmpty (ErrorText))
				return Rectangle.Empty;

			Size error_icon = new Size (12, 11);
			return new Rectangle (new Point (Size.Width - error_icon.Width - 5, (Size.Height - error_icon.Height) / 2), error_icon);
		}

		protected override Size GetPreferredSize (Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			object o = FormattedValue;

			if (o != null) {
				Size s = DataGridViewCell.MeasureTextSize (graphics, o.ToString (), cellStyle.Font, TextFormatFlags.Default);
				s.Height = Math.Max (s.Height, 20);
				s.Width += 2;
				return s;
			} else
				return new Size (21, 20);
		}

		protected override void OnEnter (int rowIndex, bool throughMouseClick)
		{
		}

		protected override void OnLeave (int rowIndex, bool throughMouseClick)
		{
		}

		protected override void OnMouseClick (DataGridViewCellMouseEventArgs e)
		{
		}
		 */
		protected override void Paint (Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			// Prepaint
			DataGridViewPaintParts pre = DataGridViewPaintParts.Background | DataGridViewPaintParts.SelectionBackground;
			pre = pre & paintParts;

			base.Paint (graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, pre);

			// Paint content
			if (!IsInEditMode && (paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground) {
				Color color = Selected ? cellStyle.SelectionForeColor : cellStyle.ForeColor;

				TextFormatFlags flags = TextFormatFlags.EndEllipsis | TextFormatFlags.TextBoxControl;
				flags |= AlignmentToFlags (cellStyle.Alignment);

				Rectangle contentbounds = cellBounds;
				
				contentbounds.Height -= cellStyle.Padding.Vertical;
				contentbounds.Width -= cellStyle.Padding.Horizontal;
				

				// If we are top aligned, give ourselves some padding from the top
				//if (((int)cellStyle.Alignment & 7) > 0) {
					contentbounds.Offset (cellStyle.Padding.Left, cellStyle.Padding.Top);
				//	contentbounds.Height -= 2;
				//}
				
				
				if (formattedValue != null)
					TextRenderer.DrawText (graphics, formattedValue.ToString (), cellStyle.Font, contentbounds, color, flags);
			}

			// Postpaint
			DataGridViewPaintParts post = DataGridViewPaintParts.Border | DataGridViewPaintParts.Focus | DataGridViewPaintParts.ErrorIcon;
			post = post & paintParts;

			base.Paint (graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, post);
		}
		
		internal TextFormatFlags AlignmentToFlags (DataGridViewContentAlignment align)
		{
			TextFormatFlags flags = TextFormatFlags.Default;

			switch (align) {
				case DataGridViewContentAlignment.BottomCenter:
					flags |= TextFormatFlags.Bottom;
					flags |= TextFormatFlags.HorizontalCenter;
					break;
				case DataGridViewContentAlignment.BottomLeft:
					flags |= TextFormatFlags.Bottom;
					break;
				case DataGridViewContentAlignment.BottomRight:
					flags |= TextFormatFlags.Bottom;
					flags |= TextFormatFlags.Right;
					break;
				case DataGridViewContentAlignment.MiddleCenter:
					flags |= TextFormatFlags.VerticalCenter;
					flags |= TextFormatFlags.HorizontalCenter;
					break;
				case DataGridViewContentAlignment.MiddleLeft:
					flags |= TextFormatFlags.VerticalCenter;
					break;
				case DataGridViewContentAlignment.MiddleRight:
					flags |= TextFormatFlags.VerticalCenter;
					flags |= TextFormatFlags.Right;
					break;
				case DataGridViewContentAlignment.TopLeft:
					flags |= TextFormatFlags.Top;
					break;
				case DataGridViewContentAlignment.TopCenter:
					flags |= TextFormatFlags.HorizontalCenter;
					flags |= TextFormatFlags.Top;
					break;
				case DataGridViewContentAlignment.TopRight:
					flags |= TextFormatFlags.Right;
					flags |= TextFormatFlags.Top;
					break;
			}

			return flags;
		}

		internal Rectangle AlignInRectangle (Rectangle outer, Size inner, DataGridViewContentAlignment align)
		{
			int x = 0;
			int y = 0;

			if (align == DataGridViewContentAlignment.BottomLeft || align == DataGridViewContentAlignment.MiddleLeft || align == DataGridViewContentAlignment.TopLeft)
				x = outer.X;
			else if (align == DataGridViewContentAlignment.BottomCenter || align == DataGridViewContentAlignment.MiddleCenter || align == DataGridViewContentAlignment.TopCenter)
				x = Math.Max (outer.X + ((outer.Width - inner.Width) / 2), outer.Left);
			else if (align == DataGridViewContentAlignment.BottomRight || align == DataGridViewContentAlignment.MiddleRight || align == DataGridViewContentAlignment.TopRight)
				x = Math.Max (outer.Right - inner.Width, outer.X);
			if (align == DataGridViewContentAlignment.TopCenter || align == DataGridViewContentAlignment.TopLeft || align == DataGridViewContentAlignment.TopRight)
				y = outer.Y;
			else if (align == DataGridViewContentAlignment.MiddleCenter || align == DataGridViewContentAlignment.MiddleLeft || align == DataGridViewContentAlignment.MiddleRight)
				y = Math.Max (outer.Y + (outer.Height - inner.Height) / 2, outer.Y);
			else if (align == DataGridViewContentAlignment.BottomCenter || align == DataGridViewContentAlignment.BottomRight || align == DataGridViewContentAlignment.BottomLeft)
				y = Math.Max (outer.Bottom - inner.Height, outer.Y);

			return new Rectangle (x, y, Math.Min (inner.Width, outer.Width), Math.Min (inner.Height, outer.Height));
		}

	}
}

