using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace Supermarket_2
{
    public partial class Admin : Form
    {
        Myconnection obj = new Myconnection();

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
         
        public Admin()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            PnlNav.Height = BtnEmployee.Height;
            PnlNav.Top = BtnEmployee.Top;
            PnlNav.Left = BtnEmployee.Left;
            BtnEmployee.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Product product1 = new Product();
            product1.Show();
            this.Close();   
        }

        private void Form2_Load(object sender, EventArgs e)
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

            obj.con.Open();
            SqlCommand command = new SqlCommand("Select E_Name,Position from Employee Where E_ID='" + LoginGlobal.EID + "'", obj.con);

            SqlDataReader reader1 = command.ExecuteReader();

            if (reader1.Read())
            {
                lb_name.Text = Convert.ToString(reader1["E_Name"]);           
                lb_Position.Text = Convert.ToString(reader1["Position"]);
            }


            Count c1 = new Count();
            Count c2 = new Count();
            Count c3= new Count();
            Count c4= new Count();

            lblEmpCount.Text = Convert.ToString(c1.GetCount("select Count (E_ID) from Employee"));
            lblProdCount.Text = Convert.ToString(c2.GetCount("select Count (BarCode)from Product"));
            lblProfit.Text = "Rs"+Convert.ToString(c3.GetCount("Select sum(total_profit) from Profit_Analysis"));
            lblEarning.Text = "Rs"+Convert.ToString(c4.GetCount("Select SUm(Total_Price)from Billing_summary"));

            obj.con.Close();


        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            Employee emploee1= new Employee();
            this.Close();
            emploee1.Show();
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            var res=MessageBox.Show("Are you sure you want to logout?","Info",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if(res==DialogResult.Yes)
            {
                this.Close();
                Login login1=new Login();
                login1.Show();
            }

            
        }

        private void BtnDashboard_Leave(object sender, EventArgs e)
        {
            BtnEmployee.BackColor = Color.FromArgb(24, 30, 54);
        }

        

        private void BtnProduct_Leave(object sender, EventArgs e)
        {
            BtnProduct.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void BtnLogout_Leave(object sender, EventArgs e)
        {
            BtnLogout.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CloseIcon_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lb_name_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Show();
        }
    }
}
