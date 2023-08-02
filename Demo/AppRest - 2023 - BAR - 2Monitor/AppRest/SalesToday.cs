using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class SalesToday
    {
        string salesDate;
        string salesLable;
        float salesUnit;
        float salesAmount;

        public SalesToday()
        {
        }

        public SalesToday(string salesDate, string salesLable, float salesUnit, float salesAmount)
        {
            SalesDate = salesDate;
            SalesLable = salesLable;
            SalesUnit = salesUnit;
            SalesAmount = salesAmount;
        }

        public string SalesDate
        {
            get { return salesDate; }
            set { salesDate = value; }
        }

        public string SalesLable
        {
            get { return salesLable; }
            set { salesLable = value; }
        }

        public float SalesUnit
        {
            get { return salesUnit; }
            set { salesUnit = value; }
        }


        public float SalesAmount
        {
            get { return salesAmount; }
            set { salesAmount = value; }
        }


    }
}
