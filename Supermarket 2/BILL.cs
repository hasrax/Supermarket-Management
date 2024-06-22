using Microsoft.Reporting.WinForms;
using Supermarket_2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bill
{
    public partial class BILL : Form
    {
        public BILL()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter ada;
        SqlDataReader dr;

        private void getcategory()
        {
            string Sql = "select Category_Name from Category";
            cmd = new SqlCommand(Sql, con);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            ada = new SqlDataAdapter(cmd);
            ada.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                cmb_category.Items.Add(dr["Category_Name"].ToString());
            }

        }

        private void getbarcodeinfo()
        {
            try
            {
                string str3 = "select p.BarCode,p.P_Name,r.Selling_Price from Product p,Price r where p.BarCode=r.BarCode and p.BarCode='" + txt_barcode.Text + "' ";
                cmd = new SqlCommand(str3, con);
                cmd.Parameters.AddWithValue("@BarCode", txt_barcode.Text);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txt_pname.Text = dr.GetValue(1).ToString();
                    txt_price.Text = dr.GetValue(2).ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void getEmp()
        {

        }
        private void Bill_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=M_HASRA\\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True");
            con.Open();
            lbl_date.Text=DateTime.Today.ToShortDateString();
            getcategory();
            
            txteid.Text=LoginGlobal.EID;
            getEmp();
            con.Close();

            panel8.Visible = false;

           

            Myconnection obj = new Myconnection();

            obj.con.Open();
            SqlCommand command = new SqlCommand("Select E_Name,Position from Employee Where E_ID='" + LoginGlobal.EID + "'", obj.con);

            SqlDataReader reader1 = command.ExecuteReader();

            if (reader1.Read())
            {
                lb_name.Text = Convert.ToString(reader1["E_Name"]);
                lb_position.Text = Convert.ToString(reader1["Position"]);
            }

           SqlConnection conn = new SqlConnection("Data Source=M_HASRA\\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True");
            conn.Open();
            string query = "SELECT TOP 1 Invoice_No FROM Billing_summary ORDER BY Invoice_No DESC";
            cmd = new System.Data.SqlClient.SqlCommand(query, conn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                string ID = Convert.ToString(dr["Invoice_No"]);
                ID = ID.Substring(1);
                int EID = Convert.ToInt32(ID) + 1;
                txt_invoice.Text = "I" + Convert.ToString(EID);
            }
            conn.Close();
            
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            try
            {
                txt_barcode.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                txt_pname.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                txt_price.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_barcode.Clear();
            txt_pname.Clear();
            txt_price.Clear();
            txt_quantity.Clear();
            txt_total.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }

        double net_total=0, discount;
        int n = 0;
        DataTable dta=new DataTable();
        int indexRow;
        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow newDataRow = dataGridView1.Rows[indexRow];
                if (cmb_discount.Text == "Loyal Customer")
                {
                    newDataRow.Cells[3].Value = txt_quantity.Text;
                    double total = Convert.ToInt32(txt_quantity.Text) * Convert.ToDouble(txt_price.Text);
                    newDataRow.Cells[4].Value = total.ToString();
                    net_total = net_total + total;
                    discount = net_total * 0.1;// Discounted price
                    net_total = net_total - discount;
                }
                else if (cmb_discount.Text == "Normal Customer")
                {
                    newDataRow.Cells[3].Value = txt_quantity.Text;
                    double total = Convert.ToInt32(txt_quantity.Text) * Convert.ToDouble(txt_price.Text);
                    newDataRow.Cells[4].Value = total.ToString();
                    net_total = net_total + total;
                }
                txt_total.Text = "Rs." + net_total.ToString();
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Enter the amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_quantity.Focus();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        double tot;
        private void btn_remove_Click(object sender, EventArgs e)
        {
            try
            {
                txt_total.Text = (net_total - tot).ToString();
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Not included any data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
           

        }

        private void txt_quantity_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Calculation()
        {
            con.Open();
            // try
            // {
            if (txt_barcode.Text == "")
            {
                MessageBox.Show("Input the BarCode", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (txt_quantity.Text == "")
            {
                MessageBox.Show("Please , Enter the quantity.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_quantity.Focus();

            }
            else if (cmb_discount.Text == "")
            {
                MessageBox.Show("Please , Select the discount ", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_discount.Focus();

            }
            else if (cmb_discount.Text == "Loyal Customer")
            {
                dataGridView1.Rows.Add(txt_barcode.Text, txt_pname.Text, txt_price.Text, txt_quantity.Text);
                double total = Convert.ToInt32(txt_quantity.Text) * Convert.ToDouble(txt_price.Text);
                dataGridView1.Rows[n++].Cells[4].Value = total;
                net_total = net_total + total;
                discount = net_total * 0.1;// Discounted price
                net_total = net_total - discount;
                //dataGridView1.Rows.Add("net_total -"(double)total);
                string Pname = txt_pname.Text;
                string quantity = txt_quantity.Text;
                string price = txt_price.Text;
                string tot = Convert.ToString(dataGridView1.Rows[n++].Cells[3].Value);
                string netT = Convert.ToString(net_total);

                try
                {
                    //con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into Receipt values('" + txt_invoice.Text + "','" + Pname + "','" + price + "','" + quantity + "','" + total + "')", con);
                    cmd.ExecuteNonQuery();
                    //con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }


            }
            else if (cmb_discount.Text == "Normal Customer")
            {
                dataGridView1.Rows.Add(txt_barcode.Text, txt_pname.Text, txt_price.Text, txt_quantity.Text);
                double total = Convert.ToInt32(txt_quantity.Text) * Convert.ToDouble(txt_price.Text);
                dataGridView1.Rows[n++].Cells[4].Value = total;
                net_total = net_total + total;

                string Pname = txt_pname.Text;
                string quantity = txt_quantity.Text;
                string price = txt_price.Text;



                foreach (DataGridViewRow row in dataGridView1.Rows)
{
    string tot = Convert.ToString(row.Cells[4].Value);
    // Do something with tot
}




                string netT = Convert.ToString(net_total);

                try
                {
                    //con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into Receipt values('" + txt_invoice.Text + "','" + Pname + "','" + price + "','" + quantity + "','" + total + "')", con);
                    cmd.ExecuteNonQuery();
                    //con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
                txt_total.Text =  net_total.ToString();
                con.Close();
            //}


            /* catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }*/
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            con.Open();
            getbarcodeinfo();
            con.Close();
        }

        private void btn_scan_Click(object sender, EventArgs e)
        {
           Barcode BarcodeInBill = new Barcode();
           BarcodeInBill.ShowDialog();
            txt_barcode.Text=LoginGlobal.Barcode;
        }

        private void txt_quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmb_category_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            ada = new SqlDataAdapter("select p.BarCode,p.P_Name,r.Selling_Price from Product p,Category c,Price r where r.Category_ID=p.Category_ID and p.Category_ID =c.Category_ID and c.Category_Name='" + cmb_category.SelectedItem.ToString() + "'", con);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();

        }

        private void btn_print_Click(object sender, EventArgs e)
        {
           

        }

        private void btn_summary_Click(object sender, EventArgs e)
        {
            summary s2= new summary();
            s2.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tot = Convert.ToDouble(dataGridView1.SelectedRows[0].Cells[4].Value);
        }

        private void CloseIcon_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void BtnLoyalty_Click(object sender, EventArgs e)
        {
            this.Close();
            Loyalty loyalty1= new Loyalty();
            loyalty1.Show();
        }

        private void BtnLogout_Click_1(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Are you sure you want to logout?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                this.Close();
                Login login1 = new Login();
                login1.Show();
            }
        }

        private void txt_total_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_eid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txteid_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_print_Click_1(object sender, EventArgs e)
        {
            LoginGlobal.InvoiceID = txt_invoice.Text;
            LoginGlobal.NetTotal = txt_total.Text;
            //LoginGlobal.balance = balance;
            con.Open();
            DataSet ds = new DataSet();
            
           
            //txt_invoice.Text.StartsWith("X????");
            try
            {

                if (txt_invoice.Text == "")
                {
                    MessageBox.Show("Please , Enter the Invoice.No", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_invoice.Focus();
                }
                
                else if (txteid.Text == "")
                {
                    MessageBox.Show("Fill the ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txteid.Focus();
                }
                else if (radiocard.Checked)
                {
                    cmd = new SqlCommand("Insert into Billing_summary values ('" + txt_invoice.Text + "','" + txteid.Text + "','" + lbl_date.Text + "','" + radiocard.Text + "','" + txt_total.Text + "') ", con);
                    //SqlCommand cmd2 = new SqlCommand("Insert into Profit_Analysis values('"++"')");
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        MessageBox.Show("Data added successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Data cannot add", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (radiocash.Checked)
                {
                    cmd = new SqlCommand("Insert into Billing_summary values  ('" + txt_invoice.Text + "','" + txteid.Text + "','" + lbl_date.Text + "','" + radiocash.Text + "' ,'" + txt_total.Text + "') ", con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        double cash = Convert.ToDouble(txt_cash.Text);
                        double net = Convert.ToDouble(net_total);
                        double balance = cash-net;
                        LoginGlobal.balance = balance;
                        Receipt receipt = new Receipt();
                        receipt.Show();
                    }
                    else
                    {
                        MessageBox.Show("Data cannot add", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
            
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radiocard_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radiocash_CheckedChanged(object sender, EventArgs e)
        {
            panel8.Visible = true;

        }

        private void txt_cash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
               
            Calculation();
            txt_barcode.Clear();
            txt_quantity.Clear();
            txt_price.Clear();
        }
    }

}
