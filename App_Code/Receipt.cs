using System;
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

        public Receipt(DateTime orderDate, int userId, int appointmentId, int price, string creditCard)
        {
            this.Id = CountForId();
            this.OrderDate = orderDate;
            this.UserID = userId;
            this.AppointmentID = appointmentId;
            this.Price = price;
            this.CreditCard = creditCard;
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
        public bool Insert()
        {
            // BUILD STRING AS AN SQL COMMAND
            string sql = "INSERT INTO Receipt (Id, Orderdate, UserId, AppointmentId, Price, CreditCard) VALUES (\'";
            sql += Id + "\', \'" + OrderDate + "\', \'" + UserID + "\', \'" + AppointmentID + "\', \'" + Price + "\', \'" + CreditCard + "\')";

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
    }
}