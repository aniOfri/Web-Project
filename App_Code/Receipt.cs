using System;
using System.Data;
using DALLib;

namespace VR_Web_Project
{
    public class Receipt
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserID { get; set; }
        public int AppointmentID { get; set; }
        public int Price { get; set; }
        public string CreditCard { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Receipt(DateTime orderDate, int userId, int appointmentId, int price, string creditCard, string fn, string ln)
        {
            this.Id = CountForId();
            this.OrderDate = orderDate;
            this.UserID = userId;
            this.AppointmentID = appointmentId;
            this.Price = price;
            this.CreditCard = creditCard;
            this.FirstName = fn;
            this.LastName = ln;
        }


        // A private function which returns the next Id
        // INPUT: none
        // OUTPUT: int as the Id
        private int CountForId()
        {
            string sql = "SELECT COUNT(*) FROM Receipt";
            return DAL.ExecuteCounting(sql);
        }

        // A public function which inserts the data of the class to the database
        // INPUT: none
        // OUTPUT: bool as success/fail
        public bool Insert()
        {
            // BUILD STRING AS AN SQL COMMAND
            string sql = "INSERT INTO Receipt (Id, Orderdate, UserId, AppointmentId, Price, CreditCard, FirstName, LastName) VALUES (\'";
            sql += Id + "\', \'" + OrderDate + "\', \'" + UserID + "\', \'" + AppointmentID + "\', \'" + Price + "\', \'" + CreditCard + "\', \'" + FirstName + "\', \'" + LastName + "\')";

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

        public static string BuildReceiptString(string appointmentId)
        {
            string sql = "SELECT * FROM Receipt";
            sql += " WHERE AppointmentId='" + appointmentId + "'";
            
            DataTable dt = DAL.ExecuteDataTable(sql);

            DataRow dr = dt.Rows[0];

            string receiptString = "";
            receiptString += "תאריך הזמנה: " + ((DateTime)dr[1]).ToString() + "\n";
            receiptString += "מחיר: " + ((int)dr[4]).ToString() + "\n";
            receiptString += "כרטיס אשראי: " + ((string)dr[5]) + "\n";
            receiptString += "שם פרטי: " + ((string)dr[6]) + "\n";
            receiptString += "שם משפחה: " + ((string)dr[7]) + "\n";

            return receiptString;
        }
    }
}