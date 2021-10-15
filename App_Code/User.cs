using DALLib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

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

        private static string GenerateSalt()
        {
            var bytes = new byte[128 / 8];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }

        public static string HashPassword(string password, string salt = null)
        {
            // Generate the Salt
            var genSalt = (salt == null) ? GenerateSalt() : salt;

            // Hash the password
            var byteResult = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(genSalt), 10000);
            var hash = Convert.ToBase64String(byteResult.GetBytes(24));

            return hash + "." + genSalt;
        }

        public static bool VerifyPassword(string password, string digest)
        {
            // Split the digest into two parts
            string[] parts = digest.Split('.');
            var storedHash = parts[0];
            var storedSalt = parts[1];

            var hash = HashPassword(password, storedSalt);

            return (hash == digest);
        }


        // A private function which returns the next Id
        // INPUT: none
        // OUTPUT: int as the Id
        public static int CountForId()
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
            var readerAndConnection = DAL.GetReader(selectQuery);
            SqlDataReader reader = readerAndConnection.Item1;

            // ASSIGN NEW DATA TO Id OF this
            if ((bool)reader.Read())
                Id = (int)reader["Id"];

            // CLOSE
            reader.Close();
            readerAndConnection.Item2.Close();
        }

        // A private function which returns IsManager of the user who correlate to the given id
        // INPUT: string as id
        // OUTPUT: bool as manager state
        public static bool GetManager(string id)
        {
            // BUILD STRING AS AN SQL COMMAND
            string selectQuery = "SELECT IsManager FROM Member";
            selectQuery += " WHERE Id ='" + id + "'";

            // GET READER USING DAL
            var readerAndConnection = DAL.GetReader(selectQuery);
            SqlDataReader reader = readerAndConnection.Item1;

            // ASSIGN NEW DATA TO IsManager OF this
            bool isManager = false;
            if ((bool)reader.Read())
                isManager = (bool)reader["IsManager"];

            // CLOSE
            reader.Close();
            readerAndConnection.Item2.Close();

            return isManager;
        }

        // A private function which returns PhoneNumber of the user who correlate to the given id
        // INPUT: string as id
        // OUTPUT: string as phone number
        public static string GetPhoneNumber(string id)
        {
            // BUILD STRING AS AN SQL COMMAND
            string selectQuery = "SELECT PhoneNumber FROM Member";
            selectQuery += " WHERE Id ='" + id + "'";

            // GET READER USING DAL
            var readerAndConnection = DAL.GetReader(selectQuery);
            SqlDataReader reader = readerAndConnection.Item1;

            // ASSIGN NEW DATA TO PhoneNumber OF this
            string phoneNumber = "";
            if ((bool)reader.Read())
                phoneNumber = (string)reader["PhoneNumber"];

            // CLOSE
            reader.Close();
            readerAndConnection.Item2.Close();
            return phoneNumber;
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
                if (VerifyPassword(Password, pass))
                {
                    Password = pass;
                    // ASSIGN this WITH TRUE DATA
                    SetId();
                    IsManager = GetManager(Id.ToString());
                    PhoneNumber = GetPhoneNumber(Id.ToString());

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
            sql += Id + "\', \'" + Username + "\', \'" + PhoneNumber + "\', \'" + HashPassword(Password) + "\', \'" + false + "\')";
            
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
        // INPUT: string as the new password and string as userid or User as user
        // OUTPUT: bool as success/fail
        public static bool ChangePassword(string newPass, string id = null, User user = null)
        {
            if (user != null)
                id = user.Id.ToString();

            string hashedPassword = HashPassword(newPass);
            // BUILD STRING AS AN SQL COMMAND
            string sql = "UPDATE Member SET Password='" + hashedPassword + "'";
            sql += "WHERE Id='" + id + "'";

            // TRY TO EXECUTE COMMAND
            try
            {
                DAL.ExecNonQuery(sql);
                if (user != null)
                    user.Password = hashedPassword;

                // PASSWORD CHANGED SUCCESSFULLY, RETURNS TRUE
                return true;
            }
            catch
            {

                // THERE WAS AN ERROR, RETURNS FALSE
                return false;
            }
        }

        // A public function which sets IsManager of the users to correlates to the given id
        // INPUT: string as id and bool as new value
        // OUTPUT: bool as success/fail
        public static bool SetManager(string id, bool newValue)
        {
            // BUILD STRING AS AN SQL COMMAND
            string sql = "UPDATE Member SET IsManager='" + newValue + "'";
            sql += "WHERE Id='" + id + "'";

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

        public static string GetUsername(string id)
        {
            // BUILD STRING AS AN SQL COMMAND
            string selectQuery = "SELECT Username FROM Member";
            selectQuery += " WHERE Id ='" + id + "'";

            // GET READER USING DAL
            var readerAndConnection = DAL.GetReader(selectQuery);
            SqlDataReader reader = readerAndConnection.Item1;

            // READ VALUE NEEDED
            string username = "";
            if ((bool)reader.Read())
                username = (string)reader["Username"];

            // CLOSE AND RETURN
            reader.Close();
            readerAndConnection.Item2.Close();
            return username;
        }

        public static DataTable GetUsers(int userOffset)
        {
            string sql = "SELECT * FROM Member";
            sql += " WHERE Id BETWEEN " + userOffset + " AND " + (userOffset + 10);
            
            DataTable dt = DAL.ExecuteDataTable(sql);

            return dt;
        }
    }
}