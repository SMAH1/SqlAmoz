using SqlAmoz.DB;
using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace SqlAmoz.QueryForms
{
    public partial class OrderForm : Form
    {
        Color bkTest;
        public OrderForm()
        {
            InitializeComponent();

            var Original = txtSelect.Font;

            bkTest = txtSelect.BackColor;
            txtSelect.Font = new Font(Original.FontFamily, Original.SizeInPoints * 2, Original.Style);
            txtWhere.Font = txtSelect.Font;
            txtOrder.Font = txtSelect.Font;

            txtSelect.TextChanged += TxtTextChanged;
            txtWhere.TextChanged += TxtTextChanged;
            txtOrder.TextChanged += TxtTextChanged;
        }

        private void TxtTextChanged(object sender, EventArgs e)
        {
            txtSelect.BackColor = bkTest;
            txtWhere.BackColor = bkTest;
            txtOrder.BackColor = bkTest;
        }

        private void OrderForm_Load(object sender, EventArgs e)
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
                string where = string.Empty;
                if (!string.IsNullOrWhiteSpace(txtWhere.Text))
                    where = "WHERE " + txtWhere.Text;

                string order = string.Empty;
                if (!string.IsNullOrWhiteSpace(txtOrder.Text))
                    order = "ORDER BY " + txtOrder.Text;

                using (SQLiteCommand cmd = new SQLiteCommand($"SELECT {txtSelect.Text} FROM Persons {where} {order}", Database.Connection))
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
                txtOrder.BackColor = Color.Pink;
            }
        }
    }
}
