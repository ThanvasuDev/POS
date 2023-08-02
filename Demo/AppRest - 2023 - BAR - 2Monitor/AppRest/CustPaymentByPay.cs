using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class CustPaymentByPay
    {

        int custPaymentID;
        int trnID;
        string payDate;
        float billAmount;
        float payAmount;
        float creditAmount;
        string creditCustPayRemark;


        public CustPaymentByPay(int custPaymentID, int trnID, string payDate, float billAmount, float payAmount, float creditAmount, string creditCustPayRemark)
        {
            CustPaymentID = custPaymentID;
            TrnID = trnID;
            PayDate = payDate;
            BillAmount = billAmount;
            PayAmount = payAmount;
            CreditAmount = creditAmount;
            CreditCustPayRemark = creditCustPayRemark;
        }

        public int CustPaymentID
        {
            get { return custPaymentID; }
            set { custPaymentID = value; }
        }
        

        public int TrnID
        {
            get { return trnID; }
            set { trnID = value; }
        }
       

        public string PayDate
        {
            get { return payDate; }
            set { payDate = value; }
        }
       

        public float BillAmount
        {
            get { return billAmount; }
            set { billAmount = value; }
        }
       

        public float PayAmount
        {
            get { return payAmount; }
            set { payAmount = value; }
        }
        

        public float CreditAmount
        {
            get { return creditAmount; }
            set { creditAmount = value; }
        }
        

        public string CreditCustPayRemark
        {
            get { return creditCustPayRemark; }
            set { creditCustPayRemark = value; }
        }
            



    }
}
