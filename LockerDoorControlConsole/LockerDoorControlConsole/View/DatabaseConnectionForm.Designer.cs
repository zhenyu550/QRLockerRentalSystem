namespace LockerDoorControlConsole.View
{
    partial class DatabaseConnectionForm
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
            this.panelDBConnectionNorth = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelDBConnectionSouth = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.panelDBConnectionCenter = new System.Windows.Forms.Panel();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUid = new System.Windows.Forms.TextBox();
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUID = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelServer = new System.Windows.Forms.Label();
            this.labelInstruction = new System.Windows.Forms.Label();
            this.panelDBConnectionNorth.SuspendLayout();
            this.panelDBConnectionSouth.SuspendLayout();
            this.panelDBConnectionCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.SuspendLayout();
            // 
            // panelDBConnectionNorth
            // 
            this.panelDBConnectionNorth.Controls.Add(this.labelTitle);
            this.panelDBConnectionNorth.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDBConnectionNorth.Location = new System.Drawing.Point(0, 0);
            this.panelDBConnectionNorth.Name = "panelDBConnectionNorth";
            this.panelDBConnectionNorth.Size = new System.Drawing.Size(349, 42);
            this.panelDBConnectionNorth.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(3, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(283, 25);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Database Connection Setup";
            // 
            // panelDBConnectionSouth
            // 
            this.panelDBConnectionSouth.Controls.Add(this.buttonCancel);
            this.panelDBConnectionSouth.Controls.Add(this.buttonConnect);
            this.panelDBConnectionSouth.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelDBConnectionSouth.Location = new System.Drawing.Point(0, 217);
            this.panelDBConnectionSouth.Name = "panelDBConnectionSouth";
            this.panelDBConnectionSouth.Size = new System.Drawing.Size(349, 52);
            this.panelDBConnectionSouth.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(8, 7);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 39);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(262, 7);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 39);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // panelDBConnectionCenter
            // 
            this.panelDBConnectionCenter.Controls.Add(this.textBoxDatabase);
            this.panelDBConnectionCenter.Controls.Add(this.textBoxPassword);
            this.panelDBConnectionCenter.Controls.Add(this.textBoxUid);
            this.panelDBConnectionCenter.Controls.Add(this.numericUpDownPort);
            this.panelDBConnectionCenter.Controls.Add(this.textBoxServer);
            this.panelDBConnectionCenter.Controls.Add(this.labelDatabase);
            this.panelDBConnectionCenter.Controls.Add(this.labelPassword);
            this.panelDBConnectionCenter.Controls.Add(this.labelUID);
            this.panelDBConnectionCenter.Controls.Add(this.labelPort);
            this.panelDBConnectionCenter.Controls.Add(this.labelServer);
            this.panelDBConnectionCenter.Controls.Add(this.labelInstruction);
            this.panelDBConnectionCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDBConnectionCenter.Location = new System.Drawing.Point(0, 42);
            this.panelDBConnectionCenter.Name = "panelDBConnectionCenter";
            this.panelDBConnectionCenter.Size = new System.Drawing.Size(349, 175);
            this.panelDBConnectionCenter.TabIndex = 2;
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(88, 147);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(249, 22);
            this.textBoxDatabase.TabIndex = 10;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(88, 119);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '●';
            this.textBoxPassword.Size = new System.Drawing.Size(249, 22);
            this.textBoxPassword.TabIndex = 9;
            // 
            // textBoxUid
            // 
            this.textBoxUid.Location = new System.Drawing.Point(88, 91);
            this.textBoxUid.Name = "textBoxUid";
            this.textBoxUid.Size = new System.Drawing.Size(249, 22);
            this.textBoxUid.TabIndex = 8;
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Location = new System.Drawing.Point(88, 61);
            this.numericUpDownPort.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownPort.Name = "numericUpDownPort";
            this.numericUpDownPort.Size = new System.Drawing.Size(249, 22);
            this.numericUpDownPort.TabIndex = 7;
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(88, 33);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(249, 22);
            this.textBoxServer.TabIndex = 6;
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(3, 150);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(77, 17);
            this.labelDatabase.TabIndex = 5;
            this.labelDatabase.Text = "Database: ";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(5, 122);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(77, 17);
            this.labelPassword.TabIndex = 4;
            this.labelPassword.Text = "Password: ";
            // 
            // labelUID
            // 
            this.labelUID.AutoSize = true;
            this.labelUID.Location = new System.Drawing.Point(5, 94);
            this.labelUID.Name = "labelUID";
            this.labelUID.Size = new System.Drawing.Size(39, 17);
            this.labelUID.TabIndex = 3;
            this.labelUID.Text = "UID: ";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort.Location = new System.Drawing.Point(5, 63);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(42, 17);
            this.labelPort.TabIndex = 2;
            this.labelPort.Text = "Port: ";
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelServer.Location = new System.Drawing.Point(5, 36);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(58, 17);
            this.labelServer.TabIndex = 1;
            this.labelServer.Text = "Server: ";
            // 
            // labelInstruction
            // 
            this.labelInstruction.AutoSize = true;
            this.labelInstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInstruction.Location = new System.Drawing.Point(5, 3);
            this.labelInstruction.Name = "labelInstruction";
            this.labelInstruction.Size = new System.Drawing.Size(305, 18);
            this.labelInstruction.TabIndex = 0;
            this.labelInstruction.Text = "Please enter the database connection details.";
            // 
            // DatabaseConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 269);
            this.Controls.Add(this.panelDBConnectionCenter);
            this.Controls.Add(this.panelDBConnectionSouth);
            this.Controls.Add(this.panelDBConnectionNorth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DatabaseConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Locker Door Control Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DatabaseConnectionForm_FormClosing);
            this.panelDBConnectionNorth.ResumeLayout(false);
            this.panelDBConnectionNorth.PerformLayout();
            this.panelDBConnectionSouth.ResumeLayout(false);
            this.panelDBConnectionCenter.ResumeLayout(false);
            this.panelDBConnectionCenter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDBConnectionNorth;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelDBConnectionSouth;
        private System.Windows.Forms.Panel panelDBConnectionCenter;
        private System.Windows.Forms.Label labelInstruction;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUid;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelUID;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonConnect;
    }
}