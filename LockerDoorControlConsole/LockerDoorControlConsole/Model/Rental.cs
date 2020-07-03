/// <summary>
/// The class that holds the attributes and Database SQL for the Rental
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
    public class Rental
    {
        //Attributes

        private int _id;
        private string _code;
        private DateTime _bookingDateTime;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _duration;
        private DateTime _returnDateTime;
        private int _customerId;
        private int _lockerId;
        private int _employeeId;
        private string _key;
        private string _status;

        //Getters and Setters

        public int Id { get { return _id; } set { _id = value; } }
        public string Code { get { return _code; } set { _code = value; } }
        public DateTime BookingDateTime { get { return _bookingDateTime; } set { _bookingDateTime = value; } }
        public DateTime StartDate { get { return _startDate; } set { _startDate = value; } }
        public DateTime EndDate { get { return _endDate; } set { _endDate = value; } }
        public int Duration { get { return _duration; } set { _duration = value; } }
        public DateTime ReturnDateTime { get { return _returnDateTime; } set { _returnDateTime = value; } }
        public int CustomerId { get { return _customerId; } set { _customerId = value; } }
        public int LockerId { get { return _lockerId; } set { _lockerId = value; } }
        public int EmployeeId { get { return _employeeId; } set { _employeeId = value; } }
        public string Key { get { return _key; } set { _key = value; } }
        public string Status { get { return _status; } set { _status = value; } }

        //Constants

        const string TableName = "rental";

        //Constructors

        public Rental()
        {
            _id = 0;
        }

        public Rental(MySqlDataReader dataReader)
        {
            Set(dataReader);
        }

        //Static Methods

        public static List<Rental> All(int count, int offset)
        {
            List<Rental> list = new List<Rental>();
            string query = String.Format("SELECT * FROM {0} ORDER BY id ASC LIMIT {1}, {2};", TableName, count, offset);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                list.Add(new Rental(dataReader));
            }
            dataReader.Close();
            return list;
        }

        public static List<Rental> Where(string condition, int count, int offset)
        {
            List<Rental> list = new List<Rental>();
            string query = String.Format("SELECT * FROM {0} WHERE {1} ORDER BY id ASC LIMIT {2}, {3}", TableName,
                condition, count, offset);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                list.Add(new Rental(dataReader));
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

        public static Rental Get(int id)
        {
            Rental item = null;
            string query = String.Format("SELECT * FROM {0} WHERE id = {1}", TableName, id);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                item = new Rental(dataReader);
            }
            dataReader.Close();
            return item;
        }

        //Instance Methods - MySQL Related

        public void Set(MySqlDataReader dataReader)
        {
            _id = Convert.ToInt32(dataReader["id"] + "");
            _code = dataReader["code"] + "";
            _bookingDateTime = DateTime.Parse(dataReader["booking_date_time"] + "");
            _startDate = DateTime.Parse(dataReader["start_date"] + "");
            _endDate = DateTime.Parse(dataReader["end_date"] + "");
            _duration = Convert.ToInt32(dataReader["duration"] + "");
            _customerId = Convert.ToInt32(dataReader["customer_id"] + "");
            _lockerId = Convert.ToInt32(dataReader["locker_id"] + "");
            _employeeId = Convert.ToInt32(dataReader["employee_id"] + "");
            _key = dataReader["rental_key"] + "";
            _status = dataReader["status"] + "";

            string returnDateTimeString = dataReader["return_date_time"] + "";
            if (!String.IsNullOrWhiteSpace(returnDateTimeString))
                _returnDateTime = DateTime.Parse(returnDateTimeString);
        }

        //Instance Methods - Functional 
        public bool IsNotStarted()
        {
            return (Status == "Not Started");
        }

        public bool IsStarted()
        {
            return (Status == "Started");
        }

        public bool IsOverdue()
        {
            return (Status == "Overdue");
        }

        public bool IsEnded()
        {
            return (Status == "Ended");
        }
    }
}
