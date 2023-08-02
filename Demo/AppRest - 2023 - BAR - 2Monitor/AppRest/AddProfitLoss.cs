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
    public partial class AddProfitLoss : AddDataTemplate
    {
        GetDataRest gd; 
         

        string printerCashName;

        string plDate = ""; 
        float plAmount = 0;
        string plDesc = "";
        string plRem = "";
        string plUseBy = "";

        List<Member> allMems;
         

        double servicePercent;
        double taxPercent;


        public AddProfitLoss(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();

            this.Width = 1024;
            this.Height = 764;

         


            printerCashName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();
            printCashDrawer.PrinterSettings.PrinterName = this.printerCashName;
            printCashEndBill.PrinterSettings.PrinterName = this.printerCashName;
            printDocument3.PrinterSettings.PrinterName = this.printerCashName;

            gd = new GetDataRest();

            allMems = gd.getListAllMember(); 
            getComboAllMem(); 
            defaultFunc(); 

            this.plDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");


            getDataTablePL();

            getComboPLIncome();
            getComboPLExpense();
        

            //taxPercent = Double.Parse(ConfigurationSettings.AppSettings["TaxPercent"].ToString());
            //servicePercent = Double.Parse(ConfigurationSettings.AppSettings["ServiceChargePercent"].ToString());

            //getDataTablePL();

            // Tab 2 
            getComboTitle();
            defaultTab2();

        }


        private void defaultFunc()
        {
            // Before 6 Am is Today
            dateTimePickerStartDate.Value = DateTime.Now.AddHours(-6);
     

            txtBoxOutAmt.Text = "0";
            comboBoxOutUseBy.Text = "Select Member";
            comboBoxAccExDesc.Text = "รายละเอียดรายจ่าย";
            textBoxOutRem.Text = "เหตุผล";


            txtBoxInAmt.Text = "0";
            comboBoxInUseBy.Text = "Select Member";
            comboBoxAccInDesc.Text = "รายละเอียดรายรับ";
            textBoxInRem.Text = "เหตุผล";
                
        }

        List<Account> listAcc;

        int accPLINSelected;
        int accPLEXSelected;

        private void getComboPLIncome()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            
            listAcc = gd.getPL_AllAccName(1);

            foreach (Account c in listAcc)
            {
                data.Add(new KeyValuePair<int, string>(c.AccountNo, c.AccountName));
            }


            // Clear the combobox
            comboBoxAccIn.DataSource = null;
            comboBoxAccIn.Items.Clear();

            // Bind the combobox
            comboBoxAccIn.DataSource = new BindingSource(data, null);
            comboBoxAccIn.DisplayMember = "Value";
            comboBoxAccIn.ValueMember = "Key";

            comboBoxAccIn.SelectedIndex = 0;

            getComboPLIncomeDesc();

        }

        private void getComboPLIncomeDesc()
        {
            try
            {

                List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

                // Add data to the List

                accPLINSelected = Int32.Parse(comboBoxAccIn.SelectedValue.ToString());

                listAcc = gd.getPL_AllAccDesc(accPLINSelected);

                foreach (Account c in listAcc)
                {
                    data.Add(new KeyValuePair<int, string>(c.AccountNo, c.AccountName));
                }


                // Clear the combobox
                comboBoxAccInDesc.DataSource = null;
                comboBoxAccInDesc.Items.Clear();

                // Bind the combobox
                comboBoxAccInDesc.DataSource = new BindingSource(data, null);
                comboBoxAccInDesc.DisplayMember = "Value";
                comboBoxAccInDesc.ValueMember = "Key";

                comboBoxAccInDesc.Text = "รายละเอียดรายรับ";

            }
            catch (Exception ex)
            { 
            }

        }


        private void getComboPLExpense()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            listAcc = gd.getPL_AllAccName(-1);

            foreach (Account c in listAcc)
            {
                data.Add(new KeyValuePair<int, string>(c.AccountNo, c.AccountName));
            }


            // Clear the combobox
            comboBoxAccEx.DataSource = null;
            comboBoxAccEx.Items.Clear();

            // Bind the combobox
            comboBoxAccEx.DataSource = new BindingSource(data, null);
            comboBoxAccEx.DisplayMember = "Value";
            comboBoxAccEx.ValueMember = "Key";

            comboBoxAccEx.SelectedIndex = 0;

            getComboPLExpenseDesc();

        }

        private void getComboPLExpenseDesc()
        {
            try
            {

                List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

                // Add data to the List

                accPLEXSelected = Int32.Parse(comboBoxAccEx.SelectedValue.ToString());

                listAcc = gd.getPL_AllAccDesc(accPLEXSelected);

                foreach (Account c in listAcc)
                {
                    data.Add(new KeyValuePair<int, string>(c.AccountNo, c.AccountName));
                }


                // Clear the combobox
                comboBoxAccExDesc.DataSource = null;
                comboBoxAccExDesc.Items.Clear();

                // Bind the combobox
                comboBoxAccExDesc.DataSource = new BindingSource(data, null);
                comboBoxAccExDesc.DisplayMember = "Value";
                comboBoxAccExDesc.ValueMember = "Key";

                comboBoxAccExDesc.Text = "รายละเอียดรายจ่าย";


            }
            catch (Exception ex)
            {
            }

        }




        private void comboBoxAccIn_SelectedValueChanged(object sender, EventArgs e)
        {
            getComboPLIncomeDesc();
        }


        private void comboBoxAccEx_SelectedIndexChanged(object sender, EventArgs e)
        {
            getComboPLExpenseDesc();
        }

        private void getComboAllMem()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, ""));

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
          
      
 
         

        private void buttonInSAVE_Click(object sender, EventArgs e)
        {
            try
            {

                this.plAmount = float.Parse(txtBoxInAmt.Text);

                if (this.plAmount > 0)
                {
                    this.plDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
                    this.plDesc = comboBoxAccInDesc.Text;
                    this.plRem = textBoxInRem.Text;
                    this.plUseBy = comboBoxInUseBy.Text; 

                    int result = gd.instNewPLTransaction(this.plDate, this.accPLINSelected, this.plDesc, this.plRem + "/" + this.plUseBy, this.plAmount);


                    if (result <= 0)
                        throw new Exception("ไม่สามารถบันทึกข้อมูลได้ โปรดตรวจสอบอีกครั้ง");
                    else 
                        MessageBox.Show("บันทึกรายรับ + สำเร็จ !!");

                    defaultFunc();
                    getDataTablePL();

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

                this.plAmount = float.Parse(txtBoxOutAmt.Text);

                if (this.plAmount > 0)
                {

                    this.plDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
                    
                    this.plDesc = comboBoxAccExDesc.Text;
                    this.plRem = textBoxOutRem.Text;
                    this.plUseBy = comboBoxOutUseBy.Text;

                    int result = gd.instNewPLTransaction(this.plDate, this.accPLEXSelected, this.plDesc, this.plRem + "/" + this.plUseBy, this.plAmount);


                    if (result <= 0)
                        throw new Exception("ไม่สามารถบันทึกข้อมูลได้ โปรดตรวจสอบอีกครั้ง");
                    else
                        MessageBox.Show("บันทึกรายจ่าย - สำเร็จ !!");

                    defaultFunc();
                    getDataTablePL();

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
           


                //y += 10;

                e.Graphics.DrawString("[[ บันทึกรายการ ]]", fontTable, brush, x + 5, y);

                y += 30;

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd/MM/yyyy}", dt);
                string strTime = String.Format("{0:HH:mm:ss}", dt);

                e.Graphics.DrawString("Date " + strDate + "   Time  " + strTime, fontSubHeader, brush, x + 5, y);

                y += 15;

                e.Graphics.DrawString("รายการ : " + this.plDesc , fontSubHeader, brush, x + 5, y);

                y += 15;

                e.Graphics.DrawString("ยอดเงิน : " + this.plAmount.ToString("###,###.##") + " THB.", fontSubHeader, brush, x + 5, y);

                y += 15;

                e.Graphics.DrawString("เหตุผล : " + this.plRem, fontSubHeader, brush, x + 5, y);

                y += 15;

                e.Graphics.DrawString("ผู้รับเงิน : " + this.plUseBy + "   Sign ................. ", fontSubHeader, brush, x + 5, y);

                y += 15;

                e.Graphics.DrawString("ทำรายการโดย : " + this.plRem + "   Sign ................ ", fontSubHeader, brush, x + 5, y);

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
            comboBoxAccInDesc.Text = "รายละเอียดรายรับ";
            textBoxInRem.Text = "เหตุผล";

        }

        private void buttonOutClLEAR_Click(object sender, EventArgs e)
        {
            txtBoxOutAmt.Text = "0";
            comboBoxOutUseBy.Text = "Select Member";
            comboBoxAccExDesc.Text = "รายละเอียดรายรับ";
            textBoxOutRem.Text = "เหตุผล";
        }


        private void getDataTablePL()
        {

            try
            {

                DataTable dtIncome = gd.getPL_PLTransaction(this.plDate, "0", 1);
                dataGridViewIncome.DataSource = dtIncome;

                if (dataGridViewIncome.RowCount > 0)
                { 
                    dataGridViewIncome.Columns[0].Visible = false;
                    dataGridViewIncome.Columns[1].Visible = false;
                    dataGridViewIncome.Columns[2].Visible = false;
                }


                DataTable dtOutcome = gd.getPL_PLTransaction(this.plDate, "0", -1);
                dataGridViewOutCome.DataSource = dtOutcome;

                if (dataGridViewOutCome.RowCount > 0)
                {
                    dataGridViewOutCome.Columns[0].Visible = false;
                    dataGridViewOutCome.Columns[1].Visible = false;
                    dataGridViewOutCome.Columns[2].Visible = false;
                }
            
 


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
 
        }

      

        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.plDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
          
                getDataTablePL();
            }
            catch (Exception ex)
            {


            }
        }
        ///
        // Tab 2 




        string pathExportResultName;

        string fromDate = "";
        string toDate = "";
        int dataSelectID = 0;
        DataTable resultReport;
        TrnMax trnMax;
        CultureInfo ci = new CultureInfo("en-EN");

        private void defaultTab2()
        {

            try
            {
                // trnMax = gd.getTrnMax();


                var todayDate = DateTime.Now.ToString("dd/MM/yyyy", ci);


                textBoxFromDate.Text = todayDate;
                textBoxToDate.Text = todayDate;


                getComboTitle();
                getResultDatatable();
            }
            catch (Exception ex)
            {

            }
        }
          

        private void getComboTitle()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List



            data.Add(new KeyValuePair<int, string>(1, "สรุปกำไร/ขาดทุน "));
            data.Add(new KeyValuePair<int, string>(2, "สรุปกำไร/ขาดทุน - Detail")); 
            data.Add(new KeyValuePair<int, string>(3, "สรุปบันทึกรายรับ (อื่นๆ)/รายจ่าย"));


            // Clear the combobox
            comboBoxTitle.DataSource = null;
            comboBoxTitle.Items.Clear();

            // Bind the combobox
            comboBoxTitle.DataSource = new BindingSource(data, null);
            comboBoxTitle.DisplayMember = "Value";
            comboBoxTitle.ValueMember = "Key";

            comboBoxTitle.SelectedIndex = 0;

        }


        private void getResultDatatable()
        {
            try
            {

                fromDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxFromDate.Text);
                toDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxToDate.Text); 

                dataSelectID = Int32.Parse(comboBoxTitle.SelectedValue.ToString());

                if (dataSelectID == 1)
                    this.resultReport = gd.getPL_Sum_PLSummary(fromDate, toDate);
                if (dataSelectID == 2)
                    this.resultReport = gd.getPL_Sum_PLSummary_Detail(fromDate, toDate);
                if (dataSelectID == 3)
                    this.resultReport = gd.getPL_Sum_PLTransaction(fromDate, toDate); 

                dataGridViewResult.DataSource = null;
                dataGridViewResult.DataSource = this.resultReport;

            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }

        private void KeyPassFinish(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                getResultDatatable();
            }
        }

        private void comboBoxTitle_SelectedValueChanged(object sender, EventArgs e)
        {
            getResultDatatable();
        }

        private void buttonVewStockReport_Click(object sender, EventArgs e)
        {
            getResultDatatable();
        }

        private void buttonExportData_Click(object sender, EventArgs e)
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

        private void textBoxSearchInv_TextChanged(object sender, EventArgs e)
        {
            searchReport();
        }

        private void searchReport()
        {
            try
            {

                string srPName = textBoxSearchInv.Text;



                if ((srPName.Length > 0))
                {
                    this.resultReport.DefaultView.RowFilter = string.Format("AccountNo like '*{0}*' or  AccountName like '*{0}*' ", srPName);
                }
                else
                {
                    this.resultReport.DefaultView.RowFilter = string.Format("1 = 1", 1);  // All
                }


            }
            catch (Exception ex)
            {

            }

        }
     
        
    }
}
