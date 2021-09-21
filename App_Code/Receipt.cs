using System;

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

        public Receipt(int id, DateTime orderDate, int userId, int appointmentId, int price, string creditCard)
        {
            this.Id = id;
            this.OrderDate = orderDate;
            this.UserID = userId;
            this.AppointmentID = appointmentId;
            this.Price = price;
            this.CreditCard = creditCard;
        }
    }
}