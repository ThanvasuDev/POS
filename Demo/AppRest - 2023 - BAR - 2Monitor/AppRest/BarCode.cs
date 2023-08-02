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
    public static class BarCode
    {

        public static BarcodeSettings settings;
        public static BarcodeScanner Scan;

        public static Image resultBarcode;


        public static void GenBarCode(string data)
        {
            //set the configuration of barcode

            string pathPrintBarcode = ConfigurationSettings.AppSettings["PathPrintBarcode"].ToString();

            settings = new BarcodeSettings();

            string type = "Code128";
            string border = "Solid";

            settings.Data2D = data;
            settings.Data = data;

            settings.Type = (BarCodeType)Enum.Parse(typeof(BarCodeType), type);

            settings.HasBorder = false;
            settings.BorderDashStyle = (DashStyle)Enum.Parse(typeof(DashStyle), border);

            short fontSize = 15;
            string font = "SimSun";

            settings.TextFont = new System.Drawing.Font(font, fontSize, FontStyle.Bold);

            short barHeight = 20;
            settings.BarHeight = barHeight;

            //  settings.WideNarrowRatio = (float)0.5 ; 

            settings.ShowText = true;

            settings.ShowCheckSumChar = false;


            string foreColor = "Black";
            settings.ForeColor = Color.FromName(foreColor);

            //generate the barcode use the settings
            BarCodeGenerator generator = new BarCodeGenerator(settings);
            Image barcode = generator.GenerateImage();

            ImageHandler im = new ImageHandler();

            // im.Save((Bitmap) barcode, 260, 140, 100000, pathPrintBarcode);

            resultBarcode = barcode;



            // Size size = new Size(13, 5);

            // Image bar = (Image)new Bitmap(barcode, size); 

            //save the barcode as an image
            // bar.Save(pathPrintBarcode);
            //  barcode.Save(pathPrintBarcode);


            //launch the generated barcode image
            //  System.Diagnostics.Process.Start(pathPrintBarcode);
        }

        //private Image resizeImage(Image imgToResize, Size size)
        //{
        //    return (Image)(new Bitmap(imgToResize, size));
        //}

    }
}
