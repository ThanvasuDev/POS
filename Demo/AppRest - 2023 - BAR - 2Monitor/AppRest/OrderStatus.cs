using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class OrderStatus
    {
        int trnID;
        string orderQ;
        string orderBarcode;

        int tableID;
        string tableName;
        int tableZoneID;
        string zoneName;
        string orderName;
        string orderRemark;
        float orderAmt;
        float orderCal;
        DateTime getOrderTime;
        string getOrderBy;
        string cookOrderBy;
        DateTime cookOrderTime;
        int kitchenID;


        public OrderStatus(int trnID, string orderQ, string orderBarcode, int tableID, string tableName, int tableZoneID, string ZoneName, string orderName, string orderRemark, float orderAmt, float orderCal, DateTime getOrderTime, string getOrderBy, string cookOrderBy, DateTime cookOrderTime, int kitchenID)
        {

            TrnID = trnID;
            OrderQ = orderQ;
            OrderBarcode = orderBarcode;
            TableID = tableID;
            TableName = tableName;
            TableZoneID = tableZoneID;
            ZoneName = zoneName;
            OrderName = orderName;
            OrderRemark = orderRemark;
            OrderAmt = orderAmt;
            OrderCal = orderCal;
            GetOrderTime = getOrderTime;
            GetOrderBy = getOrderBy;
            CookOrderBy = cookOrderBy;
            CookOrderTime = cookOrderTime;
            KitchenID = kitchenID;
        }


        public int TrnID
        {
            get { return trnID; }
            set { trnID = value; }
        } 

        public string OrderQ
        {
            get { return orderQ; }
            set { orderQ = value; }
        }


        public string OrderBarcode
        {
            get { return orderBarcode; }
            set { orderBarcode = value; }
        }
        public int TableID
        {
            get { return tableID; }
            set { tableID = value; }
        } 

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        public int TableZoneID
        {
            get { return tableZoneID; }
            set { tableZoneID = value; }
        } 

        public string ZoneName
        {
            get { return zoneName; }
            set { zoneName = value; }
        } 

        public string OrderName
        {
            get { return orderName; }
            set { orderName = value; }
        } 

        public string OrderRemark
        {
            get { return orderRemark; }
            set { orderRemark = value; }
        } 

        public float OrderAmt
        {
            get { return orderAmt; }
            set { orderAmt = value; }
        } 

        public float OrderCal
        {
            get { return orderCal; }
            set { orderCal = value; }
        } 

        public DateTime GetOrderTime
        {
            get { return getOrderTime; }
            set { getOrderTime = value; }
        } 

        public string GetOrderBy
        {
            get { return getOrderBy; }
            set { getOrderBy = value; }
        } 

        public string CookOrderBy
        {
            get { return cookOrderBy; }
            set { cookOrderBy = value; }
        } 

        public DateTime CookOrderTime
        {
            get { return cookOrderTime; }
            set { cookOrderTime = value; }
        }


        public int KitchenID
        {
            get { return kitchenID; }
            set { kitchenID = value; }
        }
       
    }
}
