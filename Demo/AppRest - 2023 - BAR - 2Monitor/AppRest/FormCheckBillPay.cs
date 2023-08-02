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
using System.Net;
using System.IO;

namespace AppRest
{
    public partial class FormCheckBillPay : Form
    {

        GetDataRest gd;

        public string amtFromCustomer;
        public string amtFromChange;
        public int paytype; 
        int keyTime;
        public float payAmount;
        public BillPayment billPaymentByType;

        string promtpayCode = "";


        public FormCheckBillPay() {

            paytype = 0;
        }

        public FormCheckBillPay(Form frmlkFrom, int flagFrmClose, float payOrder, string checkTableName,int displayType)
        {
            InitializeComponent();
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }

            gd = new GetDataRest();


            textBox_PayAmount.Enabled = false;
            textBox_Change.Enabled = false;
            textBox_PayAmount.Text = payOrder.ToString();    

            this.ControlBox = false;  

            textBox_FromCust.Focus();

            keyTime = 0;
            paytype = 0;
            labelTableName.Text = checkTableName; 

              

            panelCash.Visible = true;
            panelCreditCard.Visible = false;
            panelBanking.Visible = false;

            if (displayType == 2)
            { 
                buttonCreditCust.Visible = false;
                buttonCashCard.Visible = false;
            }

            this.Height = 600;
            this.Width = 1000;
            ///
            this.payAmount = payOrder;
            txtBoxCCBalanaceEnd.Text = "0"; 

            printQR.PrinterSettings.PrinterName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();

            promtpayCode = ConfigurationSettings.AppSettings["PromtpayCode"].ToString();

        }

        private void buttonCash_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.payAmount == 0)
                {
                    textBox_FromCust.Text = "0";
                    textBox_Change.Text = "0";

                    this.amtFromCustomer = "0";
                    this.amtFromChange = "0";

                    billPaymentByType = new BillPayment(0, 1, "Cash", this.payAmount, textBox_FromCust.Text, textBox_Change.Text, "");                   
                    paytype = 1;
                    this.Close();
                }
                else
                {

                    if (float.Parse(textBox_Change.Text) >= 0 && float.Parse(textBox_FromCust.Text) > 0)
                    {
                        paytype = 1; 
                        billPaymentByType = new BillPayment(0, 1, "Cash", this.payAmount, textBox_FromCust.Text, textBox_Change.Text, "");
                        this.Close();
                    }
                    else
                    {
                        throw new Exception(" เงินที่รับจากลูกค้าต้องมากกว่าราคาสินค้า ");
                    }
                }

            }
            catch (Exception ex)
            {

                textBox_FromCust.Text = "0";
                textBox_Change.Text = "0";
                MessageBox.Show(ex.Message);
            }
        }

        private void change_TextChange(object sender, EventArgs e)
        {
            string amtFromCust = "";

            try
            {
                if (textBox_FromCust.Text.Trim().Length == 0)
                {
                    amtFromCust = "0";
                }
                else
                {
                    amtFromCust = textBox_FromCust.Text;
                    amtFromCustomer = amtFromCust;
                }

                textBox_Change.Text = (float.Parse(amtFromCust) - float.Parse(textBox_PayAmount.Text)).ToString("###,###.#0");
                amtFromChange = textBox_Change.Text;
            }
            catch (Exception ex)
            {

                textBox_FromCust.Text = "0";
                textBox_Change.Text = "0";
               // MessageBox.Show(ex.Message);
            }
        }

        private void KeyPassFinish(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.buttonCash_Click(buttonCash, e);
            }
        }


        private void ButtonNoClick(object sender, EventArgs e)
        {
            Button bt = (Button)sender;

            string txt = "";

            keyTime++;

            float oldAmount = 0;

            try
            {
                defaultColButOrderNo();
                bt.BackColor = System.Drawing.Color.Orange;

                oldAmount = float.Parse(textBox_FromCust.Text.ToString());

                if (bt.Name.Substring(0, 5) == "bTBK_")
                {

                    txt = bt.Name.Replace("bTBK_", " ").Trim();
                    oldAmount += float.Parse(txt);

                    textBox_FromCust.Text = oldAmount.ToString();


                }
                else
                {

                    txt = bt.Name.Replace("button_", " ").Trim();

                    if (keyTime == 1)
                        textBox_FromCust.Text = txt;
                    else
                        textBox_FromCust.Text += txt;

                }

            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString());
            }

        }

        private void defaultColButOrderNo()
        {
            button_1.BackColor = System.Drawing.Color.FromArgb(128,255,128);
            button_2.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_3.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_4.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_5.BackColor = System.Drawing.Color.FromArgb(128,255,128);
            button_6.BackColor = System.Drawing.Color.FromArgb(128,255,128);
            button_7.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_8.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_9.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_0.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_00.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_000.BackColor = System.Drawing.Color.FromArgb(128,255,128);
            buttonEqualBill.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            textBox_FromCust.Text = "0";
            keyTime = 0;
        }

        private void buttonEqualBill_Click(object sender, EventArgs e)
        {
            buttonEqualBill.BackColor = System.Drawing.Color.Orange;
            textBox_FromCust.Text = textBox_PayAmount.Text;
        }



        private void buttonCreditCust_Click(object sender, EventArgs e)
        {
            paytype = 3;
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            paytype = 0;
            this.Close();
        }

        private void buttonCashCard_Click(object sender, EventArgs e)
        {

            panelCash.Visible = false;
            panelCreditCard.Visible = false;
            panelBanking.Visible = false;
            panelCashCard.Visible = true;

            txtBoxCCCode.Focus();
        }

  

        private void fromCust_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    if (Int32.Parse(textBox_Change.Text) >= 0 && Int32.Parse(textBox_FromCust.Text) > 0)
                    {
                        paytype = 1;
                        billPaymentByType = new BillPayment(0, 1, "Cash", this.payAmount, textBox_FromCust.Text, textBox_Change.Text, "");
                        this.Close();
                    }
                    else
                    {
                        throw new Exception(" เงินที่รับจากลูกค้าต้องมากกว่าราคาสินค้า ");
                    }

                }
                catch (Exception ex)
                {

                    textBox_FromCust.Text = "0";
                    textBox_Change.Text = "0";
                    MessageBox.Show(ex.Message);
                }
            } 
        }

        private void ButtonCD_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;

            string txt = ""; 

            try
            { 

                if (bt.Name.Substring(0, 9) == "buttonCD_")
                { 
                    txt = bt.Name.Replace("buttonCD_", " ").Trim();  
                }
                else
                { 
                    txt = "OTHERS";
                }

                if (comboBoxBankType.Text.Length > 0)
                    comboBoxCardType.Text = comboBoxBankType.Text + "-" + txt;
                else
                    comboBoxCardType.Text = txt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ButtonCard_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;

            string txt = "";

            try
            {

                if (bt.Name.Substring(0, 11) == "buttonCard_")
                {
                    txt = bt.Name.Replace("buttonCard_", " ").Trim();
                }
                else
                {
                    txt = "OTHERS";
                } 
                comboBoxBankName.Text = txt; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonShowCash_Click(object sender, EventArgs e)
        {
            panelCash.Visible = true;
            panelCreditCard.Visible = false;
            panelBanking.Visible = false;
            panelCashCard.Visible = false;
        }

        private void buttonCreditCard_Click(object sender, EventArgs e)
        { 
            panelCash.Visible = false;
            panelCreditCard.Visible = true;
            panelBanking.Visible = false;
            panelCashCard.Visible = false;
        }

        private void buttonBanking_Click(object sender, EventArgs e)
        { 
            panelCash.Visible = false;
            panelCreditCard.Visible = false;
            panelBanking.Visible = true;
            panelCashCard.Visible = false;

        } 

        private void buttonPayByBanking_Click(object sender, EventArgs e)
        {
            //Validate Data

            try
            {
                if (comboBoxBankName.Text == "Select Type")
                    throw new Exception("Please Select QR / Transfer Type");

                if (textBoxBankingDetail.Text == "เวลาโอน/รายละเอียด")
                    throw new Exception("Please Fill Detail");

                billPaymentByType = new BillPayment(0, 5, "QR", this.payAmount, comboBoxBankName.Text, textBoxBankingDetail.Text, textBoxBankingCustName.Text);
                     
                paytype = 5;
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonPayByCredit_Click(object sender, EventArgs e)
        {

            try
            {
                if (comboBoxCardType.Text == "Credit Card Type")
                    throw new Exception("Please Select Card Type");

                //if (textBoxCreditCardNo.Text == "xxxx-xxxx-xxxx-xxxx")
                //    throw new Exception("Please Fill Card No");

                billPaymentByType = new BillPayment(0, 2, "Credit Card", this.payAmount, comboBoxCardType.Text, textBoxCreditCardNo.Text, textBoxCreditCardCust.Text);            
                paytype = 2;
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
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
                searchMemCard();
                checkCashCard();
            }
        }


        List<CashCard> ccLists;

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

                            if (c.CCBalanceAmt <  (int)this.payAmount)
                            {
                                MessageBox.Show("ไม่สามารถชำระเงินได้ ยอดเงืนในบัตรไม่เพียงพอ !! \n\r" + " ยอดเงินคงในบัตรเท่ากับ " + txtBoxCCBalanceAmt.Text + " บาท");
                                txtBoxCCCode.Text = "";
                                txtBoxCCCode.Focus();

                                txtBoxCCBalanceAmt.Text = "";
                
                            }
                            else
                            {
                                //MessageBox.Show("ยอดเงินคงในบัตรเท่ากับ " + txtBoxCCBalanceAmt.Text + " บาท");
                                txtBoxCCBalanaceEnd.Text = (c.CCBalanceAmt - (int)this.payAmount).ToString();
                            } 
                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่มีข้อมูลบัตร กรุณาไปเค้าเตอร์เติมเงิน");
                        txtBoxCCCode.Text = "";
                        txtBoxCCCode.Focus();
                    }  
                }

            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }

        }

        private void buttonCashCardPayment_Click(object sender, EventArgs e)
        {

            try
            {
                 

                billPaymentByType = new BillPayment(0, 4, "Cash Card", this.payAmount, txtBoxCCCode.Text, txtBoxCCBalanceAmt.Text, txtBoxCCBalanaceEnd.Text);                        
                paytype = 4;
                this.Close(); 
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        MemCard mc;

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
                    MessageBox.Show("ตัดเงินจากบัตรสมาชิก : " + mc.MemCardID + "\n\r" + "ชื่อสมาชิก : " + mc.MemCardName);

                    txtBoxCCCode.Text = mc.MemCardID;
                }


            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void buttonCard_PROMPAY_Click(object sender, EventArgs e)
        {
            comboBoxBankName.Text = "PROMTPAY";


            try
            {
                textBoxBankingDetail.Text = DateTime.Now.ToString("HH:MM");

                if (pictureBoxQR.Visible == false)
                {
                    pictureBoxQR.Visible = true;

                    string amount = payAmount.ToString();
                    string url = "https://promptpay.io/" + promtpayCode + "/" + amount + ".png";

                    Image qr =  getImageFromURL(url); // global::AppRest.Properties.Resources.QR;

                    pictureBoxQR.Image = qr; 

                    ScreenService.SecondMonitor.pictureBox1.Image = qr; 
                   
                }
                else
                {
                    pictureBoxQR.Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonCard_PRINT_Click(object sender, EventArgs e)
        {
            comboBoxBankName.Text = "PROMPAY";
            textBoxBankingDetail.Text = DateTime.Now.ToString("HH:MM");
            printQR.Print();

        }

        private void printQR_PrintPage(object sender, PrintPageEventArgs e)
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

                e.Graphics.DrawString(" PROMPAY : " + promtpayCode, fontSubHeader, brush, x + 50, y);

                y += 15;

                e.Graphics.DrawString("ยอด : " + textBox_PayAmount.Text , fontTable, brush, x + 50, y);

                y += 25;

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd/MM/yyyy}", dt);
                string strTime = String.Format("{0:HH:mm:ss}", dt);

                e.Graphics.DrawString("Date " + strDate + " : " + strTime, fontSubHeader, brush, x + 40, y);

                y += 15;

                string amount = payAmount.ToString();
                string url = "https://promptpay.io/" + promtpayCode + "/" + amount + ".png";
              
              //  MessageBox.Show(url);
                Image qr = getImageFromURL(url);

                e.Graphics.DrawImage(qr, x + 60, y, 150, 150);
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


    }
}
