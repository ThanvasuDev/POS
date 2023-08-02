using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class PromDetail
    {
        int promID;
        int promSegNo;
        int productGroupID;
        string productGroupName; 
        string setPriceGroup;
        float setPrice;
        float discountPeritem;
        float discountAmtitem;


        public PromDetail(int promID, int promSegNo, int productGroupID,string productGroupName , string setPriceGroup, float setPrice, float discountPeritem, float discountAmtitem)
        {
            PromID = promID;
            PromSegNo = promSegNo;
            ProductGroupID = productGroupID;
            ProductGroupName = productGroupName;
            SetPriceGroup = setPriceGroup;
            SetPrice = setPrice;  
            DiscountPeritem = discountPeritem;
            DiscountAmtitem = discountAmtitem;
             
        }
         
        public int PromID
        {
            get { return promID; }
            set { promID = value; }
        } 

        public int PromSegNo
        {
            get { return promSegNo; }
            set { promSegNo = value; }
        } 

        public int ProductGroupID
        {
            get { return productGroupID; }
            set { productGroupID = value; }
        }


        public string ProductGroupName
        {
            get { return productGroupName; }
            set { productGroupName = value; }
        }

        public string SetPriceGroup
        {
            get { return setPriceGroup; }
            set { setPriceGroup = value; }
        } 

        public float SetPrice
        {
            get { return setPrice; }
            set { setPrice = value; }
        } 

        public float DiscountPeritem
        {
            get { return discountPeritem; }
            set { discountPeritem = value; }
        } 

        public float DiscountAmtitem
        {
            get { return discountAmtitem; }
            set { discountAmtitem = value; }
        }


    }
}
