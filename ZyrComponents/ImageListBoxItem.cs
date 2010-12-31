using System;
using System.Drawing;

namespace Zyrenth.Components
{

	[Serializable]
	public class ImageListBoxItem
	{
		public object Item { get; set; }
		public Bitmap Image { get; set; }

		public ImageListBoxItem(object o, Bitmap image)
		{
			Item = o;
			Image = image;
		}

		public override string ToString() { return Item.ToString(); }
	}
}
