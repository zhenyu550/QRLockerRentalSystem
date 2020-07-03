namespace LockerRentalManagementSystem.View
{
    partial class CustomerForm
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
            this.labelViewCustomer = new System.Windows.Forms.Label();
            this.labelGender = new System.Windows.Forms.Label();
            this.labelAddCustomer = new System.Windows.Forms.Label();
            this.comboBoxGender = new System.Windows.Forms.ComboBox();
            this.textBoxHomeAddress = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelIcPassport = new System.Windows.Forms.Label();
            this.textBoxPhoneNo = new System.Windows.Forms.TextBox();
            this.textBoxIcPassport = new System.Windows.Forms.TextBox();
            this.labelPhoneNo = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelHomeAddress = new System.Windows.Forms.Label();
            this.labelEditCustomer = new System.Windows.Forms.Label();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelCustomerForm = new System.Windows.Forms.Panel();
            this.panelCustomerFormBottom = new System.Windows.Forms.Panel();
            this.panelCustomerForm.SuspendLayout();
            this.panelCustomerFormBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelViewCustomer
            // 
            this.labelViewCustomer.AutoSize = true;
            this.labelViewCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelViewCustomer.Location = new System.Drawing.Point(12, 9);
            this.labelViewCustomer.Name = "labelViewCustomer";
            this.labelViewCustomer.Size = new System.Drawing.Size(177, 25);
            this.labelViewCustomer.TabIndex = 53;
            this.labelViewCustomer.Text = "Customer Details";
            // 
            // labelGender
            // 
            this.labelGender.AutoSize = true;
            this.labelGender.Location = new System.Drawing.Point(14, 132);
            this.labelGender.Name = "labelGender";
            this.labelGender.Size = new System.Drawing.Size(64, 17);
            this.labelGender.TabIndex = 50;
            this.labelGender.Text = "Gender: ";
            // 
            // labelAddCustomer
            // 
            this.labelAddCustomer.AutoSize = true;
            this.labelAddCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddCustomer.Location = new System.Drawing.Point(12, 9);
            this.labelAddCustomer.Name = "labelAddCustomer";
            this.labelAddCustomer.Size = new System.Drawing.Size(198, 25);
            this.labelAddCustomer.TabIndex = 44;
            this.labelAddCustomer.Text = "Add New Customer";
            // 
            // comboBoxGender
            // 
            this.comboBoxGender.FormattingEnabled = true;
            this.comboBoxGender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.comboBoxGender.Location = new System.Drawing.Point(138, 129);
            this.comboBoxGender.MaxLength = 10;
            this.comboBoxGender.Name = "comboBoxGender";
            this.comboBoxGender.Size = new System.Drawing.Size(301, 24);
            this.comboBoxGender.TabIndex = 3;
            // 
            // textBoxHomeAddress
            // 
            this.textBoxHomeAddress.Location = new System.Drawing.Point(138, 241);
            this.textBoxHomeAddress.MaxLength = 1000;
            this.textBoxHomeAddress.Multiline = true;
            this.textBoxHomeAddress.Name = "textBoxHomeAddress";
            this.textBoxHomeAddress.Size = new System.Drawing.Size(301, 76);
            this.textBoxHomeAddress.TabIndex = 8;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(14, 50);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(49, 17);
            this.labelName.TabIndex = 45;
            this.labelName.Text = "Name:";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(138, 187);
            this.textBoxEmail.MaxLength = 100;
            this.textBoxEmail.Multiline = true;
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(301, 48);
            this.textBoxEmail.TabIndex = 7;
            // 
            // labelIcPassport
            // 
            this.labelIcPassport.AutoSize = true;
            this.labelIcPassport.Location = new System.Drawing.Point(14, 104);
            this.labelIcPassport.Name = "labelIcPassport";
            this.labelIcPassport.Size = new System.Drawing.Size(118, 17);
            this.labelIcPassport.TabIndex = 38;
            this.labelIcPassport.Text = "IC / Passport No.:";
            // 
            // textBoxPhoneNo
            // 
            this.textBoxPhoneNo.Location = new System.Drawing.Point(138, 159);
            this.textBoxPhoneNo.MaxLength = 20;
            this.textBoxPhoneNo.Name = "textBoxPhoneNo";
            this.textBoxPhoneNo.Size = new System.Drawing.Size(301, 22);
            this.textBoxPhoneNo.TabIndex = 6;
            // 
            // textBoxIcPassport
            // 
            this.textBoxIcPassport.Location = new System.Drawing.Point(138, 101);
            this.textBoxIcPassport.MaxLength = 20;
            this.textBoxIcPassport.Name = "textBoxIcPassport";
            this.textBoxIcPassport.Size = new System.Drawing.Size(301, 22);
            this.textBoxIcPassport.TabIndex = 2;
            // 
            // labelPhoneNo
            // 
            this.labelPhoneNo.AutoSize = true;
            this.labelPhoneNo.Location = new System.Drawing.Point(14, 162);
            this.labelPhoneNo.Name = "labelPhoneNo";
            this.labelPhoneNo.Size = new System.Drawing.Size(79, 17);
            this.labelPhoneNo.TabIndex = 48;
            this.labelPhoneNo.Text = "Phone No.:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(138, 47);
            this.textBoxName.MaxLength = 100;
            this.textBoxName.Multiline = true;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(301, 48);
            this.textBoxName.TabIndex = 1;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(14, 190);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(51, 17);
            this.labelEmail.TabIndex = 49;
            this.labelEmail.Text = "E-Mail:";
            // 
            // labelHomeAddress
            // 
            this.labelHomeAddress.AutoSize = true;
            this.labelHomeAddress.Location = new System.Drawing.Point(14, 244);
            this.labelHomeAddress.Name = "labelHomeAddress";
            this.labelHomeAddress.Size = new System.Drawing.Size(105, 17);
            this.labelHomeAddress.TabIndex = 43;
            this.labelHomeAddress.Text = "Home Address:";
            // 
            // labelEditCustomer
            // 
            this.labelEditCustomer.AutoSize = true;
            this.labelEditCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEditCustomer.Location = new System.Drawing.Point(12, 9);
            this.labelEditCustomer.Name = "labelEditCustomer";
            this.labelEditCustomer.Size = new System.Drawing.Size(220, 25);
            this.labelEditCustomer.TabIndex = 54;
            this.labelEditCustomer.Text = "Edit Customer Details";
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(17, 8);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 31);
            this.buttonBack.TabIndex = 4;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.ButtonBack_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(364, 8);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 31);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(364, 8);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 31);
            this.buttonEdit.TabIndex = 3;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(17, 8);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 31);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(364, 8);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 31);
            this.buttonConfirm.TabIndex = 0;
            this.buttonConfirm.Text = "Confirm";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.ButtonConfirm_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(17, 8);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 31);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // panelCustomerForm
            // 
            this.panelCustomerForm.Controls.Add(this.labelViewCustomer);
            this.panelCustomerForm.Controls.Add(this.labelGender);
            this.panelCustomerForm.Controls.Add(this.labelAddCustomer);
            this.panelCustomerForm.Controls.Add(this.comboBoxGender);
            this.panelCustomerForm.Controls.Add(this.textBoxHomeAddress);
            this.panelCustomerForm.Controls.Add(this.labelName);
            this.panelCustomerForm.Controls.Add(this.textBoxEmail);
            this.panelCustomerForm.Controls.Add(this.labelIcPassport);
            this.panelCustomerForm.Controls.Add(this.textBoxPhoneNo);
            this.panelCustomerForm.Controls.Add(this.textBoxIcPassport);
            this.panelCustomerForm.Controls.Add(this.labelPhoneNo);
            this.panelCustomerForm.Controls.Add(this.textBoxName);
            this.panelCustomerForm.Controls.Add(this.labelEmail);
            this.panelCustomerForm.Controls.Add(this.labelHomeAddress);
            this.panelCustomerForm.Controls.Add(this.labelEditCustomer);
            this.panelCustomerForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCustomerForm.Location = new System.Drawing.Point(0, 0);
            this.panelCustomerForm.Name = "panelCustomerForm";
            this.panelCustomerForm.Size = new System.Drawing.Size(450, 331);
            this.panelCustomerForm.TabIndex = 2;
            // 
            // panelCustomerFormBottom
            // 
            this.panelCustomerFormBottom.Controls.Add(this.buttonBack);
            this.panelCustomerFormBottom.Controls.Add(this.buttonSave);
            this.panelCustomerFormBottom.Controls.Add(this.buttonEdit);
            this.panelCustomerFormBottom.Controls.Add(this.buttonClose);
            this.panelCustomerFormBottom.Controls.Add(this.buttonConfirm);
            this.panelCustomerFormBottom.Controls.Add(this.buttonCancel);
            this.panelCustomerFormBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelCustomerFormBottom.Location = new System.Drawing.Point(0, 331);
            this.panelCustomerFormBottom.Name = "panelCustomerFormBottom";
            this.panelCustomerFormBottom.Size = new System.Drawing.Size(450, 51);
            this.panelCustomerFormBottom.TabIndex = 3;
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 382);
            this.Controls.Add(this.panelCustomerForm);
            this.Controls.Add(this.panelCustomerFormBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerForm";
            this.panelCustomerForm.ResumeLayout(false);
            this.panelCustomerForm.PerformLayout();
            this.panelCustomerFormBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelViewCustomer;
        private System.Windows.Forms.Label labelGender;
        private System.Windows.Forms.Label labelAddCustomer;
        private System.Windows.Forms.ComboBox comboBoxGender;
        private System.Windows.Forms.TextBox textBoxHomeAddress;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.Label labelIcPassport;
        private System.Windows.Forms.TextBox textBoxPhoneNo;
        private System.Windows.Forms.TextBox textBoxIcPassport;
        private System.Windows.Forms.Label labelPhoneNo;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label labelHomeAddress;
        private System.Windows.Forms.Label labelEditCustomer;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelCustomerForm;
        private System.Windows.Forms.Panel panelCustomerFormBottom;
    }
}