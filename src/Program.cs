using SqlAmoz.DB;
using System;
using System.Windows.Forms;

namespace SqlAmoz
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Database.InitDatabase();
            Application.Run(new SelectForm());
        }
    }
}
