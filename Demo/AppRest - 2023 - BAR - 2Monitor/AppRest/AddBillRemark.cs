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
    public partial class AddBillRemark : AddDataTemplate
    {
        GetDataRest gd;
        MainManage formMainManage;

        List<BillRemark> allBillRemark;


        Branch branch;
        string defaultlangBill;
        string flagLang;
        int flagFD;
        int billID;
        int copyPrint = 0;

        string restlink;
        string fblink;
        string iglink;
        string qrlink;
        string linelink;


        string remarkLine1;
        string remarkLine2;
        string remarkLine3;



        public AddBillRemark(Form frmlkFrom, int flagFrmClose)
        {
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();
            this.Width = 1024;
            this.Height = 764;


            flagLang = "TH";
            flagFD = 0;
            defaultlangBill = ConfigurationSettings.AppSettings["DefaultlangBill"].ToString();
            restlink = ConfigurationSettings.AppSettings["RestLink"].ToString();
            fblink = ConfigurationSettings.AppSettings["FBLink"].ToString();
            iglink = ConfigurationSettings.AppSettings["IGLink"].ToString();
            qrlink = ConfigurationSettings.AppSettings["QRLink"].ToString();
            linelink = ConfigurationSettings.AppSettings["LINELink"].ToString();

            printBillABB.PrinterSettings.PrinterName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();
            printBillFullTax.PrinterSettings.PrinterName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();

            billID = 231006001;

            gd = new GetDataRest();

       
            BillHeader(); 
            BillFooter();

        }

        private void BillHeader()
        {
            branch = gd.getBranchDesc();

            textBoxBranchNameTH.Text = branch.BranchNameTH;
            textBoxBranchNameEN.Text = branch.BranchNameEN;

            textBoxRestNameTH.Text = branch.RestNameTH;
            textBoxAddrTH1.Text = branch.RestAddr1TH;
            textBoxAddrTH2.Text = branch.RestAddr2TH;
            textBoxTelNo.Text = branch.RestTel;

            textBoxRestNameEN.Text = branch.RestNameEN;
            textBoxAddrEN1.Text = branch.RestAddr1EN;
            textBoxAddrEN2.Text = branch.RestAddr2EN;

            textBoxLineNo1.Text = branch.RestLine1;
            textBoxLineNo2.Text = branch.RestLine2;
            textBoxTaxID.Text = branch.RestTaxID; 
            textBoxTAXRD.Text = branch.RestTaxRD;

        }

        private void buttonDefaultBillHeader_Click(object sender, EventArgs e)
        {
            BillHeader();
        }

        private void buttonUpdateBillHeader_Click(object sender, EventArgs e)
        {
            try
            {


                if (MessageBox.Show("คุณต้องการจะปรับหัวใบเสร็จ หรือไม่ ?", "แก้ไข", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {


                    int result = gd.updsReshBranch(textBoxBranchNameTH.Text, textBoxBranchNameEN.Text, textBoxRestNameTH.Text, textBoxAddrTH1.Text, textBoxAddrTH2.Text, textBoxRestNameEN.Text, textBoxAddrEN1.Text, textBoxAddrEN2.Text, textBoxTelNo.Text, textBoxTaxID.Text, textBoxLineNo1.Text, textBoxLineNo2.Text, textBoxTAXRD.Text);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error Insert New Bill Header : Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("ปรับหัวใบเสร็จ >> (Success)");
                        BillHeader();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void buttonAddTable_Click(object sender, EventArgs e)
        {

            try
            {

                string strline1 = txtBoxLine01.Text;
                string strline2 = txtBoxLine02.Text;
                string strline3 = txtBoxLine03.Text;

                if (MessageBox.Show("คุณต้องการจะแก้ไขข้อความท้ายบิล หรือไม่ ?", "เพิ่ม ข้อความ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                { 
              

                    int result = gd.updsBillRemark(1, strline1, strline2, strline3);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error Insert New Bill Remark : Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("ข้อความท้ายบิล >> (Success)");
                        BillFooter();

                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }




        private void button1_Click(object sender, EventArgs e)
        {
            txtBoxLine1.Text = "";
            txtBoxLine2.Text = "";
            txtBoxLine3.Text = "";
        }

        private void BillFooter()
        {
            try
            {

                allBillRemark = gd.getAllBillRemark();


                foreach (BillRemark c in allBillRemark)
                {
                    txtBoxLine1.Text = c.BillRemarkL1;
                    txtBoxLine2.Text = c.BillRemarkL2;
                    txtBoxLine3.Text = c.BillRemarkL3;

                    remarkLine1 = c.BillRemarkL1;
                    remarkLine2 = c.BillRemarkL2;
                    remarkLine3 = c.BillRemarkL3;

                }

                richTextBox1.Text = txtBoxLine1.Text + "\n" + txtBoxLine2.Text + "\n" + txtBoxLine3.Text;

            }
            catch (Exception ex)
            {

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

                    e.Graphics.DrawString("ใบเสร็จรับเงิน", fontBody, brush, x + 120, y);


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
                string tableName = "T1";

      

                e.Graphics.DrawString(" " + strDate + " " + strTime, fontSubHeader, brush, x + 150, y);

                y += 10;

                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 10;
                e.Graphics.DrawString(" " + tableName, fontHeader, brush, x + 10, y);
                e.Graphics.DrawString("Gst 2", fontHeader, brush, x + 200, y);
                y += 20;

                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 15;

                //// Print Order

              

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



                if (flagLang == "TH")
                {  
                    str1 = "สินค้า/อาหาร/เครื่องดื่ม (10)"; 
                }
                else if (flagLang == "EN")
                {
                    str1 = "Product / Food / Drink (10)";
                }

                str3 = "100";
                str2 = "11";
                str4 = "110.00";
                str4 = String.Format("{0,10}", str4);


                e.Graphics.DrawString(str1, fontBodylist, brush, x + 22, y);
                e.Graphics.DrawString(str2, fontBodylist, brush, x + 0, y);
                e.Graphics.DrawString(str4, fontNum, brush, x + 180, y);



                y += 15;


                //////////////////////////////////////////////////////////////////////////

                string txtOrder = "";
                string txtAmt = "";

                 


                y += 15;
                txtOrder = "Sub Total";
                txtAmt = (110).ToString("###,###.#0");
                txtAmt = String.Format("{0,10}", txtAmt);
                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);


                if (copyPrint != 99)
                {

                    y += 15;
                    txtOrder = "Total Discount ";
                    txtAmt = (10).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);


                    //y += 15;
                    //txtOrder = "Service " + ((float)(0.1 * 100)).ToString() + "%";
                    //txtAmt = (10).ToString("###,###.#0");
                    //txtAmt = String.Format("{0,10}", txtAmt);
                    //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    //e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

 
                        y += 15;
                        txtOrder = "Amt Before VAT " + ((float)(0.07 * 100)).ToString() + "%";
                        txtAmt = (100.0).ToString("###,###.#0");
                        txtAmt = String.Format("{0,10}", txtAmt);
                        e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                        e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);
 

                        y += 15;
                        txtOrder = "VAT " + ((float)(0.07 * 100)).ToString() + "%";
                        txtAmt = (7.00).ToString("###,###.#0");
                        txtAmt = String.Format("{0,10}", txtAmt);
                        e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                        e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    }




                    y += 25;
                    txtOrder = "Total ";
                    txtAmt = (107.00).ToString("###,###.#0");
                    e.Graphics.DrawString(txtOrder, fontTable, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontTable, brush, x + 190, y);


                    y += 20;


                    string remark = "";
                    string custName = "";


 


                    //textBoxCustShare

                    e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);

 




                y += 15;


                    y += 15;

                if (langBill == "TH")
                {

                    e.Graphics.DrawString(" การชำระเงิน " + "บิลเลขที่ : #" + this.billID.ToString(), fontBody, brush, x + 10, y);
                    y += 20;
                    i = 1;

                    y += 15;
                    txtOrder = i.ToString() + ". ชำระเงินสด  :";
                    txtAmt = (107).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    y += 15;
                    txtOrder = "  - รับเงินมา  : ";
                    txtAmt = (110).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    y += 15;
                    txtOrder = "  - เงินทอน  : ";
                    txtAmt = (3).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                }
                else
                {

                    e.Graphics.DrawString(" Payment " + "Bill No. : " + this.billID.ToString(), fontBody, brush, x + 10, y);
                    y += 20;
                    i = 1;

                    y += 15;
                    txtOrder = i.ToString() + ". Cash  :";
                    txtAmt = (107).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    y += 15;
                    txtOrder = "  - RECEIVE  : ";
                    txtAmt = (110).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    y += 15;
                    txtOrder = "  - CHANGE  : ";
                    txtAmt = (3).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);


                }


                    y += 15;

                

                // Update เรื่องคะแนนของ MemmCard



             

             

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
                y += 20;
                e.Graphics.DrawImage(global::AppRest.Properties.Resources.Logo_IG, x + 75, y - 5, 18, 18);
                e.Graphics.DrawString(this.iglink, fontBody, brush, x + 98, y);

                y += 20;
                e.Graphics.DrawImage(global::AppRest.Properties.Resources.Logo_LINE, x + 75, y - 5, 18, 18);
                e.Graphics.DrawString("@" + this.linelink, fontBody, brush, x + 98, y);


           

                y += 15;

                //if (qrlink.Length > 0)
                //{
                //    Bitmap img3 = global::AppRest.Properties.Resources.QR;
                //    e.Graphics.DrawImage(img3, x + 63, y, 150, 150);
                //    y += 145;
                //}

                //e.Graphics.DrawString(this.restlink, fontBody, brush, x + 70, y);

                e.HasMorePages = false;

            }
            catch (Exception ex)
            {

            }
        }

        private void OnPrintPageCash2(object sender, PrintPageEventArgs e)
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

                Font fontHeaderQ = new Font("Arail", 18, FontStyle.Bold);
                Font fontHeader = new Font("Arail", 12, FontStyle.Bold);
                Font fontTable = new Font("Arail", 11, FontStyle.Bold);
                Font fontSubHeader = new Font("Arail", 9, FontStyle.Bold);
                Font fontFooter = new Font("Arail", 7, FontStyle.Regular);
                Font fontBody = new Font("Arail", 8, FontStyle.Regular);
                Font fontBodylist = new Font("Arail", 9, FontStyle.Regular);
                Font fontNum = new Font("Consolas", 9, FontStyle.Regular);

                //  Bitmap img = global::AppRest.Properties.Resources.Logo_New;

                //if (this.billID > 0)
                //{
                //    e.Graphics.DrawString("Q" + FuncString.Right(this.billID.ToString(), 3), fontHeaderQ, brush, x + 100, y);
                //    y += 30;
                //}


                if (this.copyPrint == 99)
                {
                    e.Graphics.DrawString("ใบสรุป Order", fontBody, brush, x + 120, y);

                    y += 12;

                }
                else
                {

                    //e.Graphics.DrawString("ใบเสร็จรับเงิน", fontBody, brush, x + 120, y);


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

                    e.Graphics.DrawString(Login.userID.ToString() + " " + Login.userName, fontBodylist, brush, x + 5, y);

                }

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd MMM yy}", dt);
                string strTime = String.Format("{0:HH:mm}", dt);
                string tableName = "T1";



                e.Graphics.DrawString(" " + strDate + " " + strTime, fontSubHeader, brush, x + 150, y);

                y += 10;

                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 10;
                e.Graphics.DrawString(" " + tableName, fontHeader, brush, x + 10, y);
                e.Graphics.DrawString("Gst 2", fontHeader, brush, x + 200, y);
                y += 20;

                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 15;

                //// Print Order



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


                if (flagFD == 1)
                {
                    if (flagLang == "TH")
                    {
                        str1 = "อาหาร/เครื่องดื่ม";
                    }
                    else if (flagLang == "EN")
                    {
                        str1 = "Food/Drink";
                    }

                    str3 = "100";
                    str2 = "11";
                    str4 = "1,100.00";
                    str4 = String.Format("{0,10}", str4);


                    e.Graphics.DrawString(str1, fontBodylist, brush, x + 22, y);
                    e.Graphics.DrawString(str2, fontBodylist, brush, x + 0, y);
                    e.Graphics.DrawString(str4, fontNum, brush, x + 180, y);



                    y += 15;
                }
                else
                {

                    if (flagLang == "TH")
                    {
                        str1 = "สินค้า - อาหาร - เครื่องดื่ม 1 (10)";
                    }
                    else if (flagLang == "EN")
                    {
                        str1 = "Product - Food - Drink 1 (10)";
                    }

                    str3 = "100";
                    str2 = "6";
                    str4 = "600.00";
                    str4 = String.Format("{0,10}", str4);


                    e.Graphics.DrawString(str1, fontBodylist, brush, x + 22, y);
                    e.Graphics.DrawString(str2, fontBodylist, brush, x + 0, y);
                    e.Graphics.DrawString(str4, fontNum, brush, x + 180, y);



                    y += 15;

                    if (flagLang == "TH")
                    {
                        str1 = "สินค้า - อาหาร - เครื่องดื่ม 2 (10)";
                    }
                    else if (flagLang == "EN")
                    {
                        str1 = "Product - Food - Drink 2 (10)";
                    }

                    str3 = "100";
                    str2 = "5";
                    str4 = "500.00";
                    str4 = String.Format("{0,10}", str4);


                    e.Graphics.DrawString(str1, fontBodylist, brush, x + 22, y);
                    e.Graphics.DrawString(str2, fontBodylist, brush, x + 0, y);
                    e.Graphics.DrawString(str4, fontNum, brush, x + 180, y);
 
                }

                y += 15;

                //////////////////////////////////////////////////////////////////////////

                string txtOrder = "";
                string txtAmt = "";




                y += 15;
                txtOrder = "Sub Total";
                txtAmt = (110).ToString("###,###.#0");
                txtAmt = String.Format("{0,10}", txtAmt);
                e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);


                if (copyPrint != 99)
                {

                    y += 15;
                    txtOrder = "Total Discount ";
                    txtAmt = (10).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);


                    //y += 15;
                    //txtOrder = "Service " + ((float)(0.1 * 100)).ToString() + "%";
                    //txtAmt = (10).ToString("###,###.#0");
                    //txtAmt = String.Format("{0,10}", txtAmt);
                    //e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    //e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);


                    y += 15;
                    txtOrder = "Amt Before VAT " + ((float)(0.07 * 100)).ToString() + "%";
                    txtAmt = (100.0).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);


                    y += 15;
                    txtOrder = "VAT " + ((float)(0.07 * 100)).ToString() + "%";
                    txtAmt = (7.00).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                }




                y += 25;
                txtOrder = "Total ";
                txtAmt = (107.00).ToString("###,###.#0");
                e.Graphics.DrawString(txtOrder, fontTable, brush, x + 20, y);
                e.Graphics.DrawString(txtAmt, fontTable, brush, x + 190, y);


                y += 20;


                string remark = "";
                string custName = "";





                //textBoxCustShare

                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);


                 

                y += 15;


                y += 15;

                if (langBill == "TH")
                {

                    e.Graphics.DrawString(" การชำระเงิน " + "บิลเลขที่ : #" + this.billID.ToString(), fontBody, brush, x + 10, y);
                    y += 20;
                    i = 1;

                    y += 15;
                    txtOrder = i.ToString() + ". ชำระเงินสด  :";
                    txtAmt = (107).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    y += 15;
                    txtOrder = "  - รับเงินมา  : ";
                    txtAmt = (110).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    y += 15;
                    txtOrder = "  - เงินทอน  : ";
                    txtAmt = (3).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                }
                else
                {

                    e.Graphics.DrawString(" Payment " + "Bill No. : " + this.billID.ToString(), fontBody, brush, x + 10, y);
                    y += 20;
                    i = 1;

                    y += 15;
                    txtOrder = i.ToString() + ". Cash  :";
                    txtAmt = (107).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    y += 15;
                    txtOrder = "  - RECEIVE  : ";
                    txtAmt = (110).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);

                    y += 15;
                    txtOrder = "  - CHANGE  : ";
                    txtAmt = (3).ToString("###,###.#0");
                    txtAmt = String.Format("{0,10}", txtAmt);
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 20, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 180, y);


                }


                y += 15;



                // Update เรื่องคะแนนของ MemmCard

                string custnName = "";
                string tel = "";
                string address = "";
                string taxID = "";
                string title = "";

                y += 15;

                int custID = 1;


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
                y += 20;
                e.Graphics.DrawImage(global::AppRest.Properties.Resources.Logo_IG, x + 75, y - 5, 18, 18);
                e.Graphics.DrawString(this.iglink, fontBody, brush, x + 98, y);

                y += 20;
                e.Graphics.DrawImage(global::AppRest.Properties.Resources.Logo_LINE, x + 75, y - 5, 18, 18);
                e.Graphics.DrawString("@" + this.linelink, fontBody, brush, x + 98, y);




                y += 15;

                //if (qrlink.Length > 0)
                //{
                //    Bitmap img3 = global::AppRest.Properties.Resources.QR;
                //    e.Graphics.DrawImage(img3, x + 63, y, 150, 150);
                //    y += 145;
                //}

                //e.Graphics.DrawString(this.restlink, fontBody, brush, x + 70, y);

                e.HasMorePages = false;

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonPrintBill_Click(object sender, EventArgs e)
        {
            printBillABB.Print();
        }

        private void buttonPrintFullTAX_Click(object sender, EventArgs e)
        {
            printBillFullTax.Print();
        } 
 

        private void radioButtonPD_Click(object sender, EventArgs e)
        {
            if (radioButtonFDFlag.Checked == true)
            {
                flagFD = 1;
            }
            else
            {
                flagFD = 0;
            }
        }
    }
}
