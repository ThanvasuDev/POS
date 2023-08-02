using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class ProductStock
    {

        int productID; 
        int catID; 
        string catName; 
        string productName; 
        string stockItem;
        string stockMap;

    


        public ProductStock()
        {  }

        public ProductStock(int catID, string catName)
        {
            CatID = catID;
            CatName = catName;
        }

        public ProductStock(int productID, int catID, string catName, string productName, string stockItem, string stockMap)
        {
            ProductID = productID;
            CatID = catID;
            CatName = catName;
            ProductName = productName;
            StockItem = stockItem;
            StockMap = stockMap;
        }

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
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

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public string StockItem
        {
            get { return stockItem; }
            set { stockItem = value; }
        }

        public string StockMap
        {
            get { return stockMap; }
            set { stockMap = value; }
        }

    }
}
