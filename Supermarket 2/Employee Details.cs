﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermarket_2
{
    public partial class Employee_Details : Form
    {
        public Employee_Details()
        {
            InitializeComponent();
        }

        private void Employee_Details_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter.Fill(this.dataSet1.Employee);

            this.reportViewer2.RefreshReport();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Are you sure you want to logout?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                this.Close();
                Login login1 = new Login();
                login1.Show();
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CloseIcon_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void reportViewer2_Load(object sender, EventArgs e)
        {

        }
    }
}
