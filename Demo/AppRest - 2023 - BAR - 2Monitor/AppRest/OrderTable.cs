using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing.Printing;
using System.Globalization;
using System.Net;
using System.IO;
using ThaiNationalIDCard;
using System.Threading;

namespace AppRest
{
    public partial class OrderTable : Form
    {
        int tableID;
        GetDataRest gd;

        List<Cat> allCats;
        List<Cat> allCatsBD;
        List<Cat> allCatsRemark;
        List<Table> allTbs;
     
        float taxPercent;
        float servicePercent;

        int memID;

        int catIDSected;
        int catIDSectedRemark;
         
        float orderAmount;
        float totalDiscount;
        float totalServiceCharge;
        float totalVAT; 
        float totalSalesAmount;
         
          

        string flagLang;

        MainTable formMainTable;
        FormCheckBillPay formCheckBill;
        FormCheckBill2 formCheckBill2;
         
        int billID;

        List<Order> orderPrint;
        Table tablePrint;

        Table tableStatus;

        string flagCheckBill;
        int tableCustID;

        int printBillFlag;
         
        string defaultServiceCharge;
        string defalutTax;
        string flagVATOut;


        // เพิ่มเติม พิมบิล ครัว / น้ำ
         

        string txtOrderAppToPrintOne;
        string txtOrderAppToPrint;
        string txtOrderAppToPrintAll;
        string txtOrderAppToPrintPickup;
        string strPrinterOrder;

        string defaultlangBill;

        Table tableCheckBill ;


        // MemCard 

        int grouprestID;
        int restID;
        string statusRestAllUse;

        MemCard mc;
        string memCardID  ;
        string flagExpire;

        List<Product> productlistSr;
        List<Product> productlists;
         
        string flagCheckprintBillBefore;

        string posIDConfig;

        int copyPrint = 0;
        int isSend = 0;
        int custNumber = 0;

        string strRemark = "";

        int keyOrderNo = 0;
        int flagCouponCanUse = 0;

        List<Coupon> couponInput;
        List<Product> productSyntaxLists;

        List<BillRemark> allBillRemark;

        string remarkLine1 = "";
        string remarkLine2 = "";
        string remarkLine3 = "";

        string defalutBarcodeOrder = "";

        int CatPage = 1;
        int CatPageMax = 0;

        int ProPage = 1;
        int ProPageMax = 0;

        int RemPage = 1;
        int RemPageMax = 0;

        string flagLangProduct = "TH"; 

        // Cash Card

        List<CashCard> ccLists ; 

        string flagPrintOneOrderPerbill;
        string flagPrintOrderChecker;

        string restlink;
        string fblink;
        string iglink;
        string qrlink;
        string linelink;

        object senderVoidBill;

        DataTable dataAllProduct;

        int dataGridproductID = 0;
        string dataGridproductName = "";
        string dataGridproductPrice = "";
        string dataGridproductUnit = "";

        string flagOrderShowPrice;
        string flagOrderCheckerShowPrice;
        string flagKitchenMonitor;
        string flagOrderSendNotPrint;

        int kitMonitorCanCheckBill;
        int flagsendDelORder = 0;

        string orderBarcodePrint = "";

        int memIDSelected = 0;

        AddCustomer formAddCustomer;
        List<Customer> allCustomers;

        MainCashCard formMainCashCard;
        AddMemCard formAddMemCard; 
        AddMemCardRenew formAddMemCardRenew;

        string imgPath = "";

        Image imgProduct = null;
        int flagGetImg = 0;

        Image imgDefault = null;
         

        List<POSPrinter> posPrinters;

        int groupCatSelected = 0;
        int groupCatPage = 1;
        int groupCatPageMax = 0;
        List<GroupCat> allGroupCats;

        int productActionType = 0;

        float salesOrderGroup1 = 0;
        float salesOrderGroup2 = 0;
        float salesOrderGroup3 = 0; 
        float salesOrderGroup4 = 0;
        float salesNonServiceCharge = 0;
        float salesNonVAT = 0;
        float salesSubTotal = 0;


        float discount1 = 0;
        float discount2 = 0;
        float discount3 = 0;
        float discount4 = 0;
        float discountAmt = 0;

        float orderPromotion = 0;

        int flagResendORder = 0;
      //  ContextMenu cm;
         

         List<DiscountGroup> discountGroup;
         List<Table> tbs;
         int start = 0;

         DataTable dataTableAllCust;

         string strDisplayLine1 = "";
         string strDisplayLine2 = "";
         string tableZoneVAT = "";
         int tableZonePriceID = 0;


         int orderGetPoint = 0;
         int orderCutPoint = 0;


         Branch branch ;

     

         //   FuncString.displayPOSMonitorSecLine(this.strDisplayLine1,this.strDisplayLine2);


        public OrderTable(Form frmlkFrom, int flagFrmClose, int tableID)
        {
            InitializeComponent();
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }

            this.custNumber = 0;
            this.ControlBox = false;
            this.Text = this.Text + "( By : " + Login.userName + ")";
             

            posPrinters = new List<POSPrinter>(); 
            posLoadPrinter();

            this.StartPosition = FormStartPosition.CenterScreen;

            if (Login.isFrontPOS)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }

            genDefault();
             

            memID = Login.userID;
            this.flagExpire = "N";

            // Config From Program

            imgPath = ConfigurationSettings.AppSettings["PathImages"].ToString();

            flagLang = ConfigurationSettings.AppSettings["Defaultlang"].ToString();
            defaultServiceCharge = ConfigurationSettings.AppSettings["DefalutServicCharge"].ToString();
            defalutTax = ConfigurationSettings.AppSettings["DefalutTax"].ToString();

            restlink = ConfigurationSettings.AppSettings["RestLink"].ToString();
            fblink = ConfigurationSettings.AppSettings["FBLink"].ToString();
            iglink = ConfigurationSettings.AppSettings["IGLink"].ToString();
            qrlink = ConfigurationSettings.AppSettings["QRLink"].ToString();
            linelink = ConfigurationSettings.AppSettings["LINELink"].ToString();

            /* ----- New Feature Printer Checker */


            flagOrderSendNotPrint = ConfigurationSettings.AppSettings["FlagOrderSendNotPrint"].ToString();
            flagOrderShowPrice = ConfigurationSettings.AppSettings["FlagOrderShowPrice"].ToString();
            flagOrderCheckerShowPrice = ConfigurationSettings.AppSettings["FlagOrderCheckerShowPrice"].ToString();
            flagKitchenMonitor = ConfigurationSettings.AppSettings["FlagKitchenMonitor"].ToString();


            if (flagOrderSendNotPrint == "N")
                buttonUpdateflagSend.Visible = false;


            defaultlangBill = ConfigurationSettings.AppSettings["DefaultlangBill"].ToString();

            posIDConfig = ConfigurationSettings.AppSettings["POSIDConfig"].ToString();


          
             
            buttonAllOrder.Enabled = false;

            if (posPrinters[1].FlagPrint.ToLower() == "y")
                buttonAllOrder.Enabled = true;

            printQRWebOrdering.PrinterSettings.PrinterName = posPrinters[0].PrinterName;
            printCash.PrinterSettings.PrinterName = posPrinters[0].PrinterName;
            printOrderDel.PrinterSettings.PrinterName = posPrinters[0].PrinterName; ;
            printOrderAll.PrinterSettings.PrinterName = posPrinters[0].PrinterName; ;
             


            txtOrderAppToPrint = "";
            txtOrderAppToPrintPickup = "";

            if (defalutTax.ToLower() == "y")
                checkBoxTax.Checked = true;

            flagVATOut = ConfigurationSettings.AppSettings["FlagVATOut"].ToString();

           

            radioBoxBillTH.Checked = true;

            ///////////////////////

         

            this.tableID = tableID;
            gd = new GetDataRest(); 
            branch = gd.getBranchDesc();

            allCustomers = gd.getListAllCustomer();

            dataTableAllCust = gd.getDataAllCustomer();
            dataGridViewAllMember.DataSource = dataTableAllCust;


            tableStatus = gd.getOrderTableStatus(tableID);
            Login.selecttablezoneID = tableStatus.ZoneID;
            tableZoneVAT = tableStatus.TableZoneVAT;
            tableZonePriceID = tableStatus.TableZonePriceID;
             

            // List Table
            allTbs = gd.getMainTable(0,1);
            getComboAllTable();

           
            getComboAllCust();

            catIDSected = 13;
            catIDSectedRemark = 1;

            allGroupCats = gd.getAllGroupCat(1);
            genObjOrderGroupCat();

           

            // List Product Category
            allCatsBD = gd.getOrderCat(0);
            allCats = allCatsBD;

            genObjOrderCat();

            if (imgPath.Length == 0)
                this.genObjOrderProduct(catIDSected);
            else
                this.genObjOrderProductImg(catIDSected); 


            allCatsRemark = gd.getOrderCat(1);
            genObjOrderCatRemark();
         //   genObjOrderProductRemark();

            CustListPN.Visible = true; 

            // Tax & Service Charge

            taxPercent = (float)Double.Parse(ConfigurationSettings.AppSettings["TaxPercent"].ToString());
            servicePercent = (float)Double.Parse(ConfigurationSettings.AppSettings["ServiceChargePercent"].ToString());

          
            orderAmount = 0;
            totalDiscount = 0;
            totalServiceCharge = 0;
            totalVAT = 0; 
            totalSalesAmount = 0;

            discountGroup = gd.getAllDiscountGroup();


            labelGroupDis1.Text = discountGroup[0].DiscountGroupNameTH;
            labelGroupDis2.Text = discountGroup[1].DiscountGroupNameTH;
            labelGroupDis3.Text = discountGroup[2].DiscountGroupNameTH;
            labelGroupDis4.Text = discountGroup[3].DiscountGroupNameTH;

             

            // Add Remark
            panelBill.Visible = false;
            AddRemarkOrderPN.Visible = false;


            flagCheckBill = "N";
            billID = 0;

            // Cust Status


            memCardID = "0";
            mc = new MemCard();
             
            textBoxStrSearchMemCard.Text = tableStatus.StrMemSearch;

            searchMemCard(); 
            if(tableStatus.Remark != "FOR:Disc=:0:0:0:0:0:0" )
                this.actionDiscountBySynTax(tableStatus.Remark);
             

            if (tableZoneVAT == "INVAT" || tableZoneVAT == "EXVAT")
                checkBoxTax.Checked = true;

            if (tableStatus.ZoneServiceCharge.ToLower() == "y")
                checkBoxServiceCharge.Checked = true;


            printBillFlag = tableStatus.TablePrintBill;

            // MemCard

            panelMemCard.Visible = false;


            grouprestID = Int32.Parse(ConfigurationSettings.AppSettings["GroupRestID"].ToString());
            restID = Int32.Parse(ConfigurationSettings.AppSettings["RestID"].ToString());
            statusRestAllUse = ConfigurationSettings.AppSettings["StatusRestAllUse"].ToString();

      
            getComboAllCat();

            // Bar Code

            buttonAddOrderBC.Enabled = false;

            flagCheckprintBillBefore = ConfigurationSettings.AppSettings["FlagCheckprintBillBefore"].ToString();
            CheckPrintBill();
             

            strRemark = ConfigurationSettings.AppSettings["POSIDConfig"].ToString();

            keyOrderNo = 1;
            button_01.BackColor = System.Drawing.Color.Orange;
            labelTxtNumOrder.Text = "1";

            allBillRemark = new List<BillRemark>();
            getBillRemark();
             

            defalutBarcodeOrder = ConfigurationSettings.AppSettings["DefalutBarcodeOrder"].ToString();

            if (defalutBarcodeOrder.ToLower() == "y" )
            { 
                panelScanBC.Visible = true;
                buttonBC.Text = "Hide Barcode Scan";
                textBoxSearchBC.Focus();
            } 


            flagPrintOneOrderPerbill = ConfigurationSettings.AppSettings["FlagPrintOneOrderPerbill"].ToString(); 
            flagPrintOrderChecker = ConfigurationSettings.AppSettings["FlagPrintOrderChecker"].ToString(); ;

             

             panelVoidOrder.Visible = false;


             dataGridViewAllProduct.DataSource = dataAllProduct;

             //if (dataGridViewAllProduct.RowCount > 0)
             //{ 
             //    dataGridViewAllProduct.Columns[1].Visible = false;
             //    dataGridViewAllProduct.Columns[2].Visible = false;
             //    dataGridViewAllProduct.Columns[4].Visible = false;
             //    dataGridViewAllProduct.Columns[5].Visible = false;
             //    dataGridViewAllProduct.Columns[6].Visible = false;
             //    dataGridViewAllProduct.Columns[7].Visible = false;
             //    dataGridViewAllProduct.Columns[9].Visible = false;
             //    dataGridViewAllProduct.Columns[10].Visible = false;
             //    dataGridViewAllProduct.Columns[11].Visible = false;
             //    dataGridViewAllProduct.Columns[12].Visible = false;
             //    dataGridViewAllProduct.Columns[14].Visible = false;
             //    dataGridViewAllProduct.Columns[15].Visible = false;
             //    dataGridViewAllProduct.Columns[16].Visible = false;
             //    dataGridViewAllProduct.Columns[17].Visible = false;
             //    dataGridViewAllProduct.Columns[18].Visible = false;
             //    dataGridViewAllProduct.Columns[19].Visible = false;
             //    dataGridViewAllProduct.Columns[20].Visible = false;
             //    dataGridViewAllProduct.Columns[21].Visible = false;
             //}

             this.kitMonitorCanCheckBill = 0;
 

             productlists = gd.getProductByCat(0, 0, 0);

             panelSelPrice.Visible = false;

             if( ConfigurationSettings.AppSettings["FlagManualPriceBC"].ToString() == "Y" )
                 radioButtonManualPrice.Checked = true; 
           

             checkBoxServiceCharge.Visible = false;
             checkBoxTax.Visible = false;

             

              
             formCheckBill = new FormCheckBillPay();

             this.Width = 1024;
             this.Height = 764;
             if (Login.isFrontWide)
             {
                 this.Width = 1280;
                 //allTbs = gd.getMainTable(0);
                 //getComboAllTable();

                 getComboAllTableChange();
                 genObjMainTable();

             }

             radioButtonAppendRemark.Checked = true;

             idcard = new ThaiIDCard();


            // Split Order
            getComboOrderFromTo();
            OrderConfirm();


            buttonClearAllOrder.Visible = false;
             buttonVoidOrder.Visible = false; 


             if (Login.userStatus.ToLower() == "admin" || Login.userStatus.ToLower() == "manager")
             {
                 buttonClearAllOrder.Visible = true;
                 buttonVoidOrder.Visible = true;
               //  buttonMoveTable.Visible = true; 
             }

            if (Login.userStatus.ToLower() == "cashier")
            {
                buttonVoidOrder.Visible = true;
            }

            if (Login.userStatus.ToLower() == "captain")
             {
                 buttonVoidOrder.Visible = true;
             }


             if (Login.userStatus.ToLower() == "work" || Login.userStatus.ToLower() == "captain")
             {
               
                ButtonShowCheck.Visible = false;
                buttonMoveTable.Visible = false;
            }

            if (Login.userStatus.ToLower() == "work"  )
            {
                buttonPrintBill.Visible = false;
            }


            if (Login.userStatus.ToLower() == "work" || Login.userStatus.ToLower() == "captain" || Login.userStatus.ToLower() == "stock" )
             { 
                 textBoxGroupDis2.ReadOnly = true;
                 textBoxGroupDis1.ReadOnly = true;
                 textBoxGroupDis3.ReadOnly = true;
                 textBoxGroupDis4.ReadOnly = true;
                 textBoxDiscountAmt.ReadOnly = true;
                 textBoxAdjustPriceOrder.ReadOnly = true; 
             } 

             panelFinance1.Visible = false;
             panelFinance2.Visible = false;
             buttonCheckBill.Visible = false;
             MoveOrderByItemPN.Visible = false;
             PickupOrderByItemPN.Visible = false;

             if (Login.userStatus.ToLower() == "cashier" ||  Login.userStatus.ToLower() == "admin" || Login.userStatus.ToLower() == "manager")
             {
                 buttonCheckBill.Visible = true;
             }

             buttonMemBerPayByCC.Visible = false;
             

        }

        private void getComboAllTableChange()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            //   data.Add(new KeyValuePair<int, string>(0, " เลือกโต๊ะย้าย "));

            foreach (Table t in allTbs)
            {
                if (t.TableFlagUse == "Y")
                    data.Add(new KeyValuePair<int, string>(t.TableID, t.TableName));

                if (start == 0)
                    comboBoxChangeTable.SelectedValue = this.tableID;

                start = 1;
            }


            // Clear the combobox
            comboBoxChangeTable.DataSource = null;
            comboBoxChangeTable.Items.Clear();

            // Bind the combobox
            comboBoxChangeTable.DataSource = new BindingSource(data, null);
            comboBoxChangeTable.DisplayMember = "Value";
            comboBoxChangeTable.ValueMember = "Key";
        }

        private void genObjMainTable()
        {
            try
            {
                 

                MainTablePN.Controls.Clear();


                tbs = gd.getMainTable(Login.zoneDefaultID);

                int tableID;
                string tableName;
                string tableFlagUse;
                int tableCountOrder;
                int tablePrintBillFlag;
                int tableCustID;
                int tableZoneID;
                Button bTable;

                int sizeX = 80;
                int sizeY = 54;

                int yy = 12;

                int i = 0;
                int j = 0;
                int k = 0;

                int zoneIDold = 1;

                foreach (Table t in tbs)
                {
                    tableID = t.TableID;
                    tableName = t.TableName;
                    tableFlagUse = t.TableFlagUse;
                    tableCountOrder = (int)t.OrderQTY;
                    tablePrintBillFlag = t.TablePrintBill;
                    tableCustID = t.TableCustID;
                    tableZoneID = t.ZoneID;


                    if (tableFlagUse.ToLower() == "y")
                    {

                        bTable = new Button();

                        if (tableZoneID % 2 == 0)
                        {
                            bTable.BackColor = System.Drawing.Color.AntiqueWhite;
                        }
                        else
                        {
                            bTable.BackColor = System.Drawing.Color.White;
                        }


                        bTable.ForeColor = System.Drawing.Color.Black;

                        if (tableCountOrder > 0)
                        {
                            if (tablePrintBillFlag > 0)
                            {

                                if (tableCustID > 0)
                                {
                                    bTable.BackColor = System.Drawing.Color.Red; // ลูกค้ารอลงบิล
                                    bTable.ForeColor = System.Drawing.Color.White;
                                }
                                else
                                {
                                    bTable.BackColor = System.Drawing.Color.Fuchsia; // รอจ่ายเงิน     
                                    bTable.ForeColor = System.Drawing.Color.Black;
                                }
                                k++;
                            }
                            else
                            {

                                bTable.BackColor = System.Drawing.Color.Blue;
                                bTable.ForeColor = System.Drawing.Color.White;
                                j++;

                            }
                        }


                        if ((Login.userTableID == 0) || (Login.userTableID == tableID))
                        {
                            bTable.Cursor = System.Windows.Forms.Cursors.Default;
                            bTable.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            bTable.Font = new System.Drawing.Font("Century Gothic", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));

                            //   bTable.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + ((sizeY) * (i / yy)));
                            bTable.Location = new System.Drawing.Point(1 + ((sizeX) * (i / yy)), 1 + (sizeY * (i % yy)));
                            bTable.Name = tableID.ToString();

                            bTable.Size = new System.Drawing.Size(sizeX, sizeY);
                            bTable.TabIndex = tableCountOrder;
                            bTable.Text = tableName;
                            bTable.UseVisualStyleBackColor = false;
                            bTable.Click += new System.EventHandler(this.bTable_Click);

                            if (tableName.Length == 0)
                                bTable.Visible = false;


                            MainTablePN.Controls.Add(bTable);
                            zoneIDold = tableZoneID;
                            i++;

                        }
                    }

                }

            }
            catch (Exception ex)
            {

                // MessageBox.Show(ex.Message);
            }

        }

        private void bTable_Click(object sender, EventArgs e)
        {

            Button bClick = (Button)sender;

            this.tableID = Int32.Parse(bClick.Name);

            if (this.start == 1)
                txtTableName.Text = bClick.Text;


            tableStatus = gd.getOrderTableStatus(tableID);
            Login.selecttablezoneID = tableStatus.ZoneID;

            this.memIDSelected = tableStatus.TableCustID;

        //    Table table = gd.getMainOrderByTable(this.tableID, this.flagLang, 0, 0, this.memIDSelected, this.subOrderID);
          
            genOrder();
        }


        private void comboBoxChangeTable_SelectedIndexChanged(object sender, EventArgs e)
        { 

            try
            {
                int moveTableID = Int32.Parse(comboBoxChangeTable.SelectedValue.ToString());

                if (this.start == 1)
                    txtTableName.Text = comboBoxChangeTable.Text;

                this.tableID = moveTableID;

                tableStatus = gd.getOrderTableStatus(tableID);
                Login.selecttablezoneID = tableStatus.ZoneID;

                this.memIDSelected = tableStatus.TableCustID;

                Table table = gd.getMainOrderByTable(this.tableID, this.flagLang, 0, 0, this.memIDSelected, this.subOrderID);
               
                genOrder();



                //   tableStatus = gd.getOrderTableStatus(tableID);

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void PEmpty_Click(object sender, EventArgs e)
        {


            MenuItem bClick = (MenuItem)sender;
            string [] txt  = bClick.Text.Split('|'); 
            string pID = txt[1]; 

            gd.updsProductFlag(Int32.Parse(pID), "E", "");
            OrderProductPN.Controls.Clear();


            if (imgPath.Length == 0)
                this.genObjOrderProduct(catIDSected);
            else
                this.genObjOrderProductImg(catIDSected);

        }

        private void PNonUse_Click(object sender, EventArgs e)
        {
            MenuItem bClick = (MenuItem)sender;
            string[] txt = bClick.Text.Split('|');
            string pID = txt[1];

            gd.updsProductFlag(Int32.Parse(pID), "N", "");
            OrderProductPN.Controls.Clear();

            if (imgPath.Length == 0)
                this.genObjOrderProduct(catIDSected);
            else
                this.genObjOrderProductImg(catIDSected);

          
        }

        private void PUse_Click(object sender, EventArgs e)
        {
            MenuItem bClick = (MenuItem)sender;
            string[] txt = bClick.Text.Split('|');
            string pID = txt[1];

            gd.updsProductFlag(Int32.Parse(pID), "Y", "");
            OrderProductPN.Controls.Clear();

            if (imgPath.Length == 0)
                this.genObjOrderProduct(catIDSected);
            else
                this.genObjOrderProductImg(catIDSected);

            
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool bHandled = false;
            // switch case is the easy way, a hash or map would be better, 
            // but more work to get set up.
            switch (keyData)
            {
                case Keys.F9:
                    printAllOrder();
                    break;
                //case Keys.F10:
                //    PrintOrderAndMain();
                //    break;
                case Keys.F11:
                    buttonPrintBill_Click(null, null);
                    bHandled = true;
                    break;
                case Keys.F12:
                    this.buttonCheckBill_Click(null, null);
                    bHandled = true;
                    break;

            }
            return bHandled;
        }

         
        private void genObjOrderGroupCat()
        {

            // int catID;
            int groupcatID;
            string catName;

            string[] productcolor;
            string[] productColorBG;
            string productColorTxt;
            string productcolorFull;

            Button bCat;

            int sizeX = 100;
            int sizeY = 42;
            int yy = 5;

            int i = 0;
             

            this.groupCatPageMax = (allGroupCats.Count / 4) + 1;
             

            if (allGroupCats.Count == 4 || allGroupCats.Count == 5)
            {
                this.groupCatPageMax = 1;
            }
            else if (allGroupCats.Count == 8)
            {
                this.groupCatPageMax = 2;
            }

            int indexstart = 0;
            int indexEnd = 0;

            if (this.groupCatPage == 1 && (allGroupCats.Count == 4 || allGroupCats.Count == 5))
            {
                indexstart = 1;
                indexEnd = allGroupCats.Count;
              //  MessageBox.Show("1");
            }
            else if (this.groupCatPage == 2 && allGroupCats.Count == 8)
            {
                indexstart = 5;
                indexEnd = allGroupCats.Count;
               // MessageBox.Show("2");
            }
            else if (this.groupCatPage == 1)
            {
                indexstart = 1;
                indexEnd = 4;
              //  MessageBox.Show("3");
            }
            else if (this.groupCatPage == 2)
            {
                indexstart = 5;
                indexEnd = 7;

              // MessageBox.Show("4");
            }
            else
            {
              // MessageBox.Show("5");
                indexstart = (this.groupCatPage - 1) * 4 + 1;
                indexEnd = (this.groupCatPage - 1) * 4 + 3;
            }


           if (this.groupCatPageMax > 1 && (this.groupCatPage > 1))
             {
                bCat = new Button();
                bCat.ForeColor = System.Drawing.Color.Black;
                bCat.BackColor = System.Drawing.Color.White;
                bCat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                bCat.Cursor = System.Windows.Forms.Cursors.Default;
                bCat.FlatAppearance.BorderColor = System.Drawing.Color.Green;
                bCat.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                bCat.Name = "Pre";
                bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                bCat.TabIndex = 2;
                bCat.Text = "Pre <<";
                bCat.UseVisualStyleBackColor = false;
                bCat.Click += new System.EventHandler(this.bGroupCatPre_Click);

                OrderGroupCatPN.Controls.Add(bCat);
                i++;
            }

          

            int y = 1;

            foreach (GroupCat t in allGroupCats)
            {
                //  catID = t.CatID;
                catName = t.GroupCatName;
                groupcatID = t.GroupCatID;
                productcolorFull = t.GroupCatColour;

                if ((y >= indexstart && y <= indexEnd))
                {
                     
                    bCat = new Button();


                    bCat.ForeColor = System.Drawing.Color.Black;
                    bCat.BackColor = System.Drawing.Color.LightYellow;


                    if (FuncString.Right(productcolorFull, 2).ToLower() == "|b" || FuncString.Right(productcolorFull, 2).ToLower() == "|w")
                    {

                        productcolor = productcolorFull.Split('|');
                        productColorBG = productcolor[0].Split(',');
                        productColorTxt = productcolor[1];

                        if (productColorTxt.ToLower() == "b")
                        {
                            bCat.ForeColor = System.Drawing.Color.Black;
                        }
                        else
                        {
                            bCat.ForeColor = System.Drawing.Color.White;
                        }


                        bCat.BackColor = Color.FromArgb(Int32.Parse(productColorBG[0]), Int32.Parse(productColorBG[1]), Int32.Parse(productColorBG[2]));

                    }


                    bCat.FlatAppearance.BorderColor = System.Drawing.Color.Aqua;
                    bCat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    bCat.Font = new System.Drawing.Font("Century Gothic", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                    bCat.Name = groupcatID.ToString();
                    bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                    bCat.TabIndex = 2;
                    bCat.Text = catName;
                    bCat.UseVisualStyleBackColor = false;
                    bCat.Click += new System.EventHandler(this.bGroupCat_Click);

                    OrderGroupCatPN.Controls.Add(bCat);
                    i++;
                     
                   

                }

                y++;
            }


             
            if (this.groupCatPageMax >= 1 && (this.groupCatPage < this.groupCatPageMax))
            {
                bCat = new Button();
                bCat.ForeColor = System.Drawing.Color.Black;
                bCat.BackColor = System.Drawing.Color.White;
                bCat.Cursor = System.Windows.Forms.Cursors.Default;
                bCat.FlatStyle = System.Windows.Forms.FlatStyle.Popup; 
                bCat.Font = new System.Drawing.Font("Century Gothic", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                bCat.Name = "Next";
                bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                bCat.TabIndex = 2;
                bCat.Text = "Next >>";
                bCat.UseVisualStyleBackColor = false;
                bCat.Click += new System.EventHandler(this.bGroupCatNext_Click);

                OrderGroupCatPN.Controls.Add(bCat);
                i++;
            }
             
             
        }

 

        private void genAllCat(int groupcatID)
        {
           
            
            this.allCats =  new List<Cat>(); 
            int i = 1;

            foreach (Cat c in allCatsBD)
            {
                if (c.GroupCatID == groupcatID)
                {
                    this.allCats.Add(new Cat(c.CatID, c.GroupCatID, c.CatName ,c.CatNameTH ,c.CatNameEN ,c.CatDesc , 0 , "",c.CatColour, 0 ,0,c.CatFlagUse));
                    i++;
                }
            }

        }


        private void bGroupCat_Click(object sender, EventArgs e)
        {

            Button bClick = (Button)sender;

            string groupcatID = bClick.Name;

            this.ProPage = 1;
            this.CatPage = 1;

            this.groupCatSelected = Int32.Parse(groupcatID);

            genAllCat(this.groupCatSelected);
            OrderCatPN.Controls.Clear(); 
            genObjOrderCat();
             

        }

        private void bGroupCatNext_Click(object sender, EventArgs e)
        {
            this.groupCatPage++;
            OrderGroupCatPN.Controls.Clear();
            genObjOrderGroupCat();

        }

        private void bGroupCatPre_Click(object sender, EventArgs e)
        {
            this.groupCatPage--;
            OrderGroupCatPN.Controls.Clear();
            genObjOrderGroupCat();
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
            strPrinter = "Checker";

            posPrinters.Add(new POSPrinter(21, strPrinter, printerName, flagPrint));

            flagPrint = ConfigurationSettings.AppSettings["flagPrintOrderCheckerOther2"].ToString();
            printerName = ConfigurationSettings.AppSettings["PrinterNameCheckOrder2"].ToString();
            strPrinter = "Checker2";

            posPrinters.Add(new POSPrinter(22, strPrinter, printerName, flagPrint));


        }


        private void genDefault()
        {
            TxtFooter.Text = "Copy Right @ " + ConfigurationSettings.AppSettings["RestName"];
        }


        private void getBillRemark()
        {
            try
            {

                allBillRemark = gd.getAllBillRemark(); 

                foreach (BillRemark c in allBillRemark)
                {
                    this.remarkLine1  = c.BillRemarkL1;
                    this.remarkLine2 = c.BillRemarkL2;
                    this.remarkLine3 = c.BillRemarkL3;
                }

            //    richTextBox1.Text = txtBoxLine1.Text + "\n" + txtBoxLine2.Text + "\n" + txtBoxLine3.Text;

            }
            catch (Exception ex)
            {

            }
        } 



        private void getComboAllTable()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0," เลือกโต๊ะย้าย "));

            foreach (Table t in allTbs)
            {
                if( t.TableFlagUse == "Y" )
                      data.Add(new KeyValuePair<int, string>( t.TableID, t.TableName));
            }
             

            // Clear the combobox
            comboBoxListAllTable.DataSource = null;
            comboBoxListAllTable.Items.Clear();

            // Bind the combobox
            comboBoxListAllTable.DataSource = new BindingSource(data, null);
            comboBoxListAllTable.DisplayMember = "Value";
            comboBoxListAllTable.ValueMember = "Key";
        }

        private void getComboAllCust()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            //Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "== เลือกรายชื่อลูกค้า ==")); 

            foreach (Customer c in allCustomers)
            {
                data.Add(new KeyValuePair<int, string>(c.CustID, c.Name));
            }


            // Clear the combobox
            comboBoxListCust.DataSource = null;
            comboBoxListCust.Items.Clear();

            // Bind the combobox
            comboBoxListCust.DataSource = new BindingSource(data, null);
            comboBoxListCust.DisplayMember = "Value";
            comboBoxListCust.ValueMember = "Key";
             
        }

        string dlOrderNo = "";

        private void genOrder()
        {

            try
            {

                ScreenService.SendDataToSecondMonitor(null);


                DelOrderPN.Controls.Clear();
                MoveOrderByItemPN.Controls.Clear();
                PickupOrderByItemPN.Controls.Clear();


                txtOrderDetail.Text = "DESC";
                txtOrderUnit.Text = "U";
                txtOrderAmt.Text = "A";
                //  printBillTextBox.Text = txtOrderBill + txtOrder;



                this.txtSalesAmount.Text = "0";
                this.textBoxDiscountTotal.Text = "0";
                this.textBoxAddTax.Text = "0";
                this.textBoxAddService.Text = "0";
                this.textBoxSalesTotal.Text = "0";


                salesOrderGroup1 = 0;
                salesOrderGroup3 = 0;
                salesOrderGroup2 = 0;
                salesOrderGroup4 = 0;
                salesNonServiceCharge = 0;
                salesNonVAT = 0;
                orderPromotion = 0;

                salesSubTotal = 0;



                Table table = gd.getMainOrderByTable(this.tableID, this.flagLang, 0, 0, this.memIDSelected, this.subOrderID);

                this.tablePrint = table;

                string tableName;

                tableName = table.TableName;
                this.orderAmount = table.OrderAmount;

                QROrder = table.QROrder;

                List<Order> orders = table.Order;

                this.orderPrint = orders;

                string txtOrder = "";

                string txtOrderApp = "";
                string txtOrderApp1 = "";
                string txtOrderApp2 = "";
                string txtOrderBill = "";
                int i = 1;
                 

                string str1 = "";
                string str2 = "";
                string str3 = "";
                string strNum = "";


                string[] txtProduct;
                string txtProductName = "";
                string txtProductRemark = "";

                this.custNumber = 0;
                this.dlOrderNo = "";

                this.orderGetPoint = 0;
                this.orderCutPoint = 0;

                foreach (Order o in orders)
                {
                    this.salesSubTotal += o.OrderAmount;

                    if (o.OrderProductPoint > 0)
                        this.orderGetPoint += (int)o.OrderProductPoint * (int)o.OrderQTY;

                    if (o.OrderProductPoint < 0)
                        this.orderCutPoint += (int)o.OrderProductPoint * (int)o.OrderQTY;


                    txtProduct = o.ProductName.Split('|');
                    txtProductName = txtProduct[0];  
                    txtProductRemark = txtProduct[1];
                 

                    if (txtProductName.Substring(0, 1) == "-")
                    {
                        strNum = "  ";
                    }
                    else
                    {
                        strNum = i.ToString() + ". ";
                        i++;
                    }

                    if (o.ProductID == 4)
                        this.custNumber += Int32.Parse(o.OrderQTY.ToString());

                    if (o.ProductID == 10)
                        this.dlOrderNo = txtProductRemark.Replace("+","");



                  //  str1 = strNum + o.ProductName + "";
                    str2 = o.OrderQTY.ToString();
                    str3 = o.OrderAmount.ToString("###,###.#");


                    if (o.Flagsend == 0)
                    {
                        txtProductName += " [*Cancle*] ";
                        o.OrderAmount = 0;
                        str3 = "0";
                    }
                     
  

                    if (txtProductName.Length >= 60)
                    {
                        txtOrderApp += strNum + txtProductName.Substring(0, 29) + "\r\n";
                        txtOrderApp += "    " + txtProductName.Substring(29, 29) + "\r\n";
                        txtOrderApp += "    " + txtProductName.Substring(59, txtProductName.Length - 59) + "\r\n";
                    }
                    else if (txtProductName.Length >= 30)
                    {
                        txtOrderApp += strNum + txtProductName.Substring(0, 29) + "\r\n";
                        txtOrderApp += "    " + txtProductName.Substring(29, txtProductName.Length - 29) + "\r\n"; 
                    }
                    else
                    {
                        txtOrderApp += strNum + txtProductName + "\r\n"; 
                    }


                    txtOrderApp1 += str2 + "\r\n";
                    txtOrderApp2 += str3 + "" + "\r\n";

                    if (txtProductName.Length >= 60)
                    {
                        txtOrderApp1 += "\r\n\r\n";
                        txtOrderApp2 += "\r\n\r\n";
                    }
                    else if (txtProductName.Length >= 30)
                    {
                        txtOrderApp1 += "\r\n";
                        txtOrderApp2 += "\r\n";
                    }



                    if (txtProductRemark.Trim().Length > 1)
                    {
                        string[] remarkString = txtProductRemark.Remove(0, 1).Split('+');

                        foreach (string r in remarkString)
                        {
                            txtOrderApp += "   +" + r.Split('{')[0] + "\r\n";
                            txtOrderApp1 += "\r\n";
                            txtOrderApp2 += "\r\n";
                        }
                    }

                    /// Cat Tail Bill

                    if (txtProductRemark.Contains("[NS]") || o.FlagNonServiceCharge == 1) 
                        salesNonServiceCharge += o.OrderAmount;

                    if (o.FlagNonVAT == 1)
                        salesNonVAT += o.OrderAmount;


                    if (o.RemarkOrderAmt < 0 || o.ProductDesc.Contains("{ND}") || o.FlagNonDiscount == 1 || txtProductRemark.Trim().Contains("+PRO")) // SET MENU
                    { 
                        orderPromotion += o.OrderAmount; 
                    }
                    else
                    {
                        
                        if(o.DiscountGroupID == 1)
                            this.salesOrderGroup1 += o.OrderAmount;
                        else if (o.DiscountGroupID == 2)
                            this.salesOrderGroup2 += o.OrderAmount;
                        else if (o.DiscountGroupID == 3)
                            this.salesOrderGroup3 += o.OrderAmount;
                        else  
                            this.salesOrderGroup4 += o.OrderAmount; 

                    } 

                }


                this.txtTableName.Text = tableName + " : " + this.dlOrderNo ;

               

                txtOrder += "\r\n";
                txtOrder += "            " + this.discountGroup[0].DiscountGroupName + " : " + this.salesOrderGroup1.ToString("###,###.#0") + " B." + "\r\n";
                txtOrder += "            " + this.discountGroup[1].DiscountGroupName + " : " + this.salesOrderGroup2.ToString("###,###.#0") + " B." + "\r\n";
                txtOrder += "            " + this.discountGroup[2].DiscountGroupName + " : " + this.salesOrderGroup3.ToString("###,###.#0") + " B." + "\r\n";
                txtOrder += "            " + this.discountGroup[3].DiscountGroupName + " : " + this.salesOrderGroup4.ToString("###,###.#0") + " B." + "\r\n";
                txtOrder += "            ORDER PROMOTION   : " + this.orderPromotion.ToString("###,###.#0") + " B." + "\r\n\r\n";
             //   txtOrder += "            ORDER NonVAT     : " + this.salesNonVAT.ToString("###,###.#0") + " B." + "\r\n" + "\r\n";

                
                
                txtOrder += "            ORDER AMOUNT     : " + this.orderAmount.ToString("###,###.#0") + " B." + "\r\n" ;

                     
                getDiscount();

                if (this.totalDiscount > 0)
                {
           
                    if (discount1 > 0)
                        txtOrder += "           " + this.discountGroup[0].DiscountGroupNameEN + " " + (discount1 * 100).ToString() + " : " + (this.salesOrderGroup1 * discount1).ToString("###,###.#0") + " B." + "\r\n";

                    if (discount2 > 0)
                        txtOrder += "           " + this.discountGroup[1].DiscountGroupNameEN + " " + (discount2 * 100).ToString() + " : " + (this.salesOrderGroup2 * discount2).ToString("###,###.#0") + " B." + "\r\n";

                    if (discount3 > 0)
                        txtOrder += "           " + this.discountGroup[2].DiscountGroupNameEN + " " + (discount3 * 100).ToString() + " : " + (this.salesOrderGroup3 * discount3).ToString("###,###.#0") + " B." + "\r\n";
                  
                    if (discount4 > 0)
                        txtOrder += "           " + this.discountGroup[3].DiscountGroupNameEN + "  " + (discount4 * 100).ToString() + " : " + (this.salesOrderGroup4 * discount4).ToString("###,###.#0") + " B." + "\r\n";
                    
                    if(discountAmt >0)
                        txtOrder += "            Discount Amount (B.) : " + discountAmt.ToString("###,###.#0") + " B." + "\r\n";

                        txtOrder += "            Total Discount : " + totalDiscount.ToString("###,###.#0") + " B." + "\r\n";
                     
                }

                 

                if (this.totalServiceCharge > 0)
                    if (checkBoxServiceCharge.Checked == true)
                    {
                        txtOrder += "            Service Charge " + ((float)(this.servicePercent * 100)).ToString() + "% ++++++ : " + this.totalServiceCharge.ToString("###,###.#0") + " B." + "\r\n";
                    }

                if (this.totalVAT > 0)
                    if (checkBoxTax.Checked == true)
                    {
                        txtOrder += "            VAT " + ((float)(this.taxPercent * 100)).ToString() + "% ++++ : " + this.totalVAT.ToString("###,###.#0") + " B." + "\r\n"  ;
                    }

                 
                this.txtSalesAmount.Text = this.totalSalesAmount.ToString("###,###.#0");
                this.textBoxDiscountTotal.Text = this.totalDiscount.ToString("###,###.#0");
                this.textBoxAddTax.Text = this.totalVAT.ToString("###,###.#0");
                this.textBoxAddService.Text = this.totalServiceCharge.ToString("###,###.#0");
                this.textBoxSalesTotal.Text = this.totalSalesAmount.ToString("###,###.#0");

                this.strDisplayLine2 = this.totalSalesAmount.ToString("###,###.#0");

                FuncString.displayPOSMonitorSecLine(this.strDisplayLine1, this.strDisplayLine2);


                ScreenService.SendDataToSecondMonitor(new DisplayData
                {
                    Orders = orders,
                    DiscountTotal = this.totalDiscount,
                    SalesTotal = this.totalSalesAmount,
                    SubTotal = this.salesSubTotal,
                    ServiceCharge = this.totalServiceCharge,
                    Vat = this.totalVAT
                });


                // delete Order

                int sizeX = 80; // เดิม 45
                int sizeY = 21; // เดิม 15  ข้างนอกใช้ ตัวหนังสือ 9 
                int yy = 1;

                i = 0;

                DelOrderPN.Controls.Clear();
                MoveOrderByItemPN.Controls.Clear();
                PickupOrderByItemPN.Controls.Clear();

                Button bDel;

                string timetoStart = "";

                foreach (Order o in orders)
                {
                   this.staffName = o.TimetoOrder;

                    txtProduct = o.ProductName.Split('|');
                    txtProductName = txtProduct[0];
                    txtProductRemark = txtProduct[1];

                    timetoStart = o.TimetoOrder;


                    if (o.Flagsend == 0)
                    {
                        txtProductName += " [*Cancle*] ";
                        o.OrderAmount = 0;
                        str3 = "0";
                    }


                    bDel = new Button();

                    if (o.Flagsend == 1)
                    {
                        bDel.BackColor = System.Drawing.Color.Red;
                        bDel.ForeColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        //if (flagKitchenMonitor == "N")
                        //{
                        //    bDel.BackColor = System.Drawing.Color.Green;
                        //    bDel.ForeColor = System.Drawing.Color.White; 
                        //} 

                        if (o.Flagsend == 2)
                        {
                            bDel.BackColor = System.Drawing.Color.Green;
                            bDel.ForeColor = System.Drawing.Color.White;
                        }
                        else if (o.Flagsend == 3)
                        {
                            bDel.BackColor = System.Drawing.Color.BlueViolet;
                            bDel.ForeColor = System.Drawing.Color.White;
                        }
                        else if (o.Flagsend == 4)
                        {
                            bDel.BackColor = System.Drawing.Color.White;
                            bDel.ForeColor = System.Drawing.Color.Black;

                            this.kitMonitorCanCheckBill = 1;
                        }

                        if (txtProductRemark.Contains("HOLD"))
                        {
                            bDel.BackColor = System.Drawing.Color.Yellow;
                            bDel.ForeColor = System.Drawing.Color.Yellow;
                        }

                        //if (Login.userStatus.ToLower() == "work" || Login.userStatus.ToLower() == "cashier")
                        //    bDel.Visible = false;



                        this.isSend = 1;
                    } 


                    

                    bDel.Font = new System.Drawing.Font("Century Gothic", 7, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));

                    bDel.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                    bDel.Name = o.ProductID.ToString();
                    bDel.Size = new System.Drawing.Size(sizeX, sizeY);
                    bDel.TabIndex = 4;
                    bDel.Text = "  " + o.TimetoOrder + "   |" + txtProductName + '|' + o.CreateDate.ToString() + '|' + o.OrderBarcode + '|' + o.OrderQTY.ToString() + '|' + txtProductRemark;



                    if (o.Flagsend == 0)
                    { 
                        bDel.BackColor = System.Drawing.Color.Black;
                        bDel.ForeColor = System.Drawing.Color.White;
                        bDel.Text = "  *Cancle*";
                        bDel.Enabled = false;
                    }

                    
                    bDel.UseVisualStyleBackColor = false;
                    bDel.Click += new System.EventHandler(this.bDel_Click);

                    DelOrderPN.Controls.Add(bDel);



                    //---- Move Order by Table



                    Button bMoveOrder = new Button();

                    bMoveOrder.BackColor = System.Drawing.Color.Yellow;
                    bMoveOrder.ForeColor = System.Drawing.Color.Yellow;


                    bMoveOrder.Font = new System.Drawing.Font("Century Gothic", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));

                    bMoveOrder.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                    bMoveOrder.Name = o.ProductID.ToString();
                    bMoveOrder.Size = new System.Drawing.Size(sizeX, sizeY);
                    bMoveOrder.TabIndex = 4;
                    bMoveOrder.Text = "  " + o.TimetoOrder + "   |" + txtProductName + '|' + o.CreateDate.ToString() + '|' + o.OrderBarcode + '|' + o.OrderQTY ;
                    bMoveOrder.UseVisualStyleBackColor = false;
                    bMoveOrder.Click += new System.EventHandler(this.bMoveOrder_Click);


                    if (o.Flagsend == 0)
                    {
                        bMoveOrder.Visible = false;
                    }

                    MoveOrderByItemPN.Controls.Add(bMoveOrder);


                    //---- Move Order by Table



                    Button bHoldOrder = new Button();

                    bHoldOrder.BackColor = System.Drawing.Color.Black;
                    bHoldOrder.ForeColor = System.Drawing.Color.Black;


                    bHoldOrder.Font = new System.Drawing.Font("Century Gothic", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));

                    bHoldOrder.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                    bHoldOrder.Name = o.ProductID.ToString();
                    bHoldOrder.Size = new System.Drawing.Size(sizeX, sizeY);
                    bHoldOrder.TabIndex = 4;
                    bHoldOrder.Text = "  " + o.TimetoOrder + "   |" + txtProductName + '|' + o.CreateDate.ToString() + '|' + o.OrderBarcode + '|' + o.OrderQTY;
                    bHoldOrder.UseVisualStyleBackColor = false;
                    bHoldOrder.Click += new System.EventHandler(this.bHoldOrder_Click);

                    bHoldOrder.Visible = false;




                    if (txtProductRemark.ToLower().Contains("hold") == true)
                    {
                        bHoldOrder.Visible = true;
                    }


                    PickupOrderByItemPN.Controls.Add(bHoldOrder);



                    if (txtProductRemark.Trim().Length > 1)
                    {
                        string[] remarkString = txtProductRemark.Remove(0, 1).Split('+');

                        foreach (string r in remarkString)
                            i++;
                    }


                    i++;


                    if (txtProductName.Length >= 60)
                    {
                        i++;
                        i++;
                    }
                    else if (txtProductName.Length >= 30)
                    {
                        i++;
                    }
                }




                // Add Remark 

                i = 0;

                AddRemarkOrderPN.Controls.Clear();

                Button bRemark;

                foreach (Order o in orders)
                {


                    txtProduct = o.ProductName.Split('|');
                    txtProductName = txtProduct[0];
                    txtProductRemark = txtProduct[1];


                    if (o.Flagsend == 0)
                    {
                        txtProductName += " [*Cancle*] ";
                        o.OrderAmount = 0;
                        str3 = "0";
                    }


                    bRemark = new Button();

                    //if (o.Flagsend == 1)
                    //{
                    //    bRemark.BackColor = System.Drawing.Color.Blue;
                    //    bRemark.ForeColor = System.Drawing.Color.Blue;
                    //}
                    //else
                    //{
                    //    bRemark.BackColor = System.Drawing.Color.Green;
                    //    bRemark.ForeColor = System.Drawing.Color.Green;

                    //    // this.isSend = 1;

                    //   // bRemark.Visible = false;

                    //    //if (Login.userStatus.ToLower() == "work")
                    //    //    bDel.Visible = false;
                    //}

                    bRemark.BackColor = System.Drawing.Color.Blue;
                    bRemark.ForeColor = System.Drawing.Color.Blue;



                    bRemark.Font = new System.Drawing.Font("Century Gothic", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));

                    bRemark.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                    bRemark.Name = o.ProductID.ToString();
                    bRemark.Size = new System.Drawing.Size(sizeX, sizeY);
                    bRemark.TabIndex = 4;
                    bRemark.Text = txtProductName + '|' + o.OrderBarcode.ToString();
                    bRemark.UseVisualStyleBackColor = false;
                    bRemark.Click += new System.EventHandler(this.bRemark_Click);



                    if (o.Flagsend == 0)
                    {
                        bRemark.Visible = false;
                    }

                    AddRemarkOrderPN.Controls.Add(bRemark);


                    if (txtProductRemark.Trim().Length > 1)
                    {
                        string[] remarkString = txtProductRemark.Remove(0, 1).Split('+');

                        foreach (string r in remarkString)
                            i++;
                    }

                    i++;

                    if (txtProductName.Length >= 60)
                    {
                        i++;
                        i++;
                    }
                    else if (txtProductName.Length >= 30)
                    {
                        i++;
                    }
                }



                txtOrderDetail.Text = txtOrderApp + txtOrder;
                txtOrderUnit.Text = txtOrderApp1;
                txtOrderAmt.Text = txtOrderApp2;
                printBillTextBox.Text = txtOrderBill + txtOrder;

                //    MessageBox.Show("555");


             //   textBoxCustShare.Text = this.orderCutPoint.ToString();



                checkButtonClearAll();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            } 

            

        }

        private void bMoveOrder_Click(object sender, EventArgs e)
        {

            try
            {




                Button bClick = (Button)sender;

                string productID = bClick.Name;
                string productName = "";
                string orderBarcodePrint = "";
                string createDate = "";

                string[] txt = bClick.Text.Split('|');
                productName = txt[1];
                createDate = txt[2];
                orderBarcodePrint = txt[3];

               // string color = bClick.BackColor.ToString();

               

                int moveTableID = Int32.Parse( comboBoxListAllTable.SelectedValue.ToString() );


                if (moveTableID != 0)
                {

                    if (MessageBox.Show("คุณต้องการจะย้ายออร์เดอร์ " + bClick.Text + "ไปยัง" + comboBoxListAllTable.Text + " นี้หรือไม่ ? ", "ย้ายออร์เดอร์นี้", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updsMoveOrderByTable(this.tableID, Int32.Parse(productID), Login.userID, orderBarcodePrint, moveTableID);
                         


                        if (productName.Trim().Substring(0, 1) != "-")
                        {
                            this.txtOrderAppToPrint = "" + productName + "\n\r";
                            this.txtOrderAppToPrint += "  Time " + "\n\r";
                            this.txtOrderAppToPrint += " " + createDate.Substring(0, 17) + "\n\r\n";


                            if (result > 0)  // ออก Checker 
                                SelectPrinterMove(result, posPrinters[21].PrinterName);
                        }

                         


                      //  genObjOrderCat();
                        genOrder();
                    }
                }
                else
                {
                    MessageBox.Show("ย้ายโต๊ะ : กรุณาเลือกโต๊ะ !!");
                }


            }
            catch (Exception ex)
            {

            }

        }


        private void bHoldOrder_Click(object sender, EventArgs e)
        {

            try
            {


                Button bClick = (Button)sender;

                string productID = bClick.Name;
                string productName = ""; 
                string createDate = "";
                string productQty = "";

                string[] txt = bClick.Text.Split('|');
                productName = txt[1].Replace("-","");
                createDate = txt[2];
                orderBarcodePrint = txt[3];
                productQty = txt[4];


                string reason = "Pick Up Order : "   ;

                this.txtOrderAppToPrint = " (" + productQty + ") " + productName + "\n\r";
                this.txtOrderAppToPrint += "\n\r" + reason;

                int result = gd.delOrderByTable(this.tableID, Int32.Parse(productID), orderBarcodePrint, reason + "[RS]");
                  
                flagResendORder = 1;

                if (result > 0)
                    SelectPrinterDel(result, posPrinters[result].PrinterName);

               //  SelectPrinterDel(99, posPrinters[0].PrinterName);

                //if (posPrinters[22].FlagPrint == "Y")
                //{
                //    result = gd.delOrderByTable(this.tableID, Int32.Parse(productID), orderBarcodePrint, reason + "[RS2]");
                     
                //    if (result > 0)
                //        SelectPrinterDel(result, posPrinters[0].PrinterName);
                //}


                flagResendORder = 0;

                genOrder();
               

            }
            catch (Exception ex)
            {

            }

        }


        private void bDel_Click(object sender, EventArgs e)
        {

            Button bClick = (Button)sender;

            string productID = bClick.Name;
            string productName = ""; 
            string createDate = "";
            string orderBarcode = "";

            string[] txt = bClick.Text.Split('|');
            productName = txt[1];
            createDate = txt[2];
            orderBarcode = txt[3];

            string color = bClick.BackColor.ToString();

        
            this.senderVoidBill = sender;
            string reason = "";

            if (defalutBarcodeOrder != "Y")
            {
                
                if (bClick.BackColor != Color.Red)
                { 

                    if (bClick.BackColor == Color.Green)
                        flagsendDelORder = 2;
                    else if (bClick.BackColor == Color.BlueViolet)
                        flagsendDelORder = 3;
                    else
                        flagsendDelORder = 4; 

                    string text = "";

                    text = ">> " + productName + "\n\r";
        

                    labelVoid.Text = text;

                    panelVoidOrder.Visible = true;
                }
                else
                {
                    reason = "Not Send Order yet";

                    int result = gd.delOrderByTable(this.tableID, Int32.Parse(productID), orderBarcode, reason);
                }
            }
            else
            {
                int result = gd.delOrderByTable(this.tableID, Int32.Parse(productID), orderBarcode, reason);
          
            }
             
 
            genOrder();

        }


        private void bRemark_Click(object sender, EventArgs e)
        {

            try
            {

                Button bClick = (Button)sender;

                string productID = bClick.Name;
                string productName = "";

                string createDate = "";

                string[] txt = bClick.Text.Split('|');
                productName = txt[0];
                createDate = txt[1];

                string remark = textBoxReason.Text;

                string color = bClick.BackColor.ToString();

                float addOrderAmt = float.Parse(textBoxAdjustPriceOrder.Text.ToString());


                int flagAddOrderRemBaht = 0;
                int flagAddOrderRemByItem = 0;

                if (radioButtonOrderRemBaht.Checked == true)
                    flagAddOrderRemBaht = 1;
                else
                    flagAddOrderRemBaht = 0;

                if (radioButtonRemByItem.Checked == true)
                    flagAddOrderRemByItem = 1;
                else if (radioButtonRemByProduct.Checked == true)
                    flagAddOrderRemByItem = 2;
                else if (radioButtonAppendRemark.Checked == true)
                    flagAddOrderRemByItem = 3;



                int result = gd.instRemarkOrderByTable(this.tableID, Int32.Parse(productID), Login.userID, createDate, remark, addOrderAmt, flagAddOrderRemBaht, flagAddOrderRemByItem);

                if (result <= 0)
                    MessageBox.Show("Error Add Remark order By Table");

                textBoxReason.Text = "";
                textBoxAdjustPriceOrder.Text = "0";
                 
                genOrder();

            }
            catch (Exception ex)
            {
                

            }

        }

         

        private void getDiscount()
        {
            try
            {
                discount1 = 0;
                discount2 = 0;
                discount3 = 0;
                discount4 = 0;
                discountAmt = 0;



               float  discountAll = 0;
               discount1 = float.Parse(textBoxGroupDis1.Text) / 100;
               discount2 = float.Parse(textBoxGroupDis2.Text) / 100; 
               discount3 = float.Parse(textBoxGroupDis3.Text) / 100;
               discount4 = float.Parse(textBoxGroupDis4.Text) / 100;


                 discountAmt = float.Parse(textBoxDiscountAmt.Text);

                 discountAll = discountAmt + salesOrderGroup2 * discount2 +
                        salesOrderGroup1 * discount1 + salesOrderGroup3 * discount3 +
                        salesOrderGroup4 * discount4;
                 
                this.totalDiscount = (float) Math.Round(discountAll);

                getTaxAndService();

          


            }
            catch (Exception ex)
            {
               // MessageBox.Show("Error Discount");
            }

        }

        float diffAmount = 0;

        private void getTaxAndService()
        {
            float serviceCharge = 0;
            float vat = 0;

            diffAmount = 0;

            if (checkBoxServiceCharge.Checked == true)
                serviceCharge = (float)((this.orderAmount - this.salesNonServiceCharge - this.totalDiscount) * this.servicePercent); // คิดเฉพาะอาหาร

            if (checkBoxTax.Checked == true)
                if (tableZoneVAT == "INVAT")
                    vat = (float)((this.orderAmount - this.totalDiscount - this.salesNonVAT + serviceCharge) * (7.00 / 107.00));
                else if (tableZoneVAT == "EXVAT")
                    vat = (float)((this.orderAmount - this.totalDiscount - this.salesNonVAT + serviceCharge) * (0.07));
                else if (tableZoneVAT == "NOVAT")
                    vat = 0;

            this.totalServiceCharge = serviceCharge;
            this.totalVAT = vat;

            if (tableZoneVAT == "INVAT")
                this.totalSalesAmount = this.orderAmount - this.totalDiscount + this.totalServiceCharge;
            else if (tableZoneVAT == "EXVAT")
                this.totalSalesAmount = this.orderAmount - this.totalDiscount + this.totalServiceCharge + this.totalVAT;
            else if (tableZoneVAT == "NOVAT")
                this.totalSalesAmount = this.orderAmount - this.totalDiscount + this.totalServiceCharge;


            if (this.totalSalesAmount < 0)
                this.totalSalesAmount = 0;

            this.diffAmount = (float)Math.Round(this.totalSalesAmount, 0) - this.totalSalesAmount; ;

            this.totalSalesAmount =  (float) Math.Round( this.totalSalesAmount , 0 );


            this.textBoxAddTax.Text = this.totalVAT.ToString("###,###.#0");
            this.textBoxAddService.Text = this.totalServiceCharge.ToString("###,###.#0");

            this.textBoxDiscountTotal.Text = this.totalDiscount.ToString("###,###.#0");
            this.textBoxSalesTotal.Text = this.totalSalesAmount.ToString("###,###.#0");
             
        }

        private void genObjOrderCat()
        {

            int catID;
            int groupcatID;
            string catName; 

            Button bCat;

            int sizeX = 99;
            int sizeY = 47;
            int yy = 5;

            int i = 0;

            string[] productcolor;
            string[] productColorBG;
            string productColorTxt;
            string productcolorFull;


           
                 
                //CatPage = 1;
                this.CatPageMax = (allCats.Count / 13) + 1;

                if (allCats.Count == 14 || allCats.Count == 15)
                {
                    this.CatPageMax = 1;
                }
                else if (allCats.Count == 28 )
                {
                    this.CatPageMax = 2;
                }


                int indexstart = 0;
                int indexEnd = 0;

                if (this.CatPage == 1 && (allCats.Count == 14 || allCats.Count == 15))
                {
                    indexstart = 1;
                    indexEnd = allCats.Count;
                }
                else if (this.CatPage == 2  && allCats.Count == 28)
                {
                    indexstart = 15;
                    indexEnd = allCats.Count;
                }  
                else if (this.CatPage == 1)
                {
                    indexstart = 1;
                    indexEnd = 14;
                }
                else if (this.CatPage == 2)
                {
                    indexstart = 15;
                    indexEnd = 27; 
                }
                else
                {
                    indexstart = (this.CatPage - 1) * 13 + 1;
                    indexEnd = (this.CatPage - 1) * 13 + 13;
                }



                if (this.CatPageMax > 1 && (this.CatPage > 1))
                {
                    bCat = new Button();
                    bCat.ForeColor = System.Drawing.Color.Black;
                    bCat.BackColor = System.Drawing.Color.White;
                    bCat.Cursor = System.Windows.Forms.Cursors.Default;
                    bCat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    bCat.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                    bCat.Name = "Pre";
                    bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                    bCat.TabIndex = 2;
                    bCat.Text = "Pre <<";
                    bCat.UseVisualStyleBackColor = false;
                    bCat.Click += new System.EventHandler(this.bCatPre_Click);

                    OrderCatPN.Controls.Add(bCat);
                    i++;
                }

                int y = 1;

                foreach (Cat t in allCats)
                {
                    catID = t.CatID;
                    catName = t.CatName;
                    groupcatID = t.GroupCatID;

                    productcolorFull = t.CatColour;

                    if ((catID > 0 && catID != 99) && (y >= indexstart && y <= indexEnd))
                    {


                        bCat = new Button();


                        bCat.ForeColor = System.Drawing.Color.Black;
                        bCat.BackColor = System.Drawing.Color.LightYellow;


                        if (FuncString.Right(productcolorFull, 2).ToLower() == "|b" || FuncString.Right(productcolorFull, 2).ToLower() == "|w")
                        {

                            productcolor = productcolorFull.Split('|');
                            productColorBG = productcolor[0].Split(',');
                            productColorTxt = productcolor[1];

                            if (productColorTxt.ToLower() == "b")
                            {
                                bCat.ForeColor = System.Drawing.Color.Black;
                            }
                            else
                            {
                                bCat.ForeColor = System.Drawing.Color.White;
                            }


                            bCat.BackColor = Color.FromArgb(Int32.Parse(productColorBG[0]), Int32.Parse(productColorBG[1]), Int32.Parse(productColorBG[2]));

                        }


                        bCat.FlatAppearance.BorderColor = System.Drawing.Color.Aqua;
                        bCat.Font = new System.Drawing.Font("Century Gothic", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                        bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                        bCat.Name = catID.ToString();
                        bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                        bCat.TabIndex = 2;
                        bCat.Text = catName;
                        bCat.UseVisualStyleBackColor = false;
                        bCat.Click += new System.EventHandler(this.bCat_Click);

                        OrderCatPN.Controls.Add(bCat);
                        i++;

                    }

                    if ((catID > 0 && catID != 99))
                        y++;

                }

            if (this.CatPageMax >= 1 && (this.CatPage < this.CatPageMax))
            {
                bCat = new Button();


                bCat.ForeColor = System.Drawing.Color.Black;
                bCat.BackColor = System.Drawing.Color.LightYellow;

                bCat.Cursor = System.Windows.Forms.Cursors.Default;
                bCat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                bCat.FlatAppearance.BorderColor = System.Drawing.Color.Green;
                bCat.Font = new System.Drawing.Font("Century Gothic", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                bCat.Name = "Next";
                bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                bCat.TabIndex = 2;
                bCat.Text = "Next >>";
                bCat.UseVisualStyleBackColor = false;
                bCat.Click += new System.EventHandler(this.bCatNext_Click);

                OrderCatPN.Controls.Add(bCat);
                i++;
            }

           
          

        }


        private void genObjOrderCatRemark()
        {

            int catID;
            string catName;

            Button bCat;

            int sizeX = 95;
            int sizeY = 60;
            int yy = 5;

            int i = 0;

            panelCatRemark.Controls.Clear();
             

            foreach (Cat t in allCatsRemark)
            {
                catID = t.CatID;
                catName = t.CatName;

                this.catIDSectedRemark = catID;

                if (  catID > 0 )  
                { 

                    bCat = new Button();
                    bCat.ForeColor = System.Drawing.Color.White;
                    bCat.BackColor = System.Drawing.Color.Brown;
                    bCat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    bCat.FlatAppearance.BorderColor = System.Drawing.Color.Aqua; 
                    bCat.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                    bCat.Name = catID.ToString();
                    bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                    bCat.TabIndex = 2;
                    bCat.Text = catName;
                    bCat.UseVisualStyleBackColor = false;
                    bCat.Click += new System.EventHandler(this.bCatRemark_Click);

                    panelCatRemark.Controls.Add(bCat);
                    i++;

                }  
            }
   
        }

        private void bCatRemark_Click(object sender, EventArgs e)
        { 
            Button bClick = (Button)sender; 
            string catID = bClick.Name; 

            this.catIDSectedRemark = Int32.Parse(catID);
            OrderProductRemarkPN.Controls.Clear();
            this.genObjOrderProductRemark();

        } 
          
        private void bCat_Click(object sender, EventArgs e)
        {

            Button bClick = (Button)sender;

            string catID = bClick.Name;

            this.ProPage = 1;

            this.catIDSected =  Int32.Parse(catID);
            OrderProductPN.Controls.Clear();


            if (imgPath.Length == 0)
                this.genObjOrderProduct(catIDSected);
            else
                this.genObjOrderProductImg(catIDSected); 
              
        }

        private void bCatNext_Click(object sender, EventArgs e)
        { 
            this.CatPage++;
            OrderCatPN.Controls.Clear();
            genObjOrderCat();

        }

        private void bCatPre_Click(object sender, EventArgs e)
        { 
            this.CatPage--;
            OrderCatPN.Controls.Clear();
            genObjOrderCat();
        }

        List<Product> productByCat = new List<Product>();


        private void genObjOrderProduct(int catID)
        {

            try{

            int productID;
            string productName;
            float productPrice;
            string productFlagUse;
            string productDesc;
            string[] productcolor;
            string[] productColorBG;
            string productColorTxt;
            string productcolorFull;

            Button bCat;

            int sizeX = 100;
            int sizeY = 102;
            int yy = 5;

            int i = 0;

                 

            this.productByCat = gd.getProductByCat(catID, Login.selecttablezoneID, this.memIDSelected);



           //  MessageBox.Show("1");

             //MessageBox.Show(productByCat.Count.ToString());

            //CatPage = 1;
            if (productByCat.Count % 20 == 0)
                this.ProPageMax = (productByCat.Count / 20);
            else
                this.ProPageMax = (productByCat.Count / 20) + 1;

            int indexstart = 0;
            int indexEnd = 0;


            if (this.ProPageMax == 1)
            {
                indexstart = 1;
                indexEnd = 20;
                buttonProPre.Visible = false;
                buttonPorNext.Visible = false;
            }
            else
            {
                indexstart = (this.ProPage - 1) * 20 + 1;
                indexEnd = (this.ProPage - 1) * 20 + 20;

                 buttonProPre.Visible = true;
                 buttonPorNext.Visible = true;
            }

            if (this.ProPage == 1)
            {
                buttonProPre.Visible = false;
            }
            else if (this.ProPage == this.ProPageMax)
            {
                buttonPorNext.Visible = false;
            }

            if (this.ProPageMax == 1)
            { 
                buttonProPre.Visible = false;
                buttonPorNext.Visible = false;
            }


            int y = 1;

            productSyntaxLists = new List<Product>();


            ContextMenu cm;
                

            foreach (Product t in productByCat)
            {
                productID = t.ProductID;
                productName = t.ProductName;
                productPrice = t.ProductPrice;
                productFlagUse = t.ProductFlagUse;
                productDesc = t.ProductDesc; 
                productcolorFull = t.ProductColour;

             //   MessageBox.Show(productPrice.ToString());

                if (this.flagLangProduct == "EN")
                    productName = t.ProductNameEN;
                if (this.flagLangProduct == "TH")
                    productName = t.ProductName;
                else if (this.flagLangProduct == "OT")
                    productName = t.ProductDesc;

                // Syntax
                if( productDesc.Length > 4 )
                     if( productDesc.Substring(0,4) == "FOR:" )  
                              productSyntaxLists.Add(new Product(productID, productDesc));


               

                if (productFlagUse.ToLower() == "y" || productFlagUse.ToLower() == "e") 
                {

                    bCat = new Button();

                    bCat.ForeColor = System.Drawing.Color.Black;
                    bCat.BackColor = System.Drawing.Color.LightYellow;

                         

                    if (y >= indexstart && y <= indexEnd)
                    {

                        cm = new ContextMenu();
                     
                        cm.MenuItems.Add("Product Empty" + "| " + productID.ToString(), new EventHandler(PEmpty_Click));
                        cm.MenuItems.Add("Product Use" + "| " + productID.ToString(), new EventHandler(PUse_Click));
                      


                        if (FuncString.Right(productcolorFull, 2).ToLower() == "|b" || FuncString.Right(productcolorFull, 2).ToLower() == "|w")
                        {

                            productcolor = productcolorFull.Split('|');
                            productColorBG = productcolor[0].Split(',');
                            productColorTxt = productcolor[1];

                            if (productColorTxt.ToLower() == "b")
                            {
                                bCat.ForeColor = System.Drawing.Color.Black;
                            }
                            else
                            {
                                bCat.ForeColor = System.Drawing.Color.White;
                            }


                            bCat.BackColor = Color.FromArgb(Int32.Parse(productColorBG[0]), Int32.Parse(productColorBG[1]), Int32.Parse(productColorBG[2]));

                        }

                        bCat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
           
                       // bCat.Cursor = System.Windows.Forms.Cursors.Default;
                        bCat.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                        bCat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                        bCat.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                        bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                        bCat.Name = productID.ToString();
                        bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                        bCat.TabIndex = 2;
                        bCat.Text = productName + " (" + productPrice.ToString() + ")";
                        bCat.UseVisualStyleBackColor = false;

                        if (productFlagUse.ToLower() == "e")
                        { 
                            bCat.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                            bCat.BackColor = Color.Red;
                        }
                        else
                        {
                            bCat.Click += new System.EventHandler(this.bProduct_Click);
                        }

                      

                        bCat.ContextMenu = cm; 
                     

                        OrderProductPN.Controls.Add(bCat);
                        i++;
                    }

                    y++;

                  
                }
            }

            }catch(Exception ex )

            {
               // MessageBox.Show(ex.Message);

            }

        }
         
        private void genObjOrderProductRemark()
        {

            int productID;
            string productName;
            float productPrice;
            string productFlagUse;
            string productDesc;
         //   string productColour;
            string[] productcolor;
            string[] productColorBG;
            string productColorTxt;
            string productcolorFull;

            Button bCat;
            int sizeX = 124;
            int sizeY = 70;
            int yy = 4;

            int i = 0;

            List<Product> productByCat = gd.getProductByCat(this.catIDSectedRemark, Login.selecttablezoneID, this.memIDSelected); // Remark Cat


            //CatPage = 1;

            if (productByCat.Count % 20 == 0)
                this.RemPageMax = (productByCat.Count / 20);
            else
                this.RemPageMax = (productByCat.Count / 20) + 1;
            


            int indexstart = 0;
            int indexEnd = 0;


            if (this.RemPageMax == 1)
            {
                indexstart = 1;
                indexEnd = 20;
                buttonRemPre.Visible = false;
                buttonRemNext.Visible = false;
            }
            else
            {
                indexstart = (this.RemPage - 1) * 20 + 1;
                indexEnd = (this.RemPage - 1) * 20 + 20;

                buttonRemPre.Visible = true;
                buttonRemNext.Visible = true;
            }

            if (this.RemPage == 1)
            {
                buttonRemPre.Visible = false;
            }
            else if (this.RemPage == this.RemPageMax)
            {
                buttonRemNext.Visible = false;
            }

            if (this.RemPageMax == 1)
            {
                buttonRemPre.Visible = false;
                buttonRemNext.Visible = false;
            }


            int y = 1;



            foreach (Product t in productByCat)
            {
                productID = t.ProductID;
                productName = t.ProductName;

                productPrice = 0;
                if (tableZoneVAT == "ZERO")
                    productPrice = 0;
                else if (this.memCardID.Length > 2)
                    productPrice = t.ProductCost;
                else if (tableZonePriceID == 1)
                    productPrice = t.ProductPrice;
                else if (tableZonePriceID == 2)
                    productPrice = t.ProductPrice2;
                else if (tableZonePriceID == 3)
                    productPrice = t.ProductPrice3;
                else if (tableZonePriceID == 4)
                    productPrice = t.ProductPrice4;
                else if (tableZonePriceID == 5)
                    productPrice = t.ProductPrice5;


                productFlagUse = t.ProductFlagUse;
                productDesc = t.ProductDesc;
                productcolorFull = t.ProductUnit;

                if(productPrice > 0)
                     productName = productName + " (" + productPrice.ToString() + ")";
                 

                if (productFlagUse.ToLower() == "y"  )
                {

                    bCat = new Button();

                    if (y >= indexstart && y <= indexEnd)
                    {


                        if (FuncString.Right(productcolorFull, 2).ToLower() == "|b" || FuncString.Right(productcolorFull, 2).ToLower() == "|w")
                        {

                            productcolor = productcolorFull.Split('|');
                            productColorBG = productcolor[0].Split(',');
                            productColorTxt = productcolor[1];

                            if (productColorTxt.ToLower() == "b")
                            {
                                bCat.ForeColor = System.Drawing.Color.Black;
                            }
                            else
                            {
                                bCat.ForeColor = System.Drawing.Color.White;
                            }


                            bCat.BackColor = Color.FromArgb(Int32.Parse(productColorBG[0]), Int32.Parse(productColorBG[1]), Int32.Parse(productColorBG[2]));

                        }



                        bCat.Cursor = System.Windows.Forms.Cursors.Default;
                        bCat.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                        bCat.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));


                        bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));

                       

                        bCat.Name = productPrice.ToString();
                        bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                        bCat.TabIndex = productID;
                        bCat.Text = productName;
                        bCat.UseVisualStyleBackColor = false;
                        bCat.Click += new System.EventHandler(this.bProductRemark_Click);

                        OrderProductRemarkPN.Controls.Add(bCat);
                        i++;
                    }

                    y++;
                }
            }

        }


        private void bProductRemark_Click(object sender, EventArgs e)
        {
            try
            {
                Button bClick = (Button)sender;

                string remark = bClick.Text.Split('(')[0]  ;
                string productPrice = bClick.Name;
                int productID = bClick.TabIndex;

                this.strDisplayLine1 = remark + " (" + productPrice + ")";


                if (flagAppendRemark == 0)
                {

                    textBoxReason.Text += "+" + remark + "{" + productID.ToString() + "}";

                    float adjustPrice = float.Parse(textBoxAdjustPriceOrder.Text);
                    adjustPrice += float.Parse(productPrice);
                    textBoxAdjustPriceOrder.Text = adjustPrice.ToString();

                    if (remark.Contains('%') == true)
                        radioButtonOrderRemPercent.Checked = true;

                }
                else
                {
                     
                    int keyOrder = 100 + this.keyOrderNo;

                    int result = gd.instRemarkOrderByTable(this.tableID, this.productIDAct, Login.userID, "", "+" + remark + "{" + productID.ToString() + "}", float.Parse(productPrice), 1, 100 + this.keyOrderNo);


                    if (this.productActionType == 3)
                    {
                        if (this.catIDSectedRemark == 301 || this.catIDSectedRemark == 300 )
                        {
                            this.catIDSectedRemark = 309;
                            OrderProductRemarkPN.Controls.Clear();
                            genObjOrderProductRemark();
                        }
                    }

                    genOrder();


                    if (result <= 0)
                        MessageBox.Show("Error Add Remark order By Table");

                }

            }
            catch (Exception ex)
            {

            }

        }


        int productIDAct = 0;
        string productNameAct = "";
        float productPriceAct = 0;
        int productRemAct = 0;

        private void bProduct_Click(object sender, EventArgs e)
        {

            try
            {

                if (Login.userName.Contains("Kit-"))
                {
                    //  MessageBox.Show("Kitchen ไม่สามารถ Order ได้ ");
                    throw new Exception("-100");
                }

                Button bClick = (Button)sender;

                string productID = bClick.Name;
                string productSyntax = "";
                string productName = bClick.Text;


                this.strDisplayLine1 = bClick.Text;

                // Check Syntax

                foreach (Product p in productSyntaxLists)
                {
                    if (p.ProductID.ToString() == productID.ToString())
                    {
                        productSyntax = p.ProductDesc;
                        this.actionDiscountBySynTax(productSyntax);
                    }
                }




                this.productActionType = 0;

                int result = 0;
                float percentOntop = 0;
                float discountIntop = 0;


                foreach (Product p in productByCat)
                {
                    if (p.ProductID.ToString() == productID.ToString())
                    {
                        if (p.ProductUnit.ToLower().Contains("kg") && p.ProductDesc.ToLower().Contains("rem"))
                        {
                            this.productActionType = 1;
                            this.productRemAct = Int32.Parse(FuncString.TextBetween(p.ProductDesc, "[", "]"));
                        }
                        else if (p.ProductUnit.ToLower().Contains("kg"))
                        {
                            this.productActionType = 2;
                        }
                        else if (p.ProductDesc.ToLower().Contains("rem"))
                        {
                            result = this.fn_AddOrder(Int32.Parse(productID), this.keyOrderNo, "");
                            genOrder();
                            this.ActionforProduct();
                            this.productActionType = 3;
                            this.productRemAct = Int32.Parse(FuncString.TextBetween(p.ProductDesc, "[", "]"));
                        }
                         

                        productIDAct = p.ProductID;

                        productNameAct = p.ProductName;
                        productPriceAct = p.ProductPrice;


                    }
                }

                if (this.productActionType > 0)
                {
                    this.ActionforProduct();
                    throw new Exception("100");

                }


                if (productID == "6")
                {
                    discountIntop = this.totalSalesAmount * (float)0.03;
                    result = gd.instOrderByTable(this.tableID, Int32.Parse(productID), this.memID, "", discountIntop);
                }
                else if (productID == "7")
                {
                    discountIntop = (this.totalSalesAmount - 30000) * (float)0.03;
                    result = gd.instOrderByTable(this.tableID, Int32.Parse(productID), this.memID, "", discountIntop);
                }
                else if (productName.Contains("On-Top"))
                {
                    percentOntop = float.Parse(FuncString.TextBetween(productName, "[On-Top:", "%]"));
                    discountIntop = this.totalSalesAmount * percentOntop / (float)-100;
                    remarkLine1 = "+On-Top Dis. : " + this.totalSalesAmount.ToString();

                    result = gd.instOrderByTable(this.tableID, Int32.Parse(productID), this.memID, remarkLine1, discountIntop);
                }
                else
                {

                    if (this.imgPath.Length > 0)
                    {
                        imgProduct = getImageFromURL(this.imgPath + productID + ".jpg");
                        if(flagGetImg == 1)
                            ScreenService.SecondMonitor.pictureBox1.Image = imgProduct;
                        else
                            ScreenService.SecondMonitor.pictureBox1.Image = global::AppRest.Properties.Resources.Logo_New; 
                    }

                    result = this.fn_AddOrder(Int32.Parse(productID), this.keyOrderNo, "");
                }



                if (result < 0)
                {
                    throw new Exception(result.ToString());
                }
                else if (result > 1000)
                { // Pro BEFORE
                    // BEFORE

                    this.productIDBF = Int32.Parse(productID);
                    this.promIDBF = result - 1000;

                    this.promproductBF();

                }

                genOrder();
                defaultColBut();
                OrderProductRemarkPN.VerticalScroll.Value = OrderProductRemarkPN.VerticalScroll.Maximum;
                panelOrder.VerticalScroll.Value = panelOrder.VerticalScroll.Maximum;

            }
            catch (Exception ex)
            {
             //   



                if (FuncString.IsNumeric(ex.Message))
                {

                    int errorNo = Int32.Parse(ex.Message);

                    if (errorNo == -1)
                        MessageBox.Show("ไม่สามารถขายช่วงวันนี้ได้");
                    else if (errorNo == -2)
                        MessageBox.Show("ไม่สามารถขายช่วงเวลานี้ได้");
                    else if (errorNo == -3)
                        MessageBox.Show("Promotion ไม่สามารถใช้งานได้ : NONE");
                    else if (errorNo == -4)
                        MessageBox.Show("Promotion ไม่สามารถใช้งานได้ : AFTER");
                    else if (errorNo == -5)
                        MessageBox.Show("Promotion ขายครบตามจำนวนแล้ว");
                    else if (errorNo == -100)
                        MessageBox.Show(Login.userName + " ไม่สามารถ Order ได้ ");
                }
                else
                {

                    MessageBox.Show(ex.Message);
                }



            }
        }

        private void ActionforProduct()
        {
            try
            {
                if (this.productActionType == 1 || this.productActionType == 2 || this.productActionType == 4) // Pro
                {
                    keyTime = 1;
                    panelShowActWeight.Visible = true;
                    txtBoxActProductName.Text = productNameAct;
                    txtBoxActProductPrice.Text = productPriceAct.ToString();

                    txtBoxActProductWeight.Focus();

                }
                else if (this.productActionType == 3)
                {
                    flagAppendRemark = 1;

                    panelBill.Visible = true;
                    AddRemarkOrderPN.Visible = true;
                    buttonViewBill.Text = "Hide เหตุผล";
                    textBoxReason.Focus();
                    textBoxReason.Text = "";
                    textBoxAdjustPriceOrder.Text = "0";

                    this.catIDSectedRemark = this.productRemAct;

                    OrderProductRemarkPN.Controls.Clear();
                    genObjOrderProductRemark();

                }
            }
            catch (Exception ex)
            {


            }

        }




        private void buttonClearAllOrder_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("คุณต้องการจะลบออร์เดอร์ทั้งหมดหรือไม่ ? " , "ลบทั้งหมด", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int result = gd.delAllOrderByTable(this.tableID);

                ScreenService.SendDataToSecondMonitor(null);

                if (result <= 0)
                    MessageBox.Show("Error Delete All order By Table");

                txtOrderDetail.Text = "No Order";
                txtOrderUnit.Text = "Unit";
                txtOrderAmt.Text = "Amt";
                txtSalesAmount.Text = "0";

                AddRemarkOrderPN.Controls.Clear();
                DelOrderPN.Controls.Clear();

                LinkFormMainTable();
            }
  
        }

        private void buttonMoveTable_Click(object sender, EventArgs e)
        {
            string moveTableName = comboBoxListAllTable.Text;

            int moveTableID = Int32.Parse( comboBoxListAllTable.SelectedValue.ToString() );
             


            if (moveTableID != 0)
            {

                if (MessageBox.Show("คุณต้องการจะเปลี่ยนไปโต๊ะ #" + moveTableName + " หรือไม่ ? ", "เปลี่ยนโต๊ะ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.UpsMoveTable(this.tableID, moveTableID);

                    if (result <= 0)
                        MessageBox.Show("Error Move Table : ไม่มีออร์เดอร์ย้ายโต๊ะ !!");

                    this.tableID = moveTableID;


                    tableStatus = gd.getOrderTableStatus(this.tableID);

                    checkBoxServiceCharge.Checked = false; 
                    if (tableStatus.ZoneServiceCharge.ToLower() == "y")
                        checkBoxServiceCharge.Checked = true; 

                     
                    genOrder();
                }
            }
            else
            { 
                MessageBox.Show("ย้ายโต๊ะ : กรุณาเลือกโต๊ะ !!");
            }


        }

        private void txtBoxDiscount_Change(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;

                if (FuncString.IsNumeric(txt.Text) && txt.Text.Length > 0)
                {
                    genOrder();
                }
                else
                {
                    MessageBox.Show("กรุณาป้อนส่วนดเป็นตัวเลข");
                    txt.Text = "0";
                }
            }
            catch (Exception ex)
            {

            }
        }

        

        private void CheckBoxTaxAndService_Change(object sender, EventArgs e)
        {
            genOrder();
        }

     

        
        private void buttonLinkToMainTable_Click(object sender, EventArgs e)
        {

            try
            {
                int custID = 0;
                int discountype = 0; // 0 = เฉพาะอาหาร , 1 = ทั้งหมด
                int discountPer = 0;
                int discountAmt = 0;
                int tax = 0;
                int servicecharge = 0;
                string reason = this.strRemark;
                // 0 = No Print , 1 = Print
                 
                custID = Int32.Parse(comboBoxListCust.SelectedValue.ToString());

                discountype = 1;

                discountPer = 0;

                discountAmt = Int32.Parse(textBoxDiscountAmt.Text);

                if (checkBoxTax.Checked == true)
                    tax = 1;

                if (checkBoxServiceCharge.Checked == true)
                    servicecharge = 1;



                reason = @"FOR:Disc=:" +textBoxGroupDis1.Text+ ":" + textBoxGroupDis2.Text + 
                                     ":"+ textBoxGroupDis3.Text +":" +textBoxGroupDis4.Text + 
                                     ":"+ textBoxDiscountAmt.Text+ ":0";

                string strMemSearch = textBoxStrSearchMemCard.Text.Trim();

                int result = gd.instPrintBillFlag(this.tableID, custID, discountype, discountPer, discountAmt, tax, servicecharge, this.printBillFlag, reason, strMemSearch);


                FuncString.displayPOSMonitorSecLine("      Thank you     ", ConfigurationSettings.AppSettings["RestName"].Replace("+", "").Trim());

  


                LinkFormMainTable();
            }
            catch (Exception ex)
            {
                LinkFormMainTable();
            }
             
        }

        private void LinkFormMainTable()
        {

            ScreenService.SendDataToSecondMonitor(null);
            ScreenService.SecondMonitor.pictureBox1.Image = global::AppRest.Properties.Resources.Logo_New;

            if (Login.orderType == 2)
            {
                this.Close();
            }
            else
            { 
                Cursor.Current = Cursors.WaitCursor;
                if (formMainTable == null)
                {
                    formMainTable = new MainTable(this, 1);
                }
                Cursor.Current = Cursors.Default;
                if (formMainTable.ShowDialog() == DialogResult.OK)
                {
                    formMainTable.Dispose();
                    formMainTable = null;
                }
            }
        }

      


       

        private void buttonPrintBill_Click(object sender, EventArgs e)
        {
            try
            {
                int custID = 0;
                int discountype = 0; // 0 = เฉพาะอาหาร , 1 = ทั้งหมด
                int discountPer = 0;
                int discountAmt = 0;
                int tax = 0;
                int servicecharge = 0;
                
                this.printBillFlag = 1; // 0 = No Print , 1 = Print

                string reason = "";
                 

                custID = Int32.Parse(comboBoxListCust.SelectedValue.ToString());

                discountype = 1;

                discountPer = 0;

                discountAmt = Int32.Parse(textBoxDiscountAmt.Text);

                if (checkBoxTax.Checked == true)
                    tax = 1;

                if (checkBoxServiceCharge.Checked == true)
                    servicecharge = 1;

                reason = ""; //textBoxReason.Text;
                string strMemSearch = textBoxStrSearchMemCard.Text.Trim();

                int result = gd.instPrintBillFlag(this.tableID, custID, discountype, discountPer, discountAmt, tax, servicecharge, this.printBillFlag, reason, strMemSearch);

                if (result <= 0)
                {
                    MessageBox.Show("Error Insert Print Bill Flag");
                }


                string langBill = this.defaultlangBill;

                if (this.defaultlangBill == "NO")
                    langBill = this.flagLang;

                this.tableCheckBill = gd.getMainOrderByTable(this.tableID, langBill, 0, -1, this.memIDSelected, this.subOrderID);  

                flagLang_Change(null, null);

                if (posPrinters[0].FlagPrint.ToLower() == "y")
                    printCash.Print();

                CheckPrintBill();




            }
            catch (Exception ex)
            {

            }
        }

     
        

        // OnPrintPage
        private void OnPrintPageCash(object sender, PrintPageEventArgs e)
        {
            try
            {

                int x = 0;
                int y = 10;

                // Information

                string restName = "";
                string restAddr1 = ""; // 160/8 ซ.ทองหล่อ ถ.สุขุมวิท 55
                string restAddr2 = ""; // แขวงคลองตันเหนือ แขตวัฒนา กรุงเทพฯ 10110 
                string restTel = ""; // โทร. 02-714-9402
                string restTaxID = "";
                string restLine1 = "";
                string restLine2 = "";
                string restTaxRD = "";
                string appTaxRD = "";

                branch = gd.getBranchDesc();

                /* 
                 
                 * Default Bill Config @ Program
                 * 
                 * EN , TH , NO
                 * 
                 */

                string langBill = this.defaultlangBill;

                if (this.defaultlangBill == "NO")
                    langBill = this.flagLang;


                if (langBill == "TH")
                {
                    restName = branch.RestNameTH;
                    restAddr1 = branch.RestAddr1TH;
                    restAddr2 = branch.RestAddr2TH;
                    restTel = "โทร. : " + branch.RestTel;
                }
                else
                {
                    restName = branch.RestNameEN;
                    restAddr1 = branch.RestAddr1EN;
                    restAddr2 = branch.RestAddr2EN;
                    restTel = "Tel. : " + branch.RestTel;
                }



                restLine1 = branch.RestLine1;
                restLine2 = branch.RestLine2;
                restTaxID = branch.RestTaxID;
                restTaxRD = branch.RestTaxRD;


                if (restTaxRD.Contains(":"))
                {
                    string [] ArrgrestTaxRD = restTaxRD.Split(':');

                    int ii = 0 ;
                    foreach (string s in ArrgrestTaxRD)
                    {
                        if (s == Login.posBranchID.ToString())
                        {
                            appTaxRD = ArrgrestTaxRD[ii+1];
                        }

                         ii++;
                    }
                }


                ///////////////////////////


                Brush brush = new SolidBrush(Color.Black);
                //Font fontHeader = new Font("Tahoma", 14);
                //Font fontTable = new Font("Tahoma", 13);
                //Font fontSubHeader = new Font("Tahoma", 9);
                //Font fontBody = new Font("Tahoma", 9);
                //Font fontBodylist = new Font("Tahoma", 8);
                //Font fontNum = new Font("Consolas", 8);


                Font fontHeaderQ = new Font("Arail", 18, FontStyle.Bold);
                Font fontHeader = new Font("Arail", 12, FontStyle.Bold);
                Font fontTable = new Font("Arail", 11, FontStyle.Bold);
                Font fontSubHeader = new Font("Arail", 9, FontStyle.Bold);
                Font fontFooter = new Font("Arail", 7, FontStyle.Regular);
                Font fontBody = new Font("Arail", 8, FontStyle.Regular);
                Font fontBodylist = new Font("Arail", 9, FontStyle.Regular);
                Font fontNum = new Font("Consolas", 9, FontStyle.Regular);

                //  Bitmap img = global::AppRest.Properties.Resources.Logo_New;

                if (this.billID > 0)
                {
                    e.Graphics.DrawString("Q" + FuncString.Right(this.billID.ToString(), 3), fontHeaderQ, brush, x + 100, y);
                    y += 30;
                }


                if (this.copyPrint == 99)
                {
                    e.Graphics.DrawString("ใบสรุป Order", fontBody, brush, x + 120, y);

                    y += 12;

                }
                else
                {

                    if (this.flagCheckBill == "Y")
                        if (this.copyPrint > 0)
                            e.Graphics.DrawString("copy", fontBody, brush, x + 150, y);
                        else
                            e.Graphics.DrawString("ใบเสร็จรับเงิน", fontBody, brush, x + 120, y);
                    else
                        e.Graphics.DrawString("ใบรองบิล", fontBody, brush, x, y);


                    y += 12;

                    if (Login.flagLogoSQ.ToLower() == "y")
                    {
                        e.Graphics.DrawImage(global::AppRest.Properties.Resources.Logo_New, x + 85, y, 110, 110);
                        y += 110;
                    }
                    else
                    {
                        e.Graphics.DrawImage(global::AppRest.Properties.Resources.Logo_New, x + 85, y, 110, 55);
                        y += 55;
                    }

                    

                    e.Graphics.DrawString(restName, fontBody, brush, x + 10, y);

                    y += 12;



                    e.Graphics.DrawString(restAddr1, fontBody, brush, x + 10, y);

                    y += 12;


                    if (restAddr2.Trim().Length > 0)
                    {
                        e.Graphics.DrawString(restAddr2, fontBody, brush, x + 10, y);

                        y += 12;

                    }


                    if (restLine1.Trim().Length > 0)
                    {
                        e.Graphics.DrawString(restLine1, fontBody, brush, x + 50, y);

                        y += 12;
                    }


                    if (restTaxID.Trim().Length > 0)
                    {
                        e.Graphics.DrawString(restTaxID, fontBody, brush, x + 50, y);

                        y += 12;
                    }


                    if (appTaxRD.Trim().Length > 0)
                    {
                        e.Graphics.DrawString("TAX RD : " + appTaxRD, fontBody, brush, x + 50, y);

                        y += 12;
                    }

                    if (appTaxRD.Trim().Length > 0)
                    {
                        e.Graphics.DrawString("VAT INCLUDED", fontBody, brush, x + 85, y);

                        y += 12;
                    }


                    if (restTel.Trim().Length > 0)
                    {
                        e.Graphics.DrawString(restTel, fontBody, brush, x + 65, y);

                        y += 12;

                    } 

                    e.Graphics.DrawString(Login.userID.ToString() + " " + Login.userName, fontBodylist, brush, x + 5, y);

                }

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd MMM yy}", dt);
                string strTime = String.Format("{0:HH:mm}", dt);
                string tableName = this.txtTableName.Text;

                if (this.subOrderID != -1)
                    tableName += " " + comboBoxORDER.Text;

                e.Graphics.DrawString(" " + strDate + " " + strTime, fontSubHeader, brush, x + 150, y);

                y += 10;

                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 10;
                e.Graphics.DrawString(" " + tableName, fontHeader, brush, x + 10, y);
                e.Graphics.DrawString("Gst " + this.custNumber.ToString(), fontHeader, brush, x + 200, y); 
                y += 20;
                           
                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 15;

                //// Print Order

                Table table = tableCheckBill; //gd.getMainOrderByTable(this.tableID, langBill, 0, 0);  

                 
                this.orderAmount = table.OrderAmount;

                List<Order> orders = table.Order;

                int i = 1;
                 

                string str1 = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";
                string strNum = "";

                int firstimeordercount = 0;
                string firstimeorder = "";

                string[] txtProduct;
                string txtProductName = "";
                string txtProductRemark = "";

                List<string> txtPrint;
                int len = 28;

 

                foreach (Order o in orders)
                {
             


                    if (o.Flagsend > 0) 
                    {
                        txtProduct = o.ProductName.Split('|');
                        txtProductName = txtProduct[0];
                        txtProductRemark = txtProduct[1];

                        if (firstimeordercount == 0)
                        {
                            firstimeorder = o.CreateDate;
                            firstimeordercount++;
                        }

                        if (o.ProductName.Substring(0, 1) == "-")
                        {
                            strNum = "  ";
                        }
                        else
                        {
                            strNum = i.ToString() + ". ";
                            i++;
                        }

                        str1 = txtProductName.Trim();
                        str3 = o.ProductPrice.ToString();

                        if (o.OrderQTY > 1)
                            str1 += "(" + str3 + ")";

                        str2 = o.OrderQTY.ToString();
                        str4 = o.OrderAmount.ToString("###,###.##");

                        str4 = String.Format("{0,10}", str4);

                        if (!( ( o.ProductID > 10 && o.ProductID <= 100 ) ) && o.OrderNo != 99 && o.ProductID != 4)
                        {

                            if (o.ProductName.Substring(0, 1) == "-" && o.OrderQTY == 1)
                                 e.Graphics.DrawString("", fontBodylist, brush, x + 0, y);
                            else
                                 e.Graphics.DrawString(str2, fontBodylist, brush, x + 0, y);
                             

                            if( copyPrint != 99 )
                              e.Graphics.DrawString(str4, fontNum, brush, x + 180, y);

                            txtPrint = FuncString.WordWrap(str1, len);
                            str1 = "";

                            foreach (string op in txtPrint)
                            {
                                e.Graphics.DrawString(op, fontBodylist, brush, x + 22, y);
                                y += 19;
                            }

                        }


                        if (txtProductRemark.Trim().Length > 1 && o.OrderNo != 99)
                        {
                            string[] remarkString = txtProductRemark.Remove(0, 1).Split('+');

                            foreach (string r in remarkString)
                            {

                                str1 = "  +" + r.Split('{')[0] + "\r\n";

                                e.Graphics.DrawString(str1, fontBodylist, brush, x + 22, y);
                                y += 15;
                            }
                        }

                    }
                    
                }


                //////////////////////////////////////////////////////////////////////////

                string txtOrder = "";
                string txtAmt = "";



                //if (this.salesOrderGroup1 > 0)
                //{
                //    y += 15;
                //    txtOrder = "" + this.discountGroup[0].DiscountGroupName + "";
                //    txtAmt = this.salesOrderGroup1.ToString("###,###.#0");
                //    txtAmt = String.Format("{0,10}", txtAmt);
                //    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                //    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);
                //}



                //if (this.salesOrderGroup2 > 0)
                //{
                //    y += 15;
                //    txtOrder = "" + this.discountGroup[1].DiscountGroupName + "";
                //    txtAmt = this.salesOrderGroup2.ToString("###,###.#0");
                //    txtAmt = String.Format("{0,10}", txtAmt);
                //    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                //    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);
                //}



                //if (this.salesOrderGroup3 > 0)
                //{
                //    y += 15;
                //    txtOrder = "" + this.discountGroup[2].DiscountGroupName + "";
                //    txtAmt = this.salesOrderGroup3.ToString("###,###.#0");
                //    txtAmt = String.Format("{0,10}", txtAmt);
                //    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                //    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);
                //}

                //if (this.salesOrderGroup4 > 0)
                //{
                //    y += 15;
                //    txtOrder = "" + this.discountGroup[3].DiscountGroupName + "";
                //    txtAmt = this.salesOrderGroup4.ToString("###,###.#0");
                //    txtAmt = String.Format("{0,10}", txtAmt);
                //    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                //    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);
                //}

                //if (this.orderPromotion > 0)
                //{
                //    y += 15;
                //    txtOrder = "ORDER PROMOTIOM ";
                //    txtAmt = this.orderPromotion.ToString("###,###.#0");
                //    txtAmt = String.Format("{0,10}", txtAmt);
                //    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                //    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);
                //}


                y += 15;
                txtOrder = "Sub Total";
                txtAmt = this.salesSubTotal.ToString("###,###.#0");
                txtAmt = String.Format("{0,10}", txtAmt);
                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);


                if (copyPrint != 99)
                {

                    if (this.totalDiscount > 0)
                    {


                        if (discount1 > 0)
                        {
                            y += 15;
                            txtOrder = "" + this.discountGroup[0].DiscountGroupNameEN + " " + (discount1 * 100).ToString();
                            txtAmt = (this.salesOrderGroup1 * discount1).ToString("###,###.#0");
                            txtAmt = String.Format("{0,10}", txtAmt);
                            e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                            e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                        }

                        if (discount2 > 0)
                        {
                            y += 15;
                            txtOrder = "" + this.discountGroup[1].DiscountGroupNameEN + " " + (discount2 * 100).ToString();
                            txtAmt = (this.salesOrderGroup2 * discount2).ToString("###,###.#0");
                            txtAmt = String.Format("{0,10}", txtAmt);
                            e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                            e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                        }

                        if (discount3 > 0)
                        {
                            y += 15;
                            txtOrder = "" + this.discountGroup[2].DiscountGroupNameEN + " " + (discount3 * 100).ToString();
                            txtAmt = (this.salesOrderGroup3 * discount3).ToString("###,###.#0");
                            txtAmt = String.Format("{0,10}", txtAmt);
                            e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                            e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                        }

                        if (discount4 > 0)
                        {
                            y += 15;
                            txtOrder = "" + this.discountGroup[3].DiscountGroupNameEN + " " + (discount4 * 100).ToString();
                            txtAmt = (this.salesOrderGroup4 * discount4).ToString("###,###.#0");
                            txtAmt = String.Format("{0,10}", txtAmt);
                            e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                            e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                        }

                        if (discountAmt > 0)
                        {
                            y += 15;
                            txtOrder = " Discount Amount (B) ";
                            txtAmt = (discountAmt).ToString("###,###.#0");
                            txtAmt = String.Format("{0,10}", txtAmt);
                            e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                            e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                        }

                        if (totalDiscount > 0)
                        {
                            y += 15;
                            txtOrder = " Total Discount ";
                            txtAmt = (totalDiscount).ToString("###,###.#0");
                            txtAmt = String.Format("{0,10}", txtAmt);
                            e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                            e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                        }

                    }

                    if (this.totalServiceCharge > 0)
                    {
                        y += 15;
                        txtOrder = "Service " + ((float)(this.servicePercent * 100)).ToString() + "%";
                        txtAmt = this.totalServiceCharge.ToString("###,###.#0");
                        txtAmt = String.Format("{0,10}", txtAmt);
                        e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                        e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    }


                    if (this.totalVAT > 0)
                    {

                        y += 15;
                        txtOrder = "Amt Before VAT " + ((float)(this.taxPercent * 100)).ToString() + "%";
                        txtAmt = (this.orderAmount - this.totalDiscount + this.totalServiceCharge).ToString("###,###.#0");
                        txtAmt = String.Format("{0,10}", txtAmt);
                        e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                        e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                        //y += 15;
                        //txtOrder = "Amt Non VAT ";
                        //txtAmt = this.salesNonVAT.ToString("###,###.#0");
                        //txtAmt = String.Format("{0,10}", txtAmt);
                        //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                        //e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                        y += 15;
                        txtOrder = "VAT " + ((float)(this.taxPercent * 100)).ToString() + "%";
                        txtAmt = this.totalVAT.ToString("###,###.#0");
                        txtAmt = String.Format("{0,10}", txtAmt);
                        e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                        e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                        y += 15;
                        txtOrder = "Rounding ";
                        txtAmt = this.diffAmount.ToString("###,###.#0");
                        txtAmt = String.Format("{0,10}", txtAmt);
                        e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                        e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);


                    }




                    y += 25;
                    txtOrder = "Total ";
                    txtAmt = this.totalSalesAmount.ToString("###,###.#0");
                    e.Graphics.DrawString(txtOrder, fontTable, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontTable, brush, x + 190, y);


                    y += 20;


                    string remark = "";
                    string custName = "";



                    custName = comboBoxListCust.Text;
                    remark = "";//textBoxReason.Text;


                    //textBoxCustShare

                    e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);


                    // Share BILL

                    //if (this.flagCheckBill == "N")
                    //{

                    //    y += 15;
                    //    e.Graphics.DrawString(this.remarkLine1 + "\n" + this.remarkLine2 + "\n" + this.remarkLine3 + "\n" + "          ( Cordially Made Co. Ltd) ", fontTable, brush, x + 5, y);

                    //    if (this.remarkLine1.Length > 0)
                    //        y += 15;
                    //    if (this.remarkLine2.Length > 0)
                    //        y += 15;
                    //    if (this.remarkLine3.Length > 0)
                    //        y += 15;
                    //}


                    // Share BILL

                    if (this.flagCheckBill == "N")
                    {
                        if (textBoxCustShare.Text != "0" && textBoxCustShare.Text.Length > 0)
                        {
                            if(  FuncString.IsNumeric(textBoxCustShare.Text) )
                            {
                                y += 25;

                                txtOrder = "> No of Cust. :";
                                txtAmt = textBoxCustShare.Text;
                                txtAmt = String.Format("{0,10}", txtAmt);
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                                y += 15;

                                txtOrder =  "> Payment per Cust :";
                                txtAmt = (this.totalSalesAmount / float.Parse(textBoxCustShare.Text)).ToString("###,###.#0");
                                txtAmt = String.Format("{0,10}", txtAmt);
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);
                            }
                        }
                    }

                    if (checkBoxQR.Checked)
                    {

                        string promtpayCode = ConfigurationSettings.AppSettings["PromtpayCode"].ToString();
                        y += 10;
                        e.Graphics.DrawString("PROMPAY : " + promtpayCode, fontSubHeader, brush, x + 50, y);

                        y += 15;

                        e.Graphics.DrawString("ยอด : " + this.totalSalesAmount.ToString("###,###.#0"), fontTable, brush, x + 50, y);

                        y += 25;

                        string amount = this.totalSalesAmount.ToString();
                        string url = "https://promptpay.io/" + promtpayCode + "/" + this.totalSalesAmount + ".png";

                        //  MessageBox.Show(url);
                        Image qr = getImageFromURL(url);

                        //  Image qr = global::AppRest.Properties.Resources.QR;

                        e.Graphics.DrawImage(qr, x + 60, y, 150, 150);
                        y += 145;
                    }




                    y += 15;


                    y += 15;

                    if (langBill == "TH")
                    {

                        e.Graphics.DrawString(" การชำระเงิน " + "บิลเลขที่ : #" + this.billID.ToString(), fontBody, brush, x + 10, y);
                        y += 20;
                        i = 1;
                        foreach (BillPayment b in billPayment)
                        {
                            if (b.PaytypeID == 1)
                            {
                                y += 15;
                                txtOrder = i.ToString() + ". ชำระเงินสด ยอด  :";
                                txtAmt = b.PayAmount.ToString("###,###.#0");
                                txtAmt = String.Format("{0,10}", txtAmt);
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                                y += 15;
                                txtOrder = "  - รับเงิน  : ";
                                txtAmt = b.PayDesc1;
                                txtAmt = String.Format("{0,10}", txtAmt);
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                                y += 15;
                                txtOrder = "  - เงินทอน  : ";
                                txtAmt = b.PayDesc2;
                                txtAmt = String.Format("{0,10}", txtAmt);
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);


                            }
                            else if (b.PaytypeID == 2)
                            {
                                y += 15;
                                txtOrder = i.ToString() + ". ชำระบัตรเครดิต  :";
                                txtAmt = b.PayAmount.ToString("###,###.#0");
                                txtAmt = String.Format("{0,10}", txtAmt);
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                                y += 15;
                                txtOrder = "  - ประเภท : " + b.PayDesc1;
                                txtAmt = b.PayDesc1;
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);

                                y += 15;
                                txtOrder = "  - เลขที่   : " + b.PayDesc2;
                                txtAmt = b.PayDesc2;
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);

                                y += 15;
                                txtOrder = "  - ชื่อลูกค้า : " + b.PayDesc3;
                                txtAmt = b.PayDesc3;
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);

                            }
                            else if (b.PaytypeID == 3)
                            {

                                y += 15;
                                txtOrder = i.ToString() + ". ลงบิลลูกค้า  :";
                                txtAmt = b.PayAmount.ToString("###,###.#0");
                                txtAmt = String.Format("{0,10}", txtAmt);
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                                y += 15;
                                txtOrder = "  - ชื่อ :" + b.PayDesc1;
                                txtAmt = b.PayDesc1;
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                            }
                            else if (b.PaytypeID == 4)
                            {
                                e.Graphics.DrawString(i.ToString() + ". บัตรเงินสด ยอด  :" + b.PayAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                                e.Graphics.DrawString("  - รหัสบัตร    : " + b.PayDesc1, fontBody, brush, x + 10, y); y += 15;
                                e.Graphics.DrawString("  - ยอดเริ่มต้น   : " + b.PayDesc2, fontBody, brush, x + 10, y); y += 15;
                                e.Graphics.DrawString("  - ยอดคงเหลือ  : " + b.PayDesc3, fontBody, brush, x + 10, y); y += 15;
                            }
                            else if (b.PaytypeID == 5)
                            {

                                y += 15;
                                txtOrder = i.ToString() + ". QR/โอนเงิน  :";
                                txtAmt = b.PayAmount.ToString("###,###.#0");
                                txtAmt = String.Format("{0,10}", txtAmt);
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                                y += 15;
                                txtOrder = "  - ประเภท : " + b.PayDesc1;
                                txtAmt = b.PayDesc1;
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);

                                y += 15;
                                txtOrder = "  - รายละเอียด : " + b.PayDesc2;
                                txtAmt = b.PayDesc2;
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);

                                y += 15;
                                txtOrder = "  - ชื่อลูกค้า : " + b.PayDesc3;
                                txtAmt = b.PayDesc3;
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                            } 
                            i++; 
                        }
                    }
                    else
                    {

                        e.Graphics.DrawString(" Payment " + "Bill No. : " + this.billID.ToString(), fontBody, brush, x + 10, y);
                        y += 20;
                        i = 1;
                        foreach (BillPayment b in billPayment)
                        {
                            if (b.PaytypeID == 1)
                            {
                                y += 15;
                                txtOrder = i.ToString() + ". Cash  :";
                                txtAmt = b.PayAmount.ToString("###,###.#0");
                                txtAmt = String.Format("{0,10}", txtAmt);
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                                y += 15;
                                txtOrder = "  - RECEIVE  : ";
                                txtAmt = b.PayDesc1;
                                txtAmt = String.Format("{0,10}", txtAmt);
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                                y += 15;
                                txtOrder = "  - CHANGE  : ";
                                txtAmt = b.PayDesc2;
                                txtAmt = String.Format("{0,10}", txtAmt);
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);


                            }
                            else if (b.PaytypeID == 2)
                            {
                                y += 15;
                                txtOrder = i.ToString() + ". Credit Card  :";
                                txtAmt = b.PayAmount.ToString("###,###.#0");
                                txtAmt = String.Format("{0,10}", txtAmt);
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                                y += 15;
                                txtOrder = "  - TYPE : " + b.PayDesc1;
                                txtAmt = b.PayDesc1;
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);

                                y += 15;
                                txtOrder = "  - NO.   : " + b.PayDesc2;
                                txtAmt = b.PayDesc2;
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);

                                if (FuncString.IsNumeric(b.PayDesc3) && b.PayDesc3 != "0")
                                { 
                                    y += 15;
                                    txtOrder = "  - THANK YOU TIP : " ;
                                    txtAmt = (float.Parse(b.PayDesc3) - b.PayAmount ).ToString("###,###.#0");
                                    txtAmt = String.Format("{0,10}", txtAmt);
                                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);
                                }

                            }
                            else if (b.PaytypeID == 3)
                            {

                                y += 15;
                                txtOrder = i.ToString() + ". Credit Customer :";
                                txtAmt = b.PayAmount.ToString("###,###.#0");
                                txtAmt = String.Format("{0,10}", txtAmt);
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                                y += 15;
                                txtOrder = "  - Name :" + b.PayDesc1;
                                txtAmt = b.PayDesc1;
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                            }
                            else if (b.PaytypeID == 4)
                            {
                                e.Graphics.DrawString(i.ToString() + ". Cash Card / OC.  :" + b.PayAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                                e.Graphics.DrawString("  - รหัสบัตร    : " + b.PayDesc1, fontBody, brush, x + 10, y); y += 15;
                                e.Graphics.DrawString("  - ยอดเริ่มต้น   : " + b.PayDesc2, fontBody, brush, x + 10, y); y += 15;
                                e.Graphics.DrawString("  - ยอดคงเหลือ  : " + b.PayDesc3, fontBody, brush, x + 10, y); y += 15;
                            }
                            else if (b.PaytypeID == 5)
                            {

                                y += 15;
                                txtOrder = i.ToString() + ". OR Banking  :" + b.PayDesc1 ;
                                txtAmt = b.PayAmount.ToString("###,###.#0");
                                txtAmt = String.Format("{0,10}", txtAmt);
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                                y += 15;
                                txtOrder = "  - TYPE : " + b.PayDesc1;
                                txtAmt = b.PayDesc1;
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);

                                y += 15;
                                txtOrder = "  - DESC : " + b.PayDesc2;
                                txtAmt = b.PayDesc2;
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);

                                y += 15;
                                txtOrder = "  - NAME : " + b.PayDesc3;
                                txtAmt = b.PayDesc3;
                                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                            }
                            i++;
                        }

                    }


                    y += 15;

                }
             
                 // Update เรื่องคะแนนของ MemmCard



                if (this.memCardID != "0")
                {

                    mc = gd.SelMemCard_Search(this.memCardID, this.statusRestAllUse);

                    int pointthisBill = mc.Point - Int32.Parse(textBoxMemCardPoint.Text.ToString());

                    y += 15;
                    e.Graphics.DrawString("รหัสบัตรสมาชิก : " + this.memCardID, fontBody, brush, x + 20, y);
                    y += 15;
                    e.Graphics.DrawString("ลูกค้า : " + textBoxMemCardName.Text, fontBody, brush, x + 20, y);
                    y += 15;
                    e.Graphics.DrawString("คะแนนสะสม: " + textBoxMemCardPoint.Text, fontBody, brush, x + 20, y);
                    y += 15;

                     

                    if (this.billID > 0)
                    {
                        e.Graphics.DrawString("บิลนี้สะสมคะแนน: " + (pointthisBill - this.orderCutPoint).ToString(), fontBody, brush, x + 20, y);
                        y += 15;
                        e.Graphics.DrawString("ใช้คะแนนสะสมภายในบิล : " + this.orderCutPoint.ToString(), fontBody, brush, x + 20, y);
                        y += 15;
                        e.Graphics.DrawString("คะแนนสะสมคงเหลือปัจจุบัน : " + mc.Point.ToString(), fontBody, brush, x + 20, y);
                        y += 15;
                    }


                }
                  

                if (this.flagCouponCanUse == 1)
                {
                    y += 15;
                    e.Graphics.DrawString("ใช้ CouponCode : " + textBoxCouponCode.Text, fontBody, brush, x + 5, y);
                    y += 15;
                    e.Graphics.DrawString( textBoxCouponDesc.Text , fontBody, brush, x + 5, y);
                    y += 15;
                }

                y += 15;
                e.Graphics.DrawString(this.remarkLine1 + "\n" + this.remarkLine2 + "\n" + this.remarkLine3, fontBody, brush, x + 5, y);
              
                if (this.remarkLine1.Length > 0)
                    y += 15;
                if (this.remarkLine2.Length > 0)
                    y += 15;
                if (this.remarkLine3.Length > 0)
                    y += 15;


               // Bitmap imgLogoFB = global::AppRest.Properties.Resources.Logo_FB;
                //Bitmap imgLogoIG = global::AppRest.Properties.Resources.Logo_IG;
                //Bitmap imgLogoLINE = global::AppRest.Properties.Resources.Logo_LINE;

                //y += 15;

                e.Graphics.DrawString(" THANK YOU / ขอบคุณค่ะ ", fontBody, brush, x + 70, y);
                y += 20;
                e.Graphics.DrawImage(global::AppRest.Properties.Resources.Logo_FB, x + 75, y - 5, 18, 18);
                e.Graphics.DrawString(this.fblink, fontBody, brush, x + 98, y);
                //y += 20;
                //e.Graphics.DrawImage(imgLogoIG, x + 75, y - 5, 18, 18);
                //e.Graphics.DrawString(this.iglink, fontBody, brush, x + 98, y);

                y += 20;
                e.Graphics.DrawImage(global::AppRest.Properties.Resources.Logo_LINE, x + 75, y - 5, 18, 18);
                e.Graphics.DrawString("@" + this.linelink, fontBody, brush, x + 98, y);


                //e.Graphics.DrawString("LINE/IG/FB : @easybuddybkk", fontFooter, brush, x + 65, y); y += 30;
                //e.Graphics.DrawString("Sathorn Soi 8/ Central Embassy 6th Fl./", fontFooter, brush, x + 40, y); y += 15;
                //e.Graphics.DrawString("Central World 7th Fl./ theCOMMONS Thonglor 2nd Fl./ ", fontFooter, brush, x + 15, y); y += 15;
                //e.Graphics.DrawString("Phayathai (Delivery Only)/ Siam Center 2nd Fl.", fontFooter, brush, x + 30, y); y += 30;
                //e.Graphics.DrawString("ตรวจสอบแล้ว รายละเอียดถูกต้อง", fontFooter, brush, x + 65, y); y += 50;
                //e.Graphics.DrawString("ถูกต้อง : ..............................................", fontFooter, brush, x + 40, y); y += 15;
                //e.Graphics.DrawString("#kapraoislife", fontFooter, brush, x + 70, y); y += 80;


                y += 15;

                if (qrlink.Length > 0)
                {
                    Bitmap img3 = global::AppRest.Properties.Resources.QR;
                    e.Graphics.DrawImage(img3, x + 63, y, 150, 150);
                    y += 145;
                }

                //e.Graphics.DrawString(this.restlink, fontBody, brush, x + 70, y);

                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                
            }
        }

        private void buttonViewBill_Click(object sender, EventArgs e)
        {
            if (panelBill.Visible == false)
            {
                panelBill.Visible = true;
                AddRemarkOrderPN.Visible = true;
                buttonViewBill.Text = "Hide เหตุผล";
                textBoxReason.Focus();
                textBoxReason.Text = "";
                textBoxAdjustPriceOrder.Text = "0";
               
            }
            else
            {
                panelBill.Visible = false;
                AddRemarkOrderPN.Visible = false;
                buttonViewBill.Text = "ระบุเหตุผล";
                textBoxReason.Text = "";
                textBoxAdjustPriceOrder.Text = "0";
            }
            this.flagAppendRemark = 0;
        }

        private void flagLang_Change(object sender, EventArgs e)
        {

            if (radioBoxBillTH.Checked == true)
            {
                flagLang = "TH";
            }
            else
            {
                flagLang = "EN";
            }

            genOrder();
        }
         

        private void checkButtonClearAll()
        {
            //if (this.isSend == 1)
            //    buttonClearAllOrder.Visible = false;
            //else
            //    buttonClearAllOrder.Visible = true;

        }

      

        private void buttonAllOrder_Click(object sender, EventArgs e)
        {
            try
            {

                printAllOrder();

           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        int flagPrinter2 = 0;

        private void printAllOrder()
        {


            int i = 0;
            int printerNo = 0;

            string subPrintName = "";

            try
            {

                Table table = gd.getMainOrderByTable(this.tableID, this.flagLang, 0, 1, this.memIDSelected, this.subOrderID);

                this.tablePrint = table;

                string tableName;

                tableName = table.TableName;

                List<Order> orders = table.Order;

                this.orderPrint = orders;

                string strNum = "";

                txtOrderAppToPrintOne = "";
                txtOrderAppToPrint = "";
                txtOrderAppToPrintAll = "";

                int orderOldNo = 0;

                string[] txtProduct;
                string txtProductName = "";
                string txtProductRemark = "";

                int noPrinter = 0;

                int noOrderNo = 1;

                int subPrintNo = 0;

                List<string> txtPrint;
                int len = 35;
                float totalpriceperorder = 0;


                foreach (Order o in orders)
                {


                    txtProduct = o.ProductName.Split('|');
                    txtProductName = txtProduct[0];
                    txtProductRemark = txtProduct[1];

                    noPrinter = Int32.Parse(o.CatName);
                    subPrintNo = o.ProductCatID;
                    subPrintName = o.ProductCatName;

                    txtOrderAppToPrintOne = "";

                    txtPrint = FuncString.WordWrap(txtProductName, len);
                    txtProductName = "";

                    foreach (string op in txtPrint)
                    {
                        txtProductName += op + "\r\n";
                    }

                    if (subPrintName == "ITEM")
                    {

                        txtOrderAppToPrintOne = "(" + o.OrderQTY + ") " + txtProductName;


                        if (txtProductRemark.Trim().Length > 1)
                        {
                            string[] remarkString = txtProductRemark.Remove(0, 1).Split('+');

                            foreach (string r in remarkString)
                            {
                                txtOrderAppToPrintOne += "  +" + r.Split('{')[0] + "\r\n";
                            }
                        }

                        txtOrderAppToPrintOne += "\r\n";

                        orderBarcodePrint = o.OrderBarcode;

                        if (noPrinter > 0)
                            SelectPrinter(noPrinter);

                    }
                    else if (subPrintName == "ONE")
                    {

                        for (i = 1; i <= o.OrderQTY; i++)
                        {

                            txtOrderAppToPrintOne = "(1) " + txtProductName;


                            if (txtProductRemark.Trim().Length > 1)
                            {
                                string[] remarkString = txtProductRemark.Remove(0, 1).Split('+');

                                foreach (string r in remarkString)
                                {
                                    txtOrderAppToPrintOne += "  +" + r.Split('{')[0] + "\r\n";
                                }
                            }

                            txtOrderAppToPrintOne += "\r\n";
                            orderBarcodePrint = o.OrderBarcode;

                            if (noPrinter > 0)
                                SelectPrinter(noPrinter);
                        }


                    }

                }

                txtOrderAppToPrintOne = "";
               

                orderBarcodePrint = "";
                txtOrderAppToPrint = "";
                totalpriceperorder = 0;

                foreach (Order o in orders)
                {

                    txtProduct = o.ProductName.Split('|');
                    txtProductName = txtProduct[0];
                    txtProductRemark = txtProduct[1];
                     

                    txtPrint = FuncString.WordWrap(txtProductName, len);
                    txtProductName = "";

                    foreach (string op in txtPrint)
                    {
                        txtProductName += op + "\r\n";
                    }


                    noPrinter = Int32.Parse(o.CatName);
                    subPrintNo = o.ProductCatID;
                    subPrintName = o.ProductCatName;

                    if (subPrintName == "ALL" )
                    {
                        if (noPrinter > 0)
                        { 

                            if (printerNo == noPrinter)
                            {
                                totalpriceperorder += o.OrderAmount;

                                // ingredian same order 
                                if (txtProductName.Substring(0,1) == "-")
                                {
                                    if(o.OrderQTY == 1)
                                        txtOrderAppToPrint += "  " + txtProductName;
                                    else
                                        txtOrderAppToPrint += "(" + o.OrderQTY + ") " + txtProductName;
                                }
                                else
                                {
                                    txtOrderAppToPrint += "\n(" + o.OrderQTY + ") " + txtProductName;
                                } 

                                if (txtProductRemark.Trim().Length > 1)
                                {
                                    string[] remarkString = txtProductRemark.Remove(0, 1).Split('+');

                                    foreach (string r in remarkString)
                                    {
                                        txtOrderAppToPrint += "  +" + r.Split('{')[0] + "\r\n";
                                    }
                                }

                              //  txtOrderAppToPrint += "\r\n";
                            }
                            else
                            {
                                // New Order 
                                   
                                if (noPrinter > 0)
                                {
                                    if (txtOrderAppToPrint.Length > 0)
                                    {
                                        if (flagOrderShowPrice == "Y")
                                            txtOrderAppToPrint += "\r\n Order Amount : " + totalpriceperorder.ToString("###,###") + "\r\n";

                                        totalpriceperorder = 0;
                                        SelectPrinter(printerNo);
                                    }

                                }

                                totalpriceperorder = o.OrderAmount;


                                if (txtProductName.Substring(0, 1) == "-")
                                {
                                    if (o.OrderQTY == 1)
                                        txtOrderAppToPrint = "  " + txtProductName;
                                    else
                                        txtOrderAppToPrint = "(" + o.OrderQTY + ") " + txtProductName;
                                }
                                else
                                {
                                    txtOrderAppToPrint = "\n(" + o.OrderQTY + ") " + txtProductName;
                                } 

                                 

                                if (txtProductRemark.Trim().Length > 1)
                                {
                                    string[] remarkString = txtProductRemark.Remove(0, 1).Split('+');

                                    foreach (string r in remarkString)
                                    {
                                        txtOrderAppToPrint += "  +" + r.Split('{')[0] + "\r\n";
                                    }
                                }

                               // txtOrderAppToPrint += "\r\n";

                                printerNo = noPrinter;
                            }
                            noOrderNo++;
                        }
                    }

                    orderBarcodePrint = o.OrderBarcode; 

                }



                if (txtOrderAppToPrint.Length > 0)
                {
                    if (flagOrderShowPrice == "Y")
                        txtOrderAppToPrint += "\r\n Order Amount : " + totalpriceperorder.ToString("###,###") + "\r\n";

                    totalpriceperorder = 0;

                    SelectPrinter(printerNo);
                }


                // Printer 2

                flagPrinter2 = 1;

                orderBarcodePrint = "";
                txtOrderAppToPrint = "";
                totalpriceperorder = 0;

                foreach (Order o in orders)
                {

                    txtProduct = o.ProductName.Split('|');
                    txtProductName = txtProduct[0];
                    txtProductRemark = txtProduct[1];

                    totalpriceperorder += o.OrderAmount;


                    txtPrint = FuncString.WordWrap(txtProductName, len);
                    txtProductName = "";

                    foreach (string op in txtPrint)
                    {
                        txtProductName += op + "\r\n";
                    }


                    noPrinter = o.StdTime;
                    subPrintNo = o.ProductCatID;
                    subPrintName = o.ProductCatName;

                    if (subPrintName != "Y")
                    {
                        if (noPrinter > 0)
                        {
                            if (printerNo == noPrinter)
                            {
                                 
                                // ingredian same order 
                                txtOrderAppToPrint += "(" + o.OrderQTY + ") " + txtProductName;

                                if (txtProductRemark.Trim().Length > 1)
                                {
                                    string[] remarkString = txtProductRemark.Remove(0, 1).Split('+');

                                    foreach (string r in remarkString)
                                    {
                                        txtOrderAppToPrint += "  +" + r.Split('{')[0] + "\r\n";
                                    }
                                }

                                txtOrderAppToPrint += "\r\n";
                            }
                            else
                            {
                                // New Order 

                                txtOrderAppToPrint = "(" + o.OrderQTY + ") " + txtProductName;

                                if (txtProductRemark.Trim().Length > 1)
                                {
                                    string[] remarkString = txtProductRemark.Remove(0, 1).Split('+');

                                    foreach (string r in remarkString)
                                    {
                                        txtOrderAppToPrint += "  +" + r.Split('{')[0] + "\r\n";
                                    }
                                }

                                txtOrderAppToPrint += "\r\n";

                                printerNo = noPrinter;
                            }
                            noOrderNo++;
                        }
                    }

                    orderBarcodePrint = o.OrderBarcode;

                } 

                if (txtOrderAppToPrint.Length > 0)
                    SelectPrinter(printerNo);

             


                flagPrinter2 = 0; 


                orderBarcodePrint = "";
                txtOrderAppToPrintAll = "";
                totalpriceperorder = 0;

                if (this.flagPrintOrderChecker == "Y")
                {

                    foreach (Order o in orders)
                    {

                        txtProduct = o.ProductName.Split('|');
                        txtProductName = txtProduct[0];
                        txtProductRemark = txtProduct[1];


                        txtPrint = FuncString.WordWrap(txtProductName, len);
                        txtProductName = "";

                        foreach (string op in txtPrint)
                        {
                            txtProductName += op + "\r\n";
                        }


                        noPrinter = Int32.Parse(o.CatName);
                        subPrintNo = o.ProductCatID;
                        //  subPrintName = o.ProductCatName;


                        totalpriceperorder += o.OrderAmount;

                        if (noPrinter > 0)
                        {
                            if (printerNo == noPrinter)
                            {

                                txtOrderAppToPrintAll += "(" + o.OrderQTY + ") " + txtProductName;


                                if (txtProductRemark.Trim().Length > 1)
                                {

                                    string[] remarkString = txtProductRemark.Remove(0, 1).Split('+');

                                    foreach (string r in remarkString)
                                    {
                                        txtOrderAppToPrintAll += "  +" + r.Split('{')[0] + "\r\n";
                                    }

                                }

                               // txtOrderAppToPrintAll += "\r\n";

                            }
                            else
                            {

                                if (noPrinter > 1)
                                    txtOrderAppToPrintAll += "--------------------------------------" + "\r\n";

                                txtOrderAppToPrintAll += "(" + o.OrderQTY + ") " + txtProductName;

                                if (txtProductRemark.Trim().Length > 1)
                                {
                                    string[] remarkString = txtProductRemark.Remove(0, 1).Split('+');

                                    foreach (string r in remarkString)
                                    {
                                        txtOrderAppToPrintAll += "  +" + r.Split('{')[0] + "\r\n";
                                    }
                                }

                               // txtOrderAppToPrintAll += "\r\n";

                                printerNo = noPrinter;

                            }

                            noOrderNo++;
                        }

                    }

                    if (flagOrderCheckerShowPrice == "Y")
                        txtOrderAppToPrintAll += "\r\n Total Amount : " + totalpriceperorder.ToString("###,###") + "\r\n";

                    //  printOrderAll.Print();

                    /*  Add Printer Checker Name */

                    if (posPrinters[21].FlagPrint == "Y")
                    {
                        if (txtOrderAppToPrintAll.Length > 0)
                        {
                            printOrderAll.PrinterSettings.PrinterName = posPrinters[21].PrinterName; ;
                            printOrderAll.Print();
                            printOrderAll.PrinterSettings.PrinterName = posPrinters[0].PrinterName; ;
                        }
                    }

                    if (posPrinters[22].FlagPrint == "Y")
                    {
                        if (txtOrderAppToPrintAll.Length > 0)
                        {
                            printOrderAll.PrinterSettings.PrinterName = posPrinters[22].PrinterName; ;
                            printOrderAll.Print();
                            printOrderAll.PrinterSettings.PrinterName = posPrinters[0].PrinterName; ;
                        }
                    }

                }



            }
            catch (Exception ex)
            {

            }
            finally
            {
                //if (subPrintName != "Y")
                //    SelectPrinter(printerNo);  

                genOrder();
            }
        }




        private void SelectPrinter(int printerNo)
        {

            strPrinterOrder = posPrinters[printerNo].PrinterStrName;
            printSendOrder.PrinterSettings.PrinterName = posPrinters[printerNo].PrinterName;


            if (printerNo > 0)
                printSendOrder.Print();
        }

        private void OnPrintOrderAll(object sender, PrintPageEventArgs e)
        {
            try
            {

                int x = 0;
                int y = 0;

                Brush brush = new SolidBrush(Color.Black);

                Font fontHeader = new Font("Tahoma", 15);
                Font fontTable = new Font("Tahoma", 17);
                Font fontSubHeader = new Font("Tahoma", 10);
                Font fontBody = new Font("Tahoma", 13);

                //   y += 50;

                Font fontHeaderQ = new Font("Arail", 30);

                if (this.billID > 0)
                {
                    e.Graphics.DrawString("Q" + FuncString.Right(this.billID.ToString(), 3), fontHeaderQ, brush, x + 80, y);
                    y += 45;
                }

                //   y += 50;

                e.Graphics.DrawString("[[ Order Checker ]]", fontTable, brush, x + 5, y);

                y += 30;

                e.Graphics.DrawString("Print All Order " + " ( Order by : " + Login.userName + ")", fontSubHeader, brush, x + 5, y);


                if (this.staffName.Length > 0)
                {

                    y += 25;
                    e.Graphics.DrawString("[[[[ Staff Name : " + this.staffName + " ]]]]]", fontSubHeader, brush, x + 5, y);
             
                }


                y += 15;

                e.Graphics.DrawString(" " + this.txtTableName.Text, fontTable, brush, x + 5, y);

                y += 25;

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd/MM/yyyy}", dt);
                string strTime = String.Format("{0:HH:mm:ss}", dt);

                e.Graphics.DrawString("Date " + strDate + "   Time  " + strTime, fontSubHeader, brush, x + 5, y);

                y += 15;

                e.Graphics.DrawString("--------------------------------------------------", fontSubHeader, brush, x, y);

                y += 15;

                e.Graphics.DrawString(this.txtOrderAppToPrintAll, fontBody, brush, x, y);

                e.HasMorePages = false;

                //System.Threading.Thread.Sleep(500);


            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
            finally
            {
                // genOrder();
            }
        }

      //  int salesNextBill = 0;
         

        private void OnPrintSendOrder(object sender, PrintPageEventArgs e)
        {
            try
            {

                string txtPrint = "";

                if (this.txtOrderAppToPrintPickup != "")
                {
                    txtPrint = this.txtOrderAppToPrintPickup;
                }
                if (this.txtOrderAppToPrintOne != "")
                {
                    txtPrint = this.txtOrderAppToPrintOne;
                }
                else
                {
                    if (this.txtOrderAppToPrint.Length > 0)
                        txtPrint = this.txtOrderAppToPrint;
                }


                int x = 0;
                int y = 0;

                Brush brush = new SolidBrush(Color.Black);
                Font fontHeaderBIG = new Font("Tahoma", 30);
                Font fontHeader = new Font("Tahoma", 15);
                Font fontTable = new Font("Tahoma", 17);
                Font fontSubHeader = new Font("Tahoma", 10);
                Font fontBody = new Font("Tahoma", 13);

                y += 20;

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd/MM/yyyy}", dt);
                string strTime = String.Format("{0:HH:mm:ss}", dt);


                Font fontHeaderQ = new Font("Arail", 30);

                if (this.billID > 0)
                {
                    e.Graphics.DrawString("Q" + FuncString.Right(this.billID.ToString(), 3), fontHeaderQ, brush, x + 80, y);
                    y += 45;
                }

                if (this.txtOrderAppToPrintPickup != "")
                {
                    y += 10;

                    e.Graphics.DrawString("[[ Pickup Order ]]", fontTable, brush, x + 5, y);

                    y += 30;

                    e.Graphics.DrawString(this.txtTableName.Text, fontHeaderBIG, brush, x + 0, y);

                    y += 60;
                }
                else if (this.txtOrderAppToPrintOne != "")
                {

                    y += 10;

                    e.Graphics.DrawString("ส่ง " + strPrinterOrder + " ( Order by : " + Login.userName + ")", fontSubHeader, brush, x + 5, y);

                    y += 15;

                    if (flagPrinter2 == 1)
                        e.Graphics.DrawString("[[  Combine Menu  ]]", fontTable, brush, x + 5, y);
                    else
                        e.Graphics.DrawString("[[  Send Order  ]]", fontTable, brush, x + 5, y);

                    y += 30;

                    e.Graphics.DrawString(this.txtTableName.Text, fontHeaderBIG, brush, x + 0, y);

                    y += 60;
                }
                else
                {

                    e.Graphics.DrawString("Send " + strPrinterOrder + " ( Order by : " + Login.userName + ")", fontSubHeader, brush, x + 5, y);


                    if (this.staffName.Length > 0)
                    {

                        y += 15;
                        e.Graphics.DrawString("[[[[ Staff Name : " + this.staffName + " ]]]]]", fontSubHeader, brush, x + 5, y);

                    }

                    y += 15;

                    e.Graphics.DrawString(" " + this.txtTableName.Text, fontTable, brush, x + 5, y);

                    y += 25;

                }

                if (this.staffName.Length > 0)
                {  
                    e.Graphics.DrawString("[[[[ Staff Name : " + this.staffName + " ]]]]]", fontSubHeader, brush, x + 5, y);
                    y += 30;
                }

                e.Graphics.DrawString("Date " + strDate + "   Time  " + strTime, fontSubHeader, brush, x + 5, y);

                y += 20;

                if (flagKitchenMonitor == "Y")
                {


                    BarCode.GenBarCode(orderBarcodePrint);
                    Image barcodeImg = BarCode.resultBarcode;

                    e.Graphics.DrawImage(barcodeImg, x + 10, y, 200, 90);

                    y += 90;
                }




                if (this.isSend == 0)
                    e.Graphics.DrawString("------------------- (New Table) ------------------", fontSubHeader, brush, x, y);
                else
                    e.Graphics.DrawString("-----------------------------------------------", fontSubHeader, brush, x, y);

                y += 15;

                e.Graphics.DrawString(txtPrint, fontBody, brush, x, y);




                e.HasMorePages = false;

                //System.Threading.Thread.Sleep(500);


            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
            finally
            {
                // genOrder();
            }
        } 

        private void buttonMemCard_Click(object sender, EventArgs e)
        {
            if (panelMemCard.Visible == false)
            {
                panelMemCard.Visible = true;
                textBoxStrSearchMemCard.Focus();
            }
            else
            {
                panelMemCard.Visible = false;
            }
        }

        private void buttonSearchMemCard_Click(object sender, EventArgs e)
        {
            searchMemCard();
        }

        private void textBoxStrSearchMemCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                searchMemCard();
            }

            if (e.KeyChar == (char)8)
            {
                clearMemCard();
            }
        }

        // Coupon

        List<Coupon> allCoupon;

        private void searchMemCard()
        {
            string strSearchMemCard = textBoxStrSearchMemCard.Text;

            clearMemCard();

            string flagSynTax = "";


            if (strSearchMemCard.Length > 1)
            {
                mc = gd.SelMemCard_Search(strSearchMemCard, this.statusRestAllUse);
            }
            else
            {
                this.memCardID = "0";
                this.memIDSelected = 0;
                textBoxMemCardPromotion.Text = "ไม่พบสมาชิก";
            }


            try
            {

                if (mc == null)
                {
                    if(idCardRead == 1)
                        throw new Exception("ไม่พบสมาชิกที่มีเลขบัตรประชาชนนี้ \n\r กรุณาสมัครสมาชิกก่อนค่ะ");
                    else
                        throw new Exception("ไม่พบเลขสมาชิก / เบอร์โทรนี้ / ยังไม่ได้สมัครสมาชิก");
                }

                this.memCardID = mc.MemCardID;

                if (this.memCardID.Length > 2)
                  this.memIDSelected = 999;
                 

                textBoxMemCardID.Text = mc.MemCardID;
                textBoxCardID.Text = mc.CardID;
                 
                textBoxMemCardName.Text = mc.MemCardName;
                textBoxMemCardTel.Text = mc.Tel;
                textBoxMemCardProName.Text = mc.PromotionCode;
                textBoxMemCardPromotion.Text = mc.PromotionDesc;

                if (mc.AdviceName.Length > 1)
                    textBoxMemCardPromotion.Text += "\r\n" + mc.AdviceName;

                textBoxMemCardBD.Text = mc.BirthDate.Date.ToString("dd-MMM-yyyy");
                textBoxMemCardExpireDate.Text = mc.ExpireDate.Date.ToString("dd-MMM-yyyy");
                textBoxMemCardPoint.Text = mc.Point.ToString();
                textBoxMemCardPointPeriod.Text = mc.PointPeriod.ToString();
                textBoxMemCardAmt.Text = mc.PAmount.ToString();
                textBoxMemCardAmtPeriod.Text = mc.PAmountPeriod.ToString();
                textBoxMemCashBalance.Text = mc.CCBalanceAmt.ToString();
                 
                //// 

                //////// Coupon

                allCoupon = gd.getAllCoupon(0, "000", this.memCardID);
                dataGridViewCouponData.DataSource = allCoupon;

                //   countUsedCoupon();

                dataGridViewCouponData.Columns[1].Visible = false;
                dataGridViewCouponData.Columns[4].Visible = false;
                dataGridViewCouponData.Columns[7].Visible = false;
                dataGridViewCouponData.Columns[8].Visible = false;
                dataGridViewCouponData.Columns[9].Visible = false;
                dataGridViewCouponData.Columns[10].Visible = false;
                dataGridViewCouponData.Columns[12].Visible = false;
                dataGridViewCouponData.Columns[11].DisplayIndex = 1;
                dataGridViewCouponData.Columns[11].HeaderText = "Used";

                flagSynTax = mc.PromotionSyntax;

                //this.flagExpire = mc.FlagExpire;

                //if (this.flagExpire == "Y")
                //{
                //    labelFlagExpire.Text = "สถานะบัตร : บัตรหมดอายุแล้ว";
                //    MessageBox.Show("สถานะบัตร : บัตรหมดอายุแล้ว " + "ไม่สามารถสะสมคะแนนได้ กรุณาต่ออายุบัตรก่อน");
                //}
                //else
                //{
                //    labelFlagExpire.Text = "สถานะบัตร : บัตรยังไม่หมดอายุ"; 
                    
                //}

                actionDiscountBySynTax(flagSynTax);
               

                //labelFlagExpire.Text = "สถานะบัตร : ";

                if (textBoxStrSearchMemCard.Text.Length > 1)
                    buttonMemCard.Text = "สมาชิก : " + textBoxMemCardID.Text;
                else
                    buttonMemCard.Text = "บัตรสมาชิก";




                /// Change Zone
                /// 

                // string [] tablecode;
                // int tCode = 0;

                //if (textBoxCardID.Text.Contains("-"))
                //{
                //    tablecode = textBoxCardID.Text.Split('-'); 
                //    tCode = Int32.Parse(tablecode[0]);

                //    if (tCode > 0)
                //    {

                //        int result = gd.UpsMoveTable(this.tableID, tCode); 

                //        if (result <= 0)
                //            MessageBox.Show("Error Move Table : ไม่มีออร์เดอร์ย้ายโต๊ะ !!");

                //        this.tableID = tCode;

                //        genOrder();
                //    }
                //}


            }
            catch (Exception ex)
            {
                this.memCardID = "0";
                this.memIDSelected = 0;
                textBoxMemCardPromotion.Text = "ไม่พบสมาชิก";
            }
            finally
            {
                genOrder();
            }
        }

        private void clearMemCard()
        {
            textBoxMemCardID.Text = "";
            textBoxMemCardName.Text = "";
            textBoxMemCardTel.Text = "";
            textBoxMemCardPromotion.Text = "";
            textBoxMemCardExpireDate.Text = "";
            textBoxMemCardPoint.Text = "";

            textBoxDiscountAmt.Text = "0";
            textBoxGroupDis2.Text = "0";
            textBoxGroupDis1.Text = "0";
            textBoxGroupDis3.Text = "0";
            textBoxGroupDis4.Text = "0";


            labelFlagExpire.Text = "สถานะบัตร : ";
        }

        /*  Search  Item By Key */

        private void buttonSearchItem_Click(object sender, EventArgs e)
        {
            if (panelSearch.Visible == false)
            {
                panelSearch.Visible = true;
                buttonSearch.Text = "Hide Search";
                textBoxSRProductName.Focus();
            }
            else
            {
                panelSearch.Visible = false;
                buttonSearch.Text = "Show Search";
            }
        }


        private void getComboAllCat()
        {
            try
            {
                List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

                // Add data to the List

                data.Add(new KeyValuePair<int, string>(0, "--- เลือกกลุ่มทั้งหมด ---"));

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

                comboBoxAllCat.SelectedIndex = 0; 

            }
            catch (Exception ex)
            {

            }


        } 

        private void buttonClearOrderSr_Click(object sender, EventArgs e)
        {
            textBoxSRProductName.Text = "";
            comboBoxAllCat.SelectedIndex = 0;
            comboBoxAddUnitSr.Text = "1";
        }

     

        private void buttonAddOrderSr_Click(object sender, EventArgs e)
        {
            int productID = 0;
            int qtyAdditems = 0;

            string remarkBC = "";
            string productItemWBC = "";

            int result = 0;
            float amtOrder = 0;

            try
            {

                productID = this.dataGridproductID;
                qtyAdditems = Int32.Parse(comboBoxAddUnitSr.Text.ToString());
                productItemWBC = textBoxWeightSR.Text.ToString();


                if (dataGridproductUnit.ToLower().Contains("kg"))
                {
                    remarkBC = "@W=" + productItemWBC + " KG.";
                    amtOrder = (float.Parse(dataGridproductPrice) * float.Parse(productItemWBC));
                    result = this.fn_AddOrderBarCode(productID, qtyAdditems, remarkBC, amtOrder);


                    if (result < 0)
                    {
                        throw new Exception(result.ToString());
                    }
                    else if (result > 1000)
                    { // Pro BEFORE
                        // BEFORE

                        this.productIDBF = productID;
                        this.promIDBF = result - 1000;
                        this.promproductBF();
                    }
                }
                else
                {


                    if (productID > 0 && qtyAdditems > 0)
                    {
                        result = this.fn_AddOrder(productID, qtyAdditems, remarkBC);

                         
                        if (result < 0)
                        {
                            throw new Exception(result.ToString());
                        }
                        else if (result > 1000)
                        { // Pro BEFORE
                             // BEFORE
                            this.productIDBF = productID;
                            this.promIDBF = result - 1000;
                            this.promproductBF();

                        }
                         
                    }
                } 
                genOrder();

            }
            catch (Exception ex)
            {

                int errorNo = Int32.Parse(ex.Message);

                if (errorNo == -1)
                     MessageBox.Show("ไม่สามารถขายช่วงวันนี้ได้");
                else if (errorNo == -2)
                    MessageBox.Show("ไม่สามารถขายช่วงเวลานี้ได้");
                else if (errorNo == -3)
                    MessageBox.Show("Promotion ไม่สามารถใช้งานได้ : NONE");
                else if (errorNo == -4)
                    MessageBox.Show("Promotion ไม่สามารถใช้งานได้ : AFTER");
                else if (errorNo == -5)
                    MessageBox.Show("Promotion ขายครบตามจำนวนแล้ว"); 
                 
            }


        }


        private int fn_AddOrderBarCode(int productID, int qtyAdditems, string remark, float amt)
        {
             
            int result = 0;

            for (int i = 1; i <= qtyAdditems; i++)
            {
                result = gd.instOrderByTable(this.tableID, productID, this.memID, remark, amt);
                 
            }
             

            return result;

        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (panelScanBC.Visible == false)
            {
                panelScanBC.Visible = true;
                buttonBC.Text = "Hide Barcode Scan";
                textBoxSearchBC.Focus();
            }
            else
            {
                panelScanBC.Visible = false;
                buttonBC.Text = "Show Barcode Scan";
            }
             


            if (radioButtonAutoAddItem.Checked == true)
            {
                buttonAddOrderBC.Enabled = false;
            }
            else
            {
                buttonAddOrderBC.Enabled = true;
            }


        }

        private void buttonScanBC_Click(object sender, EventArgs e)
        {
            clearBarcodePanel();
            textBoxSearchBC.Focus();
        }

        private void ScanBarcode_txtChange(object sender, EventArgs e)
        {
            string keySr = "";
            int productIDBC = 0;
            int qtyAdditems = 0;
            string productNameBC = "";

            int result = 0;

            try
            {
                keySr = textBoxSearchBC.Text.Trim();
                productlistSr = gd.getProductByCat_SCBarCode(keySr, Login.selecttablezoneID, this.memIDSelected);
 
                foreach (Product c in productlistSr)
                {
                    productIDBC =  c.ProductID;
                    productNameBC = c.ProductDesc ;
                }

                textBoxProductIDBC.Text = productIDBC.ToString();
                textBoxProductNameBC.Text = productNameBC;


                qtyAdditems = Int32.Parse(textBoxProductQTYBC.Text);

                if (productIDBC > 0 && qtyAdditems > 0)
                {

                    if (radioButtonAutoAddItem.Checked == true)
                    {
                        result = this.fn_AddOrder(productIDBC, qtyAdditems,"");

                        if (result < 0)
                        {
                            throw new Exception(result.ToString());
                        }
                        else if (result > 1000)
                        { // Pro BEFORE
                            // BEFORE

                            this.productIDBF = productIDBC;
                            this.promIDBF = result - 1000;
                            this.promproductBF();
                        }

                        clearBarcodePanel();

                       
                        genOrder();
                       
                    }
                }


            }
            catch (Exception ex)
            {
                int errorNo = Int32.Parse(ex.Message);

                if (errorNo == -1)
                    MessageBox.Show("ไม่สามารถขายช่วงวันนี้ได้");
                else if (errorNo == -2)
                    MessageBox.Show("ไม่สามารถขายช่วงเวลานี้ได้");
                else if (errorNo == -3)
                    MessageBox.Show("Promotion ไม่สามารถใช้งานได้ : NONE");
                else if (errorNo == -4)
                    MessageBox.Show("Promotion ไม่สามารถใช้งานได้ : AFTER");
                else if (errorNo == -5)
                    MessageBox.Show("Promotion ขายครบตามจำนวนแล้ว"); 
            }
        }

        private int fn_AddOrder(int productID, int qtyAdditems, string remark)
        {

            int result = 0;

            //  string remark = textBoxReason.Text;


            for (int i = 1; i <= qtyAdditems; i++)
            {
                result = gd.instOrderByTable(this.tableID, productID, this.memID, remark, 0); 

            }

            return result;

        }

        private void buttonAddOrderBC_Click(object sender, EventArgs e)
        {
            string keySr = "";
            int productIDBC = 0;
            int qtyAdditems = 0;
            string productNameBC = "";

            int result = 0;

            try
            {
                keySr = textBoxSearchBC.Text.Trim(); 

                productIDBC = Int32.Parse(textBoxProductIDBC.Text);   
                qtyAdditems = Int32.Parse(textBoxProductQTYBC.Text);

                if (productIDBC > 0 && qtyAdditems > 0)
                {
                    result = this.fn_AddOrder(productIDBC, qtyAdditems,"");

                    if (result < 0)
                    {
                        throw new Exception(result.ToString());
                    }
                    else if (result > 1000)
                    { // Pro BEFORE
                        // BEFORE

                        this.productIDBF = productIDBC;
                        this.promIDBF = result - 1000;
                        this.promproductBF();
                    }

                }

               
                genOrder();
                clearBarcodePanel(); 
                

            }
            catch (Exception ex)
            {
                int errorNo = Int32.Parse(ex.Message);

                if (errorNo == -1)
                    MessageBox.Show("ไม่สามารถขายช่วงวันนี้ได้");
                else if (errorNo == -2)
                    MessageBox.Show("ไม่สามารถขายช่วงเวลานี้ได้");
                else if (errorNo == -3)
                    MessageBox.Show("Promotion ไม่สามารถใช้งานได้ : NONE");
                else if (errorNo == -4)
                    MessageBox.Show("Promotion ไม่สามารถใช้งานได้ : AFTER");
                else if (errorNo == -5)
                    MessageBox.Show("Promotion ขายครบตามจำนวนแล้ว"); 
            }
        }

        private void clearBarcodePanel()
        {
            textBoxSearchBC.Text = "";
            textBoxProductIDBC.Text = "";
            textBoxProductNameBC.Text = "";
            textBoxWeightBC.Text = "";
            textBoxItemCountBC.Text = "";
            textBoxPriceBC.Text = "";
            textBoxSearchBC.Focus();
        }

        private void radioButBarcode_Change(object sender, EventArgs e)
        {

            if (radioButtonAutoAddItem.Checked )
            {
                buttonAddOrderBC.Enabled = false;
                panelSelPrice.Visible = false;
            }
            else if( radioButtonManualAdd.Checked )
            {
                buttonAddOrderBC.Enabled = true;
                panelSelPrice.Visible = false;
            } 

           
        }

        private void CheckPrintBill()
        {

            if (flagCheckprintBillBefore == "Y")
            {

                if (this.printBillFlag == 0)
                {
                    buttonCheckBill.Enabled = false;
                }
                else
                {
                    buttonCheckBill.Enabled = true;
                }
            }
        }

        private void printOrderDel_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

                int x = 0;
                int y = 0;

                Brush brush = new SolidBrush(Color.Black);
                Font fontHeader = new Font("Tahoma", 15);
                Font fontTable = new Font("Tahoma", 17);
                Font fontSubHeader = new Font("Tahoma", 10);
                Font fontBody = new Font("Tahoma", 14);

                y += 10;

                if (flagResendORder == 1)
                    e.Graphics.DrawString("[[ Pick up Order ]]", fontTable, brush, x + 5, y);
                else
                  e.Graphics.DrawString("[[ ยกเลิกรายการ ]]", fontTable, brush, x + 5, y);
                 

                y += 30;

                e.Graphics.DrawString( " (  By : " + Login.userName + ")", fontSubHeader, brush, x + 5, y);

                y += 15;

                e.Graphics.DrawString(" " + this.txtTableName.Text, fontHeader, brush, x + 5, y);

                y += 25;

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd/MM/yyyy}", dt);
                string strTime = String.Format("{0:HH:mm:ss}", dt);

                e.Graphics.DrawString("Date " + strDate + "   Time  " + strTime, fontSubHeader, brush, x + 5, y);

                y += 20;

           

                e.Graphics.DrawString(this.txtOrderAppToPrint, fontBody, brush, x, y);

                e.HasMorePages = false;

                //System.Threading.Thread.Sleep(500);


            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
            finally
            {
                // genOrder();
            }
        }

        private void ButtonNum_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;


            try
            {
                defaultColButOrderNo();
                bt.BackColor = System.Drawing.Color.Orange;

                this.keyOrderNo = Int32.Parse(bt.Name.Replace("button_", " ").Trim());
                string txtShow = this.keyOrderNo.ToString();

                labelTxtNumOrder.Text = txtShow;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void hideButOrderNo()
        {
            button_01.Visible = false;
            button_02.Visible = false;
            button_03.Visible = false;
            button_04.Visible = false;
            button_05.Visible = false;
            button_06.Visible = false;
            button_07.Visible = false;
            button_08.Visible = false;
            button_09.Visible = false;
            button_10.Visible = false;
            button_20.Visible = false;
        }



        private void defaultColButOrderNo()
        {
            button_01.BackColor = System.Drawing.Color.White;
            button_02.BackColor = System.Drawing.Color.White;
            button_03.BackColor = System.Drawing.Color.White;
            button_04.BackColor = System.Drawing.Color.White;
            button_05.BackColor = System.Drawing.Color.White;
            button_06.BackColor = System.Drawing.Color.White;
            button_07.BackColor = System.Drawing.Color.White;
            button_08.BackColor = System.Drawing.Color.White;
            button_09.BackColor = System.Drawing.Color.White;
            button_10.BackColor = System.Drawing.Color.White;
            button_20.BackColor = System.Drawing.Color.White;
        }

        private void defaultColBut()
        {
            keyOrderNo = 1;
            button_01.BackColor = System.Drawing.Color.Orange;
            labelTxtNumOrder.Text = "1";

            button_02.BackColor = System.Drawing.Color.White;
            button_03.BackColor = System.Drawing.Color.White;
            button_04.BackColor = System.Drawing.Color.White;
            button_05.BackColor = System.Drawing.Color.White;
            button_06.BackColor = System.Drawing.Color.White;
            button_07.BackColor = System.Drawing.Color.White;
            button_08.BackColor = System.Drawing.Color.White;
            button_09.BackColor = System.Drawing.Color.White;
            button_10.BackColor = System.Drawing.Color.White;
            button_20.BackColor = System.Drawing.Color.White;
        }

        private void buttonCheckCoupon_Click(object sender, EventArgs e)
        {
            try
            {

                string couponCodetxt = textBoxCouponCode.Text;
                textBoxCouponDesc.Text = "";
                textBoxGroupDis1.Text = "0";
                textBoxDiscountAmt.Text = "0";

                couponInput = gd.getAllCoupon(0, couponCodetxt, "000");

                int i = 0;
                string txtCouponDesc = "";
                string flagExpire = "Y";
                string flagUse = "N";
                string flagCouponUsed = "Y";
                string flagSynTax = "";

                foreach (Coupon c in couponInput)
                {
                    txtCouponDesc = c.CouponTextShow;
                    flagExpire = c.CouponFlagExpire;
                    flagUse = c.CouponFlagUse;
                    flagCouponUsed = c.CouponUsed;
                    flagSynTax = c.CouponSynTax;

                    i++;
                }

                if (i > 0)
                {
                    if (flagExpire == "N" && flagUse == "Y" && flagCouponUsed == "N")
                    {
                        this.flagCouponCanUse = 1;

                        MessageBox.Show(couponCodetxt + " คูปองสามารถใช้ได้");
                        textBoxCouponDesc.Text = txtCouponDesc;
                        actionDiscountBySynTax(flagSynTax);
                    }
                    else
                    {
                        MessageBox.Show(txtCouponDesc);
                        textBoxCouponCode.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบ Coupon Code นี้ : " + couponCodetxt + " ในระบบ !!");
                    textBoxCouponCode.Text = "";
                }

            }
            catch (Exception ex)
            {

            }


        }

        private void insertOrderProduct(int orderProductID, int orderQty)
        {

            try
            {

                string productSyntax = "";
                string productName = "";
                int ProductPoint = 0;

                foreach (Product p in productlists)
                {
                    if (p.ProductID == orderProductID)
                    {
                        productSyntax = p.ProductDesc;
                        ProductPoint = p.ProductGetPoint;
                        productName = p.ProductName;

                    }
                }





                if (productSyntax.Contains("FOR:"))
                    this.actionDiscountBySynTax(productSyntax);



                //


                this.productActionType = 0;

                int result = 0;
                float percentOntop = 0;
                float discountIntop = 0;


                foreach (Product p in productByCat)
                {
                    if (p.ProductID == orderProductID)
                    {
                        this.strDisplayLine1 = p.ProductNameEN;


                        if (p.ProductUnit.ToLower().Contains("kg") && p.ProductDesc.ToLower().Contains("rem"))
                        {
                            this.productActionType = 1;
                            this.productRemAct = Int32.Parse(FuncString.TextBetween(p.ProductDesc, "[", "]"));
                        }
                        else if (p.ProductUnit.ToLower().Contains("kg"))
                        {
                            this.productActionType = 2;
                        }
                        else if (p.ProductDesc.ToLower().Contains("rem"))
                        {
                            result = this.fn_AddOrder(orderProductID, this.keyOrderNo, "");
                            genOrder();
                            this.ActionforProduct();
                            this.productActionType = 3;
                            this.productRemAct = Int32.Parse(FuncString.TextBetween(p.ProductDesc, "[", "]"));

                            //  MessageBox.Show(this.productRemAct.ToString());
                        }

                        productIDAct = p.ProductID;

                        productNameAct = p.ProductName;
                        productPriceAct = p.ProductPrice;


                    }
                }

                if (this.productActionType > 0)
                {
                    this.ActionforProduct();
                    throw new Exception("100");

                }

                //  string adRem = "";


                if (orderProductID == 6)
                {
                    discountIntop = this.totalSalesAmount * (float)0.03;
                    result = gd.instOrderByTable(this.tableID, orderProductID, this.memID, "", discountIntop);
                }
                else if (orderProductID == 7)
                {
                    discountIntop = (this.totalSalesAmount - 30000) * (float)0.03;
                    result = gd.instOrderByTable(this.tableID, orderProductID, this.memID, "", discountIntop);
                }
                else if (productName.Contains("On-Top"))
                {
                    percentOntop = float.Parse(FuncString.TextBetween(productName, "[On-Top:", "%]"));
                    discountIntop = this.totalSalesAmount * percentOntop / (float)-100;
                    remarkLine1 = "+On-Top Dis. : " + this.totalSalesAmount.ToString();

                    result = gd.instOrderByTable(this.tableID, orderProductID, this.memID, remarkLine1, discountIntop);
                }
                else
                {

                    result = this.fn_AddOrder(orderProductID, this.keyOrderNo, "");

                }

                if (result < 0)
                {
                    throw new Exception(result.ToString());
                }
                else if (result > 1000)
                { // Pro BEFORE
                    // BEFORE

                    this.productIDBF = orderProductID;
                    this.promIDBF = result - 1000;

                    this.promproductBF();

                }

                genOrder();

                OrderProductRemarkPN.VerticalScroll.Value = OrderProductRemarkPN.VerticalScroll.Maximum;

                panelOrder.VerticalScroll.Value = panelOrder.VerticalScroll.Maximum;

            }
            catch (Exception ex)
            {
                if (FuncString.IsNumeric(ex.Message))
                {

                    int errorNo = Int32.Parse(ex.Message);

                    if (errorNo == -1)
                        MessageBox.Show("ไม่สามารถขายช่วงวันนี้ได้");
                    else if (errorNo == -2)
                        MessageBox.Show("ไม่สามารถขายช่วงเวลานี้ได้");
                    else if (errorNo == -3)
                        MessageBox.Show("Promotion ไม่สามารถใช้งานได้ : NONE");
                    else if (errorNo == -4)
                        MessageBox.Show("Promotion ไม่สามารถใช้งานได้ : AFTER");
                    else if (errorNo == -100)
                        MessageBox.Show(Login.userName + " ไม่สามารถ Order ได้ ");
                }
                else
                {

                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void actionDiscountBySynTax(string synt)
        {
            try
            {

                /*
                 * 
                
                 * 
                 
                FOR:Disc=:10:8:2:8:0:0
                 
               [1] > DL = Discount Live Product
               [2] > DR = Discount Food Drink
               [3] > DA = Discount Alcohol
               [4] > DS = Discount Super
               [5] > DB = Discount Baht
               [6] > เลือกสินค้าอื่นด้วย = ProductID

                
                 */

                string[] syntCode;

                synt = synt.Replace("FOR:", "");


                if (synt.Contains("*"))
                {
                    syntCode = synt.Split('*');
                    textBoxStrSearchMemCard.Text = syntCode[1];
                    searchMemCard();

                    synt = syntCode[0];
                }

                string[] disCodes = synt.Split(':');

                string dis1 = disCodes[1];
                string dis2 = disCodes[2];
                string dis3 = disCodes[3];
                string dis4 = disCodes[4];
                string disB = disCodes[5];
                int couponProductID = Int32.Parse(disCodes[6].ToString());


                if (couponProductID > 0)
                {
                    //  couponProductID = gd.instOrderByTable(this.tableID, couponProductID, this.memID, "", 0); 
                    //  genOrder();

                    this.insertOrderProduct(couponProductID, 1);
                }
                else
                {
                    textBoxDiscountAmt.Text = disB;
                    textBoxGroupDis1.Text = dis1;
                    textBoxGroupDis2.Text = dis2;
                    textBoxGroupDis3.Text = dis3;
                    textBoxGroupDis4.Text = dis4;
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxCouponCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.buttonCheckCoupon_Click(null, e);
            }
        }
        
        private void buttonMainT_Click(object sender, EventArgs e)
        {

            
        } 

        private void ButtonBCNum_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            string numstr = "";

            try
            {
                defaultColButOrderNoBC();
                bt.BackColor = System.Drawing.Color.Orange;

                numstr = Int32.Parse(bt.Name.Replace("buttonBC_", " ").Trim()).ToString();

                if (numstr == "13")
                {
                    textBoxProductQTYBC.Text = textBoxProductQTYBC.Text.Substring(0, textBoxProductQTYBC.Text.Length - 1);
                }
                else
                {
                    if (textBoxProductQTYBC.Text == "0")
                        textBoxProductQTYBC.Text = numstr;
                    else
                        textBoxProductQTYBC.Text += numstr;
                }

                textBoxSearchBC.Focus();


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }



        private void defaultColButOrderNoBC()
        {
            buttonBC_01.BackColor = System.Drawing.Color.White;
            buttonBC_02.BackColor = System.Drawing.Color.White;
            buttonBC_03.BackColor = System.Drawing.Color.White;
            buttonBC_04.BackColor = System.Drawing.Color.White;
            buttonBC_05.BackColor = System.Drawing.Color.White;
            buttonBC_06.BackColor = System.Drawing.Color.White;
            buttonBC_07.BackColor = System.Drawing.Color.White;
            buttonBC_08.BackColor = System.Drawing.Color.White;
            buttonBC_09.BackColor = System.Drawing.Color.White;
            buttonBC_00.BackColor = System.Drawing.Color.White;
            buttonBC_13.BackColor = System.Drawing.Color.White;
        }

       

        private void textBoxSearchBC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                string keySr = "";
                int productIDBC = 0;
                int qtyAdditems = 0;
                string productNameBC = "";



                float productItemWBC = 0;
                float productPriceBC = 0;

                int result = 0;

                int flagfindbar = 0;


                float p1 = 0;
                float p2 = 0;
                float p3 = 0;

                try
                {
                    keySr = textBoxSearchBC.Text.Trim();
                    productlistSr = gd.getProductByCat_SCBarCode(keySr, 0,0);


                    textBoxItemCountBC.Text = "0";
                    textBoxWeightBC.Text = "0";

                    labelfindBC.Text = "";

                    foreach (Product c in productlistSr)
                    {
                        productIDBC = c.ProductID;
                        productNameBC = c.ProductDesc;
                        productItemWBC = c.ProductWeight;
                        productPriceBC = c.ProductPrice;
                        flagfindbar = 1;


                        p3 = c.ProductPrice3;
                        p2 = c.ProductPrice2;
                        p1 = c.ProductPrice;

                    }


                    if (flagfindbar == 0)
                    {
                        labelfindBC.Text = "ไม่พบ Barcode : " + keySr;
                        System.Media.SystemSounds.Exclamation.Play();
                    }
                    else
                    {

                        System.Media.SystemSounds.Beep.Play();
                    }


                    textBoxProductIDBC.Text = productIDBC.ToString();
                    textBoxProductNameBC.Text = productNameBC;
                    textBoxPriceBC.Text = productPriceBC.ToString();


                    if (keySr.Substring(0, 1).ToLower() == "w")
                        textBoxWeightBC.Text = productItemWBC.ToString();
                    else
                        textBoxItemCountBC.Text = productItemWBC.ToString();


                    qtyAdditems = Int32.Parse(textBoxProductQTYBC.Text);
                     

                    string remarkBC = "";
                    float amtOrder = 0;

                    if (productItemWBC > 0)
                    {
                        if (keySr.Substring(0, 1).ToLower() == "w")
                        {
                            textBoxWeightBC.Text = productItemWBC.ToString();
                            remarkBC = "@W=" + productItemWBC + " KG.";
                        }
                        else
                        {
                            textBoxItemCountBC.Text = productItemWBC.ToString();
                            remarkBC = "@I=" + productItemWBC + " item.";
                        }
                        amtOrder = productPriceBC;
                    }


                    if (productIDBC > 0 && qtyAdditems > 0)
                    {

                        if (radioButtonAutoAddItem.Checked == true)
                        {
                            result = this.fn_AddOrderBarCode(productIDBC, qtyAdditems, remarkBC, amtOrder);

                            if (result <= 0)
                                throw new Exception("Error Insert Order Search Item");

                            clearBarcodePanel();


                            genOrder();

                        }
                        else if (radioButtonManualPrice.Checked == true)
                        {

                            panelSelPrice.Visible = true;

                            //textBoxSelPriceNo.Focus();

                            textBoxSERIAL.Focus();

                            buttonSelPrice1.Text = p1.ToString();
                            buttonSelPrice2.Text = p2.ToString();
                            buttonSelPrice3.Text = p3.ToString();

                            labelProductSelID.Text = productIDBC.ToString();

                           

                        }
                    }


                }
                catch (Exception ex)
                {
                    int errorNo = Int32.Parse(ex.Message);

                    if (errorNo == -1)
                        MessageBox.Show("ไม่สามารถขายช่วงวันนี้ได้");
                    else if (errorNo == -2)
                        MessageBox.Show("ไม่สามารถขายช่วงเวลานี้ได้");
                    else if (errorNo == -3)
                        MessageBox.Show("Promotion ไม่สามารถใช้งานได้ : NONE");
                    else if (errorNo == -4)
                        MessageBox.Show("Promotion ไม่สามารถใช้งานได้ : AFTER");
                    else if (errorNo == -5)
                        MessageBox.Show("Promotion ขายครบตามจำนวนแล้ว"); 
                }
                finally
                {
                    if (radioButtonAutoAddItem.Checked == true)
                    {
                        textBoxSearchBC.Text = "";
                        textBoxSearchBC.Focus();

                         

                    }
                     

                }
            }
        }



        private void buttonUpdateflagSend_Click(object sender, EventArgs e)
        {
            try
            {

                Table tableUpdateSend = gd.getMainOrderByTable(this.tableID, this.flagLang, 0, 1, this.memIDSelected,this.subOrderID);
                genOrder();
            }
            catch (Exception ex)
            {


            }
        }

        private void buttonClearRemark_Click(object sender, EventArgs e)
        {
            textBoxReason.Text = "";
            textBoxAdjustPriceOrder.Text = "0";
            radioButtonOrderRemBaht.Checked = true;
            radioButtonAppendRemark.Checked = true;
        }

        private void radioButtonTH_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTH.Checked == true)
            {
                flagLangProduct = "TH";
            }
            else if (radioButtonEN.Checked == true)
            {
                flagLangProduct = "EN";
            }
            else
            {
                flagLangProduct = "OT";
            }

            OrderProductPN.Controls.Clear();

            if (imgPath.Length == 0)
                this.genObjOrderProduct(catIDSected);
            else
                this.genObjOrderProductImg(catIDSected); 
         //   genObjOrderProduct(this.catIDSected); 
        }

        private void ButtonShowCheck_Click(object sender, EventArgs e)
        {
             

            if (panelFinance2.Visible == true)
                panelFinance2.Visible = false;
            else
                panelFinance2.Visible = true; 

            if (Login.userStatus.ToLower() == "cashier" || Login.userStatus.ToLower() == "admin" || Login.userStatus.ToLower() == "manager" )
            {
                if (panelFinance1.Visible == true)
                {
                    panelFinance1.Visible = false;
                    panelMemCard.Visible = false;
                }
                else
                {
                    panelFinance1.Visible = true;
                    panelMemCard.Visible = true;
                }

            }


            textBoxCouponCode.Focus();

        }

        private void buttonPorNext_Click(object sender, EventArgs e)
        {
            this.ProPage++;
            OrderProductPN.Controls.Clear();


            if (imgPath.Length == 0)
                this.genObjOrderProduct(catIDSected);
            else
                this.genObjOrderProductImg(catIDSected); 
          //  this.genObjOrderProduct(catIDSected);
        }

        private void buttonProPre_Click(object sender, EventArgs e)
        {
            this.ProPage--;
            OrderProductPN.Controls.Clear();

            if (imgPath.Length == 0)
                this.genObjOrderProduct(catIDSected);
            else
                this.genObjOrderProductImg(catIDSected); 
            
            //this.genObjOrderProduct(catIDSected);
        }

        private void buttonRemPre_Click(object sender, EventArgs e)
        {
            this.RemPage--;
            OrderProductRemarkPN.Controls.Clear();
            this.genObjOrderProductRemark();
        }

        private void buttonRemNext_Click(object sender, EventArgs e)
        {
            this.RemPage++;
            OrderProductRemarkPN.Controls.Clear();
            this.genObjOrderProductRemark();
        }

        private void ButtonScanCC_Click(object sender, EventArgs e)
        {
            txtBoxCCCode.Text = "";
            txtBoxCCCode.Focus();
        }

        private void txtBoxCCCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)13)
            {
                checkCashCard();
            }
        
        }


        private void checkCashCard()
        {
          
            try
            {

                string ccCode = txtBoxCCCode.Text;

                if (ccCode.Length > 1)
                {

                    ccLists = gd.getCashCard(ccCode);


                    if (ccLists.Count > 0)
                    {
                        foreach (CashCard c in ccLists)
                        {
                            txtBoxCCBalanceAmt.Text = c.CCBalanceAmt.ToString();
                           

                            if (c.CCBalanceAmt < Int32.Parse(this.totalSalesAmount.ToString()))
                            {
                                MessageBox.Show("ไม่สามารถชำระเงินได้ ยอดเงืนในบัตรไม่เพียงพอ !! \n\r" + " ยอดเงินคงในบัตรเท่ากับ " + txtBoxCCBalanceAmt.Text + " บาท");
                                txtBoxCCCode.Text = "";
                                txtBoxCCCode.Focus();

                                txtBoxCCBalanceAmt.Text = "";

                            }
                            else
                            {
                                MessageBox.Show("ยอดเงินคงในบัตรเท่ากับ " + txtBoxCCBalanceAmt.Text + " บาท");
                                buttonCheckBill_Click(null, null);
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่มีข้อมูลบัตร กรุณาไปเค้าเตอร์เติมเงิน");
                        txtBoxCCCode.Text = "";
                        txtBoxCCCode.Focus();
                    }

                    // textBoxAmt.Focus(); 
                    // textBoxChange.Text = txtBoxCCBalanceAmt.Text; 


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }


        private void checkBoxMoveOrderbyItem_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMoveOrderbyItem.Checked == true)
                MoveOrderByItemPN.Visible = true;
            else
                MoveOrderByItemPN.Visible = false;

        }

        private void buttonVoidOrder_Click(object sender, EventArgs e)
        {

            try
            {

                Button bClick = (Button)this.senderVoidBill;

                string productID = bClick.Name;
                string productName = "";
                string orderBarcode = "";
                string orderRemark = "";
                string createDate = "";
                int delOrderlimit = 0;

                string[] txt = bClick.Text.Split('|');
                productName = txt[1];
                createDate = txt[2];
                orderBarcode = txt[3];
                delOrderlimit = Int32.Parse(txt[4]);
                orderRemark = txt[5];


                int delOrderCount = Int32.Parse(comboBoxVoidCount.Text);
                string reason = comboBoxVoidReason.Text;

                if (delOrderCount > delOrderlimit)
                {
                    comboBoxVoidCount.Text = delOrderlimit.ToString();
                    throw new Exception(" ลบรายการได้ไม่เกิน " + delOrderlimit.ToString());
                }


                if (reason == "ระบุเหตุผล")
                    throw new Exception("กรูณาระบุเหตุผล ลบ ORder");

                int result = 0;
                string remarkText = reason + '|' + orderRemark + '|' + delOrderCount.ToString();

                result = gd.delOrderByTable(this.tableID, Int32.Parse(productID), orderBarcode, remarkText);
                // ปรัยใน SQL ให้ลบ ที่ Remark ล่าสุด เหมือนรกับตัวลบ

                if (productName.Trim().Substring(0, 1) != "-")
                {
                    this.txtOrderAppToPrint = "[" + delOrderCount.ToString() + "] " + productName + "\n\r";

                    if (orderRemark.Trim().Length > 1)
                    {
                        string[] remarkString = orderRemark.Remove(0, 1).Split('+');

                        foreach (string r in remarkString)
                        {
                            this.txtOrderAppToPrint += "  +" + r.Split('{')[0] + "\r\n";
                        }
                    }

                    this.txtOrderAppToPrint += "\n\r" + "สั่งเวลา " + createDate.Substring(0, 17) + "\n\r\n";
                    this.txtOrderAppToPrint += "เหตุผลการลบ :\n\r" + reason;


                    MsgNotify.lineNotify("ลบโดย : " + Login.userName + "\n\r" + this.txtOrderAppToPrint);

                    if (result > 0)
                        SelectPrinterDel(result, posPrinters[0].PrinterName);


                    if (Int32.Parse(productID) > 10)
                        SelectPrinterDel(99, posPrinters[21].PrinterName);
                }



                panelVoidOrder.Visible = false;


                genOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void SelectPrinterDel(int printerNo,string printerName)
        {


            if (printerNo == 99)
            {
                strPrinterOrder = posPrinters[0].PrinterStrName;
                printOrderDel.PrinterSettings.PrinterName = posPrinters[0].PrinterName;
            }
            else
            {
                strPrinterOrder = posPrinters[printerNo].PrinterStrName;
                printOrderDel.PrinterSettings.PrinterName = posPrinters[printerNo].PrinterName;

            }
 

            if (printerNo > 0 ) //&& printOrderDel.PrinterSettings.PrinterName != printerName)
                printOrderDel.Print();
        }


        private void SelectPrinterMove(int printerNo, string printerName)
        {

            strPrinterOrder = posPrinters[printerNo].PrinterStrName;
            printMoveTableByOrder.PrinterSettings.PrinterName = posPrinters[printerNo].PrinterName;


            if (printerNo > 0) //&& printOrderDel.PrinterSettings.PrinterName != printerName)
                printMoveTableByOrder.Print();
        }


        private void checkBoxPickupItem_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPickupItem.Checked == true)
                PickupOrderByItemPN.Visible = true;
            else
                PickupOrderByItemPN.Visible = false;
        }

        private void buttonCloseVBill_Click(object sender, EventArgs e)
        {
            panelVoidOrder.Visible = false;
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //string srPID = textBoxSRProductID.Text;
                //string srPBC = textBoxSRBarcode.Text;
                string srPName = textBoxSRProductName.Text;



                //   this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductID like '*{0}*' and ProductImage like '*{1}*' and ProductName like '*{2}*' ", srPID, srPBC, srPName);

                if (srPName.Length > 0)
                {
                    this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductName like '*{0}*' ", srPName);

                }
                 

            }
            catch (Exception ex)
            {

            } 

        }

        private void comboBoxAllCat_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedCat = Int32.Parse(comboBoxAllCat.SelectedValue.ToString());
                dataAllProduct = gd.getAllProduct(selectedCat);
                dataGridViewAllProduct.DataSource = dataAllProduct;

            }
            catch (Exception ex)
            {

            }
        }

        private void onChangeAdditemDetail()
        {
            string productQTY = comboBoxAddUnitSr.Text;

            if (dataGridproductUnit.ToLower().Contains("kg"))  // Weight
                  textBoxAddItemDetail.Text = dataGridproductName + " (" + dataGridproductPrice + ") " + " > QTY : " + textBoxWeightSR.Text + " KG.";
             else 
                textBoxAddItemDetail.Text = dataGridproductName + " (" + dataGridproductPrice + ") " + " > QTY : " + comboBoxAddUnitSr.Text;
         
              
        }

        private void textBoxWeightSR_TextChanged(object sender, EventArgs e)
        {
            onChangeAdditemDetail();
        }

        private void comboBoxAddUnitSr_TextChanged(object sender, EventArgs e)
        {
            onChangeAdditemDetail();
        }

        private void dataGridViewAllProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                this.dataGridproductID = Int32.Parse(dataGridViewAllProduct.Rows[e.RowIndex].Cells["ProductID"].Value.ToString());
                this.dataGridproductName = dataGridViewAllProduct.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();
                this.dataGridproductPrice = dataGridViewAllProduct.Rows[e.RowIndex].Cells["ProductPrice"].Value.ToString();
                this.dataGridproductUnit = dataGridViewAllProduct.Rows[e.RowIndex].Cells["ProductUnit"].Value.ToString().ToLower();
                 
                onChangeAdditemDetail();
            }
            catch (Exception ex)
            {

            }
        }

        private void buttonClearOrderSr_Click_1(object sender, EventArgs e)
        {
            textBoxSRProductName.Text = "";
            textBoxSRProductName.Focus();
            comboBoxAllCat.SelectedIndex = 0;

        }

        private void comboBoxListCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    memIDSelected = Int32.Parse(comboBoxListCust.SelectedValue.ToString()); 
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void txttaxID_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (e.KeyChar == (char)13)
            //{

            //    TextBox tb = (TextBox)sender;

            //    foreach (Member m in allCustomers)
            //    {
            //        if (tb.Text == m.Password)
            //        {
            //            MessageBox.Show("พบลูกค้า :" + m.Name);
            //            comboBoxListCust.SelectedValue = m.UserID;
            //            tb.Text = "";
            //        }

            //    }

            //    if (tb.Text != "")
            //        MessageBox.Show("ไม่พบเบอร์ TAX ID ลูกค้า");

            //}
        }

        private void textTEL_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)13)
            //{

            //    TextBox tb = (TextBox)sender;

            //    foreach (Customer m in allCustomers)
            //    {
            //        if (tb.Text == m.Tel)
            //        {
            //            MessageBox.Show("พบลูกค้า :" + m.Name);
            //            comboBoxListCust.SelectedValue = m.UserID;
            //            tb.Text = "";
            //        }

            //    }

            //    if (tb.Text != "")
            //        MessageBox.Show("ไม่พบเบอร์โทร. ลูกค้า");

            //}
        }

        private void buttonAddMember_Click(object sender, EventArgs e)
        {
            LinkFormAddCustomer();

            // List Cust
          //  allCusts = gd.getListAllCust();
            allCustomers = gd.getListAllCustomer();
            getComboAllCust();

            dataTableAllCust = gd.getDataAllCustomer();
            dataGridViewAllMember.DataSource = dataTableAllCust;
        }

        private void LinkFormAddCustomer()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddCustomer == null)
            {
                formAddCustomer = new AddCustomer(this, 0);
            }
            //  Cursor.Current = Cursors.Default;
            if (formAddCustomer.ShowDialog() == DialogResult.OK)
            {
                formAddCustomer.Dispose();
                formAddCustomer = null;
            }
        }

        private void textTEL_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttaxID_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonMemBerPayByCC_Click(object sender, EventArgs e)
        {


            panelFinance1.Visible = true;
            panelFinance2.Visible = true;

            txtBoxCCCode.Text = textBoxMemCardID.Text;
            checkCashCard();

        }

        private void buttonLinktoMainCashCard_Click(object sender, EventArgs e)
        {
            if (this.GetType() != typeof(MainCashCard))
                LinkFormMainCashCard();
        }


        private void LinkFormMainCashCard()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formMainCashCard == null)
            {
                formMainCashCard = new MainCashCard(this, 0);
            }
            Cursor.Current = Cursors.Default;
            if (formMainCashCard.ShowDialog() == DialogResult.OK)
            {
                formMainCashCard.Dispose();
                formMainCashCard = null;
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            LinkFormAddMemCard();
        }

        private void LinkFormAddMemCard()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddMemCard == null)
            {
                formAddMemCard = new AddMemCard(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddMemCard.ShowDialog() == DialogResult.OK)
            {
                formAddMemCard.Dispose();
                formAddMemCard = null;
            }
        }

        private void buttonCheckMember_Click(object sender, EventArgs e)
        {
            LinkFormAddMemCardRenew();
        }

        private void LinkFormAddMemCardRenew()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddMemCardRenew == null)
            {
                formAddMemCardRenew = new AddMemCardRenew(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddMemCardRenew.ShowDialog() == DialogResult.OK)
            {
                formAddMemCardRenew.Dispose();
                formAddMemCardRenew = null;
            }
        }

       // Prom Before

        private void buttonBFClosePanel_Click(object sender, EventArgs e)
        {
            panelPromBefore.Visible = false;
        } 


        int ProPageMaxBF = 0;
        int ProPageBF = 1;

        int productIDBF = 0;
        int promIDBF = 0;

        string promNameBF = "";
        int promtotalSeq = 0;
        int promSeq = 1;

        int progroupIDBF = 0;
        string proSetPriceGroup = "";

        float priceperitemBF = 0;

         

        private void promproductBF()
        {
            panelPromBefore.Visible = true; 
            this.promSeq = 1;

            this.ProPageMaxBF = 0;
            this.ProPageBF = 1; 

             List<Prom> pm = gd.getListProm(this.promIDBF);

             promNameBF = "";
             promtotalSeq = 0;

             foreach (Prom p in pm)
             {
                 promNameBF = p.PromName;
                 promtotalSeq = p.PromCountItem; 
             }


             textBoxProNameBF.Text = this.promNameBF;
             labelProCFTotalSeq.Text = "@" + this.promtotalSeq.ToString();




             promproductDetailBF(this.promIDBF);

        }


        private void promproductDetailBF(int promIDBF )
        {
              
            string progroupNameBF = "";
            progroupIDBF = 0;
            proSetPriceGroup = "";
            float proSetPrice = 0;
            float proDiscountPeritem = 0;
            float proDiscountAmtitem = 0;

           


            List<PromDetail> pd = gd.getPromDetail(promIDBF, this.promSeq);

            foreach (PromDetail p in pd)
            {
                progroupNameBF = p.ProductGroupName;
                this.progroupIDBF = p.ProductGroupID;
                proSetPriceGroup = p.SetPriceGroup;

                // --- Price ----/
                proSetPrice = p.SetPrice;
                proDiscountPeritem = p.DiscountPeritem;
                proDiscountAmtitem = p.DiscountAmtitem;
            }

            //MessageBox.Show(this.promSeq.ToString());
            //MessageBox.Show(this.progroupIDBF.ToString());


            textBoxPDSegNo.Text = this.promSeq.ToString() + "/" + this.promtotalSeq.ToString();
            labelBFProductGroup.Text = progroupNameBF;
            textBoxBFPriceType.Text = proSetPriceGroup;

            if (proSetPriceGroup == "NormalPrice")
            {
                textBoxProAmt.Text = "ราคาสินค้าปกติ";
            }
            else if (proSetPriceGroup == "SetPrice")
            {
                textBoxProAmt.Text = "ราคาชิ้นนี้ " + proSetPrice.ToString();
                priceperitemBF = proSetPrice;
            }
            else if (proSetPriceGroup == "DiscountPeritem")
            {
                textBoxProAmt.Text = "ส่วนลดชิ้นนี้ " + proDiscountPeritem.ToString() + "%";
                priceperitemBF = proDiscountPeritem;

                if (proDiscountPeritem == 100)
                    textBoxProAmt.Text += " (ฟรี)";
            }
            else if (proSetPriceGroup == "DiscountAmtitem")
            {
                textBoxProAmt.Text = "ส่วนลดชิ้นนี้ " + proDiscountAmtitem.ToString();
                priceperitemBF = proDiscountAmtitem;
            }

            // --- Pro list product



            if (imgPath.Length == 0)
                genObjOrderProductBF(progroupIDBF);
            else
                genObjOrderProductBFIMG(progroupIDBF);


        }     




        private void genObjOrderProductBF(int progroupID)
        {


            try
            {

                int productID;
                string productName;
                float productPrice;
                string productFlagUse;
                string productDesc;
                string[] productcolor;
                string[] productColorBG;
                string productColorTxt;
                string productcolorFull;

                Button bCat; 

                int sizeX = 125;
                int sizeY = 116; 
                int yy = 4;

                int i = 0;


                List<Product> productByCat = gd.getProductGroup(progroupID, "");

                 

                //CatPage = 1;
                if (productByCat.Count % 16 == 0)
                    this.ProPageMaxBF = (productByCat.Count / 16);
                else
                    this.ProPageMaxBF = (productByCat.Count / 16) + 1;

                int indexstart = 0;
                int indexEnd = 0;



                if (this.ProPageMaxBF == 1 )
                {
                    indexstart = 1;
                    indexEnd = 16;
                    buttonBFPrev.Visible = false;
                    buttonBFNext.Visible = false;
                }
                else
                {
                    indexstart = (this.ProPageBF - 1) * 16 + 1;
                    indexEnd = (this.ProPageBF - 1) * 16 + 16;
                    buttonBFPrev.Visible = true;
                    buttonBFNext.Visible = true;
                }

                if (this.ProPageBF == 1)
                {
                    buttonBFPrev.Visible = false;
                }
                else if (this.ProPageBF == this.ProPageMaxBF)
                {
                    buttonBFNext.Visible = false;
                }

                if (this.ProPageMaxBF == 1)
                {
                    buttonBFPrev.Visible = false;
                    buttonBFNext.Visible = false;
                }


                int y = 1;

                //   productSyntaxLists = new List<Product>();
                OrderProductBFPN.Controls.Clear();

                foreach (Product t in productByCat)
                {
                    productID = t.ProductID;
                    productName = t.ProductName;

                    productPrice = 0;

                    if ( tableZoneVAT == "ZERO")
                        productPrice = 0;
                    //else if (this.memCardID.Length > 2) 
                    //    productPrice = t.ProductCost; 
                    else if (tableZonePriceID == 1)
                        productPrice = t.ProductPrice;
                    else if (tableZonePriceID == 2)
                        productPrice = t.ProductPrice2;
                    else if (tableZonePriceID == 3)
                        productPrice = t.ProductPrice3;
                    else if (tableZonePriceID == 4)
                        productPrice = t.ProductPrice4;
                    else if (tableZonePriceID == 5)
                        productPrice = t.ProductPrice5;
                     


                    productFlagUse = t.ProductFlagUse;
                    productDesc = t.ProductDesc;
                    productcolorFull = t.ProductColour;



                    if (this.flagLangProduct == "EN")
                        productName = t.ProductNameEN;
                    if (this.flagLangProduct == "TH")
                        productName = t.ProductName;
                    else if (this.flagLangProduct == "OT")
                        productName = t.ProductDesc;

                     

                    if (productFlagUse.ToLower() == "y")
                    {

                        if (y >= indexstart && y <= indexEnd)
                        {

                            bCat = new Button();
                            

                            bCat.Cursor = System.Windows.Forms.Cursors.Default;
                            bCat.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            bCat.Font = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                            bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                            bCat.Name = productID.ToString();
                            bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                            bCat.TabIndex = 2;

                            
 
                            bCat.Text = productName + " (" + productPrice.ToString() + ")";


                            if (FuncString.Right(productcolorFull, 2).ToLower() == "|b" || FuncString.Right(productcolorFull, 2).ToLower() == "|w")
                            {

                                productcolor = productcolorFull.Split('|');
                                productColorBG = productcolor[0].Split(',');
                                productColorTxt = productcolor[1];

                                if (productColorTxt.ToLower() == "b")
                                {
                                    bCat.ForeColor = System.Drawing.Color.Black;
                                }
                                else
                                {
                                    bCat.ForeColor = System.Drawing.Color.White;
                                }
                                bCat.BackColor = Color.FromArgb(Int32.Parse(productColorBG[0]), Int32.Parse(productColorBG[1]), Int32.Parse(productColorBG[2]));

                            }
                            else
                            {
                                bCat.BackColor = Color.White;
                                bCat.ForeColor = System.Drawing.Color.Black;
                            }
                                 
                           

                            bCat.Click += new System.EventHandler(this.bProductProm_Click);

                            OrderProductBFPN.Controls.Add(bCat);
                            i++;
                        }

                        y++;

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        
        }
        private void genObjOrderProductBFIMG(int progroupID)
        {


            try
            {

                int productID;
                string productName;
                float productPrice;
                string productFlagUse;
                string productDesc;
                string[] productcolor;
                string[] productColorBG;
                string productColorTxt;
                string productcolorFull;

                Button bCat;
                Button bText;

                int sizeX = 125;
                int sizeY = 155;
                int sizeYText = 45;
                int yy = 4;

                int i = 0;


                List<Product> productByCat = gd.getProductGroup(progroupID, "");



                //  MessageBox.Show("xx");

                //   MessageBox.Show(productByCat.Count.ToString());

                //CatPage = 1;
                if (productByCat.Count % 12 == 0)
                    this.ProPageMaxBF = (productByCat.Count / 12);
                else
                    this.ProPageMaxBF = (productByCat.Count / 12) + 1;

                int indexstart = 0;
                int indexEnd = 0;



                if (this.ProPageMaxBF == 1)
                {
                    indexstart = 1;
                    indexEnd = 12;
                    buttonBFPrev.Visible = false;
                    buttonBFNext.Visible = false;
                }
                else
                {
                    indexstart = (this.ProPageBF - 1) * 12 + 1;
                    indexEnd = (this.ProPageBF - 1) * 12 + 12;
                    buttonBFPrev.Visible = true;
                    buttonBFNext.Visible = true;
                }

                if (this.ProPageBF == 1)
                {
                    buttonBFPrev.Visible = false;
                }
                else if (this.ProPageBF == this.ProPageMaxBF)
                {
                    buttonBFNext.Visible = false;
                }

                if (this.ProPageMaxBF == 1)
                {
                    buttonBFPrev.Visible = false;
                    buttonBFNext.Visible = false;
                }


                int y = 1;

                //   productSyntaxLists = new List<Product>();
                OrderProductBFPN.Controls.Clear();



                foreach (Product t in productByCat)
                {
                    productID = t.ProductID;
                    productName = t.ProductName;

                    productPrice = 0;

                    if (tableZoneVAT == "ZERO")
                        productPrice = 0;
                    //else if (this.memCardID.Length > 2) 
                    //    productPrice = t.ProductCost; 
                    else if (tableZonePriceID == 1)
                        productPrice = t.ProductPrice;
                    else if (tableZonePriceID == 2)
                        productPrice = t.ProductPrice2;
                    else if (tableZonePriceID == 3)
                        productPrice = t.ProductPrice3;
                    else if (tableZonePriceID == 4)
                        productPrice = t.ProductPrice4;
                    else if (tableZonePriceID == 5)
                        productPrice = t.ProductPrice5;

                     

                    productFlagUse = t.ProductFlagUse;
                    productDesc = t.ProductDesc;
                    productcolorFull = t.ProductUnit;



                    if (this.flagLangProduct == "EN")
                        productName = t.ProductNameEN;
                    if (this.flagLangProduct == "TH")
                        productName = t.ProductName;
                    else if (this.flagLangProduct == "OT")
                        productName = t.ProductDesc;



                    if (productFlagUse.ToLower() == "y")
                    {
                        bCat = new Button();
                        bText = new Button();
                         

                        if (y >= indexstart && y <= indexEnd)
                        {

                            bCat.Cursor = System.Windows.Forms.Cursors.Default;
                            bCat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                            bCat.Font = new System.Drawing.Font("Century Gothic", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                            bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                            bCat.Name = productID.ToString();
                            bCat.Size = new System.Drawing.Size(sizeX, sizeY - sizeYText);
                            bCat.TabIndex = 2;

                            bText.FlatStyle = FlatStyle.Flat;
                            bText.Font = new System.Drawing.Font("Century Gothic", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                            bText.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY - sizeYText) + ((sizeY) * (i / yy)));
                            bText.Name = productID.ToString();
                            bText.Size = new System.Drawing.Size(sizeX, sizeYText);
                            bText.BackColor = Color.White;
                            bText.ForeColor = Color.Black;




                            //flagGetImg = 0;
                            imgProduct = getImageFromURL(this.imgPath + productID.ToString() + ".jpg");



                            if (flagGetImg == 0)
                            {
                                bCat.Text = productName + " (" + productPrice.ToString() + ")";
                                bText.Text = "";

                                if (FuncString.Right(productcolorFull, 2).ToLower() == "|b" || FuncString.Right(productcolorFull, 2).ToLower() == "|w")
                                {

                                    productcolor = productcolorFull.Split('|');
                                    productColorBG = productcolor[0].Split(',');
                                    productColorTxt = productcolor[1];

                                    if (productColorTxt.ToLower() == "b")
                                    {
                                        bCat.ForeColor = System.Drawing.Color.Black;
                                    }
                                    else
                                    {
                                        bCat.ForeColor = System.Drawing.Color.White;
                                    }
                                    bCat.BackColor = Color.FromArgb(Int32.Parse(productColorBG[0]), Int32.Parse(productColorBG[1]), Int32.Parse(productColorBG[2]));

                                }
                                else
                                {
                                    bCat.BackColor = Color.White;
                                    bCat.ForeColor = System.Drawing.Color.Black;
                                }

                                //  bCat.ForeColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                bCat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                                bCat.Font = new System.Drawing.Font("Century Gothic", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                                bCat.TextAlign = ContentAlignment.MiddleRight;
                                //  bCat.Text = "\n\n\n" + productPrice.ToString() + ".-";
                                bCat.ForeColor = System.Drawing.Color.Black;

                                bCat.BackgroundImage = imgProduct;
                                bCat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                                bCat.UseVisualStyleBackColor = false;

                                bText.Text = productName + " (" + productPrice.ToString() + ")";
                            }


                            if (productFlagUse.ToLower() == "e")
                            {

                                bText.Font = new System.Drawing.Font("Century Gothic", 10, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                                bText.BackColor = Color.Red;

                                bCat.Enabled = false;
                            }

                              

                            bCat.Click += new System.EventHandler(this.bProductProm_Click);

                            OrderProductBFPN.Controls.Add(bCat);
                            OrderProductBFPN.Controls.Add(bText);
                            i++;
                        }

                        y++;


                    }
                }

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);

            }


        }


        private void bProductProm_Click(object sender, EventArgs e)
        {

            try
            {

                Button bClick = (Button)sender;

                string productID = bClick.Name;
                string productSyntax = "";

                float amt = 0;
                int result = 0;

                float productprice = 0;

                this.productActionType = 0;

                foreach (Product p in productlists)
                {
                    if (Int32.Parse(productID) == p.ProductID)
                    {
                      
                        if (tableZoneVAT == "ZERO")
                            productprice = 0;
                        //else if (this.memCardID.Length > 2)
                        //    productprice = p.ProductCost;
                        else if (tableZonePriceID == 1)
                            productprice = p.ProductPrice;
                        else if (tableZonePriceID == 2)
                            productprice = p.ProductPrice2;
                        else if (tableZonePriceID == 3)
                            productprice = p.ProductPrice3;
                        else if (tableZonePriceID == 4)
                            productprice = p.ProductPrice4;
                        else if (tableZonePriceID == 5)
                            productprice = p.ProductPrice5;

                        if (p.ProductUnit.ToLower().Contains("kg"))
                        {
                            this.productActionType = 4;
                        }

                        productIDAct = p.ProductID;
                        productNameAct = p.ProductName;

                        //if (Login.selecttablezoneID == 99)
                        //    productPriceAct = p.ProductPrice2;
                        //else if (Login.selecttablezoneID == 100)
                        //    productPriceAct = p.ProductPrice3;
                        //else
                        //    productPriceAct = p.ProductPrice;

                        if (tableZoneVAT == "ZERO")
                            productPriceAct = 0;
                        //else if (this.memCardID.Length > 2)
                        //    productPriceAct = p.ProductCost;
                        else if (tableZonePriceID == 1)
                            productPriceAct = p.ProductPrice;
                        else if (tableZonePriceID == 2)
                            productPriceAct = p.ProductPrice2;
                        else if (tableZonePriceID == 3)
                            productPriceAct = p.ProductPrice3;
                        else if (tableZonePriceID == 4)
                            productPriceAct = p.ProductPrice4;
                        else if (tableZonePriceID == 5)
                            productPriceAct = p.ProductPrice5;



                    }

                }
                 

                if (textBoxBFPriceType.Text == "SetPrice")
                {
                    amt = priceperitemBF - productprice;
                    productPriceAct = priceperitemBF;
                }
                else if (textBoxBFPriceType.Text == "DiscountPeritem")
                {
                    amt = (int)(productprice * (priceperitemBF / 100)) * -1;
                    productPriceAct = productprice * (1 - priceperitemBF / 100);

                }
                else if (textBoxBFPriceType.Text == "DiscountAmtitem")
                {
                    amt = priceperitemBF * -1;
                    productPriceAct = productprice - priceperitemBF;
                }


                if (this.productActionType > 0)
                {
                    this.ActionforProduct();
                }
                else
                {

                    result = gd.instOrderByTable_Prom(this.tableID, Int32.Parse(productID), this.memID, "c", amt, this.productIDBF, this.progroupIDBF, this.promSeq);


                    if (result < 100)
                    {
                        /// Next Sequence
                        this.promSeq++;
                        promproductDetailBF(this.promIDBF);
                    }
                    else
                    {
                        /// Finish

                        //   MessageBox.Show(" Add Product Promotion Success " + textBoxProNameBF.Text); 
                        panelPromBefore.Visible = false;

                        genOrder();
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

          
        private void buttonBFPrev_Click(object sender, EventArgs e)
        {
            this.ProPageBF--;
            OrderProductBFPN.Controls.Clear();


            if (imgPath.Length == 0)
                genObjOrderProductBF(progroupIDBF);
            else
                genObjOrderProductBFIMG(progroupIDBF);

        }

        private void buttonBFNext_Click(object sender, EventArgs e)
        {
            this.ProPageBF++;
            OrderProductBFPN.Controls.Clear();

            if (imgPath.Length == 0)
                genObjOrderProductBF(progroupIDBF);
            else
                genObjOrderProductBFIMG(progroupIDBF);


        }

        private void buttonSelPrice1_Click(object sender, EventArgs e)
        {
            textBoxSelInputPrice.Text = buttonSelPrice1.Text;
            textBoxSelInputPrice.Focus();
        }

        private void buttonSelPrice2_Click(object sender, EventArgs e)
        {
            textBoxSelInputPrice.Text = buttonSelPrice2.Text;
            textBoxSelInputPrice.Focus();
        }

        private void buttonSelPrice3_Click(object sender, EventArgs e)
        {
            textBoxSelInputPrice.Text = buttonSelPrice3.Text;
            textBoxSelInputPrice.Focus();
        }

        private void textBoxSelPriceNo_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {

                if (e.KeyChar == (char)13)
                {

                    int selectNoP = Int32.Parse(textBoxSelPriceNo.Text);
                    int productIDBC = Int32.Parse(labelProductSelID.Text);
                    int qtyAdditems = Int32.Parse(textBoxProductQTYBC.Text);
                    string remarkBC = "";

                    float normalprice = float.Parse(buttonSelPrice3.Text);
                    float memprice = float.Parse(buttonSelPrice2.Text);
                    float lowprice = float.Parse(buttonSelPrice1.Text);

                    float amtOrder = 0;

                    if (selectNoP == 3)
                        amtOrder = normalprice - lowprice;
                    else if (selectNoP == 2)
                        amtOrder = normalprice - memprice;

                    remarkBC = "+#SRC : " + textBoxSERIAL.Text;

                    if ((selectNoP >= 1 && selectNoP <= 3))
                    {

                        int result = 0;

                        result = this.fn_AddOrderBarCode(productIDBC, qtyAdditems, remarkBC, amtOrder);

                        if (result <= 0)
                            throw new Exception("Error Insert Order Search Item");


                        textBoxSelPriceNo.Text = "";
                        textBoxSERIAL.Text = "";
                        panelSelPrice.Visible = false;

                        clearBarcodePanel();
                        genOrder();

                    }
                    else
                    {
                        MessageBox.Show("ป้อน Short Cut ราคาไม่ถูกต้อง");
                    }

                }


            }
            catch (Exception ex)
            {


            }

        }

        private void buttonAddOrderManualPrice_Click(object sender, EventArgs e)
        {
            AddORderfromSelPrice();
        }


        private void AddORderfromSelPrice()
        {

            try
            {

                // int selectNoP = Int32.Parse(textBoxSelPriceNo.Text);
                int productIDBC = Int32.Parse(labelProductSelID.Text);
                int qtyAdditems = Int32.Parse(textBoxProductQTYBC.Text);
                string remarkBC = "";

                float normalprice = float.Parse(buttonSelPrice1.Text);
                float memprice = float.Parse(buttonSelPrice2.Text);
                float lowprice = float.Parse(buttonSelPrice3.Text);
                float selPrice = float.Parse(textBoxSelInputPrice.Text);

                float amtOrder = 0;

                amtOrder = selPrice - normalprice;

                int result = 0;

                remarkBC = "+#SRC : " + textBoxSERIAL.Text;


                result = this.fn_AddOrderBarCode(productIDBC, qtyAdditems, remarkBC, amtOrder);

                if (result <= 0)
                    throw new Exception("Error Insert Order Search Item");


                textBoxSelPriceNo.Text = "";
                textBoxSelInputPrice.Text = "0";
                textBoxSERIAL.Text = "";
                panelSelPrice.Visible = false;

                clearBarcodePanel();
                genOrder();
            }
            catch (Exception ex)
            {


            }


        }

        private void textBoxSERIAL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                textBoxSelPriceNo.Focus();
            }
        }

        // Image
       
        private void genObjOrderProductImg(int catID)
        {

            try
            {


                int productID;
                string productName;
                float productPrice;
                string productFlagUse;
                string productDesc;
                string[] productcolor;
                string[] productColorBG;
                string productColorTxt;
                string productcolorFull;


                string productImage;

                Button bCat;
                Button bText;

                int sizeX = 166;
                int sizeY = 135;
                int sizeYText = 45;
                int yy = 3;

                int i = 0;
                this.productByCat = gd.getProductByCat(catID, Login.selecttablezoneID, this.memIDSelected);


                //CatPage = 1;
                if (productByCat.Count % 9 == 0)
                    this.ProPageMax = (productByCat.Count / 9);
                else
                    this.ProPageMax = (productByCat.Count / 9) + 1;

                int indexstart = 0;
                int indexEnd = 0;


                if (this.ProPageMax == 1)
                {
                    indexstart = 1;
                    indexEnd = 9;
                    buttonProPre.Visible = false;
                    buttonPorNext.Visible = false;
                }
                else
                {
                    indexstart = (this.ProPage - 1) * 9 + 1;
                    indexEnd = (this.ProPage - 1) * 9 + 9;

                    buttonProPre.Visible = true;
                    buttonPorNext.Visible = true;
                }

                if (this.ProPage == 1)
                {
                    buttonProPre.Visible = false;
                }
                else if (this.ProPage == this.ProPageMax)
                {
                    buttonPorNext.Visible = false;
                }

                if (this.ProPageMax == 1)
                {
                    buttonProPre.Visible = false;
                    buttonPorNext.Visible = false;
                }


                int y = 1;

                productSyntaxLists = new List<Product>();

                ContextMenu cm;

                foreach (Product t in productByCat)
                {
                    productID = t.ProductID;
                    productName = t.ProductName;
                    productPrice = t.ProductPrice;
                    productFlagUse = t.ProductFlagUse;
                    productDesc = t.ProductDesc;
                    productcolorFull = t.ProductUnit;

                    productImage = t.ProductBarcode;



                    if (this.flagLangProduct == "EN")
                        productName = t.ProductNameEN;
                    if (this.flagLangProduct == "TH")
                        productName = t.ProductName; 

                    //  Syntax
                    if (productDesc.Length > 4)
                        if (productDesc.Substring(0, 4) == "FOR:")
                            productSyntaxLists.Add(new Product(productID, productDesc));




                    if (productFlagUse.ToLower() == "y" || productDesc.ToLower() == "e")
                    {

                        bCat = new Button();
                        bText = new Button();


                        cm = new ContextMenu();

                        cm.MenuItems.Add("Product Empty" + "| " + productID.ToString(), new EventHandler(PEmpty_Click));
                        cm.MenuItems.Add("Product Use" + "| " + productID.ToString(), new EventHandler(PUse_Click));

                        if (y >= indexstart && y <= indexEnd)
                        {

                            bCat.Cursor = System.Windows.Forms.Cursors.Default;
                            bCat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                            bCat.Font = new System.Drawing.Font("Century Gothic", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                            bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                            bCat.Name = productID.ToString();
                            bCat.Size = new System.Drawing.Size(sizeX, sizeY - sizeYText);
                            bCat.TabIndex = 2;

                            bText.FlatStyle = FlatStyle.Flat;
                            bText.Font = new System.Drawing.Font("Century Gothic", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                            bText.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY - sizeYText) + ((sizeY) * (i / yy)));
                            bText.Name = productID.ToString();
                            bText.Size = new System.Drawing.Size(sizeX, sizeYText);
                            bText.BackColor = Color.White;
                            bText.ForeColor = Color.Black;
                          
                             


                            //flagGetImg = 0;
                            imgProduct = getImageFromURL(this.imgPath + productID.ToString() + ".jpg");



                            if (flagGetImg == 0)
                            {
                                bCat.Text = productName + " (" + productPrice.ToString() + ")";
                                bText.Text = "";

                                if (FuncString.Right(productcolorFull, 2).ToLower() == "|b" || FuncString.Right(productcolorFull, 2).ToLower() == "|w")
                                {

                                    productcolor = productcolorFull.Split('|');
                                    productColorBG = productcolor[0].Split(',');
                                    productColorTxt = productcolor[1];

                                    if (productColorTxt.ToLower() == "b")
                                    {
                                        bCat.ForeColor = System.Drawing.Color.Black;
                                    }
                                    else
                                    {
                                        bCat.ForeColor = System.Drawing.Color.White;
                                    }
                                    bCat.BackColor = Color.FromArgb(Int32.Parse(productColorBG[0]), Int32.Parse(productColorBG[1]), Int32.Parse(productColorBG[2]));

                                }
                                else
                                {
                                    bCat.BackColor = Color.White;
                                    bCat.ForeColor = System.Drawing.Color.Black;
                                }

                                //  bCat.ForeColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                bCat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                                bCat.Font = new System.Drawing.Font("Century Gothic", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                                bCat.TextAlign = ContentAlignment.MiddleRight;
                                //  bCat.Text = "\n\n\n" + productPrice.ToString() + ".-";
                                bCat.ForeColor = System.Drawing.Color.Black;

                                bCat.BackgroundImage = imgProduct;
                                bCat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                                bCat.UseVisualStyleBackColor = false;

                                bText.Text = productName + " (" + productPrice.ToString() + ")";
                            }


                            if (productFlagUse.ToLower() == "e")
                            {

                                bText.Font = new System.Drawing.Font("Century Gothic", 10, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                                bText.BackColor = Color.Red;

                                bCat.Enabled = false;
                            }
                            

                            bCat.ContextMenu = cm;
                            bText.ContextMenu = cm;

                             

                            bCat.Click += new System.EventHandler(this.bProduct_Click); 

                            OrderProductPN.Controls.Add(bCat);
                            OrderProductPN.Controls.Add(bText);
                            i++;
                        }

                        y++;


                    }
                }

            }
            catch (Exception ex)
            {
            }

        }
        

        protected Image getImageFromURL(string URL)
        {

            Image img = null;
            WebClient wc = new WebClient();


            //byte[] bytes1 = wc.DownloadData(imgPath + "/0.jpg");
            //MemoryStream mss = new MemoryStream(bytes1);
            //img = System.Drawing.Image.FromStream(mss);

            img = imgDefault;

            try
            {

                byte[] bytes = wc.DownloadData(URL);
                MemoryStream ms = new MemoryStream(bytes);
                img = System.Drawing.Image.FromStream(ms);
                flagGetImg = 1;

            }
            catch (Exception ex)
            {
                flagGetImg = 0;
            }
             
            return img;

        }

        private void buttonClosePanelViewMaterial_Click(object sender, EventArgs e)
        {
            panelViewMaterial.Visible = false;
        }

        private void buttonViewMaeterial_Export_Click(object sender, EventArgs e)
        {
            try
            {
                ExportData.Excel_FromDataTable_DT(dataGridViewViewMaterial);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void buttonCloseBC_Click(object sender, EventArgs e)
        {
            panelShowActWeight.Visible = false;
        }

        private void txtBoxActProductWeight_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (FuncString.IsNumeric(txtBoxActProductWeight.Text))
                {
                    if(radioButtonOrderGram.Checked)
                        txtBoxActProductWeightAmt.Text = (float.Parse(txtBoxActProductWeight.Text) / (float)1000.00 *  float.Parse(txtBoxActProductPrice.Text)).ToString("###,###.#0"); 
                    else
                        txtBoxActProductWeightAmt.Text = (float.Parse(txtBoxActProductWeight.Text) * float.Parse(txtBoxActProductPrice.Text)).ToString("###,###.#0");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
            
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            txtBoxActProductWeight.Text = "";
        }

        private void buttonOK_Click(object sender, EventArgs e)
        { 
            fnAddWeightItemFromAct();
            
        }

        int flagAppendRemark = 0;

        private void fnAddWeightItemFromAct()
        {

            try
            {
                int productID = 0;
                int qtyAdditems = 0;

                string remarkBC = "";
                string productItemWBC = "";

                int result = 0;
                float amtOrder = 0;

                productID = this.productIDAct;
                qtyAdditems = 1;

                if (radioButtonOrderGram.Checked)
                    productItemWBC = ( float.Parse(txtBoxActProductWeight.Text) / (float)1000 ).ToString();
                else
                    productItemWBC = (float.Parse(txtBoxActProductWeight.Text) * (float)1).ToString();

                remarkBC = "@W=" + productItemWBC + " KG.";

                if (txtBoxActProductWeightAmt.Text.Length == 0)
                    amtOrder = 0;
                else
                    amtOrder = float.Parse(txtBoxActProductWeightAmt.Text.ToString());


                if (this.productActionType == 4)
                {
                    result = gd.instOrderByTable_Prom(this.tableID, productID, this.memID, remarkBC, amtOrder, this.productIDBF, this.progroupIDBF, this.promSeq);

                    if (result < 100)
                    {
                        /// Next Sequence
                        this.promSeq++;
                        promproductDetailBF(this.promIDBF);
                    }
                    else
                    {
                        /// Finish

                        //   MessageBox.Show(" Add Product Promotion Success " + textBoxProNameBF.Text); 
                        panelPromBefore.Visible = false;

                        genOrder();
                    }

                    panelShowActWeight.Visible = false;
                    txtBoxActProductWeight.Text = "0";
                }
                else
                {

                    result = this.fn_AddOrderBarCode(productID, qtyAdditems, remarkBC, amtOrder);

                    panelShowActWeight.Visible = false;
                    txtBoxActProductWeight.Text = "0";

                    if (this.productActionType == 1)
                    {
                        flagAppendRemark = 1;

                        panelBill.Visible = true;
                        AddRemarkOrderPN.Visible = true;
                        buttonViewBill.Text = "Hide เหตุผล";
                        textBoxReason.Focus();
                        textBoxReason.Text = "";
                        textBoxAdjustPriceOrder.Text = "0";

                        this.catIDSectedRemark = this.productRemAct;
                        OrderProductRemarkPN.Controls.Clear();
                        genObjOrderProductRemark();
                    }

                    genOrder();

                    if (result < 0)
                    {
                        throw new Exception(result.ToString());
                    }
                    else if (result > 1000)
                    { // Pro BEFORE
                        // BEFORE

                        this.productIDBF = productID;
                        this.promIDBF = result - 1000;
                        this.promproductBF();
                    }

                }

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

        }

        private void txtBoxActProductWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                fnAddWeightItemFromAct();
            } 
        }

        private void radioBoxWaitPay_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxServiceCharge.Checked = false;
            textBoxDiscountAmt.Text = orderAmount.ToString();
            genOrder();
        }

        private void printMoveTableByOrder_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

                int x = 0;
                int y = 0;

                Brush brush = new SolidBrush(Color.Black);
                Font fontHeader = new Font("Tahoma", 15);
                Font fontTable = new Font("Tahoma", 17);
                Font fontSubHeader = new Font("Tahoma", 10);
                Font fontBody = new Font("Tahoma", 12);

                y += 10;

                e.Graphics.DrawString("[[ ย้ายโต๊ะใหม่ ]]", fontTable, brush, x + 5, y);

                y += 30;

                e.Graphics.DrawString("ย้ายรายการ โดย : " + Login.userName + ")", fontBody, brush, x + 5, y);

                y += 20;

                e.Graphics.DrawString("จากโต๊ะ : " + this.txtTableName.Text + "ไปยัง : " + comboBoxListAllTable.Text, fontBody, brush, x + 5, y);

                y += 20;

                e.Graphics.DrawString("ไปยัง : " + comboBoxListAllTable.Text, fontBody, brush, x + 5, y);

                y += 20;

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd/MM/yyyy}", dt);
                string strTime = String.Format("{0:HH:mm:ss}", dt);

                e.Graphics.DrawString("Date " + strDate + "   Time  " + strTime, fontBody, brush, x + 5, y);

                y += 25;

                e.Graphics.DrawString(this.txtOrderAppToPrint, fontBody, brush, x, y);

                e.HasMorePages = false;

                //System.Threading.Thread.Sleep(500);


            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
            finally
            {
                // genOrder();
            }
        }

        private void buttonResendOrder_Click(object sender, EventArgs e)
        {
            try
            {

                Button bClick = (Button)this.senderVoidBill;

                string productID = bClick.Name;
                string productName = ""; 


                string createDate = "";

                string[] txt = bClick.Text.Split('|');
                productName = txt[1];
                createDate = txt[2];
                orderBarcodePrint = txt[3];


                string reason = comboBoxVoidReason.Text;

                int result = gd.delOrderByTable(this.tableID, Int32.Parse(productID), orderBarcodePrint, reason + "[RS]");


                if (productName.Trim().Substring(0, 1) != "-")
                {
                    this.txtOrderAppToPrint = "1." + productName + "\n\r";
                    this.txtOrderAppToPrint += "  สั่งเวลา " + "\n\r";
                    this.txtOrderAppToPrint += " " + createDate.Substring(0, 17) + "\n\r\n";
                    this.txtOrderAppToPrint += "  ส่งซ้ำ :\n\r" + reason;

                    flagResendORder = 1;
                    if (result > 0)
                        SelectPrinterDel(result, posPrinters[0].PrinterName);

                    flagResendORder = 0;
                }


                panelVoidOrder.Visible = false; 

               // genOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Resend Order");
            }
        }

        private void buttonResendOrderLocal_Click(object sender, EventArgs e)
        {
            try
            {

                Button bClick = (Button)this.senderVoidBill;

                string productID = bClick.Name;
                string productName = ""; 


                string createDate = "";

                string[] txt = bClick.Text.Split('|');
                productName = txt[1];
                createDate = txt[2];
                orderBarcodePrint = txt[3];


                string reason = comboBoxVoidReason.Text;

             //   int result = gd.delOrderByTable(this.tableID, Int32.Parse(productID), createDate, reason + "[RS]");


                if (productName.Trim().Substring(0, 1) != "-")
                {
                    this.txtOrderAppToPrint = "1." + productName + "\n\r";
                    this.txtOrderAppToPrint += "  สั่งเวลา " + "\n\r";
                    this.txtOrderAppToPrint += " " + createDate.Substring(0, 17) + "\n\r\n";
                    this.txtOrderAppToPrint += "  ส่งซ้ำ :\n\r" + reason;

                    flagResendORder = 1;
                    SelectPrinterDel(99, posPrinters[0].PrinterName);
                    flagResendORder = 0;


                } 

                panelVoidOrder.Visible = false;

              //  genOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ErrorREsend Local ");
            }
        }
        
        int flagSenderCust = 0;

     

        private void searchCustomer()
        {

            try
            {

                string srMemName = textBoxSRMemName.Text;
                string strSearchMemCard = textBoxStrSearchMemCardtoTable.Text;
                string srMemTel = textBoxSRTel.Text;
                string srLevelName = comboBoxSRLevel.Text;

                if (comboBoxSRLevel.SelectedIndex == 0)
                {

                    if (srMemName.Length > 0)
                    {
                        dataTableAllCust.DefaultView.RowFilter = string.Format("Name like '*{0}*' ", srMemName);

                    }
                    else
                    {
                        if (strSearchMemCard.Length > 0)
                            this.dataTableAllCust.DefaultView.RowFilter = string.Format("TaxID = '{0}' ", strSearchMemCard);
                        else if (srMemTel.Length > 0)
                            this.dataTableAllCust.DefaultView.RowFilter = string.Format("Tel = '{0}' ", srMemTel);
                        else
                            this.dataTableAllCust.DefaultView.RowFilter = string.Format(" 1 = 1 ");

                    }
                }
                else
                {
                    if (srMemName.Length > 0)
                    {
                        dataTableAllCust.DefaultView.RowFilter = string.Format("Name like '*{0}*' and Status =  '{1}' ", srMemName, srLevelName);

                    }
                    else
                    {
                        if (strSearchMemCard.Length > 0)
                            this.dataTableAllCust.DefaultView.RowFilter = string.Format("TaxID = '{0}'  and Status =  '{1}' ", strSearchMemCard, srLevelName);
                        else if (srMemTel.Length > 0)
                            this.dataTableAllCust.DefaultView.RowFilter = string.Format("Tel = '{0}'  and Status =  '{1}' ", srMemTel, srLevelName);
                        else
                            this.dataTableAllCust.DefaultView.RowFilter = string.Format(" Status =  '{0}' ", srLevelName);

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonCloseSearchPanelCust_Click(object sender, EventArgs e)
        {
            panelSearchCust.Visible = false ;
        }

        private void textBoxSRTel_TextChanged(object sender, EventArgs e)
        {
            searchCustomer();
        }

        private void textBoxSRMemName_TextChanged(object sender, EventArgs e)
        {
            searchCustomer();
        }

        private void dataGridViewAllMember_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataID = Int32.Parse(dataGridViewAllMember.Rows[e.RowIndex].Cells["CustID"].Value.ToString());

                comboBoxListCust.SelectedValue = dataID;

                //if (flagSenderCust == 1)
                //    comboBoxListCust.SelectedValue = dataID;
                //else
                //    comboBoxListSender.SelectedValue = dataID;


            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxStrSearchMemCardtoTable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                searchCustomer();
            }
        }

        private void buttonSearchCustomer_Click(object sender, EventArgs e)
        {
            panelSearchCust.Visible = true;
            flagSenderCust = 1;
            labelSearchCustSender.Text = "ค้นหาลูกค้า (Customer)";

           // comboBoxSRLevel.Text = "Customer";

           // searchCustomer();
        }

        private void comboBoxSRLevel_SelectedValueChanged(object sender, EventArgs e)
        {
            searchCustomer();
        }

        private void buttonCloseCustHist_Click(object sender, EventArgs e)
        {
            panelCustHist.Visible = false;
        }

        // Cust History
        private void buttonOpenPanelCustHist_Click(object sender, EventArgs e)
        {
            try
            {
                int custID = Int32.Parse(comboBoxListCust.SelectedValue.ToString());

                if (custID > 0)
                {

                    panelSearchCust.Visible = false;
                    panelCustHist.Visible = true;

                    radioButtonByCustID.Checked = true;
                    // radioButtonBySummary.Checked = true; 

                    dateTimePickerFromDate.Value = DateTime.Now.AddDays(-90);
                    dateTimePickerToDate.Value = DateTime.Now;

                    SrcHistoryCustPayment();
                }

                
            }
            catch (Exception ex)
            {

                //   MessageBox.Show(ex.Message);
            }
        }

        DataTable histCustPay;
        DataTable histCustTrn;

        private void SrcHistoryCustPayment()
        {
            try
            {
                int type = 0;

                if (radioButtonByMemCard.Checked)
                    type = 1;

                string fromDate = dateTimePickerFromDate.Value.ToString("yyyyMMdd");
                string toDate = dateTimePickerToDate.Value.ToString("yyyyMMdd");

                int trnID = 0;
                int custID = Int32.Parse(comboBoxListCust.SelectedValue.ToString());
                string memCardID = this.memCardID;

                histCustPay = gd.getHistPaymentByCust(fromDate, toDate, custID, memCardID, type);
                dataGridViewPaymentByCust.DataSource = histCustPay;

                dataGridViewPaymentByCust.Columns[4].Visible = false;


                histCustTrn = gd.getHistTransactionByCust(fromDate, toDate, custID, memCardID, type, trnID);
                dataGridViewTrnByCust.DataSource = histCustTrn;

                dataGridViewTrnByCust.Columns[1].Visible = false;
                dataGridViewTrnByCust.Columns[2].Visible = false;
                dataGridViewTrnByCust.Columns[5].Visible = false;


            }
            catch (Exception ex)
            {

                // MessageBox.Show(ex.Message);
            }
        }

        private void buttonViewCustHist_Click(object sender, EventArgs e)
        {
            SrcHistoryCustPayment();
        }

        private void buttonAddHistAllitems_Click(object sender, EventArgs e)
        {
            try
            {

                int productID = 0;
                string trnRemark = "";
                int orderQty = 0;


                foreach (DataGridViewRow row in dataGridViewTrnByCust.Rows)
                {

                    productID = Int32.Parse(row.Cells["ProductID"].Value.ToString());
                    trnRemark = row.Cells["TrnRemark"].Value.ToString();
                    orderQty = Int32.Parse(row.Cells["SalesQTY"].Value.ToString());

                    this.fn_AddOrder(productID, orderQty, trnRemark);
                }

                genOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void dataGridViewPaymentByCust_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int type = 0;

                if (radioButtonByMemCard.Checked)
                    type = 1;

                string fromDate = dateTimePickerFromDate.Value.ToString("yyyyMMdd");
                string toDate = dateTimePickerToDate.Value.ToString("yyyyMMdd");

                int trnID = Int32.Parse(dataGridViewPaymentByCust.Rows[e.RowIndex].Cells["TrnID"].Value.ToString());
                int custID = Int32.Parse(comboBoxListCust.SelectedValue.ToString());
                string memCardID = this.memCardID;

                //   MessageBox.Show(trnID.ToString());

                histCustTrn = gd.getHistTransactionByCust(fromDate, toDate, custID, memCardID, type, trnID);
                dataGridViewTrnByCust.DataSource = histCustTrn;


                dataGridViewTrnByCust.Columns[1].Visible = false;
                dataGridViewTrnByCust.Columns[2].Visible = false;
                dataGridViewTrnByCust.Columns[5].Visible = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            } 
        }

        private void dataGridViewTrnByCust_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int productID = Int32.Parse(dataGridViewTrnByCust.Rows[e.RowIndex].Cells["ProductID"].Value.ToString());
                string trnRemark = dataGridViewTrnByCust.Rows[e.RowIndex].Cells["TrnRemark"].Value.ToString();

                this.fn_AddOrder(productID, 1, trnRemark);

                genOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void ScanCard_Click(object sender, EventArgs e)
        {
            textBoxStrSearchMemCard.Focus();
        }

        private void buttonOpenPanelCustHist_MEM_Click(object sender, EventArgs e)
        {
            try
            { 
                if (this.memCardID.Length > 1)
                {
                    panelMemCard.Visible = false;
                    panelSearchCust.Visible = false;
                    panelCustHist.Visible = true;

                   // radioButtonByCustID.Checked = true;
                    radioButtonByMemCard.Checked = true;
                    // radioButtonBySummary.Checked = true; 

                    dateTimePickerFromDate.Value = DateTime.Now.AddDays(-90);
                    dateTimePickerToDate.Value = DateTime.Now;

                    SrcHistoryCustPayment();
                }


            }
            catch (Exception ex)
            {

                //   MessageBox.Show(ex.Message);
            }
        }

        private ThaiIDCard idcard;
        int idCardRead = 0;

        private void buttonReadIDCard_Click(object sender, EventArgs e)
        {
            try
            {
                //Clear 
                textBoxStrSearchMemCard.Text = "";

             //   string addr = "";

                LabelStatus.Text = "Reading...";
                // Refresh();

                idCardRead = 0;
                Personal personal = idcard.readAll();
                if (personal != null)
                {
                    textBoxStrSearchMemCard.Text = personal.Citizenid;
                    idCardRead = 1;
                    searchMemCard();

                    //dateTimePickerBirthDate.Value = personal.Birthday;

                    //if (personal.Sex == "1")
                    //    radioSexMale.Checked = true;
                    //else
                    //    radioSexFMale.Checked = false;

                    //if (radioButtonTH.Checked)
                    //    txtBoxName.Text = personal.Th_Prefix + personal.Th_Firstname + " " + personal.Th_Lastname;
                    //else
                    //    txtBoxName.Text = personal.En_Prefix + personal.En_Firstname + " " + personal.En_Lastname;


                    //txtBoxAddress.Text = personal.Address;
                  
                }
                else if (idcard.ErrorCode() > 0)
                {
                    MessageBox.Show(idcard.Error());
                }
                else
                {
                    MessageBox.Show("Catch all");
                }

                LabelStatus.Text = "Complete Read";
            }
            catch (Exception ex)
            {
                LabelStatus.Text = "Reading..Fail";
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonCloseSplitOrder_Click(object sender, EventArgs e)
        {
            panelSplitOrder.Visible = false;
        }

        private void getComboOrderFromTo()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List 
            data.Add(new KeyValuePair<int, string>(0, this.txtTableName.Text + " #0"));
            data.Add(new KeyValuePair<int, string>(1, this.txtTableName.Text + " #1"));
            data.Add(new KeyValuePair<int, string>(2, this.txtTableName.Text + " #2"));
            data.Add(new KeyValuePair<int, string>(3, this.txtTableName.Text + " #3"));
            data.Add(new KeyValuePair<int, string>(4, this.txtTableName.Text + " #4"));
            data.Add(new KeyValuePair<int, string>(5, this.txtTableName.Text + " #5"));

            // Clear the combobox
            comboBoxOrderFrom.DataSource = null;
            comboBoxOrderFrom.Items.Clear();

            // Bind the combobox
            comboBoxOrderFrom.DataSource = new BindingSource(data, null);
            comboBoxOrderFrom.DisplayMember = "Value";
            comboBoxOrderFrom.ValueMember = "Key"; 
            comboBoxOrderFrom.SelectedIndex = 0;

            // Clear the combobox
            comboBoxOrderTo.DataSource = null;
            comboBoxOrderTo.Items.Clear();

            // Bind the combobox
            comboBoxOrderTo.DataSource = new BindingSource(data, null);
            comboBoxOrderTo.DisplayMember = "Value";
            comboBoxOrderTo.ValueMember = "Key";
            comboBoxOrderTo.SelectedIndex = 1;

        }

        int subOrderID = -1;

        private void comboBoxORDER_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (comboBoxORDER.Text == "#ALL")
                    this.subOrderID = -1;
                else
                    this.subOrderID = Int32.Parse(comboBoxORDER.Text.Replace("#", ""));
                 
            }catch( Exception ex )
            {
                this.subOrderID = -1;
            }
            finally
            {
                genOrder();
            }
        }

        private void buttonViewSplitOrder_Click(object sender, EventArgs e)
        {

            try
            {
                panelSplitOrder.Visible = true; 
                refreshSplitOrder();
                //panelOrderList.Visible = true;
                //OrderConfirm();

            }
            catch (Exception ex)
            { 
            } 
        }

        private void refreshSplitOrder()
        {
            try
            {
               
                // From
                int fromSubOrderID = Int32.Parse(comboBoxOrderFrom.SelectedValue.ToString());
                int toSubOrderID = Int32.Parse(comboBoxOrderTo.SelectedValue.ToString());

                Table tableOrderFrom = gd.getMainOrderByTable(this.tableID, this.flagLang, 0, 0, this.memIDSelected, fromSubOrderID);
                Table tableOrderTo = gd.getMainOrderByTable(this.tableID, this.flagLang, 0, 0, this.memIDSelected, toSubOrderID);

                dataGridViewOrderFrom.DataSource = tableOrderFrom.Order;

                if (tableOrderFrom.Order.Count > 0)
                {
                    dataGridViewOrderFrom.Columns[1].Visible = false;
                    dataGridViewOrderFrom.Columns[2].Visible = false;
                    dataGridViewOrderFrom.Columns[3].Visible = false;
                    dataGridViewOrderFrom.Columns[4].Visible = false;
                    dataGridViewOrderFrom.Columns[5].HeaderText = "Desc";
                    dataGridViewOrderFrom.Columns[6].HeaderText = "Price";
                    dataGridViewOrderFrom.Columns[7].Visible = false;
                    dataGridViewOrderFrom.Columns[8].HeaderText = "Qty";
                    dataGridViewOrderFrom.Columns[9].HeaderText = "Amount";
                    dataGridViewOrderFrom.Columns[10].HeaderText = "OrderTime";
                    dataGridViewOrderFrom.Columns[11].Visible = false;
                    dataGridViewOrderFrom.Columns[12].Visible = false;
                    dataGridViewOrderFrom.Columns[13].Visible = false;
                    dataGridViewOrderFrom.Columns[14].Visible = false;
                    dataGridViewOrderFrom.Columns[15].Visible = false;
                    dataGridViewOrderFrom.Columns[16].Visible = false;
                    dataGridViewOrderFrom.Columns[17].Visible = false;
                    dataGridViewOrderFrom.Columns[18].Visible = false;
                    dataGridViewOrderFrom.Columns[19].Visible = false;
                    dataGridViewOrderFrom.Columns[20].Visible = false;
                    dataGridViewOrderFrom.Columns[21].Visible = false;
                    dataGridViewOrderFrom.Columns[22].Visible = false;
                }

                dataGridViewOrderTo.DataSource = tableOrderTo.Order;

                if (tableOrderTo.Order.Count > 0)
                {
                    dataGridViewOrderTo.Columns[1].Visible = false;
                    dataGridViewOrderTo.Columns[2].Visible = false;
                    dataGridViewOrderTo.Columns[3].Visible = false;
                    dataGridViewOrderTo.Columns[4].Visible = false;
                    dataGridViewOrderTo.Columns[5].HeaderText = "Desc";
                    dataGridViewOrderTo.Columns[6].HeaderText = "Price";
                    dataGridViewOrderTo.Columns[7].Visible = false;
                    dataGridViewOrderTo.Columns[8].HeaderText = "Qty";
                    dataGridViewOrderTo.Columns[9].HeaderText = "Amount";
                    dataGridViewOrderTo.Columns[10].HeaderText = "OrderTime";
                    dataGridViewOrderTo.Columns[11].Visible = false;
                    dataGridViewOrderTo.Columns[12].Visible = false;
                    dataGridViewOrderTo.Columns[13].Visible = false;
                    dataGridViewOrderTo.Columns[14].Visible = false;
                    dataGridViewOrderTo.Columns[15].Visible = false;
                    dataGridViewOrderTo.Columns[16].Visible = false;
                    dataGridViewOrderTo.Columns[17].Visible = false;
                    dataGridViewOrderTo.Columns[18].Visible = false;
                    dataGridViewOrderTo.Columns[19].Visible = false;
                    dataGridViewOrderTo.Columns[20].Visible = false;
                    dataGridViewOrderTo.Columns[21].Visible = false;
                    dataGridViewOrderTo.Columns[22].Visible = false;
                }

            }
            catch (Exception ex)
            {
            } 

        }

        private void dataGridViewOrderFrom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int fromSubOrderID = Int32.Parse(comboBoxOrderFrom.SelectedValue.ToString());
                int toSubOrderID = Int32.Parse(comboBoxOrderTo.SelectedValue.ToString());

                string orderBarcode = dataGridViewOrderFrom.Rows[e.RowIndex].Cells["OrderBarcode"].Value.ToString(); 
                int result = gd.updsMoveOrderSubID(this.tableID, fromSubOrderID, toSubOrderID, orderBarcode);

                refreshSplitOrder();
            }
            catch (Exception ex)
            {
            } 
        }

        private void dataGridViewOrderTo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int fromSubOrderID = Int32.Parse(comboBoxOrderTo.SelectedValue.ToString());
                int toSubOrderID = Int32.Parse(comboBoxOrderFrom.SelectedValue.ToString());

                string orderBarcode = dataGridViewOrderTo.Rows[e.RowIndex].Cells["OrderBarcode"].Value.ToString(); 
                int result = gd.updsMoveOrderSubID(this.tableID, fromSubOrderID, toSubOrderID, orderBarcode);

                refreshSplitOrder();
            }
            catch (Exception ex)
            {
            } 
        }

        private void comboBoxOrderFrom_SelectedValueChanged(object sender, EventArgs e)
        {
            refreshSplitOrder();
        }
          
        private void buttonPayment_ClearPay_Click(object sender, EventArgs e)
        {
            this.keyTime = 1;
            textBoxPay_PaythisType.Text = "0";
            payment_calBalancePay();

        }



        float totalAlreadyPay = 0;
        float totalpaythisBill = 0; 
        int keyTime = 1;
        List<BillPayment> billPayment;

        private void buttonCheckBill_Click(object sender, EventArgs e)
        {
            try
            {
                keyTime = 1;
                panelPaymentType.Visible = true;
                billPayment = new List<BillPayment>();
                defaultColButOrderNoPayment();

                dataGridViewBillPayment.DataSource = null;
                this.totalAlreadyPay = 0;
                buttonPay_Payment.Focus();


                string flagPrintCash = ConfigurationSettings.AppSettings["FlagPrintCash"].ToString();

                if (flagPrintCash.ToLower() == "y")
                    radioButtonFinalPrintBill.Checked = true;
                else
                    radioButtonFinalPrintBill.Checked = false;

                textBoxPay_BillTotal.Text = this.totalSalesAmount.ToString("###,###.#0");
                textBoxPay_AlreadyPay.Text = this.totalAlreadyPay.ToString("###,###.#0");
                textBoxPay_PaythisType.Text = (this.totalSalesAmount - this.totalAlreadyPay).ToString("###,###.#0");

               // buttonPay_Payment_Click( sender,  e);

            }
            catch (Exception ex)
            {

            }

        }

        private void defaultColButOrderNoPayment()
        {
            button_1.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_2.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_3.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_4.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_5.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_6.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_7.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_8.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_9.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_0.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_00.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_000.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            // buttonEqualBill.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
        }

        string oldtxt = "";

        private void ButtonNoClick(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            oldtxt = textBoxPay_PaythisType.Text;
            string txt = "";


            try
            {
                defaultColButOrderNoPayment();
                bt.BackColor = System.Drawing.Color.Orange;

                txt = bt.Name.Replace("button_", " ").Trim();

                if (keyTime == 1)
                    textBoxPay_PaythisType.Text = txt;
                else
                    textBoxPay_PaythisType.Text += txt;

                payment_calBalancePay();

                keyTime++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        private void ButtonKGNoClick(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            oldtxt = textBoxPay_PaythisType.Text;
            string txt = "";


            try
            {
                defaultColButOrderNoPayment();
                bt.BackColor = System.Drawing.Color.Orange;

                txt = bt.Name.Replace("buttonKG_", " ").Trim();

                if (txt == "DOT")
                    txt = ".";

                if (keyTime == 1)
                    txtBoxActProductWeight.Text = txt;
                else
                    txtBoxActProductWeight.Text += txt;

               // payment_calBalancePay();

                keyTime++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void payment_calBalancePay()
        {
            try
            {

                if (FuncString.IsNumeric(textBoxPay_PaythisType.Text))
                    totalpaythisBill = float.Parse(textBoxPay_PaythisType.Text);

                if (textBoxPay_PaythisType.Text.Length == 0)
                    totalpaythisBill = 0;
                 

                if (this.totalSalesAmount - this.totalAlreadyPay - this.totalpaythisBill < 0)
                    textBoxPay_PaythisType.Text = oldtxt;
                else
                    textBoxPay_Balance.Text = (this.totalSalesAmount - this.totalAlreadyPay - this.totalpaythisBill).ToString("###,###.#0");
 

            }
            catch (Exception ex)
            {
                textBoxPay_PaythisType.Text = "";
            }

        }

        private void buttonPayment_Cancle_Click(object sender, EventArgs e)
        {
            ScreenService.SecondMonitor.pictureBox1.Image = global::AppRest.Properties.Resources.Logo_New;
            panelPaymentType.Visible = false;
            dataGridViewBillPayment.DataSource = null;
            this.totalAlreadyPay = 0;
            payment_calBalancePay();
        }

        private void buttonPayment_FullBill_Click(object sender, EventArgs e)
        {
            try
            { 
                textBoxPay_PaythisType.Text = (this.totalSalesAmount - this.totalAlreadyPay).ToString("###,###.#0"); 
                payment_calBalancePay();
            }
            catch (Exception ex)
            {
                textBoxPay_PaythisType.Text = "";
            }
        }

        float cashPay = 0;
        float cashRecieve = 0;
        float cashChange = 0;

        private void buttoncheckBillPay_Click(object sender, EventArgs e)
        {
            try
            {
                string langBill = this.defaultlangBill;
                string couponCode = "";

                if (this.defaultlangBill == "NO")
                    langBill = this.flagLang;
                else if (this.defaultlangBill == "EN")
                    radioBoxBillEN.Checked = true;

                if (flagCouponCanUse == 1) 
                    couponCode = textBoxCouponCode.Text;

                int custID = Int32.Parse(comboBoxListCust.SelectedValue.ToString());

                // Quick Service
                 printAllOrder(); 

                 
                this.tableCheckBill = gd.getMainOrderByTable(this.tableID, langBill, 0, -1, this.memIDSelected, this.subOrderID);


                this.orderGetPoint = 0;
                this.orderCutPoint = 0;

                foreach (Order o in this.tableCheckBill.Order)
                {

                    if (o.OrderProductPoint > 0)
                        this.orderGetPoint += (int)o.OrderProductPoint * (int)o.OrderQTY;

                    if (o.OrderProductPoint < 0)
                        this.orderCutPoint += (int)o.OrderProductPoint * (int)o.OrderQTY;

                }

                this.billID = 0;
                int result = gd.checkBillByTable(this.tableID, memID, this.totalSalesAmount, this.totalDiscount, this.totalServiceCharge, this.totalVAT, custID, this.posIDConfig, this.memCardID, couponCode, this.subOrderID, this.billPayment);

                 

                if (result <= 0)
               {
                     MessageBox.Show("Error Check Bill By Table");
               }
               else
               {
                    if (this.orderCutPoint != 0)
                        MessageBox.Show( "ตัดคะแนนสมาชิก " + textBoxMemCardName.Text +  " : " + orderCutPoint.ToString()  + " คะแนน");

                    int rsPointCloud = 0;

                    if (this.memCardID.Length > 2)
                        rsPointCloud = gd.instMemCard_Point(this.memCardID, this.orderCutPoint, this.totalSalesAmount, result);

                    if (couponCode.Length > 2)
                        rsPointCloud = gd.updsCoupon_Used_ByCashier(couponCode);


                    this.flagCheckBill = "Y";
                     this.billID =  result ;
                    

                   if (posPrinters[0].FlagPrint.ToLower() == "y" && radioButtonFinalPrintBill.Checked)
                   {
                      
                       printCash.Print();
                       ++copyPrint; 

                       if (ConfigurationSettings.AppSettings["flagPrintCopyAfterCheckBill"].ToString().ToLower() == "y")
                           printCash.Print();
                         
                   }



                    foreach (BillPayment b in billPayment)
                   {
                       if (b.PaytypeID == 1)
                       {
                           RawPrinterHelper.OpenCashDrawer1(posPrinters[0].PrinterName);

                           this.strDisplayLine1 = "Change";
                           this.strDisplayLine2 = b.PayDesc2;
                           FuncString.displayPOSMonitorSecLine(this.strDisplayLine1, this.strDisplayLine2);

                           this.cashPay = (int)b.PayAmount ;
                           this.cashRecieve = (int)float.Parse( b.PayDesc1 );

                           this.cashChange = (int)float.Parse(b.PayDesc2); 

                          //   LinkFormCheckBill2();

                            if (ConfigurationSettings.AppSettings["MonitorDisplay"] == "Y")
                            {
                                buttonLinkToMainTable.Enabled = false;
                                panelPaymentType.Visible = false;

                                ScreenService.ShowOnMonitorCashResult(1);
                                ScreenService.FormCheckBill2Screen.fromCust.Text = this.cashRecieve.ToString();
                                ScreenService.FormCheckBill2Screen.totalsales.Text = this.cashPay.ToString();
                                ScreenService.FormCheckBill2Screen.change.Text = this.cashChange.ToString();

                                int counter = 0;
                                while (counter < 20)
                                {
                                    Application.DoEvents();
                                    Thread.Sleep(100);
                                    ++counter;
                                }

                                ScreenService.ShowOnMonitorCashResultHide();

                            }

                            LinkFormCheckBill2();

                        }
                   }            

                  
                   // only Super market 
                   panelPaymentType.Visible = false;
 


                   if (this.subOrderID == -1)
                   { 
                       if (defalutBarcodeOrder == "Y")
                       {
                           this.strDisplayLine1 = "Welcome"; 
                           genOrder();
                           OrderConfirm();

                           AddRemarkOrderPN.Controls.Clear();
                           DelOrderPN.Controls.Clear();

                           txtOrderDetail.Text = "No Order";
                           txtOrderUnit.Text = "Unit";
                           txtOrderAmt.Text = "Amt";
                           txtSalesAmount.Text = "0";  

                           textBoxSalesTotal.Text = "0"; 

                           panelFinance1.Visible = false;
                           panelFinance2.Visible = false;

                           textBoxDiscountAmt.Text = "0";
                           textBoxGroupDis1.Text = "0";
                           textBoxGroupDis2.Text = "0";
                           textBoxGroupDis3.Text = "0";
                           textBoxGroupDis4.Text = "0";
                           textBoxDiscountTotal.Text = "0";

                           textBoxItemCountBC.Text = "0";
                           textBoxWeightBC.Text = "0";

                            buttonLinkToMainTable.Enabled = true;

                            // ScreenService.ShowOnMonitorCashResultHide();
                            ScreenService.SecondMonitor.pictureBox1.Image = global::AppRest.Properties.Resources.Logo_New;
                            ScreenService.SendDataToSecondMonitor(null);
                            textBoxStrSearchMemCard.Text = "0";
                            searchMemCard();

                           comboBoxListCust.SelectedIndex = 0;

                           textBoxSearchBC.Focus();

                       }
                       else
                       {
                           // ScreenService.ShowOnMonitorCashResultHide();
                            ScreenService.SecondMonitor.pictureBox1.Image = global::AppRest.Properties.Resources.Logo_New;
                            ScreenService.SendDataToSecondMonitor(null);
                            LinkFormMainTable();
                       }

                   }
                   else
                   {
                        genOrder();
                        comboBoxORDER.SelectedIndex = 0;
                       dataGridViewBillPayment.DataSource = null;
                       this.totalAlreadyPay = 0;
                       payment_calBalancePay();
                   }
               }

            }
            catch (Exception ex)
            {
                
            }
        }

        private void LinkFormCheckBill()
        {
            Cursor.Current = Cursors.WaitCursor;

            this.totalpaythisBill = float.Parse(textBoxPay_PaythisType.Text);

            if (formCheckBill == null)
            {
                formCheckBill = new FormCheckBillPay(this, 0, this.totalpaythisBill, txtTableName.Text, 1);
            }
            else
            {

                formCheckBill = new FormCheckBillPay(this, 0, this.totalpaythisBill, txtTableName.Text, 1);
                formCheckBill.payAmount = this.totalpaythisBill;
                formCheckBill.textBox_PayAmount.Text = totalpaythisBill.ToString("###,###.#0");
            }


            Cursor.Current = Cursors.Default;
            if (formCheckBill.ShowDialog() == DialogResult.OK)
            {
                formCheckBill.Dispose();
                formCheckBill = null;
            }
        }

        private void buttonPay_Payment_Click(object sender, EventArgs e)
        {
            try
            {
              //  ScreenService.SecondMonitor.pictureBox1.Image = global::AppRest.Properties.Resources.QR;

                LinkFormCheckBill();

                ScreenService.SecondMonitor.pictureBox1.Image = global::AppRest.Properties.Resources.Logo_New;


                if (formCheckBill.paytype > 0)
                {

                    if (formCheckBill.paytype == 3)
                    {
                        int custID = Int32.Parse(comboBoxListCust.SelectedValue.ToString());
                        string custName = comboBoxListCust.Text;

                        // Check Existing

                        foreach (BillPayment b in billPayment)
                        {
                            if (b.PaytypeID == 3)
                                throw new Exception("คุณลงบิลไปแล้วกับลูกค้า " + b.PayDesc2 + "\n" + "Note. " + "1 บิล ลงบิลได้ 1 ครั้ง");
                        }


                        if (custID == 0)
                        {
                           panelFinance1.Visible = true;
                           panelFinance2.Visible = true; 
                            throw new Exception("กรุณาเลือกชื่อลูกค้าลงบิล !!!");
                        }
                        else
                        {
                            billPayment.Add(new BillPayment(billPayment.Count + 1, 3, "Credit Cust", this.totalpaythisBill, custID.ToString(), custName, ""));
                        }
                    }
                    else
                    {
                        foreach (BillPayment b in billPayment)
                        {
                            if (b.PaytypeID == 1 && formCheckBill.paytype == 1)
                                throw new Exception("คุณมีการชำระเงินสดไปแล้ว รับมา :" + b.PayDesc1 + " เงินทอน : " + b.PayDesc2 + "\n" + "Note. " + "1 บิล ลงได้ 1 ครั้ง");
                        }


                        billPayment.Add(new BillPayment(billPayment.Count + 1, formCheckBill.billPaymentByType.PaytypeID,
                                             formCheckBill.billPaymentByType.PaytypeName, formCheckBill.billPaymentByType.PayAmount,
                                              formCheckBill.billPaymentByType.PayDesc1, formCheckBill.billPaymentByType.PayDesc2, formCheckBill.billPaymentByType.PayDesc3));
                    }
                     

                    dataGridViewBillPayment.DataSource = null;
                    dataGridViewBillPayment.DataSource = billPayment;

                    if (dataGridViewBillPayment.RowCount > 0)
                    {
                        dataGridViewBillPayment.Columns[0].HeaderText = "No.";
                        dataGridViewBillPayment.Columns[1].Visible = false;
                        dataGridViewBillPayment.Columns[2].HeaderText = "Type";
                        dataGridViewBillPayment.Columns[3].HeaderText = "Amount";
                        dataGridViewBillPayment.Columns[4].HeaderText = "Desc1";
                        dataGridViewBillPayment.Columns[5].HeaderText = "Desc2";
                        dataGridViewBillPayment.Columns[6].HeaderText = "Desc3";
                    }


                    this.totalAlreadyPay = 0;
                    foreach (BillPayment b in billPayment)
                    {
                        this.totalAlreadyPay += b.PayAmount;
                    }

                    textBoxPay_AlreadyPay.Text = this.totalAlreadyPay.ToString("###,###.#0");
                    textBoxPay_PaythisType.Text = (this.totalSalesAmount - this.totalAlreadyPay).ToString("###,###.#0");
                    payment_calBalancePay();
                    keyTime = 1;

                    if (Int32.Parse(this.totalSalesAmount.ToString("##0")) == Int32.Parse(this.totalAlreadyPay.ToString("##0")))

                    {
                        buttonPay_CheckBill.Enabled = true;

                        buttoncheckBillPay_Click(sender, e);

                    }

                }
               
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 

        private void LinkFormCheckBill2()
        {
            Cursor.Current = Cursors.WaitCursor; 

            if (formCheckBill2 == null)
            {
                formCheckBill2 = new FormCheckBill2(this, 0,  this.cashPay,  this.cashRecieve ,  this.cashChange);
            }
            else
            {
                formCheckBill2.totalsales.Text = this.cashPay.ToString("###,###.#0");
                formCheckBill2.fromCust.Text = this.cashRecieve.ToString("###,###.#0");
                formCheckBill2.change.Text = this.cashChange.ToString("###,###.#0");
            

            }
            Cursor.Current = Cursors.Default;
            if (formCheckBill2.ShowDialog() == DialogResult.OK)
            {
                formCheckBill2.Dispose();
                formCheckBill2 = null;
            }
        }

        private void buttonLinkToMainTable_1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonViewQuotation_Click(object sender, EventArgs e)
        {


            try
            {


                int custID = Int32.Parse(comboBoxListCust.SelectedValue.ToString());
                int VATType = 0;


                if (checkBoxTax.Checked)
                    VATType = 2;

                int discountype = 0; // 0 = เฉพาะอาหาร , 1 = ทั้งหมด
                int discountPer = 0;
                int discountAmt = 0;
                int tax = 0;
                int servicecharge = 0;
                string remark = this.strRemark;
                // 0 = No Print , 1 = Print
 

                if (checkBoxTax.Checked == true)
                    tax = 1;

                if (checkBoxServiceCharge.Checked == true)
                    servicecharge = 1;



                remark = @"FOR:Disc=:" + textBoxGroupDis1.Text + ":" + textBoxGroupDis2.Text +
                                     ":" + textBoxGroupDis3.Text + ":" + textBoxGroupDis4.Text +
                                     ":" + textBoxDiscountAmt.Text + ":0";

                //  remark += "|" + textBoxDeliveryPO.Text + "|" + dateTimeDeliveryDate.Value.ToString("yyyyMMdd") + "|" + textBoxDeliveryNoted.Text + "|" + textBoxDeliveryAddress.Text;

                string strMemSearch = textBoxStrSearchMemCard.Text.Trim();

                int result = gd.instPrintBillFlag(this.tableID, custID, discountype, discountPer, discountAmt, tax, servicecharge, this.printBillFlag, remark, strMemSearch);


                // MessageBox.Show(type.ToString());
                LinkFromRptBillReport(this.tableID, 1, custID, Login.userName, VATType, this.totalDiscount);
                countReport++;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        FromRptQuotation formRptQuoReport;
        int countReport = 0;
        private void LinkFromRptBillReport(int tableID, int BranchID, int MemID, string CreateBy, int VATType, float Discount)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formRptQuoReport == null)
            {
                formRptQuoReport = new FromRptQuotation(this, 0, tableID, BranchID, MemID, CreateBy, VATType, Discount, countReport);
            }
            else
            {
                formRptQuoReport.Count = countReport;
                formRptQuoReport.tableID = tableID;
                formRptQuoReport.BranchID = BranchID;
                formRptQuoReport.MemID = MemID;
                formRptQuoReport.CreateBy = CreateBy;
                formRptQuoReport.VATType = VATType;
                formRptQuoReport.Discount = Discount;


                formRptQuoReport.viewReport();
            }
            Cursor.Current = Cursors.Default;
            if (formRptQuoReport.ShowDialog() == DialogResult.OK)
            {
                formRptQuoReport.Dispose();
                formRptQuoReport = null;
            }
        }

        API api = new API();
        int aiCount = 0;

        private void buttonAIgenRegister_Click(object sender, EventArgs e)
        {
            aiCount = 0;
            textBoxJodID.Text = "";
            AIgenRegister();
        }

        private void buttonAIgenSearch_Click(object sender, EventArgs e)
        {
            aiCount = 0;
            textBoxJodID.Text = "";
            AIgenIndentification();
        }

        private void AIgenRegister()
        {
          

            string jobID = "";
            Root rt;

            try
            {
                if (this.memCardID == "0")
                    throw new Exception("กรุณาป้อนรหัสสมาชิก / เบอร์โทร ก่อน");

                if (textBoxJodID.Text == "")
                {
                    jobID = api.AImemRegisterStart(this.memCardID);
                    textBoxJodID.Text = jobID;
                }

                rt = api.AImemRegister(textBoxJodID.Text);

                if (rt.data.wait == true)
                {
                    //  Loop
                    // Delay 1 Sec

                     
                    Thread.Sleep(500);
                    AIgenRegister();
                    aiCount++;
                    if( aiCount == 20 ) 
                        MessageBox.Show("Register AI Gen Error Please Try Again !!");

                }
                else
                {
                    if (rt.data.result.data.reference_id == this.memCardID)
                    {
                        MessageBox.Show("Register Success !!");
                        textBoxJodID.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Register AI Gen Error Please Try Again !!");
                    }
                     
                } 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void AIgenIndentification()
        {
           
            string jobID = "";
            Root rt;

            try
            {

                if (textBoxJodID.Text == "")
                {
                    jobID = api.AImemIdentificationStart();
                    textBoxJodID.Text = jobID;
                }

                rt = api.AImemIdentification(textBoxJodID.Text);

                if (rt.data.wait == true)
                {
                    //  Loop
                    // Delay 1 Sec
                    Thread.Sleep(500);
                    AIgenIndentification();
                    aiCount++;
                    if (aiCount == 20)
                        MessageBox.Show("Indentification AI Gen Error Please Try Again !!");
                }
                else
                {
                    textBoxStrSearchMemCard.Text = rt.data.result.data.reference_id;
                    searchMemCard();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButtonOrderKG_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (FuncString.IsNumeric(txtBoxActProductWeight.Text))
                {
                    if (radioButtonOrderGram.Checked)
                        txtBoxActProductWeightAmt.Text = (float.Parse(txtBoxActProductWeight.Text) / (float)1000.00 * float.Parse(txtBoxActProductPrice.Text)).ToString("###,###.#0");
                    else
                        txtBoxActProductWeightAmt.Text = (float.Parse(txtBoxActProductWeight.Text) * float.Parse(txtBoxActProductPrice.Text)).ToString("###,###.#0");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonOrderListClose_Click(object sender, EventArgs e)
        {
            panelOrderList.Visible = false;
        }

        string staffName = "";

        private void dataGridViewOrderConfirm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                string billNo =  dataGridViewOrderConfirm.Rows[e.RowIndex].Cells["BillNo"].Value.ToString();
                this.staffName = dataGridViewOrderConfirm.Rows[e.RowIndex].Cells["StaffName"].Value.ToString();
                string payTypeName = dataGridViewOrderConfirm.Rows[e.RowIndex].Cells["BillPayTypeName"].Value.ToString();
                string billAmount = dataGridViewOrderConfirm.Rows[e.RowIndex].Cells["BillAmount"].Value.ToString();
                string url = dataGridViewOrderConfirm.Rows[e.RowIndex].Cells["ImgRec"].Value.ToString();


                string msg = "พนักงาน : " + staffName + "\n\r" + " ชำระเงิน : " + payTypeName + "  >> " + billAmount + "\n\r" + " กรุณาตรวจสอบการชำระเงิน";

                subOrderID = Int32.Parse( billNo.Split('-')[1] );

                comboBoxORDER.SelectedIndex = subOrderID + 1;

                MessageBox.Show(msg);

                // Slip
                //  Image imgSlip = getImageFromURL(imgRec);
                if(url.Length > 0 && payTypeName.Substring(0,2) == "QR")
                     pictureBoxImgOrder.Image = getImageFromURL(url);

            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message);
                pictureBoxImgOrder.Image = global::AppRest.Properties.Resources.Logo_New;
            }
        }

        DataTable orderFromStaff = new DataTable();

        private void OrderConfirm()
        {
            try
            {
            
                orderFromStaff = gd.getStaffOrder_Header(this.tableID, 0, 1);

                dataGridViewOrderConfirm.DataSource = null;
                dataGridViewOrderConfirm.DataSource = orderFromStaff;

              //  MessageBox.Show(orderFromStaff.Rows.Count.ToString());

                panelOrderList.Visible = false;

                if (dataGridViewOrderConfirm.RowCount > 0)
                {
                    dataGridViewOrderConfirm.Columns[0].HeaderText = "บิล";
                    dataGridViewOrderConfirm.Columns[1].Visible = false;
                    dataGridViewOrderConfirm.Columns[2].Visible = false;
                    dataGridViewOrderConfirm.Columns[3].Visible = false;
                    dataGridViewOrderConfirm.Columns[4].Visible = false;
                    dataGridViewOrderConfirm.Columns[5].HeaderText = "ยอด";
                    dataGridViewOrderConfirm.Columns[6].HeaderText = "พนักงาน";
                    dataGridViewOrderConfirm.Columns[7].Visible = false;
                    dataGridViewOrderConfirm.Columns[8].Visible = false;
                    dataGridViewOrderConfirm.Columns[9].Visible = false;
                    dataGridViewOrderConfirm.Columns[10].HeaderText = "ชำระเงิน";
                    dataGridViewOrderConfirm.Columns[11].Visible = false;
                    dataGridViewOrderConfirm.Columns[12].HeaderText = "Slip";

                    panelOrderList.Visible = true;

                    foreach (DataRow row in orderFromStaff.Rows)
                    {
                        string billNo = row["BillNo"].ToString();  //dataGridViewOrderConfirm.Rows[e.RowIndex].Cells["BillNo"].Value.ToString();
                        this.staffName = row["StaffName"].ToString();  // dataGridViewOrderConfirm.Rows[e.RowIndex].Cells["StaffName"].Value.ToString();
                        string payTypeName = row["BillPayTypeName"].ToString(); // dataGridViewOrderConfirm.Rows[e.RowIndex].Cells["BillPayTypeName"].Value.ToString();
                        string billAmount = row["BillAmount"].ToString();  // dataGridViewOrderConfirm.Rows[e.RowIndex].Cells["BillAmount"].Value.ToString();
                        string url = row["ImgRec"].ToString(); // dataGridViewOrderConfirm.Rows[e.RowIndex].Cells["ImgRec"].Value.ToString();


                        string msg = "พนักงาน : " + staffName + "\n\r" + " ชำระเงิน : " + payTypeName + "  >> " + billAmount + "\n\r" + " กรุณาตรวจสอบการชำระเงิน";

                        subOrderID = Int32.Parse(billNo.Split('-')[1]);

                        comboBoxORDER.SelectedIndex = subOrderID + 1;

                        MessageBox.Show(msg);

                        // Slip
                        //  Image imgSlip = getImageFromURL(imgRec);
                        if (url.Length > 0 && payTypeName.Substring(0, 2) == "QR")
                            pictureBoxImgOrder.Image = getImageFromURL(url);
                    }

                }




            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message);
                pictureBoxImgOrder.Image = global::AppRest.Properties.Resources.Logo_New;
            }
         }

        string QROrder = "";

        private void buttonQROrder_Click(object sender, EventArgs e)
        {
           // panelQRWebOrdering.Visible = true;

            string qrOrdering = ConfigurationSettings.AppSettings["DomainWebQR"].ToString();

            qrOrdering = qrOrdering + "" + this.QROrder;

            if (qrOrdering.Length > 0)
            {
                QRCode.GenQRCode(qrOrdering);
                pictureBoxQRWebOrdering.Image = QRCode.resultBarcode;
                panelQRWebOrdering.Visible = true;
            }
        }

        private void buttonCLOSE_Click(object sender, EventArgs e)
        {
            panelQRWebOrdering.Visible = false;



        }

        private void buttonPrintQR_Click(object sender, EventArgs e)
        {
            printQRWebOrdering.Print();
        }

        private void printQRWebOrdering_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                int x = 0;
                int y = 0;

                Brush brush = new SolidBrush(Color.Black);
                Font fontHeader = new Font("Tahoma", 15);
                Font fontTable = new Font("Tahoma", 17);
                Font fontSubHeader = new Font("Tahoma", 10);
                Font fontBody = new Font("Tahoma", 12);

                y += 10;

                e.Graphics.DrawString(" QR Ordering Table : " + this.txtTableName.Text , fontSubHeader, brush, x + 50, y);
                y += 20;

                e.Graphics.DrawString("ลูกค้าสั่งอาหาร Scan QR ค่ะ", fontSubHeader, brush, x + 50, y);
                y += 20;
                 

                e.Graphics.DrawImage(pictureBoxQRWebOrdering.Image, x + 30, y, 200, 150);
                y += 145;

                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
            finally
            {
                // genOrder();
            }
        }






        //private void SendEmailConfirm()
        //{

        //    SendEmail sendEmail;

        //    string custbookingAt = txtTableName.Text;

        //    string custpaytype = "";


        //    try
        //    {
        //        int x = 0;
        //        int y = 0;

        //        int i = 1;

        //        DateTime dt = DateTime.Now;

        //        string strDate = String.Format("{0:dd MMM yy}", dt);
        //        string strTime = String.Format("{0:HH:mm}", dt);



        //        List<Transaction> trn = gd.getTrnOrder(this.billNoResult);

        //        string headerAdd = "Confirm Order [" + strDate + "] #" + this.billNoResult.ToString() + " of " + txtTableName.Text + " ( By " + Login.userName + " ) ";


        //        float salesAmountFood = 0;
        //        float salesAmountDrinks = 0;
        //        float salesAmount = 0;
        //        float tax = 0;
        //        float serviceCharge = 0;
        //        float discount = 0;

        //        string str1 = "";
        //        string str2 = "";
        //        string str3 = "";
        //        string str4 = "";
        //        string trnProductRemark = "";


        //        this.detailAdd = headerAdd + "\r\n\n";

        //        foreach (Transaction o in trn)
        //        {

        //            if (o.GroupCatID > 0)
        //            {

        //                str1 = o.ProductName;

        //                str2 = o.SalesQTY.ToString();
        //                str3 = (o.SalesAmount / o.SalesQTY).ToString("###,###.#0");
        //                str4 = o.SalesAmount.ToString("###,###.#0");

        //                trnProductRemark = o.TrnRemark;

        //                this.detailAdd += i.ToString() + ". " + str1 + " [" + str3 + "] >> @" + str2 + " " + o.ProductUnit + " :: " + str4 + " THB." + "\r\n";

        //                i++;
        //                //y += 15;

        //                //if (trnProductRemark.Trim().Length > 1)
        //                //{
        //                //    string[] remarkString = trnProductRemark.Remove(0, 1).Split('+');

        //                //    foreach (string r in remarkString)
        //                //    {

        //                //        str1 = "  +" + r + "\r\n";

        //                //        e.Graphics.DrawString(str1, fontBodylist, brush, x + 22, y);
        //                //        y += 15;
        //                //    }
        //                //}


        //                //if (o.GroupCatID == 1)
        //                //{

        //                //    salesAmountFood += o.SalesAmount;
        //                //}
        //                //else
        //                //{

        //                //    salesAmountDrinks += o.SalesAmount;
        //                //}

        //                salesAmount += o.SalesAmount;

        //            }
        //            else
        //            {

        //                if (o.ProductID == 1)
        //                {
        //                    discount = o.SalesAmount * -1;
        //                }
        //                else if (o.ProductID == 3)
        //                {
        //                    tax = o.SalesAmount;
        //                }
        //                else if (o.ProductID == 2)
        //                {
        //                    serviceCharge = o.SalesAmount;
        //                }


        //            }

        //        }


        //        this.detailAdd += "\r\n" + "  Total Sale Order :::: " + (salesAmount - discount + tax + serviceCharge).ToString("###,###.#0") + " THB." + "\r\n\n";

        //        //   this.detailAdd += "   -------------------------------------------------------------------" + "\n\n";

        //        //this.detailAdd += @"วันงานกรุณานำเอกสาร Confirm ใน Email หรือ ที่จองจาก Outlet ต่างๆ " + "\n";
        //        //this.detailAdd += @"มาแสดง พร้อมบัตรประชาชนตัวจริง / โทรศัพท์ เบอร์ เครื่องที่จองครับ" + "\n\n";

        //        this.detailAdd += "Thankyou \n I like Team \n Tel 053-328-238 ";


        //        sendEmail = new SendEmail(headerAdd, this.detailAdd);
        //        sendEmail.SendGmail();
        //        MessageBox.Show(this.detailAdd);


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Send Mail : " + ex.Message);
        //    }


        //}


    }
}
