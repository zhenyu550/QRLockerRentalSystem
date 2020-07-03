using LockerDoorControlConsole.Controller;
using LockerDoorControlConsole.Exceptions;
using System;
using System.Windows.Forms;

namespace LockerDoorControlConsole.View
{
    public partial class DatabaseConnectionForm : Form
    {
        //  Private Attributes
        private bool _setted;                       //  Boolean to determine the DbConfig file exist or not.
        private string _masterPw = "";              //  Master Password to be stored into the DbConfig file.
        private bool _connected;            //  Boolean to determine the database connection Success or Not.
        private DatabaseController _dbController = new DatabaseController();   //  Database Controller to be shared in this class.

        //  Getter and Setters
        public bool Connected { get { return _connected; } }
        public DatabaseController DbContoller { get { return _dbController; } }

        //  Overloading method for Database Connection Form
        public DatabaseConnectionForm(string masterPw, bool setted)
        {
            InitializeComponent();
            _setted = setted;
            _masterPw = masterPw;
            try
            {
                //  Load the DbConfig file if it exists and load all data into the inout fields
                _dbController = new DatabaseController();
                _dbController.LoadIniFile();
                textBoxServer.Text = _dbController.Server;
                numericUpDownPort.Value = Int32.Parse(_dbController.Port);
                textBoxUid.Text = _dbController.Uid;
                textBoxPassword.Text = _dbController.Pw;
                textBoxDatabase.Text = _dbController.Db;
            }
            catch (Exception)
            {
                //  Clear all data if DbConfig file is corrupted
                textBoxServer.Text = "";
                numericUpDownPort.Value = 0;
                textBoxUid.Text = "";
                textBoxPassword.Text = "";
                textBoxDatabase.Text = "";

                //  Delcare the database configuration is void
                _setted = false;
            }
            
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            _dbController = new DatabaseController
            {
                Server = textBoxServer.Text,
                Port = numericUpDownPort.Value.ToString(),
                Uid = textBoxUid.Text,
                Pw = textBoxPassword.Text,
                Db = textBoxDatabase.Text,
                MasterPw = _masterPw
            };

            try
            {
                _dbController.ConnectDatabase();
                _connected = true;
                _setted = true;
                this.Close();
            }
            catch (InvalidDatabaseConnectionException error)
            {
                error.ShowErrorMessage();
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //  Check if DbConfig file exists. If no, display waring message when exit. Exit application if user agrees
        private void DatabaseConnectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_setted)
            {
                var result = MessageBox.Show("Warning: No Database Configuration." +
                  "\nYou have not configure the database connection, doing so will exit the application. Are you sure?",
                  "Warning: No Database Configuration ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                { e.Cancel = true; }
            }
        }
    }
}
