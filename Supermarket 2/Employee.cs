using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Threading;
using Guna.UI2.WinForms;
using System.Linq.Expressions;
using static System.Net.WebRequestMethods;
using System.Collections;
using MaterialSkin;
using System.Data.Common;
using ZXing;

namespace Supermarket_2
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=M_HASRA\\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True");
        SqlCommand cmd;
        SqlCommand cmd4;
        SqlDataAdapter adapter;

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CloseIcon_Click(object sender, EventArgs e)
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

        private void Employee_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'supermarketDataSet5.Employee' table. You can move, or remove it, as needed.
            //this.employeeTableAdapter2.Fill(this.supermarketDataSet5.Employee);
            // TODO: This line of code loads data into the 'supermarketDataSet2.Employee' table. You can move, or remove it, as needed.
            //this.employeeTableAdapter1.Fill(this.supermarketDataSet2.Employee);
            // TODO: This line of code loads data into the 'supermarketDataSet1.Employee' table. You can move, or remove it, as needed.
            //this.employeeTableAdapter.Fill(this.supermarketDataSet1.Employee);
            panel4.Visible = false;
            btnAddEmp.Visible = false;

            
            Myconnection obj= new Myconnection();
            obj.con.Open();
            SqlCommand command = new SqlCommand("Select E_Name,Position from Employee Where E_ID='" + LoginGlobal.EID + "'", obj.con);

            SqlDataReader reader1 = command.ExecuteReader();

            if (reader1.Read())
            {
                lblName.Text = Convert.ToString(reader1["E_Name"]);           

                lblPosition.Text = Convert.ToString(reader1["Position"]);
            }

            AutoIncriment LoadID = new AutoIncriment();
            EmpID.Text="E"+LoadID.IDIncriment("SELECT TOP 1 E_ID FROM Employee ORDER BY E_ID DESC", "E_ID");




        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void button3_Click(object sender, EventArgs e)
        {
        }
        private void Registerbtn_Click(object sender, EventArgs e)
        {
            {
                conn = new SqlConnection("Data Source=M_HASRA\\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True");
                conn.Open();



                if (string.IsNullOrEmpty(EmpName.Text) || string.IsNullOrEmpty(EmpID.Text) || string.IsNullOrEmpty(EmpMail.Text) || string.IsNullOrEmpty(EmpNIC.Text) || string.IsNullOrEmpty(EmpPass.Text) || string.IsNullOrEmpty(EmpAddr.Text))

                {
                    MessageBox.Show("filelds  cannot be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    EmpName.Focus();
                }
                else
                {
                    if ((Emptp.Text.Length > 10) || (Emptp.Text.Length < 10))
                    {
                        MessageBox.Show("Telephone number must be 10 numbers ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!Regex.IsMatch(EmpMail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                    {
                        MessageBox.Show(" Enter a valid email. Ex:name@gmail.com", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        EmpMail.Focus();
                    }

                    else
                    {
                        cmd = new SqlCommand("Insert into Employee values('" + EmpID.Text + "','" + EmpName.Text + "','" + Emptp.Text + "','" + EmpMail.Text + "','" + EmpAddr.Text + "','" + combo_category.Text + "','" + EmpPass.Text + "','" + EmpNIC.Text + "')", conn);
                        int i = cmd.ExecuteNonQuery();

                        if (i == 1)
                        {
                            MessageBox.Show("Succesfully registered", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            EmpName.Clear();
                            EmpID.Clear();
                            EmpPass.Clear();
                            EmpNIC.Clear();
                            combo_category.ResetText();
                            EmpAddr.Clear();
                            EmpMail.Clear();
                            Emptp.Clear();

                            AutoIncriment RegID = new AutoIncriment();
                            EmpID.Text="E"+RegID.IDIncriment("SELECT TOP 1 E_ID FROM Employee ORDER BY E_ID DESC", "E_ID");

                     }

                        else
                        {
                            MessageBox.Show("Please check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        conn.Close();
                    }


                }




            }

            // TODO: This line of code loads data into the 'supermarketDataSet2.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter1.Fill(this.supermarketDataSet2.Employee);
            // TODO: This line of code loads data into the 'supermarketDataSet1.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter.Fill(this.supermarketDataSet1.Employee);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EmpName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void Emptp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (Emptp.Text.Length >= 10 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
           try
           {
                SqlConnection delcon = new SqlConnection("Data Source=M_HASRA\\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True");
                delcon.Open();
                cmd4 = new SqlCommand("Delete from Employee where E_ID='" + EmpID.Text + "'", delcon);

                int i;
                i = cmd4.ExecuteNonQuery();

                if (i == 1)
                {
                    MessageBox.Show("Delete successfully");
                    AutoIncriment RegID = new AutoIncriment();
                    EmpID.Text = "E" + RegID.IDIncriment("SELECT TOP 1 E_ID FROM Employee ORDER BY E_ID DESC", "E_ID");
                }
                else
                {
                    MessageBox.Show("Please check details again", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                }
                delcon.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Please check details again", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void BtnViewEmp_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = true;
            BtnViewEmp.Visible = false;
            btnAddEmp.Visible = true;
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection("Data Source=M_HASRA\\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True");
                conn.Open();
                SqlCommand cmd2;
                string UpdateProductQuery = "Update Employee SET E_Name=@Name ,Telephone=@Tp,Email=@Email,Address=@Address,Position=@Category,Password=@Password,NIC=@NIC WHERE E_ID = @ID ";

                cmd = new SqlCommand(UpdateProductQuery, conn);
                cmd.Parameters.AddWithValue("@ID", EmpID);
                cmd.Parameters.AddWithValue("@Name", EmpName.Text);
                cmd.Parameters.AddWithValue("@Tp", Emptp.Text);
                cmd.Parameters.AddWithValue("@Email", EmpMail.Text);
                cmd.Parameters.AddWithValue("@Address", EmpAddr.Text);
                cmd.Parameters.AddWithValue("@Category", combo_category.Text);
                cmd.Parameters.AddWithValue("@Password", EmpPass.Text);
                cmd.Parameters.AddWithValue("@NIC", EmpNIC.Text);


                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("Succesfully Updated !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Details Update Failed ");
                }
                
            }
            catch (Exception) 
            {
                MessageBox.Show("Update required fields must be filled","Error",MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
            }

        }

        private void btnAddEmp_Click(object sender, EventArgs e)
        {
            btnAddEmp.Visible= false;
            BtnViewEmp.Visible = true;
            panel4.Visible = false;
            panel3.Visible = true;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CircleButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
