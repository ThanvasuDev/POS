using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Drawing.Printing;


namespace AppRest
{
    public partial class MainSummary : MainTemplateS
    {
        DataTable result;
        GetDataRest gd;

        TrnMax trnMax;

        string pathExportResultStockName;
        string pathExportResultName;

        string fromDate = "";
        string toDate = "";
        List<Supplier> allSupplier;
        int dataSelectID = 0;
        int productSuplierID = 0;

        public MainSummary(Form frmlkFrom, int flagFrmClose)
        {
            InitializeComponent();
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }

            this.Text = this.Text + " ( By : " + Login.userName + ")";

            buttonLinkSummary.BackColor = System.Drawing.Color.Gray;

            result = new DataTable();
            gd = new GetDataRest();

            trnMax = gd.getTrnMax();

            textBoxFromDate.Text = trnMax.MaxDate;
            textBoxToDate.Text = trnMax.MaxDate;

            allSupplier = gd.getPC_Supplier(0, "0", "0");
            getComboAllSupplier();

            getComboTitle();
            getResultDatatable();

            pathExportResultStockName = ConfigurationSettings.AppSettings["PathExportResultStockName"].ToString();
            pathExportResultName = ConfigurationSettings.AppSettings["PathExportResultName"].ToString();

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



            printDayThermal.PrinterSettings.PrinterName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();
            printSummary.PrinterSettings.PrinterName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();

        }

        private void getComboTitle()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List


            data.Add(new KeyValuePair<int, string>(1, "สรุปยอดขายรายสินค้า"));
            data.Add(new KeyValuePair<int, string>(2, "สรุปยอดขายตาม Supplier"));
            data.Add(new KeyValuePair<int, string>(3, "สรุปข้อมูลการชำระเงิน"));
            data.Add(new KeyValuePair<int, string>(4, "สรุปข้อมูลลูกค้าลงบิล"));
            data.Add(new KeyValuePair<int, string>(5, "สรุปรายละเอียดลูกค้าลงบิล"));
            data.Add(new KeyValuePair<int, string>(6, "สรุปยอดขายรายวัน"));
            data.Add(new KeyValuePair<int, string>(7, "สรุปยอดขายรายเดือน"));
            data.Add(new KeyValuePair<int, string>(8, "สรุปข้อมูลส่งภาษี"));
            data.Add(new KeyValuePair<int, string>(9, "สรุปข้อมูลส่งภาษีแยกประเภท"));
            data.Add(new KeyValuePair<int, string>(10, "สรุปข้อมูลส่งภาษีแยกตามลูกค้า"));
            data.Add(new KeyValuePair<int, string>(11, "รายงานการลบ Order"));
            data.Add(new KeyValuePair<int, string>(12, "รายละเอียดรายบิล"));
            data.Add(new KeyValuePair<int, string>(13, "ยอดขายรายสมาชิก"));
            data.Add(new KeyValuePair<int, string>(14, "ยอดขายรายสมาชิก (Total)"));

            // Clear the combobox
            comboBoxTitle.DataSource = null;
            comboBoxTitle.Items.Clear();

            // Bind the combobox
            comboBoxTitle.DataSource = new BindingSource(data, null);
            comboBoxTitle.DisplayMember = "Value";
            comboBoxTitle.ValueMember = "Key";

            comboBoxTitle.SelectedIndex = 0;


        }



        private void getComboAllSupplier()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "= เลือก Product Suplier ="));

            foreach (Supplier c in allSupplier)
            {
                data.Add(new KeyValuePair<int, string>(c.SupplierID, c.SupplierName));
            }


            // Clear the combobox
            comboBoxSuplier.DataSource = null;
            comboBoxSuplier.Items.Clear();

            // Bind the combobox
            comboBoxSuplier.DataSource = new BindingSource(data, null);
            comboBoxSuplier.DisplayMember = "Value";
            comboBoxSuplier.ValueMember = "Key";

            comboBoxSuplier.SelectedIndex = 0;
        }
        private void getResultDatatable()
        {
            try
            {

                textBoxSRProductName.Text = "";


                fromDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxFromDate.Text);
                toDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxToDate.Text);

                dataSelectID = comboBoxTitle.SelectedIndex;

                productSuplierID = Int32.Parse(comboBoxSuplier.SelectedValue.ToString());


                if (dataSelectID == 0)
                    this.result = gd.getSum_SalesProduct(fromDate, toDate);
                else if (dataSelectID == 1)
                    this.result = gd.getSum_ProductSupplier(productSuplierID, fromDate, toDate);
                else if (dataSelectID == 2)
                    this.result = gd.getSum_TrnPayment(fromDate, toDate);
                else if (dataSelectID == 3)
                    this.result = gd.getSum_CreditCust(fromDate, toDate);
                else if (dataSelectID == 4)
                    this.result = gd.getSum_CreditCustDetail(fromDate, toDate);
                else if (dataSelectID == 5)
                    this.result = gd.getSum_TrnDay(fromDate, toDate);
                else if (dataSelectID == 6)
                    this.result = gd.getSum_TrnMonth(fromDate, toDate);
                else if (dataSelectID == 7)
                    this.result = gd.getSum_SalesPaymentTaxByDate_GroupDate(fromDate, toDate);
                else if (dataSelectID == 8)
                    this.result = gd.getSum_SalesPaymentTaxByDate_GroupDate_Type(fromDate, toDate);
                else if (dataSelectID == 9)
                    this.result = gd.getSum_SalesPaymentTaxByDate(fromDate, toDate);
                else if (dataSelectID == 10)
                    this.result = gd.getSum_LogDelOrder(fromDate, toDate);
                else if (dataSelectID == 11)
                    this.result = gd.getSum_SalesProductByBill(fromDate, toDate);
                else if (dataSelectID == 12)
                    this.result = gd.getSum_SalesByMemCard(fromDate, toDate);
                else if (dataSelectID == 13)
                    this.result = gd.getSum_SalesByMemCard_Total(fromDate, toDate);

                dataGridViewResult.DataSource = null;
                dataGridViewResult.DataSource = this.result;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonViewData_Click(object sender, EventArgs e)
        {
            getResultDatatable();
        }

        private void SelectChange(object sender, EventArgs e)
        {
            getResultDatatable();
        }

        private void KeyPassFinish(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                getResultDatatable();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ExportData.Excel_FromDataTable_DT(dataGridViewResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonPrintA4_Click(object sender, EventArgs e)
        {

            try
            {

                fromDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxFromDate.Text);
                toDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxToDate.Text);

                dataSelectID = comboBoxTitle.SelectedIndex;


                LinkFromRptBillReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        FromRptSumReport formFromRptSumReport;

        private void LinkFromRptBillReport()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formFromRptSumReport == null)
            {
                formFromRptSumReport = new FromRptSumReport(this, 0, fromDate, toDate, productSuplierID, dataSelectID);
            }
            else
            {
                formFromRptSumReport.rptFromDate = fromDate;
                formFromRptSumReport.rptToDate = toDate;
                formFromRptSumReport.rptType = dataSelectID;
                formFromRptSumReport.rptProductSupID = productSuplierID;
                formFromRptSumReport.viewReport();
            }
            Cursor.Current = Cursors.Default;
            if (formFromRptSumReport.ShowDialog() == DialogResult.OK)
            {
                formFromRptSumReport.Dispose();
                formFromRptSumReport = null;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxSRProductName.Text = "";
            textBoxSRMemCardName.Text = "";
        }

        private void textBoxSRProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string srPName = textBoxSRProductName.Text;


                if (srPName.Length > 0)
                {
                    this.result.DefaultView.RowFilter = string.Format("ProductName like '*{0}*' or GroupCatName  like '*{0}*' or  CatName  like '*{0}*' ", srPName);

                }
                else
                {
                    this.result.DefaultView.RowFilter = string.Format("1 = 1 ");

                }



            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxSRMemCardName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string srPName = textBoxSRProductName.Text;
                string srMName = textBoxSRMemCardName.Text;

                if (srPName.Length > 0 || srMName.Length == 0)
                {
                    this.result.DefaultView.RowFilter = string.Format("  ProductName like '*{0}*' or  CatName like '*{0}*' ", srPName);

                }
                else if (srPName.Length > 0 || srMName.Length > 0)
                {
                    this.result.DefaultView.RowFilter = string.Format("(MemCardID like '*{0}*' or MemCardName  like '*{0}*' ) and ProductName like '*{1}*' ", srMName, srPName);

                }
                else
                {
                    this.result.DefaultView.RowFilter = string.Format("1 = 1 ");

                }



            }
            catch (Exception ex)
            {

            }
        }

        int pageProductPrint = 1;
        int noofPage = 0;

        List<SalesToday> salesFromTo;

        private void buttonReportThermal_Click(object sender, EventArgs e)
        {
            try
            {
                fromDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxFromDate.Text);
                toDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxToDate.Text);

                dataSelectID = comboBoxTitle.SelectedIndex;

                salesFromTo = gd.getSalesByday_FromTo(dataSelectID, fromDate, toDate);

                noofPage = salesFromTo.Count / 60 + 1;

                for (pageProductPrint = 1; pageProductPrint <= noofPage; pageProductPrint++)
                    printDayThermal.Print();


            }
            catch (Exception ex)
            {

            }

            //  string DateDay = dateTimePickerStartDate.Value.ToString("dd/MM/yyyy");



        }

        private void printDayThermal_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

                int x = 0;
                int y = 0;


                Brush brush = new SolidBrush(Color.Black);
                Font fontHeader = new Font("ARAIL", 12);
                Font fontTable = new Font("ARAIL", 11);
                Font fontSubHeader = new Font("ARAIL", 9);
                Font fontBody = new Font("ARAIL", 9);
                Font fontBodylist = new Font("ARAIL", 9);
                Font fontNum = new Font("Consolas", 9);



                //    Bitmap img = global::AppRest.Properties.Resources.Logo_New;

                Branch branch = gd.getBranchDesc();

                Bitmap imgHeader = global::AppRest.Properties.Resources.Logo_New;



                //e.Graphics.DrawImage(imgHeader, x + 10, y, 250, 90);
                //y += 90;



                //y += 10;

                e.Graphics.DrawString(branch.BranchNameTH, fontTable, brush, x + 0, y);
                y += 20;

                //if (branch.RestTaxID.Length > 0)
                //    e.Graphics.DrawString("Tax Invoice(ABB)", fontTable, brush, x + 70, y);
                //else
                //    e.Graphics.DrawString("Receipt", fontTable, brush, x + 83, y);

                //y += 15;

                e.Graphics.DrawString(comboBoxTitle.Text, fontSubHeader, brush, x + 10, y);
                y += 15;
                e.Graphics.DrawString("Data Date : " + textBoxFromDate.Text + " - " + textBoxToDate.Text, fontSubHeader, brush, x + 10, y);
                y += 15;


                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd/MM/yyyy}", dt);
                string strTime = String.Format("{0:HH:mm:ss}", dt);



                e.Graphics.DrawString("POS # : " + Login.posBranchID, fontSubHeader, brush, x + 10, y);
                y += 15;
                e.Graphics.DrawString("Cashier : " + Login.userName, fontSubHeader, brush, x + 10, y);
                y += 15;
                e.Graphics.DrawString("Print Date : " + strDate, fontSubHeader, brush, x + 10, y);
                y += 15;
                e.Graphics.DrawString("Print Time : " + strTime, fontSubHeader, brush, x + 10, y);
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

                if (dataSelectID == 0)
                {
                    e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                    y += 15;

                    e.Graphics.DrawString("       Product                                 Unit      Amount ", fontSubHeader, brush, x, y);


                    y += 15;
                    e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                    y += 15;

                }
                else if (dataSelectID == 4 || (dataSelectID == 5))
                {
                    e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                    y += 15;

                    e.Graphics.DrawString("       Date                                #Bill      Amount ", fontSubHeader, brush, x, y);


                    y += 15;
                    e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                    y += 15;


                }
                else
                {
                    y += 15;
                    e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                    y += 15;

                }



                int iii = 1;
                int pagestartindex = 0;
                int pageEndindex = 0;

                pagestartindex = 60 * (pageProductPrint - 1) + 1;
                pageEndindex = 60 * pageProductPrint;

                string[] stSp;

                foreach (SalesToday st in salesFromTo)
                {

                    if (iii >= pagestartindex && iii <= pageEndindex)
                    {

                        lb = st.SalesLable;

                        val = st.SalesAmount;

                        if (st.SalesLable.Substring(0, 2) == ">>")
                            str1 = "" + st.SalesLable;
                        else
                            str1 = "  " + st.SalesLable;

                        str2 = st.SalesUnit.ToString("###,###");
                        str3 = st.SalesAmount.ToString("###,###");


                        str3 = String.Format("{0,10}", str3);
                        str2 = String.Format("{0,5}", str2);

                        if (dataSelectID < 8)
                        {

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

                        }
                        else
                        {
                            stSp = str1.Split('>');


                            e.Graphics.DrawString(iii.ToString() + ". " + stSp[0], fontBody, brush, x + 2, y);
                            y += 13;
                            e.Graphics.DrawString("    >> " + stSp[1], fontBody, brush, x + 2, y);
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

        private void buttonPrintSummary_Click(object sender, EventArgs e)
        {
            try
            {

                fromDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxFromDate.Text);
                toDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxToDate.Text);

                dataSelectID = 200;

                salesFromTo = gd.getSalesByday_FromTo(dataSelectID, fromDate, toDate);

                printSummary.Print();
            }
            catch (Exception ex)
            {


            }
        }

        private void OnPrintSummary(object sender, PrintPageEventArgs e)
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


                ///////////////////////////


                Brush brush = new SolidBrush(Color.Black);


                Font fontHeader = new Font("Arail", 12, FontStyle.Bold);
                Font fontTable = new Font("Arail", 11, FontStyle.Bold);
                Font fontSubHeader = new Font("Arail", 9, FontStyle.Bold);
                Font fontBody = new Font("Arail", 8, FontStyle.Bold);
                Font fontBodylist = new Font("Arail", 8, FontStyle.Bold);
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
                    e.Graphics.DrawString("TAX RD : " + appTaxRD, fontBody, brush, x + 40, y);

                    y += 12;
                }


                if (restTel.Trim().Length > 0)
                {
                    e.Graphics.DrawString(restTel, fontBody, brush, x + 60, y);

                    y += 12;

                }

                y += 20;

                e.Graphics.DrawString("สรุปรายการขายทั้งหมด ", fontSubHeader, brush, x + 45, y);
                y += 20;

                e.Graphics.DrawString("Data Date : " + textBoxFromDate.Text + " - " + textBoxToDate.Text, fontSubHeader, brush, x + 10, y);
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

                foreach (SalesToday st in salesFromTo)
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
                    e.Graphics.DrawString(txtOrder, fontBodylist, brush, x + 0, y);
                    e.Graphics.DrawString(txtAmt, fontNum, brush, x + 190, y);

                    

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
