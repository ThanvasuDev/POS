using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class CustPayment
    {
        int custID;
        string custName;
        string creditAmount;
        string payAmount;
        string balanceAmount;

        public CustPayment() { }

        public CustPayment(int custID, string custName, string creditAmount, string payAmount, string balanceAmount)
        {
            CustID = custID;
            CustName = custName;
            CreditAmount = creditAmount;
            PayAmount = payAmount;
            BalanceAmount = balanceAmount;
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

        public string CreditAmount
        {
            get { return creditAmount; }
            set { creditAmount = value; }
        }

        public string PayAmount
        {
            get { return payAmount; }
            set { payAmount = value; }
        }
        
        public string BalanceAmount
        {
            get { return balanceAmount; }
            set { balanceAmount = value; }
        }
    }
}
