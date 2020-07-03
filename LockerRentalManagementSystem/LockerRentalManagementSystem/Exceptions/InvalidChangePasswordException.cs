using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockerRentalManagementSystem.Exceptions
{
    class InvalidChangePasswordException : Exception
    {
        private readonly string _errorType;
        private string _errorMessage = "";
        private string _errorHeader = "";

        // Constructor for the Database Connection Exception 
        public InvalidChangePasswordException(string errorType)
        {
            _errorType = errorType;
        }

        //  Function to display the error message.
        public void ShowErrorMessage()
        {
            //  Determine the content of Message to be displayed using the error type
            switch (_errorType)
            {
                case "Reconfirm Fail":
                    _errorHeader = "Password Change Error";
                    _errorMessage = "Password Change Error: New Passwords are not same." +
                        "\nPlease ensure that New Password and Confirm New Password are the same.";
                    break;

                case "Incorrrect Old Password":
                    _errorHeader = "Password Change Error";
                    _errorMessage = "Password Change Error: Incorrect Old Password." +
                        "\nPlease ensure you have entered the correct old password for this user.";
                    break;

                case "Old and New Same":
                    _errorHeader = "Password Change Error";
                    _errorMessage = "Password Change Error: Old and New Passwords are same." +
                        "\nYou cannot use your previous password as the new password." +
                        "\nPlease change your password.";
                    break;
            }

            //  Display the Error Message
            MessageBox.Show(_errorMessage, _errorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
