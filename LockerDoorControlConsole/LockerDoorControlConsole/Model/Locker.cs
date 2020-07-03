/// <summary>
/// The class that holds the attributes and the Database SQL for the Locker
/// </summary>

using LockerDoorControlConsole.Core;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockerDoorControlConsole.Model
{
    public class Locker
    {
        //Attributes

        private int _id;
        private string _code;
        private string _status;
        private string _doorStatus;
        private int _cabinetId;

        //Getters and Setters

        public int Id { get { return _id; } set { _id = value; } }
        public string Code { get { return _code; } set { _code = value; } }
        public string Status { get { return _status; } set { _status = value; } }
        public string DoorStatus { get { return _doorStatus; } set { _doorStatus = value; } }
        public int CabinetId { get { return _cabinetId; } set { _cabinetId = value; } }

        //  Constants
        const string TableName = "Locker";

        //  Constructors

        public Locker()
        {
            _id = 0;
        }

        public Locker(MySqlDataReader dataReader)
        {
            Set(dataReader);
        }

        //  Instance Methods - MySQL Related

        public void Set(MySqlDataReader dataReader)
        {
            _id = Convert.ToInt32(dataReader["id"] + "");
            _code = dataReader["code"] + "";
            _cabinetId = Convert.ToInt32(dataReader["cabinet_id"] + "");
            _doorStatus = dataReader["door_status"] + "";
            _status = dataReader["status"] + "";
        }

        //  Static Methods

        public static List<Locker> Where(string condition, int count, int offset)
        {
            List<Locker> list = new List<Locker>();
            string query = String.Format("SELECT * FROM {0} WHERE {1} ORDER BY id ASC LIMIT {2}, {3}", TableName,
                condition, count, offset);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                list.Add(new Locker(dataReader));
            }
            dataReader.Close();
            return list;
        }

        public static int Count(string condition)
        {
            string query = String.Format("SELECT COUNT(*) FROM {0} WHERE {1}", TableName, condition);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            return int.Parse(cmd.ExecuteScalar().ToString());
        }

        public static Locker Get(int id)
        {
            Locker item = null;
            string query = String.Format("SELECT * FROM {0} WHERE id = {1}", TableName, id);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                item = new Locker(dataReader);
            }
            dataReader.Close();
            return item;
        }

        // Instance Methods
        public void Open()
        {
            string query = "UPDATE {0} SET door_status = '{1}' WHERE id = {2}";
            query = string.Format(query, TableName, "Opened", _id);
            MySqlCommand command = new MySqlCommand(query, Database.Connection);
            command.ExecuteNonQuery();
        }

        public void Lock()
        {
            string query = "UPDATE {0} SET door_status = '{1}' WHERE id = {2}";
            query = string.Format(query, TableName, "Locked", _id);
            MySqlCommand command = new MySqlCommand(query, Database.Connection);
            command.ExecuteNonQuery();
        }

        // Booleans

        public bool IsAvailable()
        {
            return (Status == "Available");
        }

        public bool IsOccupied()
        {
            return (Status == "Occupied");
        }

        public bool IsNotAvailable()
        {
            return (Status == "Not Available");
        }

        public bool IsOverdued()
        {
            return (Status == "Overdue");
        }

        public bool IsDisabled()
        {
            return (Status == "Disabled");
        }

        public bool IsOpened()
        {
            return (DoorStatus == "Opened");
        }

        public bool IsLocked()
        {
            return (DoorStatus == "Locked");
        }
    }
}
