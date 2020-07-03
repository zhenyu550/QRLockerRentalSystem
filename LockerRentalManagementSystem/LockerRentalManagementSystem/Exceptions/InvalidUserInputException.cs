using System;
using System.Windows.Forms;

namespace LockerRentalManagementSystem.Exceptions
{
    public class InvalidUserInputException : Exception
    {
        //  Read Only Static Variables (Set by Constructor)
        private readonly string _errorType = "";
        private readonly string _errorObject = "";
        private readonly string _errorTable = "";
        private readonly string _errorValue = "";

        //  Private Variables
        private string _errorHeader = "";
        private string _errorMessage = "";

        //  Constructors
        public InvalidUserInputException(string errorType)
        {
            _errorType = errorType;
        }

        public InvalidUserInputException(string errorType, string errorObject, string errorValue, string errorTable)
        {
            _errorType = errorType;
            _errorObject = errorObject;
            _errorValue = errorValue;
            _errorTable = errorTable;
        }

        //  Function to display the error message.
        public void ShowErrorMessage()
        {
            //  Determine the content of Message to be displayed using the error type
            switch (_errorType)
            {
                //  General Exceptions
                case "Empty Field":
                    _errorHeader = "Input Error";
                    _errorMessage = "Input Error: Empty Field(s) Detected!" +
                        "\nPlease ensure that all fields are filled. ";
                    break;

                case "Duplicate Detected":
                    _errorHeader = "Input Error";
                    _errorMessage = "Input Error: Duplicate attribute detected!\n" +
                        _errorObject + " '" + _errorValue + "' already exists in the "+ _errorTable + " table.";
                    break;

                case "Export Fail":
                    _errorHeader = "Export Error";
                    _errorMessage = "Export Error: Export file not saved.\n" +
                        "You cannot export " + _errorTable + " and delete it from the database without saving the exported file.";
                    break;

                case "Empty Records":
                    _errorHeader = "Export Error";
                    _errorMessage = "Export Error: Empty " + _errorTable +" records.\n" +
                        "There was no deleted " + _errorTable + " to export.";
                    break;

                // Customer Exceptions
                case "Invalid ComboBox Input - Customer":
                    _errorHeader = "Input Error";
                    _errorMessage = "Input Error: Invalid input(s) Detected!" +
                        "\nPlease ensure that field 'Gender' are filled with provided items.";
                    break;

                case "Delete Error - Customer Rental":
                    _errorHeader = "";
                    _errorMessage = "Deletion Error: Rental detected." +
                        "To delete this customer, please ensure that all rentals assoicated to this customer was ended.";
                    break;

                // Employee Exceptions
                case "No Admin":
                    _errorHeader = "Initialization Error";
                    _errorMessage = "Initialization Error: No Admin Account in Employee Table!" + 
                        "You must create an admin account to continue.";
                    break;

                case "Invalid ComboBox Input - Employee":
                    _errorHeader = "Input Error";
                    _errorMessage = "Input Error: Invalid input(s) Detected!" +
                        "\nPlease ensure that fields 'Gender', 'Account Permission' and 'Position' " +
                        "are filled with provided items.";
                    break;

                case "Last Admin":
                    _errorHeader = "Edit Error";
                    _errorMessage = "Edit Error: Only one admin account." + "\nThis is the only admin account in the database." +
                        "\nYou must have one admin account in this database.";
                    break;

                case "Delete Error - Employee Rental":
                    _errorHeader = "Delete Error";
                    _errorMessage = "Delete Error: Rental detected." +
                        "\nTo delete this employee, please ensure that all rentals managed by this employee was ended.";
                    break;

                case "Last Admin - Delete":
                    _errorHeader = "Delete Error";
                    _errorMessage = "Delete Error: Only one admin account." + "\nThis is the only admin account in the database." +
                        "\nYou must have one admin account in this database.";
                    break;

                // Locker Type Exception
                case "Delete Error - Locker Type Cabinet":
                    _errorHeader = "Delete Error";
                    _errorMessage = "Delete Error: Cabinets detected." +
                        "\nTo delete this locker type, please delete all cabinet(s) assoicated to it.";
                    break;

                case "Export Fail - Cabinet":
                    _errorHeader = "Export Error";
                    _errorMessage = "Export Error: Cabinet detected." +
                        "\nYou cannot export this locker type from the database with cabinet(s) assoicated to it exist in the database. " +
                        "\nTo export this locker type, please export all cabinet(s) assoicated to it from the database.";
                    break;

                // Cabinet Exceptions
                case "Delete Error - Cabinet Locker Booked":
                    _errorHeader = "Delete Error";
                    _errorMessage = "Delete Error: Booked Lockers detected." +
                        "\nTo delete this cabinet, please ensure that all lockers assoicated to this cabinet were not booked in any rental.";
                    break;

                case "Restore Error - Cabinet Locker Type":
                    _errorHeader = "Restore Error";
                    _errorMessage = "Restore Error: Locker Type disabled." +
                        "To restore this cabinet, please ensure that the locker type '" + _errorValue + 
                        "is restored.";
                    break;

                // Locker Exceptions
                case "Disable Error - Locker Disabled":
                    _errorHeader = "Disable Error";
                    _errorMessage = "Disable Error: Locker is disabled. \nThis locker was already disabled.";
                    break;

                case "Disable Error - Locker Booked":
                    _errorHeader = "Disable Error";
                    _errorMessage = "Disable Error: Locker is booked or occupied." +
                        "\nYou cannot disable an booked or occupied locker. " +
                        "\nTo disable this locker, please change the locker of the rental bookings related to it" +
                        " or end the rentals related to it.";
                    break;

                case "Reset Error - Locker Occupied":
                    _errorHeader = "Reset Error";
                    _errorMessage = "Reset Error: Locker is occupied or overdued." +
                        "\nPlease end the rental related to the locker if you want to make it available for rental booking.";
                    break;


                // Rental Exceptions
                case "Insufficient Payment":
                    _errorHeader = "Payment Error";
                    _errorMessage = "Payment Error: Insufficient Payment Amount." + 
                        "\nPlease ensure the payment amount equals or more than the total price of the rental.";
                    break;

                case "Invalid Duration":
                    _errorHeader = "Input Error";
                    _errorMessage = "Input Error: Invalid Duration." +
                        "\nThe value of duration must be larger than 0.";
                    break;

                case "Empty Customer":
                    _errorHeader = "Input Error";
                    _errorMessage = "Input Error: No Customer Selected." +
                        "\nPlease ensure you have selected a customer.";
                    break;

                case "Empty Locker":
                    _errorHeader = "Input Error";
                    _errorMessage = "Input Error: No Locker Selected." +
                        "\nPlease ensure you have selected a locker.";
                    break;

                case "Reserved Timeslot":
                    _errorHeader = "Input Error";
                    _errorMessage = "Input Error: Time Period Overlaps." +
                        "\nThis locker will be occupied by other rental during the setted time peroid. " +
                        "\nPlease change to another locker or change the rental time period.";
                    break;

                case "Invalid From Until Date":
                    _errorHeader = "Input Error";
                    _errorMessage = "Input Error: Invalid Date Range." +
                        "\nThe Date of 'From' should not be later than the date of 'Until'. " +
                        "\nPlease change to a valid date range.";
                    break;

                case "Export Today Date":
                    _errorHeader = "Input Error";
                    _errorMessage = "Input Error: Today Date." +
                        "\nYou cannot export rental that was booked today. " +
                        "\nPlease ensure both dates in 'From' and 'Until' are not today date.";
                    break;
            }

            //  Display the Error Message
            MessageBox.Show(_errorMessage, _errorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
