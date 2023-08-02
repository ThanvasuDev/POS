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
    public partial class MainCashCard : MainTemplate
    {
         
        GetDataRest gd;
        List<CashCard> ccLists;

        int ccTrnType;
        int lastPaytype;
        string lastPaytypeName;
        string flagExpire;
        string expireDate;
        string ccStatus;

        string ccCode;
        int cashBalance;
        int cashBalancetxt;
        int ccAmt;

        // Report Cash Card

        DataTable resultTable; 

        TrnMax trnMax;

        string pathExportResultStockName;
        string pathExportResultName;

        MemCard  mc;

        public MainCashCard(Form frmlkFrom, int flagFrmClose)
        {
            InitializeComponent();
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose(); 
 
            }
            else
            {
                buttonClose.Visible = true;
                panelHeaderFromOrder.Visible = true;
            }


            

            this.Text = this.Text + " ( By : " + Login.userName + ")";

            buttonLinkCashCard.BackColor = System.Drawing.Color.Gray;
            InitializeComponent();


            if (flagFrmClose == 1)
            {

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

            }





            defaultApp();

            gd = new GetDataRest();

            ccLists = new List<CashCard>();

            printCashCardBalance.PrinterSettings.PrinterName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();

            defaultColButOrderNo();

            // Report



            pathExportResultStockName = ConfigurationSettings.AppSettings["PathExportResultStockName"].ToString();
            pathExportResultName = ConfigurationSettings.AppSettings["PathExportResultName"].ToString();

            resultTable = new DataTable();

            dateTimePickerStartDate.Value = DateTime.Now;
            dateTimePickerEnd.Value = DateTime.Now;

            getComboTitle();
            getResultDatatable();

            mc = new MemCard();

        }

        private void getResultDatatable()
        {
            try
            {
                int dataSelectID = 0;

                string fromDate = "";
                string toDate = ""; 

                fromDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
                toDate = dateTimePickerEnd.Value.ToString("yyyyMMdd");

                dataSelectID = comboBoxTitle.SelectedIndex; 
                 

                if (dataSelectID == 0)
                    this.resultTable = gd.getCC_Transaction(fromDate, toDate,Login.userID , Login.userName , 0 , 0 ,"" , "" );
                else if (dataSelectID == 1)
                    this.resultTable = gd.getCC_SumTranBy_POSCouter(fromDate, toDate, Login.userID, Login.userName, 0, 0, "", "");
                else if (dataSelectID == 2)
                    this.resultTable = gd.getCC_AllCashCard(fromDate, toDate, Login.userID, Login.userName, 0, 0, "", ""); 

                dataGridViewResult.DataSource = this.resultTable;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAddRestDesc_Click(object sender, EventArgs e)
        {
            getResultDatatable();
        }

        private void SelectChange(object sender, EventArgs e)
        {
            getResultDatatable();
        }

 

        private void buttonExport_Click(object sender, EventArgs e)
        {
            // string fileName = "D:\\result.xls";

           // ExportData.Excel_FromDataTable(this.resultTable, this.pathExportResultName);

            try
            {
                ExportData.Excel_FromDataTable_DT(dataGridViewResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }
         

        private void defaultApp()
        {
            ccTrnType = 1;
            txtBoxCCCode.Text = "";
            txtBoxCCBalanceAmt.Text = "0";
            textBoxCCDeposit.Text = "0";
            textBoxAmt.Text = "0";
            textBoxCustPay.Text = "0";
            textBoxChange.Text = "0";

            defaultColButOrderNo();
            txtBoxCCCode.Focus();
            radioBoxCash.Checked = true;

            textBoxRemark.Text = "";
            cashBalance = 0;
            cashBalancetxt = 0;
            ccAmt = 0;


        }


        private void getComboTitle()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List 

            data.Add(new KeyValuePair<int, string>(1, "ข้อมูลการเติมเงิน / คืนเงิน / ใช้บัตร"));
            data.Add(new KeyValuePair<int, string>(2, "รายงานสรุปยอดรายรับ ตามจุดขายบัตรและร้านค้า"));
            data.Add(new KeyValuePair<int, string>(3, "รายการข้อมูล Balance คงเหลือของบัตร")); 

            // Clear the combobox
            comboBoxTitle.DataSource = null;
            comboBoxTitle.Items.Clear();

            // Bind the combobox
            comboBoxTitle.DataSource = new BindingSource(data, null);
            comboBoxTitle.DisplayMember = "Value";
            comboBoxTitle.ValueMember = "Key";

            comboBoxTitle.SelectedIndex = 0;


        }
         

        private void ButtonScan_Click(object sender, EventArgs e)
        {
            txtBoxCCCode.Text = "";
            txtBoxCCCode.Focus();
        }

        private void txtBoxCCCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {

                    this.ccCode = txtBoxCCCode.Text;

                    if (ccCode.Length > 1)
                    {

                        ccLists = gd.getCashCard(ccCode);

                        searchMemCard();

                        if (ccLists.Count > 0)
                        {
                            foreach (CashCard c in ccLists)
                            {
                                txtBoxCCBalanceAmt.Text = c.CCBalanceAmt.ToString();
                                textBoxCCDeposit.Text = c.CCDeposit.ToString();

                                this.cashBalancetxt = c.CCBalanceAmt;
                                this.lastPaytype = c.CCLastPayType;
                                this.lastPaytypeName = c.CCLastPayTypeName;
                                this.flagExpire = c.FlagExpire;
                                this.expireDate = c.CCExpireDate;
                                this.ccStatus = c.CCStatusDesc;
                            }

                            string flagExpiretxt = "ใช้งานได้";

                            if (this.flagExpire == "Y")
                                flagExpiretxt = "บัตรหมดอายุ";

                            textBoxRemark.Text = "เติมเงินล่าสุดด้วย : " + this.lastPaytypeName + "\r\n"
                                                + "วันที่บัตรหมดอายุ : " + this.expireDate + "\r\n"
                                                + "สถานะอายุบัตร : " + flagExpiretxt + "\r\n"
                                //    + "สถานะบัตร : " + this.ccStatus  + "\r\n"
                                                + "เติมเงินเข้าบัตรสมาชิก : " + mc.MemCardID + "\r\n"
                                                + "ชื่อลูกค้า : " + mc.MemCardName;

                            searchMemCard();
                          
                        }
                        else
                        {
                            MessageBox.Show("บัตรใหม่");
                            textBoxRemark.Text = "บัตรใหม่";
                        }

                           textBoxAmt.Focus();

                           textBoxChange.Text = txtBoxCCBalanceAmt.Text; 
                           
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



        private void searchMemCard()
        {
            string strSearchMemCard = txtBoxCCCode.Text;
             
           // string flagSynTax = ""; 
            mc = gd.SelMemCard_Search(strSearchMemCard, "Y");

            try
            {

                if (mc.MemCardID.Length == 0)
                {
                   // throw new Exception("ไม่พบเลขสมาชิก / เบอร์โทรนี้ / ยังไม่ได้สมัครสมาชิก");
                }
                else
                {
                    MessageBox.Show("เติมเงินเข้าบัตรสมาชิก : " + mc.MemCardID + "\n\r" + "ชื่อลูกค้า : " + mc.MemCardName);

                    txtBoxCCCode.Text = mc.MemCardID; 
                }
                 

            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }

        private void textBoxAmt_TextChanged(object sender, EventArgs e)
        {
            textBoxCustPay.Text = textBoxAmt.Text;

            // คืนบัตร
            if (textBoxAmt.Text != "0")
            {
                textBoxChange.Text = "0";
            }  
        }
         
        private void textBoxAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                updateTranCashcard();
            }
        }


        private void updateTranCashcard()
        {
            try
            {

                if (txtBoxCCCode.Text.Length > 1)
                {

                    this.ccCode = txtBoxCCCode.Text;
                    int ccdeposit = Int32.Parse(textBoxCCDeposit.Text);
                    ccAmt = Int32.Parse(textBoxAmt.Text);
                    

                    int ccPayType = 1;

                    if( radioBoxCreditCard.Checked == true )
                        ccPayType = 2;



                    if (textBoxAmt.Text == "0")
                    {
                        this.ccTrnType = 2;
                        MessageBox.Show("คืนเงินในบัตร" + txtBoxCCBalanceAmt.Text + " บาท");

                        ccAmt = Int32.Parse(txtBoxCCBalanceAmt.Text);

                        if( ccPayType == 2 )
                             throw new Exception("ไม่สามารถคืนเงินเป็นบัตรเครดิตได้");

                        if( this.flagExpire == "Y")
                            throw new Exception("ไม่สามารถคืนเงินได้บัตรหมดอายุวันที่ : " + this.expireDate);

                        if( this.lastPaytype == 2 )
                            throw new Exception("ไม่สามารถคืนได้ !! เนื่องจากเติมเงินด้วยบัตรเคดิต");
                    }


                    int result = gd.updsCashCard(ccCode, 1, this.ccTrnType, 1, ccdeposit, ccAmt, 0, ccPayType, "00");


                    //   MessageBox.Show(result.ToString());

                    if (result != -999999)
                    {
                        MessageBox.Show("ยอดเงินคงเหลือในบัตรเท่ากับ : " + result.ToString() + " บาท");

                        printCashCardBalance.Print();

                    }
                    else
                    {
                        throw new Exception("ไม่สามารถทำรายการได้");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                defaultApp();
            }

        }



        private void ButtonNum_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            string numstr = "";

            try
            {
                defaultColButOrderNo();
                bt.BackColor = System.Drawing.Color.Orange;

                numstr = Int32.Parse(bt.Name.Replace("buttonBC_", " ").Trim()).ToString();


                    if (numstr == "88")
                    {

                        if (textBoxAmt.Text != "0")
                        {
                            textBoxAmt.Text = textBoxAmt.Text.Substring(0, textBoxAmt.Text.Length - 1);
                        }

                        if (textBoxAmt.Text.Length == 0)
                            textBoxAmt.Text = "0";

                    }
                    else if (numstr == "13")
                    {
                        updateTranCashcard();
                    }
                    else
                    {

                        if (textBoxAmt.Text == "0")
                            textBoxAmt.Text = ""; 

                        textBoxAmt.Text += numstr;
                    }
                     
                

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void defaultColButOrderNo()
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
            buttonBC_88.BackColor = System.Drawing.Color.White;
            buttonBC_13.BackColor = System.Drawing.Color.White;
        }

        private void buttonTopup_Click(object sender, EventArgs e)
        {
            updateTranCashcard();
        }

        private void buttonRefund_Click(object sender, EventArgs e)
        {
            updateTranCashcard();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            radioBoxCash.Checked = true;
            textBoxAmt.Focus();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            radioBoxCreditCard.Checked = true;
            textBoxAmt.Focus();
        }

        private void printCashCardBalance_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

                int x = 0;
                int y = 0;

                // Information

                ///////////////////////////


                Brush brush = new SolidBrush(Color.Black);
                Font fontHeader = new Font("Tahoma", 16);
                Font fontTable = new Font("Tahoma", 13);
                Font fontSubHeader = new Font("Tahoma", 9);
                Font fontBody = new Font("Tahoma", 9);
                Font fontBodylist = new Font("Tahoma", 9);


                Bitmap img = global::AppRest.Properties.Resources.Logo_New;



                ccLists = gd.getCashCard(this.ccCode);


                if (ccLists.Count > 0)
                {
                    foreach (CashCard c in ccLists)
                    {
                        this.cashBalance = c.CCBalanceAmt; 
                        this.lastPaytype = c.CCLastPayType;
                        this.lastPaytypeName = c.CCLastPayTypeName;
                        this.flagExpire = c.FlagExpire;
                        this.expireDate = c.CCExpireDate;
                        this.ccStatus = c.CCStatusDesc;
                    }

                    string flagExpiretxt = "ใช้งานได้";

                    if (this.flagExpire == "Y")
                        flagExpiretxt = "บัตรหมดอายุ";  
                } 

                 


                e.Graphics.DrawImage(img, x + 40, y, 220, 100);

                y += 100;


                e.Graphics.DrawString(" Cash Card Balance", fontTable, brush, x + 60, y);

                y += 20;

                e.Graphics.DrawString(Login.userID.ToString() + " " + Login.userName, fontBodylist, brush, x + 5, y);

                y += 10;

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd MMM yy}", dt);
                string strTime = String.Format("{0:HH:mm}", dt);

                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 12;
                e.Graphics.DrawString(" " + strDate + " " + strTime, fontSubHeader, brush, x + 80, y);
                y += 10;
                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 15;

                if( this.cashBalancetxt == 0 )
                    e.Graphics.DrawString(" *** เงินในบัตรตั้งต้น : " + "0" + " บาท.", fontBody, brush, x + 20, y); 
                else
                   e.Graphics.DrawString(" *** เงินในบัตรตั้งต้น : " + this.cashBalancetxt.ToString("###,###") + " บาท.", fontBody, brush, x + 20, y);

                y += 15;

                if (this.ccTrnType == 1)
                {
                    e.Graphics.DrawString(" **** เติมเงินในบัตร : " + this.ccAmt.ToString("###,###") + " บาท.", fontBody, brush, x + 20, y);

                }
                else
                {
                    e.Graphics.DrawString(" **** คืนเงินในบัตร : " + this.ccAmt.ToString("###,###") + " บาท.", fontBody, brush, x + 20, y);

                } 
                 

                y += 15;

                e.Graphics.DrawString(" **** รหัสบัตร : " + txtBoxCCCode.Text, fontBody, brush, x + 20, y);

                y += 15;

                e.Graphics.DrawString(" **** ชื่อลูกค้า : " + mc.MemCardName, fontBody, brush, x + 20, y);

                y += 15;

                e.Graphics.DrawString(" **** วันหมดอายุ : " + this.expireDate , fontBody, brush, x + 20, y);


                y += 15;

                e.Graphics.DrawString(" **** สถานะบัตร: " + this.ccStatus, fontBody, brush, x + 20, y);

                y += 25;

                e.Graphics.DrawString(" ** เงินในบัตรล่าสุด : " + this.cashBalance, fontTable, brush, x + 20, y);

            

                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        
        }

        private void comboBoxTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            getResultDatatable();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

  

       




        

        

   
      

     
      

 
       

     
  

 

 
 
       

      

 

       

     
      
      
    }
}
