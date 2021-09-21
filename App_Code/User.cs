using System.Data;
using System.Data.SqlClient;

namespace VR_Web_Project
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsManager { get; set; }
        //private bool IsManager { get; set; }

        public User(string username, string password)
        {
            this.Id = CountForId();
            this.Username = username;
            this.Password = password;
            this.IsManager = false;
            //this.IsManager = isManager;
        }

        private int CountForId()
        {
            string sql = "SELECT COUNT(*) FROM Member";
            return DAL.ExecuteScalar(sql);
        }

        private void SetId()
        {
            string selectQuery = "SELECT Id FROM Member";
            selectQuery += " WHERE Username ='" + Username + "'";
            selectQuery += " AND Password ='" + Password + "'";

            SqlDataReader reader = DAL.GetReader(selectQuery);
            if ((bool)reader.Read())
                Id = (int)reader["Id"];

            reader.Close();
        }

        public bool SetManager()
        {
            string selectQuery = "SELECT IsManager FROM Member";
            selectQuery += " WHERE Id ='" + Id + "'";

            SqlDataReader reader = DAL.GetReader(selectQuery);
            if ((bool)reader.Read())
                IsManager = (bool)reader["IsManager"];

            return IsManager;
        }

        public void SetPhoneNumber()
        {
            string selectQuery = "SELECT PhoneNumber FROM Member";
            selectQuery += " WHERE Id ='" + Id + "'";

            SqlDataReader reader = DAL.GetReader(selectQuery);
            if ((bool)reader.Read())
                PhoneNumber = (string)reader["PhoneNumber"];
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
                    SetId();
                    SetManager();
                    SetPhoneNumber();
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