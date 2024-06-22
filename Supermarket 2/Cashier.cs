using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermarket_2
{
    public partial class Cashier : Form
    {
       
    public Cashier()
        {
            InitializeComponent();
        }

        private void Cashier_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CloseIcon_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnLoyalty_Click(object sender, EventArgs e)
        {
            Loyalty loyalty1= new Loyalty();
            loyalty1.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        private void billbtn_Click(object sender, EventArgs e)
        {
            customertype c1=new customertype();
            this.Close();
            c1.Show();
        }
    }
}
