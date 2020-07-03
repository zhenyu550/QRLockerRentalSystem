/// <summary>
/// The class that manages and controls all functions for Database Connection.
/// </summary>

using LockerDoorControlConsole.Core;
using LockerDoorControlConsole.Exceptions;
using LockerDoorControlConsole.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LockerDoorControlConsole.Controller
{
    public class DatabaseController
    {
        //  Private Database Attributes
        private string _server;
        private string _port;
        private string _uid;
        private string _pw;
        private string _db;
        private string _cabinetCode;
        private bool _connected;
        private bool _dbChanged;

        //  Private Security Attributes
        private string _masterPw;

        //  Getters & Setters for the Attributes
        public string Server { get { return _server; } set { _server = value; } }
        public string Port { get { return _port; } set { _port = value; } }
        public string Uid { get { return _uid; } set { _uid = value; } }
        public string Pw { get { return _pw; } set { _pw = value; } }
        public string Db { get { return _db; } set { _db = value; } }
        public string CabinetCode { get { return _cabinetCode; } set { _cabinetCode = value; } }
        public bool Connected { get { return _connected; }  }
        public bool DbChanged { get { return _dbChanged; } }
        public string MasterPw { get { return _masterPw; } set { _masterPw = value; } }

        //  Function to start Connect to the database
        public void ConnectDatabase()
        {
            Database.Initialize(_server, _port, _uid, _pw, _db);
            if (Database.Connect())
            {
                if (!Database.TableExists("rental") || !Database.TableExists("locker") || !Database.TableExists("cabinet"))
                {
                    _connected = false;
                    throw new InvalidDatabaseConnectionException("Invalid table");
                }
                else
                {
                    _connected = true;
                    _dbChanged = true;
                }
            }
            else
            {
                _connected = false;
                throw new InvalidDatabaseConnectionException("Invalid database");
            }
        }

        //  Function to check the existence of the Database Configuration File
        public bool DbConfigExists()
        {
            if (!File.Exists("DbConfig.ini"))
                return false;
            else
                return true;
        }

        //  Function to read the Database Configuration file
        public void LoadIniFile()
        {
            var LoadIni = new INIFile("DbConfig.ini");

            //  Load all the encypted attributes from the config file
            string encryptedServer = LoadIni.Read("server");
            string encryptedPort = LoadIni.Read("port");
            string encryptedUid = LoadIni.Read("uid");
            string encryptedPw = LoadIni.Read("password");
            string encryptedDb = LoadIni.Read("database");
            string encryptedcabinet = LoadIni.Read("cabinet");

            //  Decrypt all the attributes readed form the config file
            _server = Security.EncryptDecrypt(encryptedServer, 20);
            _port = Security.EncryptDecrypt(encryptedPort, 35);
            _uid = Security.EncryptDecrypt(encryptedUid, 48);
            _pw = Security.EncryptDecrypt(encryptedPw, 87);
            _db = Security.EncryptDecrypt(encryptedDb, 94);
            _cabinetCode = Security.EncryptDecrypt(encryptedcabinet, 102);

            //  Load the encrypted master password from the config file
            _masterPw = LoadIni.Read("mpw");
        }

        //  Function to Save the Database Configuration file
        public void SaveIniFile()
        {
            //  Encrypt all the attributes
            string encryptedServer = Security.EncryptDecrypt(_server, 20);
            string encryptedPort = Security.EncryptDecrypt(_port, 35);
            string encryptedUid = Security.EncryptDecrypt(_uid, 48);
            string encryptedPw = Security.EncryptDecrypt(_pw, 87);
            string encryptedDb = Security.EncryptDecrypt(_db, 94);
            string encryptedCabinet = Security.EncryptDecrypt(_cabinetCode, 102);

            //  Save the encrypted attributes into the config file
            var newIni = new INIFile("DbConfig.ini");
            newIni.Write("server", encryptedServer);
            newIni.Write("port", encryptedPort);
            newIni.Write("uid", encryptedUid);
            newIni.Write("password", encryptedPw);
            newIni.Write("database", encryptedDb);
            newIni.Write("cabinet", encryptedCabinet);
            newIni.Write("mpw", _masterPw);

        }

        // Function to verify cabinet code
        public void CheckCabinet()
        {
            if (Cabinet.Count(String.Format("code = '{0}'", _cabinetCode)) <= 0)
                throw new InvalidCabinetException("Invalid Cabinet", _cabinetCode);
        }

        // Function to get cabinet list from database
        public List<Cabinet> GetAllCabinets()
        {
            List<Cabinet> cabinets = new List<Cabinet>();

            cabinets = Cabinet.All(0, 2147483467);

            return cabinets;
        }
    }
}
