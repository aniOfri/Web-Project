using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Design;
using System.Linq;
using System.Web;

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
            // DECLARE THE FUNCTIONS
            string[][] week = new string[8][];
                week[0] = new string[14];

            /* Available times in the system:
             "08:00", "09:15", "10:30", "11:45", "12:00",
             "13:15", "14:30", "15:45", "16:00", "17:15",
             "18:30", "19:45", "20:00", "21:15" */

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
                    week[i][j] = "0";
            }

            // GET READER FROM DATABASE
            string cmdString = "SELECT * FROM Appointment";
            SqlDataReader reader = DAL.GetReader(cmdString);
            while (reader.Read())
            {
                // FILL THE OCCUPIED SPOTS WITH THE NUMBER OF PARTICIPANTS 
                DateTime date = (DateTime)reader["DateTime"];
                int days = (date - DateTime.Today).Days + 1 + (int)DateTime.Today.DayOfWeek;
                if (days > 7) days -= 7;
                int hours = (date - DateTime.Today).Hours - 8;
                if (days > 0 && hours > 0)
                {
                    week[days][hours] = ((int)reader["Participants"]).ToString();
                }
            }

            // CLOSE READER AND RETURN SCHEDULE
            reader.Close();
            return week;
        }
    }
}