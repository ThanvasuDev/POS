using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Zone
    {
        int zoneID;
        string zoneName;
        string zoneDesc;
        string flagServiceCharge;
        int zoneSort;
        string zoneColour;
        int zonePriceID;
        string zoneVAT;
        string zoneType;
        int zonePrinterNo;
        string zonePrinterType;
        int zonePrinterCheckerNo;
        string zoneRemark;

        public Zone(int zoneID, string zoneName, string zoneDesc, string flagServiceCharge, int zoneSort, string zoneColour,int zonePriceID, string zoneVAT, string zoneType  , int zonePrinterNo , string zonePrinterType, int zonePrinterCheckerNo, string zoneRemark)
        {
            ZoneID = zoneID;
            ZoneName = zoneName;
            ZoneDesc = zoneDesc;
            FlagServiceCharge = flagServiceCharge;
            ZoneSort = zoneSort;
            ZoneColour = zoneColour;
            ZonePriceID = zonePriceID;
            ZoneVAT = zoneVAT;
            ZoneType = zoneType;
            ZonePrinterNo = zonePrinterNo;
            ZonePrinterType = zonePrinterType;
            ZonePrinterCheckerNo = zonePrinterCheckerNo;
            ZoneRemark = zoneRemark;
        }


        public int ZoneID
        {
            get { return zoneID; }
            set { zoneID = value; }
        }


        public string ZoneName
        {
            get { return zoneName; }
            set { zoneName = value; }
        }


        public string ZoneDesc
        {
            get { return zoneDesc; }
            set { zoneDesc = value; }
        }


        public string FlagServiceCharge
        {
            get { return flagServiceCharge; }
            set { flagServiceCharge = value; }
        }


        public int ZoneSort
        {
            get { return zoneSort; }
            set { zoneSort = value; }
        }


        public string ZoneColour
        {
            get { return zoneColour; }
            set { zoneColour = value; }
        }


        public int ZonePriceID
        {
            get { return zonePriceID; }
            set { zonePriceID = value; }
        }


        public string ZoneVAT
        {
            get { return zoneVAT; }
            set { zoneVAT = value; }
        }


        public string ZoneType
        {
            get { return zoneType; }
            set { zoneType = value; }
        }


        public int ZonePrinterNo
        {
            get { return zonePrinterNo; }
            set { zonePrinterNo = value; }
        }


        public string ZonePrinterType
        {
            get { return zonePrinterType; }
            set { zonePrinterType = value; }
        }

        public int ZonePrinterCheckerNo
        {
            get { return zonePrinterCheckerNo; }
            set { zonePrinterCheckerNo = value; }
        } 

        public string ZoneRemark
        {
            get { return zoneRemark; }
            set { zoneRemark = value; }
        }

    }
}
