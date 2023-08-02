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
    public partial class AddPO_Branch : AddDataTemplate
    {
        List<Store> allStores;
        List<Store> allStoresByCat;
        List<Inventory> allInven;
        List<PODetail> dataStock;
        List<PODetail> dataStockByStoreID;
        List<POType> allPOType;


        DataTable dataAllStore;    
        DataTable dataAllStore_SRC;
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
        FromRptBillPOReport_Branch formFromRptBillPOReport_Branch;

        string strSystem = "";


        public AddPO_Branch(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();


            strSystem = Login.posBranchID.ToString(); // ConfigurationSettings.AppSettings["POSIDConfig"].ToString();

            dateTimePicker_POHSelect.Value = DateTime.Now;

            this.Width = 1024;
            this.Height = 768;

            gd = new GetDataRest();

            try
            {

                allStores = gd.getListAllStore(0, 0, "000");
                allStoresByCat = gd.getListAllStore(0, 0, "000");
                getComboAllStore();
                

               // MessageBox.Show("1");

                allStoreCat = gd.getListAllStoreCat();
                getComboAllStoreCat();

              //  MessageBox.Show("2");
                //getComboAllStoreCat_Take();

              //  MessageBox.Show("3");

                allInven = gd.getAllInventory(0,"0","0","0");
                getComboAllInventory();


                allPOType = gd.getAllPOType(0, "0");
                getComboAllPOType();

              //  this.selectedStoreCat = 0;
                dataAllStore = gd.getAllStoreTake(Login.posBranchID);
                dataGridViewAllStore.DataSource = dataAllStore;

                if (dataGridViewAllStore.RowCount > 0)
                {
                    dataGridViewAllStore.Columns[0].Visible = false;
                    dataGridViewAllStore.Columns[1].Visible = false;
                    dataGridViewAllStore.Columns[3].Visible = false;

                    dataGridViewAllStore.Columns[2].HeaderText = "หมวด";
                    dataGridViewAllStore.Columns[4].HeaderText = "InvCode";
                    dataGridViewAllStore.Columns[5].HeaderText = "รายการ";
                    dataGridViewAllStore.Columns[6].HeaderText = "เบิก";
                    dataGridViewAllStore.Columns[7].HeaderText = "หน่วย(ใหญ่)";
                    dataGridViewAllStore.Columns[8].HeaderText = "เบิก";
                    dataGridViewAllStore.Columns[9].HeaderText = "หน่วย(เล็ก)";
                    dataGridViewAllStore.Columns[10].HeaderText = "Rate";
                    dataGridViewAllStore.Columns[11].HeaderText = "เหตุผล";
                    dataGridViewAllStore.Columns[12].Visible = false;
                    dataGridViewAllStore.Columns[13].Visible = false;

                     

                }

              
                poSelectProcessID = 0; 

                textBoxPONo.Text = gd.getNextPO("");  

                setDefault();

                dataAllPOHeader = gd.getAllPOHeader("0");
                dataGridViewPOHeader.DataSource = dataAllPOHeader;

                comboBoxCurPOProcess.SelectedIndex = 0;
                comboBoxSelectInven.SelectedValue = 0;
                comboBoxCando.Text = "ALL";

                if (dataGridViewPOHeader.RowCount > 0)
                { 
                    dataGridViewPOHeader.Columns[1].HeaderText = "เลขที่";
                    dataGridViewPOHeader.Columns[2].HeaderText = "วันที่รับของ";
                    dataGridViewPOHeader.Columns[3].Visible = false;
                    dataGridViewPOHeader.Columns[4].HeaderText = "จากคลัง";
                    dataGridViewPOHeader.Columns[5].Visible = false;
                    dataGridViewPOHeader.Columns[6].HeaderText = "เข้าคลัง";
                    dataGridViewPOHeader.Columns[7].HeaderText = "ประเภท";
                    dataGridViewPOHeader.Columns[8].HeaderText = "QTY";
                    dataGridViewPOHeader.Columns[9].HeaderText = "เป็นเงิน";
                }


                dataAllStore_SRC = gd.getAllStore(0);
                dataGridViewAllStoreSearch.DataSource = dataAllStore_SRC;

                dataGridViewAllStoreSearch.Columns[0].Visible = false;
                dataGridViewAllStoreSearch.Columns[1].Visible = false;
                dataGridViewAllStoreSearch.Columns[2].Visible = false;
                dataGridViewAllStoreSearch.Columns[4].Visible = false;
                dataGridViewAllStoreSearch.Columns[5].Visible = false;
                dataGridViewAllStoreSearch.Columns[6].Visible = false;
                dataGridViewAllStoreSearch.Columns[7].Visible = false;
                dataGridViewAllStoreSearch.Columns[8].Visible = false;
                dataGridViewAllStoreSearch.Columns[9].Visible = false;
                dataGridViewAllStoreSearch.Columns[10].Visible = false;
                dataGridViewAllStoreSearch.Columns[12].Visible = false;
                dataGridViewAllStoreSearch.Columns[13].Visible = false;
                dataGridViewAllStoreSearch.Columns[14].Visible = false;
                dataGridViewAllStoreSearch.Columns[15].Visible = false;

           

            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
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
                //if (Login.posBranchID == 1)
                //{
                //    data.Add(new KeyValuePair<int, string>(c.StoreCatID, c.StoreCatName));
                //}
                //else if (Login.posBranchID == 2)
                //{
                //    if (c.StoreCatName.Substring(0, 2) == "13" || c.StoreCatID == 11)
                //        data.Add(new KeyValuePair<int, string>(c.StoreCatID, c.StoreCatName)); 

                //}else if (Login.posBranchID >= 3)
                //{ 
                //    if( c.StoreCatName.Substring(0,2) == "11" || c.StoreCatName.Substring(0,2) == "12" )
                //         data.Add(new KeyValuePair<int, string>(c.StoreCatID, c.StoreCatName)); 
                //}

                data.Add(new KeyValuePair<int, string>(c.StoreCatID, c.StoreCatName));
            }


            // Clear the combobox
            comboBoxAllCat_Take.DataSource = null;
            comboBoxAllCat_Take.Items.Clear();

            // Bind the combobox
            comboBoxAllCat_Take.DataSource = new BindingSource(data, null);
            comboBoxAllCat_Take.DisplayMember = "Value";
            comboBoxAllCat_Take.ValueMember = "Key";

            comboBoxAllCat_Take.SelectedValue = selectedStoreCat; 
             

        }

        private void getComboAllStoreCat_Take()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List
            data.Add(new KeyValuePair<int, string>(0, "==== กลุ่มวัตถุดิบ ===="));

            foreach (StoreCat c in allStoreCat)
            {
                //if (Login.posBranchID == 1)
                //{
                //    data.Add(new KeyValuePair<int, string>(c.StoreCatID, c.StoreCatName));
                //}
                //else if (Login.posBranchID == 2)
                //{
                //    if (c.StoreCatName.Substring(0, 2) == "13")
                //        data.Add(new KeyValuePair<int, string>(c.StoreCatID, c.StoreCatName));

                //}
                //else if (Login.posBranchID >= 3)
                //{
                //    if (c.StoreCatName.Substring(0, 2) == "11" || c.StoreCatName.Substring(0, 2) == "12")
                //        data.Add(new KeyValuePair<int, string>(c.StoreCatID, c.StoreCatName));
                //}

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
           // getComboAllStore();


        }


        private void getComboAllInventory()
        {
             List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List
            data.Add(new KeyValuePair<int, string>(0, "= เลือกต้นทาง ="));

            foreach (Inventory c in allInven)
            {
                data.Add(new KeyValuePair<int, string>(c.InventoryID, c.InventoryName)); 
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
            data.Add(new KeyValuePair<int, string>(0, "= เลือกปลายทาง ="));

            foreach (Inventory c in allInven)
            {
                data.Add(new KeyValuePair<int, string>(c.InventoryID, c.InventoryName));
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
             //   selectedStoreCat = Int32.Parse(comboBoxAllCat.SelectedValue.ToString());
             //   allStoresByCat = gd.getListAllStore(selectedStoreCat, 0, "000");
             ////   dataGridViewAllStore.DataSource = allStoresByCat;
             //   getComboAllStore();
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

               // comboBoxListStore.SelectedIndex = 0;

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
                 

            }
            catch (Exception ex)
            {

            }

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            txtBoxAddStoreQTY.Text = "0";
            buttonAddTable_Click(sender, e);

        }

        private void setDefault()
        { 

            refreshPODetail();

            dateTimePickerAddStock.Value = DateTime.Now;
            txtBoxAddStoreQTY.Text = "0";
            txtBoxBigQTY.Text = "0";
            txtBoxAddStoreAmt.Text = "0";
            txtBoxAddStoreRemark.Text = "เพิ่มเติม";
           // textBoxRemPOHeader.Text = "เพิ่มเติม"; 
           

            flagSave = 0;
            FlagSaveChange();

            comboBoxInventoryFrom.SelectedValue = 1; // ครัวกลาง  
            comboBoxInventoryTo.SelectedValue = Login.posBranchID; // Auto เลือกคลัง

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

                string poHeaderRemark = "";// textBoxRemPOHeader.Text.ToString();
                
                float storeaddStoreQTY= (float)Double.Parse(txtBoxAddStoreQTY.Text);
                string dateAddStock = dateTimePickerAddStock.Value.ToString("yyyyMMdd");
                string storeName = comboBoxListStore.Text;
                string addStockType = comboBoxType.Text;

                //float storeaddStoreAmt = (float)Double.Parse(txtBoxAddStoreAmt.Text);

                //float storeaddStoreVat = 0;

                //if (checkBoxVat.Checked == true)
                //    storeaddStoreVat = storeaddStoreAmt * (float)0.07;

                float storeaddStoreAmt = 0;
                float storeaddStoreVat = 0;

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

                    if (MessageBox.Show("คุณต้องการจะเพิ่มข้อมูล  : " + storeName + " ใน PO" + poNO +" หรือไม่ ?", "เพิ่ม/ลบ " + storeName, MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                int addStoreID = Int32.Parse(dataGridViewAddStoreDetail["StoreID", e.RowIndex].Value.ToString());
                 
                flagSave = 1;
                FlagSaveChange();

                foreach (PODetail c in dataStock)
                {
                    if (c.AddStockID == addStockID)
                    {
                       // MessageBox.Show(c.StoreName);

                        comboBoxListStore.Text = c.StoreName;
                        txtBoxBigQTY.Text = c.StoreBigQTY.ToString();
                        txtBoxAddStoreQTY.Text = c.StoreQTY.ToString();
                        txtBoxAddStoreAmt.Text = c.StoreAmount.ToString();
                        txtBoxAddStoreRemark.Text = c.StoreRemark;
                         

                        if (c.StoreVat > 0)
                            checkBoxVat.Checked = true;
                         
                    }
                }

                foreach (Store st in allStores)
                {
                    if (st.StoreID == addStoreID)
                    {
                        this.convertRate = st.StoreConvertRate;
                        labelUnit.Text = st.StoreUnit;
                        labelUnitConvert.Text = st.StoreConvertUnit;
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
               // labelHeader.Text = "เพิ่มใบเบิกสินค้า";
                buttonSave.Text = "เพิ่มข้อมูล";
                comboBoxListStore.Enabled = true;
                comboBoxAllCat.Enabled = true;
                dateTimePickerAddStock.Enabled = true;
            }
            else
            {
               // labelHeader.Text = "แก้ไขใบเบิกสินค้า";
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
            panelTakeOne.Visible = true;
        }

        private void buttonClosePN_Click(object sender, EventArgs e)
        {
            panelTakeOne.Visible = false;
        }

        private void textBoxSrcStoreName_TextChanged(object sender, EventArgs e)
        {

            try
            {

                string srPName = textBoxSrcStoreName.Text;
                string srInvCode = textBoxSrcInvCodeName.Text;

                int srStoreCatID = Int32.Parse(comboBoxAllCat_Take.SelectedValue.ToString());

                if (srInvCode.Length == 0)
                {
                    if ((srPName.Length > 0) && (srStoreCatID > 0))
                    {
                        this.dataAllStore.DefaultView.RowFilter = string.Format("StoreName like '*{0}*' and StoreCatID = '{1}' ", srPName, srStoreCatID);
                    }
                    else if ((srPName.Length == 0) && (srStoreCatID > 0))
                    {
                        this.dataAllStore.DefaultView.RowFilter = string.Format("StoreCatID = '{0}' ", srStoreCatID);
                    }
                    else if ((srPName.Length > 0) && (srStoreCatID == 0))
                    {
                        this.dataAllStore.DefaultView.RowFilter = string.Format("StoreName like '*{0}*'", srPName);
                    }
                    else
                    {
                        this.dataAllStore.DefaultView.RowFilter = string.Format("1 = 1", 1);  // All
                    }
                }
                else
                {
                    if ((srPName.Length > 0) && (srStoreCatID > 0))
                    {
                        this.dataAllStore.DefaultView.RowFilter = string.Format("StoreName like '*{0}*' and StoreCatID = '{1}' and StoreBarcode like '*{2}*'", srPName, srStoreCatID, srInvCode);
                    }
                    else if ((srPName.Length == 0) && (srStoreCatID > 0))
                    {
                        this.dataAllStore.DefaultView.RowFilter = string.Format("StoreCatID = '{0}'  and StoreBarcode like '*{1}*' ", srStoreCatID, srInvCode);
                    }
                    else if ((srPName.Length > 0) && (srStoreCatID == 0))
                    {
                        this.dataAllStore.DefaultView.RowFilter = string.Format("StoreName like '*{0}*' and StoreBarcode like '*{1}*' ", srPName, srInvCode);
                    }
                    else
                    {
                        this.dataAllStore.DefaultView.RowFilter = string.Format("StoreBarcode like '*{0}*'", srInvCode);  // All
                    }


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
                 

               // comboBoxAllCat.SelectedValue = dataGridStoreCatID;
                comboBoxListStore.SelectedValue = dataGridStoreID;
                
            }
            catch (Exception ex)
            {

            }
        }

        private void refreshPOFormCode()
        {

            try
            {
                if (this.poOrderNo.Length > 1)
                {

                    DataTable ph = gd.getAllPOHeader(this.poOrderNo);

                    if (ph.Rows.Count > 0)
                    {

                        this.poOrderNo = textBoxPONo.Text;

                        Object procesBackObj = ph.Rows[0]["BackProcessName"];
                        Object procesCurrentObj = ph.Rows[0]["CurrentProcessName"];
                        Object procesNextObj = ph.Rows[0]["NextProcessName"];
                        Object poSelectProcessIDObj = ph.Rows[0]["ProcessID"];

                        Object poINVFrom = ph.Rows[0]["InventoryIDOUT"];
                        Object poINVTo = ph.Rows[0]["InventoryIDIN"];
                        Object poDate = ph.Rows[0]["PODate"];

                        string procesBack = (string)procesBackObj;
                        string procesCurrent = (string)procesCurrentObj;
                        string procesNext = (string)procesNextObj;
                        poSelectProcessID = Int32.Parse(poSelectProcessIDObj.ToString());


                        dateTimePickerAddStock.Value = DateTime.Parse(poDate.ToString());
                        comboBoxInventoryFrom.SelectedValue = Int32.Parse(poINVFrom.ToString());
                        comboBoxInventoryTo.SelectedValue = Int32.Parse(poINVTo.ToString());




                        buttonBackProcess_1.Text = procesBack;
                        buttonNextProcess_1.Text = procesNext;
                        textBoxProcessNow_1.Text = procesCurrent;

                        if (poSelectProcessID == 1)
                            buttonBackProcess_1.Visible = false;
                        else
                            buttonBackProcess_1.Visible = true;


                        if (poSelectProcessID == 3)
                        {
                            buttonNextProcess_1.Visible = false;
                            buttonBackProcess_1.Visible = false;
                        }
                        else
                        {
                            buttonNextProcess_1.Visible = true;
                        }

                        refreshPODetail();

                    }

                }

            }
            catch (Exception ex)
            {

            }

        }


        private void textBoxPONo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                panelTakeOne.Visible = true;

                this.poOrderNo = textBoxPONo.Text;
                refreshPOFormCode();
            }
                  
          
        }

        private void refreshPODetail()
        {

            try
            {
            if (textBoxPONo.Text.Length > 0)
            {
                dataStock = gd.getAllPODetail(textBoxPONo.Text, 0);
                dataGridViewAddStoreDetail.DataSource = dataStock;

                dataGridViewAddStoreDetail.Columns[1].Visible = false;
                dataGridViewAddStoreDetail.Columns[2].Visible = false;
                dataGridViewAddStoreDetail.Columns[3].Visible = false;
                dataGridViewAddStoreDetail.Columns[4].Visible = false;
                dataGridViewAddStoreDetail.Columns[13].Visible = false;
                dataGridViewAddStoreDetail.Columns[14].Visible = false;
            }

                  }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



      


      

        private void buttonNewPO_Click(object sender, EventArgs e)
        {
            try
            {
                setDefault(); 
                refreshPODetail();
            }
            catch (Exception ex)
            {

            }
            finally
            {
              //  panelHeaderProcess.Visible = false;

                int DateUSE = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));

                if (DateUSE > 25590101)
                    DateUSE = DateUSE - 543;
                 


                string newpoOrderNo = gd.getNextPO(strSystem).ToString();

                newpoOrderNo = DateUSE.ToString() + strSystem + ("00" + newpoOrderNo).Right(2);

                textBoxPONo.Text = newpoOrderNo;
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

     

        private void comboBoxInventoryFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxInventoryFrom.Text == comboBoxInventoryTo.Text)
                comboBoxFlagStock.Visible = true;
            else
                comboBoxFlagStock.Visible = false;
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

        private void buttonPNPODetail_Click(object sender, EventArgs e)
        {
            refreshPODetail();

            if (panelTakeOne.Visible)
            {
                panelTakeOne.Visible = false;
                buttonPNPODetail.Text = "ดูรายการเบิกสินค้า";
            }
            else
            {
                panelTakeOne.Visible = true;
                buttonPNPODetail.Text = "ซ่อนรายการเบิกสินค้า";
            }
        }

        private void buttonAddTake_Click(object sender, EventArgs e)
        {
            try
            {

                /// Header
                /// 


                comboBoxAllCat_Take.SelectedIndex = 0;

                this.poOrderNo = textBoxPONo.Text;

                int invenOutID = 0;
                int invenInID = 0;
                int selectStoreID = 0;

                int addstocksign = 1;

                if (comboBoxFlagStock.Text == "นำออก" && comboBoxFlagStock.Visible == true)
                    addstocksign = -1;


               
                invenInID = Int32.Parse(comboBoxInventoryFrom.SelectedValue.ToString());
                invenOutID = Int32.Parse(comboBoxInventoryTo.SelectedValue.ToString());
                selectStoreID = Int32.Parse(comboBoxListStore.SelectedValue.ToString());
                

                string poHeaderRemark = "";// textBoxRemPOHeader.Text.ToString();

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

                float addUnits = 0;


                List<StockChange> stcLists = new List<StockChange>();

                foreach (DataGridViewRow row in dataGridViewAllStore.Rows)
                {
                    if (row.Cells["TakeBigUnit"].Value.ToString() != "0" ||
                        row.Cells["TakeUnit"].Value.ToString() != "0"  )
                    {
                        storeID = Int32.Parse(row.Cells["StoreID"].Value.ToString());
                        storeName = row.Cells["StoreName"].Value.ToString();
                        storeUnit = row.Cells["StoreUnit"].Value.ToString();
                        storeBigUnit = row.Cells["StoreConvertUnit"].Value.ToString();
                        storeAddBigUnit = float.Parse(row.Cells["TakeBigUnit"].Value.ToString());
                        storeAddUnit = float.Parse(row.Cells["TakeUnit"].Value.ToString()); 
                        storeConvertRate = float.Parse(row.Cells["StoreConvertRate"].Value.ToString());
                        storeRemark = row.Cells["StoreRemark"].Value.ToString();


                        storeAddUnit = storeAddBigUnit * storeConvertRate + storeAddUnit;

                        storeAddBigUnit = storeAddUnit / storeConvertRate;
                       
                        stcLists.Add(new StockChange(storeID, storeName, storeAddBigUnit, storeBigUnit, storeAddUnit, storeUnit,0, storeRemark));
                    
                    }
                }

                string txtInsert = "ทำเบิกสินค้า ดังนี้ \n\r";
                int i = 1;

                string str1 = "";
                string str2 = ""; 

                foreach (StockChange st in stcLists)
                {
                    str1 = i.ToString() + ". " + st.StoreName;

                    str2 = " เบิก : " + st.StoreAddBigUnit.ToString("###,###.##") + " " + st.StoreBigUnit + " (" + st.StoreAddUnit.ToString("###,###.##") + " " + st.StoreUnit + ")";
                     
                    txtInsert += str1 + str2  + "\n\r";
                    i++;
                }



                if (this.poOrderNo.Length == 0)
                    throw new Exception("กรุณาระบุเลขที่ใบ PO");

                if (poSelectProcessID == 0 && (flagSave == 0))
                {

                    if (invenOutID == 0)
                        throw new Exception("กรุณาเลือกคลังปลายทาง");

                    if (invenInID == 0)
                        throw new Exception("กรุณาเลือกคลังต้นทาง");

                }


                int result = 0;

                if (MessageBox.Show("คุณต้องการทำรายการ \n\r" + txtInsert + " หรือไม่ ?", "ทำรายการเพิ่มข้อมูล Stock", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    foreach (StockChange st in stcLists)
                    {

                        result = gd.instNewPO(this.poOrderNo, invenOutID, invenInID, poHeaderRemark, st.StoreID, dateAddStock, st.StoreAddUnit * addstocksign, 0, 0, storeRemark, "", "เบิกสินค้า");
                          
                    }
                }


                if (result > 0)
                {
                    dataAllPOHeader = gd.getAllPOHeader("0");
                    dataGridViewPOHeader.DataSource = dataAllPOHeader;
                    refreshPOFormCode();
                    refreshPODetail(); 
                    panelTakeOne.Visible = true;
                    dataAllStore = gd.getAllStoreTake(Login.posBranchID);
                    dataGridViewAllStore.DataSource = dataAllStore;
                    MessageBox.Show("ทำรายการเบิกสำเร็จ");
                }
                 
               // defaultStock();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      // tab 2

        private void dataGridViewPOHeader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

             //   panelHeaderProcess.Visible = true;

                poOrderNo = dataGridViewPOHeader.Rows[e.RowIndex].Cells["POOrderNo"].Value.ToString();

                poSelectProcessID = Int32.Parse(dataGridViewPOHeader.Rows[e.RowIndex].Cells["ProcessID"].Value.ToString());

                string procesBack = dataGridViewPOHeader.Rows[e.RowIndex].Cells["BackProcessName"].Value.ToString();
                string procesCurrent = dataGridViewPOHeader.Rows[e.RowIndex].Cells["CurrentProcessName"].Value.ToString();
                string procesNext = dataGridViewPOHeader.Rows[e.RowIndex].Cells["NextProcessName"].Value.ToString();
                string memcandonext = dataGridViewPOHeader.Rows[e.RowIndex].Cells["MemStatusCanDo"].Value.ToString();
                string poHeaderRemark = dataGridViewPOHeader.Rows[e.RowIndex].Cells["POHeaderRemark"].Value.ToString();

                //  MessageBox.Show(memcandonext);

                textBoxPONo_Sum.Text = poOrderNo;
                textBoxProcessNow.Text = procesCurrent;
                buttonBackProcess.Text = procesBack;
                buttonNextProcess.Text = procesNext;

             //   textBoxRemPOHeader.Text = poHeaderRemark;

                // CASE processCurrentID Min = 1

                if (poSelectProcessID == 1)
                    buttonBackProcess.Visible = false;
                else
                    buttonBackProcess.Visible = true;


                if (poSelectProcessID == 3)
                {
                    buttonNextProcess.Visible = false;
                    buttonBackProcess.Visible = false;
                }
                else
                {
                    buttonNextProcess.Visible = true;
                }



                if (Login.userStatus == "UserPR" )
                 {
                        buttonNextProcess.Visible = false;

                       
                }

                if ( Login.userStatus == "UserStock")
                {
                    buttonNextProcess.Visible = false;

                    if (poSelectProcessID == 1)
                    {
                        buttonNextProcess.Visible = true;
                    }
                }
                

 
                flagSave = 0;
                FlagSaveChange();

                refreshPODetail_Sum();

            }
            catch (Exception ex)
            {

            }
        }

        private void refreshPODetail_Sum()
        {
            if (textBoxPONo.Text.Length > 0)
            {
                dataStock = gd.getAllPODetail(textBoxPONo_Sum.Text, 0);
                dataGridViewAddStoreDetail_Sum.DataSource = dataStock;

                dataGridViewAddStoreDetail_Sum.Columns[1].Visible = false;
                dataGridViewAddStoreDetail_Sum.Columns[2].Visible = false;
                dataGridViewAddStoreDetail_Sum.Columns[3].Visible = false;
                dataGridViewAddStoreDetail_Sum.Columns[4].Visible = false;
                dataGridViewAddStoreDetail_Sum.Columns[13].Visible = false;
                dataGridViewAddStoreDetail_Sum.Columns[14].Visible = false;
            }
        }

        private void dateTimePicker_POHSelect_ValueChanged(object sender, EventArgs e)
        {
            dataGridPOHeader_SelectChange();
        }

        private void comboBoxSelectInven_SelectedValueChanged(object sender, EventArgs e)
        {
            dataGridPOHeader_SelectChange();
        }

        private void comboBoxCando_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridPOHeader_SelectChange();
        }

        private void comboBoxCurPOProcess_SelectedValueChanged(object sender, EventArgs e)
        {
            dataGridPOHeader_SelectChange();
        }


        private void dataGridPOHeader_SelectChange()
        {
            try
            {

                string srDate = dateTimePicker_POHSelect.Value.ToShortDateString();
                 

                int invID = Int32.Parse(comboBoxSelectInven.SelectedValue.ToString());



                if (comboBoxCando.Text == "DATE")
                {
                   //MessageBox.Show(srDate.ToString());

                    if (invID == 0)
                    {
                        if (comboBoxCurPOProcess.SelectedIndex == 0)
                            this.dataAllPOHeader.DefaultView.RowFilter = string.Format("PODate = '{0}' ", srDate);
                        else
                            this.dataAllPOHeader.DefaultView.RowFilter = string.Format("PODate = '{0}' and CurrentProcessName like '*{1}*' ", srDate, comboBoxCurPOProcess.Text);
                    }
                    else
                    {
                        if (comboBoxCurPOProcess.SelectedIndex == 0)
                            this.dataAllPOHeader.DefaultView.RowFilter = string.Format("PODate = '{0}' and InventoryIDIN = {1} ", srDate, invID);
                        else
                            this.dataAllPOHeader.DefaultView.RowFilter = string.Format("PODate = '{0}' and InventoryIDIN = {1} and CurrentProcessName like '*{2}*'", srDate, invID, comboBoxCurPOProcess.Text);               
                    }

                }
                else
                {
                    if (invID == 0)
                    {
                        if (comboBoxCurPOProcess.SelectedIndex == 0)
                            this.dataAllPOHeader.DefaultView.RowFilter = string.Format(" 1 = 1 ");
                        else
                            this.dataAllPOHeader.DefaultView.RowFilter = string.Format("CurrentProcessName like '*{0}*' ",  comboBoxCurPOProcess.Text);
                    }
                    else
                    {
                        if (comboBoxCurPOProcess.SelectedIndex == 0)
                            this.dataAllPOHeader.DefaultView.RowFilter = string.Format(" InventoryIDIN = {0} ",   invID);
                        else
                            this.dataAllPOHeader.DefaultView.RowFilter = string.Format(" InventoryIDIN = {0} and CurrentProcessName like '*{1}*'",   invID, comboBoxCurPOProcess.Text);
                    }
                }




                //if (comboBoxCando.Text == "DATE" && invID > 0 && comboBoxCurPOProcess.SelectedIndex > 0)
                //{
                //    this.dataAllPOHeader.DefaultView.RowFilter = string.Format("PODate = '{0}' and InventoryIDIN = '{1}' and CurrentProcessName like '*{2}*' ", srDate, invID, comboBoxCurPOProcess.Text);

                //}
                //else if (comboBoxCando.Text == "DATE" && invID == 0 && comboBoxCurPOProcess.SelectedIndex > 0)
                //{
                //    this.dataAllPOHeader.DefaultView.RowFilter = string.Format("PODate like '*{0}*' and CurrentProcessName like '*{2}*' ", srDate, invID, comboBoxCurPOProcess.Text);

                //}
                //else if (comboBoxCando.Text == "ALL" && invID > 0 && comboBoxCurPOProcess.SelectedIndex > 0)
                //{
                //    this.dataAllPOHeader.DefaultView.RowFilter = string.Format(" InventoryIDIN = '{1}' and CurrentProcessName like '*{2}*' ", srDate, invID, comboBoxCurPOProcess.Text);

                //}
                //else if (comboBoxCando.Text == "DATE" && invID == 0 && comboBoxCurPOProcess.SelectedIndex == 0)
                //{
                //    this.dataAllPOHeader.DefaultView.RowFilter = string.Format("PODate like '*{0}*'  ", srDate);
                //}
                //else if (comboBoxCando.Text == "ALL" && invID > 0 && comboBoxCurPOProcess.SelectedIndex == 0)
                //{
                //    this.dataAllPOHeader.DefaultView.RowFilter = string.Format("InventoryIDIN = '{0}' ", invID);
                //}
                //else if (comboBoxCando.Text == "ALL" && invID == 0 && comboBoxCurPOProcess.SelectedIndex > 0)
                //{
                //    this.dataAllPOHeader.DefaultView.RowFilter = string.Format("CurrentProcessName like '*{0}*' ", comboBoxCurPOProcess.Text);

                //}
                //else
                //{
                //    this.dataAllPOHeader.DefaultView.RowFilter = string.Format(" 1 = 1 ", srDate);
                //}


            }
            catch (Exception ex)
            {

            }
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            // Next Process

            Button bt = (Button)sender;
            int addPOStatus = 0;

            if (bt.Name.Contains("buttonNextProcess"))
                addPOStatus = 1;
            else
                addPOStatus = -1;

         //   string poHeaderRemark = textBoxRemPOHeader.Text.ToString();

            try
            {
                int result = gd.getNextProcessPO(this.poOrderNo, "", addPOStatus);

                if (result <= 0)
                {
                    MessageBox.Show("ไม่สามารถอนุมัติรายการถัดไปได้");
                }
                else
                {
                    MessageBox.Show("ทำรายการสำเร็จ " + bt.Text);

                    dataAllPOHeader = gd.getAllPOHeader("0");
                    dataGridViewPOHeader.DataSource = dataAllPOHeader;

                    buttonBackProcess.Text = "Back";
                    buttonNextProcess.Text = "Next";

                    textBoxProcessNow.Text = "";



                   // refreshPODetail_Sum();
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
                 
                LinkFromRptBillReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonPrintConfirmPO1_Click(object sender, EventArgs e)
        {
            try
            { 
                LinkFromRptBillReport_Branch(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LinkFromRptBillReport_Branch()
        {
            Cursor.Current = Cursors.WaitCursor;

            int poproID = Int32.Parse(comboBoxCurPOProcess.SelectedIndex.ToString());
            string date = dateTimePicker_POHSelect.Value.ToString("yyyyMMdd");

            if (formFromRptBillPOReport_Branch == null)
            {
                formFromRptBillPOReport_Branch = new FromRptBillPOReport_Branch(this, 0, "0", date, poproID);
            }
            else
            {
                formFromRptBillPOReport_Branch.rptBillPO = "0";
                formFromRptBillPOReport_Branch.poDate = date;
                formFromRptBillPOReport_Branch.processType = poproID;
                
                formFromRptBillPOReport_Branch.viewReport();

            }
            Cursor.Current = Cursors.Default;
            if (formFromRptBillPOReport_Branch.ShowDialog() == DialogResult.OK)
            {
                formFromRptBillPOReport_Branch.crystalReportViewer2.Refresh();
                formFromRptBillPOReport_Branch.Dispose();
                formFromRptBillPOReport_Branch = null;
            }
        }

        private void txtBoxBigQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                txtBoxAddStoreQTY.Text = ((float)Double.Parse(txtBoxBigQTY.Text) * this.convertRate).ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void buttonNewTF_Click(object sender, EventArgs e)
        {   
            textBoxPONo.Text = gd.getNextPO("");  
        }

        private void buttonHeaderTF_Click(object sender, EventArgs e)
        {
            try
            { 

                int addstocksign = 1; 

                string poNO = textBoxPONo.Text;

                int invenOutID = 0;
                int invenInID = 0;
                int selectStoreID = 0;

              // int addstocksign = 1;

                if (comboBoxFlagStock.Text == "นำออก" && comboBoxFlagStock.Visible == true)
                    addstocksign = -1;
                 
                 invenOutID = Int32.Parse(comboBoxInventoryFrom.SelectedValue.ToString());
                 invenInID  = Int32.Parse(comboBoxInventoryTo.SelectedValue.ToString()); 


                int dateAddStockINT = Int32.Parse(dateTimePickerAddStock.Value.ToString("yyyyMMdd"));

                if (dateAddStockINT > 25590101)
                    dateAddStockINT = dateAddStockINT - 5430000;

                string addType = comboBoxType.Text ;

                if (MessageBox.Show("คุณต้องการจะแก้ไข Header TF   : " + poNO + " หรือไม่ ?", "แก้ไข TF Header ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.updsTFHeader(poNO, invenInID, invenOutID, addType, dateAddStockINT.ToString());

                    if (result > 0)
                    {
                        dataAllPOHeader = gd.getAllPOHeader("0");
                        dataGridViewPOHeader.DataSource = dataAllPOHeader;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void buttonPRSearchStore_Click(object sender, EventArgs e)
        {
            panelSearchStore.Visible = true;
        }

        private void buttonCloseSrcStorePN_Click(object sender, EventArgs e)
        {
            panelSearchStore.Visible = false;
        }

        private void dataGridViewAllStoreSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

              //  int dataGridStoreCatID = Int32.Parse(dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreCatID"].Value.ToString());
                int dataGridStoreID = Int32.Parse(dataGridViewAllStoreSearch.Rows[e.RowIndex].Cells["StoreID"].Value.ToString());


             //   comboBoxAllCat.SelectedValue = dataGridStoreCatID;
                comboBoxListStore.SelectedValue = dataGridStoreID;

            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxSrcInvCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string srPName = textBoxSrcInvName.Text;
                string invCode = textBoxSrcInvCode.Text;


                if (srPName.Length > 0 && invCode.Length > 0)
                {
                    this.dataAllStore_SRC.DefaultView.RowFilter = string.Format("StoreName like '*{0}*' and StoreOrder like '*{1}*' ", srPName, invCode);

                }
                else if (srPName.Length == 0 && invCode.Length > 0)
                {
                    this.dataAllStore_SRC.DefaultView.RowFilter = string.Format("StoreOrder like '*{0}*' ", invCode);

                }
                else if (srPName.Length > 0 && invCode.Length == 0)
                {
                    this.dataAllStore_SRC.DefaultView.RowFilter = string.Format("StoreName like '*{0}*'  ", srPName);


                }
                else
                {
                    this.dataAllStore_SRC.DefaultView.RowFilter = string.Format(" 1 = 1 ", srPName);
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxSrcInvCodeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {

                    string barcodestr = textBoxSrcInvCodeName.Text;

                    foreach (Store c in allStores)
                    {
                        if (c.StoreOrder == barcodestr)
                        {
                          //  comboBoxAllCat.SelectedValue = c.StoreCatID;
                            comboBoxListStore.SelectedValue = c.StoreID;

                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }


       



    

   




    }
}
