using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppRest
{
    public partial class AddPCPurchase : AddDataTemplate
    {

        GetDataRest gd;

        DataTable prAllDATA ;
        DataTable prDetail;
        string PRCode;

        List<Supplier> allSupplier;
        List<Store> allStores;

        DataTable dataAllStore;
        DataTable dataAllStore_PO;

        float convertRate;
        int flagSave = 0;
        int prDetailAddStockID = 0;


        DataTable poAllDATA;
        DataTable poDetail;

        string POCode;
        DataTable prSelected;
        DataTable allSupplier_Data;

        int suplierSelectID = 0 ; 

        public AddPCPurchase(Form frmlkFrom, int flagFrmClose)
        {
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();

            this.Width = 1024;
            this.Height = 768;

 

            gd = new GetDataRest();

            try
            {

                PRCode = "0";
                prAllDATA = gd.getPC_DataPRHeader(PRCode, 0);
                dataGridViewPRHeader.DataSource = prAllDATA;

                dateTimePickerPRDocDate.Value = DateTime.Now;
                dateTimePickerPRDate.Value = DateTime.Now;







                 // PO


                POCode = "0";
                poAllDATA = gd.getPC_DataPOHeader(POCode, 0);
                dataGridViewPOHeader.DataSource = poAllDATA;


                dateTimePODocDate.Value = DateTime.Now;
                dateTimePODate.Value = DateTime.Now;

                getComboPaperStstus();
                getComboPaperPRComplete();


                allSupplier = gd.getPC_Supplier(0, "0", "0");
                getComboAllSuplier();

                allStores = gd.getListAllStore(0, 0, "000");
                getComboAllStore();



                dataGridViewAllStore.DataSource = dataAllStore;

                dataGridViewAllStore.Columns[0].Visible = false;
                dataGridViewAllStore.Columns[1].Visible = false;
                dataGridViewAllStore.Columns[2].HeaderText = "InvCode";
                dataGridViewAllStore.Columns[3].HeaderText = "Name";
                dataGridViewAllStore.Columns[4].HeaderText = "Barcode";
                dataGridViewAllStore.Columns[5].HeaderText = "Unit";
                dataGridViewAllStore.Columns[6].HeaderText = "BigUnit";



                this.suplierSelectID = 0;

                if (radioBoxSuplierMapPrice.Checked)
                    dataAllStore = gd.getAllStoreSuplier_Search(0, this.suplierSelectID);
                else
                    dataAllStore = gd.getAllStore_Search(0);

                dataAllStore_PO = dataAllStore;


                dataGridViewAllStore_PO.DataSource = dataAllStore_PO;

                if (dataGridViewAllStore_PO.Rows.Count > 0)
                {
                    dataGridViewAllStore_PO.Columns[0].Visible = false;
                    dataGridViewAllStore_PO.Columns[1].Visible = false;
                    dataGridViewAllStore_PO.Columns[2].HeaderText = "รหัสสินค้า";
                    dataGridViewAllStore_PO.Columns[3].HeaderText = "ชื่อ";
                    dataGridViewAllStore_PO.Columns[4].Visible = false;
                    dataGridViewAllStore_PO.Columns[5].HeaderText = "หน่วย (ใหญ่)";
                    dataGridViewAllStore_PO.Columns[6].Visible = false;
                    dataGridViewAllStore_PO.Columns[7].HeaderText = "จำนวนสั่งซื้อ";
                    dataGridViewAllStore_PO.Columns[8].HeaderText = "ราคา (บาท)";
                    dataGridViewAllStore_PO.Columns[9].HeaderText = "หมายเหตุ";
                }

                allSupplier_Data = gd.getPC_SupplierData(0, "0", "0");
                dataGridViewAllSupplier.DataSource = allSupplier_Data;

                dataGridViewAllSupplier.Columns[0].Visible = false;
                dataGridViewAllSupplier.Columns[1].Visible = false;
                dataGridViewAllSupplier.Columns[4].Visible = false;
                dataGridViewAllSupplier.Columns[5].Visible = false;
                dataGridViewAllSupplier.Columns[6].Visible = false;
                dataGridViewAllSupplier.Columns[7].Visible = false;
                dataGridViewAllSupplier.Columns[8].Visible = false;
                dataGridViewAllSupplier.Columns[9].Visible = false;

            }
            catch (Exception ex)
            {


            }
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
                    data.Add(new KeyValuePair<int, string>(c.SupplierID, c.SupplierCompanyName));
                }

            }
            // Clear the combobox
            comboBoPRSuplier1.DataSource = null;
            comboBoPRSuplier1.Items.Clear();

            // Bind the combobox
            comboBoPRSuplier1.DataSource = new BindingSource(data, null);
            comboBoPRSuplier1.DisplayMember = "Value";
            comboBoPRSuplier1.ValueMember = "Key";

            //

            

                   // Clear the combobox
            comboBoxPRRef_Sup1.DataSource = null;
            comboBoxPRRef_Sup1.Items.Clear();

            // Bind the combobox
            comboBoxPRRef_Sup1.DataSource = new BindingSource(data, null);
            comboBoxPRRef_Sup1.DisplayMember = "Value";
            comboBoxPRRef_Sup1.ValueMember = "Key";


            comboBoxPOSupplier.DataSource = null;
            comboBoxPOSupplier.Items.Clear();

            // Bind the combobox
            comboBoxPOSupplier.DataSource = new BindingSource(data, null);
            comboBoxPOSupplier.DisplayMember = "Value";
            comboBoxPOSupplier.ValueMember = "Key";


            comboBoxPOSupplier_SRC.DataSource = null;
            comboBoxPOSupplier_SRC.Items.Clear();

            // Bind the combobox
            comboBoxPOSupplier_SRC.DataSource = new BindingSource(data, null);
            comboBoxPOSupplier_SRC.DisplayMember = "Value";
            comboBoxPOSupplier_SRC.ValueMember = "Key";

            

            ////////////////////////////////////////////////////////////////////////////////
            data = new List<KeyValuePair<int, string>>();
            data.Add(new KeyValuePair<int, string>(0, "= ต้องการ Supplier 2 ="));

            foreach (Supplier c in allSupplier)
            {
                if (c.FlagUse == "Y")
                {
                    data.Add(new KeyValuePair<int, string>(c.SupplierID, c.SupplierCompanyName));
                }

            }
            // Clear the combobox
            comboBoPRSuplier2.DataSource = null;
            comboBoPRSuplier2.Items.Clear();

            // Bind the combobox
            comboBoPRSuplier2.DataSource = new BindingSource(data, null);
            comboBoPRSuplier2.DisplayMember = "Value";
            comboBoPRSuplier2.ValueMember = "Key";


            comboBoxPRRef_Sup2.DataSource = null;
            comboBoxPRRef_Sup2.Items.Clear();

            // Bind the combobox
            comboBoxPRRef_Sup2.DataSource = new BindingSource(data, null);
            comboBoxPRRef_Sup2.DisplayMember = "Value";
            comboBoxPRRef_Sup2.ValueMember = "Key";

            ////////////////////////////////////////////////////////////////////////////////
            data = new List<KeyValuePair<int, string>>();
            data.Add(new KeyValuePair<int, string>(0, "= ต้องการ Supplier 3 ="));

            foreach (Supplier c in allSupplier)
            {
                if (c.FlagUse == "Y")
                {
                    data.Add(new KeyValuePair<int, string>(c.SupplierID, c.SupplierCompanyName));
                }

            }
            // Clear the combobox
            comboBoPRSuplier3.DataSource = null;
            comboBoPRSuplier3.Items.Clear();

            // Bind the combobox
            comboBoPRSuplier3.DataSource = new BindingSource(data, null);
            comboBoPRSuplier3.DisplayMember = "Value";
            comboBoPRSuplier3.ValueMember = "Key";


            comboBoxPRRef_Sup3.DataSource = null;
            comboBoxPRRef_Sup3.Items.Clear();

            // Bind the combobox
            comboBoxPRRef_Sup3.DataSource = new BindingSource(data, null);
            comboBoxPRRef_Sup3.DisplayMember = "Value";
            comboBoxPRRef_Sup3.ValueMember = "Key";
        }

        private void buttonAddPRDetail_Click(object sender, EventArgs e)
        {
            panelPRTakeOne.Visible = true;

        }

        private void buttonCloseBC_Click(object sender, EventArgs e)
        {
            panelPRTakeOne.Visible = false;
        }

        private void textBoxPONo_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (e.KeyChar == (char)13)
                {
                  //  panelTakeOne.Visible = true;

                    if (textBoxPRCode.Text.Length > 0)
                    {
                        PRCode = textBoxPRCode.Text;
                        prAllDATA = gd.getPC_DataPRHeader(PRCode,0);
                        dataGridViewPRHeader.DataSource = prAllDATA;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonNewPR_Click(object sender, EventArgs e)
        {
            panelPRTakeOne.Visible = true; 
           

            PRCode = gd.getPC_NewPRCode("");

            textBoxPRCode.Text = PRCode;

            textBoxPRBy.Text = Login.userName;

            setDefault();
            viewDetailofPRByCode();

        }

        private void getComboAllStore()
        {
            try
            {

                List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

                // Add data to the List

                data.Add(new KeyValuePair<int, string>(0, "= เลือกสินค้า / วัตถุดิบ ="));

                foreach (Store c in allStores)
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

                comboBoxListStore_PO.DataSource = null;
                comboBoxListStore_PO.Items.Clear();

                // Bind the combobox
                comboBoxListStore_PO.DataSource = new BindingSource(data, null);
                comboBoxListStore_PO.DisplayMember = "Value";
                comboBoxListStore_PO.ValueMember = "Key";
                 

            }
            catch (Exception ex)
            {

            }

        }

        private void buttonPRSearchStore_Click(object sender, EventArgs e)
        {
            panelSearchStore.Visible = true;
            textBoxSCStoreCode.Text = "";
            textBoxSCStoreBarcode.Text = "";
        }

        private void buttonCloseSrcStorePN_Click(object sender, EventArgs e)
        {
            panelSearchStore.Visible = false;
        }

        private void dataGridViewAllStore_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridStoreCatID = Int32.Parse(dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreCatID"].Value.ToString());
                int dataGridStoreID = Int32.Parse(dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreID"].Value.ToString());


                //  comboBoxAllCat.SelectedValue = dataGridStoreCatID;
                comboBoxListStore.SelectedValue = dataGridStoreID;

            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxSrcStoreName_TextChanged(object sender, EventArgs e)
        {

            try
            {

                string srPName = textBoxSrcStoreName.Text;
                string invCode = textBoxSrcInvCode.Text;


                if (srPName.Length > 0 && invCode.Length > 0 )
                {
                    this.dataAllStore.DefaultView.RowFilter = string.Format("StoreName like '*{0}*' and StoreOrder like '*{1}*' ", srPName, invCode);

                }else if (srPName.Length == 0 && invCode.Length > 0 )
                {
                    this.dataAllStore.DefaultView.RowFilter = string.Format("StoreOrder like '*{0}*' ", invCode);

                }
                else if (srPName.Length > 0 && invCode.Length == 0)
                {
                    this.dataAllStore.DefaultView.RowFilter = string.Format("StoreName like '*{0}*'  ", srPName );


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

        private void textBoxSCStoreBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {

                    string barcodestr = textBoxSCStoreBarcode.Text;

                    foreach (Store c in allStores)
                    {
                        if (c.StoreBarcode == barcodestr)
                        {
                           // comboBoxAllCat.SelectedValue = c.StoreCatID;
                            comboBoxListStore.SelectedValue = c.StoreID;
                            textBoxSCStoreBarcode.Text = "";
                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void textBoxSCStoreCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {

                    string barcodestr = textBoxSCStoreCode.Text;

                    foreach (Store c in allStores)
                    {
                        if (c.StoreOrder.ToString() == barcodestr)
                        {
                            // comboBoxAllCat.SelectedValue = c.StoreCatID;
                            comboBoxListStore.SelectedValue = c.StoreID;
                            textBoxSCStoreCode.Text = "";
                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }


    
        private void comboBoxListStore_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                int selectStoreID = Int32.Parse(comboBoxListStore.SelectedValue.ToString());

                foreach (Store c in allStores)
                {
                    if (c.StoreID == selectStoreID)
                    {
                        labelUnit.Text = c.StoreUnit;
                        labelUnitConvert.Text = c.StoreConvertUnit;
                        this.convertRate = float.Parse(c.StoreConvertRate.ToString()); 
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

        private void buttonPRSave_Click(object sender, EventArgs e)
        {
            try
            { 
              

                int addstocksign = 1;


                //Header
                 PRCode = textBoxPRCode.Text;
                DateTime docdate = dateTimePickerPRDocDate.Value;
                DateTime prdate = dateTimePickerPRDate.Value;
                string quotationNo = textBoxPRQuoNo.Text;
                string objective = textBoxPRObjectTive.Text;
                string prRemark = textBoxPRRemark.Text;
                int sup1 = Int32.Parse(comboBoPRSuplier1.SelectedValue.ToString());
                int sup2 = Int32.Parse(comboBoPRSuplier2.SelectedValue.ToString());
                int sup3 = Int32.Parse(comboBoPRSuplier3.SelectedValue.ToString()); 
                string prBy = textBoxPRBy.Text;
                string dateAddStock = dateTimePickerPRDate.Value.ToString("yyyyMMdd");


                
                // Detail

                int selectStoreID = Int32.Parse(comboBoxListStore.SelectedValue.ToString()); 
                float storeaddStoreQTY = float.Parse(txtBoxAddStoreQTY.Text)  +  float.Parse(txtBoxBigQTY.Text) * this.convertRate; 
                string storeName = comboBoxListStore.Text;
                string addStockType = "PR Process";

                float storeaddStoreAmt = float.Parse(txtBoxAddStoreAmt.Text);

                float storeaddStoreVat = 0;

                //if (checkBoxVat.Checked == true)
                //    storeaddStoreVat = storeaddStoreAmt * (float)0.07;

                string storeRemark = txtBoxAddStoreRemark.Text.ToString();
                string storeBarCode = "";

 


                if (flagSave == 0)
                {
                    if (storeaddStoreQTY == (float)0 && selectStoreID > 0)
                        throw new Exception("กรุณากรอกข้อมูลให้ครบ");

                    if (MessageBox.Show("คุณต้องการจะเพิ่มข้อมูล  : " + storeName + " ใน PR" + PRCode + " หรือไม่ ?", "เพิ่ม " + storeName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.instPC_NewPR(PRCode, docdate, prdate, quotationNo, objective, prRemark, sup1, sup2, sup3, prBy, selectStoreID, dateAddStock, storeaddStoreQTY * addstocksign, storeaddStoreAmt, storeaddStoreVat, storeRemark, storeBarCode, addStockType);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Add Stock : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่ม " + storeName + " ใน PR" + PRCode + " >> (Success)");

                            viewDetailofPRByCode();
                            
                            setDefault();
                            //ComboboxStore_Change(null, e);
                        }
                    }
                }
                else
                {

                    Button bt = (Button)sender ;
                    
                    
                    if( bt.Name == "buttonPRDelete" )
                        storeaddStoreQTY = 0;
 

                    if (MessageBox.Show("คุณต้องการจะแก้ไข : " + storeName + "   ใน PR" + PRCode + " หรือไม่ ?", "แก้ไข " + storeName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updatePC_PRDetail(PRCode, prDetailAddStockID,selectStoreID, storeaddStoreQTY, storeaddStoreAmt, storeaddStoreVat, storeRemark, addStockType);

                        if (result == 3)
                        {
                            MessageBox.Show("ไม่สามารถแก้ไขรายการได้ เนื่องจากอนุมัติแล้ว");

                        } 
                        else
                        {
                            if (bt.Name == "buttonPRDelete")
                                MessageBox.Show("ลบ " + storeName + " ใน PR" + PRCode + " >> (Success)");
                            else
                                MessageBox.Show("แก้ไข " + storeName + " ใน PR" + PRCode + " >> (Success)");
                            
                            viewDetailofPRByCode();
                            
                            setDefault();
                            //ComboboxStore_Change(null, e);
                        }

                    }

                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
                 
        }



        private void viewDetailofPRByCode()
        {
            try
            {

                if (PRCode.Length > 0)
                {
                    prDetail = gd.getPC_DataPRDetail(PRCode, 0);
                    dataGridViewPRDetail.DataSource = prDetail;

                    dataGridViewPRDetail.Columns[1].Visible = false;
                   // dataGridViewPRDetail.Columns[2].Visible = false;
                    dataGridViewPRDetail.Columns[3].Visible = false;
                    dataGridViewPRDetail.Columns[4].Visible = false;
                    //dataGridViewPRDetail.Columns[10].Visible = false;
                    //dataGridViewPRDetail.Columns[11].Visible = false;


                    dataGridViewPRDetail.Columns[2].HeaderText = "No.";

                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            } 
       
            
        }


        private void dataGridViewPRHeader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (Login.userStatus == "Admin" || Login.userStatus == "Manager" || Login.userStatus == "UserStock" || Login.userID == 1200 ) 
                    panelPRApproved.Visible = true;
                else
                    panelPRApproved.Visible = false; 


                // Read Data Grid
                PRCode = dataGridViewPRHeader.Rows[e.RowIndex].Cells["PRCode"].Value.ToString();

                textBoxPO_PRRef.Text = PRCode;

                int prStatus = Int32.Parse(dataGridViewPRHeader.Rows[e.RowIndex].Cells["PRStatus"].Value.ToString());
                int sup1 = Int32.Parse(dataGridViewPRHeader.Rows[e.RowIndex].Cells["RequestSuplier1"].Value.ToString());
                int sup2 = Int32.Parse(dataGridViewPRHeader.Rows[e.RowIndex].Cells["RequestSuplier2"].Value.ToString());
                int sup3 = Int32.Parse(dataGridViewPRHeader.Rows[e.RowIndex].Cells["RequestSuplier3"].Value.ToString());

                string quotation = dataGridViewPRHeader.Rows[e.RowIndex].Cells["QuotationNo"].Value.ToString();
                string objective = dataGridViewPRHeader.Rows[e.RowIndex].Cells["Objective"].Value.ToString();
                string remark = dataGridViewPRHeader.Rows[e.RowIndex].Cells["Remark"].Value.ToString();
                string prBy = dataGridViewPRHeader.Rows[e.RowIndex].Cells["PRBy"].Value.ToString(); 
                string remarkApproved = dataGridViewPRHeader.Rows[e.RowIndex].Cells["RemarkApprove"].Value.ToString();
                string approvedBy = dataGridViewPRHeader.Rows[e.RowIndex].Cells["ApproveBy"].Value.ToString();


                DateTime dtPRDocDate = DateTime.Parse(dataGridViewPRHeader.Rows[e.RowIndex].Cells["PRDocDate"].Value.ToString());
                DateTime dtPRDate = DateTime.Parse(dataGridViewPRHeader.Rows[e.RowIndex].Cells["PRDate"].Value.ToString());


                dateTimePickerPRDocDate.Value = dtPRDocDate;
                dateTimePickerPRDate.Value = dtPRDate;



                // input to Object

                textBoxPRCode.Text = PRCode;
                textBoxPRQuoNo.Text = quotation;
                textBoxPRObjectTive.Text = objective;
                textBoxPRRemark.Text = remark;

                textBoxPRBy.Text = prBy;
                comboBoPRSuplier1.SelectedValue = sup1;
                comboBoPRSuplier2.SelectedValue = sup2;
                comboBoPRSuplier3.SelectedValue = sup3;

               

                if (prStatus == 1)
                    radioBoxPRWaitApprove.Checked = true;
                else if(prStatus == 2)
                     radioBoxPRReject.Checked = true;
                else if (prStatus == 3)
                    radioBoxPRApproved.Checked = true;
                else if (prStatus == 4)
                    radioBoxPRClose.Checked = true;
                else if (prStatus == 5)
                    radioBoxPRCancle.Checked = true;

                textBoxPRApprovedRemark.Text = remarkApproved;
                textBoxPRApproveBy.Text = approvedBy;


                radioBoxPRWaitApprove.Enabled = true;
                radioBoxPRReject.Enabled = true;
                radioBoxPRApproved.Enabled = true;
                buttonPRAppSAVE.Visible = true;

 

                if (prStatus >= 3)
                {
                    radioBoxPRWaitApprove.Enabled = false;
                    radioBoxPRReject.Enabled = false;
                    radioBoxPRApproved.Enabled = false;
                   // buttonPRAppSAVE.Visible = false;
                    
                }

                if (Login.userStatus == "Admin" || Login.userID == 1200)
                {
                    radioBoxPRWaitApprove.Enabled = true;
                    radioBoxPRReject.Enabled = true;
                    radioBoxPRApproved.Enabled = true;

                }


                viewDetailofPRByCode();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void setDefault()
        {

            dateTimePickerPRDocDate.Value = DateTime.Now;
            dateTimePickerPRDate.Value = DateTime.Now;

           // dateTimePickerAddStock.Value = DateTime.Now;
            txtBoxAddStoreQTY.Text = "0";
            txtBoxBigQTY.Text = "0";
            txtBoxAddStoreAmt.Text = "0";
            txtBoxAddStoreRemark.Text = "เพิ่มเติม";
          //  textBoxRemPOHeader.Text = "เพิ่มเติม";

            prAllDATA = gd.getPC_DataPRHeader("0", 0);
            dataGridViewPRHeader.DataSource = prAllDATA;

            comboBoxListStore.SelectedIndex = 0;

            comboBoxListStore.Enabled = true;
            flagSave = 0;



           // textBoxPRCode.Text = "";
            textBoxPRQuoNo.Text = "";
            textBoxPRObjectTive.Text = "";
            textBoxPRRemark.Text = "";

            textBoxPRBy.Text = "";
            comboBoPRSuplier1.SelectedIndex = 0;
            comboBoPRSuplier2.SelectedIndex = 0;
            comboBoPRSuplier3.SelectedIndex = 0;

            textBoxPRApprovedRemark.Text = "";
            textBoxPRApproveBy.Text = "";



            radioBoxPRWaitApprove.Enabled = true;
            radioBoxPRReject.Enabled = true;
            radioBoxPRApproved.Enabled = true;
            buttonPRAppSAVE.Visible = true;

            textBoxPRApprovedRemark.Text = "";


             


        }

        private void dataGridViewPRDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
            try
            {
                 

                prDetailAddStockID = Int32.Parse(dataGridViewPRDetail["AddStockID", e.RowIndex].Value.ToString());
                 

                flagSave = 1;
             //   FlagSaveChange(); 

                int editStoreID = Int32.Parse(dataGridViewPRDetail.Rows[e.RowIndex].Cells["StoreID"].Value.ToString());
                float storeBigQty = float.Parse(dataGridViewPRDetail.Rows[e.RowIndex].Cells["StoreBigQTY"].Value.ToString());
                float stroreAmt = float.Parse(dataGridViewPRDetail.Rows[e.RowIndex].Cells["StoreAmount"].Value.ToString());
                string storeRemark = dataGridViewPRDetail.Rows[e.RowIndex].Cells["StoreRemark"].Value.ToString();

                comboBoxListStore.SelectedValue = editStoreID;
                txtBoxBigQTY.Text = storeBigQty.ToString();
                txtBoxAddStoreRemark.Text = storeRemark;
                txtBoxAddStoreAmt.Text = stroreAmt.ToString();

                comboBoxListStore.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        private void buttonPRAppSAVE_Click(object sender, EventArgs e)
        {
            try
            {

                int prStatus = 0;

                if (radioBoxPRWaitApprove.Checked)
                    prStatus = 1;
                else if (radioBoxPRReject.Checked)
                    prStatus = 2;
                else if (radioBoxPRApproved.Checked )
                    prStatus = 3;
                else if (radioBoxPRClose.Checked)
                    prStatus = 4;
                else if (radioBoxPRCancle.Checked)
                    prStatus = 5;

                string remarkApproved = textBoxPRApprovedRemark.Text;


                int result = gd.updatePC_PRHeaderApproved(this.PRCode, remarkApproved, Login.userName, prStatus);


                MessageBox.Show("ทำรายการ สำเร็จ " + this.PRCode); 
                setDefault();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxPO_PRRef_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void buttonSearchPR_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBoxPO_PRRef.Text.Length > 0)
                {
                    panelPRDetailRef.Visible = true;


                    PRCode = textBoxPO_PRRef.Text;
                    prSelected = gd.getPC_DataPRHeader(PRCode, 0);

                    int sup1 = 0;
                    int sup2 = 0;
                    int sup3 = 0;
                    string remark = "";

                     foreach(DataRow row in prSelected.Rows)
                     {
                         sup1 = Int32.Parse(row["RequestSuplier1"].ToString());
                         sup2 = Int32.Parse(row["RequestSuplier2"].ToString());
                         sup3 = Int32.Parse(row["RequestSuplier3"].ToString());
                         remark = row["Remark"].ToString();
                     }

                     comboBoxPRRef_Sup1.SelectedValue = sup1;
                     comboBoxPRRef_Sup2.SelectedValue = sup2;
                     comboBoxPRRef_Sup3.SelectedValue = sup3;
                     TextBoxPORemark.Text = remark; 


                    prDetail = gd.getPC_DataPRDetail_Ref(textBoxPO_PRRef.Text, 0);
                    dataGridViewPRDetailRef.DataSource = prDetail;

                    dataGridViewPRDetailRef.Columns[0].Visible = false;
                    dataGridViewPRDetailRef.Columns[1].Visible = false;
                    dataGridViewPRDetailRef.Columns[2].Visible = false;
                    dataGridViewPRDetailRef.Columns[3].Visible = false;
                    dataGridViewPRDetailRef.Columns[4].HeaderText = "ชื่อสินค้า/วัตถุดิบ";
                    dataGridViewPRDetailRef.Columns[5].HeaderText = "จำนวนตาม PR"; 
                    dataGridViewPRDetailRef.Columns[6].Visible = false;
                    dataGridViewPRDetailRef.Columns[7].Visible = false;
                    dataGridViewPRDetailRef.Columns[8].Visible = false;
                    dataGridViewPRDetailRef.Columns[9].Visible = false;
                    dataGridViewPRDetailRef.Columns[10].Visible = false;
                    dataGridViewPRDetailRef.Columns[11].HeaderText = "ราคา \n(หน่วยใหญ่)";
                    dataGridViewPRDetailRef.Columns[12].HeaderText = "จำนวน \n(หน่วยใหญ่)";
                    dataGridViewPRDetailRef.Columns[13].HeaderText = "ราคา \n(หน่วยเล็ก)";
                    dataGridViewPRDetailRef.Columns[14].HeaderText = "จำนวน \n(หน่วยเล็ก)";
                    dataGridViewPRDetailRef.Columns[15].HeaderText = "เป็นเงิน";
                    dataGridViewPRDetailRef.Columns[16].HeaderText = "เหตุผล";
                    dataGridViewPRDetailRef.Columns[17].Visible = false;



                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            } 
       
        }

        private void buttonClosePanelPRDetailRef_Click(object sender, EventArgs e)
        {
            panelPRDetailRef.Visible = false;
        }

        private void buttonAddPODetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxPOCode.Text.Length == 0)
                    throw new Exception("กรุณาระบุเลขที่ PO ที่จะเพิ่มรายการ");

                if (comboBoxPOSupplier.SelectedIndex == 0)
                    throw new Exception("กรุณาระบุ Supplier ที่จะเปิด PO");


                string dateAddStock = dateTimePODate.Value.ToString("yyyyMMdd");

                int storeID = 0;
                string storeName = "";
                float storeAddBigUnit = 0;
                float storeAddBigPrice = 0;
                string storeBigUnit = "";
                float storeAddUnit = 0;
                float storeAddPrice = 0;
                string storeUnit = "";
                float storeConvertRate = 0;
                string storeRemark = "";

                float storeAmt;

                float addUnits = 0;

                string flagAddStore = "N";
                string PRRefAdd = textBoxPO_PRRef.Text;
                    

                List<StockChange> stcLists = new List<StockChange>();

                foreach (DataGridViewRow row in dataGridViewPRDetailRef.Rows)
                {
                    if (row.Cells["TakeBigQTY"].Value.ToString() != "0" ||
                        row.Cells["TakeQTY"].Value.ToString() != "0")
                    {
                        storeID = Int32.Parse(row.Cells["StoreID"].Value.ToString());
                        storeName = row.Cells["StoreName"].Value.ToString();
                        storeUnit = row.Cells["StoreUnit"].Value.ToString();
                        storeBigUnit = row.Cells["StoreBigUnit"].Value.ToString();
                        storeAddBigUnit = float.Parse(row.Cells["TakeBigQTY"].Value.ToString());
                        storeAddBigPrice = float.Parse(row.Cells["TakePriceBig"].Value.ToString());
                        storeAddUnit = float.Parse(row.Cells["TakeQTY"].Value.ToString());
                        storeAddPrice = float.Parse(row.Cells["TakePrice"].Value.ToString());
                        storeConvertRate = float.Parse(row.Cells["StoreConvertRate"].Value.ToString());
                        storeAmt  = float.Parse(row.Cells["TakeAmount"].Value.ToString());
                        storeRemark = row.Cells["StoreRemark"].Value.ToString();

                        if (storeAddBigUnit > 0)
                        {
                            flagAddStore = "B";

                            if (storeAmt > 0 && storeAddBigPrice == 0)
                                storeAddBigPrice = storeAmt / storeAddBigUnit;
                            else
                                storeAmt = storeAddBigUnit * storeAddBigPrice;

                            stcLists.Add(new StockChange(storeID, storeName, storeAddBigUnit, storeBigUnit, storeAddBigPrice, storeAmt, storeRemark, flagAddStore, PRRefAdd, storeConvertRate));
                        }
                        else
                        {
                            flagAddStore = "S";

                            if (storeAmt > 0 && storeAddPrice == 0)
                                storeAddPrice = storeAmt / storeAddUnit;
                            else
                                 storeAmt = storeAddUnit * storeAddPrice;


                            stcLists.Add(new StockChange(storeID, storeName, storeAddUnit, storeUnit, storeAddPrice, storeAmt, storeRemark, flagAddStore, PRRefAdd, storeConvertRate));

                        }
                         
                    }
                }

                string txtInsert = "ทำรายการเปิด PO ดังนี้ \n\r";
                int i = 1;

                string str1 = "";
                string str2 = "";

                foreach (StockChange st in stcLists)
                {
                    str1 = i.ToString() + ". " + st.StoreName;

                    str2 = " :: " + "(" + st.StoreAddUnit.ToString("###,###.##") + " " + st.StoreUnit + ")" + ">" + " " + st.StoreAmt.ToString("###,###.##");

                    txtInsert += str1 + str2 + "\n\r";
                    i++;
                }
                 

                int result = 0;


               // string poNO = textBoxPOCode.Text;
                DateTime docdate = dateTimePODocDate.Value;
                DateTime podate = dateTimePODate.Value;
                string poRemark = TextBoxPORemark.Text;
                int poSupID = Int32.Parse(comboBoxPOSupplier.SelectedValue.ToString());
                string poTearmofPayment = comboBoxPOPaymentTerm.Text;
                string poLevel = comboBoxPOLevel.Text;
                int poVATType = 0;

                if (radioButtonPO_NonVAT.Checked)
                    poVATType = 1;
                else if(radioButtonPO_ExcVAT.Checked)
                    poVATType = 3;
                else if (radioButtonPO_IncVAT.Checked)
                    poVATType = 2;

                float poDiscount = float.Parse(textBoxPODiscount.Text);

                if (MessageBox.Show("คุณต้องการทำรายการ \n\r" + txtInsert + " หรือไม่ ?", "ทำรายการเปิด PO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    foreach (StockChange st in stcLists)
                    {

                        result = gd.instPC_NewPO(this.POCode, docdate, podate, PRRefAdd, poRemark, poSupID, poTearmofPayment, poLevel, poVATType, poDiscount, Login.userName, st.StoreID, dateAddStock, st.StoreAddUnit, st.StorePrice, st.StoreAmt, 0, st.StoreRemark, "", st.FlagAddStore, st.StoreConvertRate);

                    }
                }


              //  POCode = "0";
                poAllDATA = gd.getPC_DataPOHeader("0",0);
                dataGridViewPOHeader.DataSource = poAllDATA;
                panelPOTakeOne.Visible = true;
                panelPRDetailRef.Visible = false;
               
                viewDetailofPOByCode();


            }  
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message);
            } 
        }

        private void buttonNewPO_Click(object sender, EventArgs e)
        {
          //  panelPRTakeOne.Visible = true;


            POCode = gd.getPC_NewPOCode("");

            textBoxPOCode.Text = POCode;

         //   textBoxPRBy.Text = Login.userName;

          //  setDefault();
          //  viewDetailofPRByCode();
        }

        private void buttonPOClose_Click(object sender, EventArgs e)
        {
            panelPOTakeOne.Visible = false;
        }

        private void buttonAddPODetailTK_Click(object sender, EventArgs e)
        {
            panelPOTakeOne.Visible = true;
            viewDetailofPOByCode();

        }

        private void viewDetailofPOByCode()
        {
            try
            {
                POCode = textBoxPOCode.Text;

                if (POCode.Length > 0)
                {
                     // MessageBox.Show(POCode);

                    poDetail = gd.getPC_DataPODetail(POCode, 0);
                    dataGridViewPODetail.DataSource = poDetail;

                    if (poDetail.Rows.Count > 0)
                    {

                        dataGridViewPODetail.Columns[1].Visible = false;
                      //  dataGridViewPODetail.Columns[2].Visible = false;
                        dataGridViewPODetail.Columns[3].Visible = false;
                        dataGridViewPODetail.Columns[4].Visible = false;
                        //dataGridViewPRDetail.Columns[10].Visible = false;
                        //dataGridViewPRDetail.Columns[11].Visible = false;

                        dataGridViewPODetail.Columns[2].HeaderText = "No.";

                    }

                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message);
            }


        }

        private void dataGridViewPOHeader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               //  panelPOApproved.Visible = true; 

                if (Login.userStatus == "Admin" || Login.userStatus == "Manager" || Login.userStatus == "UserPO" || Login.userID == 1200) 
                    panelPOApproved.Visible = true;
                else
                    panelPOApproved.Visible = false; 


                // Read Data Grid
                POCode = dataGridViewPOHeader.Rows[e.RowIndex].Cells["POCode"].Value.ToString();

  
              

                int poStatus = Int32.Parse(dataGridViewPOHeader.Rows[e.RowIndex].Cells["POStatus"].Value.ToString());
                int poSupID = Int32.Parse(dataGridViewPOHeader.Rows[e.RowIndex].Cells["POSupplierID"].Value.ToString());


                string poTearmofPayment = dataGridViewPOHeader.Rows[e.RowIndex].Cells["PaymentTerm"].Value.ToString();
                string poLevel = dataGridViewPOHeader.Rows[e.RowIndex].Cells["POLevel"].Value.ToString();
                string poRemark = dataGridViewPOHeader.Rows[e.RowIndex].Cells["Remark"].Value.ToString(); 
                string remarkApproved = dataGridViewPOHeader.Rows[e.RowIndex].Cells["RemarkApprove"].Value.ToString();
                string approvedBy = dataGridViewPOHeader.Rows[e.RowIndex].Cells["ApproveBy"].Value.ToString();
                int poVATType = Int32.Parse(dataGridViewPOHeader.Rows[e.RowIndex].Cells["POVATType"].Value.ToString());
                float poDiscount = float.Parse(dataGridViewPOHeader.Rows[e.RowIndex].Cells["PODiscount"].Value.ToString());

                DateTime dtPODocDate = DateTime.Parse(dataGridViewPOHeader.Rows[e.RowIndex].Cells["PODocDate"].Value.ToString());
                DateTime dtDate = DateTime.Parse(dataGridViewPOHeader.Rows[e.RowIndex].Cells["PODate"].Value.ToString());


                dateTimePODocDate.Value = dtPODocDate;
                dateTimePODate.Value = dtDate;


                textBoxPODiscount.Text = poDiscount.ToString();

                if (poVATType == 1)
                    radioButtonPO_NonVAT.Checked = true;
                else if (poVATType == 3)
                    radioButtonPO_ExcVAT.Checked = true;
                else if (poVATType == 2)
                    radioButtonPO_IncVAT.Checked = true;

               



                // input to Object

                textBoxPOCode.Text = POCode;
                comboBoxPOLevel.Text = poLevel;
                comboBoxPOPaymentTerm.Text = poTearmofPayment;
                TextBoxPORemark.Text = poRemark; 
               
                comboBoxPOSupplier.SelectedValue = poSupID; 

                if (poStatus == 1)
                    radioBoxPOWaitApprove.Checked = true;
                else if (poStatus == 2)
                    radioBoxPOReject.Checked = true;
                else if (poStatus == 3)
                    radioBoxPOApproved.Checked = true;
                else if (poStatus == 4)
                    radioBoxPOClose.Checked = true;
                else if (poStatus == 5)
                    radioBoxPOCancle.Checked = true;

                textBoxPOApprovedRemark.Text = remarkApproved;
                textBoxPOApproveBy.Text = approvedBy;


                radioBoxPOWaitApprove.Enabled = true;
                radioBoxPOReject.Enabled = true;
                radioBoxPOApproved.Enabled = true;
                buttonPOAppSAVE.Visible = true;

                if (poStatus >= 3)
                {
                    radioBoxPOWaitApprove.Enabled = false;
                    radioBoxPOReject.Enabled = false;
                    radioBoxPOApproved.Enabled = false;
                   // buttonPOAppSAVE.Visible = false;
                }

                if (Login.userStatus == "Admin" )
                {
                    radioBoxPOWaitApprove.Enabled = true;
                    radioBoxPOReject.Enabled = true;
                    radioBoxPOApproved.Enabled = true;
                    // buttonPOAppSAVE.Visible = false;
                }


                viewDetailofPOByCode();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void textBoxPOCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    //  panelTakeOne.Visible = true;

                    if (textBoxPOCode.Text.Length > 0)
                    {
                        POCode = textBoxPOCode.Text;
                        //poAllDATA = gd.getPC_DataPOHeader(POCode);
                        //dataGridViewPOHeader.DataSource = poAllDATA;

                        viewDetailofPOByCode();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonPOAppSAVE_Click(object sender, EventArgs e)
        {
            try
            {

                int poStatus = 0;

                if (radioBoxPOWaitApprove.Checked)
                    poStatus = 1;
                else if (radioBoxPOReject.Checked)
                    poStatus = 2;
                else if (radioBoxPOApproved.Checked)
                    poStatus = 3;
                else if (radioBoxPOClose.Checked)
                    poStatus = 4;
                else if (radioBoxPOCancle.Checked)
                    poStatus = 5;

                string remarkApproved = textBoxPOApprovedRemark.Text;


                int result = gd.updatePC_POHeaderApproved(this.POCode, remarkApproved, Login.userName, poStatus);


                //if (result <= 0)
                //{
                //    MessageBox.Show("ไม่สามารถทำรายการได้ เนื่องจากอนุมัติไปแล้ว");
                //}
                //else
                //{
                //    MessageBox.Show("ทำรายการ สำเร็จ " + this.POCode);

                //    setDefaultPO();
                //    //ComboboxStore_Change(null, e);
                //}

                MessageBox.Show("ทำรายการ สำเร็จ " + this.POCode);

                setDefaultPO();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void setDefaultPO()
        {

            //   refreshPODetail();

           
            //txtBoxAddStoreQTY_PO.Text = "0";
            //txtBoxBigQTY_PO.Text = "0";
            //txtBoxAddStoreAmt_PO.Text = "0";
            //txtBoxAddStoreRemark_PO.Text = "เพิ่มเติม";

            dateTimePODocDate.Value = DateTime.Now;
            dateTimePODate.Value = DateTime.Now;


            poAllDATA = gd.getPC_DataPOHeader("0", 0);
            dataGridViewPOHeader.DataSource = poAllDATA;

            //comboBoxListStore.SelectedIndex = 0;

            //comboBoxListStore.Enabled = true;
            //flagSave = 0;
             
            radioButtonPO_NonVAT.Checked = true;
          //  textBoxPOCode.Text = "";
            comboBoxPOLevel.SelectedIndex = 0;
            comboBoxPOPaymentTerm.SelectedIndex = 0;
            TextBoxPORemark.Text = "";
            comboBoxPOSupplier.SelectedIndex = 0;


            textBoxPODiscount.Text = "0";
            radioBoxPOWaitApprove.Enabled = true;
            radioBoxPOReject.Enabled = true;
            radioBoxPOApproved.Enabled = true;
            buttonPOAppSAVE.Visible = true;

            textBoxPOApprovedRemark.Text = "";



            comboBoxListStore_PO.SelectedValue = 0;
            txtBoxAddStoreQTY_PO.Text = "0";
            txtBoxAddStorePrice_PO.Text = "0";
            labelStoreUnit_PO.Text = "0";
            comboBoxAddPOUnitType.Text = "S";

            this.flagSave = 0;

        }

        private void buttonPrintA4_Click(object sender, EventArgs e)
        {

            Button printButton = (Button)sender;
            string rptCode = "0";
            string rptType = "0";

            try
            {
                rptType = printButton.Name.Replace("buttonPrint", "").Trim();

                if (rptType == "PR")
                    rptCode = this.PRCode;
                else if(rptType == "PO")
                    rptCode = this.POCode;

               
                LinkFromRptBillReport(rptCode, rptType);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        FromRptPurchase formFromRptBillReport;

        private void LinkFromRptBillReport(string rptCode, string rptType)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formFromRptBillReport == null)
            {
                formFromRptBillReport = new FromRptPurchase(this, 0, rptCode, rptType);
            }
            else
            {
                formFromRptBillReport.rptCode = rptCode;
                formFromRptBillReport.rptType = rptType;
                formFromRptBillReport.viewReport();
            }
            Cursor.Current = Cursors.Default;
            if (formFromRptBillReport.ShowDialog() == DialogResult.OK)
            {
                formFromRptBillReport.Dispose();
                formFromRptBillReport = null;
            }
        }

        private void comboBoxPaperStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int status = Int32.Parse(comboBoxPaperStatusPR.SelectedValue.ToString());
                prAllDATA = gd.getPC_DataPRHeader("0", status);
                dataGridViewPRHeader.DataSource = prAllDATA;
            }
            catch (Exception ex)
            {


            }
        }


        private void getComboPaperStstus()
        {
            try
            {
                List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

                // Add data to the List
                data.Add(new KeyValuePair<int, string>(0, "ทุกสถานะ"));
                data.Add(new KeyValuePair<int, string>(1, "รออนุมัติ"));
                data.Add(new KeyValuePair<int, string>(2, "ไม่อนุมัติ"));
                data.Add(new KeyValuePair<int, string>(3, "อนุมัติแล้ว"));
                data.Add(new KeyValuePair<int, string>(4, "ปิดใบ"));
                data.Add(new KeyValuePair<int, string>(5, "ยกเลิก"));

                //foreach (StoreCat c in allStoreCat)
                //{
                //    data.Add(new KeyValuePair<int, string>(c.StoreCatID, c.StoreCatName));
                //}

                comboBoxPaperStatusPR.DataSource = null;
                comboBoxPaperStatusPR.Items.Clear();

                // Bind the combobox
                comboBoxPaperStatusPR.DataSource = new BindingSource(data, null);
                comboBoxPaperStatusPR.DisplayMember = "Value";
                comboBoxPaperStatusPR.ValueMember = "Key";


                comboBoxPaperStatusPO.DataSource = null;
                comboBoxPaperStatusPO.Items.Clear();

                // Bind the combobox
                comboBoxPaperStatusPO.DataSource = new BindingSource(data, null);
                comboBoxPaperStatusPO.DisplayMember = "Value";
                comboBoxPaperStatusPO.ValueMember = "Key";

    
                 


            }
            catch (Exception ex)
            {

            }

        }

        private void getComboPaperPRComplete()
        {
            try
            {
                List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

                // Add data to the List
                data.Add(new KeyValuePair<int, string>(-1, "ทุกสถานะ"));
                data.Add(new KeyValuePair<int, string>(0, "Pending PO"));
                data.Add(new KeyValuePair<int, string>(1, "Complete")); 

                //foreach (StoreCat c in allStoreCat)
                //{
                //    data.Add(new KeyValuePair<int, string>(c.StoreCatID, c.StoreCatName));
                //}

                comboBoxPaperComplete.DataSource = null;
                comboBoxPaperComplete.Items.Clear();

                // Bind the combobox
                comboBoxPaperComplete.DataSource = new BindingSource(data, null);
                comboBoxPaperComplete.DisplayMember = "Value";
                comboBoxPaperComplete.ValueMember = "Key";

                data = new List<KeyValuePair<int, string>>();

                // Add data to the List
                data.Add(new KeyValuePair<int, string>(-1, "ทุกสถานะ"));
                data.Add(new KeyValuePair<int, string>(0, "Pending GR"));
                data.Add(new KeyValuePair<int, string>(1, "Complete"));

                comboBoxPaperCompletePO.DataSource = null;
                comboBoxPaperCompletePO.Items.Clear();

                // Bind the combobox
                comboBoxPaperCompletePO.DataSource = new BindingSource(data, null);
                comboBoxPaperCompletePO.DisplayMember = "Value";
                comboBoxPaperCompletePO.ValueMember = "Key";





            }
            catch (Exception ex)
            {

            }

        }

        private void buttonCloseSrcSupplierPN_Click(object sender, EventArgs e)
        {
            panelSearchSupplier.Visible = false;
        }

        private void textBoxSRC_SupCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string srPName = textBoxSrcSupName.Text;
                string srPCode = textBoxSrcSupCode.Text;


                if (srPName.Length > 0 && srPCode.Length > 0)
                {
                    this.allSupplier_Data.DefaultView.RowFilter = string.Format("SupplierCompanyName like '*{0}*' and SupplierCode like '*{1}*' ", srPName, srPCode);

                }
                else if (srPName.Length == 0 && srPCode.Length > 0)
                {
                    this.allSupplier_Data.DefaultView.RowFilter = string.Format("SupplierCode like '*{0}*' ", srPCode);

                }
                else if (srPName.Length > 0 && srPCode.Length == 0)
                {
                    this.allSupplier_Data.DefaultView.RowFilter = string.Format("SupplierCompanyName like '*{0}*'  ", srPName);


                }
                else
                {
                    this.allSupplier_Data.DefaultView.RowFilter = string.Format(" 1 = 1 ", srPName);
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridViewAllSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridSupplierID = Int32.Parse(dataGridViewAllSupplier.Rows[e.RowIndex].Cells["SupplierID"].Value.ToString());


                if (supplierSearchFlag == 0)
                     comboBoxPOSupplier.SelectedValue = dataGridSupplierID;
                else
                    comboBoxPOSupplier_SRC.SelectedValue = dataGridSupplierID;

            }
            catch (Exception ex)
            {

            }
        }

        int supplierSearchFlag = 0;

        private void buttonSearchSupplier_Click(object sender, EventArgs e)
        {
            supplierSearchFlag = 0;
            panelSearchSupplier.Visible = true;
        }

        int poDetailAddStockID = 0;
        float stroreConvertRate_PO = 0;
        float storeQtyold_PO = 0;
        private void dataGridViewPODetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                poDetailAddStockID = Int32.Parse(dataGridViewPODetail["AddStockID", e.RowIndex].Value.ToString());


                this.flagSave = 1;
                //   FlagSaveChange(); 

                int editStoreID = Int32.Parse(dataGridViewPODetail.Rows[e.RowIndex].Cells["StoreID"].Value.ToString());
                float storeQty = float.Parse(dataGridViewPODetail.Rows[e.RowIndex].Cells["StoreQTY"].Value.ToString());
                storeQtyold_PO = float.Parse(dataGridViewPODetail.Rows[e.RowIndex].Cells["StoreQTY"].Value.ToString());
                
                string storeUnit = dataGridViewPODetail.Rows[e.RowIndex].Cells["StoreUnit"].Value.ToString();
                float strorePrice = float.Parse(dataGridViewPODetail.Rows[e.RowIndex].Cells["StorePrice"].Value.ToString());
                string storeAddType = dataGridViewPODetail.Rows[e.RowIndex].Cells["AddType"].Value.ToString();
                stroreConvertRate_PO = float.Parse(dataGridViewPODetail.Rows[e.RowIndex].Cells["StoreConvertRate"].Value.ToString());
             
                comboBoxListStore_PO.SelectedValue = editStoreID;
                txtBoxAddStoreQTY_PO.Text = storeQty.ToString(); 
                txtBoxAddStorePrice_PO.Text = strorePrice.ToString();
                labelStoreUnit_PO.Text = storeUnit;
                comboBoxAddPOUnitType.Text = storeAddType; 
              //  comboBoxListStore_PO.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonPOSave_Click(object sender, EventArgs e)
        {
            try
            {

                float storeaddStoreQTY = float.Parse(txtBoxAddStoreQTY_PO.Text);
                int selectStoreID = Int32.Parse(comboBoxListStore_PO.SelectedValue.ToString());
                string storeName = comboBoxListStore_PO.Text;
                string addStockType = comboBoxAddPOUnitType.Text;
                string storeRemark = textBoxStoreRemark_PO.Text;
                float storeaddPrice = float.Parse(txtBoxAddStorePrice_PO.Text);
                float storeaddStoreAmt = storeaddStoreQTY * storeaddPrice;
                float storeaddStoreVat = 0;


                if (this.flagSave == 0)
                { 
                    DateTime docdate = dateTimePODocDate.Value;
                    DateTime podate = dateTimePODate.Value;
                    string poRemark = TextBoxPORemark.Text;
                    int poSupID = Int32.Parse(comboBoxPOSupplier.SelectedValue.ToString());
                    string poTearmofPayment = comboBoxPOPaymentTerm.Text;
                    string poLevel = comboBoxPOLevel.Text;
                    string dateAddStock = dateTimePODate.Value.ToString("yyyyMMdd");

                    int poVATType = 0;

                    if (radioButtonPO_NonVAT.Checked)
                        poVATType = 1;
                    else if (radioButtonPO_ExcVAT.Checked)
                        poVATType = 3;
                    else if (radioButtonPO_IncVAT.Checked)
                        poVATType = 2;

                    float poDiscount = float.Parse(textBoxPODiscount.Text);

                    int result = gd.instPC_NewPO(this.POCode, docdate, podate, "NoPR", poRemark, poSupID, poTearmofPayment, poLevel, poVATType, poDiscount, Login.userName, selectStoreID, dateAddStock, storeaddStoreQTY, storeaddPrice, storeaddStoreAmt, 0, storeRemark, "", addStockType, stroreConvertRate_PO);

                    //  POCode = "0";
                    poAllDATA = gd.getPC_DataPOHeader("0", 0);
                    dataGridViewPOHeader.DataSource = poAllDATA;
                    panelPOTakeOne.Visible = true;
                    panelPRDetailRef.Visible = false;

                    viewDetailofPOByCode(); 
                    setDefaultPO();

                }
                else
                {


                    Button bt = (Button)sender;


                    if (bt.Name == "buttonPODelete")
                        storeaddStoreQTY = 0;


                    if (MessageBox.Show("คุณต้องการจะแก้ไข : " + storeName + "   ใน PO" + POCode + " หรือไม่ ?", "แก้ไข " + storeName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updatePC_PODetail(POCode, poDetailAddStockID, selectStoreID, storeQtyold_PO, storeaddStoreQTY, storeaddPrice, storeaddStoreAmt, storeaddStoreVat, storeRemark, addStockType, stroreConvertRate_PO);

                        if (result == 3)
                        {
                            MessageBox.Show("ไม่สามารถแก้ไขรายการได้ เนื่องจากอนุมัติแล้ว");

                        }
                        else
                        {
                            if (bt.Name == "buttonPODelete")
                                MessageBox.Show("ลบ " + storeName + " ใน PO" + POCode + " >> (Success)");
                            else
                                MessageBox.Show("แก้ไข " + storeName + " ใน PO" + POCode + " >> (Success)");



                            poAllDATA = gd.getPC_DataPOHeader("0", 0);
                            dataGridViewPOHeader.DataSource = poAllDATA;


                            textBoxPOCode.Text = POCode;
                            buttonHeaderPO_Click(sender, e);

                            viewDetailofPOByCode(); 
                      //      setDefaultPO();
                            //ComboboxStore_Change(null, e);
                        }

                    }
                }

            }
            catch (Exception ex)
            {


            }
        }

        private void buttonPRSearchStore_PO_Click(object sender, EventArgs e)
        {
            buttonCloseSrcStorePN_PO.Visible = true;
        }

        private void buttonCloseSrcStorePanel_PO_Click(object sender, EventArgs e)
        {
            buttonCloseSrcStorePN_PO.Visible = false;
        }

        private void textBoxSrcInvCode_PO_TextChanged(object sender, EventArgs e)
        {
            searchStoreLists();

        }

        private void searchStoreLists()
        {
            try
            {

                string invSearch = textBoxSrcInvCode_PO.Text;

                if (this.selectSuplierCode.Length > 0)
                {
                    if (invSearch.Length > 0)
                        this.dataAllStore_PO.DefaultView.RowFilter = string.Format(" ( StoreName like '*{0}*' or StoreOrder like '*{0}*' )  and StoreSupCode like '*{1}*'", invSearch, this.selectSuplierCode);

                    else
                        this.dataAllStore_PO.DefaultView.RowFilter = string.Format(" StoreSupCode like '*{0}*'  ", selectSuplierCode);

                }
                else
                {
                    if (invSearch.Length > 0)
                        this.dataAllStore_PO.DefaultView.RowFilter = string.Format("StoreName like '*{0}*' or StoreOrder like '*{0}*' ", invSearch);

                    else
                        this.dataAllStore_PO.DefaultView.RowFilter = string.Format(" 1 = 1 ", invSearch);
                }

            }
            catch (Exception ex)
            {

            }
        }


        private void dataGridViewAllStore_PO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridStoreCatID = Int32.Parse(dataGridViewAllStore_PO.Rows[e.RowIndex].Cells["StoreCatID"].Value.ToString());
                int dataGridStoreID = Int32.Parse(dataGridViewAllStore_PO.Rows[e.RowIndex].Cells["StoreID"].Value.ToString());


                //  comboBoxAllCat.SelectedValue = dataGridStoreCatID;
                comboBoxListStore_PO.SelectedValue = dataGridStoreID;

            }
            catch (Exception ex)
            {

            }
        }

        string storeUnitAdd_PO_S = "";
        string storeUnitAdd_PO_B = "";

        private void comboBoxListStore_PO_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                int selectStoreID = Int32.Parse(comboBoxListStore_PO.SelectedValue.ToString());

                foreach (Store c in allStores)
                {
                    if (c.StoreID == selectStoreID)
                    {
                        storeUnitAdd_PO_S = c.StoreUnit;
                        storeUnitAdd_PO_B = c.StoreConvertUnit;
                        this.stroreConvertRate_PO = float.Parse(c.StoreConvertRate.ToString());
                    }
                }

                if (comboBoxAddPOUnitType.Text == "B")
                    labelStoreUnit_PO.Text = storeUnitAdd_PO_B;
                else
                    labelStoreUnit_PO.Text = storeUnitAdd_PO_S;

                 

            }
            catch (Exception ex)
            {

            }
        }

        private void comboBoxAddPOUnitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAddPOUnitType.Text == "B")
                labelStoreUnit_PO.Text = storeUnitAdd_PO_B;
            else
                labelStoreUnit_PO.Text = storeUnitAdd_PO_S;
        }

        private void buttonHeaderPO_Click(object sender, EventArgs e)
        {
            try
            {
                string poNO = textBoxPOCode.Text;
                DateTime docdate = dateTimePODocDate.Value;
                DateTime podate = dateTimePODate.Value;
                string poRemark = TextBoxPORemark.Text;
                int poSupID = Int32.Parse(comboBoxPOSupplier.SelectedValue.ToString());
                string poTearmofPayment = comboBoxPOPaymentTerm.Text;
                string poLevel = comboBoxPOLevel.Text;
                string dateAddStock = dateTimePODate.Value.ToString("yyyyMMdd");

                int poVATType = 0;

                if (radioButtonPO_NonVAT.Checked)
                    poVATType = 1;
                else if (radioButtonPO_ExcVAT.Checked)
                    poVATType = 3;
                else if (radioButtonPO_IncVAT.Checked)
                    poVATType = 2;

                float poDiscount = float.Parse(textBoxPODiscount.Text);

                int result = gd.updsPC_POHeader(poNO, docdate, podate, "NoPR", poRemark, poSupID, poTearmofPayment, poLevel, poVATType, poDiscount, Login.userName);

                //  POCode = "0";
                poAllDATA = gd.getPC_DataPOHeader("0", 0);
                dataGridViewPOHeader.DataSource = poAllDATA;
                panelPOTakeOne.Visible = true;
                panelPRDetailRef.Visible = false;
               
                viewDetailofPOByCode();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void buttonHeaderPR_Click(object sender, EventArgs e)
        {
            try
            {


                int addstocksign = 1;


                //Header
                PRCode = textBoxPRCode.Text;
                DateTime docdate = dateTimePickerPRDocDate.Value;
                DateTime prdate = dateTimePickerPRDate.Value;
                string quotationNo = textBoxPRQuoNo.Text;
                string objective = textBoxPRObjectTive.Text;
                string prRemark = textBoxPRRemark.Text;
                int sup1 = Int32.Parse(comboBoPRSuplier1.SelectedValue.ToString());
                int sup2 = Int32.Parse(comboBoPRSuplier2.SelectedValue.ToString());
                int sup3 = Int32.Parse(comboBoPRSuplier3.SelectedValue.ToString());
                string prBy = textBoxPRBy.Text;
                string dateAddStock = dateTimePickerPRDate.Value.ToString("yyyyMMdd");

                 

                if (MessageBox.Show("คุณต้องการจะแก้ไข Header PR  : " + PRCode + " หรือไม่ ?", "แก้ไข PR Header ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.updsPC_PRHeader(PRCode, docdate, prdate, quotationNo, objective, prRemark, sup1, sup2, sup3, prBy);

                    viewDetailofPRByCode(); 
                    setDefault();
                } 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void comboBoxPaperComplete_SelectedValueChanged(object sender, EventArgs e)
        { 
            prDataSearch(); 
        }

        private void textBoxPRCode_Src_TextChanged(object sender, EventArgs e)
        {
            prDataSearch();
        }


        private void prDataSearch()
        {

            try
            {

                string srFlagComplete = comboBoxPaperComplete.SelectedValue.ToString();
                string srPRCode  = textBoxPRCode_Src.Text;


                if (srFlagComplete == "-1")
                    this.prAllDATA.DefaultView.RowFilter = string.Format("PRCode like '*{0}*' ", srPRCode);
                else
                    this.prAllDATA.DefaultView.RowFilter = string.Format(" PRComplete = {0} and PRCode like '*{1}*' ", srFlagComplete, srPRCode);
            }
            catch (Exception ex)
            {

            }

        }

        private void comboBoxPaperStatusPO_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int status = Int32.Parse(comboBoxPaperStatusPO.SelectedValue.ToString());
                poAllDATA = gd.getPC_DataPOHeader("0", status);
                dataGridViewPOHeader.DataSource = poAllDATA;
            }
            catch (Exception ex)
            {


            }
        }


        private void poDataSearch()
        {

            try
            {

                int srSupID = Int32.Parse(comboBoxPOSupplier_SRC.SelectedValue.ToString());
                string srPOCode = textBoxPOCode_Src.Text;
                string srPRCode = textBoxPRRef_Src.Text;


                string srFlagComplete = comboBoxPaperCompletePO.SelectedValue.ToString(); 


                if (srFlagComplete == "-1")
                    this.prAllDATA.DefaultView.RowFilter = string.Format("PRCode like '*{0}*' ", srPRCode);
                else
                    this.prAllDATA.DefaultView.RowFilter = string.Format(" PRComplete = {0} and PRCode like '*{1}*' ", srFlagComplete, srPRCode);



                if (srFlagComplete == "-1")
                { 
                    if (srSupID == 0)
                    {
                        this.poAllDATA.DefaultView.RowFilter = string.Format("POCode like '*{0}*' and PRReference like '*{1}*' ", srPOCode, srPRCode);
                    }
                    else
                    {
                        this.poAllDATA.DefaultView.RowFilter = string.Format("POCode like '*{0}*' and PRReference like '*{1}*' and POSupplierID = {2} ", srPOCode, srPRCode, srSupID);
                    }
                }
                else
                {
                    if (srSupID == 0)
                    {
                        this.poAllDATA.DefaultView.RowFilter = string.Format("POCode like '*{0}*' and PRReference like '*{1}*' and POComplete = {2} ", srPOCode, srPRCode, srFlagComplete);
                    }
                    else
                    {
                        this.poAllDATA.DefaultView.RowFilter = string.Format("POCode like '*{0}*' and PRReference like '*{1}*' and POSupplierID = {2} and POComplete = {3} ", srPOCode, srPRCode, srSupID, srFlagComplete);
                    }

                }




            }
            catch (Exception ex)
            {

            }

        }

        private void textBoxPOCode_Src_TextChanged(object sender, EventArgs e)
        {
            poDataSearch();
        }

        private void textBoxPRRef_Src_TextChanged(object sender, EventArgs e)
        {
            poDataSearch();
        }

        private void comboBoxPOSupplier_SRC_SelectedValueChanged(object sender, EventArgs e)
        {
            poDataSearch();
        }

        private void textBoxSCStoreCode_PO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {

                    string barcodestr = textBoxSCStoreCode_PO.Text;

                    foreach (Store c in allStores)
                    {
                        if (c.StoreOrder.ToString() == barcodestr)
                        {
                            // comboBoxAllCat.SelectedValue = c.StoreCatID;
                            comboBoxListStore_PO.SelectedValue = c.StoreID;
                            textBoxSCStoreCode_PO.Text = "";
                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void textBoxSCStoreBarcode_PO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {

                    string barcodestr = textBoxSCStoreBarcode_PO.Text;

                    foreach (Store c in allStores)
                    {
                        if (c.StoreBarcode == barcodestr)
                        {
                            // comboBoxAllCat.SelectedValue = c.StoreCatID;
                            comboBoxListStore_PO.SelectedValue = c.StoreID;
                            textBoxSCStoreBarcode_PO.Text = "";
                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void comboBoxPaperCompletePO_SelectedValueChanged(object sender, EventArgs e)
        {
            poDataSearch();
        }

        private void buttonSearchSupplier_SRC_Click(object sender, EventArgs e)
        {
            supplierSearchFlag = 1;
            panelSearchSupplier.Visible = true;
        }

        int selectSuplierID = 0;
        string selectSuplierCode = "";


        private void changeSupOption()
        {
            try
            {
                this.selectSuplierID = Int32.Parse(comboBoxPOSupplier.SelectedValue.ToString());

                if (radioBoxSuplierMapPrice.Checked)
                {
                    dataAllStore = gd.getAllStoreSuplier_Search(0, this.selectSuplierID);

                    dataAllStore_PO = dataAllStore;
                    dataGridViewAllStore_PO.DataSource = dataAllStore_PO;

                    if (dataGridViewAllStore_PO.Rows.Count > 0)
                    {
                        dataGridViewAllStore_PO.Columns[0].Visible = false;
                        dataGridViewAllStore_PO.Columns[1].Visible = false;
                        dataGridViewAllStore_PO.Columns[2].HeaderText = "รหัสสินค้า";
                        dataGridViewAllStore_PO.Columns[3].HeaderText = "ชื่อ";
                        dataGridViewAllStore_PO.Columns[4].Visible = false;
                        dataGridViewAllStore_PO.Columns[5].HeaderText = "หน่วย (ใหญ่)";
                        dataGridViewAllStore_PO.Columns[6].Visible = false;
                        dataGridViewAllStore_PO.Columns[7].HeaderText = "ราคา (บาท)";
                        dataGridViewAllStore_PO.Columns[8].HeaderText = "จำนวนสั่งซื้อ";
                        dataGridViewAllStore_PO.Columns[9].HeaderText = "หมายเหตุ";
                    }
                }
                else
                {
                    dataAllStore = gd.getAllStore_Search(0);

                    if (this.selectSuplierID > 0)
                    {
                        foreach (Supplier s in allSupplier)
                            if (this.selectSuplierID == s.SupplierID)
                                this.selectSuplierCode = s.SupplierCode;
                    }
                    else
                    {
                        this.selectSuplierCode = "";
                    }


                    dataAllStore_PO = dataAllStore;
                    dataGridViewAllStore_PO.DataSource = dataAllStore_PO;

                    if (dataGridViewAllStore_PO.Rows.Count > 0)
                    {
                        dataGridViewAllStore_PO.Columns[0].Visible = false;
                        dataGridViewAllStore_PO.Columns[1].Visible = false;
                        dataGridViewAllStore_PO.Columns[2].HeaderText = "รหัสสินค้า";
                        dataGridViewAllStore_PO.Columns[3].HeaderText = "ชื่อ";
                        dataGridViewAllStore_PO.Columns[4].Visible = false;
                        dataGridViewAllStore_PO.Columns[5].HeaderText = "หน่วย (ใหญ่)";
                        dataGridViewAllStore_PO.Columns[6].Visible = false;
                        dataGridViewAllStore_PO.Columns[7].HeaderText = "ราคา (บาท)";
                        dataGridViewAllStore_PO.Columns[8].HeaderText = "จำนวนสั่งซื้อ";
                        dataGridViewAllStore_PO.Columns[9].HeaderText = "หมายเหตุ";
                    }

                    searchStoreLists();
                }

            }
            catch (Exception ex)
            {
                this.selectSuplierID = 0;
                this.selectSuplierCode = "";
            }
        }

        private void comboBoxPOSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeSupOption();
        }

        private void buttonSAVEListPO_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBoxPOCode.Text.Length == 0)
                    throw new Exception("กรุณาระบุเลขที่ PO ที่จะเพิ่มรายการ");

                if (comboBoxPOSupplier.SelectedIndex == 0)
                    throw new Exception("กรุณาระบุ Supplier ที่จะเปิด PO");


                // Header 
                DateTime docdate = dateTimePODocDate.Value;
                DateTime podate = dateTimePODate.Value;
                string poRemark = TextBoxPORemark.Text;
                int poSupID = Int32.Parse(comboBoxPOSupplier.SelectedValue.ToString());
                string poTearmofPayment = comboBoxPOPaymentTerm.Text;
                string poLevel = comboBoxPOLevel.Text;
                string dateAddStock = dateTimePODate.Value.ToString("yyyyMMdd");

 

                int poVATType = 0;

                if (radioButtonPO_NonVAT.Checked)
                    poVATType = 1;
                else if (radioButtonPO_ExcVAT.Checked)
                    poVATType = 3;
                else if (radioButtonPO_IncVAT.Checked)
                    poVATType = 2;

                float poDiscount = float.Parse(textBoxPODiscount.Text);


                float storeaddStoreQTY = 0;
                int selectStoreID = 0;
                string storeName = "";
                string addStockType = "";
                string storeRemark = "";
                float storeaddPrice = 0;
                float storeaddStoreAmt = 0;
                int result = 0;

                foreach (DataGridViewRow row in dataGridViewAllStore_PO.Rows)
                {

                    selectStoreID = Int32.Parse(row.Cells["StoreID"].Value.ToString());
                    storeName = row.Cells["StoreName"].Value.ToString();
                    storeaddStoreQTY = float.Parse(row.Cells["POBigUnit"].Value.ToString());
                    storeaddPrice = float.Parse(row.Cells["POBigPrice"].Value.ToString());
                    storeRemark = row.Cells["PORemark"].Value.ToString(); 
                    addStockType = "B";
                    storeaddStoreAmt = storeaddStoreQTY * storeaddPrice;

                    if (storeaddStoreQTY > 0 )
                        result = gd.instPC_NewPO(this.POCode, docdate, podate, "NoPR", poRemark, poSupID, poTearmofPayment, poLevel, poVATType, poDiscount, Login.userName, selectStoreID, dateAddStock, storeaddStoreQTY, storeaddPrice, storeaddStoreAmt, 0, storeRemark, "", addStockType, stroreConvertRate_PO);
                     
                }
                 

                poAllDATA = gd.getPC_DataPOHeader("0", 0);
                dataGridViewPOHeader.DataSource = poAllDATA;
                panelPOTakeOne.Visible = true;
                panelPRDetailRef.Visible = false;
                buttonCloseSrcStorePN_PO.Visible = false;

                viewDetailofPOByCode();
                setDefaultPO();

                MessageBox.Show("ทำรายการสำเร็จ");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void radioBoxSuplierManualPrice_CheckedChanged(object sender, EventArgs e)
        {
            changeSupOption();
        }

        private void buttonPORefresh_Click(object sender, EventArgs e)
        {
            POCode = "0";
            poAllDATA = gd.getPC_DataPOHeader(POCode, 0);
            dataGridViewPOHeader.DataSource = poAllDATA;
        }

        private void buttonCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelPRApproved_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
