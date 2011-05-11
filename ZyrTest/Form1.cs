using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zyrenth.Winforms;
using Zyrenth.Collections;

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
			RedBlackTree<int> t = new RedBlackTree<int>();
			int NUMS = 400000;
			int GAP = 35461;

			//Console.WriteLine("Checking... (no more output means success)");

			for (int i = GAP; i != 0; i = (i + GAP) % NUMS)
				t.Add(i);

			if (t.findMin() != 1 || t.findMax() != NUMS - 1)
				MessageBox.Show("FindMin or FindMax error!");

			for (int i = 1; i < NUMS; i++)
				if (t.find(i) != i)
					MessageBox.Show("Find error1!");

            initializeAppointmentList();
            initializeTreeGrid();
            initializeTreeView();
            initializeMisc();
            initializeListBox();

			zyrTabControl1.AddForm(new MdiTestForm() { Text = "Test stuff" });
			zyrTabControl1.AddForm(new MdiTestForm() { Text = "More stuff" });
			
        }

        private void initializeAppointmentList()
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
        }

        private void initializeTreeGrid()
        {
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

        private void initializeTreeView()
        {
            ImageTreeNode node = new ImageTreeNode();
            node.Image = Properties.Resources.Blog;
            node.Text = "Node 1";
            imageTreeView1.Nodes.Add(node);

            ImageTreeNode child = new ImageTreeNode();
            child.Image = Properties.Resources.Danger;
            child.Text = "Subnode 1";
            child.Active = false;
            node.Nodes.Add(child);

            child = new ImageTreeNode();
            child.Image = Properties.Resources.Alert;
            child.Text = "Subnode 2";
            node.Nodes.Add(child);

            node = new ImageTreeNode();
            node.Image = Properties.Resources.Application;
            node.Text = "Node 2";
            node.Active = false;
            imageTreeView1.Nodes.Add(node);

            child = new ImageTreeNode();
            child.Image = Properties.Resources.About;
            child.Text = "Subnode 3";
            node.Nodes.Add(child);

            node = new ImageTreeNode();
            node.Image = Properties.Resources.Address_book;
            node.Text = "Node 3";
            imageTreeView1.Nodes.Add(node);
        }
        private void initializeListBox()
        {
            imageListBox1.Items.AddRange(new ImageListBoxItem[] {
                new ImageListBoxItem("Item 1", Properties.Resources.About),
                new ImageListBoxItem("Item 2", Properties.Resources.Address_book),
                new ImageListBoxItem("Item 3", Properties.Resources.Alert),
                new ImageListBoxItem("Item 4", Properties.Resources.Application),
                new ImageListBoxItem("Item 5", Properties.Resources.Blog),
                new ImageListBoxItem("Item 6", Properties.Resources.Danger)
            });
        }

        private void initializeMisc()
        {

            comboBox1.DataSource = Enum.GetValues(typeof(FormattedTextBoxType));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            formattedTextBox1.InputMask = (FormattedTextBoxType)comboBox1.SelectedItem;
        }

        private void extendedDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            lblDateTime.Text = "Value: " + (extendedDateTimePicker1.Value.HasValue ? extendedDateTimePicker1.Value.Value.ToShortDateString() : "");
        }

        private void extendedDateTimePicker1_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}