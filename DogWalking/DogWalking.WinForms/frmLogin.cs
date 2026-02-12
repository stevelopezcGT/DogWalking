using DogWalking.BL.DTOs;
using DogWalking.Common;
using DogWalking.WinForms.Infrastructure;
using DogWalking.WinForms.UI;
using System;
using System.Windows.Forms;

namespace DogWalking.WinForms
{
    public partial class frmLogin : BaseForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

            var loginDto = new LoginDto { Username = txtUsername.Text.Trim(), Password = txtPassword.Text.Trim() };
            AppSession.CurrentUsername = null;

            ExecuteAsync(
                work: () =>
                {
                    var success = ServiceFactory.UseAuthService(service =>
                        service.Login(loginDto));

                    if (!success)
                        throw new InvalidOperationException("Invalid username or password.");
                },
                onCompleted: () =>
                {
                    AppSession.CurrentUsername = txtUsername.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                });
        }

        protected override void SetBusyCursor(bool isBusy)
        {
            btnLogin.Enabled = !isBusy;
            btnExit.Enabled = !isBusy;
            Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        protected override void OnAsyncError(Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}