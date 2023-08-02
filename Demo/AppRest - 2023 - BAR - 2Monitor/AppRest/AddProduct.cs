using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Configuration;
using System.IO;

namespace AppRest
{
    public partial class AddProduct : AddDataTemplate
    {

        GetDataRest gd;
        MainManage formMainManage;


        List<Cat> allCats;
        List<Product> allProductsByCats;

        List<Prom> allProm;

        DataTable dataAllProduct;

        int selectedCat;

        string printerBarcodeName;
        string flagPrintBarcode;
        string pathPrintBarcode;

        int modSticker = 0;

        Image barcodeImg;

        List<POSPrinter> posPrinters;
        List<Supplier> allSupplier;

        //
        DataTable allProm_DATA;

        List<DiscountGroup> dg;
        string syntaxDesc = "";


        public AddProduct(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();
            gd = new GetDataRest(); 

            comboBoxAllProduct.Visible = false;

            selectedCat = 0;

            allCats = gd.getOrderCat(2); 
            allProductsByCats = gd.getProductByCat(selectedCat,0,0);

            dataAllProduct = gd.getAllProduct(0);
            dataGridViewAllData.DataSource = dataAllProduct;


            allProm = gd.getListProm(0);
             
            getComboAllCat();
            getComboAllProm();

            allSupplier = gd.getPC_Supplier(0, "0", "0");
            getComboAllInvent();


            buttonPrintBarCode.Enabled = false;

            printerBarcodeName = ConfigurationSettings.AppSettings["PrinterNameBarCode"].ToString();
            flagPrintBarcode = ConfigurationSettings.AppSettings["FlagPrintBarcode"].ToString();
            pathPrintBarcode = ConfigurationSettings.AppSettings["pathPrintBarcode"].ToString();

            printBarCode.PrinterSettings.PrinterName = printerBarcodeName;

            if (flagPrintBarcode == "Y")
                buttonPrintBarCode.Enabled = true;

           // printBarCode.PrinterSettings.PaperSizes  // = new  PaperSize("PaperA4", 840, 1180);

            posPrinters = new List<POSPrinter>();
            posLoadPrinter();

            getComboAllPrinterF1();
            getComboAllPrinterF2();

            this.Width = 1024;
            this.Height = 764;

            ///

            allProm_DATA = gd.getListProm_DATA(0);
            dataGridViewAllProm.DataSource = allProm_DATA;


            /*
              * 
              FOR:Disc=:10:8:2:8:0:0
                 
              [1] > D1 = Discount Live Product
              [2] > DR = Discount Food Drink
              [3] > DA = Discount Alcohol
              [4] > DS = Discount Super
              [5] > DB = Discount Baht
              [6] > เลือกสินค้าอื่นด้วย = ProductID
           
            */

            dg = gd.getAllDiscountGroup();

            syntaxDesc = "FOR:Disc=:10:8:2:8:0:0" + "\n\r";


            foreach (DiscountGroup d in dg)
            {
                syntaxDesc += "[" + d.DiscountGroupID.ToString() + "] > D" + d.DiscountGroupID.ToString() + " = " + d.DiscountGroupNameEN + "\n";
            }

            syntaxDesc += @"[5] > DB = Discount Baht
[6] > เลือกสินค้าอื่นด้วย = ProductID";

            syntaxDesc += "\n\r";

            syntaxDesc += @"Coupon Package >> CPUS:EX=30:PN=10:CL=2
  Expired Date = 30
  Package Num = 10
  Coupon Type = 2 (Future)";

            richTextBoxSynTaxCode.Text = syntaxDesc;

        }

        private void getComboAllInvent()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "= เลือก Product Suplier ="));

            foreach (Supplier c in allSupplier)
            {
                data.Add(new KeyValuePair<int, string>(c.SupplierID, c.SupplierName));
            }


            // Clear the combobox
            comboBoxSuplier.DataSource = null;
            comboBoxSuplier.Items.Clear();

            // Bind the combobox
            comboBoxSuplier.DataSource = new BindingSource(data, null);
            comboBoxSuplier.DisplayMember = "Value";
            comboBoxSuplier.ValueMember = "Key";

            comboBoxSuplier.SelectedIndex = 0;
        }



        private void posLoadPrinter()
        {
            string flagPrint = "";
            string printerName = "";
            string strPrinter = "";

            flagPrint = ConfigurationSettings.AppSettings["FlagPrintCash"].ToString();
            printerName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();
            strPrinter = "Cashier";

            posPrinters.Add(new POSPrinter(0, strPrinter, printerName, flagPrint));

            for (int i = 1; i <= 20; i++)
            {

                flagPrint = ConfigurationSettings.AppSettings["FlagPrintOrder" + i.ToString()].ToString();
                printerName = ConfigurationSettings.AppSettings["PrinterOrder" + i.ToString()].ToString();
                strPrinter = ConfigurationSettings.AppSettings["StrPrintOrder" + i.ToString()].ToString();

                posPrinters.Add(new POSPrinter(i, strPrinter, printerName, flagPrint));

            }

            flagPrint = ConfigurationSettings.AppSettings["flagPrintOrderCheckerOther"].ToString();
            printerName = ConfigurationSettings.AppSettings["PrinterNameCheckOrder"].ToString();
            strPrinter = "Cashier";

            posPrinters.Add(new POSPrinter(21, strPrinter, printerName, flagPrint));
             
        }

        private void getComboAllPrinterF1()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "= Select Printer F1 ="));

            foreach (POSPrinter ps in posPrinters)
            {
                if (ps.FlagPrint.ToLower() == "y" && ps.PrinterNo > 0 && ps.PrinterNo < 21)
                {
                    data.Add(new KeyValuePair<int, string>(ps.PrinterNo, ps.PrinterStrName + " > " + ps.PrinterName));
                }
            }
              

            // Clear the combobox
            comboBoxPrinterNameF1.DataSource = null;
            comboBoxPrinterNameF1.Items.Clear();

            // Bind the combobox
            comboBoxPrinterNameF1.DataSource = new BindingSource(data, null);
            comboBoxPrinterNameF1.DisplayMember = "Value";
            comboBoxPrinterNameF1.ValueMember = "Key"; 
        }

        private void getComboAllPrinterF2()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "= Select Printer F2 ="));

            foreach (POSPrinter ps in posPrinters)
            {
                if (ps.FlagPrint.ToLower() == "y" && ps.PrinterNo > 0 && ps.PrinterNo < 21)
                {
                    data.Add(new KeyValuePair<int, string>(ps.PrinterNo, ps.PrinterStrName + " > " + ps.PrinterName));
                }
            }


            // Clear the combobox
            comboBoxPrinterNameF2.DataSource = null;
            comboBoxPrinterNameF2.Items.Clear();

            // Bind the combobox
            comboBoxPrinterNameF2.DataSource = new BindingSource(data, null);
            comboBoxPrinterNameF2.DisplayMember = "Value";
            comboBoxPrinterNameF2.ValueMember = "Key";
        }


        private void getComboAllCat()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "===== เลือกทั้งหมด ====="));

            foreach (Cat c in allCats)
            {
                data.Add(new KeyValuePair<int, string>(c.CatID, c.CatName));
            }


            // Clear the combobox
            comboBoxAllCat.DataSource = null;
            comboBoxAllCat.Items.Clear();

            // Bind the combobox
            comboBoxAllCat.DataSource = new BindingSource(data, null);
            comboBoxAllCat.DisplayMember = "Value";
            comboBoxAllCat.ValueMember = "Key";


            // Bind the combobox
            comboBoxNewCat.DataSource = new BindingSource(data, null);
            comboBoxNewCat.DisplayMember = "Value";
            comboBoxNewCat.ValueMember = "Key";


            comboBoxAllCat.SelectedValue = selectedCat;
            getComboAllProduct();

        }



        private void getComboAllProm()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "=== เลือกกลุ่ม Promotion ===="));

            foreach (Prom c in allProm)
            {
                data.Add(new KeyValuePair<int, string>(c.PromID, c.PromCode + "-" + c.PromName));
            }


            // Clear the combobox
            comboBoxListPromotion.DataSource = null;
            comboBoxListPromotion.Items.Clear();

            // Bind the combobox
            comboBoxListPromotion.DataSource = new BindingSource(data, null);
            comboBoxListPromotion.DisplayMember = "Value";
            comboBoxListPromotion.ValueMember = "Key";


            // Bind the combobox
            comboBoxListPromotion.DataSource = new BindingSource(data, null);
            comboBoxListPromotion.DisplayMember = "Value";
            comboBoxListPromotion.ValueMember = "Key";

             

        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {

            try
            {
                string productNameTH = txtBoxNameTH.Text;
                string productNameEN = txtBoxNameEN.Text;
                string productUnit = txtBoxUnit.Text;
                string productDesc = txtBoxDesc.Text;
                string productColour = txtBoxColour.Text;
                float productPrice = float.Parse(txtBoxPrice.Text);
                float productPrice2 = float.Parse(txtBoxPrice2.Text);
                float productPrice3 = float.Parse(txtBoxPrice3.Text);
                float productPrice4 = float.Parse(txtBoxPrice4.Text);
                float productPrice5 = float.Parse(txtBoxPrice5.Text);
                float productCost = float.Parse(txtBoxPriceCost.Text);
                string productflagUse = comboBoxFlagUse.Text;
                string productflagStock = comboBoxFlagStock.Text;
                string productStockType = textBoxCRMLevelTag.Text;
                 
                string productBarcode = txtBoxBarCode.Text;

                int productID = Int32.Parse(comboBoxAllProduct.SelectedValue.ToString());
                int catID = Int32.Parse(comboBoxAllCat.SelectedValue.ToString());
                int newCatID = Int32.Parse(comboBoxNewCat.SelectedValue.ToString());


                int prodoctPromID = Int32.Parse(comboBoxListPromotion.SelectedValue.ToString());
                int productGetPoint = Int32.Parse(txtBoxGetPoint.Text);

                int productPrinterNo = Int32.Parse(comboBoxPrinterNameF1.SelectedValue.ToString());
                int productPrinterNo2 = Int32.Parse(comboBoxPrinterNameF2.SelectedValue.ToString());

                int productSuplierID = Int32.Parse(comboBoxSuplier.SelectedValue.ToString());
                float productPercentCont = float.Parse(txtBoxPercentGP.Text);
                
                int productFlagNonServiceCharge = 0;

                if (checkBoxNSC.Checked)
                    productFlagNonServiceCharge = 1;

                int productFlagNonVAT = 0;

                if (checkBoxNVAT.Checked)
                    productFlagNonVAT = 1;


                int productFlagNonDiscount = 0;

                if (checkBoxNDisc.Checked)
                    productFlagNonDiscount = 1;

                int productSTDTime = Int32.Parse(txtBoxSTDTime.Text.ToString());



                int flagDelivery = 0;
                if (checkBoxFlag_Delivery.Checked)
                    flagDelivery = 1;


                int flagCRM = 0;
                if (checkBoxFlag_CRM.Checked)
                    flagCRM = 1;

                string crmCPType = comboBoxCouponType.Text;
                string crmCouponSynTax = textBoxCRMSynTax.Text;
                string crmImgUrl1 = textBoxImgUrl1.Text;
                string crmImgUrl2 = textBoxImgUrl2.Text;
                string crmPeriodTime = textBoxPeriodTime.Text;
                string crmStore = textBoxStoreUse.Text;
                string crmTC = richTextBoxTC.Text;

                int flagQROrder = 0;
                if (checkBoxFlag_OROrder.Checked)
                    flagQROrder = 1;


                Button but = (Button)sender;
                string butName = but.Name;

                if (butName == "buttonDel")
                {
                    if (MessageBox.Show("คุณต้องการจะลบ " + productNameTH + " หรือไม่ ?", "เพิ่ม " + productNameTH, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updsProduct(catID, productID, productNameTH, productNameEN, productUnit, productDesc, productColour, productPrice, productPrice2, productPrice3, productPrice4, productPrice5, productCost, "D", productBarcode, productflagStock, productStockType, prodoctPromID, productGetPoint, 0, 0, 0, 0, 0, 0, 0, 0, flagDelivery, flagCRM, crmCPType, crmImgUrl1, crmImgUrl2, crmCouponSynTax, crmPeriodTime, crmStore, crmTC , flagQROrder);

                        if (result <= 0)
                        {
                            throw new Exception("ไม่สามารถลบรายการได้ เนื่องจากมีในรายการขายแล้ว");
                        }
                        else
                        {

                            allProductsByCats = gd.getProductByCat(selectedCat, 0,0);


                            dataAllProduct = gd.getAllProduct(0);
                            dataGridViewAllData.DataSource = dataAllProduct;

                            getComboAllProduct();
                            clearForm();

                            if (selectedCat > 0)
                            {
                                this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductCatID = '{0}' ", selectedCat);
                            }
                            else
                            { 
                                textSearch_TextChanged(this, e);
                            }
                                

                            throw new Exception("ลบ : " + productNameTH + ">> (Success)");
                        }
                    }
                     
                }

                  

                if (radioButtonAddData.Checked == true)
                {

                    if (MessageBox.Show("คุณต้องการเพิ่มเมนูชื่อ " + productNameTH + " หรือไม่ ?", "เพิ่ม " + productNameTH, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.instNewProduct(catID, productNameTH, productNameEN, productUnit, productDesc, productColour, productPrice, productPrice2, productPrice3, productPrice4, productPrice5,  productCost, productflagUse ,productBarcode, productflagStock, productStockType, prodoctPromID, productGetPoint, productPrinterNo, productPrinterNo2,productSuplierID,productPercentCont,productFlagNonServiceCharge,productFlagNonVAT,productFlagNonDiscount,productSTDTime, flagDelivery, flagCRM, crmCPType, crmImgUrl1, crmImgUrl2, crmCouponSynTax, crmPeriodTime, crmStore, crmTC , flagQROrder);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Product : Please Try Again");



                        } 
                        if (result == 99)
                        {
                            MessageBox.Show("Error Insert New Product : BarCode Exist");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่มเมนูชื่อ " + productNameTH + ">> (Success)");
                            allProductsByCats = gd.getProductByCat(selectedCat, 0,0);
                             
                            getComboAllProduct();
                            clearForm();

                            dataAllProduct = gd.getAllProduct(0);
                            dataGridViewAllData.DataSource = dataAllProduct;

                            if (selectedCat > 0)
                            {
                                this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductCatID = '{0}' ", selectedCat);
                            }
                            else
                            { 
                                textSearch_TextChanged(this, e);
                            }


                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("คุณต้องการแก้ไขเมนูชื่อ " + productNameTH + " หรือไม่ ?", "เพิ่ม " + productNameTH, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updsProduct(newCatID, productID, productNameTH, productNameEN, productUnit, productDesc, productColour, productPrice, productPrice2, productPrice3, productPrice4, productPrice5, productCost, productflagUse, productBarcode, productflagStock, productStockType, prodoctPromID, productGetPoint, productPrinterNo, productPrinterNo2, productSuplierID, productPercentCont, productFlagNonServiceCharge, productFlagNonVAT, productFlagNonDiscount, productSTDTime , flagDelivery, flagCRM, crmCPType, crmImgUrl1, crmImgUrl2, crmCouponSynTax, crmPeriodTime, crmStore, crmTC , flagQROrder);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Product : Please Try Again");
 
                        }
                        if (result == 99)
                        {
                            MessageBox.Show("Error Insert New Product : Barcode Exist");
                        }
                        else
                        {
                            MessageBox.Show("แก้ไข : " + productNameTH + ">> (Success)");

                            allProductsByCats = gd.getProductByCat(selectedCat, 0, 0);

                            getComboAllProduct();
                            clearForm();

                            dataAllProduct = gd.getAllProduct(0);
                            dataGridViewAllData.DataSource = dataAllProduct;

                            if (selectedCat > 0)
                            {
                                this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductCatID = '{0}' ", selectedCat);
                            }
                            else
                            {

                                textSearch_TextChanged(this, e);
                            }



                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void getComboAllProduct()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(-1, "= เลือกแก้ไขเมนู ="));

            foreach (Product c in allProductsByCats)
            {
                data.Add(new KeyValuePair<int, string>(c.ProductID, c.ProductName));
            }


            // Clear the combobox
            comboBoxAllProduct.DataSource = null;
            comboBoxAllProduct.Items.Clear();

            // Bind the combobox
            comboBoxAllProduct.DataSource = new BindingSource(data, null);
            comboBoxAllProduct.DisplayMember = "Value";
            comboBoxAllProduct.ValueMember = "Key";

        }


        private void button1_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void clearForm()
        {
            txtBoxNameTH.Text = "";
            txtBoxNameEN.Text = "";
            txtBoxUnit.Text = "Unit";
            txtBoxColour.Text = "Unit";
            txtBoxPrice.Text = "0";
            txtBoxPrice2.Text = "0";
            txtBoxPrice3.Text = "0";
            txtBoxPrice4.Text = "0";
            txtBoxPrice5.Text = "0";
            txtBoxPriceCost.Text = "0";
            txtBoxDesc.Text = "";
            txtBoxBarCode.Text = "";
            comboBoxFlagUse.Text = "Y";
            comboBoxFlagStock.Text = "Y";
            comboBoxStockType.Text = "Item"; 
            radioButtonAddData.Checked = true;
            radioButtonAuto.Checked = true;

            txtBoxGetPoint.Text = "0";
            comboBoxListPromotion.SelectedIndex = 0;
            comboBoxPrinterNameF1.SelectedIndex = 0;
            comboBoxPrinterNameF2.SelectedIndex = 0;

            checkBoxNSC.Checked = false;
            checkBoxNVAT.Checked = false;
            checkBoxNDisc.Checked = false;
            checkBoxFlag_Delivery.Checked = false;
            checkBoxFlag_CRM.Checked = false;
            checkBoxFlag_OROrder.Checked = false;


            txtBoxSTDTime.Text = "0";


            textBoxCRMLevelTag.Text = "";
            textBoxCRMSynTax.Text = "";
            textBoxImgUrl1.Text = "";
            textBoxImgUrl2.Text = "";
            textBoxPeriodTime.Text = "";
            textBoxStoreUse.Text = "";
            richTextBoxTC.Text = "";


        }

        private void ChangeUpdateFlag(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked == true)
            {
                comboBoxAllProduct.Visible = false;
                labelHeader.Text = "เพิ่มข้อมูล";
                buttonAddTable.Text = "เพิ่มข้อมูล";
                comboBoxNewCat.Visible = false;
            }
            else
            {
                comboBoxAllProduct.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูล";
                buttonAddTable.Text = "แก้ไขข้อมูล";
                comboBoxNewCat.Visible = true;
            }
             
        }
         


        private void buttonBackToManage_Click(object sender, EventArgs e)
        {
            LinkFormMainManage();
        }

        private void LinkFormMainManage()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formMainManage == null)
            {
                formMainManage = new MainManage(this, 1);
            }
            Cursor.Current = Cursors.Default;
            if (formMainManage.ShowDialog() == DialogResult.OK)
            {
                formMainManage.Dispose();
                formMainManage = null;
            }
        }

        private void ComboBoxAllCat_Change(object sender, EventArgs e)
        {
            try
            {
                selectedCat = Int32.Parse(comboBoxAllCat.SelectedValue.ToString());
                allProductsByCats = gd.getProductByCat(selectedCat,0,0);
                dataAllProduct = gd.getAllProduct(selectedCat);
                dataGridViewAllData.DataSource = allProductsByCats;

                getComboAllProduct();
            }
            catch (Exception ex)
            {

            }
        }

        private void CommboxAllProduct_Change(object sender, EventArgs e)
        {
            try
            {

                int productID = Int32.Parse(comboBoxAllProduct.SelectedValue.ToString());

                foreach (Product c in allProductsByCats)
                {
                    if (c.ProductID == productID)
                    {
                        txtBoxNameTH.Text = c.ProductName;
                        txtBoxNameEN.Text = c.ProductNameEN;
                        txtBoxUnit.Text = c.ProductUnit;
                        txtBoxDesc.Text = c.ProductDesc;
                        txtBoxColour.Text = c.ProductColour;
                        txtBoxPrice.Text = c.ProductPrice.ToString();
                        txtBoxPrice2.Text = c.ProductPrice2.ToString();
                        txtBoxPrice3.Text = c.ProductPrice3.ToString();
                        txtBoxPrice4.Text = c.ProductPrice4.ToString();
                        txtBoxPrice5.Text = c.ProductPrice5.ToString();
                        txtBoxPriceCost.Text = c.ProductCost.ToString();
                        txtBoxBarCode.Text = c.ProductBarcode;
                        comboBoxFlagUse.Text = c.ProductFlagUse;
                        comboBoxFlagStock.Text = c.ProductFlagStock;
                        textBoxCRMLevelTag.Text = c.ProductStockType;
                        comboBoxNewCat.SelectedValue = c.ProductCatID;
                        txtBoxGetPoint.Text = c.ProductGetPoint.ToString();
                        comboBoxListPromotion.SelectedValue = c.ProductPromID; 
                        comboBoxPrinterNameF1.SelectedValue = c.ProductPrinterNo;
                        comboBoxPrinterNameF2.SelectedValue = c.ProductPrinterNo2;

                        comboBoxSuplier.SelectedValue = c.ProductSuplierID;
                        txtBoxPercentGP.Text = c.ProductConPercent.ToString();


                        if(c.FlagNonServiceCharge == 1)
                            checkBoxNSC.Checked = true;
                        else
                            checkBoxNSC.Checked = false;

                        if (c.FlagNonVAT == 1)
                            checkBoxNVAT.Checked = true;
                        else
                            checkBoxNVAT.Checked = false;

                        if (c.FlagNonDiscount == 1)
                            checkBoxNDisc.Checked = true;
                        else
                            checkBoxNDisc.Checked = false;

                        if (c.FlagDelivery == 1)
                            checkBoxFlag_Delivery.Checked = true;
                        else
                            checkBoxFlag_Delivery.Checked = false;

                        if (c.FlagCRM == 1)
                            checkBoxFlag_CRM.Checked = true;
                        else
                            checkBoxFlag_CRM.Checked = false;

                        if (c.FlagQROrder == 1)
                            checkBoxFlag_OROrder.Checked = true;
                        else
                            checkBoxFlag_OROrder.Checked = false;

                       

                        txtBoxSTDTime.Text = c.StdTime.ToString(); 
                          

                        textBoxCRMSynTax.Text = c.CrmCouponSynTax;
                        textBoxImgUrl1.Text = c.CrmImgUrl1;
                        textBoxImgUrl2.Text = c.CrmImgUrl2;
                        textBoxPeriodTime.Text = c.CrmPeriodTime;
                        textBoxStoreUse.Text = c.CrmStore;
                        richTextBoxTC.Text = c.CrmTC; 

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonScan_Click(object sender, EventArgs e)
        {
            txtBoxBarCode.Text = "";
            txtBoxBarCode.Focus();
        }

        private void radioButtonAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAuto.Checked == true)
            {
                txtBoxBarCode.Enabled = false;
                txtBoxBarCode.Text = "";
            }
            else
            {
                txtBoxBarCode.Enabled = true;
            }
        }

        private void buttonPrintBarCode_Click(object sender, EventArgs e)
        {
            try
            {

                string txtbarCode = txtBoxBarCode.Text;

                BarCode.GenBarCode(txtbarCode);
                barcodeImg = BarCode.resultBarcode;


                int loop = 0;
                int loops = 0;

                loop = Int32.Parse(comboBoxLoopPrint.Text);

                if (loop % 3 == 0)
                    loops = loop / 3;
                else
                    loops = (loop / 3) + 1;




                for (int ii = 1; ii <= loops; ii++)
                {
                    modSticker = 3;

                    if (ii == loops)
                    {
                        modSticker = loop % 3;
                        if (modSticker == 0)
                            modSticker = 3;
                    }


                    printBarCode.Print();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {


                File.Delete(pathPrintBarcode);
            }


        }

        // OnPrintPage
        private void OnPrintBarCode(object sender, PrintPageEventArgs e)
        {



            try
            {

                int x = 5;
                int y = 8;
                string proName = txtBoxNameTH.Text;
                string proPrice = " ราคา : " + float.Parse(txtBoxPrice.Text).ToString("###,###.##") + " บาท";

                Brush brush = new SolidBrush(Color.Black);
                Font fontBody = new Font("Tahoma", 6);
                Font fontBodyShop = new Font("Tahoma", 5);
                Font fontBodyP = new Font("Tahoma", 8);


                string shopName = ConfigurationSettings.AppSettings["RestName"];

                e.Graphics.DrawString(shopName, fontBodyShop, brush, x + 10, y);

                if (this.modSticker > 1)
                    e.Graphics.DrawString(shopName, fontBodyShop, brush, x + 137 + 10, y);

                if (this.modSticker > 2)
                    e.Graphics.DrawString(shopName, fontBodyShop, brush, x + 270 + 10, y);


                y += 8;

                e.Graphics.DrawImage(barcodeImg, x, y, 110, 50);

                if (this.modSticker > 1)
                    e.Graphics.DrawImage(barcodeImg, x + 137, y, 110, 50);

                if (this.modSticker > 2)
                    e.Graphics.DrawImage(barcodeImg, x + 270, y, 110, 50);



                // ชื่อสินค้า 

                y += 50;

                if (proName.Length <= 35)
                {
                    e.Graphics.DrawString(proName, fontBodyShop, brush, x, y);

                    if (this.modSticker > 1)
                        e.Graphics.DrawString(proName, fontBodyShop, brush, x + 137, y);

                    if (this.modSticker > 2)
                        e.Graphics.DrawString(proName, fontBodyShop, brush, x + 270, y);

                }
                else if (proName.Length <= 70)
                {
                    string proName1 = proName.Substring(0, 35);
                    string proName2 = proName.Substring(35, proName.Length - 35);

                    e.Graphics.DrawString(proName1, fontBodyShop, brush, x, y);

                    if (this.modSticker > 1)
                        e.Graphics.DrawString(proName1, fontBodyShop, brush, x + 137, y);

                    if (this.modSticker > 2)
                        e.Graphics.DrawString(proName1, fontBodyShop, brush, x + 270, y);

                    y += 10;

                    e.Graphics.DrawString(proName2, fontBodyShop, brush, x, y);

                    if (this.modSticker > 1)
                        e.Graphics.DrawString(proName2, fontBodyShop, brush, x + 137, y);

                    if (this.modSticker > 2)
                        e.Graphics.DrawString(proName2, fontBodyShop, brush, x + 270, y);

                } 

                // ชื่อราคา 

                y += 10;

                e.Graphics.DrawString(proPrice, fontBodyP, brush, x, y);

                if (this.modSticker > 1)
                    e.Graphics.DrawString(proPrice, fontBodyP, brush, x + 137, y);

                if (this.modSticker > 2)
                    e.Graphics.DrawString(proPrice, fontBodyP, brush, x + 270, y);




                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        private Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }


        private void dataGridViewAllMember_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridproductID = Int32.Parse(dataGridViewAllData.Rows[e.RowIndex].Cells["ProductID"].Value.ToString());

                comboBoxAllProduct.SelectedValue = dataGridproductID;

                comboBoxAllProduct.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูล";
                buttonAddTable.Text = "แก้ไขข้อมูล";
                radioButtonUpdateData.Checked = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridViewAllMember_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridproductID = Int32.Parse(dataGridViewAllData.Rows[e.RowIndex].Cells["ProductID"].Value.ToString());

                comboBoxAllProduct.SelectedValue = dataGridproductID;
 
                comboBoxAllProduct.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูล";
                buttonAddTable.Text = "แก้ไขข้อมูล";
                radioButtonUpdateData.Checked = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void txtBoxUnit_MouseClick(object sender, MouseEventArgs e)
        {

             

         }

        private void buttonHelperColour_Click(object sender, EventArgs e)
        {
            string rgb = "";

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                rgb = colorDialog1.Color.R.ToString() + "," + colorDialog1.Color.G.ToString() + "," + colorDialog1.Color.B.ToString();

                txtBoxColour.Text = rgb + "|" + "B";
            }
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string srPID = textBoxSRProductID.Text;
                string srPBC = textBoxSRBarcode.Text;
                string srPName = textBoxSRProductName.Text;

                dataGridViewAllData.DataSource = dataAllProduct;

                //   this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductID like '*{0}*' and ProductImage like '*{1}*' and ProductName like '*{2}*' ", srPID, srPBC, srPName);

                if (srPName.Length > 0)
                {
                    this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductName like '*{0}*' ", srPName);

                }
                else
                {
                    if (srPID.Length > 0)
                        this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductID = '{0}' ", srPID);
                    else if (srPBC.Length > 0)
                        this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductImage = '{0}' ", srPBC);
                    else
                        this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductID > 4 ");

                }

                //  this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductID = '{0}' and ProductImage = '{0}'  ", srPID, srPBC);


                //private void colorDialog1_HelpRequest(object sender, EventArgs e)
                //{
                //    string rbg = colorDialog1.Color.R.ToString() + "," + colorDialog1.Color.G.ToString() + "," + colorDialog1.Color.B.ToString();

                //    txtBoxUnit.Text = rbg + "|B";
                //}


            }
            catch (Exception ex)
            {

            }

        }

        private void txtBoxPrice_TextChanged(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked)
            {
                txtBoxPrice2.Text = txtBoxPrice.Text;
                txtBoxPrice3.Text = txtBoxPrice.Text;
                txtBoxPrice4.Text = txtBoxPrice.Text;
                txtBoxPrice5.Text = txtBoxPrice.Text;
                txtBoxPriceCost.Text = txtBoxPrice.Text;
            }
        }

        private void buttonExportData_Click(object sender, EventArgs e)
        {
            try
            {
                ExportData.Excel_FromDataTable_DT(dataGridViewAllData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonPRSearchProm_Click(object sender, EventArgs e)
        {
            panelSearchProm.Visible = true;
        }

        private void textBoxPromSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string srPName = textBoxPromSearch.Text;

                if (srPName.Length > 0)
                {
                    this.allProm_DATA.DefaultView.RowFilter = string.Format("PromName like '*{0}*' or PromCode like '*{0}*' ", srPName);

                }
                else
                {
                    this.allProm_DATA.DefaultView.RowFilter = string.Format("1=1 ");
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonCloseSrcStorePN_Click(object sender, EventArgs e)
        {
            panelSearchProm.Visible = false;
        }

        private void dataGridViewAllProm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataID = Int32.Parse(dataGridViewAllProm.Rows[e.RowIndex].Cells["PromID"].Value.ToString());

                comboBoxListPromotion.SelectedValue = dataID;

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonSyntax_Click(object sender, EventArgs e)
        {
            if (panelSynax.Visible == true)
                panelSynax.Visible = false;
            else
                panelSynax.Visible = true;
        }

    

    }
}
