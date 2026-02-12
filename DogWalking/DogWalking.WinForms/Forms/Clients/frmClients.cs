using DogWalking.BL.DTOs;
using DogWalking.DL.Context;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories;
using DogWalking.WinForms.Infrastructure;
using DogWalking.WinForms.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DogWalking.WinForms.Forms.Clients
{
    public partial class frmClients : BaseForm
    {
        private List<ClientDto> _loadedClients;

        public frmClients()
        {
            InitializeComponent();

            UiStyleHelper.StyleGrid(dgvClients);
            SetPanellStyle();
            UiStyleHelper.SetTitleStyle(lblTitle);
        }        

        private void SetPanellStyle()
        {
            pnlHeader.BackColor = Color.FromArgb(245, 246, 248);
            pnlSearch.BackColor = Color.FromArgb(245, 246, 248);
            pnlGrid.BackColor = Color.White;
            pnlFooter.BackColor = Color.FromArgb(245, 246, 248);
            separator.BackColor = Color.FromArgb(220, 220, 220);
        }

        private void frmClients_Load(object sender, EventArgs e)
        {
            ActiveControl = txtSearch;
            LoadClients();
        }

        private void LoadClients()
        {
            lblMessage.Text = string.Empty;

            ExecuteAsync(
                work: () =>
                {
                    _loadedClients = ServiceFactory.UseClientService(service =>
                        service.GetAll());
                },
                onCompleted: () =>
                {
                    dgvClients.DataSource = _loadedClients;
                    dgvClients.ClearSelection();

                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                });
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            using (var form = new frmClient())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadClients();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var id = GetSelectedClientId();
            if (id == 0)
            {
                MessageBox.Show("Please select a client.");
                return;
            }

            using (var form = new frmClient(id))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadClients();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = GetSelectedClientId();
            if (id == 0)
            {
                MessageBox.Show("Please select a client.");
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this client?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            ExecuteAsync(
                work: () =>
                {
                    ServiceFactory.UseClientService(service =>
                        service.Delete(id));
                },
                onCompleted: () =>
                {
                    LoadClients();
                });
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var term = txtSearch.Text.Trim();
            lblMessage.Text = string.Empty;

            ExecuteAsync(
                work: () =>
                {
                    _loadedClients = ServiceFactory.UseClientService(service =>
                        string.IsNullOrWhiteSpace(term)
                            ? service.GetAll()
                            : service.Search(term));
                },
                onCompleted: () =>
                {
                    dgvClients.DataSource = _loadedClients;
                    dgvClients.ClearSelection();
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                });
        }

        private int GetSelectedClientId()
        {
            if (dgvClients.CurrentRow == null)
                return 0;

            var client = dgvClients.CurrentRow.DataBoundItem as ClientDto;

            if (client == null)
                return 0;

            return client.Id;
        }

        protected override void SetBusyCursor(bool isBusy)
        {
            base.SetBusyCursor(isBusy);

            btnSearch.Enabled = !isBusy;
            btnNew.Enabled = !isBusy;
            btnEdit.Enabled = !isBusy && dgvClients.CurrentRow != null;
            btnDelete.Enabled = !isBusy && dgvClients.CurrentRow != null;
        }

        protected override void OnAsyncError(Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

        private void dgvClients_SelectionChanged(object sender, EventArgs e)
        {
            bool hasSelection = dgvClients.CurrentRow != null;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
        }
    }
}