using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace AppRest
{
    public partial class AddPO : AddDataTemplate
    {
        List<Store> allStores;
        List<Store> allStoresByCat;
        List<Inventory> allInven;
        List<PODetail> dataStock;
        List<PODetail> dataStockByStoreID;
        List<POType> allPOType;


        DataTable dataAllStore;      
        List<StoreCat> allStoreCat;


        DataTable dataAllPOHeader;
         
        GetDataRest gd;

        int flagSave = 0;
        int addStockID = 0;
        int selectedStoreCat = 0;
        float convertRate = 0;

        string poOrderNo = "";
        string poStatus = "";
        string poNextStatus = "";

        int poSelectProcessID;
        float storeSelectedCost;

        FromRptBillPOReport formFromRptBillPOReport;


        public AddPO(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();
             

            gd = new GetDataRest();

            try
            {

                allStores = gd.getListAllStore(0, 0, "000");
                //getComboAllStore();

                allStoreCat = gd.getListAllStoreCat();
                getComboAllStoreCat();

                allInven = gd.getAllInventory(0,"0","0","0");
                getComboAllInventory();


                allPOType = gd.getAllPOType(0, "0");
                getComboAllPOType();

                this.selectedStoreCat = 0;
                dataAllStore = gd.getAllStore(this.selectedStoreCat);
                dataGridViewAllStore.DataSource = dataAllStore;
                allStoresByCat = gd.getListAllStore(this.selectedStoreCat, 0, "000");

                poSelectProcessID = 0;

                setDefault();

            }
            catch (Exception ex)
            {

            }
             

        }

        private void getComboAllPOType()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

          
            foreach (POType c in allPOType)
            {
                data.Add(new KeyValuePair<int, string>(c.PoTypeID, c.PoType));
            } 

            // Clear the combobox
            comboBoxType.DataSource = null;
            comboBoxType.Items.Clear();

            // Bind the combobox
            comboBoxType.DataSource = new BindingSource(data, null);
            comboBoxType.DisplayMember = "Value";
            comboBoxType.ValueMember = "Key";
             

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


        private void getComboAllInventory()
        {
             List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List
            data.Add(new KeyValuePair<int, string>(0, "= เลือกต้นทาง ="));

            foreach (Inventory c in allInven)
            {
                if (c.FlagUse == "Y")
                {
                    data.Add(new KeyValuePair<int, string>(c.InventoryID, c.InventoryName));
                }
               
            } 
            // Clear the combobox
            comboBoxInventoryFrom.DataSource = null;
            comboBoxInventoryFrom.Items.Clear();

            // Bind the combobox
            comboBoxInventoryFrom.DataSource = new BindingSource(data, null);
            comboBoxInventoryFrom.DisplayMember = "Value";
            comboBoxInventoryFrom.ValueMember = "Key";

            ////////////////////////////////////////////////////////////////////////////////

            data = new List<KeyValuePair<int, string>>();

            // Add data to the List
            data.Add(new KeyValuePair<int, string>(0, "= เลือกปลาย ="));

            foreach (Inventory c in allInven)
            {
                if (c.FlagSuplier == "N" & c.FlagUse == "Y")
                {
                    data.Add(new KeyValuePair<int, string>(c.InventoryID, c.InventoryName));
                }
            }

            // Clear the combobox
            comboBoxInventoryTo.DataSource = null;
            comboBoxInventoryTo.Items.Clear();

            // Bind the combobox
            comboBoxInventoryTo.DataSource = new BindingSource(data, null);
            comboBoxInventoryTo.DisplayMember = "Value";
            comboBoxInventoryTo.ValueMember = "Key";

            ////////////////////////////////////////////////////////////////////////////////

            data = new List<KeyValuePair<int, string>>();

            // Add data to the List
            data.Add(new KeyValuePair<int, string>(0, "เลือกคลังรับสินค้า"));

            foreach (Inventory c in allInven)
            {
                if( c.FlagSuplier == "N" & c.FlagUse == "Y" )
                    data.Add(new KeyValuePair<int, string>(c.InventoryID, c.InventoryName));
                //MessageBox.Show(c.InventoryName);
            }

            // Clear the combobox
            comboBoxSelectInven.DataSource = null;
            comboBoxSelectInven.Items.Clear();

            // Bind the combobox
            comboBoxSelectInven.DataSource = new BindingSource(data, null);
            comboBoxSelectInven.DisplayMember = "Value";
            comboBoxSelectInven.ValueMember = "Key";     

           
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
                        this.storeSelectedCost = float.Parse(c.StoreCost.ToString());
                    }
                }

                //dataStockByStoreID = gd.getAllStock(selectStoreID);
                //dataGridViewAddStoreDetail.DataSource = dataStockByStoreID;
                //dataGridViewAddStoreDetail.Columns[1].Visible = false;
                //dataGridViewAddStoreDetail.Columns[3].Visible = false;
                //dataGridViewAddStoreDetail.Columns[7].Visible = false;

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

            refreshPODetail();

            dateTimePickerAddStock.Value = DateTime.Now;
            txtBoxAddStoreQTY.Text = "0";
            txtBoxBigQTY.Text = "0";
            txtBoxAddStoreAmt.Text = "0";
            txtBoxAddStoreRemark.Text = "เพิ่มเติม";
            textBoxRemPOHeader.Text = "เพิ่มเติม"; 
           

            flagSave = 0;
            FlagSaveChange();

            panelHeaderProcess.Visible = false;


            dataAllPOHeader = gd.getAllPOHeader("0");
            dataGridViewPOHeader.DataSource = dataAllPOHeader;

            dataGridViewPOHeader.Columns[1].HeaderText = "เลขที่";
            dataGridViewPOHeader.Columns[2].HeaderText = "วันที่";
            dataGridViewPOHeader.Columns[3].Visible = false;
            dataGridViewPOHeader.Columns[4].HeaderText = "จากคลัง/Supplier";
            dataGridViewPOHeader.Columns[5].Visible = false;
            dataGridViewPOHeader.Columns[6].HeaderText = "รับเข้า";
            dataGridViewPOHeader.Columns[7].HeaderText = "ประเภท";
            dataGridViewPOHeader.Columns[8].HeaderText = "QTY";
            dataGridViewPOHeader.Columns[9].HeaderText = "เป็นเงิน";


        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {
            try
            {

                string poNO = textBoxPONo.Text;
                  
                int invenOutID = 0;
                int invenInID = 0 ;
                int selectStoreID = 0;

                int addstocksign = 1;

                if (comboBoxFlagStock.Text == "นำออก" && comboBoxFlagStock.Visible == true)
                    addstocksign = -1;

                 
                if (flagSave == 0)
                {
                    invenInID = Int32.Parse(comboBoxInventoryFrom.SelectedValue.ToString());
                    invenOutID = Int32.Parse(comboBoxInventoryTo.SelectedValue.ToString()); 
                    selectStoreID = Int32.Parse(comboBoxListStore.SelectedValue.ToString()); 
                }

                string poHeaderRemark = textBoxRemPOHeader.Text.ToString();
                
                float storeaddStoreQTY= (float)Double.Parse(txtBoxAddStoreQTY.Text);
                string dateAddStock = dateTimePickerAddStock.Value.ToString("yyyyMMdd");
                string storeName = comboBoxListStore.Text;
                string addStockType = comboBoxType.Text;

                float storeaddStoreAmt = (float)Double.Parse(txtBoxAddStoreAmt.Text);

                float storeaddStoreVat = 0 ;

                if (checkBoxVat.Checked == true)
                    storeaddStoreVat =  storeaddStoreAmt * (float)0.07;

                string storeRemark = txtBoxAddStoreRemark.Text.ToString();
                string storeBarCode = txtBoxSearchBarcode.Text.ToString();

               

              // MessageBox.Show("00");

                if( poNO.Length == 0)
                    throw new Exception("กรุณาระบุเลขที่ใบ PO");

                if (poSelectProcessID == 0 && (flagSave == 0))
                {

                    if (invenOutID == 0)
                        throw new Exception("กรุณาเลือกคลังปลายทาง"); 

                    if (invenInID == 0)
                        throw new Exception("กรุณาเลือกคลังต้นทาง");

                }


                if (flagSave == 0)
                {
                    if (storeaddStoreQTY == (float)0 && selectStoreID > 0)
                        throw new Exception("กรุณากรอกข้อมูลให้ครบ");

                    if (MessageBox.Show("คุณต้องการจะเพิ่มข้อมูล  : " + storeName + " ใน PO" + poNO +" หรือไม่ ?", "เพิ่ม " + storeName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.instNewPO(poNO, invenOutID, invenInID, poHeaderRemark, selectStoreID, dateAddStock, storeaddStoreQTY * addstocksign, storeaddStoreAmt, storeaddStoreVat, storeRemark, storeBarCode, addStockType);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Add Stock : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่ม " + storeName + " ใน PO" + poNO + " >> (Success)");
                            setDefault();
                            ComboboxStore_Change(null, e);
                        }
                    }
                }
                else
                {


                    if (MessageBox.Show("คุณต้องการจะแก้ไข : " + storeName + "   ใน PO" + poNO + " หรือไม่ ?", "แก้ไข " + storeName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updatePODetail(poNO, addStockID, storeaddStoreQTY, storeaddStoreAmt, storeaddStoreVat, storeRemark, addStockType, this.poSelectProcessID);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Add Stock : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("แก้ไข " + storeName + " ใน PO" + poNO + " >> (Success)");
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

           


                flagSave = 1;
                FlagSaveChange();

                foreach (PODetail c in dataStock)
                {
                    if (c.AddStockID == addStockID)
                    {
                        MessageBox.Show(c.StoreName);

                        comboBoxListStore.Text = c.StoreName;
                        txtBoxAddStoreQTY.Text = c.StoreQTY.ToString();
                        txtBoxAddStoreAmt.Text = c.StoreAmount.ToString();
                        txtBoxAddStoreRemark.Text = c.StoreRemark;

                        if (c.StoreVat > 0)
                            checkBoxVat.Checked = true;
                         
                    }
                }


            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }


        private void FlagSaveChange()
        {
            if (flagSave == 0)
            {
                labelHeader.Text = "เพิ่มใบเบิกสินค้า";
                buttonSave.Text = "เพิ่มข้อมูล";
                comboBoxListStore.Enabled = true;
                comboBoxAllCat.Enabled = true;
                dateTimePickerAddStock.Enabled = true;
            }
            else
            {
                labelHeader.Text = "แก้ไขใบเบิกสินค้า";
                buttonSave.Text = "แก้ไขข้อมูล";
                comboBoxListStore.Enabled = false;
                comboBoxAllCat.Enabled = false;
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
            catch( Exception ex)
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
                    this.dataAllStore.DefaultView.RowFilter = string.Format("StoreName like '*{0}*' ", srPName);

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

        private void textBoxPONo_KeyPress(object sender, KeyPressEventArgs e)
        {
            refreshPODetail();
        }

        private void refreshPODetail()
        {
            if (textBoxPONo.Text.Length > 0)
            {
                dataStock = gd.getAllPODetail(textBoxPONo.Text, 0);
                dataGridViewAddStoreDetail.DataSource = dataStock;

                dataGridViewAddStoreDetail.Columns[1].Visible = false;
                dataGridViewAddStoreDetail.Columns[2].Visible = false;
                dataGridViewAddStoreDetail.Columns[3].Visible = false;
                dataGridViewAddStoreDetail.Columns[4].Visible = false;
                dataGridViewAddStoreDetail.Columns[10].Visible = false;
                dataGridViewAddStoreDetail.Columns[11].Visible = false;
            }
        }

        private void dataGridViewPOHeader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                panelHeaderProcess.Visible = true;

                poOrderNo = dataGridViewPOHeader.Rows[e.RowIndex].Cells["POOrderNo"].Value.ToString();

                poSelectProcessID = Int32.Parse(dataGridViewPOHeader.Rows[e.RowIndex].Cells["ProcessID"].Value.ToString());

                string procesBack = dataGridViewPOHeader.Rows[e.RowIndex].Cells["BackProcessName"].Value.ToString();
                string procesCurrent = dataGridViewPOHeader.Rows[e.RowIndex].Cells["CurrentProcessName"].Value.ToString();
                string procesNext = dataGridViewPOHeader.Rows[e.RowIndex].Cells["NextProcessName"].Value.ToString();
                string memcandonext = dataGridViewPOHeader.Rows[e.RowIndex].Cells["MemStatusCanDo"].Value.ToString();
                string poHeaderRemark = dataGridViewPOHeader.Rows[e.RowIndex].Cells["POHeaderRemark"].Value.ToString();

              //  MessageBox.Show(memcandonext);

                textBoxPONo.Text = poOrderNo;
                textBoxProcessNow.Text = procesCurrent;
                buttonBackProcess.Text = procesBack;
                buttonNextProcess.Text = procesNext;

                textBoxRemPOHeader.Text = poHeaderRemark;

                // CASE processCurrentID Min = 1

                if (poSelectProcessID == 1)
                    buttonBackProcess.Visible = false;
                else
                    buttonBackProcess.Visible = true;


                if (poSelectProcessID == 5)
                {
                    buttonNextProcess.Visible = false;
                    buttonBackProcess.Visible = false;
                }
                else
                {
                    buttonNextProcess.Visible = true;
                }

                if (memcandonext == "N")
                    buttonNextProcess.Enabled = false;
                else
                    buttonNextProcess.Enabled = true;


                flagSave = 0;
                FlagSaveChange();
                 
                refreshPODetail();

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonNewPO_Click(object sender, EventArgs e)
        {
            try
            {
                setDefault();


                //panelHeaderProcess.Visible = false;

                //int DateUSE = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));

                //if (DateUSE > 25590101)
                //    DateUSE = DateUSE - 543;

                //string strSystem = ConfigurationSettings.AppSettings["POSIDConfig"].ToString();


                //string newpoOrderNo = gd.getNextPO(strSystem).ToString();

                //newpoOrderNo = DateUSE.ToString() + strSystem + ("00" + newpoOrderNo).Right(2);

                //textBoxPONo.Text = newpoOrderNo;

                refreshPODetail();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                panelHeaderProcess.Visible = false;

                int DateUSE = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));

                if (DateUSE > 25590101)
                    DateUSE = DateUSE - 543;

                string strSystem = ConfigurationSettings.AppSettings["POSIDConfig"].ToString();


                string newpoOrderNo = gd.getNextPO(strSystem).ToString();

                newpoOrderNo = DateUSE.ToString() + strSystem + ("00" + newpoOrderNo).Right(2);

                textBoxPONo.Text = newpoOrderNo;
            }
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            // Next Process

            Button bt = (Button)sender;
            int addPOStatus = 0; 

            if (bt.Name == "buttonNextProcess")
                addPOStatus = 1;
            else
                addPOStatus = -1;

            string poHeaderRemark = textBoxRemPOHeader.Text.ToString();

            try
            {
                int result = gd.getNextProcessPO(this.poOrderNo, poHeaderRemark, addPOStatus);

                if (result <= 0)
                {
                    MessageBox.Show("ไม่สามารถอนุมัติรายการถัดไปได้");
                }
                else
                {
                    MessageBox.Show("ทำรายการสำเร็จ " + bt.Text);

                    setDefault();
                }
            }
            catch (Exception ex)
            {

            }


        }

        private void buttonPOPrint_Click(object sender, EventArgs e)
        {

   
            try
            { 

                // MessageBox.Show(type.ToString());
                LinkFromRptBillReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void LinkFromRptBillReport()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formFromRptBillPOReport == null)
            {
                formFromRptBillPOReport = new FromRptBillPOReport(this, 0, this.poOrderNo, 1);
            }
            else
            {

                formFromRptBillPOReport.rptBillPO = this.poOrderNo;
                //MessageBox.Show("55");
                //formFromRptBillPOReport.crystalReportViewer2.ReportSource = null;
              //  formFromRptBillPOReport.rptType = 2; 
              //  formFromRptBillPOReport.crystalReportViewer2.Refresh();
                formFromRptBillPOReport.viewReport();
              
            }
            Cursor.Current = Cursors.Default;
            if (formFromRptBillPOReport.ShowDialog() == DialogResult.OK)
            {
                formFromRptBillPOReport.crystalReportViewer2.Refresh();
                formFromRptBillPOReport.Dispose();
                 formFromRptBillPOReport = null;
            }
        }

        private void comboBoxCando_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                string srPName = comboBoxCando.Text;
                int invID = Int32.Parse( comboBoxSelectInven.SelectedValue.ToString());


                if (srPName.Length == 1 && invID > 0)
                {
                    this.dataAllPOHeader.DefaultView.RowFilter = string.Format("MemStatusCanDo like '*{0}*' and InventoryIDIN = '{1}' ", srPName, invID);

                }
                else if (srPName.Length == 1 && invID == 0)
                {
                    this.dataAllPOHeader.DefaultView.RowFilter = string.Format("MemStatusCanDo like '*{0}*'  ", srPName );
                }
                else if (srPName.Length > 1 && invID > 0)
                {
                    this.dataAllPOHeader.DefaultView.RowFilter = string.Format("InventoryIDIN = '{0}' ",   invID);
                }
                else
                {
                    this.dataAllPOHeader.DefaultView.RowFilter = string.Format(" 1 = 1 ", srPName);
                }
                 

            }
            catch (Exception ex)
            {

            }
        }

        private void comboBoxInventoryFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxInventoryFrom.Text == comboBoxInventoryTo.Text)
                comboBoxFlagStock.Visible = true;
            else
                comboBoxFlagStock.Visible = false;
        }

        private void buttonupdateRemarkHeader_Click(object sender, EventArgs e)
        {
            string poHeaderRemark = textBoxRemPOHeader.Text.ToString();

            try
            {
                int result = gd.updatePOHeaderRem(this.poOrderNo, poHeaderRemark);

                if (result <= 0)
                {
                    MessageBox.Show("ไม่สามารถอนุมัติรายการถัดไปได้");
                }

                dataAllPOHeader = gd.getAllPOHeader("0");
                dataGridViewPOHeader.DataSource = dataAllPOHeader;

            }
            catch (Exception ex)
            {

            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

            string flagSupplier = comboBoxFlagSupplier.Text;

 
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List
            data.Add(new KeyValuePair<int, string>(0, "= เลือกต้นทาง ="));



            if (flagSupplier == "All")
            {
                foreach (Inventory c in allInven)
                {
                    if (c.FlagUse == "Y")
                        data.Add(new KeyValuePair<int, string>(c.InventoryID, c.InventoryName));

                } 
            }
            else if (flagSupplier == "Supplier")
            {
                foreach (Inventory cc in allInven)
                {
                    if (cc.FlagSuplier == "Y" & cc.FlagUse == "Y")
                        data.Add(new KeyValuePair<int, string>(cc.InventoryID, cc.InventoryName));
                }
            }
            else if (flagSupplier == "คลังสินค้า")
            {
                foreach (Inventory ccc in allInven)
                {
                    if (ccc.FlagSuplier == "N" & ccc.FlagUse == "Y")
                        data.Add(new KeyValuePair<int, string>(ccc.InventoryID, ccc.InventoryName));
                }
            }


            // Clear the combobox
            comboBoxInventoryFrom.DataSource = null;
            comboBoxInventoryFrom.Items.Clear();

            // Bind the combobox
            comboBoxInventoryFrom.DataSource = new BindingSource(data, null);
            comboBoxInventoryFrom.DisplayMember = "Value";
            comboBoxInventoryFrom.ValueMember = "Key";



            List<KeyValuePair<int, string>> data2 = new List<KeyValuePair<int, string>>();


            if (flagSupplier == "All")
            {
                foreach (POType c in allPOType)
                {
                    data2.Add(new KeyValuePair<int, string>(c.PoTypeID, c.PoType));
                }
            }
            else if (flagSupplier == "Supplier")
            {
                foreach (POType cc in allPOType)
                {
                    if (cc.PoSupplierType == "Y"  )
                        data2.Add(new KeyValuePair<int, string>(cc.PoTypeID, cc.PoType));
                }
            }
            else if (flagSupplier == "คลังสินค้า")
            {
                foreach (POType ccc in allPOType)
                {
                    if (ccc.PoSupplierType != "Y")
                        data2.Add(new KeyValuePair<int, string>(ccc.PoTypeID, ccc.PoType));
                }
            }

  

            // Clear the combobox
            comboBoxType.DataSource = null;
            comboBoxType.Items.Clear();

            // Bind the combobox
            comboBoxType.DataSource = new BindingSource(data2, null);
            comboBoxType.DisplayMember = "Value";
            comboBoxType.ValueMember = "Key";
 
        }
        
        private void txtBoxAddStoreQTY_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                txtBoxAddStoreAmt.Text = ( this.storeSelectedCost * float.Parse(txtBoxAddStoreQTY.Text)).ToString() ; 
            }
            catch (Exception ex)
            {

            }
        }

     

  



    }
}
