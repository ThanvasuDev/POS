using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spire.Barcode;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Configuration;



namespace AppRest
{
    public static class QRCode
    {

        public static BarcodeSettings settings;
     //   public static BarcodeScanner Scan; 
        public static Image resultBarcode;

        public static void GenQRCode(string data)
        {
            //set the configuration of barcode

            string pathPrintBarcode = ConfigurationSettings.AppSettings["PathPrintBarcode"].ToString();

            // settings.Type = BarCodeType.QRCode;  
            BarcodeSettings settings = new BarcodeSettings();
            settings.Type = BarCodeType.QRCode;

            settings.Data = data;
            settings.Data2D = data;

            settings.QRCodeDataMode = QRCodeDataMode.AlphaNumber;

            settings.ShowText = false;

            settings.X = 1.0f;
            settings.QRCodeECL = QRCodeECL.H;
            BarCodeGenerator generator = new BarCodeGenerator(settings);

            Image image = generator.GenerateImage();

            resultBarcode = image;
             
        }






      

    }
}
