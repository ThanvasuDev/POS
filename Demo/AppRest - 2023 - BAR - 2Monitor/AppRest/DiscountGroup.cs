using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class DiscountGroup
    {
        int discountGroupID;
        string discountGroupName;


        string discountGroupNameTH; 
        string discountGroupNameEN; 
        int discountSeqID; 
        string discountGroupFlaguse;

        public DiscountGroup(int discountGroupID, string discountGroupName, string discountGroupNameTH, string discountGroupNameEN, int discountSeqID, string discountGroupFlaguse)
        {
             DiscountGroupID = discountGroupID;
             DiscountGroupName = discountGroupName;
             DiscountGroupNameTH = discountGroupNameTH;
             DiscountGroupNameEN = discountGroupNameEN;
             DiscountSeqID = discountSeqID;
             DiscountGroupFlaguse = discountGroupFlaguse;
        }


        public int DiscountGroupID
        {
            get { return discountGroupID; }
            set { discountGroupID = value; }
        }

        public string DiscountGroupName
        {
            get { return discountGroupName; }
            set { discountGroupName = value; }
        } 

        public string DiscountGroupNameTH
        {
            get { return discountGroupNameTH; }
            set { discountGroupNameTH = value; }
        }


        public string DiscountGroupNameEN
        {
            get { return discountGroupNameEN; }
            set { discountGroupNameEN = value; }
        }

        public int DiscountSeqID
        {
            get { return discountSeqID; }
            set { discountSeqID = value; }
        }

        public string DiscountGroupFlaguse
        {
            get { return discountGroupFlaguse; }
            set { discountGroupFlaguse = value; }
        }


    }
}
