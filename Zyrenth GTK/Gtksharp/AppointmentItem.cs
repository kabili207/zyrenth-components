using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace Zyrenth.Gtksharp
{
	/// <summary>Specifies identifiers to represent availibity for an <see cref="AppointmentItem"/></summary>
	public enum AppointmentStatus {
		/// <summary>Represents available time</summary>
		Free,
		/// <summary>Represents unavailable time</summary>
		Busy,
		/// <summary>Represents time where availability is unknown</summary>
		Tentative,
		/// <summary>Represents time spent out of the office</summary>
		OutOfOffice }

	/// <summary>
	/// Represents an appointment
	/// </summary>
	/// <remarks>This class needs a lot of reworking before production</remarks>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[Serializable]
	public class AppointmentItem
	{
		public String Subject { get; set; }
		public String Location { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public Rectangle Bounds { get; set; }
		public object Tag { get; set; }

		private AppointmentStatus _status = AppointmentStatus.Free;

		[DefaultValue(typeof(AppointmentStatus),"Free")]
		public AppointmentStatus Status
		{
			get { return _status; }
			set { _status = value; }
		}

		public AppointmentItem()
		{
			Subject = "";
			Location = "";
			Start = DateTime.MinValue;
			End = DateTime.MinValue;
			/*Bounds = new Rectangle(0, 0, 0, 0);*/
			Tag = null;
		}

		/// <summary>
		/// Overloads the equality operator.
		/// </summary>
		/// <remarks>Should make use of an IComparer</remarks>
		/// <param name="l">The left-hand Appointment</param>
		/// <param name="r">The right-hand Appointment</param>
		/// <returns>True two appointments are equal</returns>
		public static bool operator ==(AppointmentItem l, AppointmentItem r)
		{

			// Cast to object to check for null
			if ((object)l == null && (object)r == null)
				return true;
			if ((object)l == null || (object)r == null)
				return false;
			return
				l.Start == r.Start &&
				l.End == r.End &&
				l.Subject == r.Subject &&
				l.Location == r.Location;
			
		}

		/// <summary>
		/// Overloads the inequality operator
		/// </summary>
		/// <param name="l">The left-hand Appointment</param>
		/// <param name="r">The right-hand Appointment</param>
		/// <returns>True if the two Appointments are not equal</returns>
		public static bool operator !=(AppointmentItem l, AppointmentItem r)
		{
			return !(l == r);
		}

		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is equal to the current <see cref="AppointmentItem"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="AppointmentItem"/>.</param>
		/// <returns>
		/// true if the specified <see cref="AppointmentItem"/> is equal to the
		/// current<see cref="AppointmentItem"/>; otherwise, false.
		/// </returns>
		public override bool Equals(Object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Point return false.
			AppointmentItem p = obj as AppointmentItem;
			if ((Object)p == null)
			{
				return false;
			}

			// Return true if the fields match:
			return this == p;
		}

		/// <summary>
		/// Returns the hash code for this Appointment
		/// </summary>
		/// <returns>A hash code for the current <see cref="AppointmentItem"/></returns>
		public override int GetHashCode()
		{
			return Subject.GetHashCode() +
				Location.GetHashCode() +
				Start.GetHashCode() +
				End.GetHashCode() ;
		}


	}
}
