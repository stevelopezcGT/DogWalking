using DogWalking.WinForms.Forms.Clients;
using DogWalking.WinForms.Forms.Dogs;
using DogWalking.WinForms.Forms.Walks;
using System;
using System.Windows.Forms;

namespace DogWalking.WinForms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void clientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new frmClients())
            {
                form.ShowDialog();
            }
        }

        private void dogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new frmDogs())
            {
                form.ShowDialog();
            }
        }

        private void walksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new frmWalks())
            {
                form.ShowDialog();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
