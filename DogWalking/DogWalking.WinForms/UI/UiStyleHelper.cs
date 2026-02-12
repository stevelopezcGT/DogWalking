using System.Drawing;
using System.Windows.Forms;

namespace DogWalking.WinForms.UI
{
    public static class UiStyleHelper
    {
        public static void ApplyFormStyle(Form form)
        {
            form.Font = new Font("Segoe UI", 9F);
            form.BackColor = Color.White;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
        }

        public static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 235, 235);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.ColumnHeadersHeight = 35;

            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.Black;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 252);
            grid.DefaultCellStyle.SelectionForeColor = Color.Black;
            grid.DefaultCellStyle.BackColor = Color.White;

            grid.RowTemplate.Height = 32;
            grid.GridColor = Color.FromArgb(230, 230, 230);

            grid.RowHeadersVisible = false;
        }

        public static void SetTitleStyle(Label lblTitle)
        {
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(50, 50, 50);
            lblTitle.Location = new Point(20, 20);

        }
    }
}
