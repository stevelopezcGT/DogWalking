using DogWalking.BL.DTOs;
using DogWalking.WinForms.Infrastructure;
using DogWalking.WinForms.UI;
using System;
using System.Windows.Forms;

namespace DogWalking.WinForms.Forms.Clients
{
    public partial class frmClient : BaseForm
    {
        private int _clientId;
        private ClientDto _loadedDto;

        private bool IsEditMode => _clientId > 0;

        public frmClient(int clientId = 0)
        {
            InitializeComponent();
            UiStyleHelper.SetTitleStyle(lblTitle);
            _clientId = clientId;
        }

        private void frmClient_Load(object sender, EventArgs e)
        {
            if (!IsEditMode)
                return;

            LoadClient();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

            var dto = new ClientDto
            {
                Name = txtName.Text.Trim(),
                Phone = txtPhone.Text.Trim()
            };

            ExecuteAsync(
                work: () =>
                {
                    ServiceFactory.UseClientService(service =>
                    {
                        if (IsEditMode)
                            service.Update(_clientId, dto);
                        else
                            service.Add(dto);
                    });
                },
                onCompleted: () =>
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                });
        }

        private void LoadClient()
        {
            SetBusyCursor(true);
            lblMessage.Text = string.Empty;

            ExecuteAsync(
                work: () =>
                {
                    _loadedDto = ServiceFactory.UseClientService(service =>
                    {
                        var dto = service.GetById(_clientId);

                        if (dto == null)
                            throw new Exception("Client not found.");

                        return dto;
                    });
                },
                onCompleted: () =>
                {
                    txtName.Text = _loadedDto.Name;
                    txtPhone.Text = _loadedDto.Phone;
                });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void OnAsyncError(Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
}