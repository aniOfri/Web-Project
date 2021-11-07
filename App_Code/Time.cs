using System;

namespace VR_Web_Project
{
    // A SIMPLE TIME STORING-AND-DISPLAYING CLASS
    public class Time
    {
        public int Hour {get;set;}
        public int Minutes { get; set; }
        public Time (int hour, int minutes)
        {
            this.Hour = hour;
            this.Minutes = minutes;
        }

        // A public function which adds time to this values
        // INPUT: int as an hour, int as a minute
        // OUTPUT: void
        public void AddTime(int hour, int minute)
        {
            // ADDS THE VALUES TO THIS.HOUR AND THIS.MINUTES
            this.Hour += hour;
            this.Minutes += minute;

            // IF MINUTES EQUALS OR LARGER THAN 60, ADD AN HOUR AND SUBTRACT 60 FROM MINUTES
            if (this.Minutes >= 60)
            {
                this.Minutes -= 60;
                this.Hour += 1;
            }

            // IF HOUR EQUALS OR LARGER THAN 24, SUBTRACT 24 FROM HOURS
            if (this.Hour >= 24)
                this.Hour -= 24;
        }

        // A public function which returns a string of time using this' values
        // INPUT: none
        // OUTPUT: string as formatted time
        public string GetTime()
        {
            // DECLARE AN EMPTY RETURN VALUE
            string toReturn = "";

            // IF HOURS LESS THAN 10 PUT A 0 AS A PLACEHOLDER, ELSE, DONT
            if (this.Hour < 10)
                toReturn += "0" + this.Hour;
            else
                toReturn += this.Hour.ToString();
            
            // SEMI COLON TO SPACE SEPERATE MINUTES FROM HOURS
            toReturn += ":";

            // IF MINTUES LESS THAN 10 PUT A 0 AS A PLACEHOLDER, ELSE, DONT
            if (this.Minutes < 10)
                toReturn += "0" + this.Minutes;
            else
                toReturn += this.Minutes.ToString();

            // RETURN
            return toReturn;
        }

        // A public static function which returns the date of the next /weekday/ parameter
        // INPUT: DayOfWeek as a day of the week
        // OUTPUT: string as a the next date of the former day of the week
        public static string Next(DayOfWeek dayOfWeek, int offset)
        {
            // DECLARE from AS TODAY
            DateTime from = DateTime.Today;
            from.AddDays(-(int)from.DayOfWeek);

            // CHECKS IF from IS NOT THE GIVEN PARAMETER
            if (from.DayOfWeek != dayOfWeek)
            {
                // IF SO, DECLARE start AS TODAY AND target AS THE PARAMETER BOTH CASTED TO INT 
                int start = (int)from.DayOfWeek;
                int target = (int)dayOfWeek;

                // ADD TO form THE DIFFERENCE BETWEEN target AND start
                from = from.AddDays(target - start);
            }

            // APPLY OFFSET
            from = from.AddDays(offset);

            // RETURN THE DATE AS STRING IN PARENTHESIS
            return "(" + from.ToShortDateString() + ")";
        }
    }
}