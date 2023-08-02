using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class POType
    {
        int poTypeID;
        string poType;
        string poSupplierType;


        public POType(int poTypeID, string poType, string poSupplierType)
        {
            PoTypeID = poTypeID;
            PoType = poType;
            PoSupplierType = poSupplierType;
        }


        public int PoTypeID
        {
            get { return poTypeID; }
            set { poTypeID = value; }
        }
        public string PoType
        {
            get { return poType; }
            set { poType = value; }
        }


        public string PoSupplierType
        {
            get { return poSupplierType; }
            set { poSupplierType = value; }
        }
        

    }
}
