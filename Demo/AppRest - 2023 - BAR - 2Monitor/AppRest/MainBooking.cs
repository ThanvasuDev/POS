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
    public partial class MainBooking : MainTemplateS
    {
         
        GetDataRest gd;
          
        string pathExportResultStockName;
        string pathExportResultName;

        MemCard mc;
        string memCardID;

        List<Tbl> allTableByZone;
        List<Zone> allZone;

        public MainBooking(Form frmlkFrom, int flagFrmClose)
        {
            InitializeComponent();
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose(); 
 
            } 

              
            this.Text = this.Text + " ( By : " + Login.userName + ")";

            buttonLinkCashCard.BackColor = System.Drawing.Color.Gray;
            InitializeComponent();


            if (Login.flagLogoSQ.ToLower() == "y")
            {
                panellogo.Width = 100;
                panellogo.Height = 100;
            }
            else
            {
                panellogo.Width = 200;
                panellogo.Height = 100;
                panellogo.Location = new Point(730 - 50, 10);
            }


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

            getComboTitle();
            getResultDatatable();
             

            printCashCardBalance.PrinterSettings.PrinterName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();
             

            pathExportResultStockName = ConfigurationSettings.AppSettings["PathExportResultStockName"].ToString();
            pathExportResultName = ConfigurationSettings.AppSettings["PathExportResultName"].ToString();

            dateTimePickerFromDate.Value = DateTime.Now;
            dateTimePickerToDate.Value = DateTime.Now;

            allZone = gd.getAllZone(); 
            genObjMainZone();

            allTableByZone = gd.getTableByZone(Login.zoneDefaultID);
            getComboAllTable();

            genBookingDisplay();
            DefaultBookAdd();

         
        }


        private void genObjMainZone()
        {
            try
            {

                //   gentxtTopMenu(); 
                panelZone.Controls.Clear();

                int zoneID = 0;
                string zoneName = "";
                string zoneColour = "";

                Button bTable;

                int sizeX = 90;
                int sizeY = 70;

                int yy = 1;

                int i = 0;
                int j = 0;
                int k = 0;

                // All

                bTable = new Button();

                //    bTable.Cursor = System.Windows.Forms.Cursors.Default;
                bTable.FlatAppearance.BorderColor = System.Drawing.Color.White;
                bTable.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));

                bTable.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + ((sizeY + 1) * (i / yy)));
                bTable.Name = "0";

                bTable.Size = new System.Drawing.Size(sizeX, sizeY);
                bTable.TabIndex = 0;
                bTable.Text = "All";
                bTable.UseVisualStyleBackColor = false;
                bTable.Click += new System.EventHandler(this.bZone_Click);

                panelZone.Controls.Add(bTable);

                i++;


                foreach (Zone t in allZone)
                {

                    zoneID = t.ZoneID;
                    zoneName = t.ZoneName;

                    if (t.ZoneDesc.Contains('|'))
                    {

                        string[] zSTR = t.ZoneDesc.Split('|');
                        zoneColour = zSTR[1];

                    }


                    bTable = new Button();


                    if (zoneColour == "C1")
                    {
                        bTable.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        bTable.BackColor = System.Drawing.Color.Silver;
                    }


                    bTable.ForeColor = System.Drawing.Color.Black;

                    //    bTable.Cursor = System.Windows.Forms.Cursors.Default;
                    bTable.FlatAppearance.BorderColor = System.Drawing.Color.White;
                    bTable.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));

                    bTable.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + ((sizeY + 1) * (i / yy)));
                    bTable.Name = zoneID.ToString();

                    bTable.Size = new System.Drawing.Size(sizeX, sizeY);
                    bTable.TabIndex = zoneID;
                    bTable.Text = zoneName;
                    bTable.UseVisualStyleBackColor = false;
                    bTable.Click += new System.EventHandler(this.bZone_Click);

                    panelZone.Controls.Add(bTable);

                    i++;
                }

            }
            catch (Exception ex)
            {

                // MessageBox.Show(ex.Message);
            }

        }

        private void bZone_Click(object sender, EventArgs e)
        {
            try
            {
                Button bClick = (Button)sender;

                Login.zoneDefaultID = Int32.Parse(bClick.Name);

                allTableByZone = gd.getTableByZone(Login.zoneDefaultID);
                getComboAllTable(); 

            }
            catch (Exception ex)
            {

            }
        }

        DataTable allBooking;

        private void genBookingDisplay()
        {
            try
            {

                int bookID = 0;
                DateTime fromDate = dateTimePickerFromDate.Value;
                DateTime toDate = dateTimePickerToDate.Value;

                int zoneID = 0;
                int tableID = 0;

                int confirm = 0;
                int active = 0;

                if (checkBoxActive.Checked)
                    active = 1;
                else
                    active = 0;


                if (checkBoxConfirm.Checked)
                    confirm = 1;
                else
                    confirm = 0;



                allBooking = gd.getBook_AllBooking(bookID, fromDate, toDate, confirm, active, zoneID, tableID);
                dataGridViewBooking.DataSource = allBooking;


                dataGridViewBooking.Columns[0].Visible = false;
                //dataGridViewBooking.Columns[2].Visible = false;
                dataGridViewBooking.Columns[4].Visible = false;
                dataGridViewBooking.Columns[5].Visible = false;
                dataGridViewBooking.Columns[6].Visible = false;


            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

        }

        private void getComboAllTable()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "== โต๊ะ / ห้อง =="));

            foreach (Tbl c in allTableByZone)
            {
                data.Add(new KeyValuePair<int, string>(c.TblID, c.TblName));
            }


            // Clear the combobox
            comboBoxAllTable.DataSource = null;
            comboBoxAllTable.Items.Clear();

            // Bind the combobox
            comboBoxAllTable.DataSource = new BindingSource(data, null);
            comboBoxAllTable.DisplayMember = "Value";
            comboBoxAllTable.ValueMember = "Key";

        }


        private void buttonSearchMem_Click(object sender, EventArgs e)
        {
            genBookingDisplay();
        }

        private void buttonOpenBookPanel_Click(object sender, EventArgs e)
        {
            PanelEditBook.Visible = true;
        }

        private void buttonCloseBookPanel_Click(object sender, EventArgs e)
        {
            PanelEditBook.Visible = false;
        }

        private void buttonClearBooking_Click(object sender, EventArgs e)
        {
            DefaultBookAdd();
        }

        private void DefaultBookAdd()
        {
            textBoxBookID.Text = "";
            dateTimePickerFromDate_ADD.Value = DateTime.Now;
            dateTimePickerToDate_ADD.Value = DateTime.Now;
            comboBoxBookDay.Text = "1";
            comboBoxH1.Text = "00";
            comboBoxM1.Text = "00";
            comboBoxH2.Text = "00";
            comboBoxM2.Text = "00";
            textBoxSrcMem.Text = "";
            textBoxCustName.Text = "";
            textBoxBookTel.Text = "";
            textBoxBookDesc.Text = "";

            comboBoxAllTable.SelectedIndex = 0;
            checkBoxConfirm_ADD.Checked = false;
            checkBoxActive_ADD.Checked = true;

        }

        private void buttonAddBooking_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dtFromDate = dateTimePickerFromDate_ADD.Value;
                DateTime dtToDate = dateTimePickerToDate_ADD.Value;
                int noOfDays = Int32.Parse(comboBoxBookDay.Text);
                string time = comboBoxH1.Text + ":" + comboBoxM1.Text + "-" + comboBoxH2.Text + ":" + comboBoxM2.Text;

                int custID = 0;
                string memCardID = "0";

                if (radioBoxSelCust.Checked)
                {
                    if (FuncString.IsNumeric(textBoxSrcMem.Text))
                        custID = Int32.Parse(textBoxSrcMem.Text);
                }
                else
                {
                    memCardID = textBoxSrcMem.Text;
                }

                string bookName = textBoxCustName.Text;
                string bookTel = textBoxBookTel.Text;
                string bookDesc = textBoxBookDesc.Text;

                int confirm = 0;
                int active = 0;

                if (checkBoxActive_ADD.Checked)
                    active = 1;
                else
                    active = 0;


                if (checkBoxConfirm_ADD.Checked)
                    confirm = 1;
                else
                    confirm = 0;

                int tableID = Int32.Parse(comboBoxAllTable.SelectedValue.ToString());

                float depositAmt = float.Parse(textBoxDeposit.Text);


                if (bookName.Length == 0)
                    throw new Exception("กรุณาระบุชื่อผู้จอง");

                String bookDescription = bookName + " > " + dtFromDate.ToString("DD/MM/YYYY") + " เวลา " + time;


                if (radioButtonAddData.Checked == true)
                {

                    if (MessageBox.Show("คุณต้องการทำรายการจอง ชื่อ " + bookDescription + " หรือไม่ ?", "ทำรายการจอง", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.insertBooking(dtFromDate, dtToDate, time, memCardID, custID, tableID, bookName, bookTel, bookDesc, depositAmt);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Booking : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่ม : ชื่อ " + bookDescription + " >> (Success)");

                            genBookingDisplay();
                            DefaultBookAdd();

                        }
                    }
                }
                else
                {

                    int bookID = Int32.Parse(textBoxBookID.Text);



                    if (MessageBox.Show("คุณต้องการแก้ไขรายการจอง ชื่อ " + bookDescription + " หรือไม่ ?", "แก้ไขรายการ ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updsBooking(bookID, dtFromDate, dtToDate, time, memCardID, custID, tableID, bookName, bookTel, bookDesc, depositAmt, confirm, active);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Booking : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่ม : ชื่อ " + bookDescription + " >> (Success)");

                            genBookingDisplay();
                            DefaultBookAdd();

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButtonAddData_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked)
            {
                labelBookRefID.Visible = false;
                textBoxBookID.Visible = false;
            }
            else
            {
                labelBookRefID.Visible = true;
                textBoxBookID.Visible = true;
            }
        }

        private void dataGridViewBooking_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                PanelEditBook.Visible = true;
                radioButtonUpdateData.Checked = true;


                textBoxBookID.Text = dataGridViewBooking.Rows[e.RowIndex].Cells["BookID"].Value.ToString();
                dateTimePickerFromDate_ADD.Value = DateTime.Parse(dataGridViewBooking.Rows[e.RowIndex].Cells["BookDate"].Value.ToString());
                dateTimePickerToDate_ADD.Value = DateTime.Parse(dataGridViewBooking.Rows[e.RowIndex].Cells["BookToDate"].Value.ToString());
                comboBoxBookDay.Text = "1";

                string booktime = dataGridViewBooking.Rows[e.RowIndex].Cells["BookTime"].Value.ToString();

                string[] booktimes = booktime.Split('-');

                string[] t1 = booktimes[0].Split(':');
                string[] t2 = booktimes[1].Split(':');

                comboBoxH1.Text = t1[0];
                comboBoxM1.Text = t1[1];
                comboBoxH2.Text = t2[0];
                comboBoxM2.Text = t2[1];
                textBoxSrcMem.Text = "";
                textBoxCustName.Text = dataGridViewBooking.Rows[e.RowIndex].Cells["BookName"].Value.ToString();
                textBoxBookTel.Text = dataGridViewBooking.Rows[e.RowIndex].Cells["BookTel"].Value.ToString();
                textBoxBookDesc.Text = dataGridViewBooking.Rows[e.RowIndex].Cells["BookDesc"].Value.ToString();
                textBoxDeposit.Text = dataGridViewBooking.Rows[e.RowIndex].Cells["BookDeposit"].Value.ToString();

                comboBoxAllTable.SelectedValue = Int32.Parse(dataGridViewBooking.Rows[e.RowIndex].Cells["BookTableID"].Value.ToString());

                checkBoxConfirm_ADD.Checked = false;
                if (Int32.Parse(dataGridViewBooking.Rows[e.RowIndex].Cells["BookConfirm"].Value.ToString()) == 1)
                    checkBoxConfirm_ADD.Checked = true;

                checkBoxActive_ADD.Checked = false;
                if (Int32.Parse(dataGridViewBooking.Rows[e.RowIndex].Cells["BookActive"].Value.ToString()) == 1)
                    checkBoxActive_ADD.Checked = true;

                
            }
            catch (Exception ex)
            {

            }
        }

        private void comboBoxBookDay_SelectedValueChanged(object sender, EventArgs e)
        {

            try
            {
                int dayNo = Int32.Parse(comboBoxBookDay.Text);

                dateTimePickerToDate_ADD.Value = dateTimePickerFromDate_ADD.Value.AddDays(dayNo);

            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxSRMemName_TextChanged(object sender, EventArgs e)
        {
            searchCust();
        }

        private void searchCust()
        {

            try
            {

                string srMemName = textBoxSRMemName.Text;

                if (srMemName.Length > 0)
                {
                    allBooking.DefaultView.RowFilter = string.Format("BookName like '*{0}*' ", srMemName);

                }
                else
                {
                    this.allBooking.DefaultView.RowFilter = string.Format(" 1 = 1 ");

                }
                 

                //string srMemName = textBoxSRMemName.Text;
                //string strSearchMemCard = textBoxStrSearchMemCardtoTable.Text;
                //string srMemTel = textBoxSRTel.Text;
                //string srLevelName = comboBoxSRLevel.Text;

                //if (comboBoxSRLevel.SelectedIndex == 0)
                //{

                //    if (srMemName.Length > 0)
                //    {
                //        dataTableAllCust.DefaultView.RowFilter = string.Format("Name like '*{0}*' ", srMemName);

                //    }
                //    else
                //    {
                //        if (strSearchMemCard.Length > 0)
                //            this.dataTableAllCust.DefaultView.RowFilter = string.Format("TaxID = '{0}' ", strSearchMemCard);
                //        else if (srMemTel.Length > 0)
                //            this.dataTableAllCust.DefaultView.RowFilter = string.Format("Tel = '{0}' ", srMemTel);
                //        else
                //            this.dataTableAllCust.DefaultView.RowFilter = string.Format(" 1 = 1 ");

                //    }
                //}
                //else
                //{
                //    if (srMemName.Length > 0)
                //    {
                //        dataTableAllCust.DefaultView.RowFilter = string.Format("Name like '*{0}*' and Status =  '{1}' ", srMemName, srLevelName);

                //    }
                //    else
                //    {
                //        if (strSearchMemCard.Length > 0)
                //            this.dataTableAllCust.DefaultView.RowFilter = string.Format("TaxID = '{0}'  and Status =  '{1}' ", strSearchMemCard, srLevelName);
                //        else if (srMemTel.Length > 0)
                //            this.dataTableAllCust.DefaultView.RowFilter = string.Format("Tel = '{0}'  and Status =  '{1}' ", srMemTel, srLevelName);
                //        else
                //            this.dataTableAllCust.DefaultView.RowFilter = string.Format(" Status =  '{0}' ", srLevelName);

                //    }

                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBoxConfirm_CheckedChanged(object sender, EventArgs e)
        {
            genBookingDisplay();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportData.Excel_FromDataTable_DT(dataGridViewBooking);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxSrcMem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                searchMemCard();

            }
        }

 

        private void searchMemCard()
        {



            string strSearchMemCard = textBoxSrcMem.Text;

            //  clearMemCard();



            try
            {

                if (strSearchMemCard.Length == 0)
                    throw new Exception("กรูณาป้อน รหัสสมาชิก / เบอร์โทร ก่อนค่ะ");

                mc = gd.SelMemCard_Search(strSearchMemCard, "Y");
                string pathPH = "";

                if (mc == null)
                    throw new Exception("ไม่พบเลขสมาชิก / เบอร์โทรนี้ / ยังไม่ได้สมัครสมาชิก ค่ะ");

                this.memCardID = mc.MemCardID;

                //textBoxMemCardID.Text = mc.MemCardID;
                //textBoxMemCardName.Text = mc.MemCardName;
                //textBoxMemCardTel.Text = mc.Tel;
                //textBoxRFIDCode.Text = mc.MemRFIDCode;
                //textBoxBarcodeRegister.Text = mc.MemRFIDCode;
                 

                // if Fitness รายเดือน 

                //string memDesc = "";
                //string memDescFull = "";


                if (this.memCardID.Length > 1)
                {

                    textBoxCustName.Text = mc.MemCardName;
                    textBoxBookTel.Text = mc.Tel;
                }
                else
                {
                    throw new Exception("ไม่พบสมาชิก !!");
                }
                textBoxSrcMem.Text = "";
                textBoxSrcMem.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBoxSrcMem.Text = "";
                textBoxSrcMem.Focus();

                // pictureBoxShow.Image = global::AppRest.Properties.Resources.Logo_New;
            }
        }

        private void buttonOrderBooking_Click(object sender, EventArgs e)
        {
            int tableID = 1;
            int productID = 5;
              
            float depositAmt =  float.Parse(textBoxDeposit.Text);
            Login.orderType = 2;

            int result = gd.instOrderByTable(tableID, productID, Login.userID, "+เงินมัด ", depositAmt);
            this.LinkFormOrderTable(tableID, 0);
        }

        OrderTable  formLinkOrderTable;

        private void LinkFormOrderTable(int tableID, int tableCustID)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formLinkOrderTable == null)
            {
                formLinkOrderTable = new OrderTable(this, 0, tableID);
            }
            Cursor.Current = Cursors.Default;
            if (formLinkOrderTable.ShowDialog() == DialogResult.OK)
            {
                formLinkOrderTable.Dispose();
                formLinkOrderTable = null;
            }
        }

    

        private void buttonPrintA4_Click(object sender, EventArgs e)
        {

           // Button printButton = (Button)sender;


            int bookID = 0;
            string fromDate = dateTimePickerFromDate.Value.ToString("yyyyMMdd");
            string toDate = dateTimePickerToDate.Value.ToString("yyyyMMdd");

            int zoneID = 0;
            int tableID = 0;

            int confirm = 0;
            int active = 0;

            if (checkBoxActive.Checked)
                active = 1;
            else
                active = 0;


            if (checkBoxConfirm.Checked)
                confirm = 1;
            else
                confirm = 0;

            try
            { 

                // MessageBox.Show(type.ToString());
                LinkFromRptBillReport(bookID, fromDate, toDate, confirm, active, zoneID, tableID);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        FromRptBookReport formRptBookReport;

        private void LinkFromRptBillReport(int bookID, string fromDate, string toDate, int flagConfirm, int flagActive, int zoneID, int tableID)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formRptBookReport == null)
            {
                formRptBookReport = new FromRptBookReport(this, 0, bookID, fromDate, toDate, flagConfirm, flagActive, zoneID, tableID);
            }
            else
            {
                formRptBookReport.bookID = bookID;
                formRptBookReport.fromDate = fromDate;
                formRptBookReport.toDate = toDate;
                formRptBookReport.flagConfirm = flagConfirm;
                formRptBookReport.flagActive = flagActive;
                formRptBookReport.zoneID = zoneID;
                formRptBookReport.tableID = tableID;


                formRptBookReport.viewReport();
            }
            Cursor.Current = Cursors.Default;
            if (formRptBookReport.ShowDialog() == DialogResult.OK)
            {
                formRptBookReport.Dispose();
                formRptBookReport = null;
            }
        }


        private void getComboTitle()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List


            data.Add(new KeyValuePair<int, string>(1, "สถานะโต๊ะที่เปิดอยู่ (Current Status)"));
            data.Add(new KeyValuePair<int, string>(2, "Log ORDER ตาม Table (Order Current)"));
            data.Add(new KeyValuePair<int, string>(3, "Log ORDER ตาม Table (Order Today)"));
            data.Add(new KeyValuePair<int, string>(4, "Log การย้ายโต๊ะ"));

            // Clear the combobox
            comboBoxTitle.DataSource = null;
            comboBoxTitle.Items.Clear();

            // Bind the combobox
            comboBoxTitle.DataSource = new BindingSource(data, null);
            comboBoxTitle.DisplayMember = "Value";
            comboBoxTitle.ValueMember = "Key";

            comboBoxTitle.SelectedIndex = 0;


        }

        int dataSelectID = 0;
        DataTable result = null;

        private void getResultDatatable()
        {
            try
            {

                dataSelectID = comboBoxTitle.SelectedIndex;

                if (dataSelectID == 0)
                    this.result = gd.getAllTableStatus();
                else if (dataSelectID == 1)
                    this.result = gd.getAllTableOrder();
                else if (dataSelectID == 2)
                    this.result = gd.getAllTableOrder_Today();
                else if (dataSelectID == 3)
                    this.result = gd.getAllTableMoveLog();


                dataGridViewResult.DataSource = null;
                dataGridViewResult.DataSource = result;


                if (dataGridViewResult.RowCount > 0)
                {
                    if (dataSelectID >= 1 && dataSelectID <= 2)
                    {
                        dataGridViewResult.Columns[1].Visible = false;
                        dataGridViewResult.Columns[3].Visible = false;
                        //dataGridViewResult.Columns[4].Visible = false;

                    }

                }

                //  textBoxSRTable_TextChanged(null, null);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void comboBoxTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            getResultDatatable();
        }

        private void textBoxSRTable_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string srPName = textBoxSRTable.Text;


                if (srPName.Length > 0)
                {
                    this.result.DefaultView.RowFilter = string.Format(" tableName like '*{0}*' ", srPName);

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

    }
}
