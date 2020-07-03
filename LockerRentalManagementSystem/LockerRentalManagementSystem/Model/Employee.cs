using System;
using System.Collections.Generic;
using LockerRentalManagementSystem.Core;
using MySql.Data.MySqlClient;

namespace LockerRentalManagementSystem.Model
{
    public class Employee
    {
        //Attributes

        private int _id;
        private string _username;
        private string _password;
        private string _permission;
        private string _name;
        private string _icPassport;
        private string _gender;
        private string _position;
        private string _phoneNo;
        private string _email;
        private string _address;
        private string _masterKey;
        private string _status;

        //Getters and Setters

        public int Id { get { return _id; } set { _id = value; } }
        public string Username { get { return _username; } set { _username = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public string Permission { get { return _permission; } set { _permission = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string IcPassport { get { return _icPassport; } set { _icPassport = value; } }
        public string Gender { get { return _gender; } set { _gender = value; } }
        public string Position { get { return _position; } set { _position = value; } }
        public string PhoneNo { get { return _phoneNo; } set { _phoneNo = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Address { get { return _address; } set { _address = value; } }
        public string MasterKey { get { return _masterKey; } set { _masterKey = value; } }
        public string Status { get { return _status; } set { _status = value; } }

        //Constants

        const string TableName = "employee";

        //Constructors

        public Employee()
        {
            _id = 0;
            _permission = "Default";
        }

        public Employee(MySqlDataReader dataReader)
        {
            Set(dataReader);
        }

        //Static Methods

        public static List<Employee> All(int count, int offset)
        {
            List<Employee> list = new List<Employee>();
            string query = String.Format("SELECT * FROM {0} ORDER BY id ASC LIMIT {1}, {2};", TableName, count, offset);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                list.Add(new Employee(dataReader));
            }
            dataReader.Close();
            return list;
        }

        public static List<Employee> Where(string condition, int count, int offset)
        {
            List<Employee> list = new List<Employee>();
            string query = String.Format("SELECT * FROM {0} WHERE {1} ORDER BY id ASC LIMIT {2}, {3}", TableName,
                condition, count, offset);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                list.Add(new Employee(dataReader));
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

        public static Employee Get(int id)
        {
            Employee item = null;
            string query = String.Format("SELECT * FROM {0} WHERE id = {1}", TableName, id);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                item = new Employee(dataReader);
            }
            dataReader.Close();
            return item;
        }

        //Instance Methods - MySQL Related

        public void Set(MySqlDataReader dataReader)
        {
            _id = Convert.ToInt32(dataReader["id"] + "");
            _username = dataReader["username"] + "";
            _password = dataReader["password"] + "";
            _permission = dataReader["permission"] + "";
            _name = dataReader["name"] + "";
            _icPassport = dataReader["ic_passport"] + "";
            _gender = dataReader["gender"] + "";
            _position = dataReader["position"] + "";
            _phoneNo = dataReader["phone_no"] + "";
            _email = dataReader["email"] + "";
            _address = dataReader["home_address"] + "";
            _masterKey = dataReader["master_key"] + "";
            _status = dataReader["status"] + "";
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
            string query = "INSERT INTO {0} (username, password, permission, name, ic_passport, gender, position, " +
                "phone_no, email, home_address, master_key)" +
                " VALUES ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}','{8}','{9}','{10}', '{11}')";
            query = String.Format(query, TableName, _username, _password, _permission, _name, _icPassport, _gender, _position,
                _phoneNo, _email, _address, _masterKey);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
        }

        public void Update()
        {
            string query = "UPDATE {0} SET username = '{1}', password = '{2}', permission = '{3}', name = '{4}', " +
                "ic_passport = '{5}', gender = '{6}', position = '{7}', phone_no='{8}', email = '{9}', " +
                "home_address = '{10}' WHERE id = {11}";
            query = String.Format(query, TableName, _username, _password, _permission, _name, _icPassport, _gender, _position,
                _phoneNo, _email, _address, _id);
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

        public void TempDelete()
        {
            string query = "UPDATE {0} SET status = '{1}' WHERE id = {2}";
            query = string.Format(query, TableName, "Disabled", _id);
            MySqlCommand command = new MySqlCommand(query, Database.Connection);
            command.ExecuteNonQuery();
        }

        public void Restore()
        {
            string query = "UPDATE {0} SET status = '{1}' WHERE id = {2}";
            query = string.Format(query, TableName, "Active", _id);
            MySqlCommand command = new MySqlCommand(query, Database.Connection);
            command.ExecuteNonQuery();
        }

        //Instance Methods - Functional 

        public bool IsAdmin()
        {
            return (Permission == "Admin");
        }

        public bool IsDisabled()
        {
            return (Status == "Disabled");
        }

        public bool IsDefault()
        {
            return (Status == "Default");
        }
    }
}
