using DogWalking.BL.DTOs;
using DogWalking.WinForms.Infrastructure;
using DogWalking.WinForms.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DogWalking.WinForms.Forms.Dogs
{
    public partial class frmDog : BaseForm
    {
        private readonly int _dogId;
        private DogDto _loadedDto;
        private List<ClientDto> _clients;

        private bool IsEditMode => _dogId > 0;

        public frmDog(int dogId = 0)
        {
            InitializeComponent();
            UiStyleHelper.SetTitleStyle(lblTitle);
            _dogId = dogId;
        }

        private void frmDog_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            lblMessage.Text = string.Empty;

            ExecuteAsync(
                work: () =>
                {
                    _clients = ServiceFactory.UseClientService(service => service.GetAll());

                    if (!IsEditMode)
                        return;

                    _loadedDto = ServiceFactory.UseDogService(service =>
                    {
                        var dto = service.GetById(_dogId);

                        if (dto == null)
                            throw new Exception("Dog not found.");

                        return dto;
                    });
                },
                onCompleted: () =>
                {
                    _clients.Insert(0, new ClientDto
                    {
                        Id = 0,
                        Name = "Select...",
                        Phone = string.Empty
                    });

                    cmbClient.DisplayMember = "Name";
                    cmbClient.ValueMember = "Id";
                    cmbClient.DataSource = _clients;

                    if (!IsEditMode)
                        return;

                    cmbClient.SelectedValue = _loadedDto.ClientId;
                    txtName.Text = _loadedDto.Name;
                    txtBreed.Text = _loadedDto.Breed;
                    txtAge.Text = _loadedDto.Age.ToString();
                });
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

            var dto = new DogDto
            {
                ClientId = (int)cmbClient.SelectedValue,
                Name = txtName.Text.Trim(),
                Breed = txtBreed.Text.Trim(),
                Age = int.TryParse(txtAge.Text.Trim(), out var age) ? age : 0
            };

            ExecuteAsync(
                work: () =>
                {
                    ServiceFactory.UseDogService(service =>
                    {
                        if (IsEditMode)
                            service.Update(_dogId, dto);
                        else
                            service.Add(dto);
                    });
                },
                onCompleted: () =>
                {
                    DialogResult = DialogResult.OK;
                    Close();
                });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        protected override void SetBusyCursor(bool isBusy)
        {
            btnSave.Enabled = !isBusy;
            btnCancel.Enabled = !isBusy;
            Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
        }

        protected override void OnAsyncError(Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
}
