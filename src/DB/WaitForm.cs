using System.Drawing;
using System.Windows.Forms;

namespace SqlAmoz.DB
{
    public partial class WaitForm : Form
    {
        public WaitForm()
        {
            InitializeComponent();

            var Original = this.Font;
            lbl.Font = new Font(Original.FontFamily, Original.SizeInPoints * 2, Original.Style);
        }
    }
}
