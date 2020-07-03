using LockerDoorControlConsole.Controller;
using LockerDoorControlConsole.Exceptions;
using System;
using System.Windows.Forms;

namespace LockerDoorControlConsole.View
{
    public partial class MasterPasswordForm : Form
    {
        //  Private attribute 
        private string _masterPw = "";
        private bool _pwSetted;
        private bool _previousConnection;   //  Boolean to indicate any database was connected
        private bool _reconfig;
        private bool _verify = false;

        //  Getters & Setters
        public string MasterPw { get { return _masterPw; } }
        public bool PwSetted { get { return _pwSetted; } }
        public bool PreviousConnection { get { return _previousConnection; } }
        public bool Reconfig { get { return _reconfig; } }
        public bool Verify { get { return _verify; } }

        public MasterPasswordForm()
        {
            InitializeComponent();

            //  Set the Height and Width for this Form
            this.Width = 330;
            this.Height = 190;

            //  Display the Set Password Panel only (Exclude the Tab Control)
            Controls.Remove(tabControl1);
            Controls.Add(panelSetPw);

            //  Set pwSetted as false as this is the first setup
            _pwSetted = false;

            //  Set reconfig as false as this is the first setup
            _reconfig = false;

            //  Set previous Connection as false as this is the first setup
            _previousConnection = false;
        }

        /*
         *  Method override to Verify Password
         */
        public MasterPasswordForm(bool reconfig, bool previousConnection)
        {
            InitializeComponent();

            // Set the Height and Width of this Form
            this.Width = 330;
            this.Height = 160;

            //  Display the Verify Password Panel only (Exclude the Tab Control)
            Controls.Remove(tabControl1);
            Controls.Add(panelVerifyPw);

            _pwSetted = true;
            _reconfig = reconfig;
            _previousConnection = previousConnection;
        }

        private void ButtonConfirmSetPw_Click(object sender, EventArgs e)
        {
            MasterPasswordController mpwController = new MasterPasswordController();
            try
            {
                //  Check if both input for "Password" & "Confirm Password" are the same, if not then error
                mpwController.CheckInputIsSame(textBoxSetPw1.Text, textBoxSetPw2.Text);

                //  Hash the master password
                _masterPw = mpwController.HashMasterPassword(textBoxSetPw1.Text);

                //  Close this form
                _pwSetted = true;
                this.Close();
            }
            catch (InvalidMasterPasswordException error)
            {
                error.ShowErrorMessage();
            }
        }

        private void ButtonConfirmVerifyPw_Click(object sender, EventArgs e)
        {
            MasterPasswordController mpwController = new MasterPasswordController();
            try
            {
                _masterPw = mpwController.HashMasterPassword(textBoxVerifyPw.Text);
                mpwController.VerifyMasterPassword(_masterPw);
                _verify = true;

                //  Bypass the previous connection check as verification succeed
                _previousConnection = true;

                this.Close();

            } catch (InvalidMasterPasswordException error)
            {
                error.ShowErrorMessage();
            }
        }

        private void ButtonCancelVerifyPw_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //  Check master password existance before closing. If NO, show warning message when exit. Exit application if user agrees
        private void MasterPasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_pwSetted)
            {
                var result = MessageBox.Show("Warning: No Master Password." +
                    "\nYou have not setup a master password, doing so will exit the application. Are you sure?", "Warning: No Master Password ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                { e.Cancel = true; }
            }
            if (!_previousConnection && _reconfig)
            {
                var result = MessageBox.Show("Warning: No Database Connection." +
                    "\nYou need to verify the master password to continue the database connection configuration process." +
                    "\nYou have no database connected, doing so will exit the application. Are you sure?", "Warning: No Database Connection ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                { e.Cancel = true; }
            }
        }
    }
}
