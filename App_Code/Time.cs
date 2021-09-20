using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VR_Web_Project
{
    public class Time
    {
        public int Hour {get;set;}
        public int Minutes { get; set; }

        public Time (int hour, int minutes)
        {
            this.Hour = hour;
            this.Minutes = minutes;
        }

        public void AddTime(int hour, int minute)
        {
            this.Hour += hour;
            this.Minutes += minute;

            if (this.Minutes == 60)
            {
                this.Minutes = 0;
                this.Hour += 1;
            }

            if (this.Hour == 24)
                this.Hour = 0;
        }

        public string GetTime()
        {
            string toReturn = "";
            if (this.Hour < 10)
                toReturn += "0" + this.Hour;
            else
                toReturn += this.Hour.ToString();
            
            toReturn += ":";

            if (this.Minutes < 10)
                toReturn += "0" + this.Minutes;
            else
                toReturn += this.Minutes.ToString();

            return toReturn;
        }
    }
}