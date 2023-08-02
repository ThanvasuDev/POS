using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.IO;
using System.Drawing.Printing;

namespace AppRest
{
    public partial class MainTable : MainTemplateS
    {
        GetDataRest gd;
        List<Table> tbs;
        List<Table> tbsBills;

        OrderTable formLinkOrderTable;

        AddTable formAddTable;
        List<Zone> allZone;

        int mainTableYMargin;

        string salesDate;
        int tableID;
        int tableCustNo;

        string defalutBarcodeOrder;

        string printerCashName;
        string cashDrawerPassword;

        AddEndDays_Cashier formLinkEndDay_Cashier;

        AddPO formAddPO;
        AddPO_Branch formAddPO_Branch;

        string barcodeScanLastest = "";

        Bitmap imgOntime = global::AppRest.Properties.Resources.On_time;
        Bitmap imgLate = global::AppRest.Properties.Resources.Late;
        Bitmap imgNoBarcode = global::AppRest.Properties.Resources.no_Barcode;

        string qrOrdering;

        Branch branch;


        public MainTable(Form frmlkFrom, int flagFrmClose)
        {
            InitializeComponent();
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }

            this.Text = this.Text + " ( By : " + Login.userName + ")";

            gd = new GetDataRest();

            try
            {

                buttonLinkMain.BackColor = System.Drawing.Color.Gray;
                mainTableYMargin = Int32.Parse(ConfigurationSettings.AppSettings["MainTableYMargin"].ToString());

                branch = gd.getBranchDesc();

                allZone = gd.getAllZone();

                if (Login.zoneOrderOption == 1)
                    radioButtonAll.Checked = true;
                else if (Login.zoneOrderOption == 2)
                    radioButtonOrder.Checked = true;
                else if (Login.zoneOrderOption == 3)
                    radioButtonList.Checked = true;


                genObjMainZone();
                refreshOrder();


                var timer = new Timer();
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = 10000; //10 seconds
                timer.Start();

                salesDate = "";


                if (Login.userStatus == "Work" || Login.userStatus == "Captain")
                {
                    buttonOpenCashDrawer.Visible = false;
                    buttonAddPOBranch.Visible = false;
                }

                panelAddCust.Visible = false;
                panelInputOrder.Visible = false;


              

                cashDrawerPassword = ConfigurationSettings.AppSettings["CashDrawerPass"];
                printerCashName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();
                printQRWebOrdering.PrinterSettings.PrinterName = this.printerCashName;
                printVoidOrder.PrinterSettings.PrinterName = this.printerCashName;

                defalutBarcodeOrder = ConfigurationSettings.AppSettings["DefalutBarcodeOrder"].ToString();


                if (Login.isFrontPOS)
                {
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    this.Bounds = Screen.PrimaryScreen.Bounds;
                }

                this.Width = 1024;
                this.Height = 764;
                this.panelOrderlist.Width = 850;

                if (Login.isFrontWide)
                {
                    this.Width = 1280;
                }

                panelShowBC.Visible = false;
                txtBoxSearchTableName.Select();


                qrOrdering = ConfigurationSettings.AppSettings["DomainWebOrdering"].ToString();

                if (qrOrdering.Length > 0)
                {
                    QRCode.GenQRCode(qrOrdering);
                    pictureBoxQRWebOrdering.Image = QRCode.resultBarcode;
                    buttonWebOrdering.Visible = true;
                }

                panelOrderListByTable.Visible = false;


                posPrinters = new List<POSPrinter>();
                posLoadPrinter();

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
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
         


        void timer_Tick(object sender, EventArgs e)
        {
            try
            {

                if (Login.zoneOrderOption == 1)
                    genObjMainTableRefreSh();
                else if (Login.zoneOrderOption == 2)
                    genObjMainTable_Order();
                else if (Login.zoneOrderOption == 3)
                    genObjMainTable_List();

            }
            catch (Exception ex)
            { 
                // MessageBox.Show(ex.Message);
            }

        }

        private void genObjMainZone()
        {
            try
            {

                //   gentxtTopMenu(); 
                panelZone.Controls.Clear();

                int zoneID = 0;
                string zoneName = "";
                string zoneColour = "";

                string[] productcolor;
                string[] productColorBG;
                string productColorTxt;
                string productcolorFull;

                Button bTable;

                int sizeX = 90;
                int sizeY = 52;

                int yy = 15;

                int i = 0;
                int j = 0;
                int k = 0;

                // All

                bTable = new Button();

                //    bTable.Cursor = System.Windows.Forms.Cursors.Default;
                bTable.FlatAppearance.BorderColor = System.Drawing.Color.White;
                bTable.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));

                bTable.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + ((sizeY + this.mainTableYMargin) * (i / yy)));
                bTable.Name = "0";

                bTable.Size = new System.Drawing.Size(sizeX, sizeY);
                bTable.TabIndex = 0;
                bTable.Text = "All";
                bTable.UseVisualStyleBackColor = false;
                bTable.Click += new System.EventHandler(this.bZone_Click);

                panelZone.Controls.Add(bTable);

                i++;


                foreach (Zone t in allZone)
                {

                    zoneID = t.ZoneID;
                    zoneName = t.ZoneName;
                    productcolorFull = t.ZoneColour;

                    if (t.ZoneDesc.Contains('|'))
                    {

                        string[] zSTR = t.ZoneDesc.Split('|');
                        zoneColour = zSTR[1];

                    }


                    bTable = new Button();


                    bTable.ForeColor = System.Drawing.Color.Black;
                    bTable.BackColor = System.Drawing.Color.LightYellow;


                    if (FuncString.Right(productcolorFull, 2).ToLower() == "|b" || FuncString.Right(productcolorFull, 2).ToLower() == "|w")
                    {

                        productcolor = productcolorFull.Split('|');
                        productColorBG = productcolor[0].Split(',');
                        productColorTxt = productcolor[1];

                        if (productColorTxt.ToLower() == "b")
                        {
                            bTable.ForeColor = System.Drawing.Color.Black;
                        }
                        else
                        {
                            bTable.ForeColor = System.Drawing.Color.White;
                        }


                        bTable.BackColor = Color.FromArgb(Int32.Parse(productColorBG[0]), Int32.Parse(productColorBG[1]), Int32.Parse(productColorBG[2]));

                    }

                    bTable.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    bTable.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));

                    bTable.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + ((sizeY + this.mainTableYMargin) * (i / yy)));
                    bTable.Name = zoneID.ToString();

                    bTable.Size = new System.Drawing.Size(sizeX, sizeY);
                    bTable.TabIndex = zoneID;
                    bTable.Text = zoneName;
                    bTable.UseVisualStyleBackColor = false;
                    bTable.Click += new System.EventHandler(this.bZone_Click);

                    panelZone.Controls.Add(bTable);

                    i++;
                }

            }
            catch (Exception ex)
            {

                // MessageBox.Show(ex.Message);
            }

        }



        private void genObjMainTable()
        {
            try
            {

                //  gentxtTopMenu();

                MainTablePN.Controls.Clear();


                tbs = gd.getMainTable(Login.zoneDefaultID, Login.zoneOrderOption);

                int tableID;
                string tableName;
                string tableFlagUse;
                int tableCountOrder;
                int tablePrintBillFlag;
                int tableCustID;
                int tableZoneID;
                int tableZoneType;

                Button bTable;

                int sizeX = 101;
                int sizeY = 60;

                int yy = 8;

                int i = 0;
                int j = 0;
                int k = 0;

                int zoneIDold = 1;

                string[] str;

                Font fTable;

                this.mainTableYMargin = 8;

                foreach (Table t in tbs)
                {
                    tableID = t.TableID;
                    tableName = t.TableName;
                    tableFlagUse = t.TableFlagUse;
                    tableCountOrder = (int)t.OrderQTY;
                    tablePrintBillFlag = t.TableCustID;
                    tableCustID = t.TableCustID;
                    tableZoneID = t.ZoneID;

                    if (t.TableZoneType == "#CUST")
                        tableZoneType = 1;
                    else if (t.TableZoneType == "#ORDER")
                        tableZoneType = 2;
                    else
                        tableZoneType = 3;

                    fTable = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));

                    if (tableName.Contains(">"))
                    {
                        str = tableName.Split('>');
                        tableName = "";
                        foreach (String s in str)
                            tableName += s + "\n";

                        fTable = new System.Drawing.Font("Century Gothic", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    }


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



                        if (zoneIDold != tableZoneID && i > 0 && i % yy != 0)
                        {
                            i = i / yy * yy + yy;
                        }

                        bTable.ForeColor = System.Drawing.Color.Black;

                        if (tableCountOrder > 0)
                        {
                            if (tablePrintBillFlag == 1)
                            {

                                bTable.BackColor = System.Drawing.Color.Orange; // รอจ่ายเงิน     
                                bTable.ForeColor = System.Drawing.Color.White;

                                k++;
                            }
                            else if (tablePrintBillFlag == 2)
                            {

                                bTable.BackColor = System.Drawing.Color.MidnightBlue; // ยังไม่ส่ง Order     
                                bTable.ForeColor = System.Drawing.Color.White;

                                k++;
                            }
                            else if (tablePrintBillFlag == 3)
                            {

                                bTable.BackColor = System.Drawing.Color.Red; // รอจ่ายเงิน     
                                bTable.ForeColor = System.Drawing.Color.White;

                                k++;
                            }
                        }


                        if ((Login.userTableID == 0) || (Login.userTableID == tableID))
                        {
                            bTable.Cursor = System.Windows.Forms.Cursors.Default;
                            bTable.FlatStyle = System.Windows.Forms.FlatStyle.Popup;

                            bTable.Font = fTable;

                            bTable.Location = new System.Drawing.Point(1 + ((sizeX + 5) * (i % yy)), 1 + ((sizeY + 1) * (i / yy)));
                            bTable.Name = "T" + tableID.ToString();

                            //if (tableZoneType == 1)
                            //    bTable.IsAccessible = true;
                            //else
                            //    bTable.IsAccessible = false;

                            bTable.FlatAppearance.BorderSize = tableZoneType;



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

        private void genObjMainTable_Order()
        {
            try
            {

                //  gentxtTopMenu();

                MainTablePN.Controls.Clear();


                tbs = gd.getMainTable(Login.zoneDefaultID, Login.zoneOrderOption);

                int tableID;
                string tableName;
                string tableFlagUse;
                int tableCountOrder;
                int tablePrintBillFlag;
                int tableCustID;
                int tableZoneID;
                int tableZoneType;

                Button bTable;

                int sizeX = 82;
                int sizeY = 80;

                int yy = 10;

                int i = 0;
                int j = 0;
                int k = 0;

                int zoneIDold = 1;

                string[] str;

                Font fTable;

                this.mainTableYMargin = 8;

                foreach (Table t in tbs)
                {
                    tableID = t.TableID;
                    tableName = t.TableName;
                    tableFlagUse = t.TableFlagUse;
                    tableCountOrder = (int)t.OrderQTY;
                    tablePrintBillFlag = t.TableCustID;
                    tableZoneID = t.ZoneID;

                    if (t.TableZoneType == "#CUST")
                        tableZoneType = 1;
                    else if (t.TableZoneType == "#ORDER")
                        tableZoneType = 2;
                    else
                        tableZoneType = 3;


                    if (tableCountOrder > 0)
                    {


                        fTable = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));

                        if (tableName.Contains(">"))
                        {
                            str = tableName.Split('>');
                            tableName = "";
                            foreach (String s in str)
                                tableName += s + "\n";

                            fTable = new System.Drawing.Font("Century Gothic", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                        }


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
                                if (tablePrintBillFlag == 1)
                                {

                                    bTable.BackColor = System.Drawing.Color.Orange; // รอจ่ายเงิน     
                                    bTable.ForeColor = System.Drawing.Color.White;

                                    k++;
                                }
                                else if (tablePrintBillFlag == 2)
                                {

                                    bTable.BackColor = System.Drawing.Color.MidnightBlue; // ยังไม่ส่ง Order     
                                    bTable.ForeColor = System.Drawing.Color.White;

                                    k++;
                                }
                                else if (tablePrintBillFlag == 3)
                                {

                                    bTable.BackColor = System.Drawing.Color.Red; // รอจ่ายเงิน     
                                    bTable.ForeColor = System.Drawing.Color.White;

                                    k++;
                                }
                            }


                            if ((Login.userTableID == 0) || (Login.userTableID == tableID))
                            {
                                bTable.Cursor = System.Windows.Forms.Cursors.Default;
                                bTable.FlatStyle = System.Windows.Forms.FlatStyle.Popup;

                                bTable.Font = fTable;

                                bTable.Location = new System.Drawing.Point(1 + ((sizeX + 5) * (i % yy)), 1 + ((sizeY + 1) * (i / yy)));
                                bTable.Name = "T" + tableID.ToString();

                                bTable.FlatAppearance.BorderSize = tableZoneType;

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



            }
            catch (Exception ex)
            {

                // MessageBox.Show(ex.Message);
            }

        }

        DataTable orderFromStaff = new DataTable();

        int OrderlastRecord = 0;
        int OrderCurrentRecord = 0;

        private void genObjMainTable_List()
        {
            try
            {

                panelOrderlist.Visible = true;
              


                //  labelBilRemark.Text = "";

                string billNo = "";
                string zone = "";
                int tableID = 0;
                string table = "";
                string staffName = "";
                string payTypeName = "";
                string billAmount = "";
                string remark = "";
                string cashRecAmount = "";

                int sizeX = 20;
                int sizeY = 20;
                int yy = 1;
                int i = 1;

                Button bOrder_Void;
                Button bOrder_Order;
                Button bOrder_Check;
                Button bOrder_QR;


                orderFromStaff = gd.getStaffOrder_Header(0, 0, 1);
                OrderCurrentRecord = orderFromStaff.Rows.Count; 
                 

                if (OrderCurrentRecord != OrderlastRecord) {

                    panelOrderlistAction.Controls.Clear();

                    labelBillNo.Text = "";
                    labelBillZone.Text = "";
                    labelBillTable.Text = "";
                    labelBillStaff.Text = "";
                    labelBillPayTypeName.Text = "";
                    labelBillAmount.Text = "";

                    foreach (DataRow row in orderFromStaff.Rows)
                    {
                       

                        billNo = row["BillNo"].ToString();
                        zone = row["BillZone"].ToString();
                        tableID = Int32.Parse(row["BillTableID"].ToString());
                        table = row["BillTable"].ToString();
                        staffName = row["StaffName"].ToString();
                        payTypeName = row["BillPayTypeName"].ToString();
                        billAmount = float.Parse(row["BillAmount"].ToString()).ToString("###,###.#0");
                        cashRecAmount = float.Parse(row["BillCash"].ToString()).ToString("###,###.#0");


                        if (this.selectZoneName == zone || this.selectZoneName == "All" )
                        {

                            //if(payTypeName == "QR")
                            remark = row["ImgRec"].ToString();

                            labelBillNo.Text += billNo + "\r\n";
                            labelBillZone.Text += zone + "\r\n";
                            labelBillTable.Text += table + "\r\n";
                            labelBillStaff.Text += staffName + "\r\n";
                            labelBillPayTypeName.Text += payTypeName + "\r\n";
                            labelBillAmount.Text += billAmount + "\r\n";
                            // labelBilRemark.Text += remark + "\r\n";


                            ///-------------------------------------
                            ///

                            //// 
                            //// buttonOrder_QR
                            //// 
                            ///
                            bOrder_QR = new Button();

                            bOrder_QR.BackColor = System.Drawing.Color.Transparent;
                            bOrder_QR.BackgroundImage = global::AppRest.Properties.Resources.QR;
                            bOrder_QR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                            bOrder_QR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
                            bOrder_QR.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                            bOrder_QR.Font = new System.Drawing.Font("Candara", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            bOrder_QR.ForeColor = System.Drawing.Color.Black;
                            bOrder_QR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            bOrder_QR.Location = new System.Drawing.Point(25 + (sizeX * (i % yy)), 0 + (i * 2) + (sizeY * (i / yy) - sizeY));
                            bOrder_QR.Name = "buttonOrder_QR" + billNo;
                            bOrder_QR.Text = billNo + "|" + billNo.Split('-')[1] + "|" + staffName + "|" + remark + "|" + payTypeName + "|" + billAmount.ToString() + "|" + cashRecAmount.ToString();
                            bOrder_QR.Size = new System.Drawing.Size(sizeX, sizeY);
                            bOrder_QR.TabIndex = tableID;
                            bOrder_QR.TabStop = false;
                            bOrder_QR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            bOrder_QR.UseVisualStyleBackColor = false;


                            bOrder_QR.Visible = false;
                            if (payTypeName == "QR")
                                bOrder_QR.Visible = true;

                            bOrder_QR.Click += new System.EventHandler(this.bOrderQR_Click);

                            panelOrderlistAction.Controls.Add(bOrder_QR);



                            //// 
                            //// buttonOrder_Check
                            //// 
                            ///
                            bOrder_Check = new Button();

                            bOrder_Check.BackColor = System.Drawing.Color.Transparent;
                            bOrder_Check.BackgroundImage = global::AppRest.Properties.Resources.Checked;
                            bOrder_Check.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                            bOrder_Check.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
                            bOrder_Check.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                            bOrder_Check.Font = new System.Drawing.Font("Candara", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            bOrder_Check.ForeColor = System.Drawing.Color.Black;
                            bOrder_Check.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            bOrder_Check.Location = new System.Drawing.Point(100 + (sizeX * (i % yy)), 0 + (i * 2) + (sizeY * (i / yy)) - sizeY);
                            bOrder_Check.Name = "buttonOrder_Check" + billNo;
                            bOrder_Check.Text = billNo + "|" + billNo.Split('-')[1] + "|" + staffName + "|" + remark + "|" + payTypeName + "|" + billAmount.ToString() + "|" + cashRecAmount.ToString();
                            bOrder_Check.Size = new System.Drawing.Size(sizeX, sizeY);
                            bOrder_Check.TabIndex = tableID;
                            bOrder_Check.TabStop = false;
                            bOrder_Check.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            bOrder_Check.UseVisualStyleBackColor = false;

                            bOrder_Check.Click += new System.EventHandler(this.bOrderQR_Click);

                            panelOrderlistAction.Controls.Add(bOrder_Check);


                            //// 
                            //// buttonOrder_Order
                            //// 
                            bOrder_Order = new Button();

                            bOrder_Order.BackColor = System.Drawing.Color.Transparent;
                            bOrder_Order.BackgroundImage = global::AppRest.Properties.Resources.Manage;
                            bOrder_Order.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                            bOrder_Order.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
                            bOrder_Order.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                            bOrder_Order.Font = new System.Drawing.Font("Candara", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            bOrder_Order.ForeColor = System.Drawing.Color.Black;
                            bOrder_Order.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            bOrder_Order.Location = new System.Drawing.Point(150 + (sizeX * (i % yy)), 0 + (i * 2) + (sizeY * (i / yy)) - sizeY);
                            bOrder_Order.Name = "T" + tableID.ToString();
                            bOrder_Order.Text = billNo + "|" + billNo.Split('-')[1] + "|" + staffName + "|" + remark + "|" + payTypeName + "|" + billAmount.ToString() + "|" + cashRecAmount.ToString();
                            bOrder_Order.Size = new System.Drawing.Size(sizeX, sizeY);
                            bOrder_Order.TabIndex = tableID;
                            bOrder_Order.TabStop = false;
                            bOrder_Order.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            bOrder_Order.UseVisualStyleBackColor = false;

                            bOrder_Order.Click += new System.EventHandler(this.bTable_Click);

                            panelOrderlistAction.Controls.Add(bOrder_Order);



                            /////////////////////////////

                            bOrder_Void = new Button();

                            bOrder_Void.BackColor = System.Drawing.Color.Transparent;
                            bOrder_Void.BackgroundImage = global::AppRest.Properties.Resources.Minus;
                            bOrder_Void.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                            bOrder_Void.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
                            bOrder_Void.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                            bOrder_Void.Font = new System.Drawing.Font("Candara", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            bOrder_Void.ForeColor = System.Drawing.Color.Black;
                            bOrder_Void.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            //   bOrder_Void.Location = new System.Drawing.Point(1059, 55);
                            bOrder_Void.Location = new System.Drawing.Point(200 + (sizeX * (i % yy)), 0 + (i * 2) + (sizeY * (i / yy)) - sizeY);
                            bOrder_Void.Name = "buttonOrder_Void" + billNo;
                            bOrder_Void.Text = billNo + "|" + billNo.Split('-')[1] + "|" + staffName + "|" + remark + "|" + payTypeName + "|" + billAmount.ToString() + "|" + cashRecAmount.ToString();
                            bOrder_Void.Size = new System.Drawing.Size(sizeX, sizeY);
                            bOrder_Void.TabIndex = tableID;
                            bOrder_Void.TabStop = false;
                            bOrder_Void.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            bOrder_Void.UseVisualStyleBackColor = false;

                            bOrder_Void.Click += new System.EventHandler(this.bOrderQR_Click);

                            panelOrderlistAction.Controls.Add(bOrder_Void);

                            i++;
                        }
                    }

                    OrderlastRecord = OrderCurrentRecord;



                }


            }
            catch (Exception ex)
            {

                // MessageBox.Show(ex.Message);
            }

        }


        private void genObjMainTableRefreSh()
        {
            try
            {

                //   gentxtTopMenu();  

                tbs = gd.getMainTable(Login.zoneDefaultID, Login.zoneOrderOption);


                int tableCountOrder;
                int tablePrintBillFlag;
                int tableZoneID;


                this.mainTableYMargin = 8;

                string tid = "";

                Button bTable = new Button();

                foreach (Table t in tbs)
                {
                    tid = "T" + t.TableID.ToString();
                    bTable = (Button)this.Controls.Find(tid, true).FirstOrDefault();


                    tableCountOrder = (int)t.OrderQTY;
                    tablePrintBillFlag = t.TableCustID;
                    tableZoneID = t.ZoneID;

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
                        if (tablePrintBillFlag == 1)
                        {

                            bTable.BackColor = System.Drawing.Color.Orange; // รอจ่ายเงิน     
                            bTable.ForeColor = System.Drawing.Color.White;

                        }
                        else if (tablePrintBillFlag == 2)
                        {

                            bTable.BackColor = System.Drawing.Color.MidnightBlue; // รอจ่ายเงิน     
                            bTable.ForeColor = System.Drawing.Color.White;

                        }
                        else if (tablePrintBillFlag == 3)
                        {

                            bTable.BackColor = System.Drawing.Color.Red; // รอจ่ายเงิน     
                            bTable.ForeColor = System.Drawing.Color.White;

                        }
                    }

                }


            }
            catch (Exception ex)
            {

                //  MessageBox.Show(ex.Message);
            }

        }



        private void bTable_Click(object sender, EventArgs e)
        {

            Button bClick = (Button)sender;

            try
            {

                this.tableID = Int32.Parse(bClick.Name.Replace("T", ""));
                this.tableCustNo = bClick.TabIndex;

                 
                int result = 0;
                int productID = 4;

                int cashdrawerStatus = gd.getcashDrawerStatus(0);

                if (cashdrawerStatus != 1)
                    throw new Exception(cashdrawerStatus.ToString());


                List<TableStayOn> ts = gd.getAllTableStayON(this.tableID);

                if (Login.userStatus.ToLower() == "admin" || Login.userStatus.ToLower() == "cashier" || Login.userStatus.ToLower() == "manager")
                {
                    if (ts[0].UserStayOn != "N")
                        MessageBox.Show("มีผู้ใช้งานอยู่ : " + ts[0].UserStayOn);
                }
                else
                {

                    if (ts[0].UserStayOn != "N")
                        if (ts[0].UserStayOn != Login.userName)
                            throw new Exception("มีผู้ใช้งานอยู่ : " + ts[0].UserStayOn + "\r\n" + "ไม่สามารถใช้งานได้");
                }


                //  Login.orderType = 1;

                if (this.tableID < 1000)
                {
                    if (tableCustNo == 0 /*&& !(this.tableID == 19 || this.tableID == 20)*/)
                    {
                        if (defalutBarcodeOrder.ToLower() == "y")
                        {
                            result = gd.instOrderByTable(this.tableID, productID, Login.userID, "", 0);
                            this.LinkFormOrderTable(this.tableID, this.tableCustNo);
                        }
                        else
                        {
                            if (bClick.FlatAppearance.BorderSize == 1)
                                panelAddCust.Visible = true;
                            else if (bClick.FlatAppearance.BorderSize == 2)
                                panelInputOrder.Visible = true;
                            else
                                this.LinkFormOrderTable(this.tableID, this.tableCustNo);

                            //if (bClick.IsAccessible)
                            //    panelAddCust.Visible = true;
                            //else
                            //    panelInputOrder.Visible = true;


                        }

                    }
                    else
                    {

                        this.LinkFormOrderTable(this.tableID, this.tableCustNo);
                    }
                }
                else
                {
                    this.LinkFormOrderTable(this.tableID, this.tableID);
                }

            }
            catch (Exception ex)
            {
                if (FuncString.IsNumeric(ex.Message))
                {
                    if (ex.Message == "-1")
                        MessageBox.Show("ปิดสิ้นวันแล้วไม่สามารถขายของต่อได้");
                    else if (ex.Message == "-3")
                        MessageBox.Show("กรุณาเปิดรอบกะก่อน เนื่องจากเป็นกะแรกของวัน");
                    else
                        MessageBox.Show("กรุณาเปิดรอบกะก่อนจะมีการขาย");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void ButtonNum_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;


            try
            {

                bt.BackColor = System.Drawing.Color.Orange;
                this.tableCustNo = Int32.Parse(bt.Name.Replace("button_", " ").Trim());

                int productID = 4;

                int result = this.fn_AddOrder(productID, this.tableCustNo);
                   // result = this.fn_AddOrder(206, this.tableCustNo); // น้ำจิ้ม

                if (result <= 0)
                    MessageBox.Show("ไม่สามารถเพิ่มลูกค้าได้");


                this.LinkFormOrderTable(this.tableID, this.tableCustNo);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private int fn_AddOrder(int productID, int qtyAdditems)
        {

            int result = 0;


            for (int i = 1; i <= qtyAdditems; i++)
            {
                result += gd.instOrderByTable(this.tableID, productID, Login.userID, "", 0);  
            }

            return result;

        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {
            LinkFormAddTable();
        }

        private void LinkFormAddTable()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formAddTable == null)
            {
                formAddTable = new AddTable(this, 1);
            }
            Cursor.Current = Cursors.Default;
            if (formAddTable.ShowDialog() == DialogResult.OK)
            {
                formAddTable.Dispose();
                formAddTable = null;
            }
        }


        private void LinkFormOrderTable(int tableID, int tableCustID)
        {
            int xx = gd.updsTableStayOn(this.tableID, Login.userName);

            Cursor.Current = Cursors.WaitCursor;
            if (formLinkOrderTable == null)
            {
                formLinkOrderTable = new OrderTable(this, 1, tableID);
            }
            Cursor.Current = Cursors.Default;
            if (formLinkOrderTable.ShowDialog() == DialogResult.OK)
            {
                formLinkOrderTable.Dispose();
                formLinkOrderTable = null;
            }
        }

  

        string selectZoneName = "All";
        private void bZone_Click(object sender, EventArgs e)
        {
            try
            {
                Button bClick = (Button)sender;

                Login.zoneDefaultID = Int32.Parse(bClick.Name);
                selectZoneName = bClick.Text; 

                this.OrderlastRecord = 0;
                refreshOrder(); 

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonCloseAddCust_Click(object sender, EventArgs e)
        {
            panelAddCust.Visible = false;
        }

        private void buttonClose2_Click(object sender, EventArgs e)
        {
            panelInputOrder.Visible = false;
        }

        private void buttonScanBarcodeforSearch_Click(object sender, EventArgs e)
        {
            txtBoxSearchBarcode.Text = "";
            txtBoxSearchBarcode.Focus();
            //findTableIDFromName();
        }


        private void findTableIDFromName()
        {
            // int tableIDNext = 0;
            int result = 0;

            try
            {
                foreach (Table t in tbs)
                {
                    if (t.TableName.Trim() == txtBoxSearchTableName.Text)
                        tableID = t.TableID;
                }

                if (tableID > 0)
                {
                    result = gd.instOrderByTable(tableID, 4, Login.userID, "", 0);
                    this.LinkFormOrderTable(this.tableID, this.tableCustNo);
                }
                else
                {
                    txtBoxSearchTableName.Text = "";
                    MessageBox.Show("ไม่พบเลขห้อง");
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void txtBoxSearchBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            { 
                scanChangeORderStatus(); 
            }
        }

        private void scanChangeORderStatus()
        {
            textBoxMsgFromScanBarcode.Text = "";

            int result = gd.updsFlagSendOrderByBarcode(txtBoxSearchBarcode.Text, Login.userName, 1);
            string resultMsg = gd.updsFlagSendOrderByBarcodeMsg(txtBoxSearchBarcode.Text, Login.userName, 1);

            if (result == 0)
            {
                labelMessageScanBar.Text = "ไมพบ !! " + txtBoxSearchBarcode.Text;
                labelMessageScanBar.ForeColor = Color.Red;

                txtBoxSearchBarcode.Text = "";
                txtBoxSearchBarcode.Focus();
            }
            else if (result == 2)
            {

                labelMessageScanBar.Text = "       เสร็จ !!";
                labelMessageScanBar.ForeColor = Color.Green;

                barcodeScanLastest = txtBoxSearchBarcode.Text;
                txtBoxSearchBarcode.Text = "";
                txtBoxSearchBarcode.Focus();
                textBoxMsgFromScanBarcode.Text = resultMsg;


            }
            else if (result == 3)
            {

                labelMessageScanBar.Text = "       เสริฟ !!";
                labelMessageScanBar.ForeColor = Color.Green;

                barcodeScanLastest = txtBoxSearchBarcode.Text;
                txtBoxSearchBarcode.Text = "";
                txtBoxSearchBarcode.Focus();

                textBoxMsgFromScanBarcode.Text = resultMsg;

            }
            else if (result == -100)
            {
                labelMessageScanBar.Text = " SCAN จุดเดิม ไม่รับ ";
                labelMessageScanBar.ForeColor = Color.Red;
                textBoxMsgFromScanBarcode.Text = "กรุณา SCAN จุดบริการลูกค้า";

                txtBoxSearchBarcode.Text = "";
                txtBoxSearchBarcode.Focus();
            }


            if (result > 0)
                if (resultMsg.Contains("ตรง"))
                    pictureBoxStatus.Image = imgOntime;
                else if (resultMsg.Contains("ช้า"))
                    pictureBoxStatus.Image = imgLate;
                else
                    pictureBoxStatus.Image = imgNoBarcode;


        }


        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxUsername.Text = ""; 
        }

        private void buttonConfirmUsername_Click(object sender, EventArgs e)
        {
            int result = 0;
            string orderNo = "+" + textBoxUsername.Text;

            if (textBoxUsername.Text.Length > 0)
                result = gd.instOrderByTable(this.tableID, 10, Login.userID, orderNo, 0);

            this.LinkFormOrderTable(this.tableID, this.tableCustNo);

        }


        private void MButtonNum_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            string numstr = "";

            try
            {
                defaultColButOrderNoM();
                bt.BackColor = System.Drawing.Color.Orange;

                numstr = Int32.Parse(bt.Name.Replace("mbutton_", " ").Trim()).ToString();


                if (numstr == "13")
                    textBoxUsername.Text = textBoxUsername.Text.Substring(0, textBoxUsername.Text.Length - 1);
                else
                    textBoxUsername.Text += numstr;


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }


        private void defaultColButOrderNoM()
        {
            mbutton_01.BackColor = System.Drawing.Color.White;
            mbutton_02.BackColor = System.Drawing.Color.White;
            mbutton_03.BackColor = System.Drawing.Color.White;
            mbutton_04.BackColor = System.Drawing.Color.White;
            mbutton_05.BackColor = System.Drawing.Color.White;
            mbutton_06.BackColor = System.Drawing.Color.White;
            mbutton_07.BackColor = System.Drawing.Color.White;
            mbutton_08.BackColor = System.Drawing.Color.White;
            mbutton_09.BackColor = System.Drawing.Color.White;
            mbutton_00.BackColor = System.Drawing.Color.White;
            mbutton_13.BackColor = System.Drawing.Color.White;
        }

        private void buttonOpenCashDrawer_Click(object sender, EventArgs e)
        { 
            LinkFormEndday_Cashier();
        }
         
        private void LinkFormEndday_Cashier()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formLinkEndDay_Cashier == null)
            {
                formLinkEndDay_Cashier = new AddEndDays_Cashier(this, 0);
            }
            Cursor.Current = Cursors.Default;
            if (formLinkEndDay_Cashier.ShowDialog() == DialogResult.OK)
            {
                formLinkEndDay_Cashier.Dispose();
                formLinkEndDay_Cashier = null;
            }
        }

        private void KeyPassFinish(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.buttonOpenCashDrawer_Click(txtBoxCashDrawerPass, e);
            }
        }

        private void txtBoxCashDrawerPass_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtBoxSearchTableName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                findTableIDFromName();
            }
        }

        private void buttonAddPO_Click(object sender, EventArgs e)
        {
            LinkFormAddPO();
        }

        private void LinkFormAddPO()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddPO == null)
            {
                formAddPO = new AddPO(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddPO.ShowDialog() == DialogResult.OK)
            {
                formAddPO.Dispose();
                formAddPO = null;
            }
        }

        private void buttonAddPOBranch_Click(object sender, EventArgs e)
        {
            LinkFormAddPO_Branch();
        }

        private void LinkFormAddPO_Branch()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddPO_Branch == null)
            {
                formAddPO_Branch = new AddPO_Branch(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddPO_Branch.ShowDialog() == DialogResult.OK)
            {
                formAddPO_Branch.Dispose();
                formAddPO_Branch = null;
            }
        }



        private void buttonCloseBC_Click(object sender, EventArgs e)
        {
            panelShowBC.Visible = false;
        }

        private void buttonOpenBC_Click(object sender, EventArgs e)
        {
            panelShowBC.Visible = true;
            txtBoxSearchBarcode.Focus();
        }

        AddOrderMonitor formAddOrderMonitor;

        private void buttonOrderMonitor_Click(object sender, EventArgs e)
        {
            LinkFormAddOrderMonitor();
        }

        private void LinkFormAddOrderMonitor()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddOrderMonitor == null)
            {
                formAddOrderMonitor = new AddOrderMonitor(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddOrderMonitor.ShowDialog() == DialogResult.OK)
            {
                formAddOrderMonitor.Dispose();
                formAddOrderMonitor = null;
            }
        }

        private void buttonWebOrdering_Click(object sender, EventArgs e)
        {
            if (panelQRWebOrdering.Visible == true)
                panelQRWebOrdering.Visible = false;
            else
                panelQRWebOrdering.Visible = true;
        }

        private void buttonCLOSE_Click(object sender, EventArgs e)
        {
            panelQRWebOrdering.Visible = false;
        }

        private void printQRWebOrdering_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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

                e.Graphics.DrawString(" Web Ordering", fontSubHeader, brush, x + 80, y);
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

        private void buttonPrintQR_Click(object sender, EventArgs e)
        {
            printQRWebOrdering.Print();
        }

        AddMemCardRenew formAddMemCardRenew;

        private void buttonMemCardRenew_Click(object sender, EventArgs e)
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

        private void radioButtonOrder_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButtonAll.Checked)
                Login.zoneOrderOption = 1;
            else if (radioButtonOrder.Checked)
                Login.zoneOrderOption = 2;
            else if (radioButtonList.Checked)
                Login.zoneOrderOption = 3;

            refreshOrder();

        }


        private void refreshOrder()
        {
            panelOrderlist.Visible = false;
            MainTablePN.Controls.Clear();


            if (Login.zoneOrderOption == 1)
            {
                genObjMainTable();
            }
            else if (Login.zoneOrderOption == 2)
            {
                genObjMainTable_Order();
            }
            else if (Login.zoneOrderOption == 3)
            {
                genObjMainTable_List();
                
            }
        }

        private void buttonClosePanelOrderNo_Click(object sender, EventArgs e)
        {
            panelOrderListByTable.Visible = false;
        }

        int orderTableID = 0;
        int orderSubTableID = 0;
        string QRSlip = "";
        string orderTableName = "";
        string orderStaffName = "";
        string orderPayType = "";
        float orderCashRecAmount = 0;

        DataTable orderDetailFromStaff;


        private void bOrderQR_Click(object sender, EventArgs e)
        {


            try
            {
                panelOrderListByTable.Visible = true;



                 
                Button bOrder = (Button)sender;
                 

                this.orderTableID = bOrder.TabIndex;
                this.orderSubTableID = Int32.Parse(bOrder.Text.Split('|')[1].ToString());
                this.QRSlip = bOrder.Text.Split('|')[3];
                this.orderStaffName = bOrder.Text.Split('|')[2];
                this.orderTableName = bOrder.Text.Split('|')[0];
                this.orderPayType = bOrder.Text.Split('|')[4];
                this.totalSalesAmount =  float.Parse( bOrder.Text.Split('|')[5] );
                this.orderCashRecAmount = float.Parse( bOrder.Text.Split('|')[6] );

              

                textBoxOrderStaffName.Text = orderStaffName;
                textBoxOrderTableName.Text = orderTableName;
                textBoxOrderTotalSales.Text = totalSalesAmount.ToString("###,###.#0");
                textBoxOrderPayType.Text = orderPayType;
                textBoxCashRec.Text = orderCashRecAmount.ToString("###,###.#0");
                textBoxOrderChange.Text = (this.totalSalesAmount - this.orderCashRecAmount).ToString("###,###.#0");

                orderDetailFromStaff = gd.getStaffOrder_Detail(this.orderTableID, 0, 1, this.orderSubTableID);

                dataGridViewOrderDetail.DataSource = null;
                dataGridViewOrderDetail.DataSource = orderDetailFromStaff;

                if (dataGridViewOrderDetail.RowCount > 0)
                {
                    dataGridViewOrderDetail.Columns[0].Visible = false;
                    dataGridViewOrderDetail.Columns[1].Visible = false;
                    dataGridViewOrderDetail.Columns[2].HeaderText = "รายการ";
                    dataGridViewOrderDetail.Columns[3].HeaderText = "จำนวน";
                    dataGridViewOrderDetail.Columns[4].HeaderText = "ยอดเงิน";
                }


                if (this.QRSlip.Length > 0 && orderPayType == "QR")
                    pictureBoxImgOrder.Image = getImageFromURL(this.QRSlip);

                textBoxCashRec.Focus();


                buttonSendOrder.Visible = false;
                buttonCheckBill.Visible = false;

                checkBoxFlagSendOrder.Checked = true;

                if (orderPayType == "QR" || orderPayType == "Cash" || orderPayType == "Credit Card")
                    buttonCheckBill.Visible = true;
                else
                    buttonSendOrder.Visible = true;

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
                pictureBoxImgOrder.Image = global::AppRest.Properties.Resources.Logo_New;
            }
            finally
            {
                panelOrderListByTable.Visible = true;
            }


        }

        Image imgDefault = null;
        int flagGetImg = 0;

        public Image getImageFromURL(string URL)
        {

            Image img = null;
            WebClient wc = new WebClient();

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

        private void buttonVoidOrder_Click(object sender, EventArgs e)
        {
            string productName = "";
            string qty = "";
            List<string> txtPrint;
            string [] txtProduct;
            int len = 35;

 

            try
            {
                int result = gd.delStaffOrderConfirm(this.orderTableID, this.orderSubTableID, Login.userID, "Delete By Cashier POS");

                if (result > 0) {
                    MessageBox.Show("ลบรายการจาก Staff สำเร็จ");

                    panelOrderListByTable.Visible = false; 

                    txtOrderAppToPrintAll = "";

                    int noOrderNo = 0;

                    foreach( DataRow row  in orderDetailFromStaff.Rows)
                    { 
                        productName = row["ProductName"].ToString();
                        qty = row["Qty"].ToString();

                        txtOrderAppToPrintAll += "(" + qty + ") " + productName + "\n\r";
                    }

                    printVoidOrder.Print();

                    genObjMainTable_List();

                    panelOrderListByTable.Visible = false;
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message); 
            }

        }

        Table tableCheckBill;
        string flagLang = "EN";
        int orderGetPoint = 0;
        int orderCutPoint = 0;

        string defaultlangBill = ConfigurationSettings.AppSettings["DefaultlangBill"].ToString(); 
        string posIDConfig = ConfigurationSettings.AppSettings["POSIDConfig"].ToString();

        float totalDiscount = 0;
        float totalSalesAmount = 0;
        float totalServiceCharge = 0;
        float totalVAT = 0;
        string memCardID = "";
        int copyPrint = 0;
        string flagCheckBill = "N";
        int billID = 0;

        List<BillPayment> billPayment;

        private void buttonCheckBill_Click(object sender, EventArgs e)
        {
            try
            {
                string langBill = this.defaultlangBill; 

                billPayment = new List<BillPayment>();

                if( this.orderPayType == "QR")
                     billPayment.Add(new BillPayment(1 , 5,"QR", this.totalSalesAmount, "QR", "", ""));
                else if (this.orderPayType.Substring(0,4)  == "Cash")
                    billPayment.Add(new BillPayment(1, 1, "Cash", this.totalSalesAmount, textBoxCashRec.Text, textBoxOrderChange.Text, ""));
                else  
                    billPayment.Add(new BillPayment(1, 3, "ลงบิล", this.totalSalesAmount, "", "", ""));


                // Quick Service
                if(checkBoxFlagSendOrder.Checked)
                       printAllOrder();

              //  MessageBox.Show(orderPayType);
                
                if (this.orderPayType == "Cash" || this.orderPayType == "QR")
                {
                     
                    this.tableCheckBill = gd.getMainOrderByTable(this.orderTableID, langBill, 0, -1, 0, this.orderSubTableID);

                    this.billID = 0;
                    int result = gd.checkBillByTable(this.orderTableID , Login.userID, this.totalSalesAmount, this.totalDiscount, this.totalServiceCharge, this.totalVAT, 0, this.posIDConfig, this.memCardID, "", this.orderSubTableID, this.billPayment);


                    if (result <= 0)
                    {
                        MessageBox.Show("Error Check Bill By Table");
                    }
                    else
                    {

                        this.flagCheckBill = "Y";
                        this.billID =  result ;

                        copyPrint = 0;

                        if (posPrinters[0].FlagPrint.ToLower() == "y")
                        {

                            printCash.Print();
                            ++copyPrint;

                            if (ConfigurationSettings.AppSettings["flagPrintCopyAfterCheckBill"].ToString().ToLower() == "y")
                                printCash.Print();

                        }
                    }

                }

                panelOrderListByTable.Visible = false;
                genObjMainTable_List();
            }
            catch (Exception ex)
            {

            }
        }

        List<POSPrinter> posPrinters;


        int flagPrinter2 = 0;

        string txtOrderAppToPrintOne = "";
        string txtOrderAppToPrint = "";
        string txtOrderAppToPrintAll = "";
        string strPrinterOrder = "";
        string orderBarcodePrint = ""; 

        string flagOrderSendNotPrint = ConfigurationSettings.AppSettings["FlagOrderSendNotPrint"].ToString();
        string flagOrderShowPrice = ConfigurationSettings.AppSettings["FlagOrderShowPrice"].ToString();
        string flagOrderCheckerShowPrice = ConfigurationSettings.AppSettings["FlagOrderCheckerShowPrice"].ToString();
        string flagKitchenMonitor = ConfigurationSettings.AppSettings["FlagKitchenMonitor"].ToString();
        string flagPrintOrderChecker = ConfigurationSettings.AppSettings["FlagPrintOrderChecker"].ToString();


        string restlink = ConfigurationSettings.AppSettings["RestLink"].ToString();
        string fblink = ConfigurationSettings.AppSettings["FBLink"].ToString();
        string iglink = ConfigurationSettings.AppSettings["IGLink"].ToString();
        string qrlink = ConfigurationSettings.AppSettings["QRLink"].ToString();
        string linelink = ConfigurationSettings.AppSettings["LINELink"].ToString();

        private void printAllOrder()
        {


            int i = 0;
            int printerNo = 0;

            string subPrintName = "";

            try
            {

                Table table = gd.getMainOrderByTable(this.orderTableID, "TH", 0, 1, 0, this.orderSubTableID);

              //  Table table = gd.getMainOrderByTable(this.tableID, this.flagLang, 0, 1, this.memIDSelected, this.subOrderID);

                //  this.tablePrint = table;

                string tableName;

                tableName = table.TableName;

                List<Order> orders = table.Order;

             //  this.orderPrint = orders;

                string strNum = "";

                txtOrderAppToPrintOne = "";
                txtOrderAppToPrint = "";
                txtOrderAppToPrintAll = "";
                 

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

                    if (subPrintName == "ALL")
                    {
                        if (noPrinter > 0)
                        {

                            if (printerNo == noPrinter)
                            {
                                totalpriceperorder += o.OrderAmount;

                                // ingredian same order 
                                if (txtProductName.Substring(0, 1) == "-")
                                {
                                    if (o.OrderQTY == 1)
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

                e.Graphics.DrawString("[[ Order Checker ]]", fontTable, brush, x + 5, y);

                y += 30;

                e.Graphics.DrawString("Print All Order " + " ( Order by : " + Login.userName + ")", fontSubHeader, brush, x + 5, y);


                if (this.orderStaffName.Length > 0)
                {

                    y += 25;
                    e.Graphics.DrawString("[[[[ Staff Name : " + this.orderStaffName + " ]]]]]", fontSubHeader, brush, x + 5, y);

                }


                y += 15;

                e.Graphics.DrawString(" " + this.orderTableName, fontTable, brush, x + 5, y);

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

         
        private void OnPrintSendOrder(object sender, PrintPageEventArgs e)
        {
            try
            {

                string txtPrint = "";

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

         

                 if (this.txtOrderAppToPrintOne != "")
                {

                    y += 10;

                    e.Graphics.DrawString("ส่ง " + strPrinterOrder + " ( Order by : " + Login.userName + ")", fontSubHeader, brush, x + 5, y);

                    y += 15;

                    if (flagPrinter2 == 1)
                        e.Graphics.DrawString("[[  Combine Menu  ]]", fontTable, brush, x + 5, y);
                    else
                        e.Graphics.DrawString("[[  Send Order  ]]", fontTable, brush, x + 5, y);

                    y += 30;

                    e.Graphics.DrawString(this.orderTableName , fontHeaderBIG, brush, x + 0, y);

                    y += 60;
                }
                else
                {

                    e.Graphics.DrawString("Send " + strPrinterOrder + " ( Order by : " + Login.userName + ")", fontSubHeader, brush, x + 5, y);


                    if (this.orderStaffName.Length > 0)
                    {

                        y += 15;
                        e.Graphics.DrawString("[[[[ Staff Name : " + this.orderStaffName + " ]]]]]", fontSubHeader, brush, x + 5, y);

                    }

                    y += 15;

                    e.Graphics.DrawString(" " + this.orderTableName, fontTable, brush, x + 5, y);

                    y += 25;

                }

                if (this.orderStaffName.Length > 0)
                {
                    e.Graphics.DrawString("[[[[ Staff Name : " + this.orderStaffName + " ]]]]]", fontSubHeader, brush, x + 5, y);
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
                    string[] ArrgrestTaxRD = restTaxRD.Split(':');

                    int ii = 0;
                    foreach (string s in ArrgrestTaxRD)
                    {
                        if (s == Login.posBranchID.ToString())
                        {
                            appTaxRD = ArrgrestTaxRD[ii + 1];
                        }

                        ii++;
                    }
                }


                ///////////////////////////


                Brush brush = new SolidBrush(Color.Black);
                 
                Font fontHeader = new Font("Arail", 12, FontStyle.Bold);
                Font fontTable = new Font("Arail", 11, FontStyle.Bold);
                Font fontSubHeader = new Font("Arail", 9, FontStyle.Bold);
                Font fontFooter = new Font("Arail", 7, FontStyle.Regular);
                Font fontBody = new Font("Arail", 8, FontStyle.Regular);
                Font fontBodylist = new Font("Arail", 9, FontStyle.Regular);
                Font fontNum = new Font("Consolas", 9, FontStyle.Regular);

                Bitmap img = global::AppRest.Properties.Resources.Logo_New;



                BarCode.GenBarCode(this.billID.ToString());
                Image barcodeImg = BarCode.resultBarcode;

                e.Graphics.DrawImage(barcodeImg, x + 70, y, 150, 50);

                y += 50;


                if (this.copyPrint == 99)
                {
                    e.Graphics.DrawString("ใบสรุป Order", fontBody, brush, x + 120, y);

                    y += 12;

                }
                else
                {

                    if (this.flagCheckBill == "Y")
                        if (this.copyPrint > 0)
                            e.Graphics.DrawString("copy", fontBody, brush, x + 120, y);
                        else
                            e.Graphics.DrawString("ใบเสร็จรับเงิน", fontBody, brush, x + 100, y);
                    else
                        e.Graphics.DrawString("ใบรองบิล", fontBody, brush, x, y);



                    y += 12;


                    e.Graphics.DrawImage(img, x + 85, y, 110, 110);
                    y += 110;



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
                string tableName = this.orderTableName;

 

                e.Graphics.DrawString(" " + strDate + " " + strTime, fontSubHeader, brush, x + 150, y);

                y += 10;

                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 10;
                e.Graphics.DrawString(" " + tableName, fontHeader, brush, x + 10, y);
              //  e.Graphics.DrawString("Gst " + this.custNumber.ToString(), fontHeader, brush, x + 200, y);
                y += 20;

                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 15;

                //// Print Order

                Table table = tableCheckBill; //gd.getMainOrderByTable(this.tableID, langBill, 0, 0);  
                 

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

                        if (!((o.ProductID > 10 && o.ProductID <= 100)) && o.OrderNo != 99 && o.ProductID != 4)
                        {

                            if (o.ProductName.Substring(0, 1) == "-" && o.OrderQTY == 1)
                                e.Graphics.DrawString("", fontBodylist, brush, x + 0, y);
                            else
                                e.Graphics.DrawString(str2, fontBodylist, brush, x + 0, y);


                            if (copyPrint != 99)
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



                //if (this.salesOrderGroup1 > 0)
                //{
                //    y += 15;
                //    txtOrder = "Sub Total";
                //    txtAmt = (this.salesOrderGroup1 + this.salesOrderGroup2 + this.salesOrderGroup3 + this.salesOrderGroup4 + this.orderPromotion).ToString("###,###.#0");
                //    txtAmt = String.Format("{0,10}", txtAmt);
                //    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                //    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);
                //}




                if (this.totalDiscount > 0)
                {


                    //    if (discount1 > 0)
                    //    {
                    //        y += 15;
                    //        txtOrder = "" + this.discountGroup[0].DiscountGroupNameEN + " " + (discount1 * 100).ToString();
                    //        txtAmt = (this.salesOrderGroup1 * discount1).ToString("###,###.#0");
                    //        txtAmt = String.Format("{0,10}", txtAmt);
                    //        e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    //        e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    //    }

                    //    if (discount2 > 0)
                    //    {
                    //        y += 15;
                    //        txtOrder = "" + this.discountGroup[1].DiscountGroupNameEN + " " + (discount2 * 100).ToString();
                    //        txtAmt = (this.salesOrderGroup2 * discount2).ToString("###,###.#0");
                    //        txtAmt = String.Format("{0,10}", txtAmt);
                    //        e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    //        e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    //    }

                    //    if (discount3 > 0)
                    //    {
                    //        y += 15;
                    //        txtOrder = "" + this.discountGroup[2].DiscountGroupNameEN + " " + (discount3 * 100).ToString();
                    //        txtAmt = (this.salesOrderGroup3 * discount3).ToString("###,###.#0");
                    //        txtAmt = String.Format("{0,10}", txtAmt);
                    //        e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    //        e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    //    }

                    //    if (discount4 > 0)
                    //    {
                    //        y += 15;
                    //        txtOrder = "" + this.discountGroup[3].DiscountGroupNameEN + " " + (discount4 * 100).ToString();
                    //        txtAmt = (this.salesOrderGroup4 * discount4).ToString("###,###.#0");
                    //        txtAmt = String.Format("{0,10}", txtAmt);
                    //        e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    //        e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    //    }

                    //    if (discountAmt > 0)
                    //    {
                    //        y += 15;
                    //        txtOrder = " Discount Amount (B) ";
                    //        txtAmt = (discountAmt).ToString("###,###.#0");
                    //        txtAmt = String.Format("{0,10}", txtAmt);
                    //        e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    //        e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    //    }

                    //    if (totalDiscount > 0)
                    //    {
                    //        y += 15;
                    //        txtOrder = " Total Discount ";
                    //        txtAmt = (totalDiscount).ToString("###,###.#0");
                    //        txtAmt = String.Format("{0,10}", txtAmt);
                    //        e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    //        e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    //    }

                    //}

                    //if (this.totalServiceCharge > 0)
                    //{
                    //    y += 15;
                    //    txtOrder = "Service " + ((float)(this.servicePercent * 100)).ToString() + "%";
                    //    txtAmt = this.totalServiceCharge.ToString("###,###.#0");
                    //    txtAmt = String.Format("{0,10}", txtAmt);
                    //    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    //    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    //}
                }

                    if (this.totalVAT > 0)
                    {

                        //y += 15;
                        //txtOrder = "Amt Before VAT " + ((float)(this.taxPercent * 100)).ToString() + "%";
                        //txtAmt = (this.totalSalesAmount - this.totalVAT - this.salesNonVAT).ToString("###,###.#0");
                        //txtAmt = String.Format("{0,10}", txtAmt);
                        //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                        //e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                        //y += 15;
                        //txtOrder = "Amt Non VAT ";
                        //txtAmt = this.salesNonVAT.ToString("###,###.#0");
                        //txtAmt = String.Format("{0,10}", txtAmt);
                        //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                        //e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                        //y += 15;
                        //txtOrder = "VAT " + ((float)(this.taxPercent * 100)).ToString() + "%";
                        //txtAmt = this.totalVAT.ToString("###,###.#0");
                        //txtAmt = String.Format("{0,10}", txtAmt);
                        //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                        //e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    }




                    y += 25;
                    txtOrder = "Total ";
                    txtAmt = this.totalSalesAmount.ToString("###,###.#0");
                    e.Graphics.DrawString(txtOrder, fontTable, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontTable, brush, x + 190, y);


                    y += 20;


                    string remark = "";
                    string custName = "";



                    //  custName = comboBoxListCust.Text;
                    remark = "";//textBoxReason.Text;


                    //textBoxCustShare

                    e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);



                    y += 15;

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
                     

                y += 15;
                y += 15;
                 

                e.Graphics.DrawString(" THANK YOU / ขอบคุณค่ะ ", fontBody, brush, x + 70, y);
                y += 20;
                e.Graphics.DrawImage(global::AppRest.Properties.Resources.Logo_FB, x + 75, y - 5, 18, 18);
                e.Graphics.DrawString(this.fblink, fontBody, brush, x + 98, y);

                 

                e.HasMorePages = false;

            }
            catch (Exception ex)
            {

            }
        } 

        private void textBoxCashRec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (FuncString.IsNumeric(textBoxCashRec.Text) && FuncString.IsNumeric(textBoxOrderTotalSales.Text))
                {
                    textBoxOrderChange.Text = (this.totalSalesAmount - float.Parse(textBoxCashRec.Text)).ToString("###,###.#0");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void buttonSendOrder_Click(object sender, EventArgs e)
        {

            try
            {
                if (checkBoxFlagSendOrder.Checked)
                    printAllOrder();
            }
            catch (Exception ex)
            {

            } 
   
        }

        private void printVoidOrder_PrintPage(object sender, PrintPageEventArgs e)
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

                e.Graphics.DrawString("[[    Void Order     ]]", fontTable, brush, x + 5, y);

                
                
                if (this.orderStaffName.Length > 0)
                {

                    y += 30;
                    e.Graphics.DrawString("[[[[ Staff Name : " + this.orderStaffName + " ]]]]]", fontSubHeader, brush, x + 5, y);

                }


                y += 20;

                e.Graphics.DrawString(" " + this.orderTableName, fontTable, brush, x + 5, y);

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
    }
}
