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
    public partial class browser : Form
    {
        public browser()
        {
            InitializeComponent();
        }

        private void browser_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://" + Program.MPC_target + "/browser.html");
        }

    }
}
