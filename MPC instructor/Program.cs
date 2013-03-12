using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MPC_instructor
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static string MPC_target;
        public static string YT_link;
        public static Form preview_form;
        public static Form browser_form;
        //public static Form YT_form;
        [STAThread]


        public static void Main()
        {
            MPC_target = "localhost:13579";
            YT_link = "";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new controller());
            //Application.Run(new YT());
        }
    }
}
