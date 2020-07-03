using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockerDoorControlConsole.Exceptions
{
    public class InvalidArduinoConnectionException : Exception
    {
        private readonly string _errorType;
        private string _errorMessage = "";
        private string _errorHeader = "";

        // Constructor for the Database Connection Exception 
        public InvalidArduinoConnectionException(string errorType)
        {
            _errorType = errorType;
        }

        //  Function to display the error message.
        public void ShowErrorMessage()
        {
            //  Determine the content of Message to be displayed using the error type
            switch (_errorType)
            {
                case "Arduino Connect Fail":
                    _errorHeader = "Connection Error";
                    _errorMessage = "Connection Error: Fail to connect Arduino." +
                        "\nPlease connect this device to the QR Locker's Arduino hardware.";
                    break;
            }

            //  Display the Error Message
            MessageBox.Show(_errorMessage, _errorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
