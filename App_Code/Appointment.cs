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
        private DAL DAL = new DAL();

        public Appointment(string phoneNumber, DateTime date, int participants)
        {
            this.Id = countForId();
            this.PhoneNumber = phoneNumber;
            this.Date = date;
            this.Participants = participants;
        }

        // Count for id
        private int countForId()
        {
            string sql = "SELECT COUNT(*) FROM Appointment";
            return DAL.ExecuteScalar(sql);
        }

        public bool Order()
        {
            string sql = "INSERT INTO Appointment (Id, PhoneNumber, DateTime, Participants) VALUES (\'";
            sql += Id + "\', \'" + PhoneNumber + "\', \'" + Date + "\', \'" + Participants + "\')";
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
        // Static method to create schedule
        public static string[][] createSchedule()
        {
            DAL DAL = new DAL();
            string[][] week = new string[8][];
                week[0] = new string[14];


            /* Available times in the system:
             "08:00", "09:15", "10:30", "11:45", "12:00",
             "13:15", "14:30", "15:45", "16:00", "17:15",
             "18:30", "19:45", "20:00", "21:15" */
            Time time = new Time(8, 0);
            for (int i = 0; i < 14; i++)
            {
                week[0][i] = time.GetTime();
                time.AddTime(1, 15);
            }

            for (int i = 1; i < 8; i++)
            {
                week[i] = new string[14];
                for (int j = 0; j < 14; j++)
                    week[i][j] = "0";
            }

            string cmdString = "SELECT * FROM Appointment";
            SqlDataReader reader = DAL.GetReader(cmdString);
            while (reader.Read())
            {
                DateTime date = (DateTime)reader["DateTime"];
                int days = (date - DateTime.Today).Days + 1 + (int)DateTime.Today.DayOfWeek;
                if (days > 7) days = days - 7;
                int hours = (date - DateTime.Today).Hours - 8;
                if (days > 0 && hours > 0)
                {
                    week[days][hours] = ((int)reader["Participants"]).ToString();
                }
            }

            reader.Close();

            return week;
        }
    }
}