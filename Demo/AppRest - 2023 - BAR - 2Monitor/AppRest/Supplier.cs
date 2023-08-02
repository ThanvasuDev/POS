using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Supplier
    {
        int supplierID;
        string supplierName; 
        string supplierCode;
        string supplierCompanyName;
        string supplierAddr;
        string supplierTaxID;
        string supplierTelNo;
        string supplierFaxNo;
        float supplierPercentGP; 
        string remark;
        string flagUse;


        public Supplier(int supplierID, string supplierName, string supplierCode, string supplierCompanyName, string supplierAddr, string supplierTaxID, string supplierTelNo, string supplierFaxNo, float supplierPercentGP, string remark, string flagUse)
        {
            SupplierID = supplierID;
            SupplierName = supplierName;
            SupplierCode = supplierCode;
            SupplierCompanyName = supplierCompanyName;
            SupplierAddr = supplierAddr;
            SupplierTaxID = supplierTaxID;
            SupplierTelNo = supplierTelNo;
            SupplierFaxNo = supplierFaxNo;
            SupplierPercentGP = supplierPercentGP;
            Remark = remark;
            FlagUse = flagUse;
        }
       

        public int SupplierID
        {
            get { return supplierID; }
            set { supplierID = value; }
        } 
        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        } 

        public string SupplierCode
        {
            get { return supplierCode; }
            set { supplierCode = value; }
        } 
        public string SupplierCompanyName
        {
            get { return supplierCompanyName; }
            set { supplierCompanyName = value; }
        } 

        public string SupplierAddr
        {
            get { return supplierAddr; }
            set { supplierAddr = value; }
        } 

        public string SupplierTaxID
        {
            get { return supplierTaxID; }
            set { supplierTaxID = value; }
        } 

        public string SupplierTelNo
        {
            get { return supplierTelNo; }
            set { supplierTelNo = value; }
        } 

        public string SupplierFaxNo
        {
            get { return supplierFaxNo; }
            set { supplierFaxNo = value; }
        } 

        public float SupplierPercentGP
        {
            get { return supplierPercentGP; }
            set { supplierPercentGP = value; }
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

  


       
    }
}
