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
    public partial class AddMapSS : AddDataTemplate
    {

        GetDataRest gd;

        List<ProductStock> catStock;
        List<ProductStock> productStock;
        DataTable productStock_Data;

        List<Store> allStoresByCat;
        List<StoreCat> allStoreCat;

        int selectedStoreCat;
        List<MapSS> allMapSS;
        List<MapSS> allMapSSPRO;

        List<MapSS> allMapSS_SS;

        int selProductID = 0;
        string selProductName = "";
        string selStoreName = "";
        int selMapSSID = 0;

        List<Store> allStores;
        DataTable dataAllStore;
        DataTable dataAllStoreSrc;
        DataTable dataAllStoreSrc_Map;
        List<Store> allStoresByCat_1;


        int selStoreIDMap = 0;
        string selStoreNameMap = "";
        string selStoreName_1 = "";
        int selMapSSID_1 = 0;

        DataTable dataAllProduct;
        List<Product> allProductsSRC;

        DataTable dataAllMapSS_Final;


        public AddMapSS(Form frmlkFrom, int flagFrmClose)
        {
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();


            this.Width = 1024;
            this.Height = 768;

            gd = new GetDataRest();


            catStock = gd.getAllCatStock();
          //  productStock = gd.getAllCatStock();
         

            getComboCatStock();



            this.selectedStoreCat = 1;

            allStoreCat = gd.getListAllStoreCat();
            getComboAllStoreCat();

            allStores = gd.getListAllStore(0, 0, "000");

            dataAllStoreSrc = gd.getAllStore(0);
            dataGridViewAllStore.DataSource = dataAllStoreSrc;

            dataGridViewAllStore.Columns[0].Visible = false;
            dataGridViewAllStore.Columns[0].HeaderText = "InvID";
            dataGridViewAllStore.Columns[1].Visible = false;
            dataGridViewAllStore.Columns[2].Visible = false;
            dataGridViewAllStore.Columns[3].HeaderText = "InvName";
            dataGridViewAllStore.Columns[4].HeaderText = "Unit";
            dataGridViewAllStore.Columns[5].HeaderText = "BigUnit";
            dataGridViewAllStore.Columns[6].HeaderText = "ConvertUnit";
            dataGridViewAllStore.Columns[6].Visible = false;
            dataGridViewAllStore.Columns[7].Visible = false;
            dataGridViewAllStore.Columns[8].Visible = false;
            dataGridViewAllStore.Columns[9].Visible = false;
            dataGridViewAllStore.Columns[10].Visible = false;
            dataGridViewAllStore.Columns[11].Visible = false;
            dataGridViewAllStore.Columns[12].Visible = false;

            ///

            dataAllStoreSrc_Map = dataAllStoreSrc;
            dataGridViewAllStore_Map.DataSource = dataAllStoreSrc_Map;

            dataGridViewAllStore_Map.Columns[0].HeaderText = "InvID";
            dataGridViewAllStore_Map.Columns[1].Visible = false;
            dataGridViewAllStore_Map.Columns[2].HeaderText = "InvName";
            dataGridViewAllStore_Map.Columns[3].HeaderText = "Unit";
            dataGridViewAllStore_Map.Columns[4].HeaderText = "BigUnit";
            dataGridViewAllStore_Map.Columns[5].HeaderText = "ConvertUnit";
            dataGridViewAllStore_Map.Columns[6].Visible = false;
            dataGridViewAllStore_Map.Columns[7].Visible = false;
            dataGridViewAllStore_Map.Columns[8].Visible = false;
            dataGridViewAllStore_Map.Columns[9].Visible = false;
            dataGridViewAllStore_Map.Columns[10].Visible = false;
            dataGridViewAllStore_Map.Columns[11].Visible = false;
            dataGridViewAllStore_Map.Columns[12].Visible = false;


            groupBoxAddMapSS_PS.Visible = false;
            groupBoxAddMapSS_PP.Visible = false;
            groupBoxEditMapSS_PS.Visible = false;
            groupBoxEditMapSS_PP.Visible = false;


            // Map 2


            this.selectedStoreCat = 0;
            dataAllStore = gd.getAllStore(this.selectedStoreCat);
            dataGridViewMapStore.DataSource = dataAllStore;
            allStoresByCat_1 = gd.getListAllStore(this.selectedStoreCat, 0, "000");



            dataGridViewMapStore.Columns[1].Visible = false;
            dataGridViewMapStore.Columns[2].Visible = false;
            // dataGridViewMapStore.Columns[4].Visible = false;
            dataGridViewMapStore.Columns[5].Visible = false;
            dataGridViewMapStore.Columns[6].Visible = false;
            dataGridViewMapStore.Columns[7].Visible = false;
            dataGridViewMapStore.Columns[8].Visible = false;
            dataGridViewMapStore.Columns[9].Visible = false;
            dataGridViewMapStore.Columns[10].Visible = false;
            dataGridViewMapStore.Columns[11].Visible = false;
            dataGridViewMapStore.Columns[12].Visible = false;
            dataGridViewMapStore.Columns[13].Visible = false;
            dataGridViewMapStore.Columns[14].Visible = false;



            getComboAllStoreCat_1();
            groupBoxAddMapSS_1.Visible = false;
            groupBoxEditMapSS_SS.Visible = false;


            allProductsSRC = gd.getProductByCat(0, 0, 0);
            getComboAllProduct();
            dataAllProduct = gd.getAllProduct(0);
            dataGridViewSrcProduct.DataSource = dataAllProduct;


            dataGridViewSrcProduct.Columns[1].Visible = false;
            dataGridViewSrcProduct.Columns[3].Visible = false;
            dataGridViewSrcProduct.Columns[4].Visible = false;
            dataGridViewSrcProduct.Columns[5].Visible = false;
            dataGridViewSrcProduct.Columns[6].Visible = false;
            // dataGridViewSrcProduct.Columns[7].Visible = false;
            dataGridViewSrcProduct.Columns[8].Visible = false;
            dataGridViewSrcProduct.Columns[9].Visible = false;
            dataGridViewSrcProduct.Columns[10].Visible = false;
            dataGridViewSrcProduct.Columns[11].Visible = false;
            dataGridViewSrcProduct.Columns[12].Visible = false;
            dataGridViewSrcProduct.Columns[13].Visible = false;
            dataGridViewSrcProduct.Columns[14].Visible = false;
            dataGridViewSrcProduct.Columns[15].Visible = false;

            dataAllMapSS_Final = gd.getAllMemMapSS_Final(0, 0);
            dataGridViewMapSS_Final.DataSource = dataAllMapSS_Final;

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
                comboBoxAllStoreCat.DataSource = null;
                comboBoxAllStoreCat.Items.Clear();

                // Bind the combobox
                comboBoxAllStoreCat.DataSource = new BindingSource(data, null);
                comboBoxAllStoreCat.DisplayMember = "Value";
                comboBoxAllStoreCat.ValueMember = "Key";

                comboBoxAllStoreCat.SelectedValue = selectedStoreCat;
                getComboAllStore();
                 


            }
            catch (Exception ex)
            {

            }

        }



        private void getComboAllStoreCat_1()
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
                comboBoxAllStoreCat_SS.DataSource = null;
                comboBoxAllStoreCat_SS.Items.Clear();

                // Bind the combobox
                comboBoxAllStoreCat_SS.DataSource = new BindingSource(data, null);
                comboBoxAllStoreCat_SS.DisplayMember = "Value";
                comboBoxAllStoreCat_SS.ValueMember = "Key";

                comboBoxAllStoreCat_SS.SelectedValue = selectedStoreCat;
                getComboAllStore_1();

                //  Map 2
                // Clear the combobox
                comboBoxStoreCAT_SS.DataSource = null;
                comboBoxStoreCAT_SS.Items.Clear();

                // Bind the combobox
                comboBoxStoreCAT_SS.DataSource = new BindingSource(data, null);
                comboBoxStoreCAT_SS.DisplayMember = "Value";
                comboBoxStoreCAT_SS.ValueMember = "Key";
                 

            }
            catch (Exception ex)
            {

            }

        }
         

        private void ComboBoxAllStoreCat_Change(object sender, EventArgs e)
        {
            try
            {
                selectedStoreCat = Int32.Parse(comboBoxAllStoreCat.SelectedValue.ToString());
                allStoresByCat = gd.getListAllStore(selectedStoreCat, 0, "000");
                // dataGridViewAllStore.DataSource = allStoresByCat;
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


        private void getComboAllStore_1()
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
                comboBoxAllStore_SS.DataSource = null;
                comboBoxAllStore_SS.Items.Clear();

                // Bind the combobox
                comboBoxAllStore_SS.DataSource = new BindingSource(data, null);
                comboBoxAllStore_SS.DisplayMember = "Value";
                comboBoxAllStore_SS.ValueMember = "Key";

            }
            catch (Exception ex)
            {

            } 
        }
         
        private void comboBoxAllStoreCat_1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                selectedStoreCat = Int32.Parse(comboBoxAllStoreCat_SS.SelectedValue.ToString());
                allStoresByCat = gd.getListAllStore(selectedStoreCat, 0, "000");
                // dataGridViewAllStore.DataSource = allStoresByCat;
                getComboAllStore_1();
            }
            catch (Exception ex)
            {

            }
        }

        float convertRate_PS = 0;

        private void CommboxAllStore_Change(object sender, EventArgs e)
        {
            try
            {

                int storeID = Int32.Parse(comboBoxAllStore.SelectedValue.ToString());

                foreach (Store c in allStoresByCat)
                {
                    if (c.StoreID == storeID)
                    {
                        labelStoreUnit.Text = c.StoreUnit;
                        labelStoreUnit_B.Text = c.StoreConvertUnit;
                        this.convertRate_PS = c.StoreConvertRate;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void getComboCatStock()
        {
            try
            {

                List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

                // Add data to the List

                data.Add(new KeyValuePair<int, string>(0, "== กลุ่มสินค้า =="));

                foreach (ProductStock c in catStock)
                {
                    data.Add(new KeyValuePair<int, string>(c.CatID, c.CatName));
                }


                // Clear the combobox
                comboBoxCat.DataSource = null;
                comboBoxCat.Items.Clear();

                // Bind the combobox
                comboBoxCat.DataSource = new BindingSource(data, null);
                comboBoxCat.DisplayMember = "Value";
                comboBoxCat.ValueMember = "Key";

               // comboBoxCat.SelectedIndex = 0;

                productStock_Data = gd.getAllProductStock_Data(0);
                dataGridViewProduct.DataSource = productStock_Data;

                dataGridViewProduct.Columns[1].Visible = false;
                dataGridViewProduct.Columns[2].Visible = false;
                dataGridViewProduct.Columns[3].Visible = false;
                dataGridViewProduct.Columns[5].Visible = false;

            }
            catch (Exception ex)
            {

            }

        }

        private void CommoStockCat_Change(object sender, EventArgs e)
        {
            try
            {

                int selectcatID = Int32.Parse(comboBoxCat.SelectedValue.ToString());

                productStock = gd.getAllProductStock(selectcatID);
                productStock_Data = gd.getAllProductStock_Data(selectcatID);
                dataGridViewProduct.DataSource = productStock_Data;

                dataGridViewProduct.Columns[1].Visible = false;
                dataGridViewProduct.Columns[2].Visible = false;
                dataGridViewProduct.Columns[3].Visible = false;
                dataGridViewProduct.Columns[5].Visible = false;

                groupBoxAddMapSS_PS.Visible = false;
                //  groupBoxShowMapSS.Visible = false;
                groupBoxAddMapSS_PP.Visible = false;


            }
            catch (Exception ex)
            {

            }
        }

        private void EditProduct_Click(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                selProductID = Int32.Parse(dataGridViewProduct["ProductID", e.RowIndex].Value.ToString());
                selProductName = dataGridViewProduct["ProductName", e.RowIndex].Value.ToString();

                labelSelectMenu.Text = selProductName;

                if (e.ColumnIndex == dataGridViewProduct.Columns["Edit"].Index && e.RowIndex >= 0)
                {
                    updateDatagridMapSS();
                    updateDatagridMapSS_Product();
                }

                groupBoxAddMapSS_PS.Visible = true;
                groupBoxAddMapSS_PP.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateDatagridMapSS()
        {
            allMapSS = gd.getAllMapSS(selProductID, 0, "PS");
            dataGridViewMapSS_Store.DataSource = allMapSS;

            dataGridViewMapSS_Store.Columns[1].Visible = false;
            dataGridViewMapSS_Store.Columns[2].Visible = false;
            dataGridViewMapSS_Store.Columns[3].Visible = false;
            dataGridViewMapSS_Store.Columns[4].Visible = false;
            dataGridViewMapSS_Store.Columns[5].Visible = false;
            dataGridViewMapSS_Store.Columns[6].Visible = false;
            dataGridViewMapSS_Store.Columns[7].Visible = false;
            dataGridViewMapSS_Store.Columns[8].Visible = false;
            dataGridViewMapSS_Store.Columns[9].Visible = false;
            dataGridViewMapSS_Store.Columns[10].Visible = false;
            dataGridViewMapSS_Store.Columns[14].Visible = false;



        }

        private void updateDatagridMapSS_Product()
        {
            allMapSSPRO = gd.getAllMapSS(selProductID, 0, "PP");
            dataGridViewMapSS_StorePRO.DataSource = allMapSSPRO;

            dataGridViewMapSS_StorePRO.Columns[1].Visible = false;
            dataGridViewMapSS_StorePRO.Columns[2].Visible = false;
            dataGridViewMapSS_StorePRO.Columns[3].Visible = false;
            dataGridViewMapSS_StorePRO.Columns[4].Visible = false;
            dataGridViewMapSS_StorePRO.Columns[5].Visible = false;
            dataGridViewMapSS_StorePRO.Columns[6].Visible = false;
            dataGridViewMapSS_StorePRO.Columns[7].Visible = false;
            dataGridViewMapSS_StorePRO.Columns[8].Visible = false;
            // dataGridViewMapSS_StorePRO.Columns[9].Visible = false;
            dataGridViewMapSS_StorePRO.Columns[10].Visible = false;
            dataGridViewMapSS_StorePRO.Columns[11].Visible = false;
            //  dataGridViewMapSS_StorePRO.Columns[12].Visible = false;
            dataGridViewMapSS_StorePRO.Columns[13].Visible = false;
            //  dataGridViewMapSS_Store.Columns[15].Visible = false;
        }


        private void buttonAddMapSS_Click(object sender, EventArgs e)
        {
            try
            {

                int storeID = Int32.Parse(comboBoxAllStore.SelectedValue.ToString());
                string storeName = comboBoxAllStore.Text;
                float storeQTY = float.Parse(textBoxAddQTY_PS.Text);

                foreach (MapSS c in allMapSS)
                {
                    if (c.MapstoreID == storeID)
                        throw new Exception("เลือกวัตถุดิบที่ Map อยู่แล้ว : กรุณาเลือกใหม่ / กดแก้ไข");
                }

                if (storeID == 0)
                    throw new Exception("กรุณาเลือกวัตุดิบ");

                if (MessageBox.Show("คุณต้องการจะเพิ่มการ Map : " + selProductName + "กับ : " + storeName + " หรือไม่ ?", "เพิ่ม " + storeName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.instNewMapSS(selProductID, 0, 0, storeID, storeQTY, "PS", selProductName + " 1 : " + storeName + " " + textBoxAddQTY_PS.Text);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error Insert Product - Store : Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("เพิ่ม : " + storeName + " >> (Success)");
                        updateDatagridMapSS();
                        textBoxAddQTY_PS.Text = "";
                        textBoxAddQTY_PS_B.Text = "";
                        comboBoxAllStore.SelectedIndex = 0;

                    }
                }

          


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        float convertRate_PS_Edit = 0;

        private void EditMapSS_Click(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                selMapSSID = Int32.Parse(dataGridViewMapSS_Store["MapSSID", e.RowIndex].Value.ToString());
                selStoreName = dataGridViewMapSS_Store["MapStoreName", e.RowIndex].Value.ToString();
                string selStoreUnit = dataGridViewMapSS_Store["StoreUnit", e.RowIndex].Value.ToString();
                float selStoreQTY = float.Parse(dataGridViewMapSS_Store["StoreQTY", e.RowIndex].Value.ToString());

                int selectStoreID = Int32.Parse(dataGridViewMapSS_Store["MapStoreID", e.RowIndex].Value.ToString());


                textBoxStoreName.Text = selStoreName;
                textBoxEditQTY_PS.Text = selStoreQTY.ToString();

                if (e.ColumnIndex == dataGridViewProduct.Columns["Edit"].Index && e.RowIndex >= 0)
                {
                    //  updateDatagridMapSS();
                }


                groupBoxEditMapSS_PS.Visible = true;



                foreach (Store c in allStoresByCat)
                {
                    if (c.StoreID == selectStoreID)
                    {
                        labelMapSSUnit.Text = c.StoreUnit;
                        labelMapSSUnit_B.Text = c.StoreConvertUnit;
                        this.convertRate_PS_Edit = float.Parse(c.StoreConvertRate.ToString());
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonEditMapSS_Click(object sender, EventArgs e)
        {
            try
            {

                float storeQTY = (float)Double.Parse(textBoxEditQTY_PS.Text);


                if (MessageBox.Show("คุณต้องการจะแก้ไข Map : " + selProductName + "กับ : " + selStoreName + " หรือไม่ ?", "เแก้ไข " + selStoreName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.updsMapSS(selMapSSID, storeQTY, selProductName + " 1 : " + selStoreName + " " + textBoxEditQTY_PS.Text);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error Update การ Map วัตถุดิบ : Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("แก้ไข : " + selStoreName + " >> (Success)");
                        updateDatagridMapSS();
                        groupBoxEditMapSS_PS.Visible = false;

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Map 2

        private void textBoxSrcStoreName_TextChanged(object sender, EventArgs e)
        {

            try
            {

                string srPName = textBoxSrcStoreName.Text;

                int srStoreCatID = Int32.Parse(comboBoxStoreCAT_SS.SelectedValue.ToString());



                if ((srPName.Length > 0) && (srStoreCatID > 0))
                {
                    this.dataAllStore.DefaultView.RowFilter = string.Format("StoreName like '*{0}*' and StoreCatID = '{0}'", srPName, srStoreCatID);
                }
                else if ((srPName.Length == 0) && (srStoreCatID > 0))
                {
                    this.dataAllStore.DefaultView.RowFilter = string.Format("StoreCatID = '{0}'", srStoreCatID);
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
            catch (Exception ex)
            {

            }

        }

        float convertRate_SS_Map = 0;

        private void dataGridViewMapStore_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                selStoreIDMap = Int32.Parse(dataGridViewMapStore["StoreID", e.RowIndex].Value.ToString());
                selStoreNameMap = dataGridViewMapStore["StoreName", e.RowIndex].Value.ToString();

                labelSelectStore.Text = selStoreNameMap;


                foreach (Store c in allStores)
                {
                    if (c.StoreID == selStoreIDMap)
                    {
                        labelSS_SUnit.Text = c.StoreUnit;
                        labelSS_BUnit.Text = c.StoreConvertUnit;
                        this.convertRate_SS_Map = float.Parse(c.StoreConvertRate.ToString());
                    }
                }



                if (e.ColumnIndex == dataGridViewProduct.Columns["Edit"].Index && e.RowIndex >= 0)
                {
                    updateDatagridMapSS_SS();
                }

                groupBoxAddMapSS_1.Visible = true;
                groupBoxRatio_SS.Visible = true;







            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateDatagridMapSS_SS()
        {
            allMapSS_SS = gd.getAllMapSS(0, selStoreIDMap, "SS");
            dataGridViewMapSS_SS.DataSource = allMapSS_SS;

            dataGridViewMapSS_SS.Columns[1].Visible = false;
            dataGridViewMapSS_SS.Columns[2].Visible = false;
            dataGridViewMapSS_SS.Columns[3].Visible = false;
            dataGridViewMapSS_SS.Columns[4].Visible = false;
            dataGridViewMapSS_SS.Columns[5].Visible = false;
            dataGridViewMapSS_SS.Columns[6].Visible = false;
            dataGridViewMapSS_SS.Columns[7].Visible = false;
            dataGridViewMapSS_SS.Columns[8].Visible = false;
            dataGridViewMapSS_SS.Columns[9].Visible = false;
            dataGridViewMapSS_SS.Columns[10].Visible = false;
            dataGridViewMapSS_SS.Columns[14].Visible = false;
        }

        private void buttonOpenSearchProduct_Click(object sender, EventArgs e)
        {
            PanelProductSearch.Visible = true;
        }

        private void buttonClosePanelProductSearch_Click(object sender, EventArgs e)
        {
            PanelProductSearch.Visible = false;
        }


        private void getComboAllProduct()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(-1, "= เลือกสินค้า ="));

            foreach (Product c in allProductsSRC)
            {
                data.Add(new KeyValuePair<int, string>(c.ProductID, c.ProductName));
            }


            // Clear the combobox
            comboBoxAllProduct.DataSource = null;
            comboBoxAllProduct.Items.Clear();

            // Bind the combobox
            comboBoxAllProduct.DataSource = new BindingSource(data, null);
            comboBoxAllProduct.DisplayMember = "Value";
            comboBoxAllProduct.ValueMember = "Key";

        }


        private void textBoxSRProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string srPName = textBoxSRProductName.Text;


                if (srPName.Length > 0)
                {
                    this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductName like '*{0}*' ", srPName);

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridViewSrcProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridproductID = Int32.Parse(dataGridViewSrcProduct.Rows[e.RowIndex].Cells["ProductID"].Value.ToString());

                comboBoxAllProduct.SelectedValue = dataGridproductID;

                comboBoxAllProduct.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void buttonAddMapSS_PP_Click(object sender, EventArgs e)
        {
            try
            {

                int productID = Int32.Parse(comboBoxAllProduct.SelectedValue.ToString());
                string productName = comboBoxAllProduct.Text;
                float storeQTY = float.Parse(textBoxAddQTY_PP.Text);

                foreach (MapSS c in allMapSSPRO)
                {
                    if (c.MapproductID == productID)
                        throw new Exception("เลือกวสินค้าที่ Map อยู่แล้ว : กรุณาเลือกใหม่ / กดแก้ไข");
                }

                if (productID == 0)
                    throw new Exception("กรุณาเลือก MAP สินค้า");

                if (MessageBox.Show("คุณต้องการจะเพิ่มการ Map : " + selProductName + "กับ : " + productName + " หรือไม่ ?", "เพิ่ม " + productName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.instNewMapSS(selProductID, 0, productID, 0, storeQTY, "PP", selProductName + " 1 : " + productName + " " + textBoxAddQTY_PP.Text);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error Insert Product - Product : Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("เพิ่ม : " + productName + " >> (Success)");
                        updateDatagridMapSS_Product();
                        textBoxAddQTY_PP.Text = "";
                        comboBoxAllProduct.SelectedIndex = 0;

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonEditMapSS_PP_Click(object sender, EventArgs e)
        {
            try
            {

                float storeQTY = float.Parse(textBoxEditQTY_PP.Text);


                if (MessageBox.Show("คุณต้องการจะแก้ไข Map : " + selProductName + "กับ : " + selStoreName + " หรือไม่ ?", "เแก้ไข " + selStoreName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.updsMapSS(selMapSSID, storeQTY, selProductName + " 1 : " + selStoreName + " " + textBoxEditQTY_PP.Text);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error Update การ Map Product : Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("แก้ไข : " + selStoreName + " >> (Success)");
                        updateDatagridMapSS();
                        groupBoxEditMapSS_PP.Visible = false;

                    }
                }

              


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewMapSS_StorePRO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                selMapSSID = Int32.Parse(dataGridViewMapSS_StorePRO["MapSSID", e.RowIndex].Value.ToString());
                selStoreName = dataGridViewMapSS_StorePRO["MapProductName", e.RowIndex].Value.ToString();
                string selStoreUnit = dataGridViewMapSS_StorePRO["ProductUnit", e.RowIndex].Value.ToString();
                decimal selStoreQTY = Decimal.Parse(dataGridViewMapSS_StorePRO["StoreQTY", e.RowIndex].Value.ToString());

                labelMapSSUnit.Text = selStoreUnit;
                textBoxProductName.Text = selStoreName;
                textBoxEditQTY_PP.Text = selStoreQTY.ToString();

                //if (e.ColumnIndex == dataGridViewProduct.Columns["Edit"].Index && e.RowIndex >= 0)
                //{
                //    //  updateDatagridMapSS();
                //}

                groupBoxEditMapSS_PP.Visible = true;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAddMapSS_SS_Click(object sender, EventArgs e)
        {
            try
            {

                int storeID = Int32.Parse(comboBoxAllStore_SS.SelectedValue.ToString());
                string storeName = comboBoxAllStore_SS.Text;
                float storeQTY = float.Parse(textBoxAddQTY_SS.Text);


                foreach (MapSS c in allMapSS_SS)
                {
                    if (c.MapstoreID == storeID)
                        throw new Exception("เลือกวัตถุดิบที่ Map อยู่แล้ว : กรุณาเลือกใหม่ / กดแก้ไข");
                }

                if (storeID <= 0)
                    throw new Exception("กรุณาเลือกวัตุดิบ");

                if (MessageBox.Show("คุณต้องการจะเพิ่มการ Map : " + selStoreNameMap + "กับ : " + storeName + " หรือไม่ ?", "เพิ่ม " + storeName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.instNewMapSS(0, selStoreIDMap, 0, storeID, storeQTY, "SS", "|" + textBoxSS_Unit_S.Text + ":" + textBoxSS_Unit_S_Map.Text);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error Insert Product - Store : Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("เพิ่ม : " + storeName + " >> (Success)");
                        updateDatagridMapSS_SS();
                        textBoxAddQTY_SS.Text = "";
                        textBoxSS_Unit_B_Map.Text = "";
                        textBoxSS_Unit_S_Map.Text = "";
                        textBoxSS_Unit_B.Text = "";
                        textBoxSS_Unit_S.Text = "";
                        comboBoxAllStore_SS.SelectedIndex = 0;

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        float convertRate_SS_Edit = 0;

        private void dataGridViewMapSS_SS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                selMapSSID = Int32.Parse(dataGridViewMapSS_SS["MapSSID", e.RowIndex].Value.ToString());
                selStoreName = dataGridViewMapSS_SS["MapStoreName", e.RowIndex].Value.ToString();
                string selStoreUnit = dataGridViewMapSS_SS["StoreUnit", e.RowIndex].Value.ToString();
                decimal selStoreQTY = Decimal.Parse(dataGridViewMapSS_SS["StoreQTY", e.RowIndex].Value.ToString());

                textBoxStoreName_SS.Text = selStoreName;
                textBoxEditQTY_SS_S.Text = selStoreQTY.ToString();

                if (e.ColumnIndex == dataGridViewProduct.Columns["Edit"].Index && e.RowIndex >= 0)
                {
                    //  updateDatagridMapSS();
                }

                groupBoxEditMapSS_SS.Visible = true;


                int selectStoreID = Int32.Parse(dataGridViewMapSS_SS["MapStoreID", e.RowIndex].Value.ToString());

                foreach (Store c in allStores)
                {
                    if (c.StoreID == selectStoreID)
                    {
                        labelMapSSUnit_SS_S.Text = c.StoreUnit;
                        labelMapSSUnit_SS_B.Text = c.StoreConvertUnit;
                        this.convertRate_SS_Edit = float.Parse(c.StoreConvertRate.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonEditMapSS_SS_Click(object sender, EventArgs e)
        {
            try
            {

                float storeQTY = (float)Double.Parse(textBoxEditQTY_SS_S.Text);


                if (MessageBox.Show("คุณต้องการจะแก้ไข Map : " + selStoreNameMap + "กับ : " + selStoreName + " หรือไม่ ?", "เแก้ไข " + selStoreName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.updsMapSS(selMapSSID, storeQTY, "|" + textBoxSS_Unit_S.Text + ":" + textBoxSS_Unit_S_Map.Text);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error Update การ Map วัตถุดิบ : Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("แก้ไข : " + selStoreName + " >> (Success)");
                        updateDatagridMapSS_SS();
                        groupBoxEditMapSS_SS.Visible = false;

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonRecalStock_Click(object sender, EventArgs e)
        {
            dataAllMapSS_Final = gd.getAllMemMapSS_Final(0, 0);
            dataGridViewMapSS_Final.DataSource = dataAllMapSS_Final;
        }

        private void textBoxMappSSSRC_PRO_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string srPROName = textBoxMappSSSRC_PRO.Text;
                string srSTRName = textBoxMappSSSRC_STR.Text;


                if ((srPROName.Length > 0) && (srSTRName.Length > 0))
                {
                    this.dataAllMapSS_Final.DefaultView.RowFilter = string.Format("ProductName like '*{0}*' and StoreName like '*{1}*'", srPROName, srSTRName);
                }
                else if ((srPROName.Length == 0) && (srSTRName.Length > 0))
                {
                    this.dataAllMapSS_Final.DefaultView.RowFilter = string.Format("StoreName like '*{0}*'", srSTRName);
                }
                else if ((srPROName.Length > 0) && (srSTRName.Length == 0))
                {
                    this.dataAllMapSS_Final.DefaultView.RowFilter = string.Format("ProductName like '*{0}*'", srPROName);
                }
                else
                {
                    this.dataAllMapSS_Final.DefaultView.RowFilter = string.Format("1 = 1", 1);  // All
                }

            }
            catch (Exception ex)
            {

            }

        }

        float convertRate_SS = 0;

        private void comboBoxAllStore_SS_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int selectStoreID = Int32.Parse(comboBoxAllStore_SS.SelectedValue.ToString());

                foreach (Store c in allStores)
                {
                    if (c.StoreID == selectStoreID)
                    {
                        labelUnit_SS_S.Text = c.StoreUnit;


                        labelUnit_SS_S_1.Text = c.StoreUnit;
                        labelUnit_SS_B_1.Text = c.StoreConvertUnit;

                        this.convertRate_SS = float.Parse(c.StoreConvertRate.ToString());
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }



        private void textBoxSS_Unit_B_Map_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBoxSS_Unit_S_Map_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAddQTY_PS_B_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEditQTY_PS_TextChanged(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (textBoxEditQTY_PS.Text.Length > 0 && textBoxEditQTY_PS.Text != "0")
                {
                    textBoxEditQTY_PS_B.Text = (float.Parse(textBoxEditQTY_PS.Text) / this.convertRate_PS_Edit).ToString();
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void textBoxEditQTY_PS_B_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (textBoxEditQTY_PS_B.Text.Length > 0 && textBoxEditQTY_PS_B.Text != "0")
                {
                    textBoxEditQTY_PS.Text = (float.Parse(textBoxEditQTY_PS_B.Text) * this.convertRate_PS_Edit).ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }



        private void textBoxSS_Unit_B_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (textBoxSS_Unit_B.Text.Length > 0 && textBoxSS_Unit_B.Text != "0")
                {
                    textBoxSS_Unit_S.Text = (float.Parse(textBoxSS_Unit_B.Text) * this.convertRate_SS_Map).ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxSS_Unit_S_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (textBoxSS_Unit_S.Text.Length > 0 && textBoxSS_Unit_S.Text != "0")
                {
                    textBoxSS_Unit_B.Text = (float.Parse(textBoxSS_Unit_S.Text) / this.convertRate_SS_Map).ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxSS_Unit_B_Map_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{

            //    if ((textBoxSS_Unit_B_Map.Text.Length > 0 && textBoxSS_Unit_B_Map.Text != "0")
            //        && (textBoxSS_Unit_S.Text.Length > 0 && textBoxSS_Unit_S.Text != "0")
            //        && (textBoxSS_Unit_B.Text.Length > 0 && textBoxSS_Unit_B.Text != "0"))
            //    {
            //        textBoxSS_Unit_S_Map.Text = (float.Parse(textBoxSS_Unit_B.Text) * this.convertRate_SS).ToString();
            //        textBoxAddQTY_SS.Text = (float.Parse(textBoxSS_Unit_S_Map.Text) / float.Parse(textBoxSS_Unit_S.Text)).ToString();

            //    }

            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void textBoxSS_Unit_S_Map_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if ((textBoxSS_Unit_S_Map.Text.Length > 0 && textBoxSS_Unit_S_Map.Text != "0")
                    && (textBoxSS_Unit_S.Text.Length > 0 && textBoxSS_Unit_S.Text != "0")
                    && (textBoxSS_Unit_B.Text.Length > 0 && textBoxSS_Unit_B.Text != "0"))
                {
                    textBoxAddQTY_SS.Text = (float.Parse(textBoxSS_Unit_S_Map.Text) / float.Parse(textBoxSS_Unit_S.Text)).ToString();
                    textBoxSS_Unit_B_Map.Text = (float.Parse(textBoxSS_Unit_S.Text) / this.convertRate_SS).ToString();

                    textBoxEditQTY_SS_S.Text = (float.Parse(textBoxSS_Unit_S_Map.Text) / float.Parse(textBoxSS_Unit_S.Text)).ToString();

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonClosegroupBoxEditMapSS_PS_Click(object sender, EventArgs e)
        {
            groupBoxEditMapSS_PS.Visible = false;
        }

        private void buttonClosegroupBoxEditMapSS_PP_Click(object sender, EventArgs e)
        {
            groupBoxEditMapSS_PP.Visible = false;
        }

        private void buttonClosegroupBoxEditMapSS_SS_Click(object sender, EventArgs e)
        {
            buttonClosegroupBoxEditMapSS_SS.Visible = false;
        }

        private void textBoxEditQTY_SS_S_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (textBoxEditQTY_SS_S.Text.Length > 0 && textBoxEditQTY_SS_S.Text != "0")
                {
                    textBoxEditQTY_SS_B.Text = (float.Parse(textBoxEditQTY_SS_S.Text) / this.convertRate_SS_Edit).ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxEditQTY_SS_B_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (textBoxEditQTY_SS_B.Text.Length > 0 && textBoxEditQTY_SS_B.Text != "0")
                {
                    textBoxEditQTY_SS_S.Text = (float.Parse(textBoxEditQTY_SS_B.Text) * this.convertRate_SS_Edit).ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxAddQTY_PS_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (textBoxAddQTY_PS.Text.Length > 0 && textBoxAddQTY_PS.Text != "0")
                {
                    textBoxAddQTY_PS_B.Text = (float.Parse(textBoxAddQTY_PS.Text) / this.convertRate_PS).ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxAddQTY_PS_B_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (textBoxAddQTY_PS_B.Text.Length > 0 && textBoxAddQTY_PS_B.Text != "0")
                {
                    textBoxAddQTY_PS.Text = (float.Parse(textBoxAddQTY_PS_B.Text) * this.convertRate_PS).ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void buttonOpenPanelStore_Click(object sender, EventArgs e)
        {
            panelSearchStore.Visible = true;
        }

        private void buttonCloseSrcStorePN_Click(object sender, EventArgs e)
        {
            panelSearchStore.Visible = false;
        }

        private void dataGridViewAllStore_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {

                int dataGridStoreCatID = Int32.Parse(dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreCatID"].Value.ToString());
                int dataGridStoreID = Int32.Parse(dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreID"].Value.ToString());

                comboBoxAllStoreCat.SelectedValue = dataGridStoreCatID;
                comboBoxAllStore.SelectedValue = dataGridStoreID;

            }
            catch (Exception ex)
            {

            }

        }

        private void buttonClosepanelStoreSRC_Click(object sender, EventArgs e)
        {
            panelStoreSRC.Visible = false;
        }

        private void buttonOpenPanelSrcStoreMap_Click(object sender, EventArgs e)
        {
            panelStoreSRC.Visible = true;
        }

        private void dataGridViewAllStore_Map_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            try
            {

                int dataGridStoreCatID = Int32.Parse(dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreCatID"].Value.ToString());
                int dataGridStoreID = Int32.Parse(dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreID"].Value.ToString());

                comboBoxAllStoreCat_SS.SelectedValue = dataGridStoreCatID;
                comboBoxAllStore_SS.SelectedValue = dataGridStoreID;

            }
            catch (Exception ex)
            {

            }


        }

        private void textBoxStoreSRC_Map_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string srPName = textBoxStoreSRC_Map.Text;


                if (srPName.Length > 0)
                {
                    this.dataAllStoreSrc_Map.DefaultView.RowFilter = string.Format("StoreName like '*{0}*' ", srPName);

                }
                else
                {
                    this.dataAllStoreSrc_Map.DefaultView.RowFilter = string.Format(" 1 = 1 ", srPName);
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxStoreSRC_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string srPName = textBoxStoreSRC.Text;


                if (srPName.Length > 0)
                {
                    this.dataAllStoreSrc.DefaultView.RowFilter = string.Format("StoreName like '*{0}*' ", srPName);

                }
                else
                {
                    this.dataAllStoreSrc.DefaultView.RowFilter = string.Format(" 1 = 1 ", srPName);
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonDelMapSS_PS_Click(object sender, EventArgs e)
        {
            try
            {

                float storeQTY = 0;//(float)Double.Parse(textBoxEditQTY_PS.Text);


                if (MessageBox.Show("คุณต้องการจะลบ Map : " + selProductName + "กับ : " + selStoreName + " หรือไม่ ?", "เแก้ไข " + selStoreName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.updsMapSS(selMapSSID, storeQTY, selProductName + " 1 : " + selStoreName + " " + textBoxEditQTY_PS.Text);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error ลบ การ Map วัตถุดิบ : Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("ลบ : " + selStoreName + " >> (Success)");
                        updateDatagridMapSS();
                        groupBoxEditMapSS_PS.Visible = false;

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonDelMapSS_PP_Click(object sender, EventArgs e)
        {
            try
            {

                float storeQTY = 0; // float.Parse(textBoxEditQTY_PP.Text);


                if (MessageBox.Show("คุณต้องการจะลบ Map : " + selProductName + "กับ : " + selStoreName + " หรือไม่ ?", "เแก้ไข " + selStoreName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.updsMapSS(selMapSSID, storeQTY, selProductName + " 1 : " + selStoreName + " " + textBoxEditQTY_PP.Text);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error ลบ  การ Map Product : Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("ลบ  : " + selStoreName + " >> (Success)");
                        updateDatagridMapSS();
                        groupBoxEditMapSS_PP.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void buttonDelMapSS_SS_Click(object sender, EventArgs e)
        {
            try
            {

                float storeQTY = 0; // (float)Double.Parse(textBoxEditQTY_SS_S.Text);


                if (MessageBox.Show("คุณต้องการจะลบ Map : " + selStoreNameMap + "กับ : " + selStoreName + " หรือไม่ ?", "เแก้ไข " + selStoreName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.updsMapSS(selMapSSID, storeQTY, "|" + textBoxSS_Unit_S.Text + ":" + textBoxSS_Unit_S_Map.Text);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error ลบ การ Map วัตถุดิบ : Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("ลบ : " + selStoreName + " >> (Success)");
                        updateDatagridMapSS_SS();
                        groupBoxEditMapSS_SS.Visible = false;

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxSRProductName_Map_TextChanged(object sender, EventArgs e)
        {
            filterProduct();
        }

        private void comboBoxProductMap_SelectedValueChanged(object sender, EventArgs e)
        {
            filterProduct();
        }

        private void filterProduct()
        {
            try
            {
                string srPName = textBoxSRProductName_Map.Text;
                string stockMap = comboBoxProductMap.Text;

                if (stockMap == "ALL")
                {
                    if (srPName.Length > 0)
                        this.productStock_Data.DefaultView.RowFilter = string.Format("ProductName like '*{0}*' ", srPName);
                    else
                        this.productStock_Data.DefaultView.RowFilter = string.Format(" 1 = 1 ", srPName);
                }
                else
                {

                    if (srPName.Length > 0)
                        this.productStock_Data.DefaultView.RowFilter = string.Format("ProductName like '*{0}*' and  StockMap = '{1}' ", srPName, stockMap);
                    else
                        this.productStock_Data.DefaultView.RowFilter = string.Format("  StockMap = '{0}' ", stockMap);

                }

            }
            catch (Exception ex)
            {

            }
        }

      

    }
}
