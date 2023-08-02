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

namespace AppRest
{
    public partial class MainOrder : MainTemplateS
    {
        GetDataRest gd;


        string trnPeriodSect;
        string trnDateSect;
        int trnBillIDSect = 0; 

        Payment payment;
        Payment paymentDay;
        List<Transaction> trn;
        List<Transaction> trnDay;
        TrnMax trnMax;

        List<TrnDate> listPeriods;
        List<TrnDate> listDates;
        List<Bill> listBills;

        string flagLang;

        double servicePercent;
        double taxPercent;

        MainTable formMainTable;
       

        string printerCashName;

        FromRptBillReport formFromRptBillReport;
        FromRptBillReport formFromRptBillReportCopy;

        FromRptBillNewReport formFromRptBillNewReport;

        List<Customer> allCusts;
        DataTable dataTableAllCust;

        string posIDConfig;

        AddCustomer formAddCustomer;

        int flagdelBill;

        string restlink;
        string fblink;
        string iglink;
        string qrlink;

        List<Member> allMembers;

        int flagFD = 0;
        

        string  msgtoLINE = "";


        public MainOrder(Form frmlkFrom, int flagFrmClose)
        {
            InitializeComponent();
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
          

            this.Text = this.Text + " ( By : " + Login.userName + ")";

            buttonLinkOrder.BackColor = System.Drawing.Color.Gray;

            restlink = ConfigurationSettings.AppSettings["RestLink"].ToString();
            fblink = ConfigurationSettings.AppSettings["FBLink"].ToString();
            iglink = ConfigurationSettings.AppSettings["IGLink"].ToString();
            qrlink = ConfigurationSettings.AppSettings["QRLink"].ToString();

            if (Login.isFrontPOS)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }

            this.Width = 1024;
            this.Height = 764;
            if (Login.isFrontWide)
            {
                this.Width = 1280;
            }

            gd = new GetDataRest();

            allMembers = gd.getListAllMember();

            try
            {


                // List Cust
                allCusts = gd.getListAllCustomer();
                getComboAllCust();

                // allCustDataTable

                dataTableAllCust = gd.getDataAllCustomer();
                dataGridViewAllMember.DataSource = dataTableAllCust;

                // Checnage + Hide Column

                dataGridViewAllMember.Columns[2].Visible = false;


                trnMax = gd.getTrnMax(); 
                trnPeriodSect = trnMax.MaxPeriod;
                trnDateSect = trnMax.MaxDate;
                trnBillIDSect = trnMax.MaxTrn;

                listPeriods = gd.getTrnPeriod();
                listDates = gd.getTrnDate();
                listBills = new List<Bill>();

                getComboAllPeriod();

                payment = gd.getTrnPayment(trnBillIDSect);

                genDataPayment();

                trn = gd.getTrnOrder(trnBillIDSect);
                dataGridViewOrder.DataSource = trn;

                dataGridViewOrder.Columns[0].Visible = false;
                dataGridViewOrder.Columns[1].Visible = false;
                dataGridViewOrder.Columns[4].Visible = false;

                flagLang = "TH";

                taxPercent = Double.Parse(ConfigurationSettings.AppSettings["TaxPercent"].ToString());
                servicePercent = Double.Parse(ConfigurationSettings.AppSettings["ServiceChargePercent"].ToString());

                printerCashName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();

                posIDConfig = ConfigurationSettings.AppSettings["POSIDConfig"].ToString();

                printBill.PrinterSettings.PrinterName = printerCashName;
                printBillDay.PrinterSettings.PrinterName = printerCashName; 
                printDayByProduct.PrinterSettings.PrinterName = printerCashName;
                printBillDay_Depart.PrinterSettings.PrinterName = printerCashName;
                printBill_Reprint.PrinterSettings.PrinterName = printerCashName;

                printBillProduct1.PrinterSettings.PrinterName = printerCashName;
                printBillProduct2.PrinterSettings.PrinterName = printerCashName;


                // เพิ่ม Cust 

                this.flagdelBill = 0; 

                buttonSummary.Visible = false;
                textBoxCodeDelBill.Visible = false;
                buttonDelBill.Visible = false;
                buttonSumSalesByday.Visible = false;

                if ( Login.userStatus.ToLower() == "cashier")
                {
                    // buttonSummary.Visible = true;
                    //textBoxCodeDelBill.Visible = true;
                    buttonDelBill.Visible = true;
                    buttonSumSalesByday.Visible = true;
                    buttonSummary.Visible = true;
                }


                if (Login.userStatus.ToLower() == "manager"  )
                {
                   // buttonSummary.Visible = true;
                    textBoxCodeDelBill.Visible = true;
                    buttonDelBill.Visible = true;
                    buttonSumSalesByday.Visible = true;
                    buttonSummary.Visible = true;
                }

                if (Login.userStatus.ToLower() == "admin"  )
                {
                    buttonSumSalesByday.Visible = true;
                    buttonSummary.Visible = true;
                    textBoxCodeDelBill.Visible = true;
                    buttonDelBill.Visible = true;
                }

                //MessageBox.Show(DateTime.Now.ToString("yyyyMMdd").Replace("-",""));
                //MessageBox.Show(comboBoxDate.SelectedValue.ToString());
                 

                if (comboBoxDate.SelectedValue.ToString() != DateTime.Now.ToString("yyyyMMdd").Replace("-", ""))
                {
                    textBoxCodeDelBill.Visible = false;
                    buttonDelBill.Visible = false;
                }



                // New Tab

                dateTimePickerStartDate.Value = DateTime.Now;
                dateTimePickerEnd.Value = DateTime.Now;

                defaultTab2();

            }
            catch (Exception ex)
            {
              //  MessageBox.Show("No Sales Data");
            }
        }


        private void getComboAllPeriod()
        {
            try
            {

                List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();

                // Add data to the List

                foreach (TrnDate c in listPeriods)
                {
                    data.Add(new KeyValuePair<string, string>(c.PeriodID, c.Period));
                }


                // Clear the combobox
                comboBoxPeriod.DataSource = null;
                comboBoxPeriod.Items.Clear();

                // Bind the combobox
                comboBoxPeriod.DataSource = new BindingSource(data, null);
                comboBoxPeriod.DisplayMember = "Value";
                comboBoxPeriod.ValueMember = "Key";

                comboBoxPeriod.SelectedIndex = 0;
                getComboAllDate();
            }
            catch (Exception Ex)
            {
               // MessageBox.Show(Ex.Message);
            }

        }


        private void getComboAllDate()
        {
            try
            {

                List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();

                // Add data to the List

                string periodID = comboBoxPeriod.SelectedValue.ToString();

                foreach (TrnDate c in listDates)
                {
                    if (periodID == c.PeriodID)
                        data.Add(new KeyValuePair<string, string>(c.DateID, c.Date));
                }


                // Clear the combobox
                comboBoxDate.DataSource = null;
                comboBoxDate.Items.Clear();

                // Bind the combobox
                comboBoxDate.DataSource = new BindingSource(data, null);
                comboBoxDate.DisplayMember = "Value";
                comboBoxDate.ValueMember = "Key";

                comboBoxDate.SelectedIndex = 0;

                getComboAllTrnID();
            }
            catch (Exception Ex)
            {
              //  MessageBox.Show(Ex.Message);
            }

        }

        private void getComboAllTrnID()
        {
            try
            {

                List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

                // Add data to the List

                string dateID = comboBoxDate.SelectedValue.ToString();

                this.listBills = gd.getTrnBillByDate(dateID);

                foreach (Bill c in listBills)
                {
                    data.Add(new KeyValuePair<int, string>(c.BillID, c.BillNo));
                }


                // Clear the combobox
                comboBoxBillNo.DataSource = null;
                comboBoxBillNo.Items.Clear();

                // Bind the combobox
                comboBoxBillNo.DataSource = new BindingSource(data, null);
                comboBoxBillNo.DisplayMember = "Value";
                comboBoxBillNo.ValueMember = "Key";

                comboBoxBillNo.SelectedIndex = 0;

            }
            catch (Exception Ex)
            {
               // MessageBox.Show(Ex.Message);
            }
          
        }
        DataTable dtBillPayment;
          
        private void genDataPayment(){

            textBoxBillID.Text =  "#" + payment.TrnID.ToString();
            textBoxBillID_TaxID.Text = "#" + payment.TrnInvID.ToString();
            textBoxTotalAmt.Text = payment.PayAmount.ToString("###,###.#0");
            textBoxtableName.Text = payment.TableName;
            textBoxOrderTime.Text = payment.OrderDateTime;
            textBoxPayTime.Text = payment.PayDateTime; 
            comboBoxListCust.SelectedValue = payment.PayCustID;

            dtBillPayment = gd.getAllPaymentByPayType(payment.TrnID); 
            dataGridViewBillPayment.DataSource = dtBillPayment;

            dataGridViewBillPayment.Columns[0].Visible = false;
            dataGridViewBillPayment.Columns[2].Visible = false;
            dataGridViewBillPayment.Columns[4].Visible = false;

        }

        private void comboBoxPeriod_Change(object sender, EventArgs e)
        {
            getComboAllDate();
        }

        private void comboBoxDate_Change(object sender, EventArgs e)
        {
            if (comboBoxDate.SelectedValue.ToString() != DateTime.Now.ToString("yyyyMMdd").Replace("-", ""))
            {
                textBoxCodeDelBill.Visible = false;
                buttonDelBill.Visible = false;
            }
            else
            {
                if (Login.userStatus.ToLower() == "admin" || Login.userStatus.ToLower() == "manager")
                {
                    textBoxCodeDelBill.Visible = true;
                    buttonDelBill.Visible = true;
                }
            }


            getComboAllTrnID();
        }

        private void comboBoxBill_Change(object sender, EventArgs e)
        {
            try
            {
                this.trnBillIDSect = Int32.Parse(comboBoxBillNo.SelectedValue.ToString());

                payment = gd.getTrnPayment(this.trnBillIDSect);

                genDataPayment();

                trn = gd.getTrnOrder(this.trnBillIDSect);
                dataGridViewOrder.DataSource = trn;

                dataGridViewOrder.Columns[0].Visible = false;
                dataGridViewOrder.Columns[1].Visible = false;
                dataGridViewOrder.Columns[4].Visible = false;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        int copyPrint = 0;

        private void buttonPrintBill_Click(object sender, EventArgs e)
        {

            this.flagFD = 0;

            this.copyPrint = 0;
            printBill.Print();
            copyPrint++;
            printBill.Print();
            this.copyPrint = 0;

           
            //



        }

                // OnBeginPrint 
        private void OnBeginPrint(object sender, PrintEventArgs e)
        {
            
        }


        // OnPrintPage
        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

                int x = 0;
                int y = 0;

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

                Branch branch = gd.getBranchDesc();

                /* 
                 
                 * Default Bill Config @ Program
                 * 
                 * EN , TH , NO
                 * 
                 */

            //    string langBill = this.defaultlangBill;

                //if (this.defaultlangBill == "NO")
                string langBill = "EN";


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


                if (this.copyPrint > 0)
                    e.Graphics.DrawString("copy", fontBody, brush, x + 150, y); 
               
                y += 15;

                

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


                if (restLine2.Trim().Length > 0)
                {
                    e.Graphics.DrawString(restLine2, fontBody, brush, x + 50, y);

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

                if (this.flagdelBill == 0)
                {

              

                    e.Graphics.DrawString(Login.userID.ToString() + " " + Login.userName, fontBodylist, brush, x + 5, y);

                    y += 10;

                }
                else
                {
                    e.Graphics.DrawString("ยกเลิกบิล โดย : " + Login.userID.ToString() + " " + Login.userName, fontTable, brush, x + 20, y);
                    y += 20;
                    e.Graphics.DrawString("เหตุผล : ........................... ", fontTable, brush, x + 20, y);
                    y += 20;
                    e.Graphics.DrawString("ลงชื่อ : ............................. ", fontTable, brush, x + 20, y);
                    y += 20;
                }

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd MMM yy}", dt);
                string strTime = String.Format("{0:HH:mm}", dt);

                int noofCust = 0;

                foreach (Transaction o in trn)
                {
                    if (o.ProductID == 4)
                        noofCust = Int32.Parse(o.SalesQTY.ToString());
                }

                e.Graphics.DrawString("-------------------------------------------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 12;
                e.Graphics.DrawString(this.textBoxtableName.Text, fontSubHeader, brush, x + 10, y);
                e.Graphics.DrawString("Cust " + noofCust.ToString() , fontSubHeader, brush, x + 220, y);
                y += 15;
                e.Graphics.DrawString( textBoxPayTime.Text, fontSubHeader, brush, x + 80, y);
                y += 10;
                e.Graphics.DrawString("-------------------------------------------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 15;

                //// Print Order

                int i = 1;

                float salesAmountFood = 0;
                float salesAmountDrinks = 0;
                float salesAmount = 0;
                float tax = 0;
                float serviceCharge = 0;
                float discount = 0;

                string str1 = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";
                string trnProductRemark = "";

                List<string> txtPrint;
                int len = 30; 

                foreach (Transaction o in trn)
                {

                    if (o.GroupCatID > 0 && trnProductRemark != "TOPPING" && !((o.ProductID > 10 && o.ProductID <= 100) && o.ProductID != 4))
                    {

                        if (this.flagLang == "TH"){
                            str1 = o.ProductName.Trim(); ;
                        }
                        else{
                            str1 = o.ProductNameEN.Trim(); ;
                        }

                        str2 = o.SalesQTY.ToString();
                        str3 = (o.SalesAmount / o.SalesQTY).ToString("###,###");
                        str4 = o.SalesAmount.ToString("###,###.#0"); 

                        str4 = String.Format("{0,10}", str4);



                        if (o.SalesQTY > 1)
                            str1 += "(" + str3 + ")";

                        trnProductRemark = o.TrnRemark;


                        if (this.flagFD == 0)
                        {

                            e.Graphics.DrawString(str2, fontBodylist, brush, x + 0, y);
                      
                            e.Graphics.DrawString(str4, fontNum, brush, x + 180, y);

                            txtPrint = FuncString.WordWrap(str1, len);
                            str1 = "";

                            foreach (string op in txtPrint)
                            {
                                e.Graphics.DrawString(op, fontBodylist, brush, x + 20, y);
                                y += 17;
                            } 
                             
                            i++;
                          //  y += 15;

                            //if (trnProductRemark.Trim().Length > 1)
                            //{
                            //    string[] remarkString = trnProductRemark.Remove(0, 1).Split('+');

                            //    foreach (string r in remarkString)
                            //    {
                            //        str1 = "  +" + r.Split('{')[0] + "\r\n";

                            //        e.Graphics.DrawString(str1, fontBodylist, brush, x + 22, y);
                            //        y += 13;
                            //    }
                            //}

                        }
                         
        
                        if( o.GroupCatID == 1){

                            salesAmountFood += o.SalesAmount;
                        }else{

                            salesAmountDrinks += o.SalesAmount;
                        }

                        salesAmount += o.SalesAmount;
                    
                    }else{

                        if( o.ProductID == 1){
                            discount = o.SalesAmount*-1;
                        }
                        else if( o.ProductID == 3){
                            tax = o.SalesAmount;
                        }else if( o.ProductID == 2){
                            serviceCharge = o.SalesAmount;
                        }


                    } 

                }

                //////////////////////////////////////////////////////////////////////////
                string txtOrder = "";

                txtOrder = "";
                string txtAmt = "";


                if (this.flagFD == 0)
                {

          
                }
                else
                {

                    e.Graphics.DrawString("อาหารและเครื่องดื่ม", fontBodylist, brush, x + 22, y);
                    e.Graphics.DrawString("1", fontBodylist, brush, x + 0, y);
                    str4 = (salesAmount).ToString("###,###");
                    str4 = String.Format("{0,10}", str4);
                    e.Graphics.DrawString(str4, fontBodylist, brush, x + 180, y);

                    y += 15;
                }
                 

                //////////////////////////////////////////////////////////////////////////
            //    string txtOrder = "";

                txtOrder = ""; 

                if (discount > 0)
                {
                    y += 15;

                    txtOrder = "Discount (Total)";
                    txtAmt = discount.ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);
                }


                if (serviceCharge > 0)
                {
                    y += 15;
                    txtOrder = "Service " + ((float)(this.servicePercent * 100)).ToString() + "%";
                    txtAmt = serviceCharge.ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                }
                if (tax > 0)
                {

                    y += 15;
                    txtOrder = "Amount Before Vat ";
                    txtAmt = (tax * (100.00 / 7.00)).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    y += 15;
                    txtOrder = "Vat " + ((float)(this.taxPercent * 100)).ToString() + "%";
                    txtAmt = tax.ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    y += 15;
                    txtOrder = "Rounding ";
                    txtAmt = (payment.PayAmount - tax * (107.00 / 7.00)).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                }


                y += 25;
                txtOrder = "Total";
                txtAmt = payment.PayAmount.ToString("###,###.#0");
                e.Graphics.DrawString(txtOrder, fontTable, brush, x + 20, y);
                e.Graphics.DrawString(txtAmt, fontTable, brush, x + 185, y);

                y += 20;



                //////////////////////////////////////////////////////////////////////


                y += 20;
                 
                string custName = "";
                string remark = "";
                int custID = 0;

                string billNo = "#" + payment.TrnID.ToString();
                string billNoFull = "#" + payment.TrnInvID.ToString();
                 

                custID = payment.PayCustID;
                custName = payment.PayCustName;
                remark = payment.PayRemark;

                int payTypeID = 0;
                float payAmount = 0;
                string PayCardType = "";
                string PayCardName = "";
                string PayCardNo = "";
                string PayCashCardCode = "";

                e.Graphics.DrawString("-------------------------------------------------------------------------------------", fontSubHeader, brush, x, y);

                y += 15;

                if (langBill == "TH")
                {
                    e.Graphics.DrawString(" " + "เลขที่ใบกำกับภาษี (TAX INV) : " + billNoFull, fontBody, brush, x + 10, y);
                    y += 20;
                    e.Graphics.DrawString(" " + "บิลเลขที่ (Bill Ref No.) : " + billNo, fontBody, brush, x + 10, y);
                    y += 20;
                    i = 1;
                    foreach (DataRow dr in dtBillPayment.Rows)
                    {
                         payTypeID = Int32.Parse(dr[dtBillPayment.Columns["PayTypeID"]].ToString()) ;
                         payAmount = float.Parse(dr[dtBillPayment.Columns["PayAmount"]].ToString());
                         PayCardType = dr[dtBillPayment.Columns["PayCreditCardType"]].ToString();
                         PayCardName = dr[dtBillPayment.Columns["PayCreditCardNo"]].ToString();
                         PayCardNo = dr[dtBillPayment.Columns["PayCreditCardCust"]].ToString();
                         PayCashCardCode = dr[dtBillPayment.Columns["CashCardCode"]].ToString();

                         if (payTypeID == 1)
                        {
                            e.Graphics.DrawString(i.ToString() + ". ชำระเงินสด ยอด  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                        }
                         else if (payTypeID == 2)
                        {
                            e.Graphics.DrawString(i.ToString() + ". ชำระบัตรเครดิต ยอด  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - ประเภท : " + PayCardType, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - เลขที่   : " + PayCardNo, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - ชื่อลูกค้า : " + PayCardName, fontBody, brush, x + 10, y); y += 15;
                        }
                         else if (payTypeID == 3 )
                        {
                            e.Graphics.DrawString(i.ToString() + ". ลงบิลลูกค้า ยอด  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - ชื่อ :" + payment.PayCustName, fontBody, brush, x + 10, y); y += 15;
                        }
                         else if (payTypeID == 4)
                        {
                            e.Graphics.DrawString(i.ToString() + ". บัตรเงินสด ยอด  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - รหัสบัตร    : " + PayCashCardCode, fontBody, brush, x + 10, y); y += 15;
                            }
                         else if (payTypeID == 5)
                        {
                            e.Graphics.DrawString(i.ToString() + ". QR/โอนเงิน ยอด  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - ประเภท   : " + PayCardType, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - รายละเอียด : " + PayCardNo, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - ชื่อลูกค้า   : " + PayCardName, fontBody, brush, x + 10, y); y += 15;
                        }

                        i++;

                    }

                }
                else
                {
                    e.Graphics.DrawString(" " + "เลขที่ใบกำกับภาษี (TAX INV) : " + billNoFull, fontBody, brush, x + 10, y);
                    y += 20;
                    e.Graphics.DrawString(" " + "บิลเลขที่ (Bill Ref No.) : " + billNo, fontBody, brush, x + 10, y);
                    y += 20;


                    i = 1;
                    foreach (DataRow dr in dtBillPayment.Rows)
                    {
                        payTypeID = Int32.Parse(dr[dtBillPayment.Columns["PayTypeID"]].ToString());
                        payAmount = float.Parse(dr[dtBillPayment.Columns["PayAmount"]].ToString());
                        PayCardType = dr[dtBillPayment.Columns["PayCreditCardType"]].ToString();
                        PayCardName = dr[dtBillPayment.Columns["PayCreditCardNo"]].ToString();
                        PayCardNo = dr[dtBillPayment.Columns["PayCreditCardCust"]].ToString();
                        PayCashCardCode = dr[dtBillPayment.Columns["CashCardCode"]].ToString();

                        if (payTypeID == 1)
                        {
                            e.Graphics.DrawString(i.ToString() + ". Cash Payment  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                        }
                        else if (payTypeID == 2)
                        {
                            e.Graphics.DrawString(i.ToString() + ". Credit Card Payment   :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Type    : " + PayCardType, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Card No.:  " + PayCardNo, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Name    : " + PayCardName, fontBody, brush, x + 10, y); y += 15;
                        }
                        else if (payTypeID == 3)
                        {
                            e.Graphics.DrawString(i.ToString() + ". Credit customer  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Name :" + payment.PayCustName, fontBody, brush, x + 10, y); y += 15;
                        }
                        else if (payTypeID == 4)
                        {
                            e.Graphics.DrawString(i.ToString() + ". Cash Card Payment :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Cashcard Code : " + PayCashCardCode, fontBody, brush, x + 10, y); y += 15;
                        }
                        else if (payTypeID == 5)
                        {
                            e.Graphics.DrawString(i.ToString() + ". QR / Banking Payment  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Type        : " + PayCardType, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Description : " + PayCardNo, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Name        : " + PayCardName, fontBody, brush, x + 10, y); y += 15;
                        }

                        i++;

                    } 


                }


                y += 15;


                string custnName = "";
                string tel = "";
                string address = "";
                string taxID = "";
                string title = "";

                y += 15;


                if (custID > 0)
                {

                    List<Customer> custLists = gd.getListAllCustomer();

                    foreach (Customer cc in custLists)
                    {
                        if (cc.CustID == custID)
                        {
                            title = cc.Title;
                            custnName = cc.Name;
                            address = cc.Address;
                            tel = cc.Tel;
                            taxID = cc.TaxID;
                        }

                    }
                     
                     
                    len = 40; 

                    txtPrint = FuncString.WordWrap(title + custnName, len);
                    foreach (string op in txtPrint)
                    {
                        e.Graphics.DrawString(op, fontBodylist, brush, x + 5, y);
                        y += 15;
                    }
                     

                    txtPrint = FuncString.WordWrap(address, len);
                    foreach (string op in txtPrint)
                    {
                        e.Graphics.DrawString(op, fontBodylist, brush, x + 5, y);
                        y += 15;
                    }


                    e.Graphics.DrawString("โทร : " + tel, fontBodylist, brush, x + 5, y);
                    y += 15;
                    e.Graphics.DrawString("เลขประจำตัวผู้เสียภาษี : " + taxID, fontBodylist, brush, x + 5, y);
                    y += 50;

                    e.Graphics.DrawString("............................................ ", fontBodylist, brush, x + 40, y);
                    y += 15;
                    e.Graphics.DrawString(" ลงชื่อผู้ออกใบกำกับภาษี     ", fontBodylist, brush, x + 80, y);
                    
                    y += 25;


                    //e.Graphics.DrawString(" VAT INCLUDED", fontBody, brush, x + 80, y);
                    //y += 15;

                }

                Bitmap imgLogoFB = global::AppRest.Properties.Resources.Logo_FB;
                Bitmap imgLogoIG = global::AppRest.Properties.Resources.Logo_IG;

                //   y += 15;
                e.Graphics.DrawString(" THANK YOU / ขอบคุณค่ะ ", fontBody, brush, x + 70, y);
                y += 20;
                e.Graphics.DrawImage(imgLogoFB, x + 75, y - 5, 18, 18);
                e.Graphics.DrawString(this.fblink, fontBody, brush, x + 98, y);
                y += 20;
                e.Graphics.DrawImage(imgLogoIG, x + 75, y - 5, 18, 18);
                e.Graphics.DrawString(this.iglink, fontBody, brush, x + 98, y);
                y += 15;

                if (qrlink.Length > 0)
                {
                    Bitmap img3 = global::AppRest.Properties.Resources.QR;
                    e.Graphics.DrawImage(img3, x + 63, y, 150, 150);
                    y += 145;
                }

                e.Graphics.DrawString(this.restlink, fontBody, brush, x + 70, y);


                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void flagLang_Change(object sender, EventArgs e)
        {

            if (radioBoxBillTH.Checked == true)
            {
                this.flagLang = "TH";
            }
            else
            {
                this.flagLang = "EN";
            }
             
        }

        private void buttonDelBill_Click(object sender, EventArgs e)
        {
            string billNo = "#" + payment.TrnID;
            string delCode = textBoxCodeDelBill.Text;

            if (MessageBox.Show("คุณต้องการจะลบบิล " + billNo + "หรือไม่ ? ", "ลบบิล" + billNo, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int result = gd.delBill(payment.TrnID, delCode);

                if (result <= 0)
                {
                    MessageBox.Show("ใส่รหัสลบบิลผิด");
                }
                else
                {
                    this.flagdelBill = 1;
                    MessageBox.Show("ลบบิล " + billNo + " สำเร็จ");

                    printBill.Print();

                }

                LinkFormMainTable();
            }
        } 

        private void LinkFormMainTable()
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

        private void KeyPassDelFinish(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.buttonDelBill_Click(buttonDelBill, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printBillDay.Print();
            SalesEnddayByLINE();
            MsgNotify.lineNotify(this.msgtoLINE);
        }


        // OnBeginPrint 
        private void OnBeginPrint2(object sender, PrintEventArgs e)
        {
            try
            {
                string DateDay = comboBoxDate.Text;


                trnDay = gd.getTrnOrderByDay(DateDay); 
                 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // OnPrintPage
        private void OnPrintPage2(object sender, PrintPageEventArgs e)
        {
            try
            {
                int x = 0;
                int y = 0;

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

                Branch branch = gd.getBranchDesc();

                /* 
                 
                 * Default Bill Config @ Program
                 * 
                 * EN , TH , NO
                 * 
                 */

                //    string langBill = this.defaultlangBill;

                //if (this.defaultlangBill == "NO")
                string langBill = "EN";


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

                    int iii = 0;
                    foreach (string s in ArrgrestTaxRD)
                    {
                        if (s == Login.posBranchID.ToString())
                        {
                            appTaxRD = ArrgrestTaxRD[iii + 1];
                        }

                        iii++;
                    }
                }




                string DateDay = comboBoxDate.Text; 

                trnDay = gd.getTrnOrderByDay(DateDay);

                List<SalesToday> salesZone = gd.getSalesByday(4, DateDay);
                List<SalesToday> salesPaymentAll = gd.getSalesByday(5, DateDay);
                List<SalesToday> salesPayment = gd.getSalesByday(6, DateDay);
                List<SalesToday> salesDelBill = gd.getSalesByday(8, DateDay);
                List<SalesToday> salesCashCard = gd.getSalesByday(9, DateDay);
                List<SalesToday> salesDelOrder = gd.getSalesByday(10, DateDay); 
                

                List<SalesToday> salesIN = gd.getSalesByday(200, DateDay);
 

                ///////////////////////////


                Brush brush = new SolidBrush(Color.Black);


                Font fontHeader = new Font("Arail", 12, FontStyle.Bold);
                Font fontTable = new Font("Arail", 11, FontStyle.Bold);
                Font fontSubHeader = new Font("Arail", 9, FontStyle.Bold);
                Font fontBody = new Font("Arail", 8, FontStyle.Bold);
                Font fontBodylist = new Font("Arail", 8, FontStyle.Bold);
                Font fontNum = new Font("Consolas", 9, FontStyle.Bold);


                //if (Login.flagLogoSQ.ToLower() == "y")
                //{

                //    e.Graphics.DrawImage(img, x + 110, y, 100, 100);
                //    y += 100;
                //}
                //else
                //{
                //    e.Graphics.DrawImage(img, x + 40, y, 220, 110);
                //    y += 110;
                //}

                e.Graphics.DrawString(restName, fontBody, brush, x + 10, y);

                y += 12;



                e.Graphics.DrawString(restAddr1, fontBody, brush, x + 10, y);

                y += 12;


                if (restAddr2.Trim().Length > 0)
                {
                    e.Graphics.DrawString(restAddr2, fontBody, brush, x + 10, y);

                    y += 12;

                }


                if (restTaxID.Trim().Length > 0)
                {
                    e.Graphics.DrawString("" + restTaxID, fontBody, brush, x + 40, y);

                    y += 12;
                }



                if (appTaxRD.Trim().Length > 0)
                {
                    e.Graphics.DrawString("TAX RD : " + appTaxRD, fontBody, brush, x + 40, y);

                    y += 12;
                }


                if (restTel.Trim().Length > 0)
                {
                    e.Graphics.DrawString(restTel, fontBody, brush, x + 60, y);

                    y += 12;

                }

                y += 20;

                e.Graphics.DrawString("สรุปรายการขายทั้งหมด " + DateDay, fontSubHeader, brush, x + 45, y);
                y += 20;
                e.Graphics.DrawString("พิมพ์ " + DateTime.Now.ToString(), fontBody, brush, x + 50, y); 

                y += 25;
                 

                int i = 1;

          
                string txtAmt = "";



                string txtOrder = "";
                string lb = "";
                float val = 0;
                int ii = 1;

               // this.msgtoLINE = "";

                foreach (SalesToday st in salesIN)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = lb;

                    if (val == 0)
                    {
                        txtAmt = "";
                    }else
                    {
                        txtAmt = (val).ToString("###,###.#0") + "";
                        txtAmt = String.Format("{0,10}", txtAmt);
                    }
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 0, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 190, y);

                  //  this.msgtoLINE = txtOrder + " : " + txtAmt + "\n\r";


                    ii++;
                    y += 15;
                }


                this.msgtoLINE = " -------------------- " + "\n\r";


                y += 20;

                //////////////////////////////////////////////////////////////////////

                e.Graphics.DrawString("การชำระเงินแยกประเภท (ทั้งหมด) ", fontBody, brush, x + 0, y);
                y += 20;

                lb = "";
                val = 0;
                ii = 1;

                foreach (SalesToday st in salesPaymentAll)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount; 

                    txtOrder = ii.ToString() + ". " + lb;
                    txtAmt = (val).ToString("###,###.#0") ;
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 0, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 190, y);
                  

                  
                    ii++;
                    y += 15;
                }


                //y += 20;

                //ii = 1;
                //e.Graphics.DrawString("ยอดเติมเงิน / คืนเงิน Cash Card ", fontBody, brush, x + 10, y);
                //y += 15;

                //float ut;

                //foreach (SalesToday st in salesCashCard)
                //{

                //    lb = st.SalesLable;
                //    ut = st.SalesUnit;
                //    val = st.SalesAmount;

                //    txtOrder = ii.ToString() + ". " + lb;
                //    txtAmt = (val).ToString("###,###.#0") ;
                //    txtAmt = String.Format("{0,10}", txtAmt);
                //    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                //    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                //    ii++;
                //    y += 15;
                //}


                 

                //////////////////////////////////////////////////////////////////////

                //// Payment

                y += 20;

                e.Graphics.DrawString("การชำระเงินแยก Zone / Cashier", fontBody, brush, x + 0, y);
                y += 20;

                lb = "";
                val = 0;
                ii = 1;

                foreach (SalesToday st in salesPayment)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = ii.ToString() + ". " + lb;
                    txtAmt = (val).ToString("###,###.#0") ; 
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 0, y);
                  //  e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);
                    y += 15;
                    e.Graphics.DrawString(">> " + txtAmt, fontNum, brush, x + 190, y);

                    ii++;
                    y += 15; 
                }

                y += 20;

                // Zone

                e.Graphics.DrawString("ยอดขาย ตาม Zone", fontBody, brush, x + 0, y);
                y += 20;

                ii = 1;
                foreach (SalesToday st in salesZone)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = ii.ToString() + ". " + lb;
                    txtAmt = (val).ToString("###,###.#0") ;
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 0, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 190, y);


                    ii++;
                    y += 15;
                }

                y += 20;

                ii = 1;
                e.Graphics.DrawString("รายการลบบิล (Void Bill) ", fontBody, brush, x + 0, y);
                y += 20;
                 

                foreach (SalesToday st in salesDelBill)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = ii.ToString() + ". " + lb;
                    txtAmt = (val).ToString("###,###.#0") ;
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 0, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 190, y);

                    ii++;
                    y += 15;
                }

                y += 20;

                ii = 1;
                e.Graphics.DrawString("รายการลบ ORDER", fontBody, brush, x + 0, y);
                y += 20;


                foreach (SalesToday st in salesDelOrder)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = ii.ToString() + ". " + lb;
                    txtAmt = (val).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 0, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 190, y);

                    ii++;
                    y += 15;
                }

                y += 20;


  

            //    SalesEnddayByLINE();
            
  

                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SalesEnddayByLINE()
        {
            try
            {

                int x = 0;
                int y = 0;

                string DateDay = comboBoxDate.Text; 

                List<SalesToday> salesIN = gd.getSalesByday(101, DateDay);


                string restName = "";
                string restAddr1 = ""; // 160/8 ซ.ทองหล่อ ถ.สุขุมวิท 55
                string restAddr2 = ""; // แขวงคลองตันเหนือ แขตวัฒนา กรุงเทพฯ 10110 
                string restTel = ""; // โทร. 02-714-9402
                string restTaxID = "";
                string restLine1 = "";
                string restLine2 = "";


                //    Bitmap img = global::AppRest.Properties.Resources.Logo_New;

                Branch branch = gd.getBranchDesc();

                restName = branch.RestNameTH;
                restAddr1 = branch.RestAddr1TH;
                restAddr2 = branch.RestAddr2TH;
                restTel = "โทร. : " + branch.RestTel;

                restLine1 = branch.RestLine1;
                restLine2 = branch.RestLine2;
                restTaxID = branch.RestTaxID;


                /////////////////


 


                this.msgtoLINE = "\n\r";


                this.msgtoLINE += restName + "\n\r";

                this.msgtoLINE += "สรุปยอดขาย  " + DateDay + "\n\r";
                this.msgtoLINE += "ส่งยอด" + DateTime.Now.ToString() + "\n\r";

                this.msgtoLINE += "____________________" + "\n\r\n\r";
            
                string txtAmt = ""; 

                string txtOrder = "";
                string lb = "";
                float val = 0;
                int ii = 1; 

                foreach (SalesToday st in salesIN)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = lb;
                    txtAmt = (val).ToString("###,###.#0") + ""; 

                    this.msgtoLINE += txtOrder + " : " + txtAmt + "\n";


                    ii++;
                    y += 15;
                }


                this.msgtoLINE += "______________ " + "\n\r";


                y += 20;

                ////////////////////////////////////////////////////////////////////////

                //this.msgtoLINE += "** By Payment Type  " + "\n\r";
                //y += 20;

                //lb = "";
                //val = 0;
                //ii = 1;

                //foreach (SalesToday st in salesPaymentAll)
                //{

                //    lb = st.SalesLable;
                //    val = st.SalesAmount;

                //    txtOrder = ii.ToString() + ". " + lb;
                //    txtAmt = (val).ToString("###,###.#0") ;
                  
                //    this.msgtoLINE += txtOrder + " : " + txtAmt + "\n\r";


                //    ii++;
                //    y += 15;
                //}


                //y += 20;

                //ii = 1;
                //e.Graphics.DrawString("**** ยอดเติมเงิน / คืนเงิน Cash Card **** ", fontBody, brush, x + 10, y);
                //y += 15;

                //float ut;

                //foreach (SalesToday st in salesCashCard)
                //{

                //    lb = st.SalesLable;
                //    ut = st.SalesUnit;
                //    val = st.SalesAmount;

                //    txtOrder = ii.ToString() + ". " + lb;
                //    txtAmt = (val).ToString("###,###.#0") ;
                //    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 10, y);
                //    e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 185, y);

                //    ii++;
                //    y += 15;
                //}


                

                //////////////////////////////////////////////////////////////////////

                //// Payment

                //y += 20;

                //this.msgtoLINE +="** By Zone : Cashier "+ "\n\r";
                //y += 20;

                //lb = "";
                //val = 0;
                //ii = 1;

                //foreach (SalesToday st in salesPayment)
                //{

                //    lb = st.SalesLable;
                //    val = st.SalesAmount;

                //    txtOrder = ii.ToString() + ". " + lb;
                //    txtAmt = (val).ToString("###,###.#0") ;

                //    this.msgtoLINE += txtOrder + " : " + txtAmt + "\n\r";

                //    //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 10, y);
                //    //e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 185, y);

                //    ii++;
                //    y += 15;
                //}

                //y += 20;

                //// Zone

                // this.msgtoLINE += "** By Zone "+ "\n\r";
                //y += 20;

                //ii = 1;
                //foreach (SalesToday st in salesZone)
                //{

                //    lb = st.SalesLable;
                //    val = st.SalesAmount;

                //    txtOrder = ii.ToString() + ". " + lb;
                //    txtAmt = (val).ToString("###,###.#0") ;


                //    this.msgtoLINE += txtOrder + " : " + txtAmt + "\n\r";


                //    ii++;
                //    y += 15;
                //}

                //y += 20;

                //ii = 1;

              //  this.msgtoLINE += "** Void Bill " + "\n\r";
           
                y += 20;

                /*

                foreach (SalesToday st in salesDelBill)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = ii.ToString() + ". " + lb;
                    txtAmt = (val).ToString("###,###.#0") ;



                   this.msgtoLINE += txtOrder + " : " + txtAmt + "\n\r";


                    ii++;
                    y += 15;
                }

                y += 20;

                */
             


           //     e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void buttonPrintA4_Click(object sender, EventArgs e)
        {

            Button printButton = (Button)sender;
          //  int trnBillIDSect = 0;
            int type = 0;

            try
            {
                type = Int32.Parse(printButton.Name.Replace("buttonPrint", "").Trim());
             //   trnBillIDSect = Int32.Parse(comboBoxBillNo.SelectedValue.ToString());

                // MessageBox.Show(type.ToString());
                LinkFromRptBillReport(this.trnBillIDSect, type);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LinkFromRptBillReport(int billID, int type)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formFromRptBillReport == null)
            {
                formFromRptBillReport = new FromRptBillReport(this, 0, billID, type);
            }
            else
            {
                formFromRptBillReport.rptBill = billID;
                formFromRptBillReport.rptType = type;
                formFromRptBillReport.viewReport();
            }
            Cursor.Current = Cursors.Default;
            if (formFromRptBillReport.ShowDialog() == DialogResult.OK)
            {
                formFromRptBillReport.Dispose();
                formFromRptBillReport = null;
            }
        }


        //private void LinkFromRptBillReport(int billID ,int type )
        //{
        //    Cursor.Current = Cursors.WaitCursor;
        //    if (formFromRptBillReport == null)
        //    {
        //        formFromRptBillReport = new FromRptBillReport(this, 0, billID, type);
        //    }
        //    Cursor.Current = Cursors.Default;
        //    if (formFromRptBillReport.ShowDialog() == DialogResult.OK)
        //    {
        //        formFromRptBillReport.Dispose();
        //        formFromRptBillReport = null;
        //    }
        //}

        private void LinkFromRptBillReportCopy(int billID, int type)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formFromRptBillReportCopy == null)
            {
                formFromRptBillReportCopy = new FromRptBillReport(this, 0, billID, type);
            }
            Cursor.Current = Cursors.Default;
            if (formFromRptBillReportCopy.ShowDialog() == DialogResult.OK)
            {
                formFromRptBillReportCopy.Dispose();
                formFromRptBillReportCopy = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
              //  int trnBillIDSect = Int32.Parse(comboBoxBillNo.SelectedValue.ToString());

                //MessageBox.Show(payment.TrnID.ToString());
                LinkFromRptBillReport(this.trnBillIDSect, 2);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //int trnBillIDSect = Int32.Parse(comboBoxBillNo.SelectedValue.ToString());

                //MessageBox.Show(payment.TrnID.ToString());
                LinkFromRptBillReportCopy(this.trnBillIDSect, 3);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
               // int trnBillIDSect = Int32.Parse(comboBoxBillNo.SelectedValue.ToString());

                //MessageBox.Show(payment.TrnID.ToString());
                LinkFromRptBillReportCopy(this.trnBillIDSect, 4);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void getComboAllCust()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "====== เลือกรายชื่อลูกค้า  ======"));

            foreach (Customer c in allCusts)
            {
                data.Add(new KeyValuePair<int, string>(c.CustID, c.Title + c.Name));
            }


            // Clear the combobox
            comboBoxListCust.DataSource = null;
            comboBoxListCust.Items.Clear();

            // Bind the combobox
            comboBoxListCust.DataSource = new BindingSource(data, null);
            comboBoxListCust.DisplayMember = "Value";
            comboBoxListCust.ValueMember = "Key";


            comboBoxAllCust_Up.DataSource = null;
            comboBoxAllCust_Up.Items.Clear();

            // Bind the combobox
            comboBoxAllCust_Up.DataSource = new BindingSource(data, null);
            comboBoxAllCust_Up.DisplayMember = "Value";
            comboBoxAllCust_Up.ValueMember = "Key";

        }

        private void buttonCalTaxAgo_Click(object sender, EventArgs e)
        {
             int custID = 0;
             string custName = "";
             int type = 0;

             int billID = this.trnBillIDSect;

             try
             {

                 custID = Int32.Parse(comboBoxListCust.SelectedValue.ToString());

                 custName = comboBoxListCust.Text;

                 if (radioButtonWithTax.Checked == true)
                     type = 1;

                 if (MessageBox.Show("คุณแน่ใจว่าจะเพิ่ม / ลด ภาษีในบิล #" + this.trnBillIDSect + "\n ในชื่อลูกค้า :" + custName + " หรือไม่ ? \n", "ยืนยัน", MessageBoxButtons.YesNo) == DialogResult.Yes)
                 {

                     int result = gd.UpsTaxAgo(this.trnBillIDSect, custID, type,"NON","NO","NON",0,0,0);

                     if (result <= 0)
                     {
                         throw new Exception("ไม่สามารถแก้ไขการคำนวณภาษีย้อนหลังได้"); 
                     }

                     getComboAllTrnID();

                     comboBoxBillNo.SelectedValue = billID;
                 }

             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
        }

        private void buttonPrintNewReport_Click(object sender, EventArgs e)
        {

            try
            {
              //  int trnBillIDSect = Int32.Parse(comboBoxBillNo.SelectedValue.ToString());


                LinkFromRptBillNewReport(this.trnBillIDSect);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void LinkFromRptBillNewReport(int billID)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formFromRptBillNewReport == null)
            {
                formFromRptBillNewReport = new FromRptBillNewReport(this, 0, billID);
            }
            Cursor.Current = Cursors.Default;
            if (formFromRptBillNewReport.ShowDialog() == DialogResult.OK)
            {
                formFromRptBillNewReport.Dispose();
                formFromRptBillNewReport = null;
            }
        }

        private void buttonAddMember_Click(object sender, EventArgs e)
        {
            LinkFormAddMember();

            // List Cust
            allCusts = gd.getListAllCustomer();
            getComboAllCust();

            dataTableAllCust = gd.getDataAllCustomer();
            dataGridViewAllMember.DataSource = dataTableAllCust;
        }


        private void LinkFormAddMember()
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

      

        int pageProductPrint = 1;
        int noofPage = 0;

        List<SalesToday> salesProduct;

        private void buttonSumSalesByday_Click(object sender, EventArgs e)
        {
            string DateDay = dateTimePickerStartDate.Value.ToString("dd/MM/yyyy");
            salesProduct = gd.getSalesByday(3, DateDay);

            noofPage = salesProduct.Count / 60 + 1;

            for (pageProductPrint = 1; pageProductPrint <= noofPage; pageProductPrint++)
                printDayByProduct.Print();
        }


        private void printDayByProduct_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

                int x = 0;
                int y = 0; 


                Brush brush = new SolidBrush(Color.Black);

                Font fontHeader = new Font("Arail", 12, FontStyle.Bold);
                Font fontTable = new Font("Arail", 11, FontStyle.Bold);
                Font fontSubHeader = new Font("Arail", 9, FontStyle.Bold);
                Font fontFooter = new Font("Arail", 7, FontStyle.Regular);
                Font fontBody = new Font("Arail", 8, FontStyle.Regular);
                Font fontBodylist = new Font("Arail", 9, FontStyle.Regular);
                Font fontNum = new Font("Consolas", 9, FontStyle.Regular);

                //    Bitmap img = global::AppRest.Properties.Resources.Logo_New;

                Branch branch = gd.getBranchDesc();

                Bitmap imgHeader = global::AppRest.Properties.Resources.Logo_New;
           //     Bitmap imgFooter = global::AppRest.Properties.Resources.Signature;



                //e.Graphics.DrawImage(imgHeader, x + 10, y, 250, 90);
                //y += 90;



                //y += 10;

                //e.Graphics.DrawString("เจียงลูกชิ้นปลา", fontTable, brush, x + 90, y);
                //y += 15;

                e.Graphics.DrawString(branch.BranchNameTH, fontTable, brush, x + 0, y);
                y += 20;

                //if (branch.RestTaxID.Length > 0)
                //    e.Graphics.DrawString("Tax Invoice(ABB)", fontTable, brush, x + 70, y);
                //else
                //    e.Graphics.DrawString("Receipt", fontTable, brush, x + 83, y);

                y += 15;

                e.Graphics.DrawString("- Report End Day -", fontTable, brush, x + 70, y);
                y += 15;


                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd/MM/yyyy}", dt);
                string strTime = String.Format("{0:HH:mm:ss}", dt);



                e.Graphics.DrawString("POS # : " + Login.posBranchID, fontSubHeader, brush, x + 10, y);
                y += 15;
                e.Graphics.DrawString("Cashier : " + Login.userName, fontSubHeader, brush, x + 10, y);
                y += 15;
                e.Graphics.DrawString("Date : " + strDate, fontSubHeader, brush, x + 10, y);
                y += 15;
                e.Graphics.DrawString("Time : " + strTime, fontSubHeader, brush, x + 10, y);
                y += 15;

                e.Graphics.DrawString("Page : " + pageProductPrint.ToString() + "/" + noofPage.ToString(), fontSubHeader, brush, x + 10, y);
                y += 15;



                int i = 1;



                string str1 = "";
                string str2 = "";
                string str3 = "";

                string lb = "";
                float val = 0;
                int ii = 1;


                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 15;

                e.Graphics.DrawString("       Product                                    Unit   Amount ", fontSubHeader, brush, x, y);


                y += 15;
                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 15;

                int iii = 1;
                int pagestartindex = 0;
                int pageEndindex = 0;

                pagestartindex = 60 * (pageProductPrint - 1) + 1;
                pageEndindex = 60 * pageProductPrint;



                foreach (SalesToday st in salesProduct)
                {

                    if (iii >= pagestartindex && iii <= pageEndindex)
                    {

                        lb = st.SalesLable;

                        val = st.SalesAmount;

                        if (st.SalesLable.Substring(0, 2) == ">>")
                            str1 = "" + st.SalesLable;
                        else
                            str1 = "  " + st.SalesLable;

                        str2 = st.SalesUnit.ToString();
                        str3 = st.SalesAmount.ToString("###,###");


                        str3 = String.Format("{0,10}", str3);
                        str2 = String.Format("{0,5}", str2);

                        e.Graphics.DrawString(str2, fontNum, brush, x + 160, y);
                        e.Graphics.DrawString(str3, fontNum, brush, x + 190, y);


                        if (str1.Length >= 80)
                        {
                            e.Graphics.DrawString("" + str1.Substring(0, 39), fontBody, brush, x + 2, y);
                            y += 13;
                            e.Graphics.DrawString("" + str1.Substring(39, 39), fontBody, brush, x + 2, y);
                            y += 13;
                            e.Graphics.DrawString("" + str1.Substring(79, str1.Length - 79), fontBody, brush, x + 2, y);
                        }
                        else if (str1.Length >= 40)
                        {
                            e.Graphics.DrawString("" + str1.Substring(0, 39), fontBody, brush, x + 2, y);
                            y += 13;
                            e.Graphics.DrawString("" + str1.Substring(39, str1.Length - 39), fontBody, brush, x + 2, y);

                        }
                        else
                        {
                            e.Graphics.DrawString(str1, fontBody, brush, x + 2, y);

                        }


                        //   e.Graphics.DrawString(str1, fontBody, brush, x + 0, y);

                        y += 13;


                    }
                    iii++;

                }

                y += 15;



            }
            catch (Exception ex)
            {


            }

        }

 
        private void buttonPrintBillFD_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;

            string txt = bt.Name;

            if (txt == "buttonPrintBillFD")
                this.flagFD = 1;
            else
                this.flagFD = 0;


            printBill.Print();
        }

        private void buttonDeprtment_Click(object sender, EventArgs e)
        {
            printBillDay_Depart.Print();
        }


        // OnPrintPage
        private void printBillDay_Depart_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                int x = 0;
                int y = 0;

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

                Branch branch = gd.getBranchDesc();

                /* 
                 
                 * Default Bill Config @ Program
                 * 
                 * EN , TH , NO
                 * 
                 */

                //    string langBill = this.defaultlangBill;

                //if (this.defaultlangBill == "NO")
                string langBill = "EN";



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

                    int iii = 0;
                    foreach (string s in ArrgrestTaxRD)
                    {
                        if (s == Login.posBranchID.ToString())
                        {
                            appTaxRD = ArrgrestTaxRD[iii + 1];
                        }

                        iii++;
                    }
                }




                string DateDay = comboBoxDate.Text;

                trnDay = gd.getTrnOrderByDay(DateDay);

                List<SalesToday> salesIN = gd.getSalesByday(100, DateDay);


                ///////////////////////////


                Brush brush = new SolidBrush(Color.Black);

                Font fontHeader = new Font("Arail", 12, FontStyle.Bold);
                Font fontTable = new Font("Arail", 11, FontStyle.Bold);
                Font fontSubHeader = new Font("Arail", 9, FontStyle.Bold);
                Font fontBody = new Font("Arail", 8, FontStyle.Bold);
                Font fontBodylist = new Font("Arail", 7, FontStyle.Bold);
                Font fontNum = new Font("Consolas", 9, FontStyle.Bold);


                e.Graphics.DrawString(restName, fontBody, brush, x + 10, y);

                y += 12;  

                e.Graphics.DrawString(restAddr1, fontBody, brush, x + 10, y);

                y += 12;


                if (restAddr2.Trim().Length > 0)
                {
                    e.Graphics.DrawString(restAddr2, fontBody, brush, x + 10, y);

                    y += 12;

                }


                if (restTaxID.Trim().Length > 0)
                {
                    e.Graphics.DrawString("" + restTaxID, fontBody, brush, x + 40, y);

                    y += 12;
                }



                if (appTaxRD.Trim().Length > 0)
                {
                    e.Graphics.DrawString("POS ID : " + appTaxRD, fontBody, brush, x + 40, y);

                    y += 12;
                }


                if (restTel.Trim().Length > 0)
                {
                    e.Graphics.DrawString(restTel, fontBody, brush, x + 60, y); 
                    y += 12;

                }

                e.Graphics.DrawString("สาขา : " + branch.BranchNameTH, fontBody, brush, x + 40, y);

                y += 12;

                y += 20;

                e.Graphics.DrawString("สรุปรายการขายทั้งหมด " + DateDay, fontSubHeader, brush, x + 45, y);
                y += 20;
                e.Graphics.DrawString("พิมพ์ " + DateTime.Now.ToString(), fontBody, brush, x + 50, y);

                y += 25;


                int i = 1;
                string txtAmt = "";



                string txtOrder = "";
                string lb = "";
                float val = 0;
                int ii = 1;

                // this.msgtoLINE = "";

                foreach (SalesToday st in salesIN)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = lb;

                    if (val == 0)
                    {
                        txtAmt = "";
                    }
                    else
                    {
                        txtAmt = (val).ToString("###,###.#0") + "";
                        txtAmt = String.Format("{0,10}", txtAmt);
                    }
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 10, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 200, y);

                    //  this.msgtoLINE = txtOrder + " : " + txtAmt + "\n\r";


                    ii++;
                    y += 15;
                }




                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

 
        /// TAb 2 Manage All Payment


        private void radioBoxToday_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dateTimePickerStartDate.Value = DateTime.Now;
                dateTimePickerEnd.Value = DateTime.Now;

                refreshDataAllPayment();

            }  
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }

        }

        private void radioButton3DayAgo_CheckedChanged(object sender, EventArgs e)
        {
            try{

            dateTimePickerStartDate.Value = DateTime.Now.AddDays(-2);
            dateTimePickerEnd.Value = DateTime.Now;

            refreshDataAllPayment();

            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }

        }

        private void radioBoxThisMonth_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
            dateTimePickerStartDate.Value = DateTimeDayOfMonthExtensions.FirstDayOfMonth_AddMethod(DateTime.Now);
            dateTimePickerEnd.Value = DateTime.Now;

            refreshDataAllPayment();

                   }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }

        }

        List<Zone> allZone;
        List<PayType> allPayType;
      //  List<Shipper> allShipper;

        DataTable dataAllPayment;
        string selectfromDate;
        string selecttoDate;
        int selectzoneID; 
        string selectshipper;


        private void defaultTab2()
        {
            allZone = gd.getAllZone(); 
            getComboAllZone();

            allPayType = gd.getAllPayType();
            getComboAllPayType();

            //allShipper = gd.getAllShipper();
            //getComboAllShipper(); 

            refreshDataAllPayment();

        }


         


        private void getComboAllZone()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "++ All Zone ++"));


            foreach (Zone c in allZone)
            {
                data.Add(new KeyValuePair<int, string>(c.ZoneID, c.ZoneName));
            }


            // Clear the combobox
            comboBoxFilterZone.DataSource = null;
            comboBoxFilterZone.Items.Clear();

            // Bind the combobox
            comboBoxFilterZone.DataSource = new BindingSource(data, null);
            comboBoxFilterZone.DisplayMember = "Value";
            comboBoxFilterZone.ValueMember = "Key";

          comboBoxFilterZone.SelectedIndex = 0;
            
        }


        private void getComboAllPayType()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "++ All PayType ++"));


            foreach (PayType c in allPayType)
            {
                data.Add(new KeyValuePair<int, string>(c.PayTypeID, c.PayTypeName));
            }


            // Clear the combobox
            comboBoxFilterPayType.DataSource = null;
            comboBoxFilterPayType.Items.Clear();

            // Bind the combobox
            comboBoxFilterPayType.DataSource = new BindingSource(data, null);
            comboBoxFilterPayType.DisplayMember = "Value";
            comboBoxFilterPayType.ValueMember = "Key";

            comboBoxFilterPayType.SelectedIndex = 0;

        }
         

        private void refreshDataAllPayment()
        {

            selectfromDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
            selecttoDate = dateTimePickerEnd.Value.ToString("yyyyMMdd");
            selectzoneID = Int32.Parse(comboBoxFilterZone.SelectedValue.ToString());  


            if (comboBoxFilterShipper.SelectedIndex > 0)
            {
                selectshipper = comboBoxFilterShipper.Text;
            }
            else
            {
                selectshipper = "";
            }

            dataAllPayment = gd.getAllPayment(selectfromDate, selecttoDate, selectzoneID,  selectshipper);
            dataGridViewPaymentTrn.DataSource = dataAllPayment;



            dataGridViewPaymentTrn.Columns[1].HeaderText = "วันที่";
            dataGridViewPaymentTrn.Columns[2].HeaderText = "บิล";
            dataGridViewPaymentTrn.Columns[3].HeaderText = "ช่องทาง";
            dataGridViewPaymentTrn.Columns[4].HeaderText = "ยอดเงิน";
            dataGridViewPaymentTrn.Columns[5].HeaderText = "มัดจำ";
            dataGridViewPaymentTrn.Columns[6].HeaderText = "รหัสลูกค้า";
            dataGridViewPaymentTrn.Columns[7].HeaderText = "ชื่อลูกค้า";
            dataGridViewPaymentTrn.Columns[8].HeaderText = "ที่อยู่";
            dataGridViewPaymentTrn.Columns[9].HeaderText = "Post Code";
            dataGridViewPaymentTrn.Columns[10].HeaderText = "Tel";
            dataGridViewPaymentTrn.Columns[11].HeaderText = "สมาชิก";
            dataGridViewPaymentTrn.Columns[12].HeaderText = "ขนส่ง";
            dataGridViewPaymentTrn.Columns[13].HeaderText = "เลขขนส่ง";
            dataGridViewPaymentTrn.Columns[14].HeaderText = "Remark";

            dataGridViewPaymentTrn.Columns[6].Visible = false;
            dataGridViewPaymentTrn.Columns[11].Visible = false; 

        }

        private void comboBoxFilterChange_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                refreshDataAllPayment();

            }
            catch (Exception ex)
            {

            }
        }

        private void radioButtonPayY_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPayN.Checked)
            {
                comboBoxFilterPayType.SelectedValue = 6;
            }
            else
            {
                comboBoxFilterPayType.SelectedValue = 0;
            }
        }

        private void radioButtonPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPayment.Checked)
            {
                panelPayment.Visible = true;
            }
            else
            {
                panelPayment.Visible = false;
            }
        }

        float upPayAmt = 0;
        float upEarning = 0;
        float upRemain = 0;

        int upPayType = 0;
        string upShipper = "";
        string upShippingNo = "";
        string upShippingRemark = "";

        int upPayCustID = 0;

        private void dataGridViewPaymentTrn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                this.trnBillIDSect = Int32.Parse(dataGridViewPaymentTrn.Rows[e.RowIndex].Cells["TrnID"].Value.ToString());

                upPayAmt = float.Parse( dataGridViewPaymentTrn.Rows[e.RowIndex].Cells["PayAmount"].Value.ToString());

                upEarning =  float.Parse( dataGridViewPaymentTrn.Rows[e.RowIndex].Cells["EarningAmt"].Value.ToString());

               
                if (upEarning > 0)
                    upRemain = upPayAmt - upEarning;
                else 
                    upRemain = 0; 

                upPayType = Int32.Parse(dataGridViewPaymentTrn.Rows[e.RowIndex].Cells["PayTypeID"].Value.ToString());
                upShipper = dataGridViewPaymentTrn.Rows[e.RowIndex].Cells["ShipName"].Value.ToString();
                upShippingNo = dataGridViewPaymentTrn.Rows[e.RowIndex].Cells["ShipNo"].Value.ToString();
                upShippingRemark = dataGridViewPaymentTrn.Rows[e.RowIndex].Cells["ShipRem"].Value.ToString();

                upPayCustID = Int32.Parse(dataGridViewPaymentTrn.Rows[e.RowIndex].Cells["PayCustID"].Value.ToString());

                textBoxBill_Up.Text = this.trnBillIDSect.ToString();
                textBoxSalesAmt_Up.Text = this.upPayAmt.ToString();
                textBoxEarningAmt_Up.Text = this.upEarning.ToString();
                textBoxRemain_Up.Text = this.upRemain.ToString();

                if (upPayType == 1) 
                    radioBoxCash.Checked = true;
                else if (upPayType == 2) 
                    radioBoxCreditCard.Checked = true;
                else if (upPayType == 3) 
                    radioBoxCreditCust.Checked = true; 
                else if (upPayType == 4) 
                    radioBoxCashCard.Checked = true; 
                else if (upPayType == 5) 
                    radioButtonBanking.Checked = true; 
                else if (upPayType == 6) 
                    radioBoxWaitPay.Checked = true;

                comboBoxAllCust_Up.SelectedValue = upPayCustID;

                comboBoxAllShipper_Up.Text = upShipper;
                textBoxShippingNo_Up.Text = upShippingNo;
                textBoxShippingRemark_Up.Text = upShippingRemark;
                 
            }
            catch (Exception ex)
            {

            }
        }

        private void buttonUpdateBill_Up_Click(object sender, EventArgs e)
        {
            int custID = 0;
            string custName = "";
            int type = 0;

            int billID = this.trnBillIDSect;

            try
            {


                this.upEarning = float.Parse(textBoxEarningAmt_Up.Text);

                if (radioBoxCash.Checked == true)
                    upPayType = 1;
                else if (radioBoxCreditCard.Checked == true)
                    upPayType = 2;
                else if (radioBoxCreditCust.Checked == true)
                    upPayType = 3;
                else if (radioBoxCashCard.Checked == true)
                    upPayType = 4;
                else if (radioButtonBanking.Checked == true)
                    upPayType = 5;
                else if (radioBoxWaitPay.Checked == true)
                    upPayType = 6;

                upPayCustID = Int32.Parse(comboBoxAllCust_Up.SelectedValue.ToString());

                upShipper = comboBoxAllShipper_Up.Text;
                upShippingNo = textBoxShippingNo_Up.Text;
                upShippingRemark = textBoxRemain_Up.Text;


                if (radioButtonWithTax_2.Checked == true)
                    type = 1;

                if (MessageBox.Show("คุณแน่ใจว่าจะเพิ่ม แก้ไขข้อมูลบิล" + this.trnBillIDSect + "\n ในชื่อลูกค้า :" + custName + " หรือไม่ ? \n", "ยืนยัน", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    int result = gd.UpsTaxAgo(this.trnBillIDSect, upPayCustID, type, upShipper, upShippingNo, upShippingRemark, this.upEarning, 0, upPayType);

                    if (result <= 0)
                    {
                        throw new Exception("ไม่สามารถ แก้ไขข้อมูลบิล ได้");
                    }

                    getComboAllTrnID();
                    //comboBoxBillNo.SelectedValue = billID;

                    refreshDataAllPayment();


                    textBoxBill_Up.Text = "0";
                    textBoxSalesAmt_Up.Text = "0";
                    textBoxEarningAmt_Up.Text = "0";
                    textBoxRemain_Up.Text = "0";
                    comboBoxAllShipper_Up.Text = "";
                    textBoxShippingNo_Up.Text = "";
                    textBoxRemain_Up.Text = "";

                    comboBoxAllCust_Up.SelectedValue = 0;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportData.Excel_FromDataTable_DT(dataGridViewPaymentTrn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void textBoxSearchCustName_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string srCustName = textBoxSearchCustName.Text;
                string srTelNo = textBoxSearchCustTelNo.Text;


                if ((srCustName.Length > 0) && (srTelNo.Length > 0))
                {
                    this.dataAllPayment.DefaultView.RowFilter = string.Format("CustName like '*{0}*' and Tel like '*{1}*'", srCustName, srTelNo);
                }
                else if ((srCustName.Length == 0) && (srTelNo.Length > 0))
                {
                    this.dataAllPayment.DefaultView.RowFilter = string.Format("Tel like '*{0}*'", srTelNo);
                }
                else if ((srCustName.Length > 0) && (srTelNo.Length == 0))
                {
                    this.dataAllPayment.DefaultView.RowFilter = string.Format("CustName like '*{0}*'", srCustName);
                }
                else
                {
                    this.dataAllPayment.DefaultView.RowFilter = string.Format("1 = 1", 1);  // All
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonSearchMEM_Click(object sender, EventArgs e)
        {
            panelSearchCustomer.Visible = true;
        }

        private void buttonCloseBC_Click(object sender, EventArgs e)
        {
            panelSearchCustomer.Visible = false;
        }

        private void searchMemCard()
        { 

            try
            {

                string srMemName = textBoxSRMemName.Text;
                string strSearchMemCard = textBoxStrSearchMemCardtoTable.Text;
                string srMemTel = textBoxSRTel.Text; 

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
            catch (Exception ex)
            { 
            }
        }

        private void textBoxStrSearchMemCardtoTable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                searchMemCard();
            }
        }

        private void textBoxSRMemName_TextChanged(object sender, EventArgs e)
        {
            searchMemCard();
        }

        private void textBoxSRTel_TextChanged(object sender, EventArgs e)
        {
            searchMemCard();
        }

        private void dataGridViewAllMember_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridproductID = Int32.Parse(dataGridViewAllMember.Rows[e.RowIndex].Cells["CustID"].Value.ToString());

                comboBoxListCust.SelectedValue = dataGridproductID;

                //comboBoxAllMember.Visible = true;
                //labelHeader.Text = "แก้ไขข้อมูล";
                //buttonAddTable.Text = "แก้ไขข้อมูล";
                //radioButtonUpdateData.Checked = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void OnPrintPage_Reprint(object sender, PrintPageEventArgs e)
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

                Branch branch = gd.getBranchDesc();

                /* 
                 
                 * Default Bill Config @ Program
                 * 
                 * EN , TH , NO
                 * 
                 */

                string langBill = this.flagLang;

               


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
                Bitmap img2 = global::AppRest.Properties.Resources.Footer;

                Bitmap imgLogoFB = global::AppRest.Properties.Resources.Logo_FB;
                Bitmap imgLogoIG = global::AppRest.Properties.Resources.Logo_IG;

                //salesNextBill = gd.salesNextBillDay("0") ;
                //e.Graphics.DrawString("Q#" + salesNextBill.ToString(), fontHeader, brush, x + 185, y); 
                //y += 20;

                e.Graphics.DrawString("copy", fontBody, brush, x + 150, y);
                 

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

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd MMM yy}", dt);
                string strTime = String.Format("{0:HH:mm}", dt);

                int noofCust = 0;

                foreach (Transaction o in trn)
                {
                    if (o.ProductID == 4)
                        noofCust = Int32.Parse(o.SalesQTY.ToString());
                }

                e.Graphics.DrawString("-------------------------------------------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 12;
                e.Graphics.DrawString(this.textBoxtableName.Text, fontSubHeader, brush, x + 10, y);
                e.Graphics.DrawString("Cust " + noofCust.ToString(), fontSubHeader, brush, x + 220, y);
                y += 15;
                e.Graphics.DrawString(textBoxPayTime.Text, fontSubHeader, brush, x + 80, y);
                y += 10;
                e.Graphics.DrawString("-------------------------------------------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 15;

                //// Print Order

                int i = 1;

                float salesAmountFood = 0;
                float salesAmountDrinks = 0;
                float salesAmount = 0;
                float tax = 0;
                float serviceCharge = 0;
                float discount = 0;

                string str1 = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";
                string trnProductRemark = "";

                List<string> txtPrint;
                int len = 30;

                foreach (Transaction o in trn)
                {

                    if (o.GroupCatID > 0 && trnProductRemark != "TOPPING" && !((o.ProductID > 10 && o.ProductID <= 100) && o.ProductID != 4))
                    {

                        if (this.flagLang == "TH")
                        {
                            str1 = o.ProductName.Trim(); ;
                        }
                        else
                        {
                            str1 = o.ProductNameEN.Trim(); ;
                        }

                        str2 = o.SalesQTY.ToString();
                        str3 = (o.SalesAmount / o.SalesQTY).ToString("###,###");
                        str4 = o.SalesAmount.ToString("###,###.#0");

                        str4 = String.Format("{0,10}", str4);



                        //if (o.SalesQTY > 1)
                        //    str1 += "(" + str3 + ")";

                        trnProductRemark = o.TrnRemark;


                        if (this.flagFD == 0)
                        {

                            e.Graphics.DrawString(str2, fontBodylist, brush, x + 0, y);

                            e.Graphics.DrawString(str4, fontNum, brush, x + 180, y);

                            txtPrint = FuncString.WordWrap(str1, len);
                            str1 = "";

                            foreach (string op in txtPrint)
                            {
                                e.Graphics.DrawString(op, fontBodylist, brush, x + 20, y);
                                y += 17;
                            }

                            i++;
                            //  y += 15;

                            //if (trnProductRemark.Trim().Length > 1 )
                            //{
                            //    string[] remarkString = trnProductRemark.Remove(0, 1).Split('+');

                            //    foreach (string r in remarkString)
                            //    {
                            //        str1 = "  +" + r.Split('{')[0] + "\r\n";

                            //        e.Graphics.DrawString(str1, fontBodylist, brush, x + 22, y);
                            //        y += 15;
                            //    }
                            //}

                        }


                        if (o.GroupCatID == 1)
                        {

                            salesAmountFood += o.SalesAmount;
                        }
                        else
                        {

                            salesAmountDrinks += o.SalesAmount;
                        }

                        salesAmount += o.SalesAmount;

                    }
                    else
                    {

                        if (o.ProductID == 1)
                        {
                            discount = o.SalesAmount * -1;
                        }
                        else if (o.ProductID == 3)
                        {
                            tax = o.SalesAmount;
                        }
                        else if (o.ProductID == 2)
                        {
                            serviceCharge = o.SalesAmount;
                        }


                    }

                }

                //////////////////////////////////////////////////////////////////////////
                string txtOrder = "";

                txtOrder = "";
                string txtAmt = "";


                if (this.flagFD == 0)
                {

                    //y += 15;
                    //txtOrder = "FOOD";
                    //txtAmt = salesAmountFood.ToString("###,###");
                    //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    //e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 230, y);

                    //y += 15;
                    //txtOrder = "BEVERAGE/OTHERs";
                    //txtAmt = salesAmountDrinks.ToString("###,###");
                    //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    //e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 230, y);

                }
                else
                {

                    e.Graphics.DrawString("อาหารและเครื่องดื่ม", fontBodylist, brush, x + 22, y);
                    e.Graphics.DrawString("1", fontBodylist, brush, x + 0, y);
                    str4 = (salesAmount).ToString("###,###");
                    str4 = String.Format("{0,10}", str4);
                    e.Graphics.DrawString(str4, fontBodylist, brush, x + 180, y);

                    y += 15;
                }


                //////////////////////////////////////////////////////////////////////////
                //    string txtOrder = "";

                txtOrder = "";
                //   string txtAmt = "";

                //y += 15;
                //txtOrder = "Amount";
                //txtAmt = (salesAmountFood + salesAmountDrinks).ToString("###,###");
                //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                //e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 230, y);

                //y += 15;
                //txtOrder = "DRINK / OTHERS";
                //txtAmt = salesAmountDrinks.ToString("###,###");
                //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                //e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 230, y);

                if (discount > 0)
                {
                    y += 15;

                    txtOrder = "Discount (Total)";
                    txtAmt = discount.ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);
                }


                if (serviceCharge > 0)
                {
                    y += 15;
                    txtOrder = "Service " + ((float)(this.servicePercent * 100)).ToString() + "%";
                    txtAmt = serviceCharge.ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                }

                if (tax > 0)
                {

                    y += 15;
                    txtOrder = "Amount Before Vat ";
                    txtAmt = (tax * (100.00 / 7.00)).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    y += 15;
                    txtOrder = "Vat " + ((float)(this.taxPercent * 100)).ToString() + "%";
                    txtAmt = tax.ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    y += 15;
                    txtOrder = "Rounding ";
                    txtAmt = (payment.PayAmount - tax * (107.00 / 7.00)).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                }


                y += 25;
                txtOrder = "Total";
                txtAmt = payment.PayAmount.ToString("###,###.#0");
                e.Graphics.DrawString(txtOrder, fontTable, brush, x + 20, y);
                e.Graphics.DrawString(txtAmt, fontTable, brush, x + 185, y);

                y += 20;



                //////////////////////////////////////////////////////////////////////


                y += 20;


                string billNo = "#" + payment.TrnID.ToString();
                string billNoFull = "#" + payment.TrnInvID.ToString();

                int payTypeID = 0;
                float payAmount = 0;
                string PayCardType = "";
                string PayCardName = "";
                string PayCardNo = "";
                string PayCashCardCode = "";

                e.Graphics.DrawString("-------------------------------------------------------------------------------------", fontSubHeader, brush, x, y);

                y += 15;

                if (langBill == "TH")
                {
                    e.Graphics.DrawString(" " + "เลขที่ใบกำกับภาษี (TAX INV) : " + billNoFull, fontBody, brush, x + 10, y);
                    y += 20;
                    e.Graphics.DrawString(" " + "บิลเลขที่ (Bill Ref No.) : " + billNo, fontBody, brush, x + 10, y);
                    y += 20;
                    i = 1;
                    foreach (DataRow dr in dtBillPayment.Rows)
                    {
                        payTypeID = Int32.Parse(dr[dtBillPayment.Columns["PayTypeID"]].ToString());
                        payAmount = float.Parse(dr[dtBillPayment.Columns["PayAmount"]].ToString());
                        PayCardType = dr[dtBillPayment.Columns["PayCreditCardType"]].ToString();
                        PayCardName = dr[dtBillPayment.Columns["PayCreditCardNo"]].ToString();
                        PayCardNo = dr[dtBillPayment.Columns["PayCreditCardCust"]].ToString();
                        PayCashCardCode = dr[dtBillPayment.Columns["CashCardCode"]].ToString();

                        if (payTypeID == 1)
                        {
                            e.Graphics.DrawString(i.ToString() + ". ชำระเงินสด ยอด  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                        }
                        else if (payTypeID == 2)
                        {
                            e.Graphics.DrawString(i.ToString() + ". ชำระบัตรเครดิต ยอด  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - ประเภท : " + PayCardType, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - เลขที่   : " + PayCardNo, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - ชื่อลูกค้า : " + PayCardName, fontBody, brush, x + 10, y); y += 15;
                        }
                        else if (payTypeID == 3)
                        {
                            e.Graphics.DrawString(i.ToString() + ". ลงบิลลูกค้า ยอด  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - ชื่อ :" + payment.PayCustName, fontBody, brush, x + 10, y); y += 15;
                        }
                        else if (payTypeID == 4)
                        {
                            e.Graphics.DrawString(i.ToString() + ". บัตรเงินสด ยอด  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - รหัสบัตร    : " + PayCashCardCode, fontBody, brush, x + 10, y); y += 15;
                        }
                        else if (payTypeID == 5)
                        {
                            e.Graphics.DrawString(i.ToString() + ". QR/โอนเงิน ยอด  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - ประเภท   : " + PayCardType, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - รายละเอียด : " + PayCardNo, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - ชื่อลูกค้า   : " + PayCardName, fontBody, brush, x + 10, y); y += 15;
                        }

                        i++;

                    }

                }
                else
                {
                    e.Graphics.DrawString(" " + "เลขที่ใบกำกับภาษี (TAX INV) : " + billNoFull, fontBody, brush, x + 10, y);
                    y += 20;
                    e.Graphics.DrawString(" " + "บิลเลขที่ (Bill Ref No.) : " + billNo, fontBody, brush, x + 10, y);
                    y += 20;


                    i = 1;
                    foreach (DataRow dr in dtBillPayment.Rows)
                    {
                        payTypeID = Int32.Parse(dr[dtBillPayment.Columns["PayTypeID"]].ToString());
                        payAmount = float.Parse(dr[dtBillPayment.Columns["PayAmount"]].ToString());
                        PayCardType = dr[dtBillPayment.Columns["PayCreditCardType"]].ToString();
                        PayCardName = dr[dtBillPayment.Columns["PayCreditCardNo"]].ToString();
                        PayCardNo = dr[dtBillPayment.Columns["PayCreditCardCust"]].ToString();
                        PayCashCardCode = dr[dtBillPayment.Columns["CashCardCode"]].ToString();

                        if (payTypeID == 1)
                        {
                            e.Graphics.DrawString(i.ToString() + ". Cash Payment  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                        }
                        else if (payTypeID == 2)
                        {
                            e.Graphics.DrawString(i.ToString() + ". Credit Card Payment   :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Type    : " + PayCardType, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Card No.:  " + PayCardNo, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Name    : " + PayCardName, fontBody, brush, x + 10, y); y += 15;
                        }
                        else if (payTypeID == 3)
                        {
                            e.Graphics.DrawString(i.ToString() + ". Credit customer  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Name :" + payment.PayCustName, fontBody, brush, x + 10, y); y += 15;
                        }
                        else if (payTypeID == 4)
                        {
                            e.Graphics.DrawString(i.ToString() + ". Cash Card Payment :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Cashcard Code : " + PayCashCardCode, fontBody, brush, x + 10, y); y += 15;
                        }
                        else if (payTypeID == 5)
                        {
                            e.Graphics.DrawString(i.ToString() + ". QR / Banking Payment  :" + payAmount.ToString("###,###.#0"), fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Type        : " + PayCardType, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Description : " + PayCardNo, fontBody, brush, x + 10, y); y += 15;
                            e.Graphics.DrawString("  - Name        : " + PayCardName, fontBody, brush, x + 10, y); y += 15;
                        }

                        i++;

                    }


                }



      
                e.Graphics.DrawString(" THANK YOU / ขอบคุณค่ะ ", fontBody, brush, x + 70, y);
                y += 20;
                e.Graphics.DrawImage(imgLogoFB, x + 75, y - 5, 18, 18);
                e.Graphics.DrawString(this.fblink, fontBody, brush, x + 98, y);
                y += 20;
                e.Graphics.DrawImage(imgLogoIG, x + 75, y - 5, 18, 18);
                e.Graphics.DrawString(this.iglink, fontBody, brush, x + 98, y);
                y += 15;

                if (qrlink.Length > 0)
                {
                    Bitmap img3 = global::AppRest.Properties.Resources.QR;
                    e.Graphics.DrawImage(img3, x + 63, y, 150, 150);
                    y += 145;
                }

                e.Graphics.DrawString(this.restlink, fontBody, brush, x + 70, y);

                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonPrintBill_Reprimt_Click(object sender, EventArgs e)
        {
            this.flagFD = 0;

            payment = gd.getTrnPayment(trnBillIDSect);

            printBill_Reprint.Print();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.flagFD = 0;

            payment = gd.getTrnPayment(trnBillIDSect);

            printBill_Reprint.Print();
        }

        private void buttonSalesNoti_Click(object sender, EventArgs e)
        {
            SalesEnddayByLINE();
            MsgNotify.lineNotify(this.msgtoLINE);
        }

        private void buttonSummary1_Click(object sender, EventArgs e)
        {
            printBillProduct1.Print();
        }

        private void buttonSummary2_Click(object sender, EventArgs e)
        {
            printBillProduct2.Print();
        }

        private void printBillProduct1_PrintPage(object sender, PrintPageEventArgs e)
        {

            try
            {
                int x = 0;
                int y = 0;

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

                Branch branch = gd.getBranchDesc();

                /* 
                 
                 * Default Bill Config @ Program
                 * 
                 * EN , TH , NO
                 * 
                 */
                string langBill = "EN";


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

                    int iii = 0;
                    foreach (string s in ArrgrestTaxRD)
                    {
                        if (s == Login.posBranchID.ToString())
                        {
                            appTaxRD = ArrgrestTaxRD[iii + 1];
                        }

                        iii++;
                    }
                }

                string DateDay = comboBoxDate.Text;
                List<SalesToday> salesIN = gd.getSalesByday(301, DateDay);


                ///////////////////////////


                Brush brush = new SolidBrush(Color.Black);

                Font fontHeader = new Font("Arail", 12);
                Font fontTable = new Font("Arail", 11);
                Font fontSubHeader = new Font("Arail", 8);
                Font fontBody = new Font("Arail", 8);
                Font fontBodylist = new Font("Arail", 8);
                Font fontNum = new Font("Consolas", 8);
                Font fontBodylistPro = new Font("Arail", 7);



                e.Graphics.DrawString(restName, fontBody, brush, x + 10, y);

                y += 12;



                e.Graphics.DrawString(restAddr1, fontBody, brush, x + 10, y);

                y += 12;


                if (restAddr2.Trim().Length > 0)
                {
                    e.Graphics.DrawString(restAddr2, fontBody, brush, x + 10, y);

                    y += 12;

                }


                if (restTaxID.Trim().Length > 0)
                {
                    e.Graphics.DrawString("" + restTaxID, fontBody, brush, x + 40, y);

                    y += 12;
                }



                if (appTaxRD.Trim().Length > 0)
                {
                    e.Graphics.DrawString("TAX RD : " + appTaxRD, fontBody, brush, x + 40, y);

                    y += 12;
                }


                if (restTel.Trim().Length > 0)
                {
                    e.Graphics.DrawString(restTel, fontBody, brush, x + 60, y);

                    y += 12;

                }

                y += 20;

                e.Graphics.DrawString("สรุปรายการสินค้า #1 : " + DateDay, fontSubHeader, brush, x + 50, y);
                y += 20;
                e.Graphics.DrawString("พิมพ์ " + DateTime.Now.ToString(), fontBody, brush, x + 50, y);

                y += 25;


                int i = 1;


                string txtAmt = "";



                string txtOrder = "";
                string lb = "";
                float val = 0;
                int ii = 1;

                // this.msgtoLINE = "";

                foreach (SalesToday st in salesIN)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = lb;

                    if (val == 0)
                    {
                        txtAmt = "";
                    }
                    else
                    {
                        txtAmt = (val).ToString("###,###.#0") + "";
                        txtAmt = String.Format("{0,10}", txtAmt);
                    }
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    //  this.msgtoLINE = txtOrder + " : " + txtAmt + "\n\r";


                    ii++;
                    y += 15;
                }


                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printBillProduct2_PrintPage(object sender, PrintPageEventArgs e)
        {

            try
            {
                int x = 0;
                int y = 0;

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

                Branch branch = gd.getBranchDesc();

                /* 
                 
                 * Default Bill Config @ Program
                 * 
                 * EN , TH , NO
                 * 
                 */
                string langBill = "EN";


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

                    int iii = 0;
                    foreach (string s in ArrgrestTaxRD)
                    {
                        if (s == Login.posBranchID.ToString())
                        {
                            appTaxRD = ArrgrestTaxRD[iii + 1];
                        }

                        iii++;
                    }
                }

                string DateDay = comboBoxDate.Text;
                List<SalesToday> salesIN = gd.getSalesByday(302, DateDay);


                ///////////////////////////


                Brush brush = new SolidBrush(Color.Black);

                Font fontHeader = new Font("Arail", 12);
                Font fontTable = new Font("Arail", 11);
                Font fontSubHeader = new Font("Arail", 8);
                Font fontBody = new Font("Arail", 8);
                Font fontBodylist = new Font("Arail", 8);
                Font fontNum = new Font("Consolas", 8);
                Font fontBodylistPro = new Font("Arail", 7);



                e.Graphics.DrawString(restName, fontBody, brush, x + 10, y);

                y += 12;



                e.Graphics.DrawString(restAddr1, fontBody, brush, x + 10, y);

                y += 12;


                if (restAddr2.Trim().Length > 0)
                {
                    e.Graphics.DrawString(restAddr2, fontBody, brush, x + 10, y);

                    y += 12;

                }


                if (restTaxID.Trim().Length > 0)
                {
                    e.Graphics.DrawString("" + restTaxID, fontBody, brush, x + 40, y);

                    y += 12;
                }



                if (appTaxRD.Trim().Length > 0)
                {
                    e.Graphics.DrawString("TAX RD : " + appTaxRD, fontBody, brush, x + 40, y);

                    y += 12;
                }


                if (restTel.Trim().Length > 0)
                {
                    e.Graphics.DrawString(restTel, fontBody, brush, x + 60, y);

                    y += 12;

                }

                y += 20;

                e.Graphics.DrawString("สรุปรายการสินค้า #2 : " + DateDay, fontSubHeader, brush, x + 50, y);
                y += 20;
                e.Graphics.DrawString("พิมพ์ " + DateTime.Now.ToString(), fontBody, brush, x + 50, y);

                y += 25;


                int i = 1;


                string txtAmt = "";



                string txtOrder = "";
                string lb = "";
                float val = 0;
                int ii = 1;

                // this.msgtoLINE = "";

                foreach (SalesToday st in salesIN)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = lb;

                    if (val == 0)
                    {
                        txtAmt = "";
                    }
                    else
                    {
                        txtAmt = (val).ToString("###,###.#0") + "";
                        txtAmt = String.Format("{0,10}", txtAmt);
                    }
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    //  this.msgtoLINE = txtOrder + " : " + txtAmt + "\n\r";


                    ii++;
                    y += 15;
                }


                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
