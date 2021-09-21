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
    }
}