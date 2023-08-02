using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    public class Order
    {
        int tableID;
        int catID;
        string catName;
        int productID;
        string productName;
        float productPrice;
        float orderQTY;
        float orderAmount;
        string createDate; 
        int flagsend;
        int orderNo;
        int productCatID;
        string productCatName;
        string productDesc;
        string orderBarcode;
        float remarkOrderAmt;
        string timetoOrder; 
        int flagNonServiceCharge; 
        int flagNonVAT; 
        int flagNonDiscount; 
        int stdTime;
        int discountGroupID;
        string productBarcode;
        float producrPriceM;
        float orderAddAmt;
        float orderProductPoint;
      


        public Order(int tableID, int catID, string catName, int productID, string productName, float productPrice, float orderQTY, float orderAmount, string createDate, int flagsend, int orderNo, int productCatID, string productCatName, string productDesc, string orderBarcode, float remarkOrderAmt, string timetoOrder, int flagNonServiceCharge, int flagNonVAT, int flagNonDiscount, int stdTime, int discountGroupID  ,  string productBarcode,  float producrPriceM, float orderAddAmt,float orderProductPoint)
        {
            TableID = tableID;
            CatID = catID;
            CatName = catName;
            ProductID = productID;
            ProductName = productName;
            ProductPrice = productPrice;
            OrderQTY = orderQTY;
            OrderAmount = orderAmount;
            CreateDate = createDate;
            Flagsend = flagsend;
            OrderNo = orderNo;
            ProductCatID = productCatID;
            ProductCatName = productCatName;
            ProductDesc = productDesc;
            OrderBarcode = orderBarcode;
            RemarkOrderAmt = remarkOrderAmt;
            TimetoOrder = timetoOrder;
            FlagNonServiceCharge = flagNonServiceCharge;
            FlagNonVAT = flagNonVAT;
            FlagNonDiscount = flagNonDiscount;
            StdTime = stdTime;
            DiscountGroupID = discountGroupID;
            ProductBarcode = productBarcode;
            ProducrPriceM = producrPriceM;
            OrderAddAmt = orderAddAmt;
            OrderProductPoint = orderProductPoint;
             
        }


        public int TableID
        {
            get { return tableID; }
            set { tableID = value; }
        }

        public int CatID
        {
            get { return catID; }
            set { catID = value; }
        }


        public string CatName
        {
            get { return catName; }
            set { catName = value; }
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


        public float ProductPrice
        {
            get { return productPrice; }
            set { productPrice = value; }
        }


        public string ProductDesc
        {
            get { return productDesc; }
            set { productDesc = value; }
        }



        public float OrderQTY
        {
            get { return orderQTY; }
            set { orderQTY = value; }
        }

        public float OrderAmount
        {
            get { return orderAmount; }
            set { orderAmount = value; }
        }



        public string CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }



        public int Flagsend
        {
            get { return flagsend; }
            set { flagsend = value; }
        }


        public int OrderNo
        {
            get { return orderNo; }
            set { orderNo = value; }
        }


        public int ProductCatID
        {
            get { return productCatID; }
            set { productCatID = value; }
        }


        public string ProductCatName
        {
            get { return productCatName; }
            set { productCatName = value; }
        }

        public string OrderBarcode
        {
            get { return orderBarcode; }
            set { orderBarcode = value; }
        }

        public float RemarkOrderAmt
        {
            get { return remarkOrderAmt; }
            set { remarkOrderAmt = value; }
        }

        public string TimetoOrder
        {
            get { return timetoOrder; }
            set { timetoOrder = value; }
        }


        public int StdTime
        {
            get { return stdTime; }
            set { stdTime = value; }
        }

        public int FlagNonDiscount
        {
            get { return flagNonDiscount; }
            set { flagNonDiscount = value; }
        }

        public int FlagNonVAT
        {
            get { return flagNonVAT; }
            set { flagNonVAT = value; }
        }

        public int FlagNonServiceCharge
        {
            get { return flagNonServiceCharge; }
            set { flagNonServiceCharge = value; }
        }

        public int DiscountGroupID
        {
            get { return discountGroupID; }
            set { discountGroupID = value; }
        }

        public string ProductBarcode
        {
            get { return productBarcode; }
            set { productBarcode = value; }
        }

        public float ProducrPriceM
        {
            get { return producrPriceM; }
            set { producrPriceM = value; }
        }

        public float OrderAddAmt
        {
            get { return orderAddAmt; }
            set { orderAddAmt = value; }
        }


        public float OrderProductPoint
        {
            get { return orderProductPoint; }
            set { orderProductPoint = value; }
        }


    }
}
