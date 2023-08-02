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

namespace AppRest
{
    public partial class AddEndDays_Cashier : AddDataTemplate
    {
        GetDataRest gd;
        MainManage formMainManage;


        List<TrnDate> listPeriods;
        List<TrnDate> listDates;

        List<Transaction> trn;
        List<Transaction> trnDay;
        TrnMax trnMax;

        string trnPeriodSect;
        string trnDateSect;

        List<EndBill> endBill;

        string printerCashName;

        string cashDate = "";
        int shiftID = 0;
        float cashAmount = 0;
        string cashDesc = "";
        string cashRem = "";
        string cashUseBy = "";

        List<Member> allMems;

        int shiftIDFromSystem = 0;

        double servicePercent;
        double taxPercent;


        public AddEndDays_Cashier(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();

            this.Width = 1024;
            this.Height = 764;

            panelCountMoney.Visible = false;


            printerCashName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();
            printCashDrawer.PrinterSettings.PrinterName = this.printerCashName;
            printCashEndBill.PrinterSettings.PrinterName = this.printerCashName;
            printDocument3.PrinterSettings.PrinterName = this.printerCashName;
            printCloseShip.PrinterSettings.PrinterName = this.printerCashName;

            gd = new GetDataRest();

            allMems = gd.getListAllMember(); 
            getComboAllMem();
             

            defaultFunc();


            this.cashDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
          //  MessageBox.Show(this.cashDate);

            shiftIDFromSystem = gd.getCashDrawerShiftIDByPOSID(this.cashDate);
            this.shiftID = shiftIDFromSystem;

            comboBoxShiftID.Text = shiftIDFromSystem.ToString();



            taxPercent = Double.Parse(ConfigurationSettings.AppSettings["TaxPercent"].ToString());
            servicePercent = Double.Parse(ConfigurationSettings.AppSettings["ServiceChargePercent"].ToString());

            textBoxRealMoney.Visible = false;
            labelCash.Visible = false;
            buttonViewCash.Visible = false;

            buttonDelCloseShift.Visible = false;
            buttonDelOpenShift.Visible = false;

            if (Login.userStatus == "Admin" || Login.userStatus == "Manager")
            {
                textBoxRealMoney.Visible = true;
                labelCash.Visible = true;
                buttonViewCash.Visible = true;

                buttonDelCloseShift.Visible = true;
                buttonDelOpenShift .Visible = true;
            }


            getDataTablePL();
        }


        private void defaultFunc()
        {
            // Before 6 Am is Today
            dateTimePickerStartDate.Value = DateTime.Now.AddHours(-6);
            textBoxCashStartDay.Text = "0";
            textBoxCashCloseDay.Text = "0";


            txtBoxOutAmt.Text = "0";
            comboBoxOutUseBy.Text = "Select Member";
            comboBoxOutGlAcc.Text = "รายละเอียดรายจ่าย";
            textBoxOutRem.Text = "เหตุผล";


            txtBoxInAmt.Text = "0";
            comboBoxInUseBy.Text = "Select Member";
            comboBoxInGlAcc.Text = "รายละเอียดรายรับ";
            textBoxInRem.Text = "เหตุผล";
                
        }

     

        private void getComboAllMem()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "Select Member"));

            foreach (Member c in allMems)
            {
                if( c.Status != "Work" && c.Status != "Customer" )
                    data.Add(new KeyValuePair<int, string>(c.UserID, c.Name));
            }


            // Clear the combobox
            comboBoxInUseBy.DataSource = null;
            comboBoxInUseBy.Items.Clear();

            // Bind the combobox
            comboBoxInUseBy.DataSource = new BindingSource(data, null);
            comboBoxInUseBy.DisplayMember = "Value";
            comboBoxInUseBy.ValueMember = "Key";


            // Clear the combobox
            comboBoxOutUseBy.DataSource = null;
            comboBoxOutUseBy.Items.Clear();

            // Bind the combobox
            comboBoxOutUseBy.DataSource = new BindingSource(data, null);
            comboBoxOutUseBy.DisplayMember = "Value";
            comboBoxOutUseBy.ValueMember = "Key";

        }
         

        private void FnCalMoney_TextChange(object sender, EventArgs e)
        {
            try
            {

                 int unit = 0;

                TextBox txtCal = (TextBox)sender;

                string strVal = txtCal.Name.Replace("textCalNum_", "").ToString();

                int val = Int32.Parse(strVal);


                if (txtCal.Text.Length > 0)
                {
                    unit = Int32.Parse(txtCal.Text);
                } 

                int subTotal = val * unit;

                //  MessageBox.Show(subTotal.ToString());

                string strAmt = "textBox_Amt_" + strVal;

                TextBox txtAmt = (TextBox)this.Controls.Find(strAmt, true)[0];  //this.Controls[strAmt];

                txtAmt.Text = subTotal.ToString("###,###,##0");

            }
            catch (Exception ex)
            {
                 
            }
            finally
            {
                fn_CountMoney();
            }

        }

        private void fn_CountMoney()
        {
            try
            {

                string nText = "";
                int totalCountMoney = 0;

                string[] textNums = new string[] { "1000", "500", "100", "50", "20", "10", "5", "2", "1" };


                foreach (string strNum in textNums)
                {

                    nText = "textBox_Amt_" + strNum;

                    TextBox txtAmt = (TextBox)this.Controls.Find(nText, true)[0];

                    if (txtAmt.Text.Length > 0)
                        totalCountMoney += Int32.Parse(txtAmt.Text, NumberStyles.AllowThousands);

                }

                textBox_Amt_Total.Text = totalCountMoney.ToString("###,###,###");
             //   textBoxBalanceReal.Text = totalCountMoney.ToString("###,###,###");

            }catch(Exception ex)
            {

            }
                                      
        }




        private void buttonCalMoneyClose_Click(object sender, EventArgs e)
        {
            panelCountMoney.Visible = false;
            textBoxCashCloseDay.Text = textBox_Amt_Total.Text;
        }

        private void buttonCountMoney_Click(object sender, EventArgs e)
        {

            panelCountMoney.Visible = true;
            textCalNum_1000.Focus();

            printDocument3.Print();

            RawPrinterHelper.OpenCashDrawer1(this.printerCashName);
        }

        private void buttonInputCashStart_Click(object sender, EventArgs e)
        {
            try
            {

                this.cashAmount = float.Parse(textBoxCashStartDay.Text);

                if (this.cashAmount > 0)
                {
                     
                    this.cashDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
                    this.shiftID = Int32.Parse(comboBoxShiftID.Text); 
                    this.cashDesc = "เงินทอนเปิดร้าน";
                    this.cashRem = "OS";
                    this.cashUseBy = Login.userName;

                    openCashDrawer(); 
                }

            }
            catch (Exception ex)
            {
                
            }
        }

        private void openCashDrawer()
        {
            try
            {

                int result = gd.instCashDrawer(this.cashDate, this.shiftID, this.cashDesc, this.cashAmount, this.cashRem, this.cashUseBy);


                if (result <= 0)
                    throw new Exception("ไม่สามารถเปิดลิ้นชักได้ กรุณาตรวจสอบข้อมูลอีกครั้ง");
                else
                {
                    RawPrinterHelper.OpenCashDrawer1(this.printerCashName);
                    printCashDrawer.Print();
                    MessageBox.Show("ทำรายการสำเร็จ !!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message ) ;
            }
          
        }

        string msgtoLINE = "";

        private void buttonEndDayCashDrawer_Click(object sender, EventArgs e)
        {
            try
            {
                this.cashAmount = float.Parse(textBoxCashCloseDay.Text);


                int cashdrawerStatus = gd.getcashDrawerStatus(1);

                if (cashdrawerStatus != 10)
                    throw new Exception(cashdrawerStatus.ToString());


                if (this.cashAmount > 0)
                {

                    this.cashDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
                    this.shiftID = Int32.Parse(comboBoxShiftID.Text);
                    this.cashDesc = "เงินทอนปิดร้าน";
                    this.cashRem = "CS";
                    this.cashUseBy = Login.userName;

                    openCashDrawer();
                }



                SalesEnddayByLINE();
                MsgNotify.lineNotify(this.msgtoLINE);

                printCashEndBill.Print();

            }
            catch (Exception ex)
            {
                if (FuncString.IsNumeric(ex.Message))
                {
                    if (ex.Message == "-10")
                        MessageBox.Show("ยังมี ORDER ค้าง ไม่สามารถยอดได้");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SalesEnddayByLINE()
        {
            try
            {

                int x = 0;
                int y = 0;

               // string DateDay = comboBoxDate.Text;


                List<SalesToday> salesIN = gd.getSalesByday(101, this.cashDate);
                 

                Branch branch = gd.getBranchDesc(); 

                /////////////////

                 

                this.msgtoLINE = "\n\r";

                this.msgtoLINE += branch.BranchNameEN + "\n\r";

                this.msgtoLINE += "สรุปยอดขาย  " + dateTimePickerStartDate.Value.ToString("dd MMM yyyy") + "\n\r";
                this.msgtoLINE += "ส่งยอด " + DateTime.Now.ToString() + "\n\r";

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

                    if (val != 0)
                         txtAmt = (val).ToString("###,###.#0") + "";
                    else
                        txtAmt = "";

                    if (txtOrder.Length >0)
                       this.msgtoLINE += txtOrder + " : " + txtAmt + "\n";


                    ii++;
                    y += 15;
                }


                this.msgtoLINE += "______________ " + "\n\r";

                List<SalesToday> dataEndBill = gd.getCashDrawerEndDay(1, this.cashDate, this.shiftID);

                this.msgtoLINE += "ใบปิดยอด " + this.cashDate + "\n\r";

                foreach (SalesToday st in dataEndBill)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = lb;

                    if (val != 0)
                        txtAmt = (val).ToString("###,###.#0") + "";
                    else
                        txtAmt = "";

                    if (txtOrder.Length > 0)
                          this.msgtoLINE += txtOrder + " : " + txtAmt + "\n";


                    ii++;
                    y += 15;
                }

                 

                y += 20;

                ////////////////////////////////////////////////////////////////////////


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonInSAVE_Click(object sender, EventArgs e)
        {
            try
            {

                this.cashAmount = float.Parse(txtBoxInAmt.Text);

                if (this.cashAmount > 0)
                {
                    this.cashDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
                    this.shiftID = Int32.Parse(comboBoxShiftID.Text);

                    this.cashDesc = comboBoxInGlAcc.Text;
                    this.cashRem = textBoxInRem.Text;
                    this.cashUseBy = comboBoxInUseBy.Text;

                    openCashDrawer();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonOutSAVE_Click(object sender, EventArgs e)
        {
            try
            {

                this.cashAmount = float.Parse(txtBoxOutAmt.Text) * -1;

                if (this.cashAmount < 0)
                {

                    this.cashDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
                    this.shiftID = Int32.Parse(comboBoxShiftID.Text);
                   
                    this.cashDesc = comboBoxOutGlAcc.Text;
                    this.cashRem = textBoxOutRem.Text;
                    this.cashUseBy = comboBoxOutUseBy.Text;

                    openCashDrawer();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
 


        private void OnPrintCashDrawer(object sender, PrintPageEventArgs e)
        {
            try
            {

                int x = 0;
                int y = 0;

                Brush brush = new SolidBrush(Color.Black);
                Font fontHeader = new Font("Consolas", 12);
                Font fontTable = new Font("Consolas", 11);
                Font fontSubHeader = new Font("Consolas", 9);
                Font fontBody = new Font("Consolas", 8);


 


                Bitmap img = global::AppRest.Properties.Resources.Logo_New;

                y += 10;
 
                 

                e.Graphics.DrawString("[[ เปิดลิ้นชักเก็บเงิน ]]", fontTable, brush, x + 5, y);

                y += 30;

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd/MM/yyyy}", dt);
                string strTime = String.Format("{0:HH:mm:ss}", dt);

                e.Graphics.DrawString("Date " + strDate + "   Time  " + strTime, fontSubHeader, brush, x + 5, y);

                y += 15;

                e.Graphics.DrawString("รายการ : " + this.cashDesc , fontSubHeader, brush, x + 5, y);

                y += 15;

                e.Graphics.DrawString("ยอดเงิน : " + this.cashAmount.ToString("###,###.##") + " THB.", fontSubHeader, brush, x + 5, y);

                y += 15;

                e.Graphics.DrawString("เหตุผล : " + this.cashRem, fontSubHeader, brush, x + 5, y);

                y += 15;

                e.Graphics.DrawString("ผู้รับเงิน : " + this.cashUseBy  + "   Sign ................. ", fontSubHeader, brush, x + 5, y);

                y += 15;

                e.Graphics.DrawString("ทำรายการโดย : " + this.cashRem + "   Sign ................ ", fontSubHeader, brush, x + 5, y);

                y += 15;
                 

                e.Graphics.DrawString("--------------------------------------------------", fontSubHeader, brush, x, y);

                y += 25;

                e.Graphics.DrawString(" " , fontBody, brush, x, y);

                e.HasMorePages = false;

                //System.Threading.Thread.Sleep(500);

                defaultFunc();
                getDataTablePL();

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

        private void buttonInCLEAR_Click(object sender, EventArgs e)
        {
            txtBoxInAmt.Text = "0";
            comboBoxInUseBy.Text = "Select Member";
            comboBoxInGlAcc.Text = "รายละเอียดรายรับ";
            textBoxInRem.Text = "เหตุผล";

        }

        private void buttonOutClLEAR_Click(object sender, EventArgs e)
        {
            txtBoxOutAmt.Text = "0";
            comboBoxOutUseBy.Text = "Select Member";
            comboBoxOutGlAcc.Text = "รายละเอียดรายรับ";
            textBoxOutRem.Text = "เหตุผล";
        }


        private void getDataTablePL()
        {

            try
            {

                DataTable dtIncome = gd.getAllCashDrawerData(this.cashDate, this.shiftID, 1);
                dataGridViewIncome.DataSource = dtIncome;

                dataGridViewIncome.Columns[0].Visible = false;
                dataGridViewIncome.Columns[1].Visible = false;
                dataGridViewIncome.Columns[2].Visible = false;
                dataGridViewIncome.Columns[6].Visible = false;
                dataGridViewIncome.Columns[7].Visible = false;
                dataGridViewIncome.Columns[9].Visible = false;


                DataTable dtOutcome = gd.getAllCashDrawerData(this.cashDate, this.shiftID, -1);
                dataGridViewOutCome.DataSource = dtOutcome;


                dataGridViewOutCome.Columns[0].Visible = false;
                dataGridViewOutCome.Columns[1].Visible = false;
                dataGridViewOutCome.Columns[2].Visible = false;
                dataGridViewOutCome.Columns[6].Visible = false;
                dataGridViewOutCome.Columns[7].Visible = false;
                dataGridViewOutCome.Columns[9].Visible = false;

                summaryCashOut();
                 
                summaryCashReal();


            }
            catch (Exception ex)
            {

            }
 
        }

        private void comboBoxShiftID_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.cashDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
                this.shiftID = Int32.Parse(comboBoxShiftID.Text);
                getDataTablePL();
            }
            catch (Exception ex)
            {


            }
             
        }



        private void printEndBill(object sender, PrintPageEventArgs e)
        {
            int x = 0;
            int y = 0;

 

            Brush brush = new SolidBrush(Color.Black);
            Font fontHeader = new Font("Consolas", 12);
            Font fontTable = new Font("Consolas", 11);
            Font fontSubHeader = new Font("Consolas", 9);
            Font fontBody = new Font("Consolas", 8);
            Font fontNum = new Font("Consolas", 9);


            this.cashDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
            this.shiftID = Int32.Parse(comboBoxShiftID.Text);


            Bitmap img = global::AppRest.Properties.Resources.Logo_New;

            y += 10;

            if (Login.flagLogoSQ.ToLower() == "y")
            {

                e.Graphics.DrawImage(img, x + 110, y, 70, 70);
                y += 70;
            }
            else
            {
                e.Graphics.DrawImage(img, x + 70, y, 110, 55);
                y += 55;
            }



            y += 10;

            e.Graphics.DrawString("[[ ใบปิดยอด ]]", fontTable, brush, x + 40, y);

            y += 30;

            e.Graphics.DrawString(dateTimePickerStartDate.Value.ToString("dd MMM yyyy"), fontHeader, brush, x + 40, y);

            y += 25;
             

            DateTime dt = DateTime.Now;

            string strDate = String.Format("{0:dd/MM/yyyy}", dt);
            string strTime = String.Format("{0:HH:mm:ss}", dt);

            e.Graphics.DrawString("Print Date " + strDate + "   Time  " + strTime, fontSubHeader, brush, x + 15, y);

            y += 15;

            e.Graphics.DrawString("By : " + Login.userName  , fontSubHeader, brush, x + 15, y);

            y += 15;

            e.Graphics.DrawString("...............................................", fontSubHeader, brush, x + 5, y);
            y += 15;
            y += 15;

             
            List<SalesToday> dataEndBill = gd.getCashDrawerEndDay(1, this.cashDate, this.shiftID);

              

            string lb = "";
            float val = 0;
            int ii = 1;
             
            string txtAmt = "";
            string txtOrder = "";

            foreach (SalesToday st in dataEndBill)
            {

                lb = st.SalesLable;
                val = st.SalesAmount;

                txtOrder =  lb;
                txtAmt = (val).ToString("###,###.##") + "";
                txtAmt = String.Format("{0,10}", txtAmt);
                e.Graphics.DrawString(txtOrder, fontBody, brush, 0, y);
                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 190, y);


                ii++;
                y += 15;
            }

            y += 40;

            ii = 1;
            e.Graphics.DrawString("***************************************** ", fontBody, brush, x + 10, y);
            y += 20;

            y += 20;

            string DateDay = dateTimePickerStartDate.Value.ToString("dd/MM/yyyy");

            trnDay = gd.getTrnOrderByDay(DateDay);

            List<SalesToday> salesZone = gd.getSalesByday(4, DateDay);
            List<SalesToday> salesPaymentAll = gd.getSalesByday(5, DateDay);
            List<SalesToday> salesPayment = gd.getSalesByday(6, DateDay);
            List<SalesToday> salesDelBill = gd.getSalesByday(8, DateDay);
            List<SalesToday> salesCashCard = gd.getSalesByday(9, DateDay);
            List<SalesToday> salesDelOrder = gd.getSalesByday(10, DateDay);

            List<SalesToday> salesIN = gd.getSalesByday(200, DateDay);

            Branch branch = gd.getBranchDesc();

  
            y += 20;

            e.Graphics.DrawString("สรุปยอดขายทั้งหมด", fontSubHeader, brush, x + 80, y);

            y += 30;

  
 
                txtAmt = "";

                foreach (SalesToday st in salesIN)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = lb;
                    txtAmt = (val).ToString("###,###.##") + "";
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBody, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 190, y);


                    ii++;
                    y += 15;
                }



                y += 20; 

                //////////////////////////////////////////////////////////////////////

                e.Graphics.DrawString("**** การชำระเงินแยกประเภท (ทั้งหมด) **** ", fontBody, brush, x + 10, y);
                y += 20;

                lb = "";
                val = 0;
               ii = 1;

                foreach (SalesToday st in salesPaymentAll)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount; 

                    txtOrder = ii.ToString() + ". " + lb;
                    txtAmt = (val).ToString("###,###.##")  ;
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBody, brush, 0, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 190, y);

                  
                    ii++;
                    y += 15;
                }


                y += 20;

                ii = 1;
                e.Graphics.DrawString("**** ยอดเติมเงิน / คืนเงิน Cash Card **** ", fontBody, brush, x + 10, y);
                y += 15;

                float ut;

                foreach (SalesToday st in salesCashCard)
                {

                    lb = st.SalesLable;
                    ut = st.SalesUnit;
                    val = st.SalesAmount;

                    txtOrder = ii.ToString() + ". " + lb;
                    txtAmt = (val).ToString("###,###.##")  ;
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBody, brush, 0, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 190, y);

                    ii++;
                    y += 15;
                }


                 

                //////////////////////////////////////////////////////////////////////

                //// Payment

                y += 20;

                e.Graphics.DrawString("**** การชำระเงินแยก Zone / Cashier **** ", fontBody, brush, x + 10, y);
                y += 20;

                lb = "";
                val = 0;
                ii = 1;

                foreach (SalesToday st in salesPayment)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = ii.ToString() + ". " + lb;
                    txtAmt = (val).ToString("###,###.##") ;
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBody, brush, 0, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 190, y);

                    ii++;
                    y += 15; 
                }

                y += 20;

                // Zone

                e.Graphics.DrawString("****  Zone ****  ", fontBody, brush, x + 10, y);
                y += 20;

                ii = 1;
                foreach (SalesToday st in salesZone)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = ii.ToString() + ". " + lb;
                    txtAmt = (val).ToString("###,###.##")  ;
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBody, brush, 0, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 190, y);


                    ii++;
                    y += 15;
                }

                y += 20;

                ii = 1;
                e.Graphics.DrawString("**** รายการลบบิล (Void Bill) **** ", fontBody, brush, x + 10, y);
                y += 20;
                 

                foreach (SalesToday st in salesDelBill)
                {

                    lb = st.SalesLable;
                    val = st.SalesAmount;

                    txtOrder = ii.ToString() + ". " + lb;
                    txtAmt = (val).ToString("###,###.##") + "";
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBody, brush, 0, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 190, y);

                    ii++;
                    y += 15;
                }

                y += 20;


                //ii = 1;
                //e.Graphics.DrawString("**** รายการลบ Order (Void Orders) **** ", fontBody, brush, x + 10, y);
                //y += 20;


                //string rw1 = "";
                //string rw2 = "";

                //string[] rw;

                //foreach (SalesToday st in salesDelOrder)
                //{

                //    rw = st.SalesLable.Split('|');

                //    rw1 = rw[0];
                //    rw2 = rw[1];


                //    txtOrder = ii.ToString() + "." + rw1;
                //    txtAmt = "  " + rw2;
                //    e.Graphics.DrawString(txtOrder, fontBody, brush, x + 10, y);
                //    y += 12;
                //    e.Graphics.DrawString(txtAmt, fontBody, brush, x + 10, y);

                //    ii++;
                //    y += 15;
                //} 
                e.HasMorePages = false; 

        }

        private void printDocument3_PrintPage(object sender, PrintPageEventArgs e)
        {
            Brush brush = new SolidBrush(Color.Black);
            Font fontHeader = new Font("Consolas", 12);

            e.Graphics.DrawString(".", fontHeader, brush, 0, 0);
        }

        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.cashDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
                this.shiftID = Int32.Parse(comboBoxShiftID.Text);
                getDataTablePL();
            }
            catch (Exception ex)
            {


            }
        }

        private void summaryCashOut()
        {
            float cashOut = 0;

            try
            { 

                foreach (DataGridViewRow row in dataGridViewOutCome.Rows)
                {
                    if (row.Cells["CashAmount"].Value.ToString().Length > 0)
                         cashOut += float.Parse( row.Cells["CashAmount"].Value.ToString() );
                   
                } 

                textBoxTotalCashOut.Text = cashOut.ToString("###,##0");

            }
            catch (Exception ex)
            {
                textBoxTotalCashOut.Text = "0" ;
            }
        }


        private void summaryCashReal()
        {
            float cashReal = 0;

            try
            {

                List<SalesToday> dataEndBill = gd.getCashDrawerEndDay(1, this.cashDate, this.shiftID);


                foreach (SalesToday st in dataEndBill)
                {
                    if (st.SalesLable == "เงินสดต้องเหลือจริง") 
                          cashReal = st.SalesAmount;
                }
                 
                textBoxRealMoney.Text = cashReal.ToString("###,##0");

            }
            catch (Exception ex)
            {
                textBoxRealMoney.Text = "0";
            }
        }

        private void buttonViewCash_Click(object sender, EventArgs e)
        {


            try
            {
                panelSummaryCashDrawer.Visible = true;
                List<SalesToday> dataEndBill = gd.getCashDrawerEndDay(1, this.cashDate, this.shiftID);
                dataGridViewSummaryCash.DataSource = dataEndBill;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            panelSummaryCashDrawer.Visible = false;
        }

        private void buttonDelOpenShift_Click(object sender, EventArgs e)
        {
            try
            {
                Button bt = (Button)sender;

                if(  bt.Name == "buttonDelOpenShift")
                    this.cashRem = "OS";
                else
                    this.cashRem = "CS";

                this.cashAmount = 0; 
                this.cashDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
                this.shiftID = Int32.Parse(comboBoxShiftID.Text);
                this.cashDesc = "เงินทอนปิดร้าน";
               // this.cashRem = "CS";
                this.cashUseBy = Login.userName;

                int result = gd.delCashDrawerShift(this.cashDate, this.shiftID, this.cashDesc, this.cashAmount, this.cashRem, this.cashUseBy);

                if (result > 0)
                {
                    MessageBox.Show("ลบรายการสำเร็จ !!");
                    printCloseShip.Print();
                    getDataTablePL();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printCloseShip_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

                int x = 0;
                int y = 0;

                Brush brush = new SolidBrush(Color.Black);
                Font fontHeader = new Font("Consolas", 12);
                Font fontTable = new Font("Consolas", 11);
                Font fontSubHeader = new Font("Consolas", 9);
                Font fontBody = new Font("Consolas", 8);



                y += 10;

                if (this.cashRem == "OS")
                    e.Graphics.DrawString("[[ ลบเงินทอนเปิดร้าน (OS) ]]", fontTable, brush, x + 5, y);
                else
                    e.Graphics.DrawString("[[ ลบเงินทอนปิดร้าน (CS) ]]", fontTable, brush, x + 5, y);

                y += 30;

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd/MM/yyyy}", dt);
                string strTime = String.Format("{0:HH:mm:ss}", dt);

                e.Graphics.DrawString("Date " + strDate + "   Time  " + strTime, fontSubHeader, brush, x + 5, y);

                y += 15;

                e.Graphics.DrawString("ทำรายการโดย : " + this.cashUseBy, fontSubHeader, brush, x + 5, y);

                y += 30;

                e.Graphics.DrawString("Sign ............................. ", fontSubHeader, brush, x + 5, y);

                y += 15;

                e.Graphics.DrawString("--------------------------------------------------", fontSubHeader, brush, x, y);

                y += 25;

                e.Graphics.DrawString(" ", fontBody, brush, x, y);

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
    }
}
