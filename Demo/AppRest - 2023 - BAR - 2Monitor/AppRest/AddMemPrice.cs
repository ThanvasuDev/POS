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
    public partial class AddMemPrice : AddDataTemplate
    {

        GetDataRest gd;

        List<ProductStock> catStock;
        List<ProductStock> productStock;

        List<Store> allStoresByCat;
        List<StoreCat> allStoreCat; 

  
        List<MapSS> allMapSS;

      
        



        List<Member> allMembers;
        List<Cat> allCats;
        List<Product> allProductsByCats;
        int selectedCat = 0;
        int selectedProduct = 0;

        int selMemID = 0;
        string selMemName = "";

        DataTable allMemPriceTable;

        int selectedStoreCat;


        string selMapProductName = "";
        int selMapProductID = 0;

        

        public AddMemPrice(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();
            gd = new GetDataRest();


            allMembers = gd.getListAllMember();
            dataGridViewAllMember.DataSource = allMembers;


            allCats = gd.getOrderCat(2);
            getComboAllCat();
             

         //   this.selectedStoreCat = 2;

            dataGridViewAllMember.Columns[1].Visible = false;
            dataGridViewAllMember.Columns[2].Visible = false;
            dataGridViewAllMember.Columns[3].Visible = false;
            dataGridViewAllMember.Columns[5].Visible = false;
            dataGridViewAllMember.Columns[6].Visible = false;
            dataGridViewAllMember.Columns[7].Visible = false;
            dataGridViewAllMember.Columns[8].Visible = false;
            dataGridViewAllMember.Columns[9].Visible = false;
            dataGridViewAllMember.Columns[10].Visible = false; 

            groupBoxAddMapSS.Visible = false;
            groupBoxShowMapSS.Visible = false;
            groupBoxEditMapSS.Visible = false;

           
        }

        private void getComboAllCat()
        {
            try
            {
        
                List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

                // Add data to the List

                data.Add(new KeyValuePair<int, string>(0, "===== เลือกกลุ่มสินค้า ====="));

                foreach (Cat c in allCats)
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

                comboBoxCat.SelectedIndex = 2;
                getComboAllProduct();

            }
            catch (Exception ex)
            {

            }

        }


        private void getComboAllProduct()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();
             

            data.Add(new KeyValuePair<int, string>(-1, "======== เลือกสินค้า ======="));

            foreach (Product c in allProductsByCats)
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
         

        //private void CommoStockCat_Change(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        int selectcatID = Int32.Parse(comboBoxCat.SelectedValue.ToString());

        //        productStock = gd.getAllProductStock(selectcatID);

        //        dataGridViewAllMember.DataSource = productStock;

        //        dataGridViewAllMember.Columns[1].Visible = false;
        //        dataGridViewAllMember.Columns[2].Visible = false;
        //        dataGridViewAllMember.Columns[3].Visible = false;

        //        groupBoxAddMapSS.Visible = false;
        //        groupBoxShowMapSS.Visible = false;
        //        groupBoxEditMapSS.Visible = false;


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        private void ComboBoxCat_Change(object sender, EventArgs e)
        {
            try
            {
                selectedCat = Int32.Parse(comboBoxCat.SelectedValue.ToString());
                allProductsByCats = gd.getProductByCat(selectedCat, 0,0);
                getComboAllProduct();
            }
            catch (Exception ex)
            {

            }
        }

        private void EditProduct_Click(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                selMemID = Int32.Parse(dataGridViewAllMember["UserID", e.RowIndex].Value.ToString());
                selMemName = dataGridViewAllMember["name", e.RowIndex].Value.ToString();

                labelSelectMember.Text = selMemName;

                if (e.ColumnIndex == dataGridViewAllMember.Columns["Edit"].Index && e.RowIndex >= 0)
                {
                    updateDatagridMemberPrice();
                }

                groupBoxAddMapSS.Visible = true;
                groupBoxShowMapSS.Visible = true;

            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }

        private void updateDatagridMemberPrice()
        {

            try
            {

            allMemPriceTable = gd.getAllMemPrice(selMemID, 0);
            dataGridViewMemPrice.DataSource = allMemPriceTable;

            dataGridViewMemPrice.Columns[1].Visible = false;
            dataGridViewMemPrice.Columns[2].Visible = false;
            dataGridViewMemPrice.Columns[3].Visible = false; 
            dataGridViewMemPrice.Columns[4].HeaderText = "ID";
            dataGridViewMemPrice.Columns[6].HeaderText = "Price VIP";
            //dataGridViewMemPrice.Columns[4].Visible = false;
            //dataGridViewMemPrice.Columns[5].Visible = false;
            //dataGridViewMemPrice.Columns[6].Visible = false;

                       }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }


        private void buttonAddMapSS_Click(object sender, EventArgs e)
        {
            try
            {

                int productID = Int32.Parse(comboBoxAllProduct.SelectedValue.ToString());
                string productName = comboBoxAllProduct.Text;
                float eProductPrice = float.Parse(textBoxAddQTY.Text);

                //foreach (AddMemPrice c in allMapSS)
                //{
                //    if (c.StoreID == storeID)
                //        throw new Exception("เลือกวัตถุดิบที่ Map อยู่แล้ว : กรุณาเลือกใหม่ / กดแก้ไข");
                //}

                if (productID == 0)
                    throw new Exception("กรุณาเลือกสินค้า");

                if (MessageBox.Show("คุณต้องการจะเพิ่มราคา VIP ของ " + selMemName + "กับ : " + productName + " หรือไม่ ?", "เแก้ไข " + selMemName + "กับ : " + selMapProductName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.updsMemPrice(selMemID, productID, eProductPrice, 1);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error Insert สร้างราคา VIP: Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("เพิ่มราคา VIP ของ " + selMemName + " กับ : " + productName + " >> (Success)");
                        updateDatagridMemberPrice();
                        groupBoxEditMapSS.Visible = false; 
                        textBoxAddQTY.Text = "";
                        comboBoxAllProduct.SelectedIndex = 0;

                    }
                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void EditMapSS_Click(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                selMapProductID = Int32.Parse(dataGridViewMemPrice["ProductID", e.RowIndex].Value.ToString());
                selMapProductName = dataGridViewMemPrice["ProductName", e.RowIndex].Value.ToString();
             //   string selStoreUnit= dataGridViewMemPrice["StoreUnit", e.RowIndex].Value.ToString();
                float selStoreQTY= float.Parse(dataGridViewMemPrice["ProductPrice", e.RowIndex].Value.ToString());


                textBoxStoreName.Text = selMapProductName;
                textBoxEditQTY.Text = selStoreQTY.ToString();

                if (e.ColumnIndex == dataGridViewAllMember.Columns["Edit"].Index && e.RowIndex >= 0)
                {
                  //  updateDatagridMapSS();
                }

                groupBoxEditMapSS.Visible = true; 

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
                 
                float eProductPrice = float.Parse(textBoxEditQTY.Text);


                if (MessageBox.Show("คุณต้องการจะแก้ไขราคา VIP ของ " + selMemName + "กับ : " + selMapProductName + " หรือไม่ ?", "เแก้ไข " + selMemName + "กับ : " + selMapProductName , MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.updsMemPrice(selMemID, selMapProductID, eProductPrice, 1);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error Update ราคา VIP : Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("แก้ไข ราคา VIP ของ " + selMemName + "กับ : " + selMapProductName + " >> (Success)");
                        updateDatagridMemberPrice();
                        groupBoxEditMapSS.Visible = false; 

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxAllProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                selectedProduct = Int32.Parse(comboBoxAllProduct.SelectedValue.ToString());


                foreach (Product p in allProductsByCats)
                {
                    if (p.ProductID == selectedProduct)
                    {
                        textBoxP1.Text = p.ProductPrice.ToString();
                        textBoxP2.Text = p.ProductPrice2.ToString();
                        textBoxP3.Text = p.ProductPrice3.ToString();
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void buttonDelMapMmPrice_Click(object sender, EventArgs e)
        {
            try
            {

                float eProductPrice = float.Parse(textBoxEditQTY.Text);


                if (MessageBox.Show("คุณต้องการจะลบราคา VIP ของ " + selMemName + "กับ : " + selMapProductName + " หรือไม่ ?", "เแก้ไข " + selMemName + "กับ : " + selMapProductName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.updsMemPrice(selMemID, selMapProductID, eProductPrice, 0);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error ลบ ราคา VIP : Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("ลบ ราคา VIP ของ " + selMemName + "กับ : " + selMapProductName + " >> (Success)");
                        updateDatagridMemberPrice();
                        groupBoxEditMapSS.Visible = false;

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}
