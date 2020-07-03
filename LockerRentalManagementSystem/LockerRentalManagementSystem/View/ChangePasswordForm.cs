using LockerRentalManagementSystem.Controller;
using LockerRentalManagementSystem.Core;
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
    public partial class ChangePasswordForm : Form
    {
        // Private attributes
        Employee _employee = new Employee();
        private bool _isFirstLogin = false;
        private bool _isPasswordChanged = false;

        // Getter & Setters
        public bool IsPasswordChanged { get { return _isPasswordChanged; } }

        // Constructor
        public ChangePasswordForm(bool isFirstLogin, Employee employee)
        {
            InitializeComponent();
            _employee = employee;
            _isFirstLogin = isFirstLogin;
        }

        // Event Handlers
        // Confirm Button Event Handler
        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            // Validate the passwords
            ChangePasswordController changePasswordController = new ChangePasswordController(_employee);
            try
            {
                changePasswordController.ValidatePassword(textBoxOldPassword.Text, textBoxNewPassword.Text, textBoxReconfrimPassword.Text);
                changePasswordController.ChangePassword();
                if (_isFirstLogin)
                {
                    changePasswordController.ActivateAccount();
                }
                _isPasswordChanged = true;
                this.Close();

            }
            catch (InvalidChangePasswordException exception)
            {
                exception.ShowErrorMessage();
            }
        }
        
        // Cancel Button Event Handler
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (_isFirstLogin)
            {
                var result = MessageBox.Show("Warning: First Time Login." +
                    "\nYour account is in Default mode with the Default password." +
                    "\n\nDo you really want to cancel the change password operation? " +
                    "Doing so will logout from the system.","Warning: First Time Login",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    _isPasswordChanged = false;
                    this.Close();
                }
            }
            else
                this.Close();
        }
    }
}
