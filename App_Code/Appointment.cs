using DALLib;
using System;
using System.Data;
using System.Data.SqlClient;

namespace VR_Web_Project
{
    public class Appointment : Order
    {
        public int Id {get;set;}
        public string UserId {get;set;}
        public DateTime Date {get;set;}
        public int Participants {get;set;}
        public int ReceiptId {get;set;}

        public Appointment(string userId, DateTime date, int participants, int price = 0) :base(price)
        {
            this.Id = CountForId();
            this.UserId = userId;
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
            string sql = "INSERT INTO Appointment (Id, UserId, DateTime, Participants, ReceiptID) VALUES (\'";
            sql += Id + "\', \'" + UserId + "\', \'" + Date + "\', \'" + Participants + "\', \'" + ReceiptId + "\')";
            
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
        public static string[][] CreateSchedule(int dayOffset=0)
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
                // GET DATA FROM READER
                DateTime date = (DateTime)reader["DateTime"];
                DateTime today = DateTime.Today;
                today = today.AddHours(date.Hour);
                today = today.AddMinutes(date.Minute);

                int days = (date.Date - today.Date).Days;
                days += (int)today.DayOfWeek + 1; // +offset, -week

                // RESET today TO TODAY (WITHOUT THE HOURS/MINUTES)
                today = DateTime.Today;
                int hours = (date - today).Hours - 8;

                // ALIGN THE TIME CORRECTLY BECAUSE OF SKIPPING HOURS
                if (hours < 0) hours += 24;
                if (hours >= 11) hours -= 3;
                else if (hours >= 7) hours -= 2;
                else if (hours >= 3) hours -= 0;

                // GET USERNAME
                string username = User.GetUsername((string)reader["UserId"]);

                // BUILD STRING TO THE APPOINTMENT SLOT
                string str = $"{username}" +
                    $" ({(int)reader["Participants"]}/6) {(int)reader["ReceiptID"]}" ;
                
                // CHECK IF VALID
                if (days > dayOffset && hours >= 0 && days < 8 + dayOffset)
                {
                    // CREATE INDEX FOR DAY 
                    int daysIndex = days + Math.Abs(dayOffset);
                    while (daysIndex > 7) daysIndex -= 7;
                    // IMPLEMENT
                    week[daysIndex][hours] = str;
                }
            }

            // CLOSE READER AND RETURN SCHEDULE
            reader.Close();
            readerAndConnection.Item2.Close();
            return week;
        }
        // A static function which gets the appointments of a user
        // INPUT: User as user
        // OUTPUT DataTable as appointments
        public static DataTable GetAppointments(User user)
        {
            string sql = "SELECT * FROM Appointment";
            sql += " WHERE UserId='" + user.Id + "'";

            DataTable dt = DAL.ExecuteDataTable(sql);

            return dt;
        }
    }
}