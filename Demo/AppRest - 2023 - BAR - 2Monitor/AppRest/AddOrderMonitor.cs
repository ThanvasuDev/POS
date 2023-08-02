using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI;
using System.Globalization;
using System.Configuration;
using System.Drawing.Printing;
using System.Net;
using System.IO;

namespace AppRest
{
    public partial class AddOrderMonitor : AddDataTemplate
    {
        GetDataRest gd;
        List<OrderStatus> orderNonF;
        List<OrderStatus> orderF;


        List<POSPrinter> posPrinters;
        List<Table> tbs; 
        int kitchenIDSelected = 0;
        int selectedTableID = 0;

        public AddOrderMonitor(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();

            this.Height = 764;
            this.Width = 1028;


            imgPath = ConfigurationSettings.AppSettings["PathImages"].ToString();

            gd = new GetDataRest();

            posPrinters = new List<POSPrinter>();
            posLoadPrinter(); 

            getComboAllPrinter();

            genObjOrderNonFinish_1();
            genObjOrderNonFinish_2();
            genObjOrderNonFinish_3(); 

            genObjOrderFinish();
            genObjMainTable();

            textBoxOrderNo.Select();
            panelOrderNo.Visible = false;

            var timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 6000; //10 seconds
            timer.Start();
             
           
        }
        void timer_Tick(object sender, EventArgs e)
        {


            genObjOrderNonFinish_1();
            genObjOrderNonFinish_2();
            genObjOrderNonFinish_3();

            genObjOrderFinish();
            genObjMainTable();

        }

        private void genObjMainTable()
        {
            try
            {


                MainTablePN.Controls.Clear();
                 
                tbs = gd.getMainTable(0);

                int tableID;
                string tableName;
                string tableFlagUse;
                int tableCountOrder;
                int tablePrintBillFlag;
                int tableCustID;
                int tableZoneID;
                Button bTable;

                int sizeX = 80;
                int sizeY = 45;

                int yy = 2;

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


                    if (tableFlagUse.ToLower() == "y" && tableCountOrder > 0)
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
                            bTable.FlatStyle = System.Windows.Forms.FlatStyle.Popup;

                            bTable.Font = new System.Drawing.Font("Century Gothic", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));

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
            finally
            {
               // textBoxOrderNo.Select();
            }

        }

        private void bTable_Click(object sender, EventArgs e)
        {
            try
            {
                Button bClick = (Button)sender; 
                this.selectedTableID =  Int32.Parse(bClick.Name);
                 
                genObjOrderNonFinish_1();
                genObjOrderNonFinish_2();
                genObjOrderNonFinish_3();
                genObjOrderNonFinish_4();
                genObjOrderFinish();


            }
            catch (Exception ex)
            {

            }
            finally
            {
               // textBoxOrderNo.Select();
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
             

        }


        private void getComboAllPrinter()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "= Select Printer ="));

            foreach (POSPrinter ps in posPrinters)
            {
                if (ps.FlagPrint.ToLower() == "y" && ps.PrinterNo > 0 && ps.PrinterNo < 21)
                {
                    data.Add(new KeyValuePair<int, string>(ps.PrinterNo, ps.PrinterStrName + " > " + ps.PrinterName));
                }
            }

            data.Add(new KeyValuePair<int, string>(99, "= Delete Category ="));


            // Clear the combobox
            comboBoxPrinterName.DataSource = null;
            comboBoxPrinterName.Items.Clear();

            // Bind the combobox
            comboBoxPrinterName.DataSource = new BindingSource(data, null);
            comboBoxPrinterName.DisplayMember = "Value";
            comboBoxPrinterName.ValueMember = "Key";

            //  comboBoxAllGroupCat.SelectedValue = selectedGruopCat;


        }


        private void genObjOrderNonFinish_1()
        {

            try
            {

                labelUpdateTime.Text = DateTime.Now.ToString();

                string strTxtDisplay = "";

                Button bCat;

                int sizeX = 102;
                int sizeY = 80;
                int yy = 2;

                int i = 0;

                orderNonF = gd.getOrderStatus(2, this.selectedTableID, 1);

              

                panelOrderNonFinish_1.Controls.Clear();

                int seqNo = 1;
                int lastBill = 0;
                int newBill = 0;

                foreach (OrderStatus o in orderNonF)
                {
                     newBill = o.TrnID;

                     if (lastBill == newBill)
                         seqNo++;
                     else
                         seqNo = 1;

                    strTxtDisplay = "[[ " + o.TableName + " ]]" + "\n\r" + o.GetOrderTime.ToString("HH:mm") + "\n\r" + o.OrderName + "\n\r" + o.OrderRemark;
                   
                    bCat = new Button(); 
                    bCat.FlatStyle = System.Windows.Forms.FlatStyle.Flat; 

                    if (o.OrderRemark.Contains("HOLD"))
                    {
                        bCat.BackColor = System.Drawing.Color.Yellow;
                        bCat.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        bCat.BackColor = System.Drawing.Color.Green;
                        bCat.ForeColor = System.Drawing.Color.White;
                    }


                    bCat.Font = new System.Drawing.Font("Century Gothic", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                    bCat.Name = o.OrderBarcode.ToString();
                    bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                    bCat.TextAlign = ContentAlignment.TopCenter; 
                    
                    bCat.TabIndex = 3;
                    //bCat.TabIndex = o.TrnID;
                    bCat.Text = strTxtDisplay;
                    bCat.UseVisualStyleBackColor = false;

                    bCat.Click += new System.EventHandler(this.updateStatus_Click); 

                    panelOrderNonFinish_1.Controls.Add(bCat);
                    i++;

                    lastBill = newBill;

                }

            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);

            }

        }

        private void genObjOrderNonFinish_2()
        {

            try
            {

                labelUpdateTime.Text = DateTime.Now.ToString();

                string strTxtDisplay = "";

                Button bCat;

                int sizeX = 102;
                int sizeY = 80;
                int yy = 2;

                int i = 0;

                orderNonF = gd.getOrderStatus(2, this.selectedTableID, 2);



                panelOrderNonFinish_2.Controls.Clear();

                int seqNo = 1;
                int lastBill = 0;
                int newBill = 0;

                foreach (OrderStatus o in orderNonF)
                {
                    newBill = o.TrnID;

                    if (lastBill == newBill)
                        seqNo++;
                    else
                        seqNo = 1;

                    strTxtDisplay = "[[ " + o.TableName + " ]]" + "\n\r" + o.GetOrderTime.ToString("HH:mm") + "\n\r" + o.OrderName + "\n\r" + o.OrderRemark;

                    bCat = new Button();
                    bCat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

                    if (o.OrderRemark.Contains("HOLD"))
                    {
                        bCat.BackColor = System.Drawing.Color.Yellow;
                        bCat.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        bCat.BackColor = System.Drawing.Color.Green;
                        bCat.ForeColor = System.Drawing.Color.White;
                    }


                    bCat.Font = new System.Drawing.Font("Century Gothic", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                    bCat.Name = o.OrderBarcode.ToString();
                    bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                    bCat.TextAlign = ContentAlignment.TopCenter;

                    bCat.TabIndex = 3;
                    //bCat.TabIndex = o.TrnID;
                    bCat.Text = strTxtDisplay;
                    bCat.UseVisualStyleBackColor = false;

                    bCat.Click += new System.EventHandler(this.updateStatus_Click);

                    panelOrderNonFinish_2.Controls.Add(bCat);
                    i++;

                    lastBill = newBill;

                }

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);

            }

        }

        private void genObjOrderNonFinish_3()
        {

            try
            {

                labelUpdateTime.Text = DateTime.Now.ToString();

                string strTxtDisplay = "";

                Button bCat;

                int sizeX = 102;
                int sizeY = 80;
                int yy = 2;

                int i = 0;

                orderNonF = gd.getOrderStatus(2, this.selectedTableID, 3);



                panelOrderNonFinish_3.Controls.Clear();

                int seqNo = 1;
                int lastBill = 0;
                int newBill = 0;

                foreach (OrderStatus o in orderNonF)
                {
                    newBill = o.TrnID;

                    if (lastBill == newBill)
                        seqNo++;
                    else
                        seqNo = 1;

                    strTxtDisplay = "[[ " + o.TableName + " ]]" + "\n\r" + o.GetOrderTime.ToString("HH:mm") + "\n\r" + o.OrderName + "\n\r" + o.OrderRemark;

                    bCat = new Button();
                    bCat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

                    if (o.OrderRemark.Contains("HOLD"))
                    {
                        bCat.BackColor = System.Drawing.Color.Yellow;
                        bCat.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        bCat.BackColor = System.Drawing.Color.Green;
                        bCat.ForeColor = System.Drawing.Color.White;
                    }


                    bCat.Font = new System.Drawing.Font("Century Gothic", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                    bCat.Name = o.OrderBarcode.ToString();
                    bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                    bCat.TextAlign = ContentAlignment.TopCenter;

                    bCat.TabIndex = 3;
                    //bCat.TabIndex = o.TrnID;
                    bCat.Text = strTxtDisplay;
                    bCat.UseVisualStyleBackColor = false;

                    bCat.Click += new System.EventHandler(this.updateStatus_Click);

                    panelOrderNonFinish_3.Controls.Add(bCat);
                    i++;

                    lastBill = newBill;

                }

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);

            }

        }

        private void genObjOrderNonFinish_4()
        {

            try
            {

                labelUpdateTime.Text = DateTime.Now.ToString();

                string strTxtDisplay = "";

                Button bCat;

                int sizeX = 102;
                int sizeY = 80;
                int yy = 2;

                int i = 0;

                orderNonF = gd.getOrderStatus(2, this.selectedTableID, 4);



                panelOrderNonFinish_4.Controls.Clear();

                int seqNo = 1;
                int lastBill = 0;
                int newBill = 0;

                foreach (OrderStatus o in orderNonF)
                {
                    newBill = o.TrnID;

                    if (lastBill == newBill)
                        seqNo++;
                    else
                        seqNo = 1;

                    strTxtDisplay = "[[ " + o.TableName + " ]]" + "\n\r" + o.GetOrderTime.ToString("HH:mm") + "\n\r" + o.OrderName + "\n\r" + o.OrderRemark;

                    bCat = new Button();
                    bCat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

                    if (o.OrderRemark.Contains("HOLD"))
                    {
                        bCat.BackColor = System.Drawing.Color.Yellow;
                        bCat.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        bCat.BackColor = System.Drawing.Color.Green;
                        bCat.ForeColor = System.Drawing.Color.White;
                    }


                    bCat.Font = new System.Drawing.Font("Century Gothic", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                    bCat.Name = o.OrderBarcode.ToString();
                    bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                    bCat.TextAlign = ContentAlignment.TopCenter;

                    bCat.TabIndex = 3;
                    //bCat.TabIndex = o.TrnID;
                    bCat.Text = strTxtDisplay;
                    bCat.UseVisualStyleBackColor = false;

                    bCat.Click += new System.EventHandler(this.updateStatus_Click);

                    panelOrderNonFinish_4.Controls.Add(bCat);
                    i++;

                    lastBill = newBill;

                }

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);

            }

        }

        private void genObjOrderFinish()
        {

            try
            {

                string strTxtDisplay = "";

                Button bCat;

                int sizeX = 105;
                int sizeY = 80;
                int yy = 1;

                int i = 0;

                orderF = gd.getOrderStatus(3, this.selectedTableID, this.kitchenIDSelected);
                
                panelOrderFinish.Controls.Clear();

                foreach (OrderStatus o in orderF)
                {
                    strTxtDisplay = "[[ " + o.TableName + " ]]" + " > " + o.GetOrderTime.ToString("HH:mm")  + "\n\r"  + o.OrderName + "\n\r" + o.OrderRemark + "\n\r" + " Cook Time : [" + (o.CookOrderTime - o.GetOrderTime).Minutes.ToString() + "]";

                    bCat = new Button();

                   

                    bCat.BackColor = System.Drawing.Color.BlueViolet;
                    bCat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    bCat.ForeColor = System.Drawing.Color.White; 
                    bCat.Name = o.OrderBarcode.ToString();
                    bCat.Font = new System.Drawing.Font("Century Gothic", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    bCat.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                    bCat.Size = new System.Drawing.Size(sizeX, sizeY);
                    bCat.TabIndex = 2;
                    bCat.Text = strTxtDisplay;
                    bCat.TextAlign = ContentAlignment.TopCenter;
                    bCat.UseVisualStyleBackColor = false;
                     

 

                    bCat.Click += new System.EventHandler(this.updateStatus_Click);

                    panelOrderFinish.Controls.Add(bCat);
                    i++;
                }

            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);

            }

        }


     
         
          private void updateStatus_Click(object sender, EventArgs e)
        {
            try
            {
                Button bClick = (Button)sender;

                string orderName = bClick.Text;
                string orderBarcode = bClick.Name;
                int orderStatus = bClick.TabIndex;
                int result = 0 ;

                if (orderStatus == 2)
                {
                    if (MessageBox.Show("คุณแน่ใจว่าจะยกเลิกการทำ: " + orderName + " หรือไม่ ?", "ยกเลิก ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        result = gd.updsOrderStatus(orderStatus, orderBarcode);
                        if (result <= 0)
                            MessageBox.Show("Error Update Cooking");
                    }
                }
                else
                {

                    result = gd.updsOrderStatus(orderStatus, orderBarcode);
                    if (result <= 0)
                        MessageBox.Show("Error Update Cooking");
                }

                genObjOrderNonFinish_1(); 
                genObjOrderFinish();
              

            }
            catch (Exception ex)
            {

            }

        }

          private void comboBoxPrinterName_SelectedIndexChanged(object sender, EventArgs e)
          {
              try
              {


                  this.kitchenIDSelected = Int32.Parse(comboBoxPrinterName.SelectedValue.ToString());
                  this.selectedTableID = 0;
                  genObjOrderNonFinish_1();
                  genObjOrderNonFinish_2();
                  genObjOrderNonFinish_3();
                   genObjOrderNonFinish_4();
                   genObjOrderFinish();
              }
              catch (Exception ex)
              {

              }
          }


       

          string imgPath = ""; 
          Image imgProduct = null;
          Image imgDefault = null; 
          int flagGetImg = 0; 


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

        DataTable orderDetailFromStaff;

        private void textBoxOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    panelOrderNo.Visible = true;
                    int BillNo = 0;


                    if (FuncString.IsNumeric(textBoxOrderNo.Text.Trim()))
                    {
                        BillNo = Int32.Parse(textBoxOrderNo.Text.Trim());

                        MessageBox.Show(BillNo.ToString());

                        orderDetailFromStaff = gd.getStaffOrder_Detail(0, 0, 2, BillNo);

                        MessageBox.Show(orderDetailFromStaff.Rows.Count.ToString());

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

                        textBoxOrderNo.Text = "";
                        textBoxOrderNo.Select();

                        labelOrderMessage.Text = "รับสินค้าสำเร็จ !! ";
                    }
                } 

            }
            catch (Exception ex)
            {
                labelOrderMessage.Text = "ไม่พบเลขที่ Order นี้ ";
            }
            finally
            {
               
            }
        }

        private void buttonClosePanelOrderNo_Click(object sender, EventArgs e)
        {
            panelOrderNo.Visible = false;
        }
    }
}
