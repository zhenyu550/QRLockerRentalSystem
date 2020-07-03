using System;
using System.Collections.Generic;
using LockerRentalManagementSystem.Core;
using MySql.Data.MySqlClient;

namespace LockerRentalManagementSystem.Model
{
    public class Customer
    {
        //Attributes

        private int _id;
        private string _name;
        private string _icPassport;
        private string _gender;
        private string _phoneNo;
        private string _email;
        private string _address;

        //Getters and Setters

        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string IcPassport { get { return _icPassport; } set { _icPassport = value; } }
        public string Gender { get { return _gender; } set { _gender = value; } }
        public string PhoneNo { get { return _phoneNo; } set { _phoneNo = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Address { get { return _address; } set { _address = value; } }

        //Constants

        const string TableName = "customer";

        //Constructors

        public Customer()
        {
            _id = 0;
        }

        public Customer(MySqlDataReader dataReader)
        {
            Set(dataReader);
        }

        //Static Methods

        public static List<Customer> All(int count, int offset)
        {
            List<Customer> list = new List<Customer>();
            string query = String.Format("SELECT * FROM {0} ORDER BY id ASC LIMIT {1}, {2}", TableName, count, offset);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                list.Add(new Customer(dataReader));
            }
            dataReader.Close();
            return list;
        }

        public static List<Customer> Where(string condition, int offset, int count)
        {
            List<Customer> list = new List<Customer>();
            string query = String.Format("SELECT * FROM {0} WHERE {1} ORDER BY id ASC LIMIT {2}, {3}", TableName, condition, offset, count);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                list.Add(new Customer(dataReader));
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

        public static Customer Get(int id)
        {
            Customer item = null;
            string query = String.Format("SELECT * FROM {0} WHERE id = {1}", TableName, id);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                item = new Customer(dataReader);
            }
            dataReader.Close();
            return item;
        }

        //Instance Methods - MySQL Related

        public void Set(MySqlDataReader dataReader)
        {
            _id = Convert.ToInt32(dataReader["id"] + "");
            _name = dataReader["name"] + "";
            _icPassport = dataReader["ic_passport"] + "";
            _gender = dataReader["gender"] + "";
            _phoneNo = dataReader["phone_no"] + "";
            _email = dataReader["email"] + "";
            _address = dataReader["home_address"] + "";
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
            string query = "INSERT INTO {0} (name, ic_passport, gender, phone_no, email, home_address)" +
                " VALUES ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}')";
            query = String.Format(query, TableName, _name, _icPassport, _gender, _phoneNo, _email, _address);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
        }

        public void Update()
        {
            string query = "UPDATE {0} SET name = '{1}', ic_passport = '{2}', gender = '{3}'," +
                "phone_no = '{4}', email = '{5}', home_address = '{6}' WHERE id = {7}";
            query = String.Format(query, TableName, _name, _icPassport, _gender, _phoneNo, _email, _address, _id);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            cmd.ExecuteNonQuery();
        }

        public void Delete()
        {
            string query = String.Format("DELETE FROM {0} WHERE id = {1}", TableName, _id);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            cmd.ExecuteNonQuery();
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
    }
}
