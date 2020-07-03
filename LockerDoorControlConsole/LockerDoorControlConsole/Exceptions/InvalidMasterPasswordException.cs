using System;
using System.Windows.Forms;

namespace LockerDoorControlConsole.Exceptions
{
    class InvalidMasterPasswordException : Exception
    {
        private readonly string _errorType = "";
        private string _errorHeader = "";
        private string _errorMessage = "";

        //  Constructor for the Master Password Exception
        public InvalidMasterPasswordException(string errorType)
        {
            _errorType = errorType;
        }

        //  Function to display the error message.
        public void ShowErrorMessage()
        {
            //  Determine the content of Message to be displayed using the error type
            switch (_errorType)
            {
                case "Inputs not equal":
                    _errorHeader = "Validation Error";
                    _errorMessage = "Validation Error: Password inputs are not equal." +
                        "\nPlease ensure that password and confirm password are the same.";
                    break;

                case "Empty password":
                    _errorHeader = "Validation Error";
                    _errorMessage = "Validation Error: Empty password." +
                        "\nYou must set a master password.";
                    break;

                case "Verify fail":
                    _errorHeader = "Verfication Error";
                    _errorMessage = "Verification Error: Incorrect Master Password." +
                        "\nPlease enter the correct master password and try again.";
                    break;
            }

            //  Display the Error Message
            MessageBox.Show(_errorMessage, _errorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
