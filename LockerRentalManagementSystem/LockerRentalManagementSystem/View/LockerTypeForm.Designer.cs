namespace LockerRentalManagementSystem.View
{
    partial class LockerTypeForm
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
            this.panelLockerTypeTop = new System.Windows.Forms.Panel();
            this.labelAddLockerType = new System.Windows.Forms.Label();
            this.labelEditLockerType = new System.Windows.Forms.Label();
            this.tableLayoutPanelLockerType = new System.Windows.Forms.TableLayoutPanel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelCode = new System.Windows.Forms.Label();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.numericUpDownRentalRate = new System.Windows.Forms.NumericUpDown();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelRentalRate = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.panelLockerTypeTop.SuspendLayout();
            this.tableLayoutPanelLockerType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRentalRate)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLockerTypeTop
            // 
            this.panelLockerTypeTop.Controls.Add(this.labelAddLockerType);
            this.panelLockerTypeTop.Controls.Add(this.labelEditLockerType);
            this.panelLockerTypeTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLockerTypeTop.Location = new System.Drawing.Point(0, 0);
            this.panelLockerTypeTop.Name = "panelLockerTypeTop";
            this.panelLockerTypeTop.Size = new System.Drawing.Size(224, 33);
            this.panelLockerTypeTop.TabIndex = 10;
            // 
            // labelAddLockerType
            // 
            this.labelAddLockerType.AutoSize = true;
            this.labelAddLockerType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddLockerType.Location = new System.Drawing.Point(6, 6);
            this.labelAddLockerType.Name = "labelAddLockerType";
            this.labelAddLockerType.Size = new System.Drawing.Size(143, 20);
            this.labelAddLockerType.TabIndex = 0;
            this.labelAddLockerType.Text = "Add Locker Type";
            // 
            // labelEditLockerType
            // 
            this.labelEditLockerType.AutoSize = true;
            this.labelEditLockerType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEditLockerType.Location = new System.Drawing.Point(6, 6);
            this.labelEditLockerType.Name = "labelEditLockerType";
            this.labelEditLockerType.Size = new System.Drawing.Size(143, 20);
            this.labelEditLockerType.TabIndex = 1;
            this.labelEditLockerType.Text = "Edit Locker Type";
            // 
            // tableLayoutPanelLockerType
            // 
            this.tableLayoutPanelLockerType.ColumnCount = 3;
            this.tableLayoutPanelLockerType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelLockerType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelLockerType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelLockerType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelLockerType.Controls.Add(this.buttonCancel, 0, 0);
            this.tableLayoutPanelLockerType.Controls.Add(this.buttonSave, 3, 0);
            this.tableLayoutPanelLockerType.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelLockerType.Location = new System.Drawing.Point(0, 112);
            this.tableLayoutPanelLockerType.Name = "tableLayoutPanelLockerType";
            this.tableLayoutPanelLockerType.RowCount = 1;
            this.tableLayoutPanelLockerType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLockerType.Size = new System.Drawing.Size(224, 38);
            this.tableLayoutPanelLockerType.TabIndex = 18;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCancel.Location = new System.Drawing.Point(3, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(68, 32);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.Location = new System.Drawing.Point(151, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(70, 32);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // labelCode
            // 
            this.labelCode.AutoSize = true;
            this.labelCode.Location = new System.Drawing.Point(8, 64);
            this.labelCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCode.Name = "labelCode";
            this.labelCode.Size = new System.Drawing.Size(38, 13);
            this.labelCode.TabIndex = 19;
            this.labelCode.Text = "Code: ";
            // 
            // textBoxCode
            // 
            this.textBoxCode.Location = new System.Drawing.Point(86, 62);
            this.textBoxCode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxCode.MaxLength = 3;
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(132, 20);
            this.textBoxCode.TabIndex = 14;
            // 
            // numericUpDownRentalRate
            // 
            this.numericUpDownRentalRate.DecimalPlaces = 2;
            this.numericUpDownRentalRate.Location = new System.Drawing.Point(86, 84);
            this.numericUpDownRentalRate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numericUpDownRentalRate.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownRentalRate.Name = "numericUpDownRentalRate";
            this.numericUpDownRentalRate.Size = new System.Drawing.Size(131, 20);
            this.numericUpDownRentalRate.TabIndex = 16;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(86, 39);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxName.MaxLength = 20;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(132, 20);
            this.textBoxName.TabIndex = 12;
            // 
            // labelRentalRate
            // 
            this.labelRentalRate.AutoSize = true;
            this.labelRentalRate.Location = new System.Drawing.Point(8, 86);
            this.labelRentalRate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRentalRate.Name = "labelRentalRate";
            this.labelRentalRate.Size = new System.Drawing.Size(70, 13);
            this.labelRentalRate.TabIndex = 17;
            this.labelRentalRate.Text = "Rental Rate: ";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(8, 41);
            this.labelName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 15;
            this.labelName.Text = "Name:";
            // 
            // LockerTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 150);
            this.Controls.Add(this.panelLockerTypeTop);
            this.Controls.Add(this.tableLayoutPanelLockerType);
            this.Controls.Add(this.labelCode);
            this.Controls.Add(this.textBoxCode);
            this.Controls.Add(this.numericUpDownRentalRate);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelRentalRate);
            this.Controls.Add(this.labelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LockerTypeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LockerTypeForm";
            this.panelLockerTypeTop.ResumeLayout(false);
            this.panelLockerTypeTop.PerformLayout();
            this.tableLayoutPanelLockerType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRentalRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelLockerTypeTop;
        private System.Windows.Forms.Label labelAddLockerType;
        private System.Windows.Forms.Label labelEditLockerType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLockerType;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelCode;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.NumericUpDown numericUpDownRentalRate;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelRentalRate;
        private System.Windows.Forms.Label labelName;
    }
}