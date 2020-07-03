namespace LockerRentalManagementSystem.View
{
    partial class MasterKeyForm
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelViewMasterKey = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.groupBoxQRCode = new System.Windows.Forms.GroupBox();
            this.pictureBoxMasterKeyQr = new System.Windows.Forms.PictureBox();
            this.labelMasterKeyFor = new System.Windows.Forms.Label();
            this.labelMasterKeyEmployee = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.groupBoxQRCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMasterKeyQr)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelViewMasterKey);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(384, 41);
            this.panelTop.TabIndex = 0;
            // 
            // labelViewMasterKey
            // 
            this.labelViewMasterKey.AutoSize = true;
            this.labelViewMasterKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelViewMasterKey.Location = new System.Drawing.Point(3, 9);
            this.labelViewMasterKey.Name = "labelViewMasterKey";
            this.labelViewMasterKey.Size = new System.Drawing.Size(141, 20);
            this.labelViewMasterKey.TabIndex = 0;
            this.labelViewMasterKey.Text = "View Master Key";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.buttonClose);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 359);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(384, 52);
            this.panelBottom.TabIndex = 1;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(297, 6);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 40);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.labelMasterKeyEmployee);
            this.panelCenter.Controls.Add(this.labelMasterKeyFor);
            this.panelCenter.Controls.Add(this.groupBoxQRCode);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 41);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(384, 318);
            this.panelCenter.TabIndex = 2;
            // 
            // groupBoxQRCode
            // 
            this.groupBoxQRCode.Controls.Add(this.pictureBoxMasterKeyQr);
            this.groupBoxQRCode.Location = new System.Drawing.Point(12, 57);
            this.groupBoxQRCode.Name = "groupBoxQRCode";
            this.groupBoxQRCode.Size = new System.Drawing.Size(360, 255);
            this.groupBoxQRCode.TabIndex = 0;
            this.groupBoxQRCode.TabStop = false;
            this.groupBoxQRCode.Text = "QR Code (Master Key)";
            // 
            // pictureBoxMasterKeyQr
            // 
            this.pictureBoxMasterKeyQr.Location = new System.Drawing.Point(81, 28);
            this.pictureBoxMasterKeyQr.Name = "pictureBoxMasterKeyQr";
            this.pictureBoxMasterKeyQr.Size = new System.Drawing.Size(210, 210);
            this.pictureBoxMasterKeyQr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxMasterKeyQr.TabIndex = 0;
            this.pictureBoxMasterKeyQr.TabStop = false;
            // 
            // labelMasterKeyFor
            // 
            this.labelMasterKeyFor.AutoSize = true;
            this.labelMasterKeyFor.Location = new System.Drawing.Point(4, 3);
            this.labelMasterKeyFor.Name = "labelMasterKeyFor";
            this.labelMasterKeyFor.Size = new System.Drawing.Size(130, 13);
            this.labelMasterKeyFor.TabIndex = 1;
            this.labelMasterKeyFor.Text = "This is the master key for: ";
            // 
            // labelMasterKeyEmployee
            // 
            this.labelMasterKeyEmployee.AutoSize = true;
            this.labelMasterKeyEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMasterKeyEmployee.Location = new System.Drawing.Point(4, 25);
            this.labelMasterKeyEmployee.Name = "labelMasterKeyEmployee";
            this.labelMasterKeyEmployee.Size = new System.Drawing.Size(45, 16);
            this.labelMasterKeyEmployee.TabIndex = 2;
            this.labelMasterKeyEmployee.Text = "label1";
            // 
            // MasterKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 411);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MasterKeyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Master Key Form";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelCenter.ResumeLayout(false);
            this.panelCenter.PerformLayout();
            this.groupBoxQRCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMasterKeyQr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelViewMasterKey;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Label labelMasterKeyEmployee;
        private System.Windows.Forms.Label labelMasterKeyFor;
        private System.Windows.Forms.GroupBox groupBoxQRCode;
        private System.Windows.Forms.PictureBox pictureBoxMasterKeyQr;
    }
}