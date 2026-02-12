using DogWalking.BL.DTOs;
using DogWalking.WinForms.Infrastructure;
using DogWalking.WinForms.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DogWalking.WinForms.Forms.Dogs
{
    public partial class frmDogs : BaseForm
    {
        private List<DogDto> _loadedDogs;

        public frmDogs()
        {
            InitializeComponent();

            UiStyleHelper.StyleGrid(dgvDogs);
            SetPanelStyle();
            UiStyleHelper.SetTitleStyle(lblTitle);
        }

        private void SetPanelStyle()
        {
            pnlHeader.BackColor = Color.FromArgb(245, 246, 248);
            pnlSearch.BackColor = Color.FromArgb(245, 246, 248);
            pnlGrid.BackColor = Color.White;
            pnlFooter.BackColor = Color.FromArgb(245, 246, 248);
            separator.BackColor = Color.FromArgb(220, 220, 220);
        }

        private void frmDogs_Load(object sender, EventArgs e)
        {
            ActiveControl = txtSearch;
            LoadDogs();
        }

        private void LoadDogs()
        {
            lblMessage.Text = string.Empty;

            ExecuteAsync(
                work: () =>
                {
                    _loadedDogs = ServiceFactory.UseDogService(service =>
                        service.GetAll());
                },
                onCompleted: () =>
                {
                    dgvDogs.DataSource = _loadedDogs;
                    dgvDogs.ClearSelection();

                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                });
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var term = txtSearch.Text.Trim();
            lblMessage.Text = string.Empty;

            ExecuteAsync(
                work: () =>
                {
                    _loadedDogs = ServiceFactory.UseDogService(service =>
                        string.IsNullOrWhiteSpace(term)
                            ? service.GetAll()
                            : service.Search(term));
                },
                onCompleted: () =>
                {
                    dgvDogs.DataSource = _loadedDogs;
                    dgvDogs.ClearSelection();
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                });
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            using (var form = new frmDog())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadDogs();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var id = GetSelectedDogId();
            if (id == 0)
            {
                MessageBox.Show("Please select a dog.");
                return;
            }

            using (var form = new frmDog(id))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadDogs();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = GetSelectedDogId();
            if (id == 0)
            {
                MessageBox.Show("Please select a dog.");
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this dog?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            ExecuteAsync(
                work: () =>
                {
                    ServiceFactory.UseDogService(service =>
                        service.Delete(id));
                },
                onCompleted: () =>
                {
                    LoadDogs();
                });
        }

        private int GetSelectedDogId()
        {
            if (dgvDogs.CurrentRow == null)
                return 0;

            var dog = dgvDogs.CurrentRow.DataBoundItem as DogDto;

            if (dog == null)
                return 0;

            return dog.Id;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void SetBusyCursor(bool isBusy)
        {
            base.SetBusyCursor(isBusy);

            btnSearch.Enabled = !isBusy;
            btnNew.Enabled = !isBusy;
            btnEdit.Enabled = !isBusy && dgvDogs.CurrentRow != null;
            btnDelete.Enabled = !isBusy && dgvDogs.CurrentRow != null;
        }

        protected override void OnAsyncError(Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

        private void dgvDogs_SelectionChanged(object sender, EventArgs e)
        {
            bool hasSelection = dgvDogs.CurrentRow != null;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
        }
    }
}
