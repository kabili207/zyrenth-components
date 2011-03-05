using System;
using Gtk;
using Zyrenth.Winforms;

public partial class MainWindow : Gtk.Window
{
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		AppointmentItem apmt1 = new AppointmentItem();
            AppointmentItem apmt2 = new AppointmentItem();
            AppointmentItem apmt3 = new AppointmentItem();

            apmt1.Start = new DateTime(2010, 1, 1, 12, 30, 0);
            apmt2.Start = new DateTime(2010, 1, 2, 12, 30, 0);
            apmt3.Start = new DateTime(2010, 1, 2, 22, 30, 0);
            apmt1.End = new DateTime(2010, 1, 1, 15, 30, 0);
            apmt2.End = new DateTime(2010, 1, 2, 15, 30, 0);
            apmt3.End = new DateTime(2010, 1, 2, 23, 0, 0);

            apmt1.Subject = "Something new";
            apmt2.Subject = "Your mom";
            apmt3.Subject = "Something else";

            apmt2.Location = "Dumpster behind Burger King";

            apmt2.Status = AppointmentStatus.Busy;
            apmt3.Status = AppointmentStatus.Tentative;

            appointmentlist1.Appointments.Add(apmt1);
            appointmentlist1.Appointments.Add(apmt2);
            appointmentlist1.Appointments.Add(apmt3);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}

