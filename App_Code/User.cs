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

        public User(string username, string password)
        {
            this.Id = CountForId();
            this.Username = username;
            this.Password = password;
            this.IsManager = false;
        }

        // A private function which returns the next Id
        // INPUT: none
        // OUTPUT: int as the Id
        private int CountForId()
        {
            string sql = "SELECT COUNT(*) FROM Member";
            return DAL.ExecuteCounting(sql);
        }

        // A private function which sets Id of this as the Id of the user who logged in using the database
        // INPUT: none
        // OUTPUT: void
        private void SetId()
        {
            // BUILD STRING AS AN SQL COMMAND
            string selectQuery = "SELECT Id FROM Member";
            selectQuery += " WHERE Username ='" + Username + "'";
            selectQuery += " AND Password ='" + Password + "'";

            // GET READER USING DAL
            SqlDataReader reader = DAL.GetReader(selectQuery);
            
            // ASSIGN NEW DATA TO Id OF this
            if ((bool)reader.Read())
                Id = (int)reader["Id"];

            // CLOSE
            reader.Close();
        }

        // A private function which sets IsManager of this as the IsManager value of the user who logged in using the database
        // INPUT: none
        // OUTPUT: void
        public void SetManager()
        {
            // BUILD STRING AS AN SQL COMMAND
            string selectQuery = "SELECT IsManager FROM Member";
            selectQuery += " WHERE Id ='" + Id + "'";

            // GET READER USING DAL
            SqlDataReader reader = DAL.GetReader(selectQuery);

            // ASSIGN NEW DATA TO IsManager OF this
            if ((bool)reader.Read())
                IsManager = (bool)reader["IsManager"];

            // CLOSE
            reader.Close();
        }

        // A private function which sets PhoneNumber of this as the PhoneNumber of the user who logged in using the database
        // INPUT: none
        // OUTPUT: void
        public void SetPhoneNumber()
        {
            // BUILD STRING AS AN SQL COMMAND
            string selectQuery = "SELECT PhoneNumber FROM Member";
            selectQuery += " WHERE Id ='" + Id + "'";

            // GET READER USING DAL
            SqlDataReader reader = DAL.GetReader(selectQuery);

            // ASSIGN NEW DATA TO PhoneNumber OF this
            if ((bool)reader.Read())
                PhoneNumber = (string)reader["PhoneNumber"];

            // CLOSE
            reader.Close();
        }

        // A public function which attempts to log in the user into the site using this' values
        // INPUT: none
        // OUTPUT: bool as success/fail
        public bool Login()
        {
            // BUILD STRING AS AN SQL COMMAND
            string selectQuery = "SELECT Password FROM Member";
            selectQuery += " WHERE ";
            selectQuery += "Username ='" + Username + "'";

            // CHECKS IF USERNAME EXISTS INSIDE THE DB
            if (DAL.IsExist(selectQuery))
            {
                // GET DATATABLE OF THE USER WHO ATTEMPTS TO LOG
                DataTable dt = DAL.ExecuteDataTable(selectQuery);

                // FORMAT AND CHECK THE PASSWORD
                string pass = dt.Rows[0]["password"].ToString().Replace(" ", "");
                if (pass == Password)
                {
                    // ASSIGN this WITH TRUE DATA
                    SetId();
                    SetManager();
                    SetPhoneNumber();

                    // RETURN SUCCESS
                    return true;
                }
            }

            // RETURN FAIL
            return false;
        }

        // A public function which registers the user the to site (inserts data to db)
        // INPUT: string as a phone number
        // OUTPUT: bool as success/fail
        public bool Register()
        {
            // BUILD STRING AS AN SQL COMMAND
            string sql = "INSERT INTO Member (Id, Username, PhoneNumber, Password, isManager) VALUES (\'";
            sql += Id + "\', \'" + Username + "\', \'" + PhoneNumber + "\', \'" + Password + "\', \'" + false + "\')";
            
            // TRY TO EXECUTE COMMAND
            try {
                DAL.ExecNonQuery(sql);
                
                // REGISTERED SUCCESSFULLY, RETURNS TRUE
                return true;
            }
            catch {

                // THERE WAS AN ERROR, RETURNS FALSE
                return false;
            }
        }

        // A public function which changes the password of the users to the pass received as a paramater
        // INPUT: string as the new password
        // OUTPUT: bool as success/fail
        public bool ChangePassword(string newPass)
        {
            // BUILD STRING AS AN SQL COMMAND
            string sql = "UPDATE Member SET Password='" + newPass + "'";
            sql += "WHERE Id='" + Id + "'";

            // TRY TO EXECUTE COMMAND
            try
            {
                DAL.ExecNonQuery(sql);

                // PASSWORD CHANGED SUCCESSFULLY, RETURNS TRUE
                return true;
            }
            catch
            {

                // THERE WAS AN ERROR, RETURNS FALSE
                return false;
            }
        }
    }
}