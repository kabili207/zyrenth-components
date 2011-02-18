namespace ZyrTest
{
	partial class Form1
	{
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.appointmentList1 = new Zyrenth.Components.AppointmentList();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.imageTreeView1 = new Zyrenth.Components.ImageTreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.imageListBox1 = new Zyrenth.Components.ImageListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.treeGridView1 = new Zyrenth.Components.TreeGridView();
            this.Column1 = new Zyrenth.Components.TreeGridColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.formattedTextBox1 = new Zyrenth.Components.FormattedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.extendedDateTimePicker1 = new Zyrenth.Components.ExtendedDateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeGridView1)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(287, 273);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.appointmentList1);
            this.tabPage1.Location = new System.Drawing.Point(4, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(279, 229);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Appointment List";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // appointmentList1
            // 
            this.appointmentList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.appointmentList1.AutoScroll = true;
            this.appointmentList1.Location = new System.Drawing.Point(0, 0);
            this.appointmentList1.Name = "appointmentList1";
            this.appointmentList1.SelectedIndex = -1;
            this.appointmentList1.SelectedItem = null;
            this.appointmentList1.Size = new System.Drawing.Size(279, 229);
            this.appointmentList1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.imageTreeView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(279, 229);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tree View";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // imageTreeView1
            // 
            this.imageTreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.imageTreeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.imageTreeView1.Location = new System.Drawing.Point(0, 0);
            this.imageTreeView1.Name = "imageTreeView1";
            this.imageTreeView1.Size = new System.Drawing.Size(279, 229);
            this.imageTreeView1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.imageListBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 40);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(279, 229);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "List Box";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // imageListBox1
            // 
            this.imageListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.imageListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.imageListBox1.FormattingEnabled = true;
            this.imageListBox1.IntegralHeight = false;
            this.imageListBox1.Location = new System.Drawing.Point(0, 0);
            this.imageListBox1.Name = "imageListBox1";
            this.imageListBox1.Size = new System.Drawing.Size(279, 229);
            this.imageListBox1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.treeGridView1);
            this.tabPage4.Location = new System.Drawing.Point(4, 40);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(279, 229);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Tree Grid";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // treeGridView1
            // 
            this.treeGridView1.AllowUserToAddRows = false;
            this.treeGridView1.AllowUserToDeleteRows = false;
            this.treeGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.treeGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.treeGridView1.ImageList = null;
            this.treeGridView1.Location = new System.Drawing.Point(0, 0);
            this.treeGridView1.Name = "treeGridView1";
            this.treeGridView1.RowHeadersVisible = false;
            this.treeGridView1.Size = new System.Drawing.Size(279, 229);
            this.treeGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.DefaultNodeImage = null;
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 40);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(279, 229);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Zoom Pic Box";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox2);
            this.tabPage6.Controls.Add(this.groupBox1);
            this.tabPage6.Location = new System.Drawing.Point(4, 40);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(279, 229);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Misc";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 45);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(115, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // formattedTextBox1
            // 
            this.formattedTextBox1.Location = new System.Drawing.Point(6, 19);
            this.formattedTextBox1.Name = "formattedTextBox1";
            this.formattedTextBox1.Size = new System.Drawing.Size(115, 20);
            this.formattedTextBox1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.formattedTextBox1);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(127, 77);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Formatted Text Box";
            // 
            // extendedDateTimePicker1
            // 
            this.extendedDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.extendedDateTimePicker1.Location = new System.Drawing.Point(6, 19);
            this.extendedDateTimePicker1.Name = "extendedDateTimePicker1";
            this.extendedDateTimePicker1.ShowCheckBox = true;
            this.extendedDateTimePicker1.Size = new System.Drawing.Size(115, 20);
            this.extendedDateTimePicker1.TabIndex = 3;
            this.extendedDateTimePicker1.Value = new System.DateTime(2011, 2, 17, 19, 39, 1, 53);
            this.extendedDateTimePicker1.CheckedChanged += new System.EventHandler(this.extendedDateTimePicker1_CheckedChanged);
            this.extendedDateTimePicker1.ValueChanged += new System.EventHandler(this.extendedDateTimePicker1_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblDateTime);
            this.groupBox2.Controls.Add(this.extendedDateTimePicker1);
            this.groupBox2.Location = new System.Drawing.Point(139, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(127, 77);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DateTime Picker";
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Location = new System.Drawing.Point(6, 48);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(40, 13);
            this.lblDateTime.TabIndex = 4;
            this.lblDateTime.Text = "Value: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 277);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeGridView1)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private Zyrenth.Components.AppointmentList appointmentList1;
		private System.Windows.Forms.TabPage tabPage2;
		private Zyrenth.Components.ImageTreeView imageTreeView1;
		private System.Windows.Forms.TabPage tabPage3;
		private Zyrenth.Components.ImageListBox imageListBox1;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TabPage tabPage6;
		private Zyrenth.Components.TreeGridView treeGridView1;
		private Zyrenth.Components.TreeGridColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.ComboBox comboBox1;
        private Zyrenth.Components.FormattedTextBox formattedTextBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblDateTime;
        private Zyrenth.Components.ExtendedDateTimePicker extendedDateTimePicker1;
	}
}