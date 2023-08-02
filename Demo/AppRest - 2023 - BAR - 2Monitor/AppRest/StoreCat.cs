using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class StoreCat
    {
        int storeCatID;
        string storeCatName;
        string storeCatDesc;
         
        public StoreCat()
        {
        }

        public StoreCat(int storeCatID, string storeCatName, string storeCatDesc)
        {
            StoreCatID = storeCatID;
            StoreCatName = storeCatName;
            StoreCatDesc = storeCatDesc;
        }

         public int StoreCatID
        {
          get { return storeCatID; }
          set { storeCatID = value; }
        } 

        public string StoreCatName
        {
          get { return storeCatName; }
          set { storeCatName = value; }
        }  

        public string StoreCatDesc
        {
          get { return storeCatDesc; }
          set { storeCatDesc = value; }
        }


    }
}
