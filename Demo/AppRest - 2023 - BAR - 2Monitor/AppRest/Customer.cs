using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Customer
    {



        int custID;

        public int CustID
        {
            get { return custID; }
            set { custID = value; }
        }
        string custCode;

        public string CustCode
        {
            get { return custCode; }
            set { custCode = value; }
        }
        string taxID;

        public string TaxID
        {
            get { return taxID; }
            set { taxID = value; }
        }
        string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        string tel;

        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        string flagUse;

        public string FlagUse
        {
            get { return flagUse; }
            set { flagUse = value; }
        }

 

        public Customer(int custID,string custCode, string taxID, string title, string name,string tel,string address,string status,string flagUse)
        {
            CustID = custID;
            CustCode = custCode;
            TaxID = taxID;
            Title = title;
            Name = name;
            Tel = tel;
            Address = address;
            Status = status;
            FlagUse = flagUse;
        }

     
    }
}
