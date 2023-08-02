using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    public class BillPayment
    {
         int paySeqNo;
         int paytypeID;
         string paytypeName;
         float payAmount;
         string payDesc1;
         string payDesc2;
         string payDesc3;

        public BillPayment()
         { }

        public BillPayment(int paySeqNo, int paytypeID, string paytypeName, float payAmount, string payDesc1, string payDesc2, string payDesc3)
        {
            PaySeqNo = paySeqNo;
            PaytypeID = paytypeID;
            PaytypeName = paytypeName;
            PayAmount = payAmount;
            PayDesc1 = payDesc1;
            PayDesc2 = payDesc2;
            PayDesc3 = payDesc3; 
        }


        public int PaySeqNo
        {
            get { return paySeqNo; }
            set { paySeqNo = value; }
        }

        public int PaytypeID
        {
            get { return paytypeID; }
            set { paytypeID = value; }
        }

        public string PaytypeName
        {
            get { return paytypeName; }
            set { paytypeName = value; }
        }

        public float PayAmount
        {
            get { return payAmount; }
            set { payAmount = value; }
        }

        public string PayDesc1
        {
            get { return payDesc1; }
            set { payDesc1 = value; }
        }

        public string PayDesc2
        {
            get { return payDesc2; }
            set { payDesc2 = value; }
        }
        

        public string PayDesc3
        {
            get { return payDesc3; }
            set { payDesc3 = value; }
        }

        



    }
}
