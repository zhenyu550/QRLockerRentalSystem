using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockerDoorControlConsole.Exceptions
{
    public class InvalidCabinetException : Exception
    {
        private readonly string _errorType;
        private string _errorMessage = "";
        private string _errorHeader = "";
        private string _errorValue = "";

        // Constructor for the Database Connection Exception 
        public InvalidCabinetException(string errorType, string errorValue)
        {
            _errorType = errorType;
            _errorValue = errorValue;
        }

        //  Function to display the error message.
        public void ShowErrorMessage()
        {
            //  Determine the content of Message to be displayed using the error type
            switch (_errorType)
            {
                case "Invalid Cabinet":
                    _errorHeader = "Initialize Error";
                    _errorMessage = "Initialize Error: Invalid Cabinet." +
                        "\nThere is no cabinet with code " + _errorValue + " in the database.";
                    break;
            }

            //  Display the Error Message
            MessageBox.Show(_errorMessage, _errorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

