using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class TrnMax
    {
        string maxPeriod;
        string maxDate;
        int maxTrn;

        public TrnMax() { }

        public TrnMax(string maxPeriod, string maxDate, int maxTrn)
        {
            MaxPeriod = maxPeriod;
            MaxDate = maxDate;
            MaxTrn = maxTrn;
        } 

        public int MaxTrn
        {
            get { return maxTrn; }
            set { maxTrn = value; }
        }
         

        public string MaxPeriod
        {
            get { return maxPeriod; }
            set { maxPeriod = value; }
        }


        public string MaxDate
        {
            get { return maxDate; }
            set { maxDate = value; }
        }




    }
}
