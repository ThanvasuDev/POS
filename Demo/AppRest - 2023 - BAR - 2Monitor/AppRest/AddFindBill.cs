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
    public partial class AddFindBill : AddDataTemplate
    {
        GetDataRest gd; 

        MainManage formMainManage;
        List<CustPayment> cp;

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

        public AddFindBill(Form frmlkFrom, int flagFrmClose, int custSelect)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();

            gd = new GetDataRest();
            cp = gd.getCustPayment();


            printerCashName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString(); 
            flagprintcash = ConfigurationSettings.AppSettings["FlagPrintCash"].ToString();


            printBill.PrinterSettings.PrinterName = printerCashName; 
             

            taxPercent = Double.Parse(ConfigurationSettings.AppSettings["TaxPercent"].ToString());
            servicePercent = Double.Parse(ConfigurationSettings.AppSettings["ServiceChargePercent"].ToString());
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
                 billNo = Int32.Parse(billName.Replace('#',' '));

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

                Payment payment = gd.getTrnPayment(trnBillIDSect);

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

          

        private void buttonPrintBill_Click(object sender, EventArgs e)
        {
            printBill.Print();
        }

         

        // OnPrintPage
        private void printBill_PrintPage(object sender, PrintPageEventArgs e)
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



                e.Graphics.DrawString(restAddr1, fontBody, brush, x + 30, y);

                y += 15;

                e.Graphics.DrawString(restAddr2, fontBody, brush, x + 30, y);

                y += 15;

                e.Graphics.DrawString(restTel, fontBody, brush, x + 30, y);

                y += 15;

                e.Graphics.DrawString(Login.userID.ToString() + " " + Login.userName, fontBodylist, brush, x + 5, y);

                y += 10;
 

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd MMM yy}", dt);
                string strTime = String.Format("{0:HH:mm}", dt);

                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 12;
                e.Graphics.DrawString("Tbl " + this.textBoxtableName.Text, fontSubHeader, brush, x + 10, y);
                e.Graphics.DrawString("Gst ", fontSubHeader, brush, x + 240, y);
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

                foreach (Transaction o in trn)
                {

                    if (o.GroupCatID > 0)
                    {

                        str1 = o.ProductName;

                        str2 = o.SalesQTY.ToString();
                        str3 = (o.SalesAmount / o.SalesQTY).ToString("###,###");
                        str4 = o.SalesAmount.ToString("###,###");



                        e.Graphics.DrawString(str1, fontBodylist, brush, x + 22, y);
                        e.Graphics.DrawString(str2, fontBodylist, brush, x + 0, y);
                        e.Graphics.DrawString(str4, fontBodylist, brush, x + 240, y);

                        i++;
                        y += 15;

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

                y += 15;
                txtOrder = "FOOD";
                txtAmt = salesAmountFood.ToString("###,###");
                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 240, y);

                y += 15;
                txtOrder = "DRINK / OTHERS";
                txtAmt = salesAmountDrinks.ToString("###,###");
                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 240, y);


                if (discount > 0)
                {
                    y += 15;

                    txtOrder = "Discount (Total)";
                    txtAmt = discount.ToString("###,###");

                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 240, y);
                }


                if (serviceCharge > 0)
                {
                    y += 15;
                    txtOrder = "Service " + ((float)(this.servicePercent * 100)).ToString() + "%";
                    txtAmt = serviceCharge.ToString("###,###");
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 240, y);

                }

                if (tax > 0)
                {
                    y += 15;
                    txtOrder = "Vat " + ((float)(this.taxPercent * 100)).ToString() + "%";
                    txtAmt = tax.ToString("###,###");
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontBodylist, brush, x + 240, y);

                }


                y += 25;
                txtOrder = "Total Due";
                txtAmt = payment.PayAmount.ToString("###,###");
                e.Graphics.DrawString(txtOrder, fontTable, brush, x + 20, y);
                e.Graphics.DrawString(txtAmt, fontTable, brush, x + 180, y);


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


                    e.Graphics.DrawString(" ** ชื่อลูกค้า : " + custnName, fontBodys, brush, x + 10, y);
                    y += 15;

                    if (address.Length >= 58)
                    {
                        e.Graphics.DrawString(" ที่อยู่ : " + address.Substring(0, 57), fontBodys, brush, x + 10, y);
                        y += 15;
                        e.Graphics.DrawString("       " + address.Substring(57, address.Length - 58), fontBodys, brush, x + 10, y);
                        y += 15;
                    }
                    else
                    {
                        e.Graphics.DrawString(" ที่อยู่ : " + address, fontBodys, brush, x + 10, y);
                        y += 15;

                    }
                    e.Graphics.DrawString(" โทร : " + tel, fontBodys, brush, x + 10, y);
                    y += 15;
                    e.Graphics.DrawString(" เลขประจำตัวผู้เสียภาษี : " + taxID, fontBodys, brush, x + 10, y);
                    y += 50;

                    e.Graphics.DrawString(" ......................... ", fontBodys, brush, x + 70, y);
                    y += 15;
                    e.Graphics.DrawString("      ลายเข็นลูกค้า       ", fontBodys, brush, x + 70, y);
                    y += 15;


                }

                y += 15;
                e.Graphics.DrawString(" THANK YOU 고맙습니다 ", fontBody, brush, x + 60, y);
                y += 15;
             //   e.Graphics.DrawString(" FB: www.facebook.com/YothinFoodPark ", fontBody, brush, x + 0, y);
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

        private void buttonSearch_Click(object sender, EventArgs e)
        {

        }

 
      



    }
}
