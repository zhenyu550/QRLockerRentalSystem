using System;
using System.Collections.Generic;
using LockerRentalManagementSystem.Core;
using MySql.Data.MySqlClient;


namespace LockerRentalManagementSystem.Model
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
            if(!String.IsNullOrWhiteSpace(returnDateTimeString))
                _returnDateTime = DateTime.Parse(returnDateTimeString);
        }

        public void Save()
        {
            if (_id == 0)
            {
                Insert();
            }
            else
            {
                Update();
            }
        }
        
        public void Insert()
        {
            string startDate = _startDate.ToString("yyyy-MM-dd");
            string endDate = _endDate.ToString("yyyy-MM-dd");
            string bookingDateTime = _bookingDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "INSERT INTO {0} (code, booking_date_time, start_date, end_date, duration, rental_key, customer_id, locker_id, employee_id, status) " +
                "VALUES ('{1}', '{2}', '{3}', '{4}', {5}, '{6}', {7}, {8}, {9}, '{10}')";
            query = String.Format(query, TableName, _code, bookingDateTime, startDate, endDate, _duration, _key, _customerId, _lockerId, _employeeId, _status);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
        }

        public void Update()
        {
            string startDate = _startDate.ToString("yyyy-MM-dd");
            string endDate = _endDate.ToString("yyyy-MM-dd");
            string bookingDateTime = _bookingDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            string returnDateTime = _returnDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            string query = "UPDATE {0} SET code = '{1}', booking_date_time = '{2}', start_date = '{3}', end_date = '{4}', duration = {5}, " +
                "rental_key = '{6}', customer_id = {7}, locker_id = {8}, employee_id = {9}, status = '{10}', " +
                "return_date_time = '{11}' WHERE id = {12}";
            query = String.Format(query, TableName, _code, bookingDateTime, startDate, endDate, _duration, _key, 
                _customerId, _lockerId, _employeeId, _status, returnDateTime, _id);
            MySqlCommand command = new MySqlCommand(query, Database.Connection);
            command.ExecuteNonQuery();
        }

        public void Delete()
        {
            string query = String.Format("DELETE FROM {0} WHERE id = {1}", TableName, _id);
            MySqlCommand command = new MySqlCommand(query, Database.Connection);
            command.ExecuteNonQuery();
            _id = 0;
        }

        public void Overdue()
        {
            string query = "UPDATE {0} SET status = 'Overdue' WHERE id = {1}";
            query = String.Format(query, TableName, _id);
            MySqlCommand command = new MySqlCommand(query, Database.Connection);
            command.ExecuteNonQuery();
        }

        public void Start()
        {
            string query = "UPDATE {0} SET status = 'Started' WHERE id = {1}";
            query = String.Format(query, TableName, _id);
            MySqlCommand command = new MySqlCommand(query, Database.Connection);
            command.ExecuteNonQuery();
        }

        public void End()
        {
            string returnDateTime = _returnDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            string query = "UPDATE {0} SET return_date_time = '{1}', status = 'Ended' WHERE id = {2}";
            query = String.Format(query, TableName, returnDateTime, _id);
            MySqlCommand command = new MySqlCommand(query, Database.Connection);
            command.ExecuteNonQuery();
        }

        public void ChangeLocker()
        {
            string query = "UPDATE {0} SET locker_id = {1} WHERE id = {2}";
            query = String.Format(query, TableName, _lockerId, _id);
            MySqlCommand command = new MySqlCommand(query, Database.Connection);
            command.ExecuteNonQuery();
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
