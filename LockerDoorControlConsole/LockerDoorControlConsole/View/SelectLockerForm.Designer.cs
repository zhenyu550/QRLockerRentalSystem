namespace LockerDoorControlConsole.View
{
    partial class SelectLockerForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "L-01-001"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Unlocked", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))))}, 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "L-01-002"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Unlocked", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))))}, 0);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectLockerForm));
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelSelectLockerInstruction = new System.Windows.Forms.Label();
            this.labelSelectLocker = new System.Windows.Forms.Label();
            this.tableLayoutPanelBottom = new System.Windows.Forms.TableLayoutPanel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.panelCabinetLabel = new System.Windows.Forms.Panel();
            this.labelCabinet = new System.Windows.Forms.Label();
            this.labelCabinetCode = new System.Windows.Forms.Label();
            this.panelLockerLabel = new System.Windows.Forms.Panel();
            this.labelLocker = new System.Windows.Forms.Label();
            this.listViewLocker = new System.Windows.Forms.ListView();
            this.columnHeaderLockerCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLockerStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLockerDoorStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList96 = new System.Windows.Forms.ImageList(this.components);
            this.panelTop.SuspendLayout();
            this.tableLayoutPanelBottom.SuspendLayout();
            this.panelCabinetLabel.SuspendLayout();
            this.panelLockerLabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelSelectLockerInstruction);
            this.panelTop.Controls.Add(this.labelSelectLocker);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(882, 75);
            this.panelTop.TabIndex = 0;
            // 
            // labelSelectLockerInstruction
            // 
            this.labelSelectLockerInstruction.AutoSize = true;
            this.labelSelectLockerInstruction.Location = new System.Drawing.Point(14, 47);
            this.labelSelectLockerInstruction.Name = "labelSelectLockerInstruction";
            this.labelSelectLockerInstruction.Size = new System.Drawing.Size(348, 17);
            this.labelSelectLockerInstruction.TabIndex = 1;
            this.labelSelectLockerInstruction.Text = "Please select the locker that you want to open or lock.";
            // 
            // labelSelectLocker
            // 
            this.labelSelectLocker.AutoSize = true;
            this.labelSelectLocker.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectLocker.Location = new System.Drawing.Point(12, 9);
            this.labelSelectLocker.Name = "labelSelectLocker";
            this.labelSelectLocker.Size = new System.Drawing.Size(173, 29);
            this.labelSelectLocker.TabIndex = 0;
            this.labelSelectLocker.Text = "Select Locker";
            // 
            // tableLayoutPanelBottom
            // 
            this.tableLayoutPanelBottom.ColumnCount = 4;
            this.tableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelBottom.Controls.Add(this.buttonCancel, 0, 0);
            this.tableLayoutPanelBottom.Controls.Add(this.buttonSelect, 3, 0);
            this.tableLayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelBottom.Location = new System.Drawing.Point(0, 501);
            this.tableLayoutPanelBottom.Name = "tableLayoutPanelBottom";
            this.tableLayoutPanelBottom.RowCount = 1;
            this.tableLayoutPanelBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBottom.Size = new System.Drawing.Size(882, 52);
            this.tableLayoutPanelBottom.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCancel.Location = new System.Drawing.Point(3, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(214, 46);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonSelect
            // 
            this.buttonSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSelect.Location = new System.Drawing.Point(663, 3);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(216, 46);
            this.buttonSelect.TabIndex = 1;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // panelCabinetLabel
            // 
            this.panelCabinetLabel.Controls.Add(this.labelCabinet);
            this.panelCabinetLabel.Controls.Add(this.labelCabinetCode);
            this.panelCabinetLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCabinetLabel.Location = new System.Drawing.Point(0, 75);
            this.panelCabinetLabel.Margin = new System.Windows.Forms.Padding(4);
            this.panelCabinetLabel.Name = "panelCabinetLabel";
            this.panelCabinetLabel.Size = new System.Drawing.Size(882, 38);
            this.panelCabinetLabel.TabIndex = 2;
            // 
            // labelCabinet
            // 
            this.labelCabinet.AutoSize = true;
            this.labelCabinet.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCabinet.Location = new System.Drawing.Point(14, 14);
            this.labelCabinet.Name = "labelCabinet";
            this.labelCabinet.Size = new System.Drawing.Size(64, 17);
            this.labelCabinet.TabIndex = 2;
            this.labelCabinet.Text = "Cabinet: ";
            // 
            // labelCabinetCode
            // 
            this.labelCabinetCode.AutoSize = true;
            this.labelCabinetCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCabinetCode.Location = new System.Drawing.Point(84, 6);
            this.labelCabinetCode.Name = "labelCabinetCode";
            this.labelCabinetCode.Size = new System.Drawing.Size(62, 25);
            this.labelCabinetCode.TabIndex = 4;
            this.labelCabinetCode.Text = "M-01";
            // 
            // panelLockerLabel
            // 
            this.panelLockerLabel.BackColor = System.Drawing.Color.LightGray;
            this.panelLockerLabel.Controls.Add(this.labelLocker);
            this.panelLockerLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLockerLabel.Location = new System.Drawing.Point(0, 113);
            this.panelLockerLabel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelLockerLabel.Name = "panelLockerLabel";
            this.panelLockerLabel.Size = new System.Drawing.Size(882, 27);
            this.panelLockerLabel.TabIndex = 8;
            // 
            // labelLocker
            // 
            this.labelLocker.AutoSize = true;
            this.labelLocker.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLocker.Location = new System.Drawing.Point(14, 4);
            this.labelLocker.Name = "labelLocker";
            this.labelLocker.Size = new System.Drawing.Size(151, 17);
            this.labelLocker.TabIndex = 0;
            this.labelLocker.Text = "Lockers in this Cabinet";
            // 
            // listViewLocker
            // 
            this.listViewLocker.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLockerCode,
            this.columnHeaderLockerStatus,
            this.columnHeaderLockerDoorStatus});
            this.listViewLocker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLocker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewLocker.HideSelection = false;
            this.listViewLocker.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listViewLocker.LargeImageList = this.imageList96;
            this.listViewLocker.Location = new System.Drawing.Point(0, 140);
            this.listViewLocker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewLocker.MultiSelect = false;
            this.listViewLocker.Name = "listViewLocker";
            this.listViewLocker.Size = new System.Drawing.Size(882, 361);
            this.listViewLocker.TabIndex = 9;
            this.listViewLocker.TileSize = new System.Drawing.Size(210, 110);
            this.listViewLocker.UseCompatibleStateImageBehavior = false;
            this.listViewLocker.View = System.Windows.Forms.View.Tile;
            // 
            // imageList96
            // 
            this.imageList96.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList96.ImageStream")));
            this.imageList96.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList96.Images.SetKeyName(0, "Lock Green 256.png");
            this.imageList96.Images.SetKeyName(1, "Lock Grey 256.png");
            this.imageList96.Images.SetKeyName(2, "Lock Red 256.png");
            this.imageList96.Images.SetKeyName(3, "Lock Yellow 256.png");
            this.imageList96.Images.SetKeyName(4, "Unlock Green 256.png");
            this.imageList96.Images.SetKeyName(5, "Unlock Grey 256.png");
            this.imageList96.Images.SetKeyName(6, "Unlock Red 256.png");
            this.imageList96.Images.SetKeyName(7, "Unlock Yellow 256.png");
            // 
            // SelectLockerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.listViewLocker);
            this.Controls.Add(this.panelLockerLabel);
            this.Controls.Add(this.panelCabinetLabel);
            this.Controls.Add(this.tableLayoutPanelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "SelectLockerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Locker Form";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.tableLayoutPanelBottom.ResumeLayout(false);
            this.panelCabinetLabel.ResumeLayout(false);
            this.panelCabinetLabel.PerformLayout();
            this.panelLockerLabel.ResumeLayout(false);
            this.panelLockerLabel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelSelectLockerInstruction;
        private System.Windows.Forms.Label labelSelectLocker;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBottom;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Panel panelCabinetLabel;
        private System.Windows.Forms.Label labelCabinet;
        private System.Windows.Forms.Label labelCabinetCode;
        private System.Windows.Forms.Panel panelLockerLabel;
        private System.Windows.Forms.Label labelLocker;
        private System.Windows.Forms.ListView listViewLocker;
        private System.Windows.Forms.ColumnHeader columnHeaderLockerCode;
        private System.Windows.Forms.ColumnHeader columnHeaderLockerStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderLockerDoorStatus;
        private System.Windows.Forms.ImageList imageList96;
    }
}