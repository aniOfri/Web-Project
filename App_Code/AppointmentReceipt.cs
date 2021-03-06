using System;
using System.Collections.Generic;
using System.Data;
using DALLib;

namespace VR_Web_Project
{
    public class AppointmentReceipt : Receipt
    {
        public int UserID { get; set; }

        public AppointmentReceipt(int Id, DateTime orderDate, int userId, int price, string creditCard, string fn, string ln)
        {
            this.Id = Id != -1 ? Id : CountForId(tableName());
            this.OrderDate = orderDate;
            this.UserID = userId;
            this.Price = price;
            this.CreditCard = creditCard;
            this.FirstName = fn;
            this.LastName = ln;
        }

        public override string tableName()
        {
            return "AppointmentReceipt";
        }

        // A public function which inserts the data of the class to the database
        // INPUT: none
        // OUTPUT: bool as success/fail
        public bool Insert()
        {
            // BUILD STRING AS AN SQL COMMAND
            string sql = "INSERT INTO " + tableName() + " (Id, Orderdate, UserId, Price, CreditCard, FirstName, LastName) VALUES (\'";
            sql += Id + "\', \'" + OrderDate + "\', \'" + UserID + "\', \'" + Price + "\', \'" + CreditCard + "\', \'" + FirstName + "\', \'" + LastName + "\')";

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
        // A static function which creates a receipt dictionary of all the receipts information
        // INPUT: string as appointment id
        // OUTPUT Dictionary<string, string> as receipt
        public static Dictionary<string, string> BuildReceiptDictionary(string receiptID)
        {
            string sql = "SELECT * FROM AppointmentReceipt";
            sql += " WHERE Id='" + receiptID + "'";

            DataTable dt = DAL.ExecuteDataTable(sql);

            DataRow dr = dt.Rows[0];

            string[] dateTime = ((DateTime)dr[1]).ToString("MM/dd/yyyy HH:mm").Split(' ');
            string date = dateTime[0];
            string time = dateTime[1];

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("date", date);
            dic.Add("time", time);
            dic.Add("price", ((int)dr[3]).ToString());
            dic.Add("ccn", ((string)dr[4]));
            dic.Add("firstname", ((string)dr[5]));
            dic.Add("lastname", ((string)dr[6]));

            return dic;
        }
    }
}