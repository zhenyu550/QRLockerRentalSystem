﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LockerRentalManagementSystem.Core;
namespace LockerRentalManagementSystem.Model
{
    public class Cabinet
    {
        //Attributes

        private int _id;
        private string _code;
        private int _row;
        private int _column;
        private string _status;
        private int _lockerTypeId;


        //Getters and Setters

        public int Id { get { return _id; } set { _id = value; } }
        public string Code { get { return _code; } set { _code = value; } }
        public int Row { get { return _row; } set { _row = value; } }
        public int Column { get { return _column; } set { _column = value; } }
        public string Status { get { return _status; } set { _status = value; } }
        public int LockerTypeId { get { return _lockerTypeId; } set { _lockerTypeId = value; } }

        //Constants

        const string TableName = "cabinet";

        //Constructors

        public Cabinet()
        {
            _id = 0;
            _row = 0;
            _column = 0;
        }

        public Cabinet(MySqlDataReader dataReader)
        {
            Set(dataReader);
        }

        //Static Methods

        public static List<Cabinet> All(int count, int offset)
        {
            List<Cabinet> list = new List<Cabinet>();
            string query = String.Format("SELECT * FROM {0} ORDER BY id ASC LIMIT {1}, {2};", TableName, count, offset);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                list.Add(new Cabinet(dataReader));
            }
            dataReader.Close();
            return list;
        }

        public static List<Cabinet> Where(string condition, int count, int offset)
        {
            List<Cabinet> list = new List<Cabinet>();
            string query = String.Format("SELECT * FROM {0} WHERE {1} ORDER BY id ASC LIMIT {2}, {3}", TableName,
                condition, count, offset);

            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                list.Add(new Cabinet(dataReader));
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

        public static Cabinet Get(int id)
        {
            Cabinet item = null;
            string query = String.Format("SELECT * FROM {0} WHERE id = {1}", TableName, id);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                item = new Cabinet(dataReader);
            }
            dataReader.Close();
            return item;
        }

        //Instance Methods - MySQL Related

        public void Set(MySqlDataReader dataReader)
        {
            _id = Convert.ToInt32(dataReader["id"] + "");
            _code = dataReader["code"] + "";
            _row = Convert.ToInt32(dataReader["cabinet_rows"] + "");
            _column = Convert.ToInt32(dataReader["cabinet_columns"] + "");
            _lockerTypeId = Convert.ToInt32(dataReader["locker_type_id"] + "");
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
            string query = "INSERT INTO {0} (code, cabinet_rows, cabinet_columns, locker_type_id) " +
                "VALUES ('{1}', {2}, {3}, {4})";
            query = String.Format(query, TableName, _code, _row, _column, _lockerTypeId);
            MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
        }

        public void Update()
        {
            string query = "UPDATE {0} SET code = '{1}', cabinet_rows = {2}, cabinet_columns = {3}," +
                "locker_type_id = {4}, status = '{5}' WHERE id = {6}";
            query = String.Format(query, TableName, _code, _row, _column, _lockerTypeId, _status, _id);
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
            query = string.Format(query, TableName, "Available", _id);
            MySqlCommand command = new MySqlCommand(query, Database.Connection);
            command.ExecuteNonQuery();
        }

        public void Full()
        {
            string query = "UPDATE {0} SET status = '{1}' WHERE id = {2}";
            query = string.Format(query, TableName, "Full", _id);
            MySqlCommand command = new MySqlCommand(query, Database.Connection);
            command.ExecuteNonQuery();
        }

        //Instance Methods - Functional 
        public bool IsFull()
        {
            return (Status == "Full");
        }

        public bool IsDisabled()
        {
            return (Status == "Disabled");
        }
    }
}
