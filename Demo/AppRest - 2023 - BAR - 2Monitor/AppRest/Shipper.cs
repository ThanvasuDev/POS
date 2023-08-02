using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Shipper
    {

        int shipperID;
        string shipperName;


        public Shipper(int shipperID, string shipperName)
        {
            ShipperID = shipperID;
            ShipperName = shipperName;
        }
         
        public int ShipperID
        {
            get { return shipperID; }
            set { shipperID = value; }
        }


        public string ShipperName
        {
            get { return shipperName; }
            set { shipperName = value; }
        }

    }
}
