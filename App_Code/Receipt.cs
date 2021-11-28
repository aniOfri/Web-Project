using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DALLib;

namespace VR_Web_Project
{
    abstract public class Receipt
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int Price { get; set; }
        public string CreditCard { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public abstract string tableName();

        // A private function which returns the next Id
        // INPUT: none
        // OUTPUT: int as the Id
        public static int CountForId(string tableName)
        {
            string sql = "SELECT COUNT(*) FROM " + tableName;
            return DAL.ExecuteCounting(sql);
        }

        public static DataTable GetReceipts(int receiptOffset, string tableName)
        {
            string sql = "SELECT * FROM "+tableName;
            sql += " WHERE Id BETWEEN " + receiptOffset + " AND " + (receiptOffset + 10);

            DataTable dt = DAL.ExecuteDataTable(sql);

            return dt;
        }
    }
}