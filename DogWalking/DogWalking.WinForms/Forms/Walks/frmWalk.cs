using DogWalking.BL.DTOs;
using DogWalking.WinForms.Infrastructure;
using DogWalking.WinForms.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DogWalking.WinForms.Forms.Walks
{
    public partial class frmWalk : BaseForm
    {
        private readonly int _walkId;
        private WalkDto _loadedWalk;
        private List<ClientDto> _clients;
        private List<DogDto> _dogs;
        private bool _isInitializing;

        private bool IsEditMode => _walkId > 0;

        public frmWalk(int walkId = 0)
        {
            InitializeComponent();
            UiStyleHelper.SetTitleStyle(lblTitle);
            _walkId = walkId;
        }

        private void frmWalk_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            lblMessage.Text = string.Empty;
            _isInitializing = true;

            ExecuteAsync(
                work: () =>
                {
                    _clients = ServiceFactory.UseClientService(service => service.GetAll());

                    if (IsEditMode)
                    {
                        _loadedWalk = ServiceFactory.UseWalkService(service =>
                        {
                            var dto = service.GetById(_walkId);
                            if (dto == null)
                                throw new Exception("Walk not found.");

                            return dto;
                        });
                    }
                },
                onCompleted: () =>
                {
                    BindClients();

                    if (IsEditMode)
                    {
                        cmbClient.SelectedValue = _loadedWalk.ClientId;
                        LoadDogsByClient(_loadedWalk.ClientId, _loadedWalk.DogId);

                        dtpWalkDate.Value = _loadedWalk.WalkDate;
                        nudDuration.Value = _loadedWalk.DurationMinutes;
                    }

                    _isInitializing = false;
                });
        }

        private void BindClients()
        {
            var list = new List<ClientDto>
            {
                new ClientDto { Id = 0, Name = "Select..." }
            };

            if (_clients != null)
                list.AddRange(_clients);

            cmbClient.DisplayMember = "Name";
            cmbClient.ValueMember = "Id";
            cmbClient.DataSource = list;
        }

        private void cmbClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing)
                return;



            if (cmbClient.SelectedValue is int clientId && clientId > 0)
            {
                LoadDogsByClient(clientId);
            }
            else
            {
                cmbDog.DataSource = null;
                
            }
        }

        private void LoadDogsByClient(int clientId, int selectedDogId = 0)
        {
            ExecuteAsync(
                work: () =>
                {
                    _dogs = ServiceFactory.UseDogService(service =>
                        service.GetAll().FindAll(d => d.ClientId == clientId));
                },
                onCompleted: () =>
                {
                    var list = new List<DogDto>
                    {
                        new DogDto { Id = 0, Name = "Select..." }
                    };

                    if (_dogs != null)
                        list.AddRange(_dogs);

                    cmbDog.DisplayMember = "Name";
                    cmbDog.ValueMember = "Id";
                    cmbDog.DataSource = list;

                    if (selectedDogId > 0)
                        cmbDog.SelectedValue = selectedDogId;

                    lblMessage.Text = string.Empty;

                    if (list.Count == 1)
                    {
                        lblMessage.ForeColor = Color.Orange;
                        lblMessage.Text = "Client without any dog registered!";
                    }
                });
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMessage.ForeColor = Color.Red;

            var dto = new WalkDto
            {
                DogId = cmbDog.SelectedValue is int id ? id : 0,
                WalkDate = dtpWalkDate.Value,
                DurationMinutes = Convert.ToInt32(nudDuration.Value)
            };

            ExecuteAsync(
                work: () =>
                {
                    ServiceFactory.UseWalkService(service =>
                    {
                        if (IsEditMode)
                            service.Update(_walkId, dto);
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
            _isInitializing = false;
            lblMessage.Text = ex.Message;
        }
    }
}
