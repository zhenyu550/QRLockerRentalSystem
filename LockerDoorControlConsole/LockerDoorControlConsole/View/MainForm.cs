using LockerDoorControlConsole.Controller;
using LockerDoorControlConsole.View;
using LockerDoorControlConsole.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using LockerDoorControlConsole.Model;

namespace LockerDoorControlConsole
{
    public partial class MainForm : Form
    {
        // Private variables
        private LockerDoorController _lockerDoorController = new LockerDoorController();
        private List<Locker> _lockers = new List<Locker>();
        private bool _isLoaded = false;

        public MainForm()
        {
            InitializeComponent();
            LoadMainForm();
            if (_isLoaded)
            {
                // Start the Current Date Time Timer
                timerCurrentDateTime.Start();

                // Start the Auto Refresh Timer
                timerAutoRefresh.Start();

                try
                {
                    _lockerDoorController.SetArduinoPort();
                }
                catch (InvalidArduinoConnectionException exception)
                {
                    exception.ShowErrorMessage();
                }

                // Load the list of lockers
                ReloadLockerTable();

                // Set the cursor to QR Code Input TextBox using Focus
                textBoxQRInput.Focus();
            }
        }

        // Methods
        public void LoadMainForm()
        {
            DatabaseController dbController = new DatabaseController();

            //  Check the DbConfig file existence and determine the next operation
            if (!dbController.DbConfigExists())
            {
                //  Set the Master Password
                MasterPasswordForm newMasterPasswordForm = new MasterPasswordForm();
                newMasterPasswordForm.ShowDialog();

                //  Exit the program if user exit the Master Password Initial Setup, else Load Database Connection Form
                if (!newMasterPasswordForm.PwSetted)
                {
                    this.Close();
                }
                else
                {
                    //  Set the Database Connection
                    DatabaseConnectionForm dbConForm = new DatabaseConnectionForm(newMasterPasswordForm.MasterPw, false);
                    dbConForm.ShowDialog();

                    //  Exit the program if user exit the Database Connection Initial Setup
                    if (!dbConForm.Connected)
                    {
                        MessageBox.Show("Connection Error: Database not connected." +
                            "\nThere is no database to be connected. The program will exit now.", "Connection Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    else
                    {
                        //  Set Up the cabinet for this system
                        SetCabinetForm setCabinetForm = new SetCabinetForm(dbConForm.DbContoller);
                        setCabinetForm.ShowDialog();

                        if (!setCabinetForm.IsCabinetSelected())
                        {
                            MessageBox.Show("Initialize Error: No cabinet assigned." +
                                "\nThe system cannot recognize a valid cabinet. The program will exit now.", "Initialize Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                        else
                        {
                            //  The Main Form will be displayed by default if Database Connection Succeed 
                            // Show the Cabinet Code if load successfully
                            labelCabinetCode.Text = setCabinetForm.GetCabinetCode();
                            _isLoaded = true;
                        }
                    }
                }
            }
            else
            {
                dbController.LoadIniFile();
                try
                {
                    dbController.ConnectDatabase();
                    dbController.CheckCabinet();
                }
                catch (InvalidDatabaseConnectionException error)
                {
                    error.ShowErrorMessage();
                }

                if (!dbController.Connected)
                {
                    MasterPasswordForm verifyPasswordForm = new MasterPasswordForm(true, false);
                    verifyPasswordForm.ShowDialog();
                    if (verifyPasswordForm.Verify)
                    {
                        //  Set the Database Connection
                        DatabaseConnectionForm dbConForm = new DatabaseConnectionForm(verifyPasswordForm.MasterPw, true);
                        dbConForm.ShowDialog();

                        //  Exit the program if user exit the Database Connection Initial Setup
                        if (!dbConForm.Connected)
                        {
                            MessageBox.Show("Database Not Connected");
                            this.Close();
                        }
                        else
                        {
                            SetCabinetForm setCabinetForm = new SetCabinetForm(dbConForm.DbContoller);
                            setCabinetForm.ShowDialog();

                            if (!setCabinetForm.IsCabinetSelected())
                            {
                                MessageBox.Show("Initialize Error: No cabinet assigned." +
                                    "\nThe system cannot recognize a valid cabinet. The program will exit now.", "Initialize Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                            else
                            {
                                // Show the Cabinet Code if load successfully
                                labelCabinetCode.Text = dbController.CabinetCode;
                                _isLoaded = true;
                            }
                        }

                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    // Show the Cabinet Code if load successfully
                    labelCabinetCode.Text = dbController.CabinetCode;
                    _isLoaded = true;
                }
            }
        }

        public void ReloadLockerTable()
        {
            // Get Data for all Lockers from the database
            _lockers = _lockerDoorController.GetLockers(labelCabinetCode.Text);

            // Clear the list and renew the list
            listViewLocker.Items.Clear();
            foreach (Locker locker in _lockers)
            {
                ListViewItem lvi = new ListViewItem(locker.Code);
                lvi.SubItems.Add(locker.Status);
                lvi.SubItems.Add(locker.DoorStatus);

                if (locker.IsAvailable())
                {
                    if (locker.IsOpened())
                        lvi.ImageIndex = 4;
                    else
                        lvi.ImageIndex = 0;
                }
                else if (locker.IsOccupied())
                {
                    if (locker.IsOpened())
                        lvi.ImageIndex = 6;
                    else
                        lvi.ImageIndex = 2;
                }
                else if (locker.IsNotAvailable())
                {
                    if (locker.IsOpened())
                        lvi.ImageIndex = 5;
                    else
                        lvi.ImageIndex = 1;
                }
                else
                {
                    // Overdued
                    if (locker.IsOpened())
                        lvi.ImageIndex = 7;
                    else
                        lvi.ImageIndex = 3;
                }

                listViewLocker.Items.Add(lvi);
            }
        }

        // Event Handlers
        private void ButtonScan_Click(object sender, EventArgs e)
        {
            string qrKey = textBoxQRInput.Text;

            try
            {
                _lockerDoorController.CabinetCode = labelCabinetCode.Text;
                _lockerDoorController.ValidateQr(qrKey);

                // Check the key is rental key or master key
                if (!_lockerDoorController.IsMasterKey())
                {
                    // The key is rental key
                    if (_lockerDoorController.Locker.IsLocked())
                    {
                        _lockerDoorController.OpenLockerDoor();

                        // Reload the list
                        ReloadLockerTable();

                        // Calculate the rental remaining time
                        int daysLeft = _lockerDoorController.GetRemainingDays();

                        // Show Door Open Message with Reminder
                        MessageBox.Show(String.Format("Locker {0} Door Opened.", _lockerDoorController.Locker.Code) + 
                            "\n\nReminder: Your rental will expire after " + daysLeft + " day(s). " +
                            "\n\nPlease clear the locker and return the locker before it expires, else the lost of items in the locker " +
                            "after rental expires is not the responsibilty of the management.", "Door Opened", MessageBoxButtons.OK);
                    }
                    else
                    {
                        _lockerDoorController.CloseLockerDoor();

                        // Reload the list
                        ReloadLockerTable();

                        // Calculate the rental remaining time
                        int daysLeft = _lockerDoorController.GetRemainingDays();

                        // Show Door Open Message with Reminder
                        MessageBox.Show(String.Format("Locker {0} Door Locked.", _lockerDoorController.Locker.Code) +
                            "\n\nReminder: Your rental will expire after " + daysLeft + " day(s). " +
                            "\n\nPlease clear the locker and return the locker before it expires, else the lost of items in the locker " +
                            "after rental expires is not the responsibilty of the management.", "Door Locked", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    // The key is master key
                    // Open the Locker Selection List
                    SelectLockerForm selectLockerForm = new SelectLockerForm(labelCabinetCode.Text);
                    selectLockerForm.ShowDialog();

                    // If a locker is selected
                    if (selectLockerForm.IsSelected())
                    {
                        // Set the selected locker
                        _lockerDoorController.Locker = selectLockerForm.SelectedLocker;

                        // Open or Lock the selected Locker
                        if (_lockerDoorController.Locker.IsLocked())
                        {
                            _lockerDoorController.OpenLockerDoor();

                            // Reload the list
                            ReloadLockerTable();
                            MessageBox.Show(String.Format("Locker {0} Door Opened.", _lockerDoorController.Locker.Code),
                                "Door Opened", MessageBoxButtons.OK);
                        }
                        else
                        {
                            _lockerDoorController.CloseLockerDoor();

                            // Reload the list
                            ReloadLockerTable();
                            MessageBox.Show(String.Format("Locker {0} Door Locked.", _lockerDoorController.Locker.Code),
                                "Door Locked", MessageBoxButtons.OK);
                        }
                    }
                }
            }
            catch (InvalidQRCodeException exception)
            {
                exception.ShowErrorMessage();
            }

            // Reload the list of lockers
            ReloadLockerTable();

            // Clear the QR Code Input TextBox
            textBoxQRInput.Clear();

            // Set the cursor to QR Code Input TextBox using Focus
            textBoxQRInput.Focus();
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            ReloadLockerTable();
        }

        // Set and run the current date time timer
        private void TimerCurrentDateTime_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            labelCurrentDate.Text = dateTime.ToString("dddd, dd MMMM yyyy");
            labelCurrentTime.Text = dateTime.ToString("hh:mm:ss tt");
        }

        // Set and run the auto refresh timer
        private void TimerAutoRefresh_Tick(object sender, EventArgs e)
        {
            // Reload the list of lockers every time it ticks
            ReloadLockerTable();
        }

        // Disable item selection on the listView
        private void ListViewUnlockLockers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
                e.Item.Selected = false;
        }

        // Enter KeyPress in textbox trigger click Scan button
        private void TextBoxQRInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonScan.PerformClick();
        }

        // Check if the person exit the program have the master key
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MasterPasswordForm masterPasswordForm = new MasterPasswordForm(false, true);
            masterPasswordForm.ShowDialog();

            if (!masterPasswordForm.Verify)
            {
                MessageBox.Show("Exit Error: Authetication Failed" +
                    "\nNo permission can be granted without the master password verification." +
                    "\nYou need the master password to exit the program.", "Exit Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                // Cancel the exit operation
                e.Cancel = true;
            }
            else
            {
                MessageBox.Show("Exit Success" +
                    "\nThe program will exit now.", "Exit Success", MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }
        }

    }
}
