using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class ProductGroup
    {
        int proGroupID;
        string productGroupCode;
        string progroupName;
        string progroupGroupCatID;
        string progroupCatID;
        string progroupProductID;
        string progroupFlaguse;


        public ProductGroup( int proGroupID,string productGroupCode, string progroupName,string progroupGroupCatID,string progroupCatID, string progroupProductID,string progroupFlaguse)
        {
            ProGroupID = proGroupID;
            ProductGroupCode = productGroupCode;
            ProgroupName = progroupName;
            ProgroupGroupCatID = progroupGroupCatID;
            ProgroupCatID = progroupCatID;
            ProgroupProductID = progroupProductID;
            ProgroupFlaguse = progroupFlaguse;
        }


        public int ProGroupID
        {
            get { return proGroupID; }
            set { proGroupID = value; }
        } 

        public string ProductGroupCode
        {
            get { return productGroupCode; }
            set { productGroupCode = value; }
        } 

        public string ProgroupName
        {
            get { return progroupName; }
            set { progroupName = value; }
        } 

        public string ProgroupGroupCatID
        {
            get { return progroupGroupCatID; }
            set { progroupGroupCatID = value; }
        } 

        public string ProgroupCatID
        {
            get { return progroupCatID; }
            set { progroupCatID = value; }
        } 

        public string ProgroupProductID
        {
            get { return progroupProductID; }
            set { progroupProductID = value; }
        } 

        public string ProgroupFlaguse
        {
            get { return progroupFlaguse; }
            set { progroupFlaguse = value; }
        }  



    }
}
