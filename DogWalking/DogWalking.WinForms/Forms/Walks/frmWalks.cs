using DogWalking.BL.DTOs;
using DogWalking.WinForms.Infrastructure;
using DogWalking.WinForms.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DogWalking.WinForms.Forms.Walks
{
    public partial class frmWalks : BaseForm
    {
        private List<WalkDto> _loadedWalks;

        public frmWalks()
        {
            InitializeComponent();

            UiStyleHelper.StyleGrid(dgvWalks);
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

        private void frmWalks_Load(object sender, EventArgs e)
        {
            ActiveControl = txtSearch;
            LoadWalks();
        }

        private void LoadWalks()
        {
            lblMessage.Text = string.Empty;

            ExecuteAsync(
                work: () =>
                {
                    _loadedWalks = ServiceFactory.UseWalkService(service => service.GetAll());
                },
                onCompleted: () =>
                {
                    dgvWalks.DataSource = _loadedWalks;
                    dgvWalks.ClearSelection();
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
                    _loadedWalks = ServiceFactory.UseWalkService(service =>
                        string.IsNullOrWhiteSpace(term)
                            ? service.GetAll()
                            : service.Search(term));
                },
                onCompleted: () =>
                {
                    dgvWalks.DataSource = _loadedWalks;
                    dgvWalks.ClearSelection();
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                });
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            using (var form = new frmWalk())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadWalks();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var id = GetSelectedWalkId();
            if (id == 0)
            {
                MessageBox.Show("Please select a walk.");
                return;
            }

            using (var form = new frmWalk(id))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadWalks();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = GetSelectedWalkId();
            if (id == 0)
            {
                MessageBox.Show("Please select a walk.");
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this walk?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            ExecuteAsync(
                work: () =>
                {
                    ServiceFactory.UseWalkService(service => service.Delete(id));
                },
                onCompleted: LoadWalks);
        }

        private int GetSelectedWalkId()
        {
            if (dgvWalks.CurrentRow == null)
                return 0;

            var walk = dgvWalks.CurrentRow.DataBoundItem as WalkDto;
            if (walk == null)
                return 0;

            return walk.Id;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvWalks_SelectionChanged(object sender, EventArgs e)
        {
            var hasSelection = dgvWalks.CurrentRow != null;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
        }

        protected override void SetBusyCursor(bool isBusy)
        {
            base.SetBusyCursor(isBusy);

            btnSearch.Enabled = !isBusy;
            btnNew.Enabled = !isBusy;
            btnEdit.Enabled = !isBusy && dgvWalks.CurrentRow != null;
            btnDelete.Enabled = !isBusy && dgvWalks.CurrentRow != null;
        }

        protected override void OnAsyncError(Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
}
