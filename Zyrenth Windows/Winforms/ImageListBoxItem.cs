using System;
using System.Drawing;

namespace Zyrenth.Winforms
{

	[Serializable]
	public class ImageListBoxItem
	{
		public object Item { get; set; }
		public Bitmap Image { get; set; }
		public bool Active { get; set; }

		public ImageListBoxItem(object o, Bitmap image)
			: this(o, image, true)
		{
		}

		public ImageListBoxItem(object o, Bitmap image, bool active)
		{
			Item = o;
			Image = image;
			Active = active;
		}

		public override string ToString() { return Item.ToString(); }


		/// <summary>
		/// Overloads the equality operator.
		/// </summary>
		/// <remarks>Should make use of an IComparer</remarks>
		/// <param name="l">The left-hand Appointment</param>
		/// <param name="r">The right-hand Appointment</param>
		/// <returns>True two appointments are equal</returns>
		public static bool operator ==(ImageListBoxItem l, ImageListBoxItem r)
		{

			// Cast to object to check for null
			if ((object)l == null && (object)r == null)
				return true;
			if ((object)l == null || (object)r == null)
				return false;
			return l.Item == r.Item;

		}

		/// <summary>
		/// Overloads the inequality operator
		/// </summary>
		/// <param name="l">The left-hand Appointment</param>
		/// <param name="r">The right-hand Appointment</param>
		/// <returns>True if the two Appointments are not equal</returns>
		public static bool operator !=(ImageListBoxItem l, ImageListBoxItem r)
		{
			return !(l == r);
		}

		/// <summary>
		/// Determines whether the specified Object is equal to the current ImageListBoxItem.
		/// </summary>
		/// <param name="obj">The Object to compare with the current ImageListBoxItem.</param>
		/// <returns>
		/// true if the specified ImageListBoxItem is equal to the
		/// current ImageListBoxItem; otherwise, false.
		/// </returns>
		public override bool Equals(Object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Point return false.
			ImageListBoxItem p = obj as ImageListBoxItem;
			if ((Object)p == null)
			{
				return false;
			}

			// Return true if the fields match:
			return this == p;
		}

		/// <summary>
		/// Returns the hash code for this ImageListBoxItem
		/// </summary>
		/// <returns>A hash code for the current ImageListBoxItem</returns>
		public override int GetHashCode()
		{
			return Item.GetHashCode();
		}
	}
}
