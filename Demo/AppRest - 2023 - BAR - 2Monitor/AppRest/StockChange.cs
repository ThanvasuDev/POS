using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class StockChange
    {

        int storeID;
        string storeName;
      
        float storeAddBigUnit;
        string storeBigUnit; 
        float storeAddUnit;
        string storeUnit;
        string storeRemark;
        float storeAmt;

        float storePrice;

    
        string flagAddStore;
        string codeRef;
        float storeConvertRate;

  
       


        public StockChange(int storeID, string storeName, float storeAddBigUnit, string storeBigUnit, float storeAddUnit, string storeUnit, float storeAmt, string storeRemark)
        {
            StoreID = storeID;
            StoreName = storeName;
            StoreAddBigUnit = storeAddBigUnit;
            StoreBigUnit = storeBigUnit;
            StoreAddUnit = storeAddUnit;
            StoreUnit = storeUnit;
            StoreAmt = storeAmt;
            StoreRemark = storeRemark;
        }


        public StockChange(int storeID, string storeName, float storeAddUnit, string storeUnit, float storePrice, float storeAmt, string storeRemark, string flagAddStore, string codeRef, float storeConvertRate)
        {
            StoreID = storeID;
            StoreName = storeName; 
            StoreAddUnit = storeAddUnit;
            StoreUnit = storeUnit;
            StoreAmt = storeAmt;
            StoreRemark = storeRemark;
            StorePrice = storePrice;
            FlagAddStore = flagAddStore;
            CodeRef = codeRef;
            StoreConvertRate = storeConvertRate;
        }



        public int StoreID
        {
            get { return storeID; }
            set { storeID = value; }
        }

        public string StoreName
        {
            get { return storeName; }
            set { storeName = value; }
        }

        public string StoreUnit
        {
            get { return storeUnit; }
            set { storeUnit = value; }
        }


        public string StoreBigUnit
        {
            get { return storeBigUnit; }
            set { storeBigUnit = value; }
        }

        public float StoreAddBigUnit
        {
            get { return storeAddBigUnit; }
            set { storeAddBigUnit = value; }
        }

        public float StoreAddUnit
        {
            get { return storeAddUnit; }
            set { storeAddUnit = value; }
        }


        public float StoreAmt
        {
            get { return storeAmt; }
            set { storeAmt = value; }
        }

        public string StoreRemark
        {
            get { return storeRemark; }
            set { storeRemark = value; }
        }


        public string FlagAddStore
        {
            get { return flagAddStore; }
            set { flagAddStore = value; }
        }


        public string CodeRef
        {
            get { return codeRef; }
            set { codeRef = value; }
        }

        public float StorePrice
        {
            get { return storePrice; }
            set { storePrice = value; }
        }

        public float StoreConvertRate
        {
            get { return storeConvertRate; }
            set { storeConvertRate = value; }
        }

    }
}
