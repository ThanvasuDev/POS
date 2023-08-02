using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Cat
    {
        int catID;

        public int CatID
        {
            get { return catID; }
            set { catID = value; }
        }
        int groupCatID;

        public int GroupCatID
        {
            get { return groupCatID; }
            set { groupCatID = value; }
        }
        string catName;

        public string CatName
        {
            get { return catName; }
            set { catName = value; }
        }
        string catNameTH;

        public string CatNameTH
        {
            get { return catNameTH; }
            set { catNameTH = value; }
        }
        string catNameEN;

        public string CatNameEN
        {
            get { return catNameEN; }
            set { catNameEN = value; }
        }
        string catDesc;

        public string CatDesc
        {
            get { return catDesc; }
            set { catDesc = value; }
        }
        int catPrinterNo;

        public int CatPrinterNo
        {
            get { return catPrinterNo; }
            set { catPrinterNo = value; }
        }
        string catPrinterType;

        public string CatPrinterType
        {
            get { return catPrinterType; }
            set { catPrinterType = value; }
        }
        string catColour;

        public string CatColour
        {
            get { return catColour; }
            set { catColour = value; }
        }
        int catSort;

        public int CatSort
        {
            get { return catSort; }
            set { catSort = value; }
        }
        float catConsignmentPercent;

        public float CatConsignmentPercent
        {
            get { return catConsignmentPercent; }
            set { catConsignmentPercent = value; }
        }
        string catFlagUse;

        public string CatFlagUse
        {
            get { return catFlagUse; }
            set { catFlagUse = value; }
        }

         

        public Cat(int catID, int groupCatID, string catName, string catNameTH, string catNameEN, string catDesc, int catPrinterNo, string catPrinterType, string catColour, int catSort, float catConsignmentPercent , string catFlagUse)
        {
            CatID = catID;
            GroupCatID = groupCatID;
            CatName = catName;
            CatNameTH = catNameTH;
            CatNameEN = catNameEN;
            CatDesc = catDesc;
            CatPrinterNo = catPrinterNo;
            CatPrinterType = catPrinterType;
            CatColour = catColour;
            CatSort = catSort;
            CatConsignmentPercent = catConsignmentPercent;
            CatFlagUse = catFlagUse;
        }

        public Cat(int catID, int groupCatID, string catName)
        {
            CatID = catID;
            GroupCatID = groupCatID;
            CatName = catName;
        }
 

    }
}
