using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Table
    {
         int zoneID;
         string zoneServiceCharge; 
         int tableID;
         string tableName;
         string tableFlagUse;
         float orderQTY;
         float orderAmount;
         int tablePrintBill;
         int tableCustID;
         string tableZoneType;
         string qROrder;

        int discountType;
        int discountPer;
        int discountAmt;
        float tax;
        float serviceCharge;
        string remark;
        string strMemSearch;
        string tableZoneVAT;
        int tableZonePriceID;

         


        List<Order> order;


        public Table()
        {
        }


        public Table(int zoneID, int tableID, string tableName, float orderQTY, string tableFlagUse, int tablePrintBill, int tableCustID, string tableZoneType)
        {
            ZoneID = zoneID;
            TableID = tableID;
            TableName = tableName;
            OrderQTY = orderQTY;
            TableFlagUse = tableFlagUse;
            TablePrintBill = tablePrintBill;
            TableCustID = tableCustID;
            TableZoneType = tableZoneType;
           
        }

        public Table(int zoneID,string zoneServiceCharge, int tableID, string tableName, float orderQTY, string tableFlagUse, int tablePrintBill, int tableCustID, int discountType, int discountPer, int discountAmt, float tax, float serviceCharge, string remark, string strMemSearch , string tableZoneVAT, int tableZonePriceID)
        {
            ZoneID = zoneID;
            ZoneServiceCharge = zoneServiceCharge;
            TableID = tableID;
            TableName = tableName;
            OrderQTY = orderQTY;
            TableFlagUse = tableFlagUse;
            TablePrintBill = tablePrintBill;
            TableCustID = tableCustID;
            DiscountType = discountType;
            DiscountPer = discountPer;
            DiscountAmt = discountAmt;
            Tax = tax;
            ServiceCharge = serviceCharge;
            Remark = remark;
            StrMemSearch = strMemSearch;
            TableZoneVAT = tableZoneVAT;
            TableZonePriceID = tableZonePriceID;

        }

        public Table(int zoneID, int tableID, string tableName, string tableFlagUse, float orderQTY, float orderAmount, List<Order> order ,string qROrder)
        {
            ZoneID = zoneID;
            TableID = tableID;
            TableName = tableName;
            TableFlagUse = tableFlagUse;
            OrderQTY = orderQTY;
            OrderAmount = orderAmount;
            Order = order;
            QROrder = qROrder;
        }


        public int ZoneID
        {
            get { return zoneID; }
            set { zoneID = value; }
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

        public string TableFlagUse
        {
            get { return tableFlagUse; }
            set { tableFlagUse = value; }
        }


        public int TablePrintBill
        {
            get { return tablePrintBill; }
            set { tablePrintBill = value; }
        }


        public int TableCustID
        {
            get { return tableCustID; }
            set { tableCustID = value; }
        }

        public float OrderQTY
        {
            get { return orderQTY; }
            set { orderQTY = value; }
        }

        public float OrderAmount
        {
            get { return orderAmount; }
            set { orderAmount = value; }
        }

        internal List<Order> Order
        {
            get { return order; }
            set { order = value; }
        }


        public int DiscountType
        {
            get { return discountType; }
            set { discountType = value; }
        }

        public int DiscountPer
        {
            get { return discountPer; }
            set { discountPer = value; }
        }

        public int DiscountAmt
        {
            get { return discountAmt; }
            set { discountAmt = value; }
        }


        public float Tax
        {
            get { return tax; }
            set { tax = value; }
        }


        public float ServiceCharge
        {
            get { return serviceCharge; }
            set { serviceCharge = value; }
        }


        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public string StrMemSearch
        {
            get { return strMemSearch; }
            set { strMemSearch = value; }
        }

        public string ZoneServiceCharge
        {
            get { return zoneServiceCharge; }
            set { zoneServiceCharge = value; }
        }

        public string TableZoneVAT
        {
            get { return tableZoneVAT; }
            set { tableZoneVAT = value; }
        }

        public int TableZonePriceID
        {
            get { return tableZonePriceID; }
            set { tableZonePriceID = value; }
        }

        public string TableZoneType
        {
            get { return tableZoneType; }
            set { tableZoneType = value; }
        }

        public string QROrder
        {
            get { return qROrder; }
            set { qROrder = value; }
        }

    }
}
