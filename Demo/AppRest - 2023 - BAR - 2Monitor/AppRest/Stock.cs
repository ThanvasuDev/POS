using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Stock
    {
        string pOOrderNo; 
        int addStockID;
        string addStockDate;
        int storeID;
        int storeLotID; 
        string storeName;
        float storeBigQTY;
        string bigUnit; 
        float storeQTY;
        string unit; 
        float storeAmount; 
        string storeRemark; 
        string addStockType;
        DateTime createDate;


        public Stock(string pOOrderNo, int addStockID, string addStockDate, int storeID, int storeLotID, string storeName,  float storeBigQTY ,string bigUnit,  float storeQTY, string unit, float storeAmount, string storeRemark, string addStockType, DateTime createDate)
        {
            POOrderNo = pOOrderNo;
            AddStockID = addStockID;
            AddStockDate = addStockDate;
            StoreID = storeID;
            StoreLotID = storeLotID;
            StoreName = storeName;
            StoreQTY = storeQTY;
            StoreBigQTY = storeBigQTY;
            BigUnit = bigUnit;
            Unit = unit;
            StoreAmount = storeAmount;
            StoreRemark = storeRemark;
            AddStockType = addStockType;
            CreateDate = createDate;
        }

        public Stock(string pOOrderNo, int addStockID, int storeID, string storeName, float storeQTY, DateTime createDate)
        {
            POOrderNo = pOOrderNo;
            AddStockID = addStockID;
            StoreID = storeID;
            StoreName = storeName;
            StoreQTY = storeQTY;
            CreateDate = createDate;
        }


        public string POOrderNo
        {
            get { return pOOrderNo; }
            set { pOOrderNo = value; }
        }


        public int AddStockID
        {
            get { return addStockID; }
            set { addStockID = value; }
        }
         

        public string AddStockDate
        {
            get { return addStockDate; }
            set { addStockDate = value; }
        }

        public int StoreID
        {
            get { return storeID; }
            set { storeID = value; }
        }


        public int StoreLotID
        {
            get { return storeLotID; }
            set { storeLotID = value; }
        }


        public string StoreName
        {
            get { return storeName; }
            set { storeName = value; }
        }

        public float StoreQTY
        {
            get { return storeQTY; }
            set { storeQTY = value; }
        }

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }


        public float StoreBigQTY
        {
            get { return storeBigQTY; }
            set { storeBigQTY = value; }
        }

        public string BigUnit
        {
            get { return bigUnit; }
            set { bigUnit = value; }
        }

        public float StoreAmount
        {
            get { return storeAmount; }
            set { storeAmount = value; }
        }

        public string AddStockType
        {
            get { return addStockType; }
            set { addStockType = value; }
        }


        public string StoreRemark
        {
            get { return storeRemark; }
            set { storeRemark = value; }
        }

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }
}
