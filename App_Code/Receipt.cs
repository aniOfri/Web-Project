using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VR_Web_Project
{
    public class Receipt
    {
        private int Id { get; set; }
        private DateTime OrderDate { get; set; }
        private int UserID { get; set; }
        private int AppointmentID { get; set; }
        private int Price { get; set; }
        private string CreditCard { get; set; }

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