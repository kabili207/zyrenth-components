using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zyrenth.Components;

namespace ZyrTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
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

			appointmentList1.Appointments.Add(apmt1);
			appointmentList1.Appointments.Add(apmt2);
			appointmentList1.Appointments.Add(apmt3);

			TreeGridNode node = treeGridView1.Nodes.Add("Node1");
			node.Nodes.Add("SubNode1");
			node.Nodes.Add("SubNode2");
			node = treeGridView1.Nodes.Add("Node2");
			node.Nodes.Add("SubNode3");
			node = treeGridView1.Nodes.Add("Node3");
			node.Nodes.Add("SubNode4");
			node.Nodes.Add("SubNode5");
			node.Nodes.Add("SubNode6");
		}
	}
}
