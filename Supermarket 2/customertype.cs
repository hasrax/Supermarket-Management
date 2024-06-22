using Bill;
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
    public partial class customertype : Form
    {
       
        
        public customertype()
        {
            InitializeComponent();
        }

        private void btnloyalty_Click(object sender, EventArgs e)
        {
            panel1.Visible= true;
        }

        private void customertype_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            Cashier c2=new Cashier();
            c2.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnConfirm2_Click(object sender, EventArgs e)
        {
            Myconnection obj = new Myconnection();

            obj.con.Open();
            obj.da = new SqlDataAdapter("SELECT COUNT(*) FROM Loyal_Customer WHERE Telephone='" + txttp.Text + "'", obj.con);
            obj.dt = new DataTable();
            obj.da.Fill(obj.dt);
            if (obj.dt.Rows[0][0].ToString() == "1")
            {
                BILL b1 = new BILL();
                b1.cmb_discount.Text = "Loyal Customer";
                b1.cmb_discount.Enabled = false;
                b1.Show();
            }
            else
            {
                MessageBox.Show("Invalid Loyalty Customer Number","Error",MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
            }
            obj.con.Close();

        }

        private void btnnormal_Click(object sender, EventArgs e)
        {
            BILL b2= new BILL();
            b2.cmb_discount.Text = "Normal Customer";
            b2.cmb_discount.Enabled = false;
            b2.Show();

        }
    }
}
