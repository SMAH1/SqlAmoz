using SqlAmoz.DB;
using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SqlAmoz.QueryForms
{
    public partial class QueryForm : Form
    {
        Color bkTest;
        public QueryForm()
        {
            InitializeComponent();

            var Original = txtSelect.Font;

            bkTest = txtSelect.BackColor;
            txtSelect.Font = new Font(Original.FontFamily, Original.SizeInPoints * 2, Original.Style);
            txtFrom.Font = txtSelect.Font;
            txtWhere.Font = txtSelect.Font;
            txtOrder.Font = txtSelect.Font;
            txtSql.Font = new Font(Original.FontFamily, Original.SizeInPoints * 1.5F, Original.Style);

            txtSelect.TextChanged += TxtTextChanged;
            txtFrom.TextChanged += TxtTextChanged;
            txtWhere.TextChanged += TxtTextChanged;
            txtOrder.TextChanged += TxtTextChanged;
        }

        private void TxtTextChanged(object sender, EventArgs e)
        {
            txtSql.BackColor = bkTest;

            string from = string.Empty;
            if (!string.IsNullOrWhiteSpace(txtFrom.Text))
                from = txtFrom.Text;

            string where = string.Empty;
            if (!string.IsNullOrWhiteSpace(txtWhere.Text))
                where = "WHERE " + txtWhere.Text;

            string order = string.Empty;
            if (!string.IsNullOrWhiteSpace(txtOrder.Text))
                order = "ORDER BY " + txtOrder.Text;

            StringBuilder sb = new StringBuilder(1000);
            sb.AppendLine($"SELECT {txtSelect.Text}");
            sb.AppendLine($"FROM {from}");
            if (!string.IsNullOrEmpty(where)) sb.AppendLine(where);
            if (!string.IsNullOrEmpty(order)) sb.AppendLine(order);

            txtSql.Text = sb.ToString();
        }

        private void txtSql_TextChanged(object sender, EventArgs e)
        {
            txtSql.BackColor = bkTest;
        }

        private void QueryForm_Load(object sender, EventArgs e)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Persons", Database.Connection))
            {
                DataTable dataTable = new DataTable();
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                da.Fill(dataTable);
                dgvPersons.DataSource = dataTable;
            }

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Courses", Database.Connection))
            {
                DataTable dataTable = new DataTable();
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                da.Fill(dataTable);
                dgvCourses.DataSource = dataTable;
            }

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Programmings", Database.Connection))
            {
                DataTable dataTable = new DataTable();
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                da.Fill(dataTable);
                dgvProgrammings.DataSource = dataTable;
            }
        }

        private void btnApply_Click(object sender, System.EventArgs e)
        {
            dgvResult.DataSource = null;
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(txtSql.Text, Database.Connection))
                {
                    DataTable dataTable = new DataTable();
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    da.Fill(dataTable);
                    dgvResult.DataSource = dataTable;
                }
            }
            catch
            {
                txtSql.BackColor = Color.Pink;
            }
        }
    }
}
