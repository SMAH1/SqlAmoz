using SqlAmoz.DB;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace SqlAmoz.QueryForms
{
    public partial class SelectForm : Form
    {
        Color bkTest;
        public SelectForm()
        {
            InitializeComponent();

            var Original = txtSelect.Font;

            bkTest = txtSelect.BackColor;
            txtSelect.Font = new Font(Original.FontFamily, Original.SizeInPoints * 2, Original.Style);
        }

        private void SelectForm_Load(object sender, System.EventArgs e)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Persons", Database.Connection))
            {
                DataTable dataTable = new DataTable();
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                da.Fill(dataTable);
                dgvMain.DataSource = dataTable;
                dgvResult.DataSource = null;
            }
        }

        private void btnApply_Click(object sender, System.EventArgs e)
        {
            dgvResult.DataSource = null;
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand($"SELECT {txtSelect.Text} FROM Persons", Database.Connection))
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
            }
        }

        private void txtSelect_TextChanged(object sender, System.EventArgs e)
        {
            txtSelect.BackColor = bkTest;
        }
    }
}
