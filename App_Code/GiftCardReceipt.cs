using System;
using System.Collections.Generic;
using System.Data;
using DALLib;

namespace VR_Web_Project
{
    public class GiftCardReceipt : Receipt
    {
        public GiftCardReceipt(int Id, DateTime orderDate, int price, string creditCard, string fn, string ln)
        {
            this.Id = Id != -1 ? Id : CountForId();
            this.OrderDate = orderDate;
            this.Price = price;
            this.CreditCard = creditCard;
            this.FirstName = fn;
            this.LastName = ln;
        }

        public override string tableName()
        {
            return "GiftCardReceipt";
        }

        // A public function which inserts the data of the class to the database
        // INPUT: none
        // OUTPUT: bool as success/fail
        public bool Insert()
        {
            // BUILD STRING AS AN SQL COMMAND
            string sql = "INSERT INTO "+tableName()+" (Id, Orderdate, Price, CreditCard, FirstName, LastName) VALUES (\'";
            sql += Id + "\', \'" + OrderDate + "\', \'" + Price + "\', \'" + CreditCard + "\', \'" + FirstName + "\', \'" + LastName + "\')";

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
        // A static function which creates a receipt dictionary
        // INPUT: string as appointment id
        // OUTPUT Dictionary<string, string> as receipt
        public static Dictionary<string, string> BuildReceiptDictionary(string receiptID)
        {
            string sql = "SELECT * FROM GiftCardReceipt";
            sql += " WHERE Id='" + receiptID + "'";
            
            DataTable dt = DAL.ExecuteDataTable(sql);

            DataRow dr = dt.Rows[0];

            string[] dateTime = ((DateTime)dr[1]).ToString("MM/dd/yyyy HH:mm").Split(' ');
            string date = dateTime[0];
            string time = dateTime[1];

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("date", date);
            dic.Add("time", time);
            dic.Add("price", ((int)dr[4]).ToString());
            dic.Add("ccn", ((string)dr[5]));
            dic.Add("firstname", ((string)dr[6]));
            dic.Add("lastname", ((string)dr[7]));

            return dic;
        }
    }
}