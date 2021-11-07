using System;
using System.Collections.Generic;
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
        public int CountForId()
        {
            string sql = "SELECT COUNT(*) FROM " + tableName();
            return DAL.ExecuteCounting(sql);
        }
    }
}