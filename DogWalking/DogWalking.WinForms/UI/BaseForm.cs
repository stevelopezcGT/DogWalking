using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DogWalking.WinForms.UI
{
    public class BaseForm : Form
    {
        private BackgroundWorker _worker;
        private Action _workAction;
        private Action _completionAction;
        private Exception _capturedException;

        protected BaseForm()
        {
            ApplyBaseStyle();
            EnableDoubleBuffering();
            if (!IsInDesignMode())
            {
                InitializeBackgroundWorker();
            }
        }

        private static bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        }

        private void ApplyBaseStyle()
        {
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(245, 246, 248);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.AutoScaleMode = AutoScaleMode.None;
        }

        private void EnableDoubleBuffering()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);

            this.UpdateStyles();
        }

        private void InitializeBackgroundWorker()
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        /// <summary>
        /// Executes background work using BackgroundWorker and invokes completion on the UI thread.
        /// </summary>
        /// <param name="work">Work to execute on a background thread.</param>
        /// <param name="onCompleted">Optional callback executed on the UI thread after successful completion.</param>
        protected void ExecuteAsync(Action work, Action onCompleted = null)
        {
            if (_worker.IsBusy)
                return;

            _workAction = work;
            _completionAction = onCompleted;
            _capturedException = null;

            SetBusyCursor(true);
            _worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _workAction?.Invoke();
            }
            catch (Exception ex)
            {
                _capturedException = ex;
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetBusyCursor(false);

            if (_capturedException != null)
            {
                OnAsyncError(_capturedException);
                return;
            }

            _completionAction?.Invoke();
        }

        protected virtual void SetBusyCursor(bool isBusy)
        {
            Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
        }

        protected virtual void OnAsyncError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

    }
}