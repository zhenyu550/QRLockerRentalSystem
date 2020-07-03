using System;
using System.Windows.Forms;

namespace LockerDoorControlConsole.Exceptions
{
    class InvalidDatabaseConnectionException : Exception
    {
        private readonly string _errorType;
        private string _errorMessage = "";
        private string _errorHeader = "";

        // Constructor for the Database Connection Exception 
        public InvalidDatabaseConnectionException(string errorType)
        {
            _errorType = errorType;
        }

        //  Function to display the error message.
        public void ShowErrorMessage()
        {
            //  Determine the content of Message to be displayed using the error type
            switch (_errorType)
            {
                case "Invalid database" :
                    _errorHeader = "Connection Error";
                    _errorMessage = "Connection Error: Fail to connect Database." +
                        "\nPlease ensure that you have entered the correct database connection details and try again." ;
                    break;

                case "Invalid table":
                    _errorHeader = "Connection Error";
                    _errorMessage = "Connection Error: Required Table(s) not detected." +
                        "\nPlease ensure you have connected to the correct database.";
                    break;
            }

            //  Display the Error Message
            MessageBox.Show(_errorMessage, _errorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
