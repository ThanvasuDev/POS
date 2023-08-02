using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class MapSS
    {
        int mapSSID;
        int productCatID;
        string catName;
        int productID;
        string productName;
        int storeID;
        string storeName;
        int mapproductID;
        string mapproductName;
        int mapstoreID;
        string mapstoreName;
   
        float storeQTY;
        string storeUnit;
        string productUnit;

 

        public MapSS() { }

        public MapSS(int mapSSID, int productCatID, string catName, int productID, string productName, int storeID, string storeName, int mapproductID, string mapproductName, int mapstoreID, string mapstoreName, float storeQTY, string storeUnit, string productUnit)
        {
            MapSSID = mapSSID;
            ProductCatID = productCatID;
            CatName = catName;
            ProductID = productID;
            ProductName = productName;
            StoreID = storeID;
            StoreName = storeName;
            MapproductID = mapproductID;
            MapproductName = mapproductName;
            MapstoreID = mapstoreID;
            MapstoreName = mapstoreName; 
            StoreQTY = storeQTY;
            StoreUnit = storeUnit;
            ProductUnit = productUnit;

        }

        public int MapSSID
        {
            get { return mapSSID; }
            set { mapSSID = value; }
        }


        public int ProductCatID
        {
            get { return productCatID; }
            set { productCatID = value; }
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

        public int MapproductID
        {
            get { return mapproductID; }
            set { mapproductID = value; }
        }

        public string MapproductName
        {
            get { return mapproductName; }
            set { mapproductName = value; }
        }

        public int MapstoreID
        {
            get { return mapstoreID; }
            set { mapstoreID = value; }
        }


        public string MapstoreName
        {
            get { return mapstoreName; }
            set { mapstoreName = value; }
        } 

        public float StoreQTY
        {
            get { return storeQTY; }
            set { storeQTY = value; }
        }


        public string StoreUnit
        {
            get { return storeUnit; }
            set { storeUnit = value; }
        }

        public string ProductUnit
        {
            get { return productUnit; }
            set { productUnit = value; }
        }

    }
}
