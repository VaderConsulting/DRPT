using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ISA.Helper;

namespace DRPlanningTool
{
    static class Program
    {
        public static string AppSettingsPath = "";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Properties.Settings.Default.General_MDIInterface)
            {
                Application.Run(new frmMDIMain());
            }
            else
            {
                Application.Run(new frmMain());
            }
        }

        public static void DebugPrint(string Message, string Category, bool Always)
        {
            if (Properties.Settings.Default.ShowDebugInformation || Always)
            {
                System.Diagnostics.Debug.WriteLine(Message, Category);
            }
        }

        public static void DebugPrint(string Message, string Category)
        {
            if (Properties.Settings.Default.ShowDebugInformation)
            {
                System.Diagnostics.Debug.WriteLine(Message, Category);
            }
        }

        public static void DebugPrint(string Message)
        {
            if (Properties.Settings.Default.ShowDebugInformation)
            {
                System.Diagnostics.Debug.WriteLine(Message);
            }
        }
    }
}
