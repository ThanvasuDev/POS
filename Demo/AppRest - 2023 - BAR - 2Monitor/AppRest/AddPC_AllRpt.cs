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


namespace AppRest
{
    public partial class AddPC_AllRpt : AddDataTemplate
    {
        DataTable result;
        GetDataRest gd;

        TrnMax trnMax;

        string pathExportResultStockName;
        string pathExportResultName;

        DateTime fromDate;
        DateTime toDate;
        int status;
        int supplierID;

        List<Supplier> allSupplier;

        int dataSelectID = 0;

        public AddPC_AllRpt(Form frmlkFrom, int flagFrmClose)
        {
              InitializeComponent();
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }

            this.Text = this.Text + " ( By : " + Login.userName + ")";

                 if (Login.isFrontPOS)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }

            this.Width = 1024;
            this.Height = 768;


            try
            {

                result = new DataTable();
                gd = new GetDataRest();

                dateTimePickerStartDate.Value = DateTime.Now;
                dateTimePickerEnd.Value = DateTime.Now;

                allSupplier = gd.getPC_Supplier(0, "0", "0");
                getComboAllSuplier();
                getComboPaperStatus();
                getComboTitle();
                getResultDatatable();

                pathExportResultStockName = ConfigurationSettings.AppSettings["PathExportResultStockName"].ToString();
                pathExportResultName = ConfigurationSettings.AppSettings["PathExportResultName"].ToString();
            }
            catch (Exception ex)
            {

            }
       
        }

        private void getComboTitle()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List


            data.Add(new KeyValuePair<int, string>(1, "สรุปรายการ PR (ใบขอซื้อ)"));
            data.Add(new KeyValuePair<int, string>(2, "สรุปรายละเอียด PR (ใบขอซื้อ)"));
            data.Add(new KeyValuePair<int, string>(3, "สรุปรายการ PO (ใบสั่งซื้อ)"));
            data.Add(new KeyValuePair<int, string>(4, "สรุปรายละเอียด PO (ใบสั่งซื้อ)"));
            data.Add(new KeyValuePair<int, string>(5, "สรุปรายการ GR (ใบรับสินค้า)"));
            data.Add(new KeyValuePair<int, string>(6, "สรุปรายละเอียด GR (ใบรับสินค้า)"));

            // Clear the combobox
            comboBoxTitle.DataSource = null;
            comboBoxTitle.Items.Clear();

            // Bind the combobox
            comboBoxTitle.DataSource = new BindingSource(data, null);
            comboBoxTitle.DisplayMember = "Value";
            comboBoxTitle.ValueMember = "Key";

            comboBoxTitle.SelectedIndex = 0;


        }


        private void getComboPaperStatus()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "ทุก Status")); 
            data.Add(new KeyValuePair<int, string>(1, "รอการอนุมัติ"));
            data.Add(new KeyValuePair<int, string>(2, "ไม่อนุมัติ"));
            data.Add(new KeyValuePair<int, string>(3, "อนุมัติแล้ว"));
         

            // Clear the combobox
            comboBoxPaperStatus.DataSource = null;
            comboBoxPaperStatus.Items.Clear();

            // Bind the combobox
            comboBoxPaperStatus.DataSource = new BindingSource(data, null);
            comboBoxPaperStatus.DisplayMember = "Value";
            comboBoxPaperStatus.ValueMember = "Key";

            comboBoxPaperStatus.SelectedIndex = 0;


        }

        private void getComboAllSuplier()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List
            data.Add(new KeyValuePair<int, string>(0, "= เลือก Supplier ="));

            foreach (Supplier c in allSupplier)
            {
                if (c.FlagUse == "Y")
                {
                    data.Add(new KeyValuePair<int, string>(c.SupplierID, c.SupplierName));
                }

            }
            // Clear the combobox
            comboBoxSupplier.DataSource = null;
            comboBoxSupplier.Items.Clear();

            // Bind the combobox
            comboBoxSupplier.DataSource = new BindingSource(data, null);
            comboBoxSupplier.DisplayMember = "Value";
            comboBoxSupplier.ValueMember = "Key";

            //

        }


        private void getResultDatatable()
        {
            try
            {

                fromDate = dateTimePickerStartDate.Value;
                toDate = dateTimePickerEnd.Value;
                status = Int32.Parse(comboBoxPaperStatus.SelectedValue.ToString());
                supplierID = Int32.Parse(comboBoxSupplier.SelectedValue.ToString());

                dataSelectID = Int32.Parse(comboBoxTitle.SelectedValue.ToString()); 
              
                if (dataSelectID == 1)
                    this.result = gd.getPC_Rpt_PRHeader(fromDate, toDate, status, supplierID);
                else if (dataSelectID == 2)
                    this.result = gd.getPC_Rpt_PRDetail(fromDate, toDate, status, supplierID);
                else if (dataSelectID == 3)
                   this.result = gd.getPC_Rpt_POHeader(fromDate, toDate, status, supplierID);
                else if (dataSelectID == 4)
                    this.result = gd.getPC_Rpt_PODetail(fromDate, toDate, status, supplierID);
                else if (dataSelectID == 5)
                   this.result = gd.getPC_Rpt_GRHeader(fromDate, toDate, status, supplierID);
                else if (dataSelectID == 6)
                    this.result = gd.getPC_Rpt_GRDetail(fromDate, toDate, status, supplierID);

                dataGridViewResult.DataSource = this.result;


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
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

            //try
            //{

            //    fromDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxFromDate.Text);
            //    toDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxToDate.Text);

            //    dataSelectID = comboBoxTitle.SelectedIndex;

                
            //    LinkFromRptBillReport();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        FromRptSumReport formFromRptSumReport;

        //private void LinkFromRptBillReport()
        //{
        //    Cursor.Current = Cursors.WaitCursor;
        //    if (formFromRptSumReport == null)
        //    {
        //        formFromRptSumReport = new FromRptSumReport(this, 0, fromDate, toDate, dataSelectID);
        //    }
        //    else
        //    {
        //        formFromRptSumReport.rptFromDate = fromDate;
        //        formFromRptSumReport.rptToDate = toDate;
        //        formFromRptSumReport.rptType = dataSelectID;
        //        formFromRptSumReport.viewReport();
        //    }
        //    Cursor.Current = Cursors.Default;
        //    if (formFromRptSumReport.ShowDialog() == DialogResult.OK)
        //    {
        //        formFromRptSumReport.Dispose();
        //        formFromRptSumReport = null;
        //    }
        //}

        private void textBoxSrcStoreName_TextChanged(object sender, EventArgs e)
        {

            try
            {

                string srPName = textBoxSrcStoreName.Text;


                if (srPName.Length > 0)
                {
                    this.result.DefaultView.RowFilter = string.Format("InvName like '*{0}*' ", srPName);

                }
                else
                {
                    this.result.DefaultView.RowFilter = string.Format(" 1 = 1 ", srPName);
                }

            }
            catch (Exception ex)
            {

            }


        } 

        private void radioBoxThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Value = DateTimeDayOfMonthExtensions.FirstDayOfMonth_AddMethod(DateTime.Now);
            dateTimePickerEnd.Value = DateTime.Now;
        }

        private void radioBoxTotal_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Value = DateTime.Now;
            dateTimePickerEnd.Value = DateTime.Now;
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            getResultDatatable();
        }

        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            getResultDatatable();
        }

        private void comboBoxSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            getResultDatatable();
        }

        private void comboBoxPaperStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            getResultDatatable();
        }

    }
}
