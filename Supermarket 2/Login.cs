using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bill;
using static Guna.UI2.WinForms.Helpers.GraphicsHelper;

namespace Supermarket_2
{
    public partial class Login : Form
    {
        Myconnection db = new Myconnection();
        

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
         (
              int nLeftRect,
              int nTopRect,
              int nRightRect,
              int nBottomRect,
              int nWidthEllipse,
             int nHeightEllipse

          );
        public Login()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
        private void Login_Load(object sender, EventArgs e)
        {
            int cornerRadius = 20;

            // Create a new region with a rounded rectangle shape
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90);
            path.AddLine(cornerRadius, 0, this.Width - cornerRadius, 0);
            path.AddArc(this.Width - cornerRadius * 2, 0, cornerRadius * 2, cornerRadius * 2, 270, 90);
            path.AddLine(this.Width, cornerRadius, this.Width, this.Height - cornerRadius);
            path.AddArc(this.Width - cornerRadius * 2, this.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            path.AddLine(this.Width - cornerRadius, this.Height, cornerRadius, this.Height);
            path.AddArc(0, this.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            path.CloseFigure();
            this.Region = new System.Drawing.Region(path);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {



                 
            }

        private void Barcode_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Admin admin1 = new Admin();
            //admin1.Show();
            //Barcode b1 = new Barcode();
            //b1.ShowDialog();
            Product pr = new Product();
            pr.ShowDialog();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            BILL newBill = new BILL();
            newBill.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void CloseIcon_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

            if (textBox2.PasswordChar == '*')
            {
                guna2Button2.BringToFront();
                textBox2.PasswordChar = '\0';
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '\0')
            {
                guna2Button3.BringToFront();
                textBox2.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection con1 = new SqlConnection("Data Source=M_HASRA\\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True"); // making connection   
            SqlDataAdapter sda1 = new SqlDataAdapter("SELECT COUNT(*) FROM Employee WHERE E_ID='" + textBox1.Text + "' AND Password='" + textBox2.Text + "'", con1);
            // in above line the program is selecting the whole data from table and the matching it with the user name and password provided by user. 


            DataTable dt = new DataTable(); //this is creating a virtual table  
            sda1.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                SqlDataAdapter sda2 = new SqlDataAdapter("SELECT COUNT(*) FROM Employee WHERE E_ID='" + textBox1.Text + "' AND Position='Manager'", con1);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                if (dt2.Rows[0][0].ToString() == "1")  //I have made a new page called home page. If the user is successfully authenticated then the form will be moved to the next form */
                {
                    LoginGlobal.EID = textBox1.Text;
                    this.Hide();
                    Admin admin1 = new Admin();
                    admin1.Show();



                }
                else if (dt2.Rows[0][0].ToString() == "0")
                {
                    LoginGlobal.EID = textBox1.Text;
                    this.Hide();
                    Cashier c1=new Cashier();
                    c1.Show();

                }
            }

            else


                MessageBox.Show("Invalid username or password","Error",MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);




        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            BILL bill = new BILL();
            bill.Show();
        }

        private void Barcode_Click_1(object sender, EventArgs e)
        {
            Summary summary = new Summary();
            summary.Show();
        }
    }
    }

