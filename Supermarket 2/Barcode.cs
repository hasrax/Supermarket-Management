using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using ZXing;
using Bill;

namespace Supermarket_2
{
    public partial class Barcode : Form
    {
        public Barcode()
        {
            InitializeComponent();
        }
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Barcode_Load(object sender, EventArgs e)
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


            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filterInfoCollection)
                cboCamera.Items.Add(device.Name);
            cboCamera.SelectedIndex = 0;
        }

        private VideoCaptureDevice GetVideoCaptureDevice()
        {
            return videoCaptureDevice;
        }

        private void btnScan_Click(object sender, EventArgs e, VideoCaptureDevice videoCaptureDevice)
        {
           
        }

        private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            try
            {
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
                BarcodeReader reader = new BarcodeReader();
                var result = reader.Decode(bitmap);
                if (result != null)
                {
                    txtBarcode.Invoke(new MethodInvoker(delegate ()
                    {
                        txtBarcode.Text = result.ToString();
                    }));
                }
                pictureBox.Image = bitmap;
            }catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Barcode_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)
                    videoCaptureDevice.Stop();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            //this.Close();
            if (txtBarcode.Text == null )
            {
                MessageBox.Show("Wrong Barcode ! Please Check Again");

            }
            else
            {
                LoginGlobal.Barcode = txtBarcode.Text;
                this.Close();
                
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }
        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void CloseIcon_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
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
            BILL bill2= new BILL();
            bill2.Show();
        }
    }
}
