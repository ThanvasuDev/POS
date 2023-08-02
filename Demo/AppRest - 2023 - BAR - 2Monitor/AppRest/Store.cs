using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Store
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
        string storeSupCode;

        public Store()
        {

        }

        public Store(int storeID, int storeCatID, string storeName, string storeUnit, string storeConvertUnit, float storeConvertRate, float storeCost, float storeAvgCost, float storeKPILowStock, float storeKPIOverStock, string storeOrder, string storeBarcode, string storeSupCode, string storeFlagUse)
        {
            StoreID = storeID;
            StoreCatID = storeCatID;
            StoreName = storeName;
            StoreUnit = storeUnit;
            StoreConvertUnit = storeConvertUnit;
            StoreConvertRate = storeConvertRate;
            StoreCost = storeCost;
            StoreAvgCost = storeAvgCost;
            StoreKPILowStock = storeKPILowStock;
            StoreKPIOverStock = storeKPIOverStock;
            StoreOrder = storeOrder;
            StoreBarcode = storeBarcode;
            StoreFlagUse = storeFlagUse;
            StoreSupCode = storeSupCode;
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

        public string StoreSupCode
        {
            get { return storeSupCode; }
            set { storeSupCode = value; }
        }


    }

}
