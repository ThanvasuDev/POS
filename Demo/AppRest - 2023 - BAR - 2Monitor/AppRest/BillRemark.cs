using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class BillRemark
    {

        int billRemarkID;
        string billRemarkL1;
        string billRemarkL2;
        string billRemarkL3;

        public BillRemark(int billRemarkID, string billRemarkL1, string billRemarkL2, string billRemarkL3)
        {
            BillRemarkID = billRemarkID;
            BillRemarkL1 = billRemarkL1;
            BillRemarkL2 = billRemarkL2;
            BillRemarkL3 = billRemarkL3;
        }

        public int BillRemarkID
        {
            get { return billRemarkID; }
            set { billRemarkID = value; }
        } 

        public string BillRemarkL1
        {
            get { return billRemarkL1; }
            set { billRemarkL1 = value; }
        } 

        public string BillRemarkL2
        {
            get { return billRemarkL2; }
            set { billRemarkL2 = value; }
        } 

        public string BillRemarkL3
        {
            get { return billRemarkL3; }
            set { billRemarkL3 = value; }
        }


    }
}
