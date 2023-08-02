using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class StoreTake
    {
        int storeID;
        int storeCatID;
        string storeName;
        string storeUnit;
        string storeConvertUnit;
        float storeConvertRate;
        float storeCost;
        float storeAvgCost;
        float storeKPILowStock;
        float storeKPIOverStock;
        string storeOrder;
        string storeBarcode;
        string storeFlagUse;


        string storeGroupCat;
        string storeCat;

        float takeUnit;
        float takeBigUnit;



  
        public StoreTake()
        {

        }

        public StoreTake(int storeCatID,string storeGroupCat ,string storeCat,  string storeBarcode,int storeID, string storeName,float takeUnit, string storeUnit,float takeBigUnit, string storeConvertUnit, float storeConvertRate, float storeCost )
        {
            
            StoreCatID = storeCatID;
            StoreGroupCat = storeGroupCat;
            StoreCat = storeCat;
            StoreBarcode = storeBarcode;
            StoreID = storeID;
            StoreName = storeName;
            TakeUnit = takeUnit;
            StoreUnit = storeUnit;
            TakeBigUnit = takeBigUnit;
            StoreConvertUnit = storeConvertUnit;
            StoreConvertRate = storeConvertRate;
            StoreCost = storeCost; 
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

        public float StoreCost
        {
            get { return storeCost; }
            set { storeCost = value; }
        }


        public float StoreKPIOverStock
        {
            get { return storeKPIOverStock; }
            set { storeKPIOverStock = value; }
        }

        public float StoreKPILowStock
        {
            get { return storeKPILowStock; }
            set { storeKPILowStock = value; }
        }


        public string StoreOrder
        {
            get { return storeOrder; }
            set { storeOrder = value; }
        }
        public string StoreFlagUse
        {
            get { return storeFlagUse; }
            set { storeFlagUse = value; }
        }


        public string StoreBarcode
        {
            get { return storeBarcode; }
            set { storeBarcode = value; }
        }
        public float StoreConvertRate
        {
            get { return storeConvertRate; }
            set { storeConvertRate = value; }
        }
        public string StoreConvertUnit
        {
            get { return storeConvertUnit; }
            set { storeConvertUnit = value; }
        }
        public float StoreAvgCost
        {
            get { return storeAvgCost; }
            set { storeAvgCost = value; }
        }

        public int StoreCatID
        {
            get { return storeCatID; }
            set { storeCatID = value; }
        }

        public string StoreGroupCat
        {
            get { return storeGroupCat; }
            set { storeGroupCat = value; }
        }


        public string StoreCat
        {
            get { return storeCat; }
            set { storeCat = value; }
        }

        public float TakeUnit
        {
            get { return takeUnit; }
            set { takeUnit = value; }
        }

        public float TakeBigUnit
        {
            get { return takeBigUnit; }
            set { takeBigUnit = value; }
        }

    }

}
