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
    public partial class AddGRN : AddDataTemplate
    {

        GetDataRest gd;

        string GRCode;


        DataTable grAllDATA;
        DataTable grDetail;

        List<Supplier> allSupplier;
        List<Inventory> allInven;
        DataTable allSupplier_Data;
        int flagSupSearch = 0;

        //DataTable dataAllStore;
          

        DataTable poAllDATA;
        DataTable poDetail;

        string POCode;
        DataTable poSelected;

        public AddGRN(Form frmlkFrom, int flagFrmClose)
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
 
                dateTimePickerGRDate.Value = DateTime.Now;

                allSupplier = gd.getPC_Supplier(0, "0", "0");
                getComboAllSuplier();

                allInven = gd.getAllInventory(0, "0", "0", "0");
                getComboAllInventory();

                getComboPaperStstus();

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
                 

                 // PO


                POCode = "0";
                poAllDATA = gd.getPC_DataPOHeader(POCode, 3);
                dataGridViewPOHeader.DataSource = poAllDATA;


                dateTimePODocDate.Value = DateTime.Now;
                dateTimePODate.Value = DateTime.Now;


                // GR

                GRCode = "0";
                grAllDATA = gd.getPC_DataGRHeader(GRCode, 0);
                dataGridViewGRHeader.DataSource = grAllDATA;

                dataGridViewPODetail.Columns[4].Visible = false;
                dataGridViewPODetail.Columns[5].Visible = false;

                dateTimePickerGRDate.Value = DateTime.Now;

                GRCode = gd.getPC_NewGRCode("");
                textBoxGRCode.Text = GRCode;


         

            }
            catch (Exception ex)
            {


            }
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
            comboBoxGRInvID.DataSource = null;
            comboBoxGRInvID.Items.Clear();

            // Bind the combobox
            comboBoxGRInvID.DataSource = new BindingSource(data, null);
            comboBoxGRInvID.DisplayMember = "Value";
            comboBoxGRInvID.ValueMember = "Key";
 

        }

        private void getComboAllSuplier()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List
            data.Add(new KeyValuePair<int, string>(0, "= ต้องการ Supplier 1 ="));

            foreach (Supplier c in allSupplier)
            {
                if (c.FlagUse == "Y")
                {
                    data.Add(new KeyValuePair<int, string>(c.SupplierID, c.SupplierName));
                }

            }
           


            comboBoxPOSupplier.DataSource = null;
            comboBoxPOSupplier.Items.Clear();

            // Bind the combobox
            comboBoxPOSupplier.DataSource = new BindingSource(data, null);
            comboBoxPOSupplier.DisplayMember = "Value";
            comboBoxPOSupplier.ValueMember = "Key";


            // GR


            comboBoxPOSupplier_Ref.DataSource = null;
            comboBoxPOSupplier_Ref.Items.Clear();

            // Bind the combobox
            comboBoxPOSupplier_Ref.DataSource = new BindingSource(data, null);
            comboBoxPOSupplier_Ref.DisplayMember = "Value";
            comboBoxPOSupplier_Ref.ValueMember = "Key";


            // GR Srarch

            comboBoxGRSupplier_SRC.DataSource = null;
            comboBoxGRSupplier_SRC.Items.Clear();

            // Bind the combobox
            comboBoxGRSupplier_SRC.DataSource = new BindingSource(data, null);
            comboBoxGRSupplier_SRC.DisplayMember = "Value";
            comboBoxGRSupplier_SRC.ValueMember = "Key";



            comboBoxPOSupplier_SRC.DataSource = null;
            comboBoxPOSupplier_SRC.Items.Clear();

            // Bind the combobox
            comboBoxPOSupplier_SRC.DataSource = new BindingSource(data, null);
            comboBoxPOSupplier_SRC.DisplayMember = "Value";
            comboBoxPOSupplier_SRC.ValueMember = "Key";

            


        }

        private void buttonAddPRDetail_Click(object sender, EventArgs e)
        {
            panelGRDetail.Visible = true;

        }

        private void buttonCloseBC_Click(object sender, EventArgs e)
        {
            panelGRDetail.Visible = false;
        }

        private void textBoxPONo_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (e.KeyChar == (char)13)
                {
                  //  panelTakeOne.Visible = true;

                    if (textBoxGRCode.Text.Length > 0)
                    {
                        GRCode = textBoxGRCode.Text;
                        grAllDATA = gd.getPC_DataGRHeader(GRCode, 0);
                        dataGridViewGRHeader.DataSource = grAllDATA;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         
       

        private void buttonClosePanelPRDetailRef_Click(object sender, EventArgs e)
        {
            panelPODetailRef.Visible = false;
        }

        private void buttonAddPODetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxPOCode.Text.Length == 0)
                    throw new Exception("กรุณาระบุเลขที่ PO ที่จะเพิ่มรายการ");

                if (comboBoxPOSupplier.SelectedIndex == 0)
                    throw new Exception("กรุณาระบุ Supplier ที่จะเปิด PO");


        

                string dateAddStock = dateTimePODocDate.Value.ToString("yyyyMMdd");

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
                string PRRefAdd = textBoxGR_PORef.Text;
                    

                List<StockChange> stcLists = new List<StockChange>();

                foreach (DataGridViewRow row in dataGridViewGRDetailRef.Rows)
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
                        storeRemark = row.Cells["StoreRemark"].Value.ToString();

                        if (storeAddBigUnit > 0)
                        {
                            flagAddStore = "B";
                            storeAmt = storeAddBigUnit*storeAddBigPrice;

                            stcLists.Add(new StockChange(storeID, storeName, storeAddBigUnit, storeBigUnit, storeAddBigPrice, storeAmt, storeRemark, flagAddStore, PRRefAdd, storeConvertRate));
                        }
                        else
                        {
                            flagAddStore = "S";
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

             

                float poDiscount = 0;

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
                panelPODetailRef.Visible = false;
               
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
                     
                    dataGridViewPODetail.Columns[1].Visible = false;
                    dataGridViewPODetail.Columns[2].Visible = false;
                    dataGridViewPODetail.Columns[3].Visible = false;
                    dataGridViewPODetail.Columns[4].Visible = false;
                    //dataGridViewPRDetail.Columns[10].Visible = false;
                    //dataGridViewPRDetail.Columns[11].Visible = false;

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
                panelPOApproved.Visible = true;


                // Read Data Grid
                POCode = dataGridViewPOHeader.Rows[e.RowIndex].Cells["POCode"].Value.ToString();

                textBoxGR_PORef.Text = POCode;

                DateTime docdate = dateTimePODocDate.Value;
                DateTime podate = dateTimePODate.Value; 
                int poVATType = 0;
 

                int poStatus = Int32.Parse(dataGridViewPOHeader.Rows[e.RowIndex].Cells["POStatus"].Value.ToString());
                int poSupID = Int32.Parse(dataGridViewPOHeader.Rows[e.RowIndex].Cells["POSupplierID"].Value.ToString());


                string poTearmofPayment = dataGridViewPOHeader.Rows[e.RowIndex].Cells["PaymentTerm"].Value.ToString();
                string poLevel = dataGridViewPOHeader.Rows[e.RowIndex].Cells["POLevel"].Value.ToString();
                string poRemark = dataGridViewPOHeader.Rows[e.RowIndex].Cells["Remark"].Value.ToString(); 
                string remarkApproved = dataGridViewPOHeader.Rows[e.RowIndex].Cells["RemarkApprove"].Value.ToString();
                string approvedBy = dataGridViewPOHeader.Rows[e.RowIndex].Cells["ApproveBy"].Value.ToString();


                DateTime dtPODocDate = DateTime.Parse(dataGridViewPOHeader.Rows[e.RowIndex].Cells["PODocDate"].Value.ToString());
                DateTime dtDate = DateTime.Parse(dataGridViewPOHeader.Rows[e.RowIndex].Cells["PODate"].Value.ToString());


                dateTimePODocDate.Value = dtPODocDate;
                dateTimePODate.Value = dtDate;

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

                textBoxPOApprovedRemark.Text = remarkApproved;
                textBoxPOApproveBy.Text = approvedBy;


                radioBoxPOWaitApprove.Enabled = true;
                radioBoxPOReject.Enabled = true;
                radioBoxPOApproved.Enabled = true;
                buttonPOAppSAVE.Visible = true;

                if (poStatus == 3)
                {
                    radioBoxPOWaitApprove.Enabled = false;
                    radioBoxPOReject.Enabled = false;
                    radioBoxPOApproved.Enabled = false;
                    radioBoxPOCancle.Enabled = false; 
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


                if (result <= 0)
                {
                    MessageBox.Show("ไม่สามารถทำรายการได้ เนื่องจากอนุมัติไปแล้ว");
                }
                else
                {
                    MessageBox.Show("ทำรายการ สำเร็จ " + this.POCode);

                    setDefaultPO();
                    //ComboboxStore_Change(null, e);
                }


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


            poAllDATA = gd.getPC_DataPOHeader("0", 3);
            dataGridViewPOHeader.DataSource = poAllDATA;

            //comboBoxListStore.SelectedIndex = 0;

            //comboBoxListStore.Enabled = true;
            //flagSave = 0;



            radioBoxPOWaitApprove.Enabled = true;
            radioBoxPOReject.Enabled = true;
            radioBoxPOApproved.Enabled = true;
            buttonPOAppSAVE.Visible = true;

            textBoxPOApprovedRemark.Text = "";





        }

        private void buttonNewGR_Click(object sender, EventArgs e)
        {
            GRCode = gd.getPC_NewGRCode(""); 
            textBoxGRCode.Text = GRCode;
        }

        private void buttonSearchPO_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBoxGR_PORef.Text.Length > 0)
                {
                    panelPODetailRef.Visible = true;


                    POCode = textBoxGR_PORef.Text;
                    poSelected = gd.getPC_DataPOHeader(POCode, 0);

                    int supID = 0;

                    foreach (DataRow row in poSelected.Rows)
                    {
                        supID = Int32.Parse(row["POSupplierID"].ToString());
                    }

                    comboBoxPOSupplier_Ref.SelectedValue = supID;



                    poDetail = gd.getPC_DataPODetail_Ref(textBoxGR_PORef.Text, 0);
                    dataGridViewGRDetailRef.DataSource = poDetail;

                    dataGridViewGRDetailRef.Columns[0].Visible = false;
                    dataGridViewGRDetailRef.Columns[1].Visible = false;
                    dataGridViewGRDetailRef.Columns[2].Visible = false;
                    dataGridViewGRDetailRef.Columns[3].Visible = false;
                    dataGridViewGRDetailRef.Columns[4].HeaderText = "ชื่อสินค้า/วัตถุดิบ";
                    dataGridViewGRDetailRef.Columns[5].HeaderText = "จำนวน PO";
                    dataGridViewGRDetailRef.Columns[6].HeaderText = "หน่วย PO";
                    dataGridViewGRDetailRef.Columns[7].HeaderText = "ราคา PO";
                    dataGridViewGRDetailRef.Columns[8].HeaderText = "เป็นเงิน PO";
                    dataGridViewGRDetailRef.Columns[9].Visible = false;
                    //dataGridViewGRDetailRef.Columns[9].HeaderText = "ราคา (รับ)";
                    dataGridViewGRDetailRef.Columns[10].HeaderText = "จำนวน (รับ)";
                    dataGridViewGRDetailRef.Columns[11].HeaderText = "เป็นเงิน";
                    dataGridViewGRDetailRef.Columns[12].HeaderText = "เหตุผล";
                    dataGridViewGRDetailRef.Columns[13].Visible = false;
                    dataGridViewGRDetailRef.Columns[14].Visible = false;


                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            } 
        }

        private void dataGridViewGRHeader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (Login.userStatus == "Admin" || Login.userStatus == "Manager" || Login.userStatus == "UserStock")
                    panelGRApproved.Visible = true;
                else
                    panelGRApproved.Visible = false; 


                

                // Read Data Grid
                GRCode = dataGridViewGRHeader.Rows[e.RowIndex].Cells["GRCode"].Value.ToString();

                DateTime grdate = DateTime.Parse(dataGridViewGRHeader.Rows[e.RowIndex].Cells["GRDate"].Value.ToString());

                int grSupID = Int32.Parse(dataGridViewGRHeader.Rows[e.RowIndex].Cells["GRSupplierID"].Value.ToString());
                int grINVID = Int32.Parse(dataGridViewGRHeader.Rows[e.RowIndex].Cells["GRInvID"].Value.ToString());
                string grPO_Ref = dataGridViewGRHeader.Rows[e.RowIndex].Cells["PORefference"].Value.ToString();
                string grRemark = dataGridViewGRHeader.Rows[e.RowIndex].Cells["Remark"].Value.ToString();
                string grBy = dataGridViewGRHeader.Rows[e.RowIndex].Cells["GRBy"].Value.ToString();
                string remarkApproved = dataGridViewPOHeader.Rows[e.RowIndex].Cells["RemarkApprove"].Value.ToString();
                string approvedBy = dataGridViewPOHeader.Rows[e.RowIndex].Cells["ApproveBy"].Value.ToString();
               
                int grStatus = Int32.Parse(dataGridViewGRHeader.Rows[e.RowIndex].Cells["GRStatus"].Value.ToString());

                // input to Object

                textBoxGRCode.Text = GRCode; 
                textBoxGRRemark.Text = grRemark;
                textBoxGRBy.Text = grBy;
                textBoxGR_PORef.Text = grPO_Ref;

                comboBoxPOSupplier_Ref.SelectedValue = grSupID;
                comboBoxGRInvID.SelectedValue = grINVID;

                dateTimePickerGRDate.Value = grdate;




                if (grStatus == 1)
                    radioBoxGRWaitApprove.Checked = true; 
                else if (grStatus == 3)
                    radioBoxGRApproved.Checked = true; 
                else if (grStatus == 5)
                    radioBoxGRCancle.Checked = true;

                textBoxGRApprovedRemark.Text = remarkApproved;
                textBoxGRApproveBy.Text = approvedBy;


                radioBoxGRWaitApprove.Enabled = true;
                radioBoxGRApproved.Enabled = true;
                radioBoxGRCancle.Enabled = true;
                buttonGRAppSAVE.Visible = true;

                if (grStatus >= 3)
                {
                    radioBoxGRWaitApprove.Enabled = false;
                    radioBoxGRApproved.Enabled = false;
                    radioBoxGRCancle.Enabled = false;
                }

                if (Login.userStatus == "Admin")
                {
                    radioBoxGRWaitApprove.Enabled = true;
                    radioBoxGRApproved.Enabled = true;
                    radioBoxGRCancle.Enabled = true;
                }
                 
                 
                viewDetailofGRByCode();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

      

        private void buttonAddGRDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxGRCode.Text.Length < 4)
                    throw new Exception("กรุณาระบุเลขที่ GR รับของ");

                if (comboBoxPOSupplier_Ref.SelectedIndex == 0)
                    throw new Exception("กรุณาระบุ Supplier GR รับของ (มาจาก PO)");  // มาจาก PO

                //if (comboBoxGRInvID.SelectedIndex == 0)
                //    throw new Exception("กรุณาระบุคลังรับสินค้า");





                string dateAddStock = dateTimePickerGRDate.Value.ToString("yyyyMMdd");

                int storeID = 0;
                string storeName = "";
                //float storeAddBigUnit = 0;
                //float storeAddBigPrice = 0;
                //string storeBigUnit = "";
                float storeAddUnit = 0;
                float storeAddPrice = 0;
                string storeUnit = "";
                float storeConvertRate = 0;
                string storeRemark = "";

                float storeAmt;
                 

                string flagAddStore = "N";
                string PORefAdd = textBoxGR_PORef.Text;


                List<StockChange> stcLists = new List<StockChange>();

                foreach (DataGridViewRow row in dataGridViewGRDetailRef.Rows)
                {
                    if ( row.Cells["TakeQTY"].Value.ToString() != "0")
                    {
                        storeID = Int32.Parse(row.Cells["StoreID"].Value.ToString());
                        storeName = row.Cells["StoreName"].Value.ToString();
                        storeUnit = row.Cells["StoreUnit"].Value.ToString();  
                        storeAddUnit =   float.Parse(row.Cells["TakeQTY"].Value.ToString());
                        storeAddPrice = float.Parse(row.Cells["TakePrice"].Value.ToString());  // float.Parse(row.Cells["TakePrice"].Value.ToString());
                        storeConvertRate = float.Parse(row.Cells["StoreConvertRate"].Value.ToString());
                        storeRemark = row.Cells["StoreRemark"].Value.ToString();
                        flagAddStore = row.Cells["AddType"].Value.ToString();

                        storeAmt = storeAddUnit * storeAddPrice;
                        stcLists.Add(new StockChange(storeID, storeName, storeAddUnit, storeUnit, storeAddPrice, storeAmt, storeRemark, flagAddStore, PORefAdd, storeConvertRate));
                         
                    }
                }

                string txtInsert = "ทำรายการ GR รับของ ดังนี้ \n\r";
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
                DateTime grdate = dateTimePickerGRDate.Value;
                string grRemark = TextBoxPORemark.Text;
                int grSupID = Int32.Parse(comboBoxPOSupplier_Ref.SelectedValue.ToString());
                int grInvID = Int32.Parse(comboBoxGRInvID.SelectedValue.ToString()); 


                float poDiscount = 0;

                if (MessageBox.Show("คุณต้องการทำรายการ \n\r" + txtInsert + " หรือไม่ ?", "ทำรายการ GR รับของ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    foreach (StockChange st in stcLists)
                    {

                        result = gd.instPC_NewGR(this.GRCode, grdate, grRemark, grInvID, grSupID, PORefAdd, Login.userName, st.StoreID, dateAddStock, st.StoreAddUnit, st.StorePrice, st.StoreAmt, 0, st.StoreRemark, "", st.FlagAddStore, st.StoreConvertRate);

                    }
                }


                //  POCode = "0";
                //poAllDATA = gd.getPC_DataPOHeader("0", 0);
                //dataGridViewPOHeader.DataSource = poAllDATA;
                //panelPOTakeOne.Visible = true;
                //panelPODetailRef.Visible = false;

                grAllDATA = gd.getPC_DataGRHeader(this.GRCode, 0);
                dataGridViewGRHeader.DataSource = grAllDATA;

                panelPOTakeOne.Visible = true;
                panelPODetailRef.Visible = false;

                viewDetailofPOByCode();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void buttonClosePanelPRDetailRef_Click_1(object sender, EventArgs e)
        {
            panelPODetailRef.Visible = false;
        }

        private void buttonAddGRDetailTK_Click(object sender, EventArgs e)
        {
            panelGRDetail.Visible = true;
            viewDetailofGRByCode();
        }


        private void viewDetailofGRByCode()
        {
            try
            {
                GRCode = textBoxGRCode.Text;

                if (GRCode.Length > 0)
                {
                    // MessageBox.Show(POCode);

                    grDetail = gd.getPC_DataGRDetail(GRCode, 0);
                    dataGridViewGRDetail.DataSource = grDetail;

                    dataGridViewGRDetail.Columns[0].Visible = false;
                    dataGridViewGRDetail.Columns[1].Visible = false;
                 //   dataGridViewGRDetail.Columns[2].Visible = false;
                    dataGridViewGRDetail.Columns[3].Visible = false;
                    dataGridViewGRDetail.Columns[4].Visible = false;
                    //dataGridViewPRDetail.Columns[10].Visible = false;
                    //dataGridViewPRDetail.Columns[11].Visible = false;

                    dataGridViewGRDetail.Columns[2].HeaderText = "No.";

                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }


        }

        private void buttonPrintA4_Click(object sender, EventArgs e)
        {

            Button printButton = (Button)sender;
            string rptCode = "0";
            string rptType = "0";

            try
            {
                rptType = printButton.Name.Replace("buttonPrint", "").Trim();

                if (rptType == "GR")
                    rptCode = this.GRCode; 


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

        private void buttonPrintPO_Click(object sender, EventArgs e)
        {
            Button printButton = (Button)sender;
            string rptCode = "0";
            string rptType = "0";

            try
            {
                rptType = printButton.Name.Replace("buttonPrint", "").Trim();

                if (rptType == "PO")
                    rptCode = this.POCode;


                LinkFromRptBillReport(rptCode, rptType);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxPaperStatusGR_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int status = Int32.Parse(comboBoxPaperStatusGR.SelectedValue.ToString());
                grAllDATA = gd.getPC_DataGRHeader("0", status);
                dataGridViewGRHeader.DataSource = grAllDATA;
            }
            catch (Exception ex)
            {


            }
        }


        private void grDataSearch()
        {

            try
            {

                int srSupID = Int32.Parse(comboBoxGRSupplier_SRC.SelectedValue.ToString());
                string srGRCode = textBoxGRCode_Src.Text;
                string srPOCode = textBoxPORef_Src.Text;


                if (srSupID == 0)
                {

                    this.grAllDATA.DefaultView.RowFilter = string.Format("GRCode like '*{0}*' and PORefference like '*{1}*' ", srGRCode, srPOCode);

                }
                else
                {

                    this.grAllDATA.DefaultView.RowFilter = string.Format("GRCode like '*{0}*' and PORefference like '*{1}*' and GRSupplierID = {2} ", srGRCode, srPOCode, srSupID);

                } 

            }
            catch (Exception ex)
            {

            }

        }

        private void textBoxGRCode_Src_TextChanged(object sender, EventArgs e)
        {
            grDataSearch();
        }

        private void textBoxPORef_Src_TextChanged(object sender, EventArgs e)
        {
            grDataSearch();
        }

        private void comboBoxGRSupplier_SRC_SelectedIndexChanged(object sender, EventArgs e)
        {
            grDataSearch();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                int grStatus = 0;

                if (radioBoxGRWaitApprove.Checked)
                    grStatus = 1; 
                else if (radioBoxGRApproved.Checked)
                    grStatus = 3; 
                else if (radioBoxGRCancle.Checked)
                    grStatus = 5;

                string remarkApproved = textBoxGRApprovedRemark.Text;


                int result = gd.updatePC_GRHeaderApproved(this.GRCode, remarkApproved, Login.userName, grStatus);


                if (result <= 0)
                {
                    MessageBox.Show("ไม่สามารถทำรายการได้ เนื่องจากอนุมัติไปแล้ว");
                }
                else
                {
                    MessageBox.Show("ทำรายการ สำเร็จ " + this.GRCode);
                     
                }

                //grAllDATA = gd.getPC_DataGRHeader(this.GRCode, 0);
                //dataGridViewGRHeader.DataSource = grAllDATA;

                grAllDATA = gd.getPC_DataGRHeader("0", 0);
                dataGridViewGRHeader.DataSource = grAllDATA;

             //   grDataSearch();



                viewDetailofGRByCode();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
               // data.Add(new KeyValuePair<int, string>(2, "ไม่อนุมัติ"));
                data.Add(new KeyValuePair<int, string>(3, "อนุมัติแล้ว"));
              //  data.Add(new KeyValuePair<int, string>(4, "ปิดใบ"));
                data.Add(new KeyValuePair<int, string>(5, "ยกเลิก"));

                comboBoxPaperStatusGR.DataSource = null;
                comboBoxPaperStatusGR.Items.Clear();

                // Bind the combobox
                comboBoxPaperStatusGR.DataSource = new BindingSource(data, null);
                comboBoxPaperStatusGR.DisplayMember = "Value";
                comboBoxPaperStatusGR.ValueMember = "Key";

                 
                 

            }
            catch (Exception ex)
            {

            }

        }

        private void buttonHeaderGR_Click(object sender, EventArgs e)
        {
            try
            {


                int addstocksign = 1;


                //Header
                this.GRCode = textBoxGRCode.Text;
                DateTime grdate = dateTimePickerGRDate.Value;
                string grPO_Ref = textBoxGR_PORef.Text ;
                string grRemark = textBoxGRRemark.Text;
                int grSupID = Int32.Parse(comboBoxPOSupplier_Ref.SelectedValue.ToString());
                int grInvID = Int32.Parse(comboBoxGRInvID.SelectedValue.ToString());



                if (MessageBox.Show("คุณต้องการจะแก้ไข Header GR  : " + this.GRCode + " หรือไม่ ?", "แก้ไข GR Header ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.updatePC_GRHeader(this.GRCode, grdate, grRemark, grPO_Ref, grInvID, grSupID);

                  //  viewDetailofGRByCode();
                  //  setDefault(); 

                    grAllDATA = gd.getPC_DataGRHeader("0", 0);
                    dataGridViewGRHeader.DataSource = grAllDATA;

               //     grDataSearch();

                   // grAllDATA = gd.getPC_DataGRHeader("0", 0);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }


        private void poDataSearch()
        {

            try
            {

                int srSupID = Int32.Parse(comboBoxPOSupplier_SRC.SelectedValue.ToString());
                string srPOCode = textBoxPOCode_Src.Text;
                string srPRCode = textBoxPRRef_Src.Text;


                if (srSupID == 0)
                {

                    this.poAllDATA.DefaultView.RowFilter = string.Format("POCode like '*{0}*' and PRReference like '*{1}*' ", srPOCode, srPRCode);

                }
                else
                {

                    this.poAllDATA.DefaultView.RowFilter = string.Format("POCode like '*{0}*' and PRReference like '*{1}*' and POSupplierID = {2} ", srPOCode, srPRCode, srSupID);

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

        private void buttonCloseSrcSupplierPN_Click(object sender, EventArgs e)
        {
            panelSearchSupplier.Visible = false;
        }

        private void textBoxSrcSupCode_TextChanged(object sender, EventArgs e)
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

                if(this.flagSupSearch == 1) 
                    comboBoxGRSupplier_SRC.SelectedValue = dataGridSupplierID;
                else
                    comboBoxPOSupplier_SRC.SelectedValue = dataGridSupplierID;

            }
            catch (Exception ex)
            {

            }
        }

         

        private void buttonSearchSupplier_Click(object sender, EventArgs e)
        {
            flagSupSearch = 1;
            panelSearchSupplier.Visible = true;

        }

        private void buttonSearchSupplier_PO_Click(object sender, EventArgs e)
        {

            flagSupSearch = 2;
            panelSearchSupplier.Visible = true;
        }


     
    }
}
