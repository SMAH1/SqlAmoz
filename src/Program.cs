using SqlAmoz.DB;
using SqlAmoz.QueryForms;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace SqlAmoz
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string starter = "select";
            if (args.Length > 0)
                starter = args[0];

            starter = starter.ToLower().Trim("-/".ToCharArray());
            Form frm = null;
            switch (starter)
            {
                case "select": frm = new SelectForm(); break;
                case "where": frm = new WhereForm(); break;
                case "order": frm = new OrderForm(); break;
                //case "from": frm = new FromForm(); break;
                //case "query": frm = new QueryForm(); break;
            }

            if (frm != null)
            {
                Database.InitDatabase();
                Application.Run(frm);
            }
            else
                MessageBox.Show("Invalid command line!");
        }
    }
}
