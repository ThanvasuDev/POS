using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class PayType
    {

        int payTypeID; 
        string payTypeName;
         

        public PayType(int payTypeID, string payTypeName)
        {
            PayTypeID = payTypeID;
            PayTypeName = payTypeName;
        }

        public int PayTypeID
        {
            get { return payTypeID; }
            set { payTypeID = value; }
        }

        public string PayTypeName
        {
            get { return payTypeName; }
            set { payTypeName = value; }
        }


    }
}
