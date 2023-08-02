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
using System.IO;

namespace AppRest
{
    public partial class AddProductBarcodeSS : AddDataTemplate
    {

        GetDataRest gd;
        MainManage formMainManage;

         
        List<Product> allProducts;

        DataTable dataAllProduct;

        int selectedCat;

        string printerBarcodeName;
        string flagPrintBarcode;
        string pathPrintBarcode;

        float finalprice = 0;

        int modSticker = 0;

        Image barcodeImg;


        List<Store> allStoresByCat;
        List<StoreCat> allStoreCat;

        DataTable dataAllStore;

        int selectedStoreCat;


        public AddProductBarcodeSS(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();
            gd = new GetDataRest();

            this.Width = 1024;
            this.Height = 760;

            comboBoxAllProduct.Visible = false;


            selectedCat = 0; 
            allCats = gd.getOrderCat(2);
            getComboAllCat();

            allProducts = gd.getProductByCat(0,0,0);
            dataAllProduct = gd.getAllProduct(0);
            dataGriadViewAllProduct.DataSource = dataAllProduct;

            getComboAllProduct();
             
               
            printerBarcodeName = ConfigurationSettings.AppSettings["PrinterNameBarCode"].ToString();
            flagPrintBarcode = ConfigurationSettings.AppSettings["FlagPrintBarcode"].ToString();
            pathPrintBarcode = ConfigurationSettings.AppSettings["pathPrintBarcode"].ToString();

            printBarcode1.PrinterSettings.PrinterName = printerBarcodeName;
            printBarcode2.PrinterSettings.PrinterName = printerBarcodeName;
            printBarcode3.PrinterSettings.PrinterName = printerBarcodeName;
            printBarcode4.PrinterSettings.PrinterName = printerBarcodeName;
            printBarcode5.PrinterSettings.PrinterName = printerBarcodeName;

            if (flagPrintBarcode == "Y")
                buttonBarcodePrinting.Enabled = true;

           // printBarCode.PrinterSettings.PaperSizes  // = new  PaperSize("PaperA4", 840, 1180);

              
            dataGriadViewAllProduct.Columns[1].Visible = false;
            dataGriadViewAllProduct.Columns[3].Visible = false;
            dataGriadViewAllProduct.Columns[4].Visible = false;
            dataGriadViewAllProduct.Columns[6].Visible = false;
            dataGriadViewAllProduct.Columns[7].HeaderText = "Retail";
            dataGriadViewAllProduct.Columns[8].HeaderText = "B2B";
            dataGriadViewAllProduct.Columns[10].HeaderText = "WH";
            dataGriadViewAllProduct.Columns[9].Visible = false;
            dataGriadViewAllProduct.Columns[13].Visible = false;
            dataGriadViewAllProduct.Columns[14].Visible = false;

            this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductID > 4 and ProductFlagUse = 'Y'");




            allStoresByCat = gd.getListAllStore(this.selectedStoreCat, 0, "000");
            allStoreCat = gd.getListAllStoreCat();
            getComboAllStoreCat();

            dataAllStore = gd.getAllStore(this.selectedStoreCat);
            dataGridViewAllStore.DataSource = dataAllStore;



            dataGridViewAllStore.Columns[0].Visible = false;
            dataGridViewAllStore.Columns[1].Visible = false;
            dataGridViewAllStore.Columns[2].Visible = false;
            dataGridViewAllStore.Columns[3].HeaderText = "InvName";
            dataGridViewAllStore.Columns[4].HeaderText = "Small(Unit)";
            dataGridViewAllStore.Columns[5].HeaderText = "Big(Unit)";
            dataGridViewAllStore.Columns[6].HeaderText = "ConvertUnit";
            dataGridViewAllStore.Columns[7].Visible = false;
            dataGridViewAllStore.Columns[8].Visible = false;


            dataGridViewAllStore.Columns[11].HeaderText = "InvCode";
            dataGridViewAllStore.Columns[12].Visible = false;
            dataGridViewAllStore.Columns[13].Visible = false; 
        
        
        
        
        }

        private void getComboAllStoreCat()
        {
            try
            {
                List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

                // Add data to the List
                data.Add(new KeyValuePair<int, string>(0, "==== Inv Category ===="));

                foreach (StoreCat c in allStoreCat)
                {
                    data.Add(new KeyValuePair<int, string>(c.StoreCatID, c.StoreCatName));
                } 

                // Clear the combobox
                comboBoxInvCat.DataSource = null;
                comboBoxInvCat.Items.Clear();

                // Bind the combobox
                comboBoxInvCat.DataSource = new BindingSource(data, null);
                comboBoxInvCat.DisplayMember = "Value";
                comboBoxInvCat.ValueMember = "Key";
 
            }
            catch (Exception ex)
            {

            }

        }


        private void ComboBoxAllStoreCat_Change(object sender, EventArgs e)
        {
            try
            {
                this.selectedStoreCat = Int32.Parse(comboBoxInvCat.SelectedValue.ToString());

                dataAllStore = gd.getAllStore(this.selectedStoreCat);
                dataGridViewAllStore.DataSource = dataAllStore;
                allStoresByCat = gd.getListAllStore(this.selectedStoreCat, 0, "000");


                dataGridViewAllStore.Columns[0].Visible = false;
                dataGridViewAllStore.Columns[1].Visible = false;
                dataGridViewAllStore.Columns[2].Visible = false;
                dataGridViewAllStore.Columns[3].HeaderText = "InvName";
                dataGridViewAllStore.Columns[4].HeaderText = "Small(Unit)";
                dataGridViewAllStore.Columns[5].HeaderText = "Big(Unit)";
                dataGridViewAllStore.Columns[6].HeaderText = "ConvertUnit";
                dataGridViewAllStore.Columns[7].Visible = false;
                dataGridViewAllStore.Columns[8].Visible = false;


                dataGridViewAllStore.Columns[11].HeaderText = "InvCode";
                dataGridViewAllStore.Columns[12].Visible = false;
                dataGridViewAllStore.Columns[13].Visible = false; 


                // getComboAllStore();
            }
            catch (Exception ex)
            {

            }
        } 

        private void buttonAddTable_Click(object sender, EventArgs e)
        {

            try
            {
                string productNameTH = txtBoxNameTH.Text; 
                string productUnit = txtBoxUnit.Text;
                string productDesc = txtBoxDesc.Text;
                float productPrice = float.Parse(txtBoxPrice.Text); 


                int productBBF = Int32.Parse(txtBoxBestBefore.Text);
                float productCost = float.Parse(txtBoxCost.Text);
                float productPriceMem = float.Parse(txtBoxB2BPrice.Text);
                 
                string productBarcode = txtBoxBarCode.Text;

                int productID = Int32.Parse(comboBoxAllProduct.SelectedValue.ToString()); 

                if (productBarcode.Length > 0)
                    if (FuncString.IsNumeric(txtBoxBarCode.Text) == false)
                        throw new Exception("Error Format Product Sorting");


  

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void getComboAllProduct()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(-1, "= เลือกแก้ไขเมนู ="));

            foreach (Product c in allProducts)
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


        private void button1_Click(object sender, EventArgs e)
        {
            txtBoxNameTH.Text = ""; 
            txtBoxUnit.Text = "หน่วย";
            txtBoxPrice.Text = "";
            txtBoxDesc.Text = "";
            txtBoxBarCode.Text = ""; 
            txtBoxB2BPrice.Text = "0";
            txtBoxWHPrice.Text = "0";
            txtBoxCost.Text = "0";
            txtBoxBestBefore.Text = "3";
           
        }

        private void clearForm()
        {
            txtBoxNameTH.Text = ""; 
            txtBoxUnit.Text = "หน่วย";
            txtBoxPrice.Text = "";
            txtBoxDesc.Text = "0";
            txtBoxBarCode.Text = ""; 
            radioButtonAddData.Checked = true; 
            txtBoxB2BPrice.Text = "0";
            txtBoxWHPrice.Text = "0";
            txtBoxCost.Text = "0";
            txtBoxBestBefore.Text = "3";
        }

        private void ChangeUpdateFlag(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked == true)
            {
                comboBoxAllProduct.Visible = false;
                labelHeader.Text = "เพิ่มสินค้า"; 
            }
            else
            {
                comboBoxAllProduct.Visible = true;
                labelHeader.Text = "แก้ไขเสินค้า"; 
            }

           // getComboAllProduct();
        }
         


        private void buttonBackToManage_Click(object sender, EventArgs e)
        {
            LinkFormMainManage();
        }

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

 

        private void CommboxAllProduct_Change(object sender, EventArgs e)
        {
            try
            {

                int productID = Int32.Parse(comboBoxAllProduct.SelectedValue.ToString());

                foreach (Product c in allProducts)
                {
                    if (c.ProductID == productID)
                    {
                        txtBoxNameTH.Text = c.ProductName; 
                        txtBoxUnit.Text = c.ProductUnit;
                        txtBoxDesc.Text = c.ProductDesc;
                        txtBoxPrice.Text = c.ProductPrice.ToString();
                        txtBoxBarCode.Text = c.ProductBarcode;
                        txtBoxWHPrice.Text = c.ProductPrice3.ToString();
                        txtBoxB2BPrice.Text = c.ProductPrice2.ToString();
                        txtBoxCost.Text = c.ProductPrice3.ToString();
                        txtBoxBestBefore.Text = "3";  //c.ProductBestBefore.ToString();

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonScan_Click(object sender, EventArgs e)
        {
            txtBoxBarCode.Text = "";
            txtBoxBarCode.Focus();
        }

      

        private void buttonPrintBarCode_Click(object sender, EventArgs e)
        {
            try
            {

                string txtbarCode = textBoxFinalBC.Text;

                BarCode.GenBarCode(txtbarCode);
                barcodeImg = BarCode.resultBarcode;
                 
                int loop = 0;
                int loops = 0;

                if (radioButtonPrint1.Checked)
                { 
                    loop = Int32.Parse(comboBoxLoopPrint.Text);
                    for (int ii = 1; ii <= loop; ii++)
                    {
                        printBarcode1.Print();
                    }
                }
                else if (radioButtonPrint2.Checked) {

                    loop = Int32.Parse(comboBoxLoopPrint.Text);

                    if (loop % 3 == 0)
                        loops = loop / 3;
                    else
                        loops = (loop / 3) + 1;

                    for (int ii = 1; ii <= loops; ii++)
                    {
                        modSticker = 3;

                        if (ii == loops)
                        {
                            modSticker = loop % 3;
                            if (modSticker == 0)
                                modSticker = 3;
                        }


                        printBarcode2.Print();
                    }

                }
                else if (radioButtonPrint3.Checked)
                {

                    loop = Int32.Parse(comboBoxLoopPrint.Text);
                    for (int ii = 1; ii <= loop; ii++)
                    {
                        printBarcode3.Print();
                    }

                }
                else if (radioButtonPrint4.Checked)
                {

                    loop = Int32.Parse(comboBoxLoopPrint.Text);

                    if (loop % 2 == 0)
                        loops = loop / 2;
                    else
                        loops = (loop / 2) + 1;

                    for (int ii = 1; ii <= loops; ii++)
                    {
                        modSticker = 2;

                        if (ii == loops)
                        {
                            modSticker = loop % 2;
                            if (modSticker == 0)
                                modSticker = 2;
                        }


                        printBarcode4.Print();
                    }

                }
                else if (radioButtonPrint5.Checked)
                {

                    loop = Int32.Parse(comboBoxLoopPrint.Text);
                    for (int ii = 1; ii <= loop; ii++)
                    {
                        printBarcode5.Print();
                    }

                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                 
                //File.Delete(pathPrintBarcode);
            }


        }

        private void OnPrintBarCode1(object sender, PrintPageEventArgs e)
        {



            try
            {

                int x = 5;
                int y = 0;
                string proName = txtBoxNameTH.Text + " (" + float.Parse(textBoxTotalPrice.Text).ToString("###,###.##") + " B.)";
                string proPrice = " ราคา : " + float.Parse(textBoxTotalPrice.Text).ToString("###,###.##") + " B.";

                Brush brush = new SolidBrush(Color.Black);
                Font fontBodyP = new Font("Tahoma", 9);
                Font fontBody = new Font("Tahoma", 7);
                Font fontBodyShop = new Font("Tahoma", 6);


                string shopName = ConfigurationSettings.AppSettings["RestName"];

                //e.Graphics.DrawString(shopName, fontBodyShop, brush, x + 10, y);

                //if (this.modSticker > 1)
                //    e.Graphics.DrawString(shopName, fontBodyShop, brush, x + 160 + 10, y);

                //if (this.modSticker > 2)
                //    e.Graphics.DrawString(shopName, fontBodyShop, brush, x + 288 + 10, y);


                y += 0;

                e.Graphics.DrawImage(barcodeImg, x, y, 110, 55); 


                // ชื่อสินค้า 

                y += 55;

                if (proName.Length <= 28)
                {
                    e.Graphics.DrawString(proName, fontBody, brush, x, y);
                     

                }
                else if (proName.Length <= 56)
                {
                    string proName1 = proName.Substring(0, 27);
                    string proName2 = proName.Substring(27, proName.Length - 27);

                    e.Graphics.DrawString(proName1, fontBody, brush, x, y); 

                    y += 10;

                    e.Graphics.DrawString(proName2, fontBody, brush, x, y);
                     

                }
                else
                {
                    string proName1 = proName.Substring(0, 27);
                    string proName2 = proName.Substring(27, proName.Length - 27);

                    e.Graphics.DrawString(proName1, fontBody, brush, x, y); 

                    y += 10;

                    e.Graphics.DrawString(proName2, fontBody, brush, x, y); 
                }

                // ชื่อราคา 

                //y += 10;

                //e.Graphics.DrawString(proPrice, fontBody, brush, x, y); 


                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        // OnPrintPage
        private void OnPrintBarCode2(object sender, PrintPageEventArgs e)
        {



            try
            {

                int x = 5;
                int y = 0;
                string proName = txtBoxNameTH.Text;
                string proPrice = " ราคา : " + float.Parse(textBoxTotalPrice.Text).ToString("###,###.##") + " B.";

                Brush brush = new SolidBrush(Color.Black);
                Font fontBodyP  = new Font("Tahoma", 9);
                Font fontBody = new Font("Tahoma", 7);
                Font fontBodyShop = new Font("Tahoma", 6);


                string shopName = ConfigurationSettings.AppSettings["RestName"];

                //e.Graphics.DrawString(shopName, fontBodyShop, brush, x + 10, y);

                //if (this.modSticker > 1)
                //    e.Graphics.DrawString(shopName, fontBodyShop, brush, x + 160 + 10, y);

                //if (this.modSticker > 2)
                //    e.Graphics.DrawString(shopName, fontBodyShop, brush, x + 288 + 10, y);


                y += 8;

                e.Graphics.DrawImage(barcodeImg, x, y, 110, 50);

                if (this.modSticker > 1)
                    e.Graphics.DrawImage(barcodeImg, x + 137, y, 110, 50);

                if (this.modSticker > 2)
                    e.Graphics.DrawImage(barcodeImg, x + 270, y, 110, 50);



                // ชื่อสินค้า 

                y += 50;

                if (proName.Length <= 35)
                {
                    e.Graphics.DrawString(proName, fontBodyShop, brush, x, y);

                    if (this.modSticker > 1)
                        e.Graphics.DrawString(proName, fontBodyShop, brush, x + 137, y);

                    if (this.modSticker > 2)
                        e.Graphics.DrawString(proName, fontBodyShop, brush, x + 270, y);

                }
                else if (proName.Length <= 70)
                {
                    string proName1 = proName.Substring(0, 35);
                    string proName2 = proName.Substring(35, proName.Length - 35);

                    e.Graphics.DrawString(proName1, fontBodyShop, brush, x, y);

                    if (this.modSticker > 1)
                        e.Graphics.DrawString(proName1, fontBodyShop, brush, x + 137, y);

                    if (this.modSticker > 2)
                        e.Graphics.DrawString(proName1, fontBodyShop, brush, x + 270, y);

                    y += 10;

                    e.Graphics.DrawString(proName2, fontBodyShop, brush, x, y);

                    if (this.modSticker > 1)
                        e.Graphics.DrawString(proName2, fontBodyShop, brush, x + 137, y);

                    if (this.modSticker > 2)
                        e.Graphics.DrawString(proName2, fontBodyShop, brush, x + 270, y);

                }

                // ชื่อราคา 

                y += 10;

                e.Graphics.DrawString(proPrice, fontBodyP, brush, x, y);

                if (this.modSticker > 1)
                    e.Graphics.DrawString(proPrice, fontBodyP, brush, x + 137, y);

                if (this.modSticker > 2)
                    e.Graphics.DrawString(proPrice, fontBodyP, brush, x + 270, y);




                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        // OnPrintPage
        private void OnPrintBarCode3(object sender, PrintPageEventArgs e)
        {

            try
            {

                int x = 10;
                int y = 7;
                string proName = txtBoxNameTH.Text;
                string proPrice = "" + float.Parse(txtBoxPrice.Text).ToString("###,###.#0");
                string proTotalPrice = "" + float.Parse(textBoxTotalPrice.Text).ToString("###,###.#0");
                string probestBefore = DateTime.Now.AddDays(Double.Parse(txtBoxBestBefore.Text)).ToString("dd/MM/yyyy");
                string proBarcode = txtBoxBarCode.Text;
             




                float iW = float.Parse(textBoxWeightitem.Text.ToString());

                Brush brush = new SolidBrush(Color.Black);
                Font fontBodyP = new Font("Tahoma", 9);
                Font fontBody = new Font("Tahoma", 8);
                Font fontBodyShop = new Font("Tahoma", 8);
                Font fontBodyShops = new Font("Tahoma", 5);


             

                if (radioButtonProduct.Checked) // Product
                {

                    e.Graphics.DrawString(proName, fontBody, brush, x, y);

                    y += 12;

                    if (txtBoxUnit.Text.ToLower().Contains("kg"))
                    {
                        if (radioButtonFlagFromWeight.Checked)
                            e.Graphics.DrawString("Best Before", fontBodyShop, brush, x, y);

                        e.Graphics.DrawString("Weight", fontBodyShop, brush, x + 80, y);
                        e.Graphics.DrawString("B/KG", fontBodyShop, brush, x + 145, y);

                    }
                    else
                    {
                        if (radioButtonFlagFromWeight.Checked)
                            e.Graphics.DrawString("Best Before", fontBodyShop, brush, x, y);

                        e.Graphics.DrawString("items", fontBodyShop, brush, x + 80, y);
                        e.Graphics.DrawString("B/QTY", fontBodyShop, brush, x + 145, y);
                    }

                    y += 12;
                     

                    e.Graphics.DrawString(probestBefore, fontBody, brush, x, y);
                    e.Graphics.DrawString(iW.ToString(), fontBody, brush, x + 90, y);
                    e.Graphics.DrawString(proPrice, fontBody, brush, x + 150, y);

                    y += 12;

                    e.Graphics.DrawImage(barcodeImg, x, y, 120, 60);


                    // ชื่อราคา 

                    y += 20;
                    e.Graphics.DrawString("Amt", fontBody, brush, x + 145, y);
                    y += 15;
                    e.Graphics.DrawString(proTotalPrice, fontBodyP, brush, x + 127, y);

                }
                else
                {
                    if (txtBoxDesc.Text.Contains("kg"))
                        iW = float.Parse(textBoxWeightitem.Text.ToString()) * 1000; // Convert


                    fontBodyP = new Font("Tahoma", 11);
                    fontBody = new Font("Tahoma", 10);
                    fontBodyShop = new Font("Tahoma", 12);
                     

                    e.Graphics.DrawString("" + proBarcode, fontBodyShop, brush, x, y); 
                    y += 18;
                    e.Graphics.DrawString("" + proName, fontBodyP, brush, x, y);
                    y += 22;
                    e.Graphics.DrawString("Lot No. : " + textBoxLotNo.Text, fontBody, brush, x, y);
                    y += 15;
                    e.Graphics.DrawString("Exp Date : " + probestBefore, fontBody, brush, x, y);
                    y += 15;
                    e.Graphics.DrawString("บรรจุ : " + iW.ToString() + " " + txtBoxUnit.Text , fontBody, brush, x, y);
                   
                }

                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        // OnPrintPage
        private void OnPrintBarCode4(object sender, PrintPageEventArgs e)
        {

            try
            {

                int x = 10;
                int y = 7;
                string proName = txtBoxNameTH.Text;
                string proPrice = "" + float.Parse(txtBoxPrice.Text).ToString("###,###.#0");
                string proTotalPrice = "" + float.Parse(textBoxTotalPrice.Text).ToString("###,###.#0");
                string probestBefore = DateTime.Now.AddDays(Double.Parse(txtBoxBestBefore.Text)).ToString("dd/MM/yyyy");
                string proBarcode = txtBoxBarCode.Text;





                float iW = float.Parse(textBoxWeightitem.Text.ToString());


                Brush brush = new SolidBrush(Color.Black);
                Font fontBodyP = new Font("Tahoma", 9);
                Font fontBody = new Font("Tahoma", 8);
                Font fontBodyShop = new Font("Tahoma", 8);
                Font fontBodyShops = new Font("Tahoma", 5);


                if (radioButtonProduct.Checked) // Product
                {
                  
                    e.Graphics.DrawString(proName, fontBody, brush, x, y);

                    if (this.modSticker > 1)
                        e.Graphics.DrawString(proName, fontBody, brush, x + 200, y);

                    y += 12;

                    if (txtBoxUnit.Text.ToLower().Contains("kg"))
                    {
                        if (radioButtonFlagFromWeight.Checked)
                            e.Graphics.DrawString("Best Before", fontBodyShop, brush, x, y);

                        e.Graphics.DrawString("Weight", fontBodyShop, brush, x + 80, y);
                        e.Graphics.DrawString("B/KG", fontBodyShop, brush, x + 145, y);


                        if (this.modSticker > 1)
                        {
                            if (radioButtonFlagFromWeight.Checked)
                                e.Graphics.DrawString("Best Before", fontBodyShop, brush, x + 200, y);

                            e.Graphics.DrawString("Weight", fontBodyShop, brush, x + 80 + 200, y);
                            e.Graphics.DrawString("B/KG", fontBodyShop, brush, x + 145 + 200, y);

                        }

                    }
                    else
                    {
                        if (radioButtonFlagFromWeight.Checked)
                            e.Graphics.DrawString("Best Before", fontBodyShop, brush, x, y);

                        e.Graphics.DrawString("items", fontBodyShop, brush, x + 80, y);
                        e.Graphics.DrawString("B/QTY", fontBodyShop, brush, x + 145, y);

                        if (this.modSticker > 1)
                        {
                            if (radioButtonFlagFromWeight.Checked)
                                e.Graphics.DrawString("Best Before", fontBodyShop, brush, x + 200, y);

                            e.Graphics.DrawString("items", fontBodyShop, brush, x + 80 + 200, y);
                            e.Graphics.DrawString("B/QTY", fontBodyShop, brush, x + 145 + 200, y);
                        }
                    }

                    y += 12;



                    e.Graphics.DrawString(probestBefore, fontBody, brush, x, y);
                    e.Graphics.DrawString(iW.ToString(), fontBody, brush, x + 90, y);
                    e.Graphics.DrawString(proPrice, fontBody, brush, x + 150, y);


                    if (this.modSticker > 1)
                    {
                        e.Graphics.DrawString(probestBefore, fontBody, brush, x + 200, y);
                        e.Graphics.DrawString(iW.ToString(), fontBody, brush, x + 90 + 200, y);
                        e.Graphics.DrawString(proPrice, fontBody, brush, x + 150 + 200, y);
                    }

                    y += 12;

                    e.Graphics.DrawImage(barcodeImg, x, y, 120, 60);

                    if (this.modSticker > 1)
                    {
                        e.Graphics.DrawImage(barcodeImg, x + 200, y, 120, 60);
                    }


                    // ชื่อราคา 

                    y += 20;
                    e.Graphics.DrawString("Amt", fontBody, brush, x + 145, y);

                    if (this.modSticker > 1)
                        e.Graphics.DrawString("Amt", fontBody, brush, x + 145 + 200, y);


                    y += 15;
                    e.Graphics.DrawString(proTotalPrice, fontBodyP, brush, x + 127, y);

                    if (this.modSticker > 1)
                        e.Graphics.DrawString(proTotalPrice, fontBodyP, brush, x + 127 + 200, y);


                }
                else
                {
                    if (txtBoxDesc.Text.Contains("kg"))
                        iW = float.Parse(textBoxWeightitem.Text.ToString()) * 1000; // Convert


                    fontBodyP = new Font("Tahoma", 11);
                    fontBody = new Font("Tahoma", 10);
                    fontBodyShop = new Font("Tahoma", 12);


                    e.Graphics.DrawString("" + proBarcode, fontBodyShop, brush, x, y);
                    if (this.modSticker > 1)
                        e.Graphics.DrawString("" + proBarcode, fontBodyShop, brush, x + 200, y);
                    y += 18;



                    if (proName.Length <= 24)
                    {
                        e.Graphics.DrawString("" + proName, fontBodyP, brush, x, y);
                        if (this.modSticker > 1)
                            e.Graphics.DrawString("" + proName, fontBodyP, brush, x + 200, y);
                        y += 20;

                    }
                    else
                    {
                        string proName1 = proName.Substring(0, 24);
                        string proName2 = proName.Substring(24, proName.Length - 24);

                        e.Graphics.DrawString(proName1, fontBodyP, brush, x, y);
                        if (this.modSticker > 1)
                            e.Graphics.DrawString(proName1, fontBodyP, brush, x + 200, y);

                        y += 15;

                        e.Graphics.DrawString(proName2, fontBodyP, brush, x, y);
                        if (this.modSticker > 1)
                            e.Graphics.DrawString(proName2, fontBodyP, brush, x + 200, y);
                         
                        y += 22;

                    }

                    e.Graphics.DrawString("Lot No. : " + textBoxLotNo.Text, fontBody, brush, x, y);
                    if (this.modSticker > 1)
                        e.Graphics.DrawString("Lot No. : " + textBoxLotNo.Text, fontBody, brush, x + 200, y);
                    y += 15;

                    e.Graphics.DrawString("Exp Date : " + probestBefore, fontBody, brush, x, y);
                    if (this.modSticker > 1)
                        e.Graphics.DrawString("Exp Date : " + probestBefore, fontBody, brush, x + 200, y);
                    y += 15;

                    e.Graphics.DrawString("บรรจุ : " + iW.ToString() + " " + txtBoxUnit.Text, fontBody, brush, x, y);
                    if (this.modSticker > 1)
                        e.Graphics.DrawString("บรรจุ : " + iW.ToString() + " " + txtBoxUnit.Text, fontBody, brush, x + 200, y);
                }

                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        // OnPrintPage
        private void OnPrintBarCode5(object sender, PrintPageEventArgs e)
        {

            try
            {

                int x = 10;
                int y = 7;
                string proName = txtBoxNameTH.Text;
                string proPrice = "" + float.Parse(txtBoxPrice.Text).ToString("###,###.#0");
                string proTotalPrice = "" + float.Parse(textBoxTotalPrice.Text).ToString("###,###.#0");
                string probestBefore = DateTime.Now.AddDays(Double.Parse(txtBoxBestBefore.Text)).ToString("dd/MM/yyyy");
                string proBarcode = txtBoxBarCode.Text;





                float iW = float.Parse(textBoxWeightitem.Text.ToString());


                Brush brush = new SolidBrush(Color.Black);
                Font fontBodyP = new Font("Tahoma", 9);
                Font fontBody = new Font("Tahoma", 8);
                Font fontBodyShop = new Font("Tahoma", 8);
                Font fontBodyShops = new Font("Tahoma", 5);

           

                y += 12;

                if (radioButtonProduct.Checked) // Product
                {

                    if (txtBoxUnit.Text.ToLower().Contains("kg"))
                    {
                        if (radioButtonFlagFromWeight.Checked)
                            e.Graphics.DrawString("Best Before", fontBodyShop, brush, x, y);

                        e.Graphics.DrawString("Weight", fontBodyShop, brush, x + 80, y);
                        e.Graphics.DrawString("B/KG", fontBodyShop, brush, x + 145, y);

                    }
                    else
                    {
                        if (radioButtonFlagFromWeight.Checked)
                            e.Graphics.DrawString("Best Before", fontBodyShop, brush, x, y);

                        e.Graphics.DrawString("items", fontBodyShop, brush, x + 80, y);
                        e.Graphics.DrawString("B/QTY", fontBodyShop, brush, x + 145, y);
                    }

                    y += 12;


                


                    e.Graphics.DrawString(probestBefore, fontBody, brush, x, y);
                    e.Graphics.DrawString(iW.ToString(), fontBody, brush, x + 90, y);
                    e.Graphics.DrawString(proPrice, fontBody, brush, x + 150, y);

                    y += 12;

                    e.Graphics.DrawImage(barcodeImg, x, y, 120, 60);


                    // ชื่อราคา 

                    y += 20;
                    e.Graphics.DrawString("Amt", fontBody, brush, x + 145, y);
                    y += 15;
                    e.Graphics.DrawString(proTotalPrice, fontBodyP, brush, x + 127, y);

                }
                else
                {
                    if (txtBoxDesc.Text.Contains("kg"))
                        iW = float.Parse(textBoxWeightitem.Text.ToString()) * 1000; // Convert


                    fontBodyP = new Font("Tahoma", 11);
                    fontBody = new Font("Tahoma", 10);

                    e.Graphics.DrawString("" + proBarcode, fontBodyP, brush, x, y);
                    y += 15;
                    e.Graphics.DrawString("" + proName, fontBodyP, brush, x, y);
                    y += 20;
                    e.Graphics.DrawString("Lot No. : " + textBoxLotNo.Text, fontBody, brush, x, y);
                    y += 15;
                    e.Graphics.DrawString("Exp Date : " + probestBefore, fontBody, brush, x, y);
                    y += 15;
                    e.Graphics.DrawString("บรรจุ : " + iW.ToString() + " " + txtBoxUnit.Text, fontBody, brush, x, y);

                }

                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        private Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }




         

   

        private void buttonHelperColour_Click(object sender, EventArgs e)
        {
            string rgb = "";

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                rgb = colorDialog1.Color.R.ToString() + "," + colorDialog1.Color.G.ToString() + "," + colorDialog1.Color.B.ToString();

                txtBoxUnit.Text = rgb + "|" + "B";
            }
        }

        private void txtBoxPrice_TextChanged(object sender, EventArgs e)
        {
            //txtBoxB2BPrice.Text = txtBoxPrice.Text;
            //txtBoxWHPrice
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string srPID = textBoxSRProductID.Text;
                string srPBC = textBoxSRBarcode.Text;
                string srPName = textBoxSRProductName.Text;



             //   this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductID like '*{0}*' and ProductImage like '*{1}*' and ProductName like '*{2}*' ", srPID, srPBC, srPName);

                if (srPName.Length > 0)
                {
                    this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductName like '*{0}*'  and ProductFlagUse = 'Y' ", srPName);

                }
                else
                {
                    if (srPID.Length > 0)
                        this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductID = '{0}' and ProductFlagUse = 'Y' ", srPID);
                    else if (srPBC.Length > 0)
                        this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductImage = '{0}' and ProductFlagUse = 'Y' ", srPBC);
                    else
                        this.dataAllProduct.DefaultView.RowFilter = string.Format("ProductID > 4 and ProductFlagUse = 'Y'");

                }
                 

            }
            catch (Exception ex)
            {

            }


        }

        private void textBoxWeightitem_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBoxWeightitem_KeyPress(object sender, KeyPressEventArgs e)
        {
            float totalPrice = 0;
            string finalBC = "";
            string wItemBC = "";
            string priceBC = "";
            string digit = "";

            float iW = 0;
            float pPrice = 0;

            if (e.KeyChar == (char)13)
            {

                try
                {

                    iW = float.Parse(textBoxWeightitem.Text.ToString());

                    if( radioButton_NormalPrice.Checked )
                        pPrice = float.Parse(txtBoxPrice.Text.ToString());
                    else if (radioButton_B2BPrice.Checked)
                        pPrice = float.Parse(txtBoxB2BPrice.Text.ToString());
                    else
                        pPrice = float.Parse(txtBoxWHPrice.Text.ToString());


                    if (txtBoxUnit.Text.ToLower().Contains("kg"))
                    {
                        totalPrice = iW * pPrice;
                        digit = "9";

                        wItemBC = "000" + (float.Parse(textBoxWeightitem.Text.ToString()) * 1000).ToString();
                        wItemBC = wItemBC.Substring(wItemBC.Length - 4, 4);
                    }
                    else
                    {
                        totalPrice = iW * pPrice;
                        digit = "8";

                        wItemBC = "000" + float.Parse(textBoxWeightitem.Text.ToString()).ToString();
                        wItemBC = wItemBC.Substring(wItemBC.Length - 4, 4);
                    }




                    priceBC = totalPrice.ToString("####.##");



                    if (priceBC.Contains("."))
                    {
                        string[] pBC = priceBC.Split('.');

                        int p1 = Int32.Parse(pBC[0]);
                        int p2 = Int32.Parse(pBC[1]);

                        if (p2 > 50)
                        {
                            p1 += 1;

                            priceBC = "00000" + p1.ToString();
                            priceBC = priceBC.Substring(priceBC.Length - 5, 5) + "00";

                            this.finalprice = (float)p1;
                        }
                        else
                        {
                            priceBC = "00000" + p1.ToString();
                            priceBC = priceBC.Substring(priceBC.Length - 5, 5) + "50";
                            this.finalprice = (float)p1 + (float)0.5;
                        }

                    }
                    else
                    {
                        priceBC = "00000" + priceBC.ToString();
                        priceBC = priceBC.Substring(priceBC.Length - 5, 5) + "00";
                        this.finalprice = float.Parse(priceBC) / 100;
                    }

                    finalBC = digit + txtBoxBarCode.Text + wItemBC + priceBC;


                    if (radioButtonFlagFromWeight.Checked)
                        textBoxFinalBC.Text = finalBC;
                    else
                        textBoxFinalBC.Text = txtBoxBarCode.Text;

                    textBoxTotalPrice.Text = this.finalprice.ToString("###,###.#0");

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }

            }
        }

        private void textBoxTotalPrice_TextChanged(object sender, EventArgs e)
        {
            float totalPrice = 0;
            string finalBC = "";
            string wItemBC = "";
            string priceBC = "";
            string digit = "";

            float iW = 0;
            float pPrice = 0;

       

                try
                {

                    iW = float.Parse(textBoxWeightitem.Text.ToString());


                   if( radioButton_NormalPrice.Checked )
                        pPrice = float.Parse(txtBoxPrice.Text.ToString());
                    else if (radioButton_B2BPrice.Checked)
                        pPrice = float.Parse(txtBoxB2BPrice.Text.ToString());
                    else
                        pPrice = float.Parse(txtBoxWHPrice.Text.ToString());


                    if (txtBoxUnit.Text.ToLower().Contains("kg"))
                    {
                        totalPrice = iW * pPrice;
                        digit = "9";

                        wItemBC = "000" + (float.Parse(textBoxWeightitem.Text.ToString()) * 1000).ToString();
                        wItemBC = wItemBC.Substring(wItemBC.Length - 4, 4);
                    }
                    else
                    {
                        totalPrice = iW * pPrice;
                        digit = "8";

                        wItemBC = "000" + float.Parse(textBoxWeightitem.Text.ToString()).ToString();
                        wItemBC = wItemBC.Substring(wItemBC.Length - 4, 4);
                    }




                    priceBC = float.Parse(textBoxTotalPrice.Text).ToString();//totalPrice.ToString("####.##");



                    if (priceBC.Contains("."))
                    {
                        string[] pBC = priceBC.Split('.');

                        int p1 = Int32.Parse(pBC[0]);
                        int p2 = Int32.Parse(pBC[1]);

                        if (p2 > 50)
                        {
                            p1 += 1;

                            priceBC = "00000" + p1.ToString();
                            priceBC = priceBC.Substring(priceBC.Length - 5, 5) + "00";

                            this.finalprice = (float)p1;
                        }
                        else
                        {
                            priceBC = "00000" + p1.ToString();
                            priceBC = priceBC.Substring(priceBC.Length - 5, 5) + "50";
                            this.finalprice = (float)p1 + (float)0.5;
                        }

                    }
                    else
                    {
                        priceBC = "00000" + priceBC.ToString();
                        priceBC = priceBC.Substring(priceBC.Length - 5, 5) + "00";
                        this.finalprice = float.Parse(priceBC) / 100;
                    }

                    finalBC = digit + txtBoxBarCode.Text + wItemBC + priceBC;
                  

                    if (radioButtonFlagFromWeight.Checked)
                        textBoxFinalBC.Text = finalBC;
                    else
                        textBoxFinalBC.Text = txtBoxBarCode.Text;

                    //textBoxTotalPrice.Text = this.finalprice.ToString("###,###.#0");

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }

            
        }

        private void radioButton_NormalPrice_CheckedChanged(object sender, EventArgs e)
        {
            float totalPrice = 0;
            string finalBC = "";
            string wItemBC = "";
            string priceBC = "";
            string digit = "";

            float iW = 0;
            float pPrice = 0;

           
                try
                {

                    iW = float.Parse(textBoxWeightitem.Text.ToString());

                     if( radioButton_NormalPrice.Checked )
                        pPrice = float.Parse(txtBoxPrice.Text.ToString());
                    else if (radioButton_B2BPrice.Checked)
                        pPrice = float.Parse(txtBoxB2BPrice.Text.ToString());
                    else
                        pPrice = float.Parse(txtBoxWHPrice.Text.ToString());


                    if (txtBoxUnit.Text.ToLower().Contains("kg"))
                    {
                        totalPrice = iW * pPrice;
                        digit = "9";

                        wItemBC = "000" + (float.Parse(textBoxWeightitem.Text.ToString()) * 1000).ToString();
                        wItemBC = wItemBC.Substring(wItemBC.Length - 4, 4);
                    }
                    else
                    {
                        totalPrice = iW * pPrice;
                        digit = "8";

                        wItemBC = "000" + float.Parse(textBoxWeightitem.Text.ToString()).ToString();
                        wItemBC = wItemBC.Substring(wItemBC.Length - 4, 4);
                    }




                    priceBC = totalPrice.ToString("####.##");



                    if (priceBC.Contains("."))
                    {
                        string[] pBC = priceBC.Split('.');

                        int p1 = Int32.Parse(pBC[0]);
                        int p2 = Int32.Parse(pBC[1]);

                        if (p2 > 50)
                        {
                            p1 += 1;

                            priceBC = "0000" + p1.ToString();
                            priceBC = priceBC.Substring(priceBC.Length - 4, 4) + "00";

                            this.finalprice = (float)p1;
                        }
                        else
                        {
                            priceBC = "0000" + p1.ToString();
                            priceBC = priceBC.Substring(priceBC.Length - 4, 4) + "50";
                            this.finalprice = (float)p1 + (float)0.5;
                        }

                    }
                    else
                    {
                        priceBC = "0000" + priceBC.ToString();
                        priceBC = priceBC.Substring(priceBC.Length - 4, 4) + "00";
                        this.finalprice = float.Parse(priceBC) / 100;
                    }

                    finalBC = digit + txtBoxBarCode.Text + wItemBC + priceBC;

                    if (radioButtonFlagFromWeight.Checked)
                        textBoxFinalBC.Text = finalBC;
                    else
                        textBoxFinalBC.Text = txtBoxBarCode.Text;

                    textBoxTotalPrice.Text = this.finalprice.ToString("###,###.#0");

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }

             
        }

        List<Cat> allCats;

        private void getComboAllCat()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "===== เลือกทั้งหมด ====="));

            foreach (Cat c in allCats)
            {
                data.Add(new KeyValuePair<int, string>(c.CatID, c.CatName));
            }


            // Clear the combobox
            comboBoxAllCat.DataSource = null;
            comboBoxAllCat.Items.Clear();

            // Bind the combobox
            comboBoxAllCat.DataSource = new BindingSource(data, null);
            comboBoxAllCat.DisplayMember = "Value";
            comboBoxAllCat.ValueMember = "Key";
              
        }

        private void comboBoxAllCat_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                selectedCat = Int32.Parse(comboBoxAllCat.SelectedValue.ToString());
                //allProductsByCats = gd.getProductByCat(selectedCat, 0, 0);
                dataAllProduct = gd.getAllProduct(selectedCat);
                dataGriadViewAllProduct.DataSource = dataAllProduct;

               // getComboAllProduct();
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


                if (srPName.Length > 0)
                {
                    this.dataAllStore.DefaultView.RowFilter = string.Format("StoreName like '*{0}*' or StoreOrder like '*{0}*' ", srPName);

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

        private void dataGriadViewAllProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridproductID = Int32.Parse(dataGriadViewAllProduct.Rows[e.RowIndex].Cells["ProductID"].Value.ToString());

                comboBoxAllProduct.SelectedValue = dataGridproductID;

                comboBoxAllProduct.Visible = true;
                labelHeader.Text = "รายละเอียดสินค้า";
                radioButtonUpdateData.Checked = true;

                textBoxWeightitem.Text = "1";

                textBoxTotalPrice.Text =  dataGriadViewAllProduct.Rows[e.RowIndex].Cells["ProductPrice"].Value.ToString() ;




                radioButtonProduct.Checked = true;
                radioButtonFlagFromManual.Checked = true;

                textBoxWeightitem_TextChanged(sender, null); 
                textBoxWeightitem.Focus();
              
            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridViewAllStore_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                txtBoxNameTH.Text = dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreName"].Value.ToString();
                txtBoxUnit.Text = dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreUnit"].Value.ToString();
                txtBoxDesc.Text = dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreConvertUnit"].Value.ToString();            
                txtBoxBarCode.Text = dataGridViewAllStore.Rows[e.RowIndex].Cells["StoreBarcode"].Value.ToString();  
                txtBoxBestBefore.Text = "3";

                txtBoxPrice.Text = "0";
                txtBoxWHPrice.Text = "0";
                txtBoxB2BPrice.Text = "0";
                radioButtonFlagFromManual.Checked = true;

                textBoxWeightitem.Text = "1";

                radioButtonInventory.Checked = true;
                radioButtonFlagFromManual.Checked = true;

                textBoxFinalBC.Text = txtBoxBarCode.Text;

           //     textBoxWeightitem_TextChanged(sender, null);
                textBoxWeightitem.Focus();

              
            }
            catch (Exception ex)
            {

            }
        }

        private void radioButtonProduct_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonProduct.Checked)
            {

                labelCode_Inv.Visible = false;
                labelDesc_Inv.Visible = false;
                labelUnit_Inv.Visible = false;

                labelCode.Visible = true;
                labelDesc.Visible = true;
                labelUnit.Visible = true;
            }else
            {
                labelCode_Inv.Visible = true;
                labelDesc_Inv.Visible = true;
                labelUnit_Inv.Visible = true;

                labelCode.Visible = false;
                labelDesc.Visible = false;
                labelUnit.Visible = false;

            }


        }

    }
}
