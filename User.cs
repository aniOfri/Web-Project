using System;
using System.Collections.Generic;
using System.Data;
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

        public bool Login()
        {
            string selectQuery = "SELECT Password FROM Member";
            selectQuery += " WHERE ";
            selectQuery += "Username ='" + Username + "'";
            if (DAL.IsExist(selectQuery))
            {
                DataTable dt = DAL.ExecuteDataTable(selectQuery);
                string pass = dt.Rows[0]["password"].ToString().Replace(" ", "");
                
                return pass == Password;
            }
            return false;
        }
    }
}