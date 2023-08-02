using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Bill
    {
        int billID;
        string billNo;


        public Bill() { }


        public Bill(int billID, string billNo)
        {
            BillID = billID;
            BillNo = billNo;
        
        }

        public int BillID
        {
            get { return billID; }
            set { billID = value; }
        }
       

        public string BillNo
        {
            get { return billNo; }
            set { billNo = value; }
        }




    }
}
