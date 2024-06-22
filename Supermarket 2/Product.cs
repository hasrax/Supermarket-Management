using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mail;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using AForge.Video;



namespace Supermarket_2
{
    public partial class Product : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader drr;
        public Product()
        {
            InitializeComponent();
        }

        private void CloseIcon_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Registerbtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=M_HASRA\\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True"))
                {
                    conn.Open();
                    SqlCommand cmd;
                    SqlCommand cmd2;

                    if (string.IsNullOrEmpty(ProdName.Text) || string.IsNullOrEmpty(Prodsellingprice.Text) || string.IsNullOrEmpty(ProdPrice.Text) || string.IsNullOrEmpty(Prodcategory.Text))
                    {
                        MessageBox.Show("Fields cannot be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ProdName.Focus();
                    }
                    else
                    {
                        double Profit = ((Convert.ToDouble(Prodsellingprice.Text) - Convert.ToDouble(ProdPrice.Text)) * (Convert.ToDouble(txtQuantity.Text)));

                        if (Profit < 0)
                        {
                            MessageBox.Show("Please check the prices again. Profit cannot be less than zero.", "Info", MessageBoxButtons.RetryCancel);
                            ProdPrice.Clear();
                            Prodsellingprice.Clear();
                            ProdPrice.Focus();
                        }
                        else
                        {
                            string insertProductQuery = "INSERT INTO Product VALUES(@Read, @Name, @Date,@Category)";
                            string insertPriceQuery = "INSERT INTO Price VALUES(@Read, @Category, @Price, @Quantity, @SellingPrice, @Profit)";

                            cmd = new SqlCommand(insertProductQuery, conn);
                            cmd.Parameters.AddWithValue("@Read", txtRead.Text);
                            cmd.Parameters.AddWithValue("@Name", ProdName.Text);
                            cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
                            cmd.Parameters.AddWithValue("@Category", Prodcategory.Text);

                            cmd2 = new SqlCommand(insertPriceQuery, conn);
                            cmd2.Parameters.AddWithValue("@Read", txtRead.Text);
                            cmd2.Parameters.AddWithValue("@Category", Prodcategory.Text);
                            cmd2.Parameters.AddWithValue("@Price", ProdPrice.Text);
                            cmd2.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                            cmd2.Parameters.AddWithValue("@SellingPrice", Prodsellingprice.Text);
                            cmd2.Parameters.AddWithValue("@Profit", Profit);

                            int i = cmd.ExecuteNonQuery();
                            int j = cmd2.ExecuteNonQuery();

                            if (i == 1 || j == 1)
                            {
                                MessageBox.Show("Successfully registered", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                           
                            else
                            {
                                MessageBox.Show("Failed to register product and price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            


        }


        private void ProdName_TextChanged(object sender, EventArgs e)
        {
        }

        private void ProdName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void Prodprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Prodsellingprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
        FilterInfoCollection filter;
        VideoCaptureDevice captureDevice;

        private void Product_Load(object sender, EventArgs e)
        {
            panelTable.Visible= false;
            btnAddProd.Visible=false;
            // TODO: This line of code loads data into the 'supermarketDataSet4.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.supermarketDataSet4.Product);
            string query = "SELECT Category_ID FROM Category ";
            conn = new SqlConnection("Data Source=M_HASRA\\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True");
            conn.Open();
            
            cmd = new System.Data.SqlClient.SqlCommand(query, conn);
            drr = cmd.ExecuteReader();
            while (drr.Read())
            {
                Prodcategory.Items.Add(drr["Category_ID"]);
            }

            conn.Close();
            
        }

        

        

        private void Product_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Barcode AddBarcode=new Barcode();
            AddBarcode.ShowDialog();
            if(!AddBarcode.Visible)
            {
                txtRead.Text = LoginGlobal.Barcode;
            }

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtRead_TextChanged(object sender, EventArgs e)
        {

        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=M_HASRA\\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True");
            conn.Open();
            SqlCommand cmd2;
            string UpdateProductQuery = "Update Product SET P_Name=@Name, stored_date=@date ,Category_ID=@Category WHERE BarCode = @Read ";
            string UpdatePriceQuery = "Update Price SET Category_ID = @Category,Manufactured_price=@Price,Measure_or_Quantity=@Quantity,Selling_price=@SellingPrice Where BarCode=@Read";

            cmd = new SqlCommand(UpdateProductQuery, conn);
            cmd.Parameters.AddWithValue("@Read", txtRead.Text);
            cmd.Parameters.AddWithValue("@Name", ProdName.Text);
            cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@Category", Prodcategory.Text);
            
            int i=cmd.ExecuteNonQuery();
           

            cmd2 = new SqlCommand(UpdatePriceQuery, conn);
            cmd2.Parameters.AddWithValue("@Read", txtRead.Text);
            cmd2.Parameters.AddWithValue("@Category", Prodcategory.Text);
            cmd2.Parameters.AddWithValue("@Price", ProdPrice.Text);
            cmd2.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
            cmd2.Parameters.AddWithValue("@SellingPrice", Prodsellingprice.Text);

            int j = cmd.ExecuteNonQuery();


            if (i == 1 || j == 1)
            {
                MessageBox.Show("Successfully Updated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show("Failed to Update Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {

            conn = new SqlConnection("Data Source=M_HASRA\\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True");
            conn.Open();
            SqlCommand cmd2;
                

            string DeleteProductQuery = "Delete * from Product WHERE BarCode = @Read ";
            string DeletePriceQuery = "Delete * from Price Where BarCode=@Read";

            cmd = new SqlCommand(DeleteProductQuery, conn);
            cmd.Parameters.AddWithValue("@Read", txtRead.Text);
            int i = cmd.ExecuteNonQuery();


            cmd2 = new SqlCommand(DeletePriceQuery, conn);
            cmd2.Parameters.AddWithValue("@Read", txtRead.Text);
            int j = cmd2.ExecuteNonQuery();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panelTable.Visible = true;
            
            BtnRemove.Visible = false;
            btnAddProd.Visible = true;
        }

        private void btnAddProd_Click(object sender, EventArgs e)
        {
            panelTable.Visible = false;
            panel3.Visible = true;

            BtnRemove.Visible =true;
            btnAddProd.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
    }
}
