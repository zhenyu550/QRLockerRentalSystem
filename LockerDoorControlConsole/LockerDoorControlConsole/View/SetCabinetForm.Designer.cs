namespace LockerDoorControlConsole.View
{
    partial class SetCabinetForm
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
            this.panelSetCabinetNorth = new System.Windows.Forms.Panel();
            this.labelSetCabinetTitle = new System.Windows.Forms.Label();
            this.panelSetCabinetSouth = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.panelSetCabinetCenter = new System.Windows.Forms.Panel();
            this.labelSetCabinet = new System.Windows.Forms.Label();
            this.labelSetCabinetInstruction = new System.Windows.Forms.Label();
            this.comboBoxCabinetCode = new System.Windows.Forms.ComboBox();
            this.panelSetCabinetNorth.SuspendLayout();
            this.panelSetCabinetSouth.SuspendLayout();
            this.panelSetCabinetCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSetCabinetNorth
            // 
            this.panelSetCabinetNorth.Controls.Add(this.labelSetCabinetTitle);
            this.panelSetCabinetNorth.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSetCabinetNorth.Location = new System.Drawing.Point(0, 0);
            this.panelSetCabinetNorth.Name = "panelSetCabinetNorth";
            this.panelSetCabinetNorth.Size = new System.Drawing.Size(420, 43);
            this.panelSetCabinetNorth.TabIndex = 0;
            // 
            // labelSetCabinetTitle
            // 
            this.labelSetCabinetTitle.AutoSize = true;
            this.labelSetCabinetTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSetCabinetTitle.Location = new System.Drawing.Point(13, 9);
            this.labelSetCabinetTitle.Name = "labelSetCabinetTitle";
            this.labelSetCabinetTitle.Size = new System.Drawing.Size(115, 25);
            this.labelSetCabinetTitle.TabIndex = 0;
            this.labelSetCabinetTitle.Text = "Set Cabinet";
            // 
            // panelSetCabinetSouth
            // 
            this.panelSetCabinetSouth.Controls.Add(this.buttonCancel);
            this.panelSetCabinetSouth.Controls.Add(this.buttonConfirm);
            this.panelSetCabinetSouth.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSetCabinetSouth.Location = new System.Drawing.Point(0, 129);
            this.panelSetCabinetSouth.Name = "panelSetCabinetSouth";
            this.panelSetCabinetSouth.Size = new System.Drawing.Size(420, 56);
            this.panelSetCabinetSouth.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(12, 6);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 40);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(307, 6);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(100, 40);
            this.buttonConfirm.TabIndex = 0;
            this.buttonConfirm.Text = "Confirm";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.ButtonConfirm_Click);
            // 
            // panelSetCabinetCenter
            // 
            this.panelSetCabinetCenter.Controls.Add(this.comboBoxCabinetCode);
            this.panelSetCabinetCenter.Controls.Add(this.labelSetCabinet);
            this.panelSetCabinetCenter.Controls.Add(this.labelSetCabinetInstruction);
            this.panelSetCabinetCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSetCabinetCenter.Location = new System.Drawing.Point(0, 43);
            this.panelSetCabinetCenter.Name = "panelSetCabinetCenter";
            this.panelSetCabinetCenter.Size = new System.Drawing.Size(420, 86);
            this.panelSetCabinetCenter.TabIndex = 2;
            // 
            // labelSetCabinet
            // 
            this.labelSetCabinet.AutoSize = true;
            this.labelSetCabinet.Location = new System.Drawing.Point(15, 46);
            this.labelSetCabinet.Name = "labelSetCabinet";
            this.labelSetCabinet.Size = new System.Drawing.Size(101, 17);
            this.labelSetCabinet.TabIndex = 1;
            this.labelSetCabinet.Text = "Cabinet Code: ";
            // 
            // labelSetCabinetInstruction
            // 
            this.labelSetCabinetInstruction.AutoSize = true;
            this.labelSetCabinetInstruction.Location = new System.Drawing.Point(15, 13);
            this.labelSetCabinetInstruction.Name = "labelSetCabinetInstruction";
            this.labelSetCabinetInstruction.Size = new System.Drawing.Size(397, 17);
            this.labelSetCabinetInstruction.TabIndex = 0;
            this.labelSetCabinetInstruction.Text = "Please assign a cabinet to the system using the cabinet code.";
            // 
            // comboBoxCabinetCode
            // 
            this.comboBoxCabinetCode.FormattingEnabled = true;
            this.comboBoxCabinetCode.Location = new System.Drawing.Point(123, 46);
            this.comboBoxCabinetCode.Name = "comboBoxCabinetCode";
            this.comboBoxCabinetCode.Size = new System.Drawing.Size(284, 24);
            this.comboBoxCabinetCode.TabIndex = 2;
            // 
            // SetCabinetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 185);
            this.Controls.Add(this.panelSetCabinetCenter);
            this.Controls.Add(this.panelSetCabinetSouth);
            this.Controls.Add(this.panelSetCabinetNorth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetCabinetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SetCabinetForm";
            this.panelSetCabinetNorth.ResumeLayout(false);
            this.panelSetCabinetNorth.PerformLayout();
            this.panelSetCabinetSouth.ResumeLayout(false);
            this.panelSetCabinetCenter.ResumeLayout(false);
            this.panelSetCabinetCenter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSetCabinetNorth;
        private System.Windows.Forms.Label labelSetCabinetTitle;
        private System.Windows.Forms.Panel panelSetCabinetSouth;
        private System.Windows.Forms.Panel panelSetCabinetCenter;
        private System.Windows.Forms.Label labelSetCabinet;
        private System.Windows.Forms.Label labelSetCabinetInstruction;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxCabinetCode;
    }
}