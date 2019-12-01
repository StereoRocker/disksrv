using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace disksrv_client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Initialize application settings
            AppSettings.Initialize();

            // This launches frmMain
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
