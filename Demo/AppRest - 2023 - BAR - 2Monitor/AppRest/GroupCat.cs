using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class GroupCat
    {
        int groupCatID;
        string groupCatName;
        string groupCatNameTH; 
        string groupCatNameEN; 
        int disCountGroupID;
        string groupCatColour;

        public string GroupCatColour
        {
            get { return groupCatColour; }
            set { groupCatColour = value; }
        }
        int groupCatSort;

        public int GroupCatSort
        {
            get { return groupCatSort; }
            set { groupCatSort = value; }
        }
        string groupCatFlagUse;

        public string GroupCatFlagUse
        {
            get { return groupCatFlagUse; }
            set { groupCatFlagUse = value; }
        }

        public GroupCat(int groupCatID, string groupCatName, string groupCatNameTH, string groupCatNameEN, int disCountGroupID, string groupCatColour,int groupCatSort, string groupCatFlagUse)
        {
            GroupCatID = groupCatID;
            GroupCatName = groupCatName;
            GroupCatNameTH = groupCatNameTH;
            GroupCatNameEN = groupCatNameEN;
            DisCountGroupID = disCountGroupID;
            GroupCatColour = groupCatColour;
            GroupCatSort = groupCatSort;
            GroupCatFlagUse = groupCatFlagUse;

        }
 
        public int GroupCatID
        {
            get { return groupCatID; }
            set { groupCatID = value; }
        }

        public string GroupCatName
        {
            get { return groupCatName; }
            set { groupCatName = value; }
        }


        public string GroupCatNameTH
        {
            get { return groupCatNameTH; }
            set { groupCatNameTH = value; }
        }

        public string GroupCatNameEN
        {
            get { return groupCatNameEN; }
            set { groupCatNameEN = value; }
        }

        public int DisCountGroupID
        {
            get { return disCountGroupID; }
            set { disCountGroupID = value; }
        }

      
    }
}
