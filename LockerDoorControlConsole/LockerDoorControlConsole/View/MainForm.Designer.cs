namespace LockerDoorControlConsole
{
    partial class MainForm
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
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "L-01-001"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Unlocked", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))))}, 0);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "L-01-002"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Unlocked", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))))}, 0);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelBase = new System.Windows.Forms.Panel();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.listViewLocker = new System.Windows.Forms.ListView();
            this.columnHeaderLockerCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLockerStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLockerDoorStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListIcon96 = new System.Windows.Forms.ImageList(this.components);
            this.panelUnlockLockerLabel = new System.Windows.Forms.Panel();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.labelLocker = new System.Windows.Forms.Label();
            this.tableLayoutPanelBottom = new System.Windows.Forms.TableLayoutPanel();
            this.buttonScan = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelQRStringInstruction = new System.Windows.Forms.Label();
            this.textBoxQRInput = new System.Windows.Forms.TextBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelCabinet = new System.Windows.Forms.Label();
            this.labelCabinetCode = new System.Windows.Forms.Label();
            this.panelCurrentDateTime = new System.Windows.Forms.Panel();
            this.labelCurrentTime = new System.Windows.Forms.Label();
            this.labelCurrentDate = new System.Windows.Forms.Label();
            this.timerCurrentDateTime = new System.Windows.Forms.Timer(this.components);
            this.timerAutoRefresh = new System.Windows.Forms.Timer(this.components);
            this.panelBase.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.panelUnlockLockerLabel.SuspendLayout();
            this.tableLayoutPanelBottom.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelCurrentDateTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBase
            // 
            this.panelBase.Controls.Add(this.panelCenter);
            this.panelBase.Controls.Add(this.tableLayoutPanelBottom);
            this.panelBase.Controls.Add(this.panelTop);
            this.panelBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBase.Location = new System.Drawing.Point(0, 0);
            this.panelBase.Margin = new System.Windows.Forms.Padding(4);
            this.panelBase.Name = "panelBase";
            this.panelBase.Size = new System.Drawing.Size(1045, 690);
            this.panelBase.TabIndex = 0;
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.listViewLocker);
            this.panelCenter.Controls.Add(this.panelUnlockLockerLabel);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 101);
            this.panelCenter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(1045, 470);
            this.panelCenter.TabIndex = 7;
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
            listViewItem3,
            listViewItem4});
            this.listViewLocker.LargeImageList = this.imageListIcon96;
            this.listViewLocker.Location = new System.Drawing.Point(0, 48);
            this.listViewLocker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewLocker.Name = "listViewLocker";
            this.listViewLocker.Size = new System.Drawing.Size(1045, 422);
            this.listViewLocker.TabIndex = 6;
            this.listViewLocker.TileSize = new System.Drawing.Size(210, 110);
            this.listViewLocker.UseCompatibleStateImageBehavior = false;
            this.listViewLocker.View = System.Windows.Forms.View.Tile;
            this.listViewLocker.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewUnlockLockers_ItemSelectionChanged);
            // 
            // imageListIcon96
            // 
            this.imageListIcon96.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIcon96.ImageStream")));
            this.imageListIcon96.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListIcon96.Images.SetKeyName(0, "Lock Green 256.png");
            this.imageListIcon96.Images.SetKeyName(1, "Lock Grey 256.png");
            this.imageListIcon96.Images.SetKeyName(2, "Lock Red 256.png");
            this.imageListIcon96.Images.SetKeyName(3, "Lock Yellow 256.png");
            this.imageListIcon96.Images.SetKeyName(4, "Unlock Green 256.png");
            this.imageListIcon96.Images.SetKeyName(5, "Unlock Grey 256.png");
            this.imageListIcon96.Images.SetKeyName(6, "Unlock Red 256.png");
            this.imageListIcon96.Images.SetKeyName(7, "Unlock Yellow 256.png");
            // 
            // panelUnlockLockerLabel
            // 
            this.panelUnlockLockerLabel.BackColor = System.Drawing.Color.LightGray;
            this.panelUnlockLockerLabel.Controls.Add(this.buttonRefresh);
            this.panelUnlockLockerLabel.Controls.Add(this.labelLocker);
            this.panelUnlockLockerLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUnlockLockerLabel.Location = new System.Drawing.Point(0, 0);
            this.panelUnlockLockerLabel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelUnlockLockerLabel.Name = "panelUnlockLockerLabel";
            this.panelUnlockLockerLabel.Size = new System.Drawing.Size(1045, 48);
            this.panelUnlockLockerLabel.TabIndex = 7;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(940, 7);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(94, 36);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // labelLocker
            // 
            this.labelLocker.AutoSize = true;
            this.labelLocker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLocker.Location = new System.Drawing.Point(16, 14);
            this.labelLocker.Name = "labelLocker";
            this.labelLocker.Size = new System.Drawing.Size(181, 20);
            this.labelLocker.TabIndex = 0;
            this.labelLocker.Text = "Lockers in this Cabinet";
            // 
            // tableLayoutPanelBottom
            // 
            this.tableLayoutPanelBottom.ColumnCount = 2;
            this.tableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelBottom.Controls.Add(this.buttonScan, 1, 0);
            this.tableLayoutPanelBottom.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelBottom.Location = new System.Drawing.Point(0, 571);
            this.tableLayoutPanelBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanelBottom.Name = "tableLayoutPanelBottom";
            this.tableLayoutPanelBottom.RowCount = 1;
            this.tableLayoutPanelBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBottom.Size = new System.Drawing.Size(1045, 119);
            this.tableLayoutPanelBottom.TabIndex = 5;
            // 
            // buttonScan
            // 
            this.buttonScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonScan.Location = new System.Drawing.Point(847, 5);
            this.buttonScan.Margin = new System.Windows.Forms.Padding(11, 5, 11, 5);
            this.buttonScan.Name = "buttonScan";
            this.buttonScan.Size = new System.Drawing.Size(187, 109);
            this.buttonScan.TabIndex = 1;
            this.buttonScan.Text = "Scan";
            this.buttonScan.UseVisualStyleBackColor = true;
            this.buttonScan.Click += new System.EventHandler(this.ButtonScan_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelQRStringInstruction, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxQRInput, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(830, 115);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelQRStringInstruction
            // 
            this.labelQRStringInstruction.AutoSize = true;
            this.labelQRStringInstruction.Location = new System.Drawing.Point(3, 12);
            this.labelQRStringInstruction.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.labelQRStringInstruction.Name = "labelQRStringInstruction";
            this.labelQRStringInstruction.Size = new System.Drawing.Size(216, 16);
            this.labelQRStringInstruction.TabIndex = 1;
            this.labelQRStringInstruction.Text = "Please scan your QR Code here.";
            // 
            // textBoxQRInput
            // 
            this.textBoxQRInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQRInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxQRInput.Location = new System.Drawing.Point(5, 38);
            this.textBoxQRInput.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.textBoxQRInput.Name = "textBoxQRInput";
            this.textBoxQRInput.Size = new System.Drawing.Size(820, 49);
            this.textBoxQRInput.TabIndex = 0;
            this.textBoxQRInput.UseSystemPasswordChar = true;
            this.textBoxQRInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxQRInput_KeyDown);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelCabinet);
            this.panelTop.Controls.Add(this.labelCabinetCode);
            this.panelTop.Controls.Add(this.panelCurrentDateTime);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1045, 101);
            this.panelTop.TabIndex = 1;
            // 
            // labelCabinet
            // 
            this.labelCabinet.AutoSize = true;
            this.labelCabinet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCabinet.Location = new System.Drawing.Point(16, 9);
            this.labelCabinet.Name = "labelCabinet";
            this.labelCabinet.Size = new System.Drawing.Size(76, 20);
            this.labelCabinet.TabIndex = 2;
            this.labelCabinet.Text = "Cabinet: ";
            // 
            // labelCabinetCode
            // 
            this.labelCabinetCode.AutoSize = true;
            this.labelCabinetCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCabinetCode.Location = new System.Drawing.Point(13, 43);
            this.labelCabinetCode.Name = "labelCabinetCode";
            this.labelCabinetCode.Size = new System.Drawing.Size(95, 38);
            this.labelCabinetCode.TabIndex = 4;
            this.labelCabinetCode.Text = "M-01";
            // 
            // panelCurrentDateTime
            // 
            this.panelCurrentDateTime.Controls.Add(this.labelCurrentTime);
            this.panelCurrentDateTime.Controls.Add(this.labelCurrentDate);
            this.panelCurrentDateTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelCurrentDateTime.Location = new System.Drawing.Point(732, 0);
            this.panelCurrentDateTime.Margin = new System.Windows.Forms.Padding(4);
            this.panelCurrentDateTime.Name = "panelCurrentDateTime";
            this.panelCurrentDateTime.Size = new System.Drawing.Size(313, 101);
            this.panelCurrentDateTime.TabIndex = 2;
            // 
            // labelCurrentTime
            // 
            this.labelCurrentTime.AutoSize = true;
            this.labelCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentTime.Location = new System.Drawing.Point(9, 43);
            this.labelCurrentTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentTime.Name = "labelCurrentTime";
            this.labelCurrentTime.Size = new System.Drawing.Size(201, 38);
            this.labelCurrentTime.TabIndex = 1;
            this.labelCurrentTime.Text = "11:00:12 AM";
            // 
            // labelCurrentDate
            // 
            this.labelCurrentDate.AutoSize = true;
            this.labelCurrentDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentDate.Location = new System.Drawing.Point(12, 9);
            this.labelCurrentDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentDate.Name = "labelCurrentDate";
            this.labelCurrentDate.Size = new System.Drawing.Size(250, 20);
            this.labelCurrentDate.TabIndex = 0;
            this.labelCurrentDate.Text = "Wednesday, 10 September 2019";
            this.labelCurrentDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // timerCurrentDateTime
            // 
            this.timerCurrentDateTime.Tick += new System.EventHandler(this.TimerCurrentDateTime_Tick);
            // 
            // timerAutoRefresh
            // 
            this.timerAutoRefresh.Interval = 10000;
            this.timerAutoRefresh.Tick += new System.EventHandler(this.TimerAutoRefresh_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 690);
            this.Controls.Add(this.panelBase);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Locker Door Control Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panelBase.ResumeLayout(false);
            this.panelCenter.ResumeLayout(false);
            this.panelUnlockLockerLabel.ResumeLayout(false);
            this.panelUnlockLockerLabel.PerformLayout();
            this.tableLayoutPanelBottom.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelCurrentDateTime.ResumeLayout(false);
            this.panelCurrentDateTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBase;
        private System.Windows.Forms.Timer timerCurrentDateTime;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelCurrentTime;
        private System.Windows.Forms.Label labelCurrentDate;
        private System.Windows.Forms.Panel panelCurrentDateTime;
        private System.Windows.Forms.Label labelCabinetCode;
        private System.Windows.Forms.Label labelCabinet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBottom;
        private System.Windows.Forms.Button buttonScan;
        private System.Windows.Forms.TextBox textBoxQRInput;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Panel panelUnlockLockerLabel;
        private System.Windows.Forms.Label labelLocker;
        private System.Windows.Forms.ListView listViewLocker;
        private System.Windows.Forms.ImageList imageListIcon96;
        private System.Windows.Forms.ColumnHeader columnHeaderLockerCode;
        private System.Windows.Forms.ColumnHeader columnHeaderLockerStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderLockerDoorStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelQRStringInstruction;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Timer timerAutoRefresh;
    }
}

