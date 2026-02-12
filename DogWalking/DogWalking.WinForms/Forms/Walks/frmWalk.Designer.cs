namespace DogWalking.WinForms.Forms.Walks
{
    partial class frmWalk
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudDuration = new System.Windows.Forms.NumericUpDown();
            this.lblDuration = new System.Windows.Forms.Label();
            this.dtpWalkDate = new System.Windows.Forms.DateTimePicker();
            this.lblWalkDate = new System.Windows.Forms.Label();
            this.cmbDog = new System.Windows.Forms.ComboBox();
            this.lblDog = new System.Windows.Forms.Label();
            this.cmbClient = new System.Windows.Forms.ComboBox();
            this.lblClient = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(619, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(17, 0, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(619, 60);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Walk Details";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 258);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(619, 80);
            this.panel2.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.Location = new System.Drawing.Point(468, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(317, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nudDuration);
            this.panel1.Controls.Add(this.lblDuration);
            this.panel1.Controls.Add(this.dtpWalkDate);
            this.panel1.Controls.Add(this.lblWalkDate);
            this.panel1.Controls.Add(this.cmbDog);
            this.panel1.Controls.Add(this.lblDog);
            this.panel1.Controls.Add(this.cmbClient);
            this.panel1.Controls.Add(this.lblClient);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(619, 198);
            this.panel1.TabIndex = 3;
            // 
            // nudDuration
            // 
            this.nudDuration.Location = new System.Drawing.Point(120, 149);
            this.nudDuration.Maximum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.nudDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDuration.Name = "nudDuration";
            this.nudDuration.Size = new System.Drawing.Size(120, 20);
            this.nudDuration.TabIndex = 11;
            this.nudDuration.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(30, 151);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(75, 13);
            this.lblDuration.TabIndex = 10;
            this.lblDuration.Text = "Duration (min):";
            // 
            // dtpWalkDate
            // 
            this.dtpWalkDate.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dtpWalkDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWalkDate.Location = new System.Drawing.Point(120, 100);
            this.dtpWalkDate.Name = "dtpWalkDate";
            this.dtpWalkDate.ShowUpDown = true;
            this.dtpWalkDate.Size = new System.Drawing.Size(200, 20);
            this.dtpWalkDate.TabIndex = 9;
            // 
            // lblWalkDate
            // 
            this.lblWalkDate.AutoSize = true;
            this.lblWalkDate.Location = new System.Drawing.Point(30, 104);
            this.lblWalkDate.Name = "lblWalkDate";
            this.lblWalkDate.Size = new System.Drawing.Size(61, 13);
            this.lblWalkDate.TabIndex = 8;
            this.lblWalkDate.Text = "Walk Date:";
            // 
            // cmbDog
            // 
            this.cmbDog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDog.FormattingEnabled = true;
            this.cmbDog.Location = new System.Drawing.Point(120, 67);
            this.cmbDog.Name = "cmbDog";
            this.cmbDog.Size = new System.Drawing.Size(430, 21);
            this.cmbDog.TabIndex = 7;
            // 
            // lblDog
            // 
            this.lblDog.AutoSize = true;
            this.lblDog.Location = new System.Drawing.Point(30, 71);
            this.lblDog.Name = "lblDog";
            this.lblDog.Size = new System.Drawing.Size(30, 13);
            this.lblDog.TabIndex = 6;
            this.lblDog.Text = "Dog:";
            // 
            // cmbClient
            // 
            this.cmbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClient.FormattingEnabled = true;
            this.cmbClient.Location = new System.Drawing.Point(120, 34);
            this.cmbClient.Name = "cmbClient";
            this.cmbClient.Size = new System.Drawing.Size(430, 21);
            this.cmbClient.TabIndex = 12;
            this.cmbClient.SelectedIndexChanged += new System.EventHandler(this.cmbClient_SelectedIndexChanged);
            // 
            // lblClient
            // 
            this.lblClient.AutoSize = true;
            this.lblClient.Location = new System.Drawing.Point(30, 38);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(36, 13);
            this.lblClient.TabIndex = 13;
            this.lblClient.Text = "Client:";
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(0, 175);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(619, 23);
            this.lblMessage.TabIndex = 5;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmWalk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 338);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "frmWalk";
            this.Text = "Walk";
            this.Load += new System.EventHandler(this.frmWalk_Load);
            this.pnlHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ComboBox cmbDog;
        private System.Windows.Forms.Label lblDog;
        private System.Windows.Forms.DateTimePicker dtpWalkDate;
        private System.Windows.Forms.Label lblWalkDate;
        private System.Windows.Forms.NumericUpDown nudDuration;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.ComboBox cmbClient;
        private System.Windows.Forms.Label lblClient;
    }
}
