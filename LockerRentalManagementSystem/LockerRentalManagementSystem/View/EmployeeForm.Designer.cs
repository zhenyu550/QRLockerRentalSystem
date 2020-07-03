namespace LockerRentalManagementSystem.View
{
    partial class EmployeeForm
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
            this.panelEmployeeFormRight = new System.Windows.Forms.Panel();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.comboBoxPosition = new System.Windows.Forms.ComboBox();
            this.comboBoxPermission = new System.Windows.Forms.ComboBox();
            this.labelPermission = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelGender = new System.Windows.Forms.Label();
            this.comboBoxGender = new System.Windows.Forms.ComboBox();
            this.textBoxHomeAddress = new System.Windows.Forms.TextBox();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.textBoxPhoneNo = new System.Windows.Forms.TextBox();
            this.textBoxIcPassport = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelHomeAddress = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelPhoneNo = new System.Windows.Forms.Label();
            this.labelIcPassport = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelViewEmployee = new System.Windows.Forms.Label();
            this.labelAddEmployee = new System.Windows.Forms.Label();
            this.labelCreateAdmin = new System.Windows.Forms.Label();
            this.panelEmployeeFormLeft = new System.Windows.Forms.Panel();
            this.labelEditEmployee = new System.Windows.Forms.Label();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelEmployeeFormBottom = new System.Windows.Forms.Panel();
            this.panelEmployeeFormRight.SuspendLayout();
            this.panelEmployeeFormLeft.SuspendLayout();
            this.panelEmployeeFormBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEmployeeFormRight
            // 
            this.panelEmployeeFormRight.Controls.Add(this.textBoxPassword);
            this.panelEmployeeFormRight.Controls.Add(this.textBoxUsername);
            this.panelEmployeeFormRight.Controls.Add(this.comboBoxPosition);
            this.panelEmployeeFormRight.Controls.Add(this.comboBoxPermission);
            this.panelEmployeeFormRight.Controls.Add(this.labelPermission);
            this.panelEmployeeFormRight.Controls.Add(this.labelUsername);
            this.panelEmployeeFormRight.Controls.Add(this.labelPassword);
            this.panelEmployeeFormRight.Controls.Add(this.labelPosition);
            this.panelEmployeeFormRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEmployeeFormRight.Location = new System.Drawing.Point(440, 0);
            this.panelEmployeeFormRight.Name = "panelEmployeeFormRight";
            this.panelEmployeeFormRight.Size = new System.Drawing.Size(338, 327);
            this.panelEmployeeFormRight.TabIndex = 4;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(140, 71);
            this.textBoxPassword.MaxLength = 50;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.ReadOnly = true;
            this.textBoxPassword.Size = new System.Drawing.Size(189, 22);
            this.textBoxPassword.TabIndex = 10;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(140, 45);
            this.textBoxUsername.MaxLength = 25;
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(189, 22);
            this.textBoxUsername.TabIndex = 9;
            // 
            // comboBoxPosition
            // 
            this.comboBoxPosition.FormattingEnabled = true;
            this.comboBoxPosition.Items.AddRange(new object[] {
            "Staff",
            "Manager",
            "Owner"});
            this.comboBoxPosition.Location = new System.Drawing.Point(140, 129);
            this.comboBoxPosition.MaxLength = 10;
            this.comboBoxPosition.Name = "comboBoxPosition";
            this.comboBoxPosition.Size = new System.Drawing.Size(189, 24);
            this.comboBoxPosition.TabIndex = 13;
            // 
            // comboBoxPermission
            // 
            this.comboBoxPermission.FormattingEnabled = true;
            this.comboBoxPermission.Items.AddRange(new object[] {
            "Normal",
            "Admin"});
            this.comboBoxPermission.Location = new System.Drawing.Point(140, 99);
            this.comboBoxPermission.MaxLength = 10;
            this.comboBoxPermission.Name = "comboBoxPermission";
            this.comboBoxPermission.Size = new System.Drawing.Size(189, 24);
            this.comboBoxPermission.TabIndex = 12;
            // 
            // labelPermission
            // 
            this.labelPermission.AutoSize = true;
            this.labelPermission.Location = new System.Drawing.Point(3, 102);
            this.labelPermission.Name = "labelPermission";
            this.labelPermission.Size = new System.Drawing.Size(136, 17);
            this.labelPermission.TabIndex = 16;
            this.labelPermission.Text = "Account Permission:";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(3, 48);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(136, 17);
            this.labelUsername.TabIndex = 13;
            this.labelUsername.Text = "Account Username: ";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(3, 74);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(128, 17);
            this.labelPassword.TabIndex = 14;
            this.labelPassword.Text = "Account Password:";
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(3, 132);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(62, 17);
            this.labelPosition.TabIndex = 18;
            this.labelPosition.Text = "Position:";
            // 
            // labelGender
            // 
            this.labelGender.AutoSize = true;
            this.labelGender.Location = new System.Drawing.Point(12, 132);
            this.labelGender.Name = "labelGender";
            this.labelGender.Size = new System.Drawing.Size(64, 17);
            this.labelGender.TabIndex = 33;
            this.labelGender.Text = "Gender: ";
            // 
            // comboBoxGender
            // 
            this.comboBoxGender.FormattingEnabled = true;
            this.comboBoxGender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.comboBoxGender.Location = new System.Drawing.Point(136, 129);
            this.comboBoxGender.MaxLength = 10;
            this.comboBoxGender.Name = "comboBoxGender";
            this.comboBoxGender.Size = new System.Drawing.Size(293, 24);
            this.comboBoxGender.TabIndex = 3;
            // 
            // textBoxHomeAddress
            // 
            this.textBoxHomeAddress.Location = new System.Drawing.Point(136, 244);
            this.textBoxHomeAddress.MaxLength = 1000;
            this.textBoxHomeAddress.Multiline = true;
            this.textBoxHomeAddress.Name = "textBoxHomeAddress";
            this.textBoxHomeAddress.Size = new System.Drawing.Size(293, 76);
            this.textBoxHomeAddress.TabIndex = 8;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(136, 190);
            this.textBoxEmail.MaxLength = 100;
            this.textBoxEmail.Multiline = true;
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(293, 48);
            this.textBoxEmail.TabIndex = 7;
            // 
            // textBoxPhoneNo
            // 
            this.textBoxPhoneNo.Location = new System.Drawing.Point(136, 162);
            this.textBoxPhoneNo.MaxLength = 20;
            this.textBoxPhoneNo.Name = "textBoxPhoneNo";
            this.textBoxPhoneNo.Size = new System.Drawing.Size(293, 22);
            this.textBoxPhoneNo.TabIndex = 6;
            // 
            // textBoxIcPassport
            // 
            this.textBoxIcPassport.Location = new System.Drawing.Point(136, 101);
            this.textBoxIcPassport.MaxLength = 20;
            this.textBoxIcPassport.Name = "textBoxIcPassport";
            this.textBoxIcPassport.Size = new System.Drawing.Size(293, 22);
            this.textBoxIcPassport.TabIndex = 2;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(136, 45);
            this.textBoxName.MaxLength = 100;
            this.textBoxName.Multiline = true;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(293, 50);
            this.textBoxName.TabIndex = 1;
            // 
            // labelHomeAddress
            // 
            this.labelHomeAddress.AutoSize = true;
            this.labelHomeAddress.Location = new System.Drawing.Point(11, 247);
            this.labelHomeAddress.Name = "labelHomeAddress";
            this.labelHomeAddress.Size = new System.Drawing.Size(105, 17);
            this.labelHomeAddress.TabIndex = 12;
            this.labelHomeAddress.Text = "Home Address:";
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(12, 193);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(51, 17);
            this.labelEmail.TabIndex = 24;
            this.labelEmail.Text = "E-Mail:";
            // 
            // labelPhoneNo
            // 
            this.labelPhoneNo.AutoSize = true;
            this.labelPhoneNo.Location = new System.Drawing.Point(11, 165);
            this.labelPhoneNo.Name = "labelPhoneNo";
            this.labelPhoneNo.Size = new System.Drawing.Size(79, 17);
            this.labelPhoneNo.TabIndex = 23;
            this.labelPhoneNo.Text = "Phone No.:";
            // 
            // labelIcPassport
            // 
            this.labelIcPassport.AutoSize = true;
            this.labelIcPassport.Location = new System.Drawing.Point(12, 104);
            this.labelIcPassport.Name = "labelIcPassport";
            this.labelIcPassport.Size = new System.Drawing.Size(118, 17);
            this.labelIcPassport.TabIndex = 4;
            this.labelIcPassport.Text = "IC / Passport No.:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 48);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(49, 17);
            this.labelName.TabIndex = 17;
            this.labelName.Text = "Name:";
            // 
            // labelViewEmployee
            // 
            this.labelViewEmployee.AutoSize = true;
            this.labelViewEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelViewEmployee.Location = new System.Drawing.Point(10, 9);
            this.labelViewEmployee.Name = "labelViewEmployee";
            this.labelViewEmployee.Size = new System.Drawing.Size(179, 25);
            this.labelViewEmployee.TabIndex = 35;
            this.labelViewEmployee.Text = "Employee Details";
            // 
            // labelAddEmployee
            // 
            this.labelAddEmployee.AutoSize = true;
            this.labelAddEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddEmployee.Location = new System.Drawing.Point(10, 9);
            this.labelAddEmployee.Name = "labelAddEmployee";
            this.labelAddEmployee.Size = new System.Drawing.Size(200, 25);
            this.labelAddEmployee.TabIndex = 14;
            this.labelAddEmployee.Text = "Add New Employee";
            // 
            // labelCreateAdmin
            // 
            this.labelCreateAdmin.AutoSize = true;
            this.labelCreateAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreateAdmin.Location = new System.Drawing.Point(10, 9);
            this.labelCreateAdmin.Name = "labelCreateAdmin";
            this.labelCreateAdmin.Size = new System.Drawing.Size(229, 25);
            this.labelCreateAdmin.TabIndex = 34;
            this.labelCreateAdmin.Text = "Create Admin Account";
            // 
            // panelEmployeeFormLeft
            // 
            this.panelEmployeeFormLeft.Controls.Add(this.labelGender);
            this.panelEmployeeFormLeft.Controls.Add(this.comboBoxGender);
            this.panelEmployeeFormLeft.Controls.Add(this.textBoxHomeAddress);
            this.panelEmployeeFormLeft.Controls.Add(this.textBoxEmail);
            this.panelEmployeeFormLeft.Controls.Add(this.textBoxPhoneNo);
            this.panelEmployeeFormLeft.Controls.Add(this.textBoxIcPassport);
            this.panelEmployeeFormLeft.Controls.Add(this.textBoxName);
            this.panelEmployeeFormLeft.Controls.Add(this.labelHomeAddress);
            this.panelEmployeeFormLeft.Controls.Add(this.labelEmail);
            this.panelEmployeeFormLeft.Controls.Add(this.labelPhoneNo);
            this.panelEmployeeFormLeft.Controls.Add(this.labelIcPassport);
            this.panelEmployeeFormLeft.Controls.Add(this.labelName);
            this.panelEmployeeFormLeft.Controls.Add(this.labelEditEmployee);
            this.panelEmployeeFormLeft.Controls.Add(this.labelViewEmployee);
            this.panelEmployeeFormLeft.Controls.Add(this.labelAddEmployee);
            this.panelEmployeeFormLeft.Controls.Add(this.labelCreateAdmin);
            this.panelEmployeeFormLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEmployeeFormLeft.Location = new System.Drawing.Point(0, 0);
            this.panelEmployeeFormLeft.Name = "panelEmployeeFormLeft";
            this.panelEmployeeFormLeft.Size = new System.Drawing.Size(440, 327);
            this.panelEmployeeFormLeft.TabIndex = 3;
            // 
            // labelEditEmployee
            // 
            this.labelEditEmployee.AutoSize = true;
            this.labelEditEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEditEmployee.Location = new System.Drawing.Point(10, 9);
            this.labelEditEmployee.Name = "labelEditEmployee";
            this.labelEditEmployee.Size = new System.Drawing.Size(222, 25);
            this.labelEditEmployee.TabIndex = 36;
            this.labelEditEmployee.Text = "Edit Employee Details";
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(15, 6);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 31);
            this.buttonBack.TabIndex = 29;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.ButtonBack_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(694, 6);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 31);
            this.buttonSave.TabIndex = 28;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(694, 6);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 31);
            this.buttonEdit.TabIndex = 27;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(15, 6);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 31);
            this.buttonClose.TabIndex = 27;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(694, 6);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 31);
            this.buttonConfirm.TabIndex = 17;
            this.buttonConfirm.Text = "Confirm";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.ButtonConfirm_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(15, 6);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 31);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // panelEmployeeFormBottom
            // 
            this.panelEmployeeFormBottom.Controls.Add(this.buttonBack);
            this.panelEmployeeFormBottom.Controls.Add(this.buttonSave);
            this.panelEmployeeFormBottom.Controls.Add(this.buttonEdit);
            this.panelEmployeeFormBottom.Controls.Add(this.buttonClose);
            this.panelEmployeeFormBottom.Controls.Add(this.buttonConfirm);
            this.panelEmployeeFormBottom.Controls.Add(this.buttonCancel);
            this.panelEmployeeFormBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEmployeeFormBottom.Location = new System.Drawing.Point(0, 327);
            this.panelEmployeeFormBottom.Name = "panelEmployeeFormBottom";
            this.panelEmployeeFormBottom.Size = new System.Drawing.Size(778, 43);
            this.panelEmployeeFormBottom.TabIndex = 5;
            // 
            // EmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 370);
            this.Controls.Add(this.panelEmployeeFormRight);
            this.Controls.Add(this.panelEmployeeFormLeft);
            this.Controls.Add(this.panelEmployeeFormBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployeeForm";
            this.panelEmployeeFormRight.ResumeLayout(false);
            this.panelEmployeeFormRight.PerformLayout();
            this.panelEmployeeFormLeft.ResumeLayout(false);
            this.panelEmployeeFormLeft.PerformLayout();
            this.panelEmployeeFormBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelEmployeeFormRight;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.ComboBox comboBoxPosition;
        private System.Windows.Forms.ComboBox comboBoxPermission;
        private System.Windows.Forms.Label labelPermission;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Label labelGender;
        private System.Windows.Forms.ComboBox comboBoxGender;
        private System.Windows.Forms.TextBox textBoxHomeAddress;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxPhoneNo;
        private System.Windows.Forms.TextBox textBoxIcPassport;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelHomeAddress;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label labelPhoneNo;
        private System.Windows.Forms.Label labelIcPassport;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelViewEmployee;
        private System.Windows.Forms.Label labelAddEmployee;
        private System.Windows.Forms.Label labelCreateAdmin;
        private System.Windows.Forms.Panel panelEmployeeFormLeft;
        private System.Windows.Forms.Label labelEditEmployee;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelEmployeeFormBottom;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
    }
}