using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Transaction
    {
        int trnID;
        int groupCatID;

        int productID;
        string productName;
        string productNameEN;
        string trnRemark;


        float salesQTY;
        float salesAmount;


        public Transaction()
        {

        }

        public Transaction(int trnID, int groupCatID, int productID, string productName, string productNameEN,string trnRemark, float salesQTY, float salesAmount)
        {
            TrnID = trnID;
            GroupCatID = groupCatID;
            ProductID = productID;
            ProductName = productName;
            ProductNameEN = productNameEN;
            TrnRemark = trnRemark;
            SalesQTY = salesQTY;
            SalesAmount = salesAmount;

        } 

        public int TrnID
        {
            get { return trnID; }
            set { trnID = value; }
        }

        public int GroupCatID
        {
            get { return groupCatID; }
            set { groupCatID = value; }
        }

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public string ProductNameEN
        {
            get { return productNameEN; }
            set { productNameEN = value; }
        }


        public string TrnRemark
        {
            get { return trnRemark; }
            set { trnRemark = value; }
        }

        public float SalesQTY
        {
            get { return salesQTY; }
            set { salesQTY = value; }
        }


        public float SalesAmount
        {
            get { return salesAmount; }
            set { salesAmount = value; }
        }

    }
}
