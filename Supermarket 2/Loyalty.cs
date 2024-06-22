using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mail;
using System.Windows.Forms;

namespace Supermarket_2
{
    public partial class Loyalty : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        public Loyalty()
        {
            InitializeComponent();
        }

        private void Loyalty_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=M_HASRA\\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True");
            conn.Open();
            string query = "SELECT TOP 1 C_ID FROM Loyal_Customer ORDER BY C_ID DESC";
            cmd = new System.Data.SqlClient.SqlCommand(query, conn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                string ID = Convert.ToString(dr["C_ID"]);
                ID = ID.Substring(1);
                int EID = Convert.ToInt32(ID) + 1;
                txtID.Text = "L" + Convert.ToString(EID);
            }
            conn.Close();

        }

        private void CloseIcon_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

            try
            {
                conn = new SqlConnection("Data Source=M_HASRA\\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True");
                conn.Open();



                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtMail.Text) || string.IsNullOrEmpty(txtNIC.Text) || string.IsNullOrEmpty(txtTP.Text)||string.IsNullOrEmpty(txtID.Text))

                {
                    MessageBox.Show("filelds  cannot be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtName.Focus();
                }
                else
                {
                    if ((txtTP.Text.Length > 10) || (txtTP.Text.Length < 10))
                    {
                        MessageBox.Show("Telephone number must be 10 numbers ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtTP.Focus();
                    }
                    else if (!Regex.IsMatch(txtMail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                    {
                        MessageBox.Show(" Enter a valid email. Ex:name@gmail.com", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMail.Focus();
                    }

                    else
                    {
                        cmd = new SqlCommand("Insert into Loyal_Customer values('" + txtID.Text + "','" + txtName.Text + "','" + txtTP.Text + "','" + txtMail.Text + "','" + txtNIC.Text + "')", conn);
                        int i = cmd.ExecuteNonQuery();

                        if (i == 1)
                        {
                            MessageBox.Show("Succesfully registered", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        else
                        {
                            MessageBox.Show("Please check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        conn.Close();
                    }


                }




            }
            catch (Exception)
            {
                MessageBox.Show("All fields must filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (txtTP.Text.Length >= 10 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txtNIC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtTP.Text.Length >= 12 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

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

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            Cashier cashier2= new Cashier();    
            cashier2.Show();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
