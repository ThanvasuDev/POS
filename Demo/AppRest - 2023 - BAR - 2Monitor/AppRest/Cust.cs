using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Cust
    {
        int custID;
        string custName;

        public Cust()
        {

        }

        public Cust(int custID, string custName)
        {
            CustID = custID;
            CustName = custName;
        }


        public int CustID
        {
            get { return custID; }
            set { custID = value; }
        }
        

        public string CustName
        {
            get { return custName; }
            set { custName = value; }
        }






    }
}
