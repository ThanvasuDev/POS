using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Payment
    {

        int trnID;
        string payDate;
        int payTypeID;
        string payTypeName;
        float payAmount;
        int payCustID;  // ลูกค้าลงบิล
        string payCustName; // ลูกค้าลงบิล
        string payRemark; // ลูกค้าลงบิล
        string payCreditType; // ลูกค้าบัตรเครดิต
        string payCreditNo; //   ลูกค้าบัตรเครดิต
        string payCreditName;  // ลูกค้าบัตรเครดิต

        string orderDateTime;
        string payDateTime;
        int tableID;
        string tableName;

        int trnInvID;

  


        public Payment()
        {

        }

        public Payment(int trnID, string payDate,float payAmount, int payCustID, string payCustName, string payRemark, string orderDateTime, string payDateTime, int tableID, string tableName, int trnInvID)
        {
            TrnID = trnID;
            PayDate = payDate;
            PayAmount = payAmount;
            PayCustID = payCustID;
            PayCustName = payCustName;
            PayRemark = payRemark;
            OrderDateTime = orderDateTime;
            PayDateTime = payDateTime;
            TableID = tableID;
            TableName = tableName;
            TrnInvID = trnInvID;
        }

        public int TrnID
        {
            get { return trnID; }
            set { trnID = value; }
        }

        public int TrnInvID
        {
            get { return trnInvID; }
            set { trnInvID = value; }
        }
        public string PayDate
        {
            get { return payDate; }
            set { payDate = value; }
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

        public float PayAmount
        {
            get { return payAmount; }
            set { payAmount = value; }
        }

        public int PayCustID
        {
            get { return payCustID; }
            set { payCustID = value; }
        }


        public string PayCustName
        {
            get { return payCustName; }
            set { payCustName = value; }
        }

        public string PayRemark
        {
            get { return payRemark; }
            set { payRemark = value; }
        }


        public string PayCreditType
        {
            get { return payCreditType; }
            set { payCreditType = value; }
        }


        public string PayCreditNo
        {
            get { return payCreditNo; }
            set { payCreditNo = value; }
        }

        public string PayCreditName
        {
            get { return payCreditName; }
            set { payCreditName = value; }
        }

        public string OrderDateTime
        {
            get { return orderDateTime; }
            set { orderDateTime = value; }
        }


        public string PayDateTime
        {
            get { return payDateTime; }
            set { payDateTime = value; }
        }


        public int TableID
        {
            get { return tableID; }
            set { tableID = value; }
        }

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

    }
}
