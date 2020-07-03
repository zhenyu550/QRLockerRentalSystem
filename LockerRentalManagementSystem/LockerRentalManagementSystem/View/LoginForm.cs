using LockerRentalManagementSystem.Controller;
using LockerRentalManagementSystem.Exceptions;
using LockerRentalManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockerRentalManagementSystem.View
{
    public partial class LoginForm : Form
    {
        // Data Attributes
        private LoginController _loginController = new LoginController();

        // Constructor
        public LoginForm()
        {
            InitializeComponent();
            InitializeLoginForm();
        }

        // Methods
        // Method to initialize the Login Form
        private void InitializeLoginForm()
        {
            // Create the DB Configuration File if not exist
            if (!_loginController.DbConfigExists())
                _loginController.WriteNewDbConfig();

            // Load the DB Configuration File
            _loginController.LoadIniFile();

            // Load the DB Confuiguration into the Input Fields
            LoadDbConfig();

            if (Validations())
            {
                // Load the Login Page if Database is connected
                labelDBSettingsTitle.Hide();
                this.Controls.Clear();
                this.Controls.Add(panelLogin);
                this.Height = 134;
            }
            else
            {
                // Load Database Settings Page if Database not connected
                this.Controls.Remove(panelLogin);
                this.Height = 202;
            }
        }

        // Method to Validate the connection, database and tables
        private bool Validations()
        {
            if (!_loginController.ValidateConnection())
            {
                MessageBox.Show("Initialization Error: Unable to verify server connection. " +
                    "\nPlease ensure you have entered a valid database connection setting.",
                    "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!_loginController.ValidateDatabase())
            {
                var result = MessageBox.Show("Initialization Error: Database does not exist." +
                    "\nDo you want to create a new database?" +
                    "\nIf you press Yes, a new database with name '" + _loginController.DbName +
                    "' with new tables will be created. ", "Initialization Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    _loginController.CreateDatabase();
                    _loginController.CreateTable();
                    _loginController.CreateAdmin();
                    if (_loginController.ValidateAdminExist())
                        return true;
                }
                return false;
            }
            if (!_loginController.ValidateTable())
            {
                var result = MessageBox.Show("Initialization Error: Database tables corrupted." +
                    "\nDo you want to recreate all tables?" +
                    "\nIf you press Yes, all existing tables will be dropped and new tables will be created." +
                    "\nYou must create an adminstrator account after this.",
                    "Initialization Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    _loginController.DropTable();
                    _loginController.CreateTable();
                    _loginController.CreateAdmin();
                    if (_loginController.ValidateAdminExist())
                        return true;
                }
                return false;
            }
            if (!_loginController.ValidateAdminExist())
            {
                var result = MessageBox.Show("Initialization Error: No Administrator Account." +
                   "\nNo adminstrator account in the Employee table of the database." +
                    "\nYou must create a adminstrator account to login into the database.",
                    "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _loginController.CreateAdmin();
                return _loginController.ValidateAdminExist();
            }
            return true;
        }

        // Method to Load Db Configuraion Settings into the Input Fields
        private void LoadDbConfig()
        {
            textBoxServer.Text = _loginController.Server;
            try
            {
                // Parse the port number into int
                numericUpDownPort.Value = Int32.Parse(_loginController.Port);
            }
            catch(Exception)
            {
                // If the port number is not int, set value as 0
                numericUpDownPort.Value = 0;
            }
            textBoxUID.Text = _loginController.Uid;
            textBoxDbPassword.Text = _loginController.DbPassword;
            textBoxDbName.Text = _loginController.DbName;
        }

        // Event Handlers
        // Save Button Event Handler
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            _loginController.Server = textBoxServer.Text;
            _loginController.Port = numericUpDownPort.Value.ToString();
            _loginController.Uid = textBoxUID.Text;
            _loginController.DbName = textBoxDbName.Text;
            _loginController.DbPassword = textBoxDbPassword.Text;

            // Save the database settings in a Ini file
            _loginController.SaveIniFile();

            // Reload the Login Form for Initalizations and Validations
            LoginForm reloadLoginForm = new LoginForm();
            reloadLoginForm.Show();
            this.Close();
        }

        // Login Button Event Handler
        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Employee employee = _loginController.Login(textBoxUsername.Text.ToLower(), textBoxUserPassword.Text);

                // Notify User to change password if first time login
                if (employee.IsDefault())
                {
                    ChangePasswordForm changePasswordForm = new ChangePasswordForm(true, employee);
                    changePasswordForm.ShowDialog();
                    if (!changePasswordForm.IsPasswordChanged)
                        return;
                }

                // Load the Main Form if no login exception is thrown
                MainForm mainForm = new MainForm();
                mainForm.SetEmployee(employee);
                mainForm.Show();

                // Close Login Form after Main Form loaded
                this.Close();
            }
            catch (InvalidLoginException exception)
            {
                exception.ShowErrorMessage();
            }
        }

        // Extend Db Settings Event Handler
        private void ButtonExtendDBSettings_Click(object sender, EventArgs e)
        {
            if (buttonExtendDBSettings.Text == "+")
            {
                buttonExtendDBSettings.Text = "-";
                this.Controls.Clear();
                this.Controls.Add(panelDBSettings);
                this.Controls.Add(panelLogin);
                this.Height = 297;
            }
            else
            {
                buttonExtendDBSettings.Text = "+";
                this.Controls.Remove(panelDBSettings);
                this.Height = 134;
            }

        }
    }
}
