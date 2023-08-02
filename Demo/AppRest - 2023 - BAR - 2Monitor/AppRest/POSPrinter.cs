using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class POSPrinter
    {

           int printerNo;
           string printerStrName;
           string printerName;
           string flagPrint;

           POSPrinter()
           {

           }

           public POSPrinter( int printerNo, string printerStrName,string printerName,string flagPrint)
           {
               PrinterNo = printerNo;
               PrinterStrName = printerStrName;
               PrinterName = printerName;
               FlagPrint = flagPrint;
           }


           public int PrinterNo
           {
               get { return printerNo; }
               set { printerNo = value; }
           }

           public string PrinterStrName
           {
               get { return printerStrName; }
               set { printerStrName = value; }
           }

           public string PrinterName
           {
               get { return printerName; }
               set { printerName = value; }
           }


           public string FlagPrint
           {
               get { return flagPrint; }
               set { flagPrint = value; }
           }

    }
}
