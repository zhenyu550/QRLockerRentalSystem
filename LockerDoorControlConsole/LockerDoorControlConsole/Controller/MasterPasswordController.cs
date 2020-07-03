/// <summary>
/// The class that manages and controls all functions related to the Master Password.
/// </summary>

using LockerDoorControlConsole.Core;
using LockerDoorControlConsole.Exceptions;
using System;

namespace LockerDoorControlConsole.Controller
{
    class MasterPasswordController
    {
        //  Function to check the inputs are equal or not
        public void CheckInputIsSame(string input1, string input2)
        {
            if (!input1.Equals(input2))
                throw new InvalidMasterPasswordException("Inputs not equal");

            if (String.IsNullOrWhiteSpace(input1))
                throw new InvalidMasterPasswordException("Empty password");
        }

        //  Function to hash the master password
        public string HashMasterPassword(string input)
        {
            string hashedMasterPw = Security.SHA256Hash(input);
            hashedMasterPw = Security.SHA256Hash(input + hashedMasterPw);   //  Hash password again for additional security

            return hashedMasterPw;
        }

        //  Function to verify the master password
        public void VerifyMasterPassword(string input)
        {
            //  Get the correct master password from DbConfig
            DatabaseController dbController = new DatabaseController();
            dbController.LoadIniFile();

            if (!input.Equals(dbController.MasterPw))
                throw new InvalidMasterPasswordException("Verify fail");
        }

    }
}
