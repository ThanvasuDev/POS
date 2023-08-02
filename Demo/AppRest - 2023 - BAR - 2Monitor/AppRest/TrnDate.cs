using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class TrnDate
    {
        string periodID;
        string period;
        string dateID;
        string date;

        public TrnDate() { }

        public TrnDate(string periodID, string period)
        {
            PeriodID = periodID;
            Period = period;
        }

        public TrnDate(string periodID, string period, string dateID, string date)
        {
            PeriodID = periodID;
            Period = period;
            DateID = dateID;
            Date = date;

        }

        public string PeriodID
        {
            get { return periodID; }
            set { periodID = value; }
        }

        public string Period
        {
            get { return period; }
            set { period = value; }
        }

        public string DateID
        {
            get { return dateID; }
            set { dateID = value; }
        }


        public string Date
        {
            get { return date; }
            set { date = value; }
        }
            

    }
}
