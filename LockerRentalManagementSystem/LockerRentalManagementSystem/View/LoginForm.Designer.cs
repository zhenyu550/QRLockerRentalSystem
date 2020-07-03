namespace LockerRentalManagementSystem.View
{
    partial class LoginForm
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
            this.labelDBSettingsTitle = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelDbName = new System.Windows.Forms.Label();
            this.labelDbPassword = new System.Windows.Forms.Label();
            this.labelUID = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelServer = new System.Windows.Forms.Label();
            this.textBoxDbName = new System.Windows.Forms.TextBox();
            this.textBoxDbPassword = new System.Windows.Forms.TextBox();
            this.textBoxUID = new System.Windows.Forms.TextBox();
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.labelDBSettings = new System.Windows.Forms.Label();
            this.buttonExtendDBSettings = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.labelUserPassword = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.panelDBSettings = new System.Windows.Forms.Panel();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.textBoxUserPassword = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.panelDBSettings.SuspendLayout();
            this.panelLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelDBSettingsTitle
            // 
            this.labelDBSettingsTitle.AutoSize = true;
            this.labelDBSettingsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDBSettingsTitle.Location = new System.Drawing.Point(4, 4);
            this.labelDBSettingsTitle.Name = "labelDBSettingsTitle";
            this.labelDBSettingsTitle.Size = new System.Drawing.Size(141, 17);
            this.labelDBSettingsTitle.TabIndex = 11;
            this.labelDBSettingsTitle.Text = "Database Settings";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(193, 161);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 31);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // labelDbName
            // 
            this.labelDbName.AutoSize = true;
            this.labelDbName.Location = new System.Drawing.Point(29, 137);
            this.labelDbName.Name = "labelDbName";
            this.labelDbName.Size = new System.Drawing.Size(77, 17);
            this.labelDbName.TabIndex = 9;
            this.labelDbName.Text = "Database: ";
            // 
            // labelDbPassword
            // 
            this.labelDbPassword.AutoSize = true;
            this.labelDbPassword.Location = new System.Drawing.Point(29, 110);
            this.labelDbPassword.Name = "labelDbPassword";
            this.labelDbPassword.Size = new System.Drawing.Size(77, 17);
            this.labelDbPassword.TabIndex = 8;
            this.labelDbPassword.Text = "Password: ";
            // 
            // labelUID
            // 
            this.labelUID.AutoSize = true;
            this.labelUID.Location = new System.Drawing.Point(29, 83);
            this.labelUID.Name = "labelUID";
            this.labelUID.Size = new System.Drawing.Size(39, 17);
            this.labelUID.TabIndex = 7;
            this.labelUID.Text = "UID: ";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(29, 55);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(42, 17);
            this.labelPort.TabIndex = 6;
            this.labelPort.Text = "Port: ";
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(29, 29);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(58, 17);
            this.labelServer.TabIndex = 5;
            this.labelServer.Text = "Server: ";
            // 
            // textBoxDbName
            // 
            this.textBoxDbName.Location = new System.Drawing.Point(116, 134);
            this.textBoxDbName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxDbName.Name = "textBoxDbName";
            this.textBoxDbName.Size = new System.Drawing.Size(151, 22);
            this.textBoxDbName.TabIndex = 4;
            // 
            // textBoxDbPassword
            // 
            this.textBoxDbPassword.Location = new System.Drawing.Point(116, 107);
            this.textBoxDbPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxDbPassword.Name = "textBoxDbPassword";
            this.textBoxDbPassword.PasswordChar = '*';
            this.textBoxDbPassword.Size = new System.Drawing.Size(151, 22);
            this.textBoxDbPassword.TabIndex = 3;
            this.textBoxDbPassword.UseSystemPasswordChar = true;
            // 
            // textBoxUID
            // 
            this.textBoxUID.Location = new System.Drawing.Point(116, 80);
            this.textBoxUID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxUID.Name = "textBoxUID";
            this.textBoxUID.Size = new System.Drawing.Size(151, 22);
            this.textBoxUID.TabIndex = 2;
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Location = new System.Drawing.Point(116, 53);
            this.numericUpDownPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDownPort.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numericUpDownPort.Name = "numericUpDownPort";
            this.numericUpDownPort.Size = new System.Drawing.Size(151, 22);
            this.numericUpDownPort.TabIndex = 1;
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(116, 26);
            this.textBoxServer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(151, 22);
            this.textBoxServer.TabIndex = 0;
            // 
            // labelDBSettings
            // 
            this.labelDBSettings.AutoSize = true;
            this.labelDBSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDBSettings.Location = new System.Drawing.Point(29, 92);
            this.labelDBSettings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDBSettings.Name = "labelDBSettings";
            this.labelDBSettings.Size = new System.Drawing.Size(124, 17);
            this.labelDBSettings.TabIndex = 11;
            this.labelDBSettings.Text = "Database Settings";
            // 
            // buttonExtendDBSettings
            // 
            this.buttonExtendDBSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExtendDBSettings.Location = new System.Drawing.Point(3, 89);
            this.buttonExtendDBSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonExtendDBSettings.Name = "buttonExtendDBSettings";
            this.buttonExtendDBSettings.Size = new System.Drawing.Size(25, 23);
            this.buttonExtendDBSettings.TabIndex = 5;
            this.buttonExtendDBSettings.Text = "+";
            this.buttonExtendDBSettings.UseVisualStyleBackColor = true;
            this.buttonExtendDBSettings.Click += new System.EventHandler(this.ButtonExtendDBSettings_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(193, 74);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 31);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.ButtonLogin_Click);
            // 
            // labelUserPassword
            // 
            this.labelUserPassword.AutoSize = true;
            this.labelUserPassword.Location = new System.Drawing.Point(29, 47);
            this.labelUserPassword.Name = "labelUserPassword";
            this.labelUserPassword.Size = new System.Drawing.Size(77, 17);
            this.labelUserPassword.TabIndex = 3;
            this.labelUserPassword.Text = "Password: ";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(29, 18);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(81, 17);
            this.labelUsername.TabIndex = 2;
            this.labelUsername.Text = "Username: ";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(116, 15);
            this.textBoxUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(151, 22);
            this.textBoxUsername.TabIndex = 0;
            // 
            // panelDBSettings
            // 
            this.panelDBSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDBSettings.Controls.Add(this.labelDBSettingsTitle);
            this.panelDBSettings.Controls.Add(this.buttonSave);
            this.panelDBSettings.Controls.Add(this.labelDbName);
            this.panelDBSettings.Controls.Add(this.labelDbPassword);
            this.panelDBSettings.Controls.Add(this.labelUID);
            this.panelDBSettings.Controls.Add(this.labelPort);
            this.panelDBSettings.Controls.Add(this.labelServer);
            this.panelDBSettings.Controls.Add(this.textBoxDbName);
            this.panelDBSettings.Controls.Add(this.textBoxDbPassword);
            this.panelDBSettings.Controls.Add(this.textBoxUID);
            this.panelDBSettings.Controls.Add(this.numericUpDownPort);
            this.panelDBSettings.Controls.Add(this.textBoxServer);
            this.panelDBSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDBSettings.Location = new System.Drawing.Point(0, 116);
            this.panelDBSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelDBSettings.Name = "panelDBSettings";
            this.panelDBSettings.Size = new System.Drawing.Size(288, 202);
            this.panelDBSettings.TabIndex = 3;
            // 
            // panelLogin
            // 
            this.panelLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLogin.Controls.Add(this.labelDBSettings);
            this.panelLogin.Controls.Add(this.buttonExtendDBSettings);
            this.panelLogin.Controls.Add(this.buttonLogin);
            this.panelLogin.Controls.Add(this.labelUserPassword);
            this.panelLogin.Controls.Add(this.labelUsername);
            this.panelLogin.Controls.Add(this.textBoxUserPassword);
            this.panelLogin.Controls.Add(this.textBoxUsername);
            this.panelLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogin.Location = new System.Drawing.Point(0, 0);
            this.panelLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(288, 116);
            this.panelLogin.TabIndex = 2;
            // 
            // textBoxUserPassword
            // 
            this.textBoxUserPassword.Location = new System.Drawing.Point(116, 44);
            this.textBoxUserPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxUserPassword.Name = "textBoxUserPassword";
            this.textBoxUserPassword.PasswordChar = '●';
            this.textBoxUserPassword.Size = new System.Drawing.Size(151, 22);
            this.textBoxUserPassword.TabIndex = 1;
            this.textBoxUserPassword.UseSystemPasswordChar = true;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 318);
            this.Controls.Add(this.panelDBSettings);
            this.Controls.Add(this.panelLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login Form";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.panelDBSettings.ResumeLayout(false);
            this.panelDBSettings.PerformLayout();
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelDBSettingsTitle;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelDbName;
        private System.Windows.Forms.Label labelDbPassword;
        private System.Windows.Forms.Label labelUID;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.TextBox textBoxDbName;
        private System.Windows.Forms.TextBox textBoxDbPassword;
        private System.Windows.Forms.TextBox textBoxUID;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.Label labelDBSettings;
        private System.Windows.Forms.Button buttonExtendDBSettings;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label labelUserPassword;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Panel panelDBSettings;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.TextBox textBoxUserPassword;
    }
}