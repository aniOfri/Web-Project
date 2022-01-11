using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DALLib;

namespace VR_Web_Project
{
    public class Feedback
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int MemberId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }

        public Feedback(DateTime date, int memberId, int rating, string content)
        {
            Id = CountForId();
            Date = date;
            MemberId = memberId;
            Rating = rating;
            Content = content;
        }

        public static int CountForId()
        {
            string sql = "SELECT COUNT(*) FROM Feedback";
            return DAL.ExecuteCounting(sql);
        }

        public static DataTable GetFeedbacks(int feedbackOffset)
        {
            string sql = "SELECT * FROM Feedback";
            sql += " WHERE Id BETWEEN " + feedbackOffset + " AND " + (feedbackOffset + 10);

            DataTable dt = DAL.ExecuteDataTable(sql);

            return dt;
        }

        // A public function which inserts the data of the class to the database
        // INPUT: none
        // OUTPUT: bool as success/fail
        public bool Insert()
        {
            // BUILD STRING AS AN SQL COMMAND
            string sql = "INSERT INTO Feedback (Id, Date, MemberId, Rating, Content) VALUES (\'";
            sql += Id + "\', \'" + Date + "\', \'" + MemberId + "\', \'" + Rating + "\', N\'" + Content + "\')";

            // EXECUTE COMMAND AND RETURN TRUE IF SUCESS AND FAIL OTHERWISE
            try
            {
                DAL.ExecNonQuery(sql);
                return true;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public bool Exist(int userId)
        {
            string sql = "SELECT Content FROM Feedback ";
            sql += "WHERE MemberId =\'" + userId.ToString() +"\'";

            return DAL.IsExist(sql);
        }

        public bool Update()
        {
            string sql = "UPDATE Feedback ";
            sql += "SET Date = \'" + Date + "\', ";
            sql += "Rating = \'" + Rating + "\', ";
            sql += "Content = N\'" + Content + "\' ";
            sql += " WHERE MemberId = \'" + MemberId + "\'";
            // EXECUTE COMMAND AND RETURN TRUE IF SUCESS AND FAIL OTHERWISE
            try
            {
                DAL.ExecNonQuery(sql);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }
    }
}