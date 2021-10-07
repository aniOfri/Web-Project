using System;
using System.Data;
using System.Data.SqlClient;

namespace VR_Web_Project
{
    public class Appointment
    {
        public int Id {get;set;}
        public string PhoneNumber {get;set;}
        public DateTime Date {get;set;}
        public int Participants {get;set;}

        public Appointment(string phoneNumber, DateTime date, int participants)
        {
            this.Id = CountForId();
            this.PhoneNumber = phoneNumber;
            this.Date = date;
            this.Participants = participants;
        }

        // A private function which returns the next Id
        // INPUT: none
        // OUTPUT: int as the Id
        private int CountForId()
        {
            string sql = "SELECT COUNT(*) FROM Appointment";
            return DAL.ExecuteCounting(sql);
        }

        // A public function which inserts the data of the class to the database
        // INPUT: none
        // OUTPUT: bool as success/fail
        public bool Order()
        {
            // BUILD STRING AS AN SQL COMMAND
            string sql = "INSERT INTO Appointment (Id, PhoneNumber, DateTime, Participants) VALUES (\'";
            sql += Id + "\', \'" + PhoneNumber + "\', \'" + Date + "\', \'" + Participants + "\')";
            
            // EXECUTE COMMAND AND RETURN TRUE IF SUCESS AND FAIL OTHERWISE
            try
            {
                DAL.ExecNonQuery(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }
        // A static function which creates the schedule
        // INPUT: none
        // OUTPUT string[][] as the schedule
        public static string[][] CreateSchedule()
        {
            // DECLARE THE ARRAY
            string[][] week = new string[8][];
                week[0] = new string[14];

            // DECLARE A CUSTOM TIME CLASS
            Time time = new Time(8, 0);

            // TITLE EACH ROW WITH TIME
            for (int i = 0; i < 14; i++)
            {
                week[0][i] = time.GetTime();
                time.AddTime(1, 15);
            }

            // FILL THE SCHEDULE WITH PLACEHOLDERS
            for (int i = 1; i < 8; i++)
            {
                week[i] = new string[14];
                for (int j = 0; j < 14; j++)
                    week[i][j] = "";
            }

            // GET READER FROM DATABASE
            string sql = "SELECT * FROM Appointment";
            var readerAndConnection = DAL.GetReader(sql);
            SqlDataReader reader = readerAndConnection.Item1;
            while (reader.Read())
            {
                // FILL THE OCCUPIED SPOTS WITH THE NUMBER OF PARTICIPANTS 
                DateTime date = (DateTime)reader["DateTime"];
                int days = (date - DateTime.Today).Days + 1 + (int)DateTime.Today.DayOfWeek;
                if (days > 7) days -= 7;
                int hours = (date - DateTime.Today).Hours - 9;

                string str = $"{GetUsername((string)reader["PhoneNumber"])}" +
                    $" ({(int)reader["Participants"]}/6)";
                if (days > 0 && hours > 0)
                {
                    week[days][hours] = str;
                }
            }

            // CLOSE READER AND RETURN SCHEDULE
            reader.Close();
            readerAndConnection.Item2.Close();
            return week;
        }

        private static string GetUsername(string phoneNumber)
        {
            // BUILD STRING AS AN SQL COMMAND
            string selectQuery = "SELECT Username FROM Member";
            selectQuery += " WHERE PhoneNumber ='" + phoneNumber + "'";

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

        public static DataTable GetAppointments(User user)
        {
            string sql = "SELECT * FROM Appointment";
            sql += " WHERE PhoneNumber='" + user.PhoneNumber + "'";

            DataTable dt = DAL.ExecuteDataTable(sql);

            return dt;
        }
    }
}