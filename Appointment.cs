using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Web;

namespace VR_Web_Project
{
    public class Appointment
    {
        private int Id {get;set;}
        private string PhoneNumber {get;set;}
        private DateTime Date {get;set;}
        private int ParticipantsID {get;set;}

        public Appointment(int id, string phoneNumber, DateTime date, int participantsId)
        {
            this.Id = id;
            this.PhoneNumber = phoneNumber;
            this.Date = date;
            this.ParticipantsID = participantsId;
        }
}
}