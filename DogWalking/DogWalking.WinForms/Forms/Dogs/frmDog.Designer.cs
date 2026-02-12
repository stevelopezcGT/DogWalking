namespace DogWalking.WinForms.Forms.Dogs
{
    partial class frmDog
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
            this.txtAge = new System.Windows.Forms.TextBox();
            this.lblAge = new System.Windows.Forms.Label();
            this.txtBreed = new System.Windows.Forms.TextBox();
            this.lblBreed = new System.Windows.Forms.Label();
            this.cmbClient = new System.Windows.Forms.ComboBox();
            this.lblClientId = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.lblTitle.Text = "Dog Details";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 308);
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
            this.panel1.Controls.Add(this.txtAge);
            this.panel1.Controls.Add(this.lblAge);
            this.panel1.Controls.Add(this.txtBreed);
            this.panel1.Controls.Add(this.lblBreed);
            this.panel1.Controls.Add(this.cmbClient);
            this.panel1.Controls.Add(this.lblClientId);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(619, 248);
            this.panel1.TabIndex = 3;
            // 
            // txtAge
            // 
            this.txtAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAge.Location = new System.Drawing.Point(95, 184);
            this.txtAge.Multiline = true;
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(104, 32);
            this.txtAge.TabIndex = 12;
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(30, 188);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(29, 13);
            this.lblAge.TabIndex = 11;
            this.lblAge.Text = "Age:";
            // 
            // txtBreed
            // 
            this.txtBreed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBreed.Location = new System.Drawing.Point(95, 134);
            this.txtBreed.Multiline = true;
            this.txtBreed.Name = "txtBreed";
            this.txtBreed.Size = new System.Drawing.Size(455, 32);
            this.txtBreed.TabIndex = 10;
            // 
            // lblBreed
            // 
            this.lblBreed.AutoSize = true;
            this.lblBreed.Location = new System.Drawing.Point(30, 138);
            this.lblBreed.Name = "lblBreed";
            this.lblBreed.Size = new System.Drawing.Size(38, 13);
            this.lblBreed.TabIndex = 9;
            this.lblBreed.Text = "Breed:";
            // 
            // cmbClient
            // 
            this.cmbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClient.FormattingEnabled = true;
            this.cmbClient.Location = new System.Drawing.Point(95, 84);
            this.cmbClient.Name = "cmbClient";
            this.cmbClient.Size = new System.Drawing.Size(455, 21);
            this.cmbClient.TabIndex = 8;
            // 
            // lblClientId
            // 
            this.lblClientId.AutoSize = true;
            this.lblClientId.Location = new System.Drawing.Point(30, 88);
            this.lblClientId.Name = "lblClientId";
            this.lblClientId.Size = new System.Drawing.Size(36, 13);
            this.lblClientId.TabIndex = 7;
            this.lblClientId.Text = "Client:";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(95, 34);
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(455, 32);
            this.txtName.TabIndex = 6;
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(0, 225);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(619, 23);
            this.lblMessage.TabIndex = 5;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(30, 38);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // frmDog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 388);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmDog";
            this.Text = "Dog";
            this.Load += new System.EventHandler(this.frmDog_Load);
            this.pnlHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.TextBox txtBreed;
        private System.Windows.Forms.Label lblBreed;
        private System.Windows.Forms.ComboBox cmbClient;
        private System.Windows.Forms.Label lblClientId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblName;
    }
}
