using System;
using Gtk;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace Zyrenth.Gtksharp
{
	[System.ComponentModel.ToolboxItem(true)]
	public class AppointmentList : global::Gtk.DrawingArea
	{
		public AppointmentList ()
		{
			// Insert initialization code here.
			
		}
		protected override bool OnButtonPressEvent (Gdk.EventButton ev)
		{
			// Insert button press handling code here.
			return base.OnButtonPressEvent (ev);
		}
		protected override bool OnExposeEvent (Gdk.EventExpose ev)
		{
			base.OnExposeEvent (ev);
			// Insert drawing code here.
			this.GdkWindow.DrawLine(this.Style.BaseGC(StateType.Normal), 0, 0, 400, 300);
			
			return true;
		}
		protected override void OnSizeAllocated (Gdk.Rectangle allocation)
		{
			base.OnSizeAllocated (allocation);
			// Insert layout code here.
		}
		protected override void OnSizeRequested (ref global::Gtk.Requisition requisition)
		{
			// Calculate desired size here.
			requisition.Height = 100;
			requisition.Width = 100;
		}
	}
}

