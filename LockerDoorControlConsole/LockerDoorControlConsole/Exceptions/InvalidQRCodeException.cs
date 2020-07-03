using System;
using System.Windows.Forms;

namespace LockerDoorControlConsole.Exceptions
{
    class InvalidQRCodeException : Exception
    {
        //  Private Attributes
        private readonly string _errorType = "";
        private readonly string _errorValue = "";
        private readonly string _errorLockerCode = "";

        private string _errorHeader = "";
        private string _errorMessage = "";

        //  Constructor for the Master Password Exception
        public InvalidQRCodeException(string errorType)
        {
            _errorType = errorType;
        }

        public InvalidQRCodeException(string errorType, string errorValue, string errorLockerCode)
        {
            _errorType = errorType;
            _errorValue = errorValue;
            _errorLockerCode = errorLockerCode;
        }


        //  Function to display the error message.
        public void ShowErrorMessage()
        {
            //  Determine the content of Message to be displayed using the error type
            switch (_errorType)
            {
                case "Invalid QR":
                    _errorHeader = "Access Error";
                    _errorMessage = "Access Error: Invalid QR Code." +
                        "\nThis is not a valid QR Code for this system." +
                        "Please scan a valid QR Code that contains rental information.";
                    break;

                case "Ended QR":
                    _errorHeader = "Access Error";
                    _errorMessage = "Access Error: Expired QR Code." +
                        "\nThis QR Code already expires due to rental ends." +
                        "\nPlease proceed to the counter for more details.";
                    break;

                case "Overdue QR":
                    _errorHeader = "Access Error";
                    _errorMessage = "Access Error: Expired QR Code." +
                        "\n\nThis QR Code already expires due to rental overdues." +
                        "\n\nLocker " + _errorLockerCode + " already overdue for " + _errorValue + " days." +
                        "\n\nPlease proceed to the counter for more details.";
                    break;

                case "Not Started QR":
                    _errorHeader = "Access Error";
                    _errorMessage = "Access Error: Not Activated QR Code." +
                        "\nThis QR Code is not activated as the rental has not started." +
                        "\nPlease use this QR Code only when the rental starts.";
                    break;

                case "Incorrect Cabinet":
                    _errorHeader = "Access Error";
                    _errorMessage = "Access Error: Incorrect Cabinet." +
                        "\nThis QR Code is valid but it does not belong to this cabinet." +
                        "\nPlease scan the QR code to its corresponding cabinet.";
                    break;

                case "Deactivated Master QR":
                    _errorHeader = "Access Error";
                    _errorMessage = "Access Error: Master Key deactivated." +
                        "\nThis master key is deactivated due to the account is being deleted." +
                        "\nPlease find the adminstrator for more details.";
                    break;

                case "Inactive Master QR":
                    _errorHeader = "Access Error";
                    _errorMessage = "Access Error: Master Key not activated." +
                        "\nThis master key is not activated as the account is in default status." +
                        "\nPlease login your account using the Locker Rental Management System to activate" +
                        " your master key.";
                    break;
            }

            //  Display the Error Message
            MessageBox.Show(_errorMessage, _errorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
