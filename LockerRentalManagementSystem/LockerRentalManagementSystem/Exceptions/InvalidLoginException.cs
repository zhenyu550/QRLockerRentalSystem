using System;
using System.Windows.Forms;

namespace LockerRentalManagementSystem.Exceptions
{
    class InvalidLoginException : Exception
    {
        private readonly string _errorType;
        private string _errorMessage = "";
        private string _errorHeader = "";

        // Constructor for the Database Connection Exception 
        public InvalidLoginException(string errorType)
        {
            _errorType = errorType;
        }

        //  Function to display the error message.
        public void ShowErrorMessage()
        {
            //  Determine the content of Message to be displayed using the error type
            switch (_errorType)
            {
                case "Incorrect Username":
                    _errorHeader = "Login Error";
                    _errorMessage = "Login Error: Incorrect Username." +
                        "\nThis username is not in the database.\nPlease enter a valid username.";
                    break;

                case "Incorrect Password":
                    _errorHeader = "Login Error";
                    _errorMessage = "Login Error: Incorrect Password." +
                        "\nPlease ensure you have entered the correct password for this user.";
                    break;

                case "Disabled Account":
                    _errorHeader = "Login Error";
                    _errorMessage = "Login Error: Account Disabled." +
                        "\nThis account is disabled.\nPlease contact the adminstrator for more information.";
                    break;
            }

            //  Display the Error Message
            MessageBox.Show(_errorMessage, _errorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
