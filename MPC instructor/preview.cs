using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MPC_instructor
{
    public partial class preview : Form
    {
        public preview()
        {
            InitializeComponent();
        }

        

        private void preview_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "http://" + Program.MPC_target + "/snapshot.jpg";

            
            
            if ((pictureBox1.Width < 800) && (pictureBox1.Height < 800))
            {
                this.Width = pictureBox1.Width;
                this.Height = pictureBox1.Height;
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox1.Dock = DockStyle.None;
            
            }
            else
            {
                this.Width = 800;
                this.Height = (Int16)((pictureBox1.Height * 8000) / (pictureBox1.Width * 10)); //800 * ratio
                //this.Height = 600; //800 * ratio
                
                
                pictureBox1.Dock = DockStyle.Fill;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            /*
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Width=pictureBox1.Width = 800;
                this.Height=pictureBox1.Height = ((pictureBox1.Height * 80000) / (pictureBox1.Width * 100)); //800 * ratio
                
             */
                Program.preview_form.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Program.preview_form.Width, Screen.PrimaryScreen.WorkingArea.Height - Program.preview_form.Height);    
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }

}
