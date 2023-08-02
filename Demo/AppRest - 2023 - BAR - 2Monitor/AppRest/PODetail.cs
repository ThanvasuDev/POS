using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class PODetail
    {

        string poOrderNo;

 
        int addStockID;
        string addStockDate;
        int storeID; 
        string storeName; 
        float storeBigQTY;


        string storeBigUnit;


        float storeQTY;
        string storeUnit;


        float storeAmount;
        float storeVat;

        string storeRemark; 
        string addStockType;
        DateTime createDate;


        public PODetail(string poOrderNo,int addStockID, string addStockDate, int storeID , string storeName, float storeBigQTY , string storeBigUnit, float storeQTY, string storeUnit , float storeAmount,float storeVat, string storeRemark, string addStockType, DateTime createDate)
        {
            PoOrderNo = poOrderNo;
            AddStockID = addStockID;
            AddStockDate = addStockDate;
            StoreID = storeID; 
            StoreName = storeName;
            StoreBigQTY = storeBigQTY;
             StoreBigUnit = storeBigUnit;
            StoreQTY = storeQTY; 
            StoreUnit = storeUnit;
            StoreAmount = storeAmount;
            StoreVat = storeVat;
            StoreRemark = storeRemark;
            AddStockType = addStockType;
            CreateDate = createDate;
        }

        public PODetail(int addStockID, string storeName, float storeQTY, DateTime createDate)
        {

            AddStockID = addStockID; 
            StoreName = storeName;
            StoreQTY = storeQTY;
            CreateDate = createDate;
        }

        public string PoOrderNo
        {
            get { return poOrderNo; }
            set { poOrderNo = value; }
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

 


        public string StoreName
        {
            get { return storeName; }
            set { storeName = value; }
        }

public float StoreBigQTY
{
  get { return storeBigQTY; }
  set { storeBigQTY = value; }
}

public string StoreBigUnit
{
  get { return storeBigUnit; }
  set { storeBigUnit = value; }
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

        public float StoreAmount
        {
            get { return storeAmount; }
            set { storeAmount = value; }
        }



        public float StoreVat
        {
            get { return storeVat; }
            set { storeVat = value; }
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
