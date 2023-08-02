using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Product
    {
        int productID;
        int productCatID; 
        string productName;
        string productNameEN;
        string productUnit;
        string productColour;


        string productDesc;
        float productPrice;
        float productPrice2; 
        float productPrice3;
        float productPrice4;
        float productPrice5;
       
        float productCost; // New

 
        string productBarcode; 
        string productFlagUse;
        string productFlagStock;
        string productStockType;
        int productPromID;
        int productGetPoint;
        float productWeight;
        int productBestBefore;
        int productPrinterNo; // New
        int productPrinterNo2; // New


        int productSuplierID; // New
        float productConPercent; // New


        int flagNonServiceCharge;
        int flagNonVAT;
        int flagNonDiscount;
        int stdTime;

        int flagDelivery;
        int flagCRM;
        string crmCPType;
        string crmCouponSynTax;
        string crmImgUrl1;
        string crmImgUrl2;
        string crmPeriodTime;
        string crmStore;
        string crmTC;

        int flagQROrder;

         
        public Product()
        {

        }

        public Product(int productID, string productDesc)
        {
            ProductID = productID;
            ProductDesc = productDesc; // SynTax Substring(1,4) = 'FOR:'
        }

        public Product(int productID, int productCatID, string productName, string productNameEN,  string productUnit, string productDesc, string productColour,  float productPrice, float productPrice2, float productPrice3, float productPrice4, float productPrice5,  float productCost, string productFlagUse, string productBarcode, string productFlagStock, string productStockType, int productPromID, int productGetPoint, float productWeight, int productBestBefore, int productPrinterNo, int productPrinterNo2, int productSuplierID, float productConPercent, int flagNonServiceCharge, int flagNonVAT, int flagNonDiscount, int stdTime , int flagDelivery , int flagCRM,
                        string crmCPType,  string crmImgUrl1, string crmImgUrl2, string crmCouponSynTax, string crmPeriodTime, string crmStore, string crmTC  , int flagQROrder)
        {
            ProductID = productID;
            ProductCatID = productCatID;
            ProductName = productName;
            ProductNameEN = productNameEN;
            ProductUnit = productUnit;
            ProductDesc = productDesc;
            ProductColour = productColour;

            
            ProductPrice = productPrice;
            ProductPrice2 = productPrice2;
            ProductPrice3 = productPrice3;
            ProductPrice4 = productPrice4;
            ProductPrice5 = productPrice5;

            ProductFlagUse = productFlagUse;
            ProductBarcode = productBarcode; 
            ProductFlagStock = productFlagStock;
            ProductStockType = productStockType;
            ProductPromID = productPromID;
            ProductGetPoint = productGetPoint;
            ProductWeight = productWeight;
            ProductBestBefore = productBestBefore;
            ProductCost = productCost;
            ProductPrinterNo = productPrinterNo;
            ProductPrinterNo2 = productPrinterNo2;
            ProductSuplierID = productSuplierID;
            ProductConPercent = productConPercent;

            FlagNonServiceCharge = flagNonServiceCharge;
            FlagNonVAT = flagNonVAT;
            FlagNonDiscount = flagNonDiscount;
            StdTime = stdTime;


            FlagDelivery =  flagDelivery;
            FlagCRM =  flagCRM;
            CrmCPType = crmCPType; 
            CrmImgUrl1 = crmImgUrl1;
            CrmImgUrl2 = crmImgUrl2;
            CrmCouponSynTax = crmCouponSynTax;
            CrmPeriodTime = crmPeriodTime;
            CrmStore = crmStore;
            CrmTC = crmTC;

            FlagQROrder = flagQROrder;
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


        public int ProductCatID
        {
            get { return productCatID; }
            set { productCatID = value; }
        }

        public string ProductUnit
        {
            get { return productUnit; }
            set { productUnit = value; }
        }


        public string ProductDesc
        {
            get { return productDesc; }
            set { productDesc = value; }
        }

        public string ProductColour
        {
            get { return productColour; }
            set { productColour = value; }
        }

        public float ProductPrice
        {
            get { return productPrice; }
            set { productPrice = value; }
        }


        public float ProductPrice2
        {
            get { return productPrice2; }
            set { productPrice2 = value; }
        }

        public float ProductPrice3
        {
            get { return productPrice3; }
            set { productPrice3 = value; }
        }

 
        public float ProductPrice4
        {
            get { return productPrice4; }
            set { productPrice4 = value; }
        }


        public float ProductPrice5
        {
            get { return productPrice5; }
            set { productPrice5 = value; }
        }

        public string ProductFlagUse
        {
            get { return productFlagUse; }
            set { productFlagUse = value; }
        }


        public string ProductBarcode
        {
            get { return productBarcode; }
            set { productBarcode = value; }
        }

        public string ProductFlagStock
        {
            get { return productFlagStock; }
            set { productFlagStock = value; }
        }

        public string ProductStockType
        {
            get { return productStockType; }
            set { productStockType = value; }
        }

        public int ProductPromID
        {
            get { return productPromID; }
            set { productPromID = value; }
        }


        public int ProductGetPoint
        {
            get { return productGetPoint; }
            set { productGetPoint = value; }
        }

        public float ProductWeight
        {
            get { return productWeight; }
            set { productWeight = value; }
        }


        public int ProductBestBefore
        {
            get { return productBestBefore; }
            set { productBestBefore = value; }
        }


        public float ProductCost
        {
            get { return productCost; }
            set { productCost = value; }
        }

        public int ProductPrinterNo
        {
            get { return productPrinterNo; }
            set { productPrinterNo = value; }
        }

        public int ProductPrinterNo2
        {
            get { return productPrinterNo2; }
            set { productPrinterNo2 = value; }
        }


        public int ProductSuplierID
        {
            get { return productSuplierID; }
            set { productSuplierID = value; }
        }


        public float ProductConPercent
        {
            get { return productConPercent; }
            set { productConPercent = value; }
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

        public int FlagDelivery
        {
            get { return flagDelivery; }
            set { flagDelivery = value; }
        }


        public int FlagCRM
        {
            get { return flagCRM; }
            set { flagCRM = value; }
        }


        public string CrmCPType
        {
            get { return crmCPType; }
            set { crmCPType = value; }
        }


        public string CrmCouponSynTax
        {
            get { return crmCouponSynTax; }
            set { crmCouponSynTax = value; }
        }


        public string CrmImgUrl1
        {
            get { return crmImgUrl1; }
            set { crmImgUrl1 = value; }
        }


        public string CrmImgUrl2
        {
            get { return crmImgUrl2; }
            set { crmImgUrl2 = value; }
        }


        public string CrmPeriodTime
        {
            get { return crmPeriodTime; }
            set { crmPeriodTime = value; }
        }


        public string CrmStore
        {
            get { return crmStore; }
            set { crmStore = value; }
        }


        public string CrmTC
        {
            get { return crmTC; }
            set { crmTC = value; }
        }

        public int FlagQROrder
        {
            get { return flagQROrder; }
            set { flagQROrder = value; }
        }

    }
}
