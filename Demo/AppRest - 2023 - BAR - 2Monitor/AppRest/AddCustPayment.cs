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
    public partial class AddCustPayment : AddDataTemplate
    {
        GetDataRest gd;

        MainManage formMainManage;
        List<CustPayment> cp;

        DataTable cpAll;
        DataTable cpByCust;
        DataTable cpPayDate;

        Payment payment;

        int trnBillIDSect;
        List<Transaction> trn;

        string printerCashName;
        string flagprintcash;
        string custName;
        int custID;

        double servicePercent;
        double taxPercent;

       

        public AddCustPayment(Form frmlkFrom, int flagFrmClose, int custSelect)
        {
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();


            this.Width = 1024;
            this.Height = 764;

            gd = new GetDataRest();
            cpAll = gd.getCustPaymentDataTable();
            cp = gd.getCustPayment();

          



            printerCashName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();


            flagprintcash = ConfigurationSettings.AppSettings["FlagPrintCash"].ToString();


            printBill.PrinterSettings.PrinterName = printerCashName;
            printCustPay.PrinterSettings.PrinterName = printerCashName;

            getComboAllCreditCust();
            genCustPay();

            if (custSelect > 0)
                comboBoxCreditCust.SelectedValue = custSelect;

            custName = "";
            custID = 0;


            taxPercent = Double.Parse(ConfigurationSettings.AppSettings["TaxPercent"].ToString());
            servicePercent = Double.Parse(ConfigurationSettings.AppSettings["ServiceChargePercent"].ToString());

            panelSearchCustomer.Visible = false;
        }

        private void genCustPay()
        {
            try
            {

                string custID = comboBoxCreditCust.SelectedValue.ToString();


                foreach (CustPayment c in this.cp)
                {
                    if (custID == c.CustID.ToString())
                    {
                        textBoxCreditCustAmt.Text = c.CreditAmount;
                        textBoxCreditCustPay.Text = c.PayAmount;
                        textBoxCreditCustBalance.Text = c.BalanceAmount;
                    }

                }

                this.cpByCust = gd.getCustPayByCust(Int32.Parse(custID));

                dataGridViewPaymentCust.DataSource = this.cpByCust;

                //dataGridViewPaymentCust.Columns[0].Visible = false;
                //dataGridViewPaymentCust.Columns[3].Visible = false;
                //dataGridViewPaymentCust.Columns[6].Visible = false;
                dataGridViewPaymentCust.Columns[1].Visible = false;
                dataGridViewPaymentCust.Columns[4].Visible = false;
                dataGridViewPaymentCust.Columns[6].Visible = false;
                dataGridViewPaymentCust.Columns[7].Visible = false;

                dataGridViewPaymentCust.Columns[8].HeaderText = "บิลเวลา";
                dataGridViewPaymentCust.Columns[9].HeaderText = "ชำระแล้ว";
                dataGridViewPaymentCust.Columns[10].HeaderText = "ชำระเงิน";
                dataGridViewPaymentCust.Columns[11].HeaderText = "ยอดค้าง";


                this.cpPayDate = gd.getCustPaymentDate(Int32.Parse(custID));

                dataGridViewCustPay.DataSource = this.cpPayDate;


                dataGridViewCustPay.Columns[4].Visible = false;
                dataGridViewCustPay.Columns[5].Visible = false;
                dataGridViewCustPay.Columns[6].Visible = false;
                dataGridViewCustPay.Columns[7].Visible = false;
                dataGridViewCustPay.Columns[8].Visible = false;


                getComboAllBillByCust();
                getBillselect();


                // genDataPayment();

            }
            catch (Exception ex)
            {

            }

        }


        private void getComboAllCreditCust()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List


            foreach (CustPayment c in this.cp)
            {
                //if (Int32.Parse(c.BalanceAmount) != 0)
                //{ // ลูกค้าที่มีหนี้ค้างชำระ
                //    data.Add(new KeyValuePair<int, string>(c.CustID, c.CustName));
                //}

                data.Add(new KeyValuePair<int, string>(c.CustID, c.CustName + " (" + c.BalanceAmount + ")"));
            }


            // Clear the combobox
            comboBoxCreditCust.DataSource = null;
            comboBoxCreditCust.Items.Clear();

            // Bind the combobox
            comboBoxCreditCust.DataSource = new BindingSource(data, null);
            comboBoxCreditCust.DisplayMember = "Value";
            comboBoxCreditCust.ValueMember = "Key";

            dataGridViewAllMember.DataSource = this.cpAll;

        }


        private void getComboAllBillByCust()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            int billNo = 0;
            string billName = "";

            foreach (DataRow row in this.cpByCust.Rows) // Loop over the rows.
            {
                billName = row["BillNo"].ToString();
                billNo = Int32.Parse(billName.Replace('#', ' '));

                data.Add(new KeyValuePair<int, string>(billNo, billName));
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

        private void getBillselect()
        {
            try
            {
                trnBillIDSect = Int32.Parse(comboBoxBillNo.SelectedValue.ToString());

                payment = gd.getTrnPayment(trnBillIDSect);

                textBoxBillID.Text = "#" + payment.TrnID.ToString();
                textBoxTotalAmt.Text = payment.PayAmount.ToString("###,###");
                textBoxtableName.Text = payment.TableName;
                textBoxOrderTime.Text = payment.OrderDateTime;
                textBoxPayTime.Text = payment.PayDateTime;
                textBoxPayType.Text = payment.PayTypeName;
                textBoxCreditCustRemark.Text = payment.PayRemark.ToString();


                trn = gd.getTrnOrder(trnBillIDSect);
                dataGridViewOrder.DataSource = trn;

                dataGridViewOrder.Columns[0].Visible = false;
                dataGridViewOrder.Columns[1].Visible = false;
                dataGridViewOrder.Columns[4].Visible = false;

                payment = gd.getTrnPayment(trnBillIDSect);

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }




        private void SelectBill_Change(object sender, EventArgs e)
        {
            getBillselect();
        }

        private void SelectCust_Change(object sender, EventArgs e)
        {
            genCustPay();
        }

        List<CustPaymentByPay> cpByPayID;

        private void buttonPayment_Click(object sender, EventArgs e)
        {
            custID = Int32.Parse(comboBoxCreditCust.SelectedValue.ToString());
            custName = comboBoxCreditCust.Text;

            try
            {

                if (float.Parse(textBoxPayAmount.Text) > float.Parse(textBoxTotalPayAmount.Text))
                    throw new Exception("กรณุาป้อนยอดเงินไม่เกินยอดบิลที่เลือก");

                LinkFormCheckBill();


                string remark = Login.posBranchID.ToString();
                float paymentCash;

                paymentCreditCardType = "";
                paymentCreditCardNo = "";
                paymentCreditCardCust = "";

                string payBillID = textBoxPayBillID.Text;

                if (formCheckBill.paytype > 0)
                {

                    paymentCash = this.totalSalesAmount;

                    if (formCheckBill.paytype == 1)
                    { 
                    }
                    else if (formCheckBill.paytype == 2)
                    { 

                        paymentCreditCardType = formCheckBill.comboBoxCardType.Text;
                        paymentCreditCardNo = formCheckBill.textBoxCreditCardNo.Text;
                        paymentCreditCardCust = formCheckBill.textBoxCreditCardCust.Text.ToUpper();

                    }
                    else if (formCheckBill.paytype == 5)
                    { 

                        paymentCreditCardType = formCheckBill.comboBoxBankName.Text;
                        paymentCreditCardNo = formCheckBill.textBoxBankingDetail.Text;
                        paymentCreditCardCust = formCheckBill.textBoxBankingCustName.Text.ToUpper();

                    } 

                    int result = gd.instCustPayment(custID, (int)paymentCash, formCheckBill.paytype, remark, paymentCreditCardType, paymentCreditCardNo, paymentCreditCardCust, "", payBillID);

                    if (result <= 0)
                    {
                        MessageBox.Show("กรุณาป้อนยอดเงินไม่เกินหนี้สิน");

                    }
                    else
                    {
                       // MessageBox.Show("ชำระเงินคูณ" + custName + " เป็นจำนวน : " + textBoxPayAmount.Text + " สำเร็จ");

                        this.cp = gd.getCustPayment();
                        this.cpAll = gd.getCustPaymentDataTable();
                        this.cpByPayID = gd.getCustPaymentByPay(result);

                        getComboAllCreditCust();
                        genCustPay();

                        comboBoxCreditCust.SelectedValue = custID;


                        if (flagprintcash == "Y")
                            printCustPay.Print();

                        textBoxPayAmount.Text = "";

                        textBoxPayBillID.Text = "#";
                        textBoxTotalPayAmount.Text = "0";

                    }


                }
                else
                {
                    throw new Exception("ยกเลิกชำระเงิน"); 
                }

                 


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        FormCheckBillPay formCheckBill;
        string  paymentCreditCardType = "";
        string  paymentCreditCardNo = "";
        string  paymentCreditCardCust = "";

        float totalSalesAmount;
        private void LinkFormCheckBill()
        {
            Cursor.Current = Cursors.WaitCursor;
 
            totalSalesAmount = Int32.Parse(textBoxPayAmount.Text);

            formCheckBill = new FormCheckBillPay(this, 0, (int)totalSalesAmount, "ค้างชำระ", 2);

            Cursor.Current = Cursors.Default;
            if (formCheckBill.ShowDialog() == DialogResult.OK)
            {
                formCheckBill.Dispose();
                formCheckBill = null;
            }
        }


















        private void buttonPrintBill_Click(object sender, EventArgs e)
        {
            printBill.Print();
        }



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

                Branch branch = gd.getBranchDesc();

                restName = branch.RestNameTH;
                restAddr1 = branch.RestAddr1TH;
                restAddr2 = branch.RestAddr2TH;
                restTel = "โทร. : " + branch.RestTel;


                restLine1 = branch.RestLine1;
                restLine2 = branch.RestLine2;
                restTaxID = branch.RestTaxID;

                ///////////////////////////

                Brush brush = new SolidBrush(Color.Black);
                Font fontHeader = new Font("Tahoma", 16);
                Font fontTable = new Font("Tahoma", 13);
                Font fontSubHeader = new Font("Tahoma", 9);
                Font fontBody = new Font("Tahoma", 9);
                Font fontBodylist = new Font("Tahoma", 9);
                Font fontBodys = new Font("Tahoma", 8);


                Bitmap img = global::AppRest.Properties.Resources.Logo_New;

                if (Login.flagLogoSQ.ToLower() == "y")
                {

                    e.Graphics.DrawImage(img, x + 85, y, 140, 140);
                    y += 140;
                }
                else
                {
                    e.Graphics.DrawImage(img, x + 40, y, 220, 110);
                    y += 110;
                }


                e.Graphics.DrawString(restAddr1, fontBody, brush, x + 20, y);

                y += 15;

                e.Graphics.DrawString(restAddr2, fontBody, brush, x + 20, y);

                y += 15;


                if (restTaxID.Trim().Length > 0)
                {
                    e.Graphics.DrawString(restTaxID, fontBody, brush, x + 20, y);

                    y += 15;
                }

                if (restLine1.Trim().Length > 0)
                {
                    e.Graphics.DrawString(restLine1, fontBody, brush, x + 20, y);

                    y += 15;
                }

                if (restLine2.Trim().Length > 0)
                {
                    e.Graphics.DrawString(restLine2, fontBody, brush, x + 20, y);

                    y += 15;
                }

                e.Graphics.DrawString(restTel, fontBody, brush, x + 30, y);

                y += 15;

                e.Graphics.DrawString(Login.userID.ToString() + " " + Login.userName, fontBodylist, brush, x + 5, y);

                y += 10;

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd MMM yy}", dt);
                string strTime = String.Format("{0:HH:mm}", dt);

                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 12;
                e.Graphics.DrawString(this.textBoxtableName.Text, fontSubHeader, brush, x + 10, y);
                //   e.Graphics.DrawString("Gst "  , fontSubHeader, brush, x + 240, y);
                y += 15;
                e.Graphics.DrawString(" " + strDate + " " + strTime, fontSubHeader, brush, x + 80, y);
                y += 10;
                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
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
                int len = 40;

                foreach (Transaction o in trn)
                {

                    if (o.GroupCatID > 0)
                    {

                        str1 = o.ProductName.Trim(); ;

                        str2 = o.SalesQTY.ToString();
                        str3 = (o.SalesAmount / o.SalesQTY).ToString("###,###");
                        str4 = o.SalesAmount.ToString("###,###");


                        if (o.SalesQTY > 1)
                            str1 += "(" + str3 + ")";

                        trnProductRemark = o.TrnRemark;


                        e.Graphics.DrawString(str2, fontBodys, brush, x + 0, y);
                        //  e.Graphics.DrawString(str3, fontBodylist, brush, x + 205, y);
                        e.Graphics.DrawString(str4, fontBodys, brush, x + 230, y);

                        txtPrint = FuncString.WordWrap(str1, len);
                        str1 = "";

                        foreach (string op in txtPrint)
                        {
                            e.Graphics.DrawString(op, fontBodys, brush, x + 22, y);
                            y += 13;
                        }

                        i++;
                        //  y += 15;

                        if (trnProductRemark.Trim().Length > 1)
                        {
                            string[] remarkString = trnProductRemark.Remove(0, 1).Split('+');

                            foreach (string r in remarkString)
                            {
                                str1 = "  +" + r + "\r\n";

                                e.Graphics.DrawString(str1, fontBodys, brush, x + 22, y);
                                y += 13;
                            }
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


                //if (this.flagFD == 0)
                //{

                //    //y += 15;
                //    //txtOrder = "FOOD";
                //    //txtAmt = salesAmountFood.ToString("###,###");
                //    //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                //    //e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 240, y);

                //    //y += 15;
                //    //txtOrder = "BEVERAGE/OTHERs";
                //    //txtAmt = salesAmountDrinks.ToString("###,###");
                //    //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                //    //e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 240, y);

                //}
                //else
                //{

                //    e.Graphics.DrawString("อาหารและเครื่องดื่ม", fontBodylist, brush, x + 22, y);
                //    e.Graphics.DrawString("1", fontBodylist, brush, x + 0, y);
                //    e.Graphics.DrawString((salesAmountFood + salesAmountDrinks).ToString("###,###"), fontBodylist, brush, x + 240, y);

                //    y += 15;
                //}



                //////////////////////////////////////////////////////////////////////////
                //    string txtOrder = "";

                txtOrder = "";
                //   string txtAmt = "";

                //y += 15;
                //txtOrder = "Amount";
                //txtAmt = (salesAmountFood + salesAmountDrinks).ToString("###,###");
                //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                //e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 240, y);

                //y += 15;
                //txtOrder = "DRINK / OTHERS";
                //txtAmt = salesAmountDrinks.ToString("###,###");
                //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                //e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 240, y);

                if (discount > 0)
                {
                    y += 15;

                    txtOrder = "Discount (Total)";
                    txtAmt = discount.ToString("###,###.##");
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 230, y);
                }


                if (serviceCharge > 0)
                {
                    y += 15;
                    txtOrder = "Service " + ((float)(this.servicePercent * 100)).ToString() + "%";
                    txtAmt = serviceCharge.ToString("###,###.##");
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 230, y);

                }

                if (tax > 0)
                {

                    y += 15;
                    txtOrder = "Amount Before Vat ";
                    txtAmt = (payment.PayAmount - tax).ToString("###,###.##");
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 230, y);

                    y += 15;
                    txtOrder = "Vat " + ((float)(this.taxPercent * 100)).ToString() + "%";
                    txtAmt = tax.ToString("###,###.##");
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 230, y);

                }


                y += 25;
                txtOrder = "Total Due";
                txtAmt = payment.PayAmount.ToString("###,###.##");
                e.Graphics.DrawString(txtOrder, fontTable, brush, x + 20, y);
                e.Graphics.DrawString(txtAmt, fontTable, brush, x + 185, y);

                y += 20;



                //////////////////////////////////////////////////////////////////////


                y += 20;

                string paymentCreditCardType = "";
                string paymentCreditCardNo = "";
                string paymentCreditCardCust = "";
                string custName = "";
                string remark = "";
                int custID = 0;

                string billNo = "#" + payment.TrnID;

                paymentCreditCardType = payment.PayCreditType;
                paymentCreditCardNo = payment.PayCreditNo;
                paymentCreditCardCust = payment.PayCreditName;

                custID = payment.PayCustID;
                custName = payment.PayCustName;
                remark = payment.PayRemark;

                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);

                y += 15;

                e.Graphics.DrawString(" การชำระเงิน " + "บิลเลขที่ : " + billNo, fontSubHeader, brush, x + 10, y);

                y += 15;

                if (payment.PayTypeID == 1)
                {
                    e.Graphics.DrawString(" ** ชำระด้วยเงินสด  ", fontSubHeader, brush, x + 20, y);
                }
                else if (payment.PayTypeID == 2)
                {
                    e.Graphics.DrawString(" ** ชำระด้วยบัตร  ", fontSubHeader, brush, x + 20, y);
                    y += 15;
                    e.Graphics.DrawString(" - ชื่อผู้ถือบัตร      : " + paymentCreditCardCust, fontBody, brush, x + 20, y);
                    y += 15;
                    e.Graphics.DrawString(" - บัตรเครติดประเภท : " + paymentCreditCardType, fontBody, brush, x + 20, y);
                    y += 15;
                    e.Graphics.DrawString(" - บัตรเครติดเลขที่   : " + paymentCreditCardNo, fontBody, brush, x + 20, y);
                }
                else if (payment.PayTypeID == 3)
                {
                    e.Graphics.DrawString(" ** ลงบิลลูกค้า : " + custName, fontBody, brush, x + 20, y);
                    y += 15;
                    e.Graphics.DrawString(" ** เหตุผล : " + remark, fontBody, brush, x + 20, y);
                }
                else if (payment.PayTypeID == 4)
                {
                    e.Graphics.DrawString(" ** บัตรเงินสด : " + custName, fontBody, brush, x + 20, y);
                    y += 15;
                }
                else if (payment.PayTypeID == 5)
                {
                    e.Graphics.DrawString(" ** ชำระด้วยการโอนเงิน/พร้อมเพย์   ", fontBody, brush, x + 20, y);
                    y += 15;
                    e.Graphics.DrawString(" - ชื่อลูกค้า      : " + paymentCreditCardCust, fontBody, brush, x + 20, y);
                    y += 15;
                    e.Graphics.DrawString(" - โอนเข้าบัญชี : " + paymentCreditCardType, fontBody, brush, x + 20, y);
                    y += 15;
                    e.Graphics.DrawString(" - รายละเอียด  : " + paymentCreditCardNo, fontBody, brush, x + 20, y);
                    y += 15;
                }

                y += 15;


                string custnName = "";
                string tel = "";
                string address = "";
                string taxID = "";


                if (custID > 0)
                {

                    List<Member> custLists = gd.getListAllMember();

                    foreach (Member cc in custLists)
                    {
                        if (cc.UserID == custID)
                        {

                            custnName = cc.Name;
                            address = cc.Address;
                            tel = cc.Tel;
                            taxID = cc.Password;
                        }

                    }


                    e.Graphics.DrawString(" ** ชื่อลูกค้า : " + custnName, fontBodys, brush, x + 5, y);
                    y += 15;

                    if (address.Length >= 58)
                    {
                        e.Graphics.DrawString(" ที่อยู่ : " + address.Substring(0, 57), fontBodys, brush, x + 5, y);
                        y += 15;
                        e.Graphics.DrawString("       " + address.Substring(57, address.Length - 58), fontBodys, brush, x + 5, y);
                        y += 15;
                    }
                    else
                    {
                        e.Graphics.DrawString(" ที่อยู่ : " + address, fontBodys, brush, x + 5, y);
                        y += 15;

                    }
                    e.Graphics.DrawString(" โทร : " + tel, fontBodys, brush, x + 5, y);
                    y += 15;
                    e.Graphics.DrawString(" เลขประจำตัวผู้เสียภาษี : " + taxID, fontBodys, brush, x + 5, y);
                    y += 50;

                    e.Graphics.DrawString(" ......................... ", fontBodys, brush, x + 70, y);
                    y += 15;
                    e.Graphics.DrawString("      ลายเข็นลูกค้า       ", fontBodys, brush, x + 70, y);
                    y += 15;


                }

                y += 15;
                e.Graphics.DrawString(" THANK YOU 고맙습니다 ", fontBody, brush, x + 60, y);
                //y += 15;
                //e.Graphics.DrawString(restlink, fontBody, brush, x + 0, y);
                //y += 15;
                //e.Graphics.DrawString(" Instagram: xxxxxxx ", fontBody, brush, x + 0, y);


                y += 20;
                e.Graphics.DrawString(" -", fontSubHeader, brush, x, y);



                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void printCustPay_PrintPage(object sender, PrintPageEventArgs e)
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

                Branch branch = gd.getBranchDesc();

                restName = branch.RestNameTH;
                restAddr1 = branch.RestAddr1TH;
                restAddr2 = branch.RestAddr2TH;
                restTel = "โทร. : " + branch.RestTel;


                ///////////////////////////

                Brush brush = new SolidBrush(Color.Black);
                Font fontHeader = new Font("Tahoma", 16);
                Font fontTable = new Font("Tahoma", 13);
                Font fontSubHeader = new Font("Tahoma", 9);
                Font fontBody = new Font("Tahoma", 9);
                Font fontBodylist = new Font("Tahoma", 9);
                Font fontBodys = new Font("Tahoma", 7);


                Bitmap img = global::AppRest.Properties.Resources.Logo_New;

                e.Graphics.DrawImage(img, x + 40, y, 220, 100);

                y += 105;

                e.Graphics.DrawString(restName, fontBody, brush, x + 30, y);

                y += 15;            

                e.Graphics.DrawString(restAddr1, fontBody, brush, x + 30, y);

                y += 15;

                e.Graphics.DrawString(restAddr2, fontBody, brush, x + 30, y);

                y += 15;

                e.Graphics.DrawString(restTel, fontBody, brush, x + 30, y);

                y += 15;

                e.Graphics.DrawString("" + this.custName, fontSubHeader, brush, x + 20, y);


                y += 30;

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd/MM/yyyy}", dt);
                string strTime = String.Format("{0:HH:mm:ss}", dt);


                e.Graphics.DrawString(" วันที่ " + strDate + "   เวลา  " + strTime, fontSubHeader, brush, x + 60, y);

                y += 15;


                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 12;

                e.Graphics.DrawString("จ่ายเป็นเงิน           : " + textBoxPayAmount.Text, fontBody, brush, x + 30, y);

                y += 15;

                e.Graphics.DrawString("ค้างชำระคงเหลือ เป็นเงิน : " + textBoxCreditCustBalance.Text, fontBody, brush, x + 30, y);

                y += 12;

                e.Graphics.DrawString("........................" , fontSubHeader, brush, x + 30, y);

                y += 12;

                // รายละเอียดการชำระเงิน

                int ii = 1;

                foreach(CustPaymentByPay cpp in this.cpByPayID)
                {

                    e.Graphics.DrawString( ii.ToString() + "."  + "#" + cpp.TrnID.ToString() + " > " + cpp.PayAmount.ToString("###,###.##")  , fontSubHeader, brush, x + 30, y);

                    ii++;
                    y += 12;
                }
                  

                y += 30;

                e.Graphics.DrawString("ลงชื่อผู้รับเงิน : ..................", fontSubHeader, brush, x + 30, y);


                y += 15;

                if (formCheckBill.paytype > 0)
                {


                    e.Graphics.DrawString(" การชำระเงิน " , fontBody, brush, x + 10, y);

                    y += 15;

                    if (formCheckBill.paytype == 1)
                    {
                        e.Graphics.DrawString(" ** ชำระด้วยเงินสด  ", fontBody, brush, x + 20, y);

                        y += 15;

                        e.Graphics.DrawString(" *** รับเงินสด : " + Int32.Parse(formCheckBill.amtFromCustomer).ToString("###,###") + " บาท.", fontBody, brush, x + 20, y);

                        y += 15;

                        e.Graphics.DrawString(" **** เงินทอน : " + Int32.Parse(formCheckBill.amtFromChange).ToString("###,###") + " บาท.", fontBody, brush, x + 20, y);

                    }
                    else if (formCheckBill.paytype == 2)
                    {
                        e.Graphics.DrawString(" ** ชำระด้วยบัตรเครดิต  ", fontBody, brush, x + 20, y);
                        y += 15;
                        e.Graphics.DrawString(" - ชื่อผู้ถือบัตร      : " + paymentCreditCardCust, fontBody, brush, x + 20, y);
                        y += 15;
                        e.Graphics.DrawString(" - บัตรเครติดประเภท : " + paymentCreditCardType, fontBody, brush, x + 20, y);
                        y += 15;
                        e.Graphics.DrawString(" - บัตรเครติดเลขที่   : " + paymentCreditCardNo, fontBody, brush, x + 20, y);
                    } 
                    else if (formCheckBill.paytype == 5)
                    {
                        e.Graphics.DrawString(" ** ชำระด้วย QR / โอนเงิน / พร้อมเพย์ / เช็ค ", fontBody, brush, x + 20, y);
                        y += 15;
                        e.Graphics.DrawString(" - ชื่อลูกค้า      : " + paymentCreditCardCust, fontBody, brush, x + 20, y);
                        y += 15;
                        e.Graphics.DrawString(" - โอนเข้าบัญชี : " + paymentCreditCardType, fontBody, brush, x + 20, y);
                        y += 15;
                        e.Graphics.DrawString(" - รายละเอียด  : " + paymentCreditCardNo, fontBody, brush, x + 20, y);
                        y += 15;
                    } 

                    y += 15;

                }

                int i = 1;


                y += 15;

                e.Graphics.DrawString(" ขอบคุณที่มาใช้บริการ ", fontSubHeader, brush, x + 90, y);

                y += 20;
                e.Graphics.DrawString(" -", fontSubHeader, brush, x, y);



                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printCustPay.Print();
        }

        List<String> selected;
        List<String> selected2;

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
             
            string strBill = "";
            selected = new List<String>();
            selected2 = new List<String>();

            string val = "";
            float amt = 0;


            try
            {

                foreach (DataGridViewRow row in dataGridViewPaymentCust.Rows)
                {


                    val = row.Cells["SelectBill"].Value.ToString();

                    if (val == "1")
                    {
                        selected.Add(row.Cells["BillNo"].Value.ToString());
                        selected2.Add(row.Cells["BalanceAmount"].Value.ToString());
                    }
                }

               // int i = 1;

                foreach (string str in selected)
                {

                    strBill += str;
                   
                }

                foreach (string str in selected2)
                {

                    amt += float.Parse(str);
                    
                }

            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }
            finally
            {

                textBoxPayBillID.Text = strBill;
                textBoxTotalPayAmount.Text = amt.ToString();
                //panelGroupCatProm.Visible = false;
            }
        


	
        }

        private void buttonCloseBC_Click(object sender, EventArgs e)
        {

        }

       

        private void textBoxSRTel_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxStrSearchMemCardtoTable_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridViewAllMember_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonClosePanelViewMaterial_Click(object sender, EventArgs e)
        {
            panelSearchCustomer.Visible = false;
        }

        private void buttonSearchCustomer_Click(object sender, EventArgs e)
        {
            panelSearchCustomer.Visible = true;
        }

        private void dataGridViewAllMember_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridproductID = Int32.Parse(dataGridViewAllMember.Rows[e.RowIndex].Cells["CustID"].Value.ToString());

                comboBoxCreditCust.SelectedValue = dataGridproductID;

                //comboBoxAllMember.Visible = true;
                //labelHeader.Text = "แก้ไขข้อมูล";
                //buttonAddTable.Text = "แก้ไขข้อมูล";
                //radioButtonUpdateData.Checked = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxSRMemName_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string srCustName = textBoxSearchCustName.Text;


                if (srCustName.Length > 0)
                {
                    this.cpAll.DefaultView.RowFilter = string.Format("Customer like '*{0}*' ", srCustName);
                }
                else
                {
                    this.cpAll.DefaultView.RowFilter = string.Format("1 = 1", 1);  // All
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonExportData_Click(object sender, EventArgs e)
        {
            try
            {
                ExportData.Excel_FromDataTable_DT(dataGridViewAllMember);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void comboBoxFlagCustPay_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                string flagCustPay = comboBoxFlagCustPay.Text;


                if (flagCustPay.ToLower() != "all")
                {
                    this.cpByCust.DefaultView.RowFilter = string.Format("FlagCreditCustPay like '{0}' ", flagCustPay);
                }
                else if (flagCustPay.ToLower() == "all")
                {
                    this.cpByCust.DefaultView.RowFilter = string.Format("1 = 1", 1);  // All
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewPaymentCust_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {



            //if (e.RowIndex == 1)
            //{
            //    DataGridViewCell cell = dataGridViewPaymentCust.Rows[e.RowIndex].Cells[0];
            //    DataGridViewCheckBoxCell chkCell = cell as DataGridViewCheckBoxCell;
            //    chkCell.Value = false;
            //    chkCell.FlatStyle = FlatStyle.Flat;
            //    chkCell.Style.ForeColor = Color.DarkGray;
            //    cell.ReadOnly = true;

            //}
        }

        string payStrBillID = "";
        int rptType = 0;

        private void buttonBillINV_Click(object sender, EventArgs e)
        {

            try
            {

                custID = Int32.Parse(comboBoxCreditCust.SelectedValue.ToString());
                payStrBillID = textBoxPayBillID.Text;
                rptType = 1;

                //fromDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxFromDate.Text);
                //toDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxToDate.Text);

                //dataSelectID = comboBoxTitle.SelectedIndex;


                LinkFromRptINVReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        FromRptINVReport formFromRptINVReport;

        private void LinkFromRptINVReport()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formFromRptINVReport == null)
            {
                formFromRptINVReport = new FromRptINVReport(this, 0, custID, payStrBillID, rptType);
            }
            else
            {
                formFromRptINVReport.rptCustID = custID;
                formFromRptINVReport.rptPayBillID = payStrBillID;
                formFromRptINVReport.rptType = rptType;
                formFromRptINVReport.viewReport();
            }
            Cursor.Current = Cursors.Default;
            if (formFromRptINVReport.ShowDialog() == DialogResult.OK)
            {
                formFromRptINVReport.Dispose();
                formFromRptINVReport = null;
            }
        }

        private void buttonINV_All_Click(object sender, EventArgs e)
        {
            try
            {

                custID = Int32.Parse(comboBoxCreditCust.SelectedValue.ToString());
                payStrBillID = textBoxPayBillID.Text;
                rptType = 2;

                //fromDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxFromDate.Text);
                //toDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxToDate.Text);

                //dataSelectID = comboBoxTitle.SelectedIndex;


                LinkFromRptINVReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBoxALL_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                //foreach (DataGridViewRow row in dataGridViewPaymentCust.Rows)
                //{
                //    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                //    chk.Value = !(chk.Value == null ? false : (bool)chk.Value); //because chk.Value is initialy null

                //}
                  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void buttonTestBill_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (DataGridViewRow row in dataGridViewPaymentCust.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = !(chk.Value == null ? false : (bool)chk.Value); //because chk.Value is initialy null

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





    }
}
