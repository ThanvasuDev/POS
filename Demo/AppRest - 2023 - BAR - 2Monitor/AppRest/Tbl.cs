using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Tbl
    {
        int tblID;
        int zoneID;
        string tblName;
        string tblDesc;
        string tblFlagUse;

        public Tbl()
        {

        }

        public Tbl(int tblID,int zoneID, string tblName, string tblDesc, string tblFlagUse)
        {
            TblID = tblID;
            ZoneID = zoneID;
            TblName = tblName;
            TblDesc = tblDesc;
            TblFlagUse = tblFlagUse;
        }


        public int TblID
        {
            get { return tblID; }
            set { tblID = value; }
        }



        public int ZoneID
        {
            get { return zoneID; }
            set { zoneID = value; }
        }

        public string TblName
        {
            get { return tblName; }
            set { tblName = value; }
        }
        

        public string TblDesc
        {
            get { return tblDesc; }
            set { tblDesc = value; }
        }
        

        public string TblFlagUse
        {
            get { return tblFlagUse; }
            set { tblFlagUse = value; }
        }




    }
}
