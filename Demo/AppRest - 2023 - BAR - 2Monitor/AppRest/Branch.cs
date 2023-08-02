using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Branch
    {
        int branchID;
        string branchNameTH;
        string branchNameEN;
        string restNameTH;
        string restAddr1TH;
        string restAddr2TH;
        string restNameEN;
        string restAddr1EN;
        string restAddr2EN;
        string restTel;
        string restTaxID;
        string restLine1;
        string restLine2;
        string restTaxRD;

        public string RestTaxRD
        {
            get { return restTaxRD; }
            set { restTaxRD = value; }
        }



        public string RestTaxID
        {
            get { return restTaxID; }
            set { restTaxID = value; }
        }
    

        public string RestLine1
        {
            get { return restLine1; }
            set { restLine1 = value; }
        }
        

        public string RestLine2
        {
            get { return restLine2; }
            set { restLine2 = value; }
        }



        public Branch()
        {

        }

        public Branch(int branchID, string branchNameTH, string branchNameEN, string restNameTH, string restAddr1TH, string restAddr2TH, string restNameEN, string restAddr1EN, string restAddr2EN, string restTel, string restTaxID, string restLine1, string restLine2 , string restTAXRD)
        {
            BranchID = branchID;
            BranchNameTH = branchNameTH;
            BranchNameEN = branchNameEN;
            RestNameTH = restNameTH;
            RestAddr1TH = restAddr1TH;
            RestAddr2TH = restAddr2TH;
            RestNameEN = restNameEN;
            RestAddr1EN = restAddr1EN;
            RestAddr2EN = restAddr2EN;
            RestTel = restTel;
            RestTaxID = restTaxID;
            RestLine1 = restLine1;
            RestLine2 = restLine2;
            RestTaxRD = restTAXRD;

        }


        public int BranchID
        {
            get { return branchID; }
            set { branchID = value; }
        }


        public string BranchNameTH
        {
            get { return branchNameTH; }
            set { branchNameTH = value; }
        }


        public string BranchNameEN
        {
            get { return branchNameEN; }
            set { branchNameEN = value; }
        }

        public string RestNameTH
        {
            get { return restNameTH; }
            set { restNameTH = value; }
        }

        public string RestAddr1TH
        {
            get { return restAddr1TH; }
            set { restAddr1TH = value; }
        }

        public string RestAddr2TH
        {
            get { return restAddr2TH; }
            set { restAddr2TH = value; }
        }


        public string RestNameEN
        {
            get { return restNameEN; }
            set { restNameEN = value; }
        }

        public string RestAddr1EN
        {
            get { return restAddr1EN; }
            set { restAddr1EN = value; }
        }


        public string RestAddr2EN
        {
            get { return restAddr2EN; }
            set { restAddr2EN = value; }
        }

        public string RestTel
        {
            get { return restTel; }
            set { restTel = value; }
        }

    }
}
