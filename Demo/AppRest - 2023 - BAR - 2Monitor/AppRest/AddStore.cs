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
    public partial class AddStore : AddDataTemplate
    {
        GetDataRest gd;
        List<Store> allStoresByCat;
        List<StoreCat> allStoreCat;

        DataTable dataAllStore;

        MainManage formMainManage;

        int selectedStoreCat;


        public AddStore(Form frmlkFrom, int flagFrmClose)
        {
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();

            this.Width = 1024;
            this.Height = 764;
            
            gd = new GetDataRest();

            comboBoxAllStore.Visible = false;
            this.selectedStoreCat = 0;


            dataAllStore = gd.getAllStore(this.selectedStoreCat);
            dataGridViewAllStore.DataSource = dataAllStore;
            allStoresByCat = gd.getListAllStore(this.selectedStoreCat, 0, "000");

            allStoreCat = gd.getListAllStoreCat();
            getComboAllStoreCat();
               

        }

        private void getComboAllStoreCat()
        {
            try
            {
                List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

                // Add data to the List
                data.Add(new KeyValuePair<int, string>(0, "==== Store Category ===="));

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

                comboBoxAllCat_Sel.DataSource = null;
                comboBoxAllCat_Sel.Items.Clear();

                // Bind the combobox
                comboBoxAllCat_Sel.DataSource = new BindingSource(data, null);
                comboBoxAllCat_Sel.DisplayMember = "Value";
                comboBoxAllCat_Sel.ValueMember = "Key";

                


                comboBoxAllCat.SelectedValue = this.selectedStoreCat;
                getComboAllStore();
            }
            catch (Exception ex)
            {

            }

        }

        private void ComboBoxAllStoreCat_Change(object sender, EventArgs e)
        {
            try
            {
                this.selectedStoreCat = Int32.Parse(comboBoxAllCat.SelectedValue.ToString());

                dataAllStore = gd.getAllStore(this.selectedStoreCat);
                dataGridViewAllStore.DataSource = dataAllStore;
                allStoresByCat = gd.getListAllStore(this.selectedStoreCat, 0, "000");
                //dataGridViewAllStore.DataSource = allStoresByCat;
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

                data.Add(new KeyValuePair<int, string>(-1, "= เลือกแก้ไขวัตถุดิบ ="));

                foreach (Store c in allStoresByCat)
                {
                    data.Add(new KeyValuePair<int, string>(c.StoreID, c.StoreName));
                }

                // Clear the combobox
                comboBoxAllStore.DataSource = null;
                comboBoxAllStore.Items.Clear();

                // Bind the combobox
                comboBoxAllStore.DataSource = new BindingSource(data, null);
                comboBoxAllStore.DisplayMember = "Value";
                comboBoxAllStore.ValueMember = "Key";

            }
            catch (Exception ex)
            {

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtBoxStoreName.Text = "";
            txtBoxStoreUnit.Text = "";
            txtBoxStoreCost.Text = ""; 
            txtBoxKPIOverStock.Text = "";
            txtBoxKPILowOut.Text = "";
            comboBoxStockTag.Text = "Stock Monthly";
            comboBoxFlagUse.Text = "Y";

            txtBoxStoreConvertUnit.Text = "";
            txtBoxStoreConvertRate.Text = "";
            txtBoxStoreAvgCost.Text = "0";
            txtBoxStoreBarcode.Text = "";
            txtBoxStoreSupCode.Text = "";
        }

        private void clearForm()
        {
            txtBoxStoreName.Text = "";
            txtBoxStoreUnit.Text = "";
            txtBoxStoreCost.Text = "";
            txtBoxKPIOverStock.Text = "";
            txtBoxKPILowOut.Text = "";
            comboBoxStockTag.Text = "Stock Monthly";
            comboBoxFlagUse.Text = "Y";
            radioButtonAddData.Checked = true;

            txtBoxStoreConvertUnit.Text = "";
            txtBoxStoreConvertRate.Text = "";
            txtBoxStoreAvgCost.Text = "0";
            txtBoxStoreBarcode.Text = "";
            txtBoxStoreSupCode.Text = "";
        }

        private void buttonAddStore_Click(object sender, EventArgs e)
        {

            try
            {
                string storeName = txtBoxStoreName.Text;
                string storeUnit = txtBoxStoreUnit.Text;
                float storeCost =   (float) Double.Parse( txtBoxStoreCost.Text );
                float storeKPILowStock = (float)Double.Parse(txtBoxKPILowOut.Text);
                float storeKPIOverStock = (float)Double.Parse(txtBoxKPIOverStock.Text);
                string storeOrder = comboBoxStockTag.Text;
                string storeflagUse = comboBoxFlagUse.Text;
                 
                string storeConvertUnit = txtBoxStoreConvertUnit.Text;

                float storeConvertRate = 0;
                if( FuncString.IsNumeric(txtBoxStoreConvertRate.Text))
                    storeConvertRate = (float)Double.Parse(txtBoxStoreConvertRate.Text);


                float storeAvgCost = 0;
                if (FuncString.IsNumeric(txtBoxStoreAvgCost.Text))
                    storeAvgCost = (float)Double.Parse(txtBoxStoreAvgCost.Text);

                if (txtBoxStoreConvertRate.Text == "0")
                    throw new Exception("อัตราส่วนหน่่วยเล็กใหญ่ ต้องไม่เท่ากับ 0");
                 
                string storeBarcode = txtBoxStoreBarcode.Text;
                string storeSubCode = txtBoxStoreSupCode.Text;



                if (radioButtonAddData.Checked == true)
                {

                    if (MessageBox.Show("คุณต้องการจะเพิ่มวัตถุดิบ : " + storeName + " หรือไม่ ?", "เพิ่ม " + storeName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.instNewStore(this.selectedStoreCat, storeName, storeUnit, storeConvertUnit, storeConvertRate, storeCost, storeAvgCost, storeKPILowStock, storeKPIOverStock, storeOrder, storeBarcode, storeSubCode, storeflagUse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Store : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่ม : " + storeName + " >> (Success)");


                            dataAllStore = gd.getAllStore(this.selectedStoreCat);
                            dataGridViewAllStore.DataSource = dataAllStore;
                            allStoresByCat = gd.getListAllStore(this.selectedStoreCat, 0, "000");

                            getComboAllStore();
                            clearForm();
                        }
                    }
                }
                else
                {
                    int storeID = Int32.Parse(comboBoxAllStore.SelectedValue.ToString());
                      int storeCATID = Int32.Parse(comboBoxAllCat_Sel.SelectedValue.ToString());
                     

                    if (MessageBox.Show("คุณต้องการจะแก้ไข : " + storeName + "   หรือไม่ ?", "แก้ไข " + storeName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updsStore(storeID, storeCATID, storeName, storeUnit, storeConvertUnit, storeConvertRate, storeCost, storeAvgCost, storeKPILowStock, storeKPIOverStock, storeOrder, storeBarcode, storeSubCode, storeflagUse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Store : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("แก้ไข : " + storeName + "  >> (Success)");


                            dataAllStore = gd.getAllStore(this.selectedStoreCat);
                            dataGridViewAllStore.DataSource = dataAllStore;
                            allStoresByCat = gd.getListAllStore(this.selectedStoreCat, 0, "000");
                            getComboAllStore();
                            clearForm();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //private void buttonBackToManage_Click(object sender, EventArgs e)
        //{
        //    //LinkFormMainManage();

        //}

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

        private void ChangeUpdateFlag(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked == true)
            {
                comboBoxAllStore.Visible = false;
                labelHeader.Text = "เพิ่มข้อมูลวัตถุดิบ";
                buttonAddTable.Text = "เพิ่มข้อมูล";
            }
            else
            {
                comboBoxAllStore.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูลวัตถุดิบ";
                buttonAddTable.Text = "แก้ไขข้อมูล";
            }
        }

        private void CommboxAllStore_Change(object sender, EventArgs e)
        {
            try
            {

                int storeID = Int32.Parse(comboBoxAllStore.SelectedValue.ToString());

                foreach (Store c in allStoresByCat)
                {
                    if (c.StoreID == storeID)
                    {
                        txtBoxStoreName.Text = c.StoreName;
                        txtBoxStoreUnit.Text = c.StoreUnit;
                        txtBoxStoreCost.Text = c.StoreCost.ToString(); 
                        txtBoxKPILowOut.Text = c.StoreKPILowStock.ToString();
                        txtBoxKPIOverStock.Text = c.StoreKPIOverStock.ToString();
                        comboBoxStockTag.Text = c.StoreOrder;
                        comboBoxFlagUse.Text = c.StoreFlagUse;

                        txtBoxStoreConvertUnit.Text = c.StoreConvertUnit;
                        txtBoxStoreConvertRate.Text = c.StoreConvertRate.ToString();
                        txtBoxStoreAvgCost.Text = c.StoreAvgCost.ToString();
                        txtBoxStoreBarcode.Text = c.StoreBarcode;
                        txtBoxStoreSupCode.Text = c.StoreSupCode;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonScanBarcodeforSearch_Click(object sender, EventArgs e)
        {
            txtBoxSearchBarcode.Focus();
        }

        private void buttonScanBarcode_Click(object sender, EventArgs e)
        {
            txtBoxStoreBarcode.Focus();
        }

        private void txtBoxStoreUnit_TextChanged(object sender, EventArgs e)
        {
            labelCosttext.Text = "ต้นทุน (" + txtBoxStoreUnit.Text +")";
            labelCosttext2.Text = txtBoxStoreUnit.Text ;
            labelCosttext3.Text = txtBoxStoreUnit.Text ;
            labelCosttext4.Text = txtBoxStoreUnit.Text ;

            labelConvert.Text = "1 " + txtBoxStoreConvertUnit.Text + " = ";
            labelCosttextBig.Text = "ต้นทุน (" + txtBoxStoreConvertUnit.Text + ")";
        }

        private void labelConvert_TextChanged(object sender, EventArgs e)
        {
            labelConvert.Text = "1 " + txtBoxStoreConvertUnit.Text + " มีค่าเท่ากับ ";
            labelCosttextBig.Text = "ต้นทุน (" + txtBoxStoreConvertUnit.Text + ")";
        }

        private void txtBoxStoreConvertUnit_TextChanged(object sender, EventArgs e)
        {
            labelConvert.Text = "1 " + txtBoxStoreConvertUnit.Text + " มีค่าเท่ากับ ";
            labelCosttextBig.Text = "ต้นทุน (" + txtBoxStoreConvertUnit.Text + ")";
        }

        private void txtBoxSearchBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {

                    string barcodestr = txtBoxSearchBarcode.Text;

                    foreach (Store c in allStoresByCat)
                    {
                        if (c.StoreBarcode == barcodestr)
                        {
                            comboBoxAllStore.SelectedValue = c.StoreID;

                            //
                            comboBoxAllStore.Visible = true;
                            labelHeader.Text = "แก้ไขข้อมูลวัตถุดิบ";
                            buttonAddTable.Text = "แก้ไขข้อมูล";
                            radioButtonUpdateData.Checked = true;
                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void dataGridViewAllStore_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridproductID = Int32.Parse(dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreID"].Value.ToString());
                int dataGridstorecatID = Int32.Parse(dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreCatID"].Value.ToString());


               
                comboBoxAllStore.SelectedValue = dataGridproductID;

                comboBoxAllStore.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูลคลังสินค้า";
                buttonAddTable.Text = "แก้ไขข้อมูล";

                comboBoxAllCat_Sel.SelectedValue = dataGridstorecatID;

                radioButtonUpdateData.Checked = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        private void txtBoxStoreAvgCost_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtBoxStoreCost.Text = (float.Parse(txtBoxStoreAvgCost.Text) / float.Parse(txtBoxStoreConvertRate.Text)).ToString(); 

            }
            catch
            {

            }
        }
 
         





    }
}
