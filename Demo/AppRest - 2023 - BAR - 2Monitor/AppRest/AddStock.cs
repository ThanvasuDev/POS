using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppRest
{
    public partial class AddStock : AddDataTemplate
    {
        List<Store> allStores;
        List<Store> allStoresByCat;
        List<Stock> dataStock;
        List<Stock> dataStockByStoreID;


        DataTable dataAddStock;

        DataTable dataAllStore;


        List<StoreCat> allStoreCat;

        GetDataRest gd;

        int flagSave = 0;
        int addStockID = 0;
        int selectedStoreCat = 0;
        int selectInvID = 0;
        float convertRate = 0;
 
        DataTable dataAllStore_Take;

        DataTable dataAllStore_Ending;
        string printerCashName;

     //   CultureInfo ci = new CultureInfo("en-EN");

        public AddStock(Form frmlkFrom, int flagFrmClose)
        {
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();


            this.Width = 1024;
            this.Height = 764;

            gd = new GetDataRest();


            try
            {

                allStores = gd.getListAllStore(0, 0, "000");
                //getComboAllStore();

                allStoreCat = gd.getListAllStoreCat();
                getComboAllStoreCat();


                this.selectedStoreCat = 0;
                dataAllStore = gd.getAllStore(this.selectedStoreCat);
                dataGridViewAllStore.DataSource = dataAllStore;
                allStoresByCat = gd.getListAllStore(this.selectedStoreCat, 0, "000");

                getComboAllStoreCat_Take();

                //  this.selectedStoreCat = 0;
                dataAllStore_Take = gd.getAllStoreTake(Login.posBranchID);
                dataGridViewAllStore_Take.DataSource = dataAllStore_Take;

                if (dataGridViewAllStore_Take.RowCount > 0)
                {
                    dataGridViewAllStore_Take.Columns[0].Visible = false;
                    dataGridViewAllStore_Take.Columns[1].Visible = false;
                    dataGridViewAllStore_Take.Columns[3].Visible = false;
                    dataGridViewAllStore_Take.Columns[10].Visible = false;
                    dataGridViewAllStore_Take.Columns[11].Visible = false;

                    dataGridViewAllStore_Take.Columns[2].HeaderText = "หมวด";
                    dataGridViewAllStore_Take.Columns[4].HeaderText = "รหัส";
                    dataGridViewAllStore_Take.Columns[5].HeaderText = "รายการ";
                    dataGridViewAllStore_Take.Columns[6].HeaderText = "จำนวน";
                    dataGridViewAllStore_Take.Columns[7].HeaderText = "หน่วย(ใหญ่)";
                    dataGridViewAllStore_Take.Columns[8].HeaderText = "จำนวน";
                    dataGridViewAllStore_Take.Columns[9].HeaderText = "หน่วย(เล็ก)";
                    //  dataGridViewAllStore_Take.Columns[10].HeaderText = "Rate";
                    //  dataGridViewAllStore_Take.Columns[11].HeaderText = "เหตุผล";
                    dataGridViewAllStore_Take.Columns[12].HeaderText = "เป็นเงิน";
                    dataGridViewAllStore_Take.Columns[13].Visible = false;

                    //dataGridViewAllStore.Columns[8].Visible = false;
                    //dataGridViewAllStore.Columns[9].Visible = false;
                    //dataGridViewAllStore.Columns[10].Visible = false;

                }

                dataAllStore_Ending = gd.getAllStoreTake_Ending(Login.posBranchID);
                dataGridViewAllStore_Ending.DataSource = dataAllStore_Ending;


                if (dataGridViewAllStore_Ending.RowCount > 0)
                {
                    dataGridViewAllStore_Ending.Columns[0].Visible = false;
                    dataGridViewAllStore_Ending.Columns[1].Visible = false;
                  

                    dataGridViewAllStore_Ending.Columns[2].HeaderText = "หมวด";
                    dataGridViewAllStore_Ending.Columns[3].Visible = false;
                    dataGridViewAllStore_Ending.Columns[4].HeaderText = "รหัส";
                    dataGridViewAllStore_Ending.Columns[5].HeaderText = "รายการ";
                    dataGridViewAllStore_Ending.Columns[6].Visible = false;
                    dataGridViewAllStore_Ending.Columns[7].Visible = false;
                //    dataGridViewAllStore_Ending.Columns[6].HeaderText = "คงเหลือ (ระบบ)";
                //    dataGridViewAllStore_Ending.Columns[7].HeaderText = "หน่วย (เล็ก)";
                    dataGridViewAllStore_Ending.Columns[8].HeaderText = "คงเหลือ (ใหญ่)";
                    dataGridViewAllStore_Ending.Columns[9].HeaderText = "หน่วย (ใหญ่ / เล็ก)";
                 //   dataGridViewAllStore_Ending.Columns[10].Visible = false;
                    //    dataGridViewAllStore_Ending.Columns[10].HeaderText = "นับจริง หน่วย (ใหญ่)";
                    //    dataGridViewAllStore_Ending.Columns[11].HeaderText = "หน่วย (เล็ก)";
                    dataGridViewAllStore_Ending.Columns[10].HeaderText = "นับจริง (หน่วยเล็ก)";
                    dataGridViewAllStore_Ending.Columns[11].HeaderText = "นับจริง (หน่วยใหญ่)";
                    dataGridViewAllStore_Ending.Columns[12].Visible = false;
                    dataGridViewAllStore_Ending.Columns[13].Visible = false;
                    dataGridViewAllStore_Ending.Columns[14].Visible = false;
                    dataGridViewAllStore_Ending.Columns[15].Visible = false;

                       dataGridViewAllStore_Ending.Columns[8].DefaultCellStyle.BackColor = Color.Yellow;
                       dataGridViewAllStore_Ending.Columns[10].DefaultCellStyle.BackColor = Color.Yellow;
                       dataGridViewAllStore_Ending.Columns[11].DefaultCellStyle.BackColor = Color.Yellow;

                 

                }

                printerCashName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();
                printCash.PrinterSettings.PrinterName = this.printerCashName;

                setDefault();
                defaultTab4();

            }
            catch (Exception ex)
            {

            }

        }

        private void getComboAllStoreCat_Take()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List
            data.Add(new KeyValuePair<int, string>(0, "==== กลุ่มวัตถุดิบ ===="));

            foreach (StoreCat c in allStoreCat)
            {
               

                data.Add(new KeyValuePair<int, string>(c.StoreCatID, c.StoreCatName));
            }


            // Clear the combobox
            comboBoxAllCat_Take.DataSource = null;
            comboBoxAllCat_Take.Items.Clear();

            // Bind the combobox
            comboBoxAllCat_Take.DataSource = new BindingSource(data, null);
            comboBoxAllCat_Take.DisplayMember = "Value";
            comboBoxAllCat_Take.ValueMember = "Key";

            //  comboBoxAllCat_Take.SelectedValue = selectedStoreCat;
            //getComboAllStore();

            // Clear the combobox
            comboBoxAllCat_Ending.DataSource = null;
            comboBoxAllCat_Ending.Items.Clear();

            // Bind the combobox
            comboBoxAllCat_Ending.DataSource = new BindingSource(data, null);
            comboBoxAllCat_Ending.DisplayMember = "Value";
            comboBoxAllCat_Ending.ValueMember = "Key";



        }


        private void getComboAllStoreCat()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List
            data.Add(new KeyValuePair<int, string>(0, "==== กลุ่มวัตถุดิบ ===="));

            foreach (StoreCat c in allStoreCat)
            {
                data.Add(new KeyValuePair<int, string>(c.StoreCatID, c.StoreCatName));
            }


            // Clear the combobox
            comboBoxAllCat.DataSource = null;
            comboBoxAllCat.Items.Clear();

            // Bind the combobox
            comboBoxAllCat.DataSource = new BindingSource(data, null);
            comboBoxAllCat.DisplayMember = "Value";
            comboBoxAllCat.ValueMember = "Key";

            comboBoxAllCat.SelectedValue = selectedStoreCat;
            getComboAllStore();

        }

        private void ComboBoxAllStoreCat_Change(object sender, EventArgs e)
        {
            try
            {
                selectedStoreCat = Int32.Parse(comboBoxAllCat.SelectedValue.ToString());
                allStoresByCat = gd.getListAllStore(selectedStoreCat, 0, "000");
                //   dataGridViewAllStore.DataSource = allStoresByCat;
                getComboAllStore();
            }
            catch (Exception ex)
            {

            }
        }


        private void getComboAllStore()
        {
            try
            {

                List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

                // Add data to the List

                data.Add(new KeyValuePair<int, string>(0, "==== เลือกวัตถุดิบ ===="));

                foreach (Store c in allStoresByCat)
                {
                    data.Add(new KeyValuePair<int, string>(c.StoreID, c.StoreName));
                }


                // Clear the combobox
                comboBoxListStore.DataSource = null;
                comboBoxListStore.Items.Clear();

                // Bind the combobox
                comboBoxListStore.DataSource = new BindingSource(data, null);
                comboBoxListStore.DisplayMember = "Value";
                comboBoxListStore.ValueMember = "Key";

                comboBoxListStore.SelectedIndex = 0;

            }
            catch (Exception ex)
            {

            }

        }

        private void ComboboxStore_Change(object sender, EventArgs e)
        {

            try
            {

                int selectStoreID = Int32.Parse(comboBoxListStore.SelectedValue.ToString());

                foreach (Store c in allStoresByCat)
                {
                    if (c.StoreID == selectStoreID)
                    {
                        labelUnit.Text = c.StoreUnit;
                        labelUnitConvert.Text = c.StoreConvertUnit;
                        this.convertRate = float.Parse(c.StoreConvertRate.ToString());

                    }
                }

                dataStockByStoreID = gd.getAllStock(selectStoreID, selectInvID);
                dataGridViewAddStoreDetail.DataSource = dataStockByStoreID;
                dataGridViewAddStoreDetail.Columns[1].Visible = false;
                dataGridViewAddStoreDetail.Columns[3].Visible = false;
                dataGridViewAddStoreDetail.Columns[7].Visible = false;

            }
            catch (Exception ex)
            {

            }

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            setDefault();
        }

        private void setDefault()
        {
            try
            {


                dateTimePickerAddStock.Value = DateTime.Now;
                dateTimePickerAddStock_Sum.Value = DateTime.Now;
                dateTimePickerAddStock_Ending.Value = DateTime.Now;

                panelView.Visible = false;

                this.dataStock = gd.getAllStock(0, selectInvID);
                this.dataAddStock = gd.getDataAllStock(0, selectInvID);

                dataGridViewAllAddStock.DataSource = dataAddStock;


                if (dataGridViewAllAddStock.RowCount > 0)
                { 
                    dataGridViewAllAddStock.Columns[1].Visible = false;
                    dataGridViewAllAddStock.Columns[3].Visible = false;
                    dataGridViewAllAddStock.Columns[4].Visible = false;

                }


                //if (dataGridViewAllStore_Ending.RowCount > 0)
                //{
                //    dataGridViewAllStore_Ending.Columns[0].Visible = false;
                //    dataGridViewAllStore_Ending.Columns[1].Visible = false;
                //    dataGridViewAllStore_Ending.Columns[3].Visible = false;

                //}

                //dataGridViewAllStore_Take.DataSource = dataAllStore_Take;
                //dataGridViewAllStore_Ending.DataSource = dataAllStore_Ending;




                txtBoxAddStoreQTY.Text = "0";
                txtBoxBigQTY.Text = "0";
                txtBoxAddStoreAmt.Text = "0";
                txtBoxAddStoreRemark.Text = "เพิ่มเติม";


                flagSave = 0;
                FlagSaveChange();

            }
            catch (Exception ex)
            {

            }


        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {
            try
            {
                string poNo = "";
                int selectStoreID = Int32.Parse(comboBoxListStore.SelectedValue.ToString());
                float storeaddStoreQTY = (float)Double.Parse(txtBoxAddStoreQTY.Text);
                string dateAddStock = dateTimePickerAddStock.Value.ToString("yyyyMMdd");
                string storeName = comboBoxListStore.Text;
                string addStockType = comboBoxType.Text;

                float storeaddStoreAmt = (float)Double.Parse(txtBoxAddStoreAmt.Text);
                string storeRemark = txtBoxAddStoreRemark.Text.ToString();
                string storeBarCode = txtBoxSearchBarcode.Text.ToString();


                if (flagSave == 0)
                {
                    if (storeaddStoreQTY == (float)0 && selectStoreID > 0)
                        throw new Exception("กรุณากรอกข้อมูลให้ครบ");

                    if (MessageBox.Show("คุณต้องการจะเพิ่มวัตถุดิบ : " + storeName + " หรือไม่ ?", "เพิ่ม " + storeName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.instNewAddStock(selectStoreID, dateAddStock, storeaddStoreQTY, storeaddStoreAmt, storeRemark, storeBarCode, addStockType, 1);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Add Stock : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่มการนำเข้า Stock : " + storeName + " >> (Success)");
                            setDefault();
                            ComboboxStore_Change(null, e);
                        }
                    }
                }
                else
                {


                    if (MessageBox.Show("คุณต้องการจะแก้ไข : " + storeName + "   หรือไม่ ?", "แก้ไข " + storeName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updsAddStock(poNo, addStockID, selectStoreID, storeaddStoreQTY, storeaddStoreAmt, storeRemark, addStockType,0);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Add Stock : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("แก้ไขการนำเข้า Stock : " + storeName + " >> (Success)");
                            setDefault();
                            ComboboxStore_Change(null, e);
                        }



                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditAddStock_Click(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                addStockID = Int32.Parse(dataGridViewAddStoreDetail["AddStockID", e.RowIndex].Value.ToString());

                if (e.ColumnIndex == dataGridViewAddStoreDetail.Columns["Edit"].Index && e.RowIndex >= 0)
                {
                    flagSave = 1;
                    FlagSaveChange();

                    foreach (Stock c in dataStockByStoreID)
                    {
                        if (c.AddStockID == addStockID)
                        {
                            comboBoxListStore.SelectedValue = c.StoreID;
                            txtBoxAddStoreQTY.Text = c.StoreQTY.ToString();
                            comboBoxType.Text = c.AddStockType;
                            txtBoxAddStoreAmt.Text = c.StoreAmount.ToString();
                            txtBoxAddStoreRemark.Text = c.StoreRemark;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void FlagSaveChange()
        {
            if (flagSave == 0)
            {
                //labelHeader.Text = "เพิ่มข้อมูลวัตถุดิบ";
                buttonSave.Text = "เพิ่มข้อมูล";
                comboBoxListStore.Enabled = true;
                dateTimePickerAddStock.Enabled = true;
            }
            else
            {
                //  labelHeader.Text = "แก้ไขข้อมูลวัตถุดิบ";
                buttonSave.Text = "แก้ไขข้อมูล";
                comboBoxListStore.Enabled = false;
                dateTimePickerAddStock.Enabled = false;
            }
        }

        private void buttonScanBarcodeforSearch_Click(object sender, EventArgs e)
        {
            txtBoxSearchBarcode.Focus();

        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxAddStoreQTY.Text = ((float)Double.Parse(txtBoxBigQTY.Text) * this.convertRate).ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void buttonConvert2_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxBigQTY.Text = ((float)Double.Parse(txtBoxAddStoreQTY.Text) / this.convertRate).ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void txtBoxSearchBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {

                    string barcodestr = txtBoxSearchBarcode.Text;

                    foreach (Store c in allStores)
                    {
                        if (c.StoreBarcode == barcodestr)
                        {
                            comboBoxAllCat.SelectedValue = c.StoreCatID;
                            comboBoxListStore.SelectedValue = c.StoreID;


                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void buttonOpenSearch_Click(object sender, EventArgs e)
        {
            panelSearch.Visible = true;
        }

        private void buttonClosePN_Click(object sender, EventArgs e)
        {
            panelSearch.Visible = false;
        }

        private void textBoxSrcStoreName_TextChanged(object sender, EventArgs e)
        {

            try
            {

                string srPName = textBoxSrcStoreName.Text;


                if (srPName.Length > 0)
                {
                    this.dataAllStore.DefaultView.RowFilter = string.Format("  ( StoreName like '*{0}*' or StoreBarcode like '*{0}*' )  and StoreFlagUse = 'Y' ", srPName);

                }
                else
                {
                    this.dataAllStore.DefaultView.RowFilter = string.Format(" 1 = 1 ", srPName);
                }




            }
            catch (Exception ex)
            {

            }


        }

        private void dataGridViewAllStore_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridStoreCatID = Int32.Parse(dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreCatID"].Value.ToString());
                int dataGridStoreID = Int32.Parse(dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreID"].Value.ToString());



                comboBoxAllCat.SelectedValue = dataGridStoreCatID;
                comboBoxListStore.SelectedValue = dataGridStoreID;

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            if (panelView.Visible)
                panelView.Visible = false;
            else
                panelView.Visible = true;
        }

        private void textBoxSrcStoreName_TAKE_TextChanged(object sender, EventArgs e)
        {

            try
            {

                string srPName = textBoxSrcStoreName_Take.Text;

                int srStoreCatID = Int32.Parse(comboBoxAllCat_Take.SelectedValue.ToString());


                if ((srPName.Length > 0) && (srStoreCatID > 0))
                {
                    this.dataAllStore_Take.DefaultView.RowFilter = string.Format(" ( StoreName like '*{0}*' or StoreBarcode like '*{0}*' )  and StoreCatID = '{0}'", srPName, srStoreCatID);
                }
                else if ((srPName.Length == 0) && (srStoreCatID > 0))
                {
                    this.dataAllStore_Take.DefaultView.RowFilter = string.Format("StoreCatID = '{0}'", srStoreCatID);
                }
                else if ((srPName.Length > 0) && (srStoreCatID == 0))
                {
                    this.dataAllStore_Take.DefaultView.RowFilter = string.Format("StoreName like '*{0}*' or StoreBarcode like '*{0}*'", srPName);
                }
                else
                {
                    this.dataAllStore_Take.DefaultView.RowFilter = string.Format("1 = 1", 1);  // All
                }


            }
            catch (Exception ex)
            {

            }


        }

        List<StockChange> stcLists;
        int flagPrint = 1; 

        private void buttonAddTake_Click(object sender, EventArgs e)
        {
            try
            {

                /// Header
                /// 

                flagPrint = 1;
                comboBoxAllCat_Take.SelectedIndex = 0;


                //int invenOutID = 0;
                //int invenInID = 0;
                //int selectStoreID = 0;

                int addstocksign = 1;

                if (comboBoxType.Text.Contains("(-)"))
                    addstocksign = -1;


                // selectStoreID = Int32.Parse(comboBoxListStore.SelectedValue.ToString());


                string poHeaderRemark = "";// textBoxRemPOHeader.Text.ToString();
                string addstockType = comboBoxType.Text;
                /////



                string dateAddStock = dateTimePickerAddStock.Value.ToString("yyyyMMdd");

                int storeID = 0;
                string storeName = "";
                float storeAddBigUnit = 0;
                string storeBigUnit = "";
                float storeAddUnit = 0;
                string storeUnit = "";
                float storeConvertRate = 0;
                string storeRemark = "";
                float storeAmt = 0;

                float addUnits = 0;


                stcLists = new List<StockChange>();

                foreach (DataGridViewRow row in dataGridViewAllStore_Take.Rows)
                {
                    if (row.Cells["TakeBigUnit"].Value.ToString() != "0" ||
                        row.Cells["TakeUnit"].Value.ToString() != "0")
                    {
                        storeID = Int32.Parse(row.Cells["StoreID"].Value.ToString());
                        storeName = row.Cells["StoreName"].Value.ToString();
                        storeUnit = row.Cells["StoreUnit"].Value.ToString();
                        storeBigUnit = row.Cells["StoreConvertUnit"].Value.ToString();
                        storeAddBigUnit = float.Parse(row.Cells["TakeBigUnit"].Value.ToString());
                        storeAddUnit = float.Parse(row.Cells["TakeUnit"].Value.ToString());
                        storeConvertRate = float.Parse(row.Cells["StoreConvertRate"].Value.ToString());
                        storeRemark = row.Cells["StoreRemark"].Value.ToString();
                        storeAmt = float.Parse(row.Cells["StoreCost"].Value.ToString());


                        storeAddUnit = storeAddBigUnit * storeConvertRate + storeAddUnit;

                        storeAddBigUnit = storeAddUnit / storeConvertRate;

                        stcLists.Add(new StockChange(storeID, storeName, storeAddBigUnit, storeBigUnit, storeAddUnit, storeUnit, storeAmt, storeRemark));

                    }
                }

                string txtInsert = "ทำรายการ " + addstockType + " ดังนี้ \n\r";
                int i = 1;

                string str1 = "";
                string str2 = "";

                foreach (StockChange st in stcLists)
                {
                    str1 = i.ToString() + ". " + st.StoreName;

                    str2 = "  : " + st.StoreAddBigUnit.ToString("###,###.##") + " " + st.StoreBigUnit + " (" + st.StoreAddUnit.ToString("###,###.##") + " " + st.StoreUnit + ")";

                    txtInsert += str1 + str2 + "\n\r";
                    i++;
                }




                int result = 0;

                if (MessageBox.Show("คุณต้องการทำรายการ \n\r" + txtInsert + " หรือไม่ ?", "ทำรายการเพิ่มข้อมูล Stock", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (StockChange st in stcLists)
                    {
                        result = gd.instNewAddStock(st.StoreID, dateAddStock, st.StoreAddUnit * addstocksign, st.StoreAmt, st.StoreRemark, "", addstockType,1);
                    }
                }


                if (result > 0)
                {
                    if (ConfigurationSettings.AppSettings["FlagPrintCash"].ToString().ToLower() == "y")
                         printCash.Print();


                    //dataAllPOHeader = gd.getAllPOHeader("0");
                    //dataGridViewPOHeader.DataSource = dataAllPOHeader;
                    //refreshPODetail();
                    //panelTakeOne.Visible = true;
                    dataAllStore_Take = gd.getAllStoreTake(Login.posBranchID);
                    dataGridViewAllStore_Take.DataSource = dataAllStore_Take;




                    MessageBox.Show("ทำรายการคลังสินค้าสำเร็จ");
                    setDefault();
                    tabControl1.SelectedTab = tabPage3;
                }

                // defaultStock();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxSrcStoreName_Sum_TextChanged(object sender, EventArgs e)
        {
            searchAddStock();
        }
        private void comboBoxType_Sum_SelectedValueChanged(object sender, EventArgs e)
        {
            searchAddStock();
        }

        private void dateTimePickerAddStock_Sum_ValueChanged(object sender, EventArgs e)
        {
            searchAddStock();
        }

        private void comboBoxTypeDATE_Sum_SelectedValueChanged(object sender, EventArgs e)
        {
            searchAddStock();
        }

        private void buttonVewStockReport_Sum_Click(object sender, EventArgs e)
        {
            searchAddStock();
        }

        private void buttonExportDATA_Sum_Click(object sender, EventArgs e)
        {
            try
            {
                ExportData.Excel_FromDataTable_DT(dataGridViewAllAddStock);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // 
        private void searchAddStock()
        {
            try
            {

                string srPName = textBoxSrcStoreName_Sum.Text;
                string srDate = dateTimePickerAddStock_Sum.Value.ToString("dd/MM/yyyy");
                string srTypr = comboBoxType_Sum.Text;

               

                if (comboBoxTypeDATE_Sum.Text == "DATE")
                {

                  //  MessageBox.Show(srDate);

                    if ((srPName.Length > 0) && (srTypr != "ALL"))
                    {
                        this.dataAddStock.DefaultView.RowFilter = string.Format("StoreName like '*{0}*' and AddType like '*{1}*' and AddStockDate = '{2}'", srPName, srTypr, srDate);
                    }
                    else if ((srPName.Length == 0) && (srTypr != "ALL"))
                    {
                        this.dataAddStock.DefaultView.RowFilter = string.Format("AddType like '*{0}*'  and AddStockDate = '{1}' ", srTypr, srDate);
                    }
                    else if ((srPName.Length > 0) && (srTypr == "ALL"))
                    {
                        this.dataAddStock.DefaultView.RowFilter = string.Format("StoreName like '*{0}*'  and AddStockDate = '{1}' ", srPName, srDate);
                    }
                    else
                    {
                        this.dataAddStock.DefaultView.RowFilter = string.Format("AddStockDate = '{0}'", srDate);  // All
                    }
                }
                else
                {

                    if ((srPName.Length > 0) && (srTypr != "ALL"))
                    {
                        this.dataAddStock.DefaultView.RowFilter = string.Format("StoreName like '*{0}*' and  AddType like '*{1}*' ", srPName, srTypr);
                    }
                    else if ((srPName.Length == 0) && (srTypr != "ALL"))
                    {
                        this.dataAddStock.DefaultView.RowFilter = string.Format("AddType like '*{0}*' ", srTypr);
                    }
                    else if ((srPName.Length > 0) && (srTypr == "ALL"))
                    {
                        this.dataAddStock.DefaultView.RowFilter = string.Format("StoreName like '*{0}*'", srPName);
                    }
                    else
                    {
                        this.dataAddStock.DefaultView.RowFilter = string.Format("1 = 1", 1);  // All
                    }

                }


            }
            catch (Exception ex)
            {

            }

        }


        private void textBoxSrcStoreName_Ending_TextChanged(object sender, EventArgs e)
        {
            try
            { 
                refreshlistEndingStock(); 
            }
            catch (Exception ex)
            {

            }
        }

        private void refreshlistEndingStock()
        {
            try
            {

                string srPName = textBoxSrcStoreName_Ending.Text;

                int srStoreCatID = Int32.Parse(comboBoxAllCat_Ending.SelectedValue.ToString());


                if (stockEndingTag == "Monthly")
                {

                    if ((srPName.Length > 0) && (srStoreCatID > 0))
                    {
                        this.dataAllStore_Ending.DefaultView.RowFilter = string.Format(" ( StoreName like '*{0}*' or StoreBarcode like '*{0}*' ) and StoreCatID = '{1}'    ", srPName, srStoreCatID);
                    }
                    else if ((srPName.Length == 0) && (srStoreCatID > 0))
                    {
                        this.dataAllStore_Ending.DefaultView.RowFilter = string.Format("StoreCatID = '{0}'    ", srStoreCatID);
                    }
                    else if ((srPName.Length > 0) && (srStoreCatID == 0))
                    {
                        this.dataAllStore_Ending.DefaultView.RowFilter = string.Format(" ( StoreName like '*{0}*' or StoreBarcode like '*{0}*' )  ", srPName);
                    }
                    else
                    {
                        this.dataAllStore_Ending.DefaultView.RowFilter = string.Format("1 = 1", 1);  // All
                    }
                }
                else if (stockEndingTag == "Weekly")
                {
                    if ((srPName.Length > 0) && (srStoreCatID > 0))
                    {
                        this.dataAllStore_Ending.DefaultView.RowFilter = string.Format(" ( StoreName like '*{0}*' or StoreBarcode like '*{0}*' ) and StoreCatID = '{1}' and  ( StoreRemark like '*Daily*' or  StoreRemark like '*Weekly*' ) ", srPName, srStoreCatID);
                    }
                    else if ((srPName.Length == 0) && (srStoreCatID > 0))
                    {
                        this.dataAllStore_Ending.DefaultView.RowFilter = string.Format("StoreCatID = '{0}'   and  ( StoreRemark like '*Daily*' or  StoreRemark like '*Weekly*' )   ", srStoreCatID);
                    }
                    else if ((srPName.Length > 0) && (srStoreCatID == 0))
                    {
                        this.dataAllStore_Ending.DefaultView.RowFilter = string.Format(" ( StoreName like '*{0}*' or StoreBarcode like '*{0}*' )  and  ( StoreRemark like '*Daily*' or  StoreRemark like '*Weekly*' )  ", srPName);
                    }
                    else
                    {
                        this.dataAllStore_Ending.DefaultView.RowFilter = string.Format("  ( StoreRemark like '*Daily*' or  StoreRemark like '*Weekly*' ) ", 1);  // All
                    }
                }
                else if (stockEndingTag == "Daily")
                {
                    if ((srPName.Length > 0) && (srStoreCatID > 0))
                    {
                        this.dataAllStore_Ending.DefaultView.RowFilter = string.Format(" ( StoreName like '*{0}*' or StoreBarcode like '*{0}*' ) and StoreCatID = '{1}' and  ( StoreRemark like '*Daily*'  ) ", srPName, srStoreCatID);
                    }
                    else if ((srPName.Length == 0) && (srStoreCatID > 0))
                    {
                        this.dataAllStore_Ending.DefaultView.RowFilter = string.Format("StoreCatID = '{0}'   and  ( StoreRemark like '*Daily*'   )   ", srStoreCatID);
                    }
                    else if ((srPName.Length > 0) && (srStoreCatID == 0))
                    {
                        this.dataAllStore_Ending.DefaultView.RowFilter = string.Format(" ( StoreName like '*{0}*' or StoreBarcode like '*{0}*' )  and  ( StoreRemark like '*Daily*' )  ", srPName);
                    }
                    else
                    {
                        this.dataAllStore_Ending.DefaultView.RowFilter = string.Format("  ( StoreRemark like '*Daily*'  ) ", 1);  // All
                    }
                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        private void buttonAddTake_Ending_Click(object sender, EventArgs e)
        {
            try
            {
                /// Header
                /// 
                this.flagPrint = 2; 
                comboBoxAllCat_Take.SelectedIndex = 0;


                //int invenOutID = 0;
                //int invenInID = 0;
                //int selectStoreID = 0;

                int addstocksign = 1;

                if (comboBoxType.Text.Contains("(-)"))
                    addstocksign = -1;


                // selectStoreID = Int32.Parse(comboBoxListStore.SelectedValue.ToString());


                string poHeaderRemark = "";// textBoxRemPOHeader.Text.ToString();
                string addstockType = "นับ Stock คงเหลือ ณ สิ้นวัน (Ending)";
                /////


                string dateAddStock = dateTimePickerAddStock.Value.ToString("yyyyMMdd");

                int storeID = 0;
                string storeName = "";
                float storeAddBigUnit = 0;
                string storeBigUnit = "";
                float storeAddUnit = 0;
                string storeUnit = "";
                float storeConvertRate = 0;
                string storeRemark = "";
                float storeAmt = 0;

                float addUnits = 0;


                stcLists = new List<StockChange>();

                foreach (DataGridViewRow row in dataGridViewAllStore_Ending.Rows)
                {
                    if (row.Cells["TakeBigUnit"].Value.ToString() != "-1" ||
                        row.Cells["TakeUnit"].Value.ToString() != "-1")
                    {
                        storeID = Int32.Parse(row.Cells["StoreID"].Value.ToString());
                        storeName = row.Cells["StoreName"].Value.ToString();
                        storeUnit = row.Cells["StoreUnit"].Value.ToString();
                        storeBigUnit = row.Cells["StoreConvertUnit"].Value.ToString();
                        
                        storeAddBigUnit = float.Parse(row.Cells["TakeBigUnit"].Value.ToString());
                        storeAddUnit = float.Parse(row.Cells["TakeUnit"].Value.ToString());

                        if (storeAddBigUnit < 0)
                            storeAddBigUnit = 0;

                        if (storeAddUnit < 0)
                            storeAddUnit = 0;


                        storeConvertRate = float.Parse(row.Cells["StoreConvertRate"].Value.ToString());
                        storeRemark = row.Cells["StoreRemark"].Value.ToString();
                        storeAmt = float.Parse(row.Cells["StoreCost"].Value.ToString());

                        storeAddUnit = storeAddBigUnit * storeConvertRate + storeAddUnit;

                        storeAddBigUnit = storeAddUnit / storeConvertRate;

                        stcLists.Add(new StockChange(storeID, storeName, storeAddBigUnit, storeBigUnit, storeAddUnit, storeUnit, storeAmt, storeRemark));

                    }
                }

                string txtInsert = "ทำรายการ " + addstockType + " ดังนี้ \n\r";
                int i = 1;

                string str1 = "";
                string str2 = "";

                foreach (StockChange st in stcLists)
                {
                    str1 = i.ToString() + ". " + st.StoreName;

                    str2 = "  : " + st.StoreAddBigUnit.ToString("###,###.##") + " " + st.StoreBigUnit + " (" + st.StoreAddUnit.ToString("###,###.##") + " " + st.StoreUnit + ")";

                    txtInsert += str1 + str2 + "\n\r";
                    i++;
                }


                int result = 0;

                if (MessageBox.Show("คุณต้องการทำรายการ \n\r" + txtInsert + " หรือไม่ ?", "ทำรายการเพิ่มข้อมูล Stock", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (StockChange st in stcLists)
                    {
                        result = gd.instNewAddStock(st.StoreID, dateAddStock, st.StoreAddUnit, 0, "Ending", "", addstockType, 1);
                    }
                }


                if (result > 0)
                {
                    if (ConfigurationSettings.AppSettings["FlagPrintCash"].ToString().ToLower() == "y")
                        printCash.Print();
                      

                    result = gd.instNewAddStock_Ending(dateAddStock);

                    dataAllStore_Ending = gd.getAllStoreTake_Ending(Login.posBranchID);
                    dataGridViewAllStore_Ending.DataSource = dataAllStore_Ending;

                    MessageBox.Show("นับ Stock คงเหลือ ณ สิ้นวัน (Ending) สำเร็จ");
                    setDefault();
                    tabControl1.SelectedTab = tabPage3;
                }

                // defaultStock();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printCash_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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


                Font fontHeader = new Font("Arail", 12);
                Font fontTable = new Font("Arail", 11);
                Font fontSubHeader = new Font("Arail", 8);
                Font fontBody = new Font("Arail", 8);
                Font fontBodylist = new Font("Arail", 8);
                Font fontBodylistPro = new Font("Arail", 7);
                Font fontNum = new Font("Consolas", 8);


        

                Bitmap img = global::AppRest.Properties.Resources.Logo_New;

                if (Login.flagLogoSQ.ToLower() == "y")
                {

                    e.Graphics.DrawImage(img, x + 100, y, 100, 100);
                    y += 100;
                }
                else
                {
                    e.Graphics.DrawImage(img, x + 40, y, 220, 110);
                    y += 110;
                }

  
                e.Graphics.DrawString(restName, fontBody, brush, x + 10, y);

                y += 12; 


                e.Graphics.DrawString(restTel, fontBody, brush, x + 60, y);

                y += 15;

  

                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd MMM yy}", dt);
                string strTime = String.Format("{0:HH:mm}", dt);

    
                e.Graphics.DrawString("-------------------------------------------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 12;

                if(this.flagPrint == 1)
                     e.Graphics.DrawString(comboBoxType.Text, fontSubHeader, brush, x + 10, y);
                else
                    e.Graphics.DrawString("(*) Ending Stock / นับสต๊อกสิ้นวัน", fontSubHeader, brush, x + 10, y);
      
                y += 15;
                e.Graphics.DrawString(strDate + " " + strTime, fontSubHeader, brush, x + 70, y);
                y += 10;
                e.Graphics.DrawString("-------------------------------------------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 25;

                //// Print Order

                int i = 1;
                 

                string str1 = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";


                e.Graphics.DrawString("           รายการ             จำนวน  เป็นเงิน", fontNum, brush, 0, y);
                y += 15;


                List<string> txtPrint;
                int len = 35;

                foreach (StockChange o in this.stcLists)
                {
 

                        str1 = i.ToString() + ". " + o.StoreName.Trim() + " (" + o.StoreBigUnit + ")" ;

                        str2 = o.StoreAddBigUnit.ToString("###,###.#0");  
                        str2 = String.Format("{0,8}", str2); 
                       

                        str4 = o.StoreAmt.ToString("###,###.#0");
                        str4 = String.Format("{0,10}", str4); 


                           e.Graphics.DrawString(str2, fontNum, brush, x + 160, y);


                           e.Graphics.DrawString(str4, fontNum, brush, x + 210, y);

                            txtPrint = FuncString.WordWrap(str1, len);
                            str1 = "";

                            foreach (string op in txtPrint)
                            {
                                e.Graphics.DrawString(op, fontBodylist, brush, x + 2, y);
                                y += 13;
                            }

                            i++;
                      

                }

                //////////////////////////////////////////////////////////////////////////
                y += 30;
                e.Graphics.DrawString("โดย : " + Login.userID.ToString() + " " + Login.userName, fontTable, brush, x + 20, y);
                y += 20;
                e.Graphics.DrawString("เหตุผล : ........................... ", fontTable, brush, x + 20, y);
                y += 20;
                e.Graphics.DrawString("ลงชื่อ : ............................. ", fontTable, brush, x + 20, y);
                y += 20;
               
 

                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Tab 4
         
        string pathExportResultName;

        string fromDate = "";
        string toDate = ""; 
        int dataSelectID = 0;
        DataTable resultStockReport;
        TrnMax trnMax;

        private void defaultTab4(){

            try
            {
                 trnMax = gd.getTrnMax();
                 var todayDate = trnMax.MaxDate;
                 

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


            data.Add(new KeyValuePair<int, string>(1, "สรุปยอด Stock Moving"));
            data.Add(new KeyValuePair<int, string>(2, "สรุปยอด Stock Ending"));

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
                 
                 
                dataSelectID = comboBoxTitle.SelectedIndex; 
                 
                if (dataSelectID == 0)
                    this.resultStockReport = gd.getSum_StockReport(fromDate, toDate, selectInvID);
                else if (dataSelectID == 1)
                    this.resultStockReport = gd.getSum_StockReport_Ending(fromDate, toDate, selectInvID);


                dataGridViewResult.DataSource = null;
                dataGridViewResult.DataSource = this.resultStockReport;

                if (dataSelectID == 0)
                {

                    dataGridViewResult.Columns[0].HeaderText = "ID สินค้า";
                    dataGridViewResult.Columns[1].HeaderText = "รหัสสินค้า";
                    dataGridViewResult.Columns[2].HeaderText = "หมวด";
                    dataGridViewResult.Columns[3].HeaderText = "ชื่อ";
                    dataGridViewResult.Columns[4].HeaderText = "วันที่เริ่ม";
                    dataGridViewResult.Columns[5].HeaderText = "วันที่สิ้นสุด";
                    dataGridViewResult.Columns[6].HeaderText = "(#) เริ่มต้น";
                    dataGridViewResult.Columns[7].HeaderText = "(+) นำเข้า";
                    dataGridViewResult.Columns[8].HeaderText = "(-) นำออก";
                    dataGridViewResult.Columns[9].HeaderText = "(-) ของเสีย";
                    dataGridViewResult.Columns[10].HeaderText = "(-) ปรับปรุง ";
                    dataGridViewResult.Columns[11].HeaderText = "(+) ปรับปรุง";
                    dataGridViewResult.Columns[12].HeaderText = "(-) ขาย";
                    dataGridViewResult.Columns[13].HeaderText = "(=) คงเหลือ (เล็ก)";
                    dataGridViewResult.Columns[14].HeaderText = "หน่วยเล็ก";
                    dataGridViewResult.Columns[15].HeaderText = "(=) คงเหลือ (ใหญ่)";
                    dataGridViewResult.Columns[16].HeaderText = "หน่วยใหญ่";
                    dataGridViewResult.Columns[17].HeaderText = "-"; 
                    dataGridViewResult.Columns[18].HeaderText = "(#) เริ่มต้น (บาท)";
                    dataGridViewResult.Columns[19].HeaderText = "(+) นำเข้า (บาท)";
                    dataGridViewResult.Columns[20].HeaderText = "(-) นำออก (บาท)";
                    dataGridViewResult.Columns[21].HeaderText = "(-) ของเสีย (บาท)";
                    dataGridViewResult.Columns[22].HeaderText = "(-) ปรับปรุง (บาท) ";
                    dataGridViewResult.Columns[23].HeaderText = "(+) ปรับปรุง (บาท)";
                    dataGridViewResult.Columns[24].HeaderText = "(-) ขาย (บาท)";
                    dataGridViewResult.Columns[25].HeaderText = "(=) คงเหลือ (บาท)";

                    dataGridViewResult.Columns[15].DefaultCellStyle.BackColor = Color.Yellow;
                    dataGridViewResult.Columns[25].DefaultCellStyle.BackColor = Color.Yellow;

                }
                else if (dataSelectID == 1)
                {
                    dataGridViewResult.Columns[0].HeaderText = "ID สินค้า";
                    dataGridViewResult.Columns[1].HeaderText = "รหัสสินค้า";
                    dataGridViewResult.Columns[2].HeaderText = "หมวด";
                    dataGridViewResult.Columns[3].HeaderText = "วันที่";
                    dataGridViewResult.Columns[4].HeaderText = "ชื่อ";
                    dataGridViewResult.Columns[5].HeaderText = "หน่วยใหญ่";
                    dataGridViewResult.Columns[6].HeaderText = "(#) เริ่มต้น";
                    dataGridViewResult.Columns[7].HeaderText = "(+) นำเข้า";
                    dataGridViewResult.Columns[8].HeaderText = "(-) นำออก";
                    dataGridViewResult.Columns[9].HeaderText = "(-) ของเสีย";
                    dataGridViewResult.Columns[10].HeaderText = "(-) ขาย";
                    dataGridViewResult.Columns[11].HeaderText = "(=) คงเหลือ (ระบบ)";
                    dataGridViewResult.Columns[12].HeaderText = "(=) นับ Stock จริง";
                    dataGridViewResult.Columns[13].HeaderText = "(-/+) ปรับปรุง ";
                    dataGridViewResult.Columns[14].HeaderText = "(=) คงเหลือ (สิ้นวัน)";

                    dataGridViewResult.Columns[11].DefaultCellStyle.BackColor = Color.Green;
                    dataGridViewResult.Columns[12].DefaultCellStyle.BackColor = Color.Yellow;
                    dataGridViewResult.Columns[14].DefaultCellStyle.BackColor = Color.Gold;
                }


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

        private void button1_Click(object sender, EventArgs e)
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
            searchStockReport();
        }

        private void searchStockReport()
        {
            try
            {

                string srPName = textBoxSearchInv.Text;



                if ((srPName.Length > 0))
                {
                    this.resultStockReport.DefaultView.RowFilter = string.Format("InvCode like '*{0}*' or  InvCat like '*{0}*' or InvName like '*{0}*'  ", srPName);
                }
                else
                {
                    this.resultStockReport.DefaultView.RowFilter = string.Format("1 = 1", 1);  // All
                }


            }
            catch (Exception ex)
            {

            }

        }

        private void buttonPrintA4_Click(object sender, EventArgs e)
        {

            try
            {

                fromDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxFromDate.Text);
                toDate = ClassConvert.ConvertDateMMDDYYYYToYYYYMMDD(textBoxToDate.Text);

                Button printButton = (Button)sender;

                dataSelectID = Int32.Parse(printButton.Name.Replace("buttonPrintRpt_", "").Trim());


                LinkFromRptBillReport();

            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }

        FromRptSumReport formFromRptSumReport;

        private void LinkFromRptBillReport()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formFromRptSumReport == null)
            {
                formFromRptSumReport = new FromRptSumReport(this, 0, fromDate, toDate, 0, dataSelectID);
            }
            else
            {
                formFromRptSumReport.rptFromDate = fromDate;
                formFromRptSumReport.rptToDate = toDate;
                formFromRptSumReport.rptType = dataSelectID;
                formFromRptSumReport.rptProductSupID = 0;
                formFromRptSumReport.viewReport();
            }
            Cursor.Current = Cursors.Default;
            if (formFromRptSumReport.ShowDialog() == DialogResult.OK)
            {
                formFromRptSumReport.Dispose();
                formFromRptSumReport = null;
            }
        }

        string stockEndingTag = "Monthly";

        private void radioButton_StockDaily_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton_StockDaily.Checked)
                    stockEndingTag = "Daily";
                else if (radioButton_StockWeekly.Checked)
                    stockEndingTag = "Weekly";
                else if (radioButton_StockMonthly.Checked)
                    stockEndingTag = "Monthly";

                refreshlistEndingStock();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                textBoxFromDate.Visible = false;
                labelStart.Visible = false;
                labelEnd.Text = "Stock สิ้นวัน (DD/MM/YYYY)";

                if (comboBoxTitle.SelectedIndex == 0)
                {
                    textBoxFromDate.Visible = true;
                    labelStart.Visible = true;
                    labelEnd.Text = "วันที่สิ้นสุด (DD/MM/YYYY)";
                }

                getResultDatatable();

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonExportEndingStock_Click(object sender, EventArgs e)
        {
            try
            {
                ExportData.Excel_FromDataTable_DT(dataGridViewAllStore_Ending);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
