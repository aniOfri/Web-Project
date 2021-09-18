﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VR_Web_Project
{
    public class User
    {
        private int Id { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        //private bool IsManager { get; set; }
        private DAL DAL = new DAL();

        public User(string username, string password)
        {
            this.Id = countForId();
            this.Username = username;
            this.Password = password;
            //this.IsManager = isManager;
        }

        private int countForId()
        {
            string sql = "SELECT COUNT(*) FROM Member";
            return DAL.ExecuteScalar(sql);
        }

        private void setId()
        {
            string selectQuery = "SELECT Id FROM Member";
            selectQuery += " WHERE Username ='" + Username + "'";
            selectQuery += " AND Password ='" + Password + "'";

            SqlDataReader reader = DAL.GetReader(selectQuery);
            if ((bool)reader.Read())
                Id = (int)reader["Id"];

            reader.Close();
        }

        public bool Login()
        {
            string selectQuery = "SELECT Password FROM Member";
            selectQuery += " WHERE ";
            selectQuery += "Username ='" + Username + "'";
            if (DAL.IsExist(selectQuery))
            {
                DataTable dt = DAL.ExecuteDataTable(selectQuery);
                string pass = dt.Rows[0]["password"].ToString().Replace(" ", "");

                if (pass == Password)
                {
                    setId();
                    return true;
                }
                else return false;
            }
            return false;
        }

        public bool Register(string phoneNumber)
        {
            string sql = "INSERT INTO Member (Id, Username, PhoneNumber, Password, isManager) VALUES (\'";
            sql += Id + "\', \'" + Username + "\', \'" + phoneNumber + "\', \'" + Password + "\', \'" + false + "\')";
            try {
                DAL.ExecNonQuery(sql);
                return true;
            }
            catch {
                return false;
            }
        }
    }
}