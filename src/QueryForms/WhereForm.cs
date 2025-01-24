using SqlAmoz.DB;
using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace SqlAmoz.QueryForms
{
    public partial class WhereForm : Form
    {
        Color bkTest;
        public WhereForm()
        {
            InitializeComponent();

            var Original = txtSelect.Font;

            bkTest = txtSelect.BackColor;
            txtSelect.Font = new Font(Original.FontFamily, Original.SizeInPoints * 2, Original.Style);
            txtWhere.Font = txtSelect.Font;
        }

        private void WhereForm_Load(object sender, EventArgs e)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Persons", Database.Connection))
            {
                DataTable dataTable = new DataTable();
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                da.Fill(dataTable);
                dgvMain.DataSource = dataTable;
            }
        }

        private void btnApply_Click(object sender, System.EventArgs e)
        {
            dgvResult.DataSource = null;
            try
            {
                string wh = string.Empty;
                if (!string.IsNullOrWhiteSpace(txtWhere.Text))
                    wh = "WHERE " + txtWhere.Text;
                using (SQLiteCommand cmd = new SQLiteCommand($"SELECT {txtSelect.Text} FROM Persons {wh}", Database.Connection))
                {
                    DataTable dataTable = new DataTable();
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    da.Fill(dataTable);
                    dgvResult.DataSource = dataTable;
                }
            }
            catch
            {
                txtSelect.BackColor = Color.Pink;
                txtWhere.BackColor = Color.Pink;
            }
        }

        private void txtSelect_TextChanged(object sender, System.EventArgs e)
        {
            txtSelect.BackColor = bkTest;
            txtWhere.BackColor = bkTest;
        }

        private void txtWhere_TextChanged(object sender, EventArgs e)
        {
            txtSelect.BackColor = bkTest;
            txtWhere.BackColor = bkTest;
        }
    }
}
