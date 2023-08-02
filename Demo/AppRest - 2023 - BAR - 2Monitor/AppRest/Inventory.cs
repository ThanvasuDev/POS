using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Inventory
    {
        int inventoryID; 
        string inventoryName;
        string flagSuplier;
        string suplierCode; 
        string suplierCompanyName; 
        string suplierAddr;
        string suplierTaxID;
        string suplierTelNo;
        string suplierFaxNo;
        float suplierPercentGP;
    
        string remark;
        string flagUse;

        public Inventory(int inventoryID, string inventoryName, string flagSuplier, string suplierCode, string suplierCompanyName, string suplierAddr, string suplierTaxID, string suplierTelNo, string suplierFaxNo, float suplierPercentGP, string remark, string flagUse)
        {
            InventoryID = inventoryID;
            InventoryName = inventoryName;
            FlagSuplier = flagSuplier;
            SuplierCode = suplierCode;
            SuplierCompanyName = suplierCompanyName;
            SuplierAddr = suplierAddr;
            SuplierTaxID = suplierTaxID;
            SuplierTelNo = suplierTelNo;
            SuplierFaxNo = suplierFaxNo;
            SuplierPercentGP = suplierPercentGP;
            Remark = remark;
            FlagUse = flagUse;
        }


        public int InventoryID
        {
            get { return inventoryID; }
            set { inventoryID = value; }
        }

        public string InventoryName
        {
            get { return inventoryName; }
            set { inventoryName = value; }
        }


        public string FlagSuplier
        {
            get { return flagSuplier; }
            set { flagSuplier = value; }
        }

        public string SuplierAddr
        {
            get { return suplierAddr; }
            set { suplierAddr = value; }
        }

        public string SuplierTaxID
        {
            get { return suplierTaxID; }
            set { suplierTaxID = value; }
        }

        public string SuplierTelNo
        {
            get { return suplierTelNo; }
            set { suplierTelNo = value; }
        }

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public string FlagUse
        {
            get { return flagUse; }
            set { flagUse = value; }
        }

        public string SuplierCode
        {
            get { return suplierCode; }
            set { suplierCode = value; }
        }

        public string SuplierCompanyName
        {
            get { return suplierCompanyName; }
            set { suplierCompanyName = value; }
        }

        public string SuplierFaxNo
        {
            get { return suplierFaxNo; }
            set { suplierFaxNo = value; }
        }


        public float SuplierPercentGP
        {
            get { return suplierPercentGP; }
            set { suplierPercentGP = value; }
        }
    }
}
