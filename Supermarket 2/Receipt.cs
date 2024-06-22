using System;
using System.Collections;
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
    public partial class Receipt : Form
    {
        public Receipt()
        {
            InitializeComponent();
        }


        private void dt_Receipt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Receipt_Load(object sender, EventArgs e)
        {
            txtBill.Text = LoginGlobal.NetTotal;
            txtEID.Text = LoginGlobal.EID;
            txtInvoice.Text = LoginGlobal.InvoiceID;
            balance.Text=LoginGlobal.balance.ToString();
            lbl_dt.Text= DateTime.Today.ToShortDateString();
            Myconnection obj=new Myconnection();
            obj.con.Open();

            string query = "Select Product_Name,Selling_Price,Quantity,Total from Receipt where invoice_id='" + LoginGlobal.InvoiceID+"'";
           /* SqlCommand command = new SqlCommand(query, obj.con);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);*/
            SqlDataAdapter da  = new SqlDataAdapter(query,obj.con);
            DataTable dt = new DataTable();
            da.Fill(dt);


            dt_Receipt.DataSource = dt;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CloseIcon_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtBill_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void balance_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
