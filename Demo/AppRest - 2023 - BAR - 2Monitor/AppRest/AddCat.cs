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
    public partial class AddCat : AddDataTemplate
    {
        GetDataRest gd;
        MainManage formMainManage;


        List<GroupCat> allGroupCats;
        List<Cat> allCatByGruopCats;

        int selectedGruopCat;
 
        //string strPrinterOrder1 = "";
        //string strPrinterOrder2 = "";
        //string strPrinterOrder3 = "";
        //string strPrinterOrder4 = "";
        //string strPrinterOrder5 = "";
        //string strPrinterOrder6 = "";


        //string printerOrder1 = "";
        //string printerOrder2 = "";
        //string printerOrder3 = "";
        //string printerOrder4 = "";
        //string printerOrder5 = "";
        //string printerOrder6 = "";


        //string flagPrintOrder1 = "";
        //string flagPrintOrder2 = "";
        //string flagPrintOrder3 = "";
        //string flagPrintOrder4 = "";
        //string flagPrintOrder5 = "";
        //string flagPrintOrder6 = "";
         

        List<POSPrinter> posPrinters;


        public AddCat(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();

            this.Width = 1024;
            this.Height = 764;
             
            gd = new GetDataRest();

            comboBoxAllCat.Visible = false;

            selectedGruopCat = 1;

            allGroupCats = gd.getAllGroupCat(1);

            allCatByGruopCats = gd.getCatByGroupCat(selectedGruopCat);
            dataGridViewAllMember.DataSource = allCatByGruopCats;
             

            posPrinters = new List<POSPrinter>();
            posLoadPrinter(); 

            getComboAllGroupCat();
            clearForm(); 
        }

        private void posLoadPrinter()
        {
            string flagPrint = "";
            string printerName = "";
            string strPrinter = "";

            flagPrint = ConfigurationSettings.AppSettings["FlagPrintCash"].ToString();
            printerName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();
            strPrinter = "Cashier";

            posPrinters.Add(new POSPrinter(0, strPrinter, printerName, flagPrint));

            for (int i = 1; i <= 20; i++)
            {

                flagPrint = ConfigurationSettings.AppSettings["FlagPrintOrder" + i.ToString()].ToString();
                printerName = ConfigurationSettings.AppSettings["PrinterOrder" + i.ToString()].ToString();
                strPrinter = ConfigurationSettings.AppSettings["StrPrintOrder" + i.ToString()].ToString();

                posPrinters.Add(new POSPrinter(i, strPrinter, printerName, flagPrint));

            }

            flagPrint = ConfigurationSettings.AppSettings["flagPrintOrderCheckerOther"].ToString();
            printerName = ConfigurationSettings.AppSettings["PrinterNameCheckOrder"].ToString();
            strPrinter = "Cashier";

            posPrinters.Add(new POSPrinter(21, strPrinter, printerName, flagPrint));


        }


        private void getComboAllGroupCat()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List
             

            foreach (GroupCat c in allGroupCats)
            {
                data.Add(new KeyValuePair<int, string>(c.GroupCatID, c.GroupCatName));
            }


            // Clear the combobox
            comboBoxAllGroupCat.DataSource = null;
            comboBoxAllGroupCat.Items.Clear();

            // Bind the combobox
            comboBoxAllGroupCat.DataSource = new BindingSource(data, null);
            comboBoxAllGroupCat.DisplayMember = "Value";
            comboBoxAllGroupCat.ValueMember = "Key";

            comboBoxAllGroupCat.SelectedValue = selectedGruopCat ;


            // Clear the combobox
            comboBoxGroupCat.DataSource = null;
            comboBoxGroupCat.Items.Clear();

            // Bind the combobox
            comboBoxGroupCat.DataSource = new BindingSource(data, null);
            comboBoxGroupCat.DisplayMember = "Value";
            comboBoxGroupCat.ValueMember = "Key";


            getComboAllCat();
            getComboAllPrinter();

        }


        private void getComboAllPrinter()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "= Select Printer ="));

            foreach (POSPrinter ps in posPrinters)
            {
                if (ps.FlagPrint.ToLower() == "y" && ps.PrinterNo > 0 && ps.PrinterNo < 21)
                {
                    data.Add(new KeyValuePair<int, string>(ps.PrinterNo, ps.PrinterStrName + " > " + ps.PrinterName));
                }
            } 
 
            data.Add(new KeyValuePair<int, string>(99, "= Delete Category ="));


            // Clear the combobox
            comboBoxPrinterName.DataSource = null;
            comboBoxPrinterName.Items.Clear();

            // Bind the combobox
            comboBoxPrinterName.DataSource = new BindingSource(data, null);
            comboBoxPrinterName.DisplayMember = "Value";
            comboBoxPrinterName.ValueMember = "Key"; 

        }
         
        private void buttonAddTable_Click(object sender, EventArgs e)
        {

            try
            {
                
                string catNameTH = txtBoxNameTH.Text;
                string catNameEN = txtBoxNameEN.Text; 
                string catDesc = txtBoxDesc.Text;
                int catSort =   Int32.Parse(txtBoxSortNo.Text); 
                int catID = Int32.Parse(comboBoxAllCat.SelectedValue.ToString());
                int groupCatID = 0;

                int catPrinterNo = Int32.Parse(comboBoxPrinterName.SelectedValue.ToString()); 
                float catConsignmentPercent = Int32.Parse(txtBoxConsignment.Text);

                string catPrinterType = comboBoxPrinterType.Text;
                string catColour = txtBoxColour.Text;
                string catFlagUse = comboBoxFlagUse.Text;


                if (radioButtonAddData.Checked == true)
                {
                    groupCatID = Int32.Parse(comboBoxAllGroupCat.SelectedValue.ToString());


                    if (MessageBox.Show("คุณต้องการเพิ่มหมวดสินค้า " + catNameTH + " หรือไม่ ?", "เพิ่ม " + catNameTH, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.instNewCat(groupCatID, catNameTH, catNameEN, catNameEN, catDesc, catPrinterNo, catPrinterType, catColour, catSort, catConsignmentPercent, catFlagUse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Cat : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่มหมวดสินค้า " + catNameTH + ">> (Success)");
                            allCatByGruopCats = gd.getCatByGroupCat(selectedGruopCat);
                            dataGridViewAllMember.DataSource = allCatByGruopCats;
                            getComboAllCat();
                            clearForm();

                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("คุณต้องการแก้ไขเหมวดสินค้า " + catNameTH + " หรือไม่ ?", "เพิ่ม " + catNameTH, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        groupCatID = Int32.Parse(comboBoxGroupCat.SelectedValue.ToString());
                         

                        int result = gd.updsCat(catID , groupCatID, catNameTH, catNameTH, catNameEN, catDesc, catPrinterNo, catPrinterType, catColour, catSort, catConsignmentPercent, catFlagUse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Product : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("แก้ไขหมวดสินค้า : " + catNameTH + ">> (Success)");
                            allCatByGruopCats = gd.getCatByGroupCat(selectedGruopCat);
                            dataGridViewAllMember.DataSource = allCatByGruopCats;
                            getComboAllCat();
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

        private void getComboAllCat()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(-1, "= เลือกแก้ไขหมวด ="));

            foreach (Cat c in allCatByGruopCats)
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


        private void button1_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void clearForm()
        {
            txtBoxNameTH.Text = "";
            txtBoxNameEN.Text = ""; 
            txtBoxDesc.Text = "";
            txtBoxSortNo.Text = "99";
            comboBoxPrinterName.SelectedValue = 0;
            txtBoxConsignment.Text = "0";
            comboBoxPrinterType.Text = "ALL";
            txtBoxColour.Text = "255,255,255|B";
            comboBoxFlagUse.Text = "";
            comboBoxGroupCat.SelectedIndex = 0;

            radioButtonAddData.Checked = true;
            
        }

        private void ChangeUpdateFlag(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked == true)
            {
                comboBoxAllCat.Visible = false;
                labelHeader.Text = "เพิ่มเหมวดสินค้า";
                buttonAddTable.Text = "เพิ่มข้อมูล";
            }
            else
            {
                comboBoxAllCat.Visible = true;
                labelHeader.Text = "แก้ไขหมวดสินค้า";
                buttonAddTable.Text = "แก้ไขข้อมูล";
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

        private void ComboBoxAllGroupCat_Change(object sender, EventArgs e)
        {
            try
            {
                selectedGruopCat = Int32.Parse(comboBoxAllGroupCat.SelectedValue.ToString());
                allCatByGruopCats = gd.getCatByGroupCat(selectedGruopCat);
                dataGridViewAllMember.DataSource = allCatByGruopCats;
                getComboAllCat();
            }
            catch (Exception ex)
            {

            }
        }

        private void CommboxAllCat_Change(object sender, EventArgs e)
        {
            try
            {

                int catID = Int32.Parse(comboBoxAllCat.SelectedValue.ToString());

                foreach (Cat c in allCatByGruopCats)
                {
                    if (c.CatID == catID)
                    {
                        txtBoxNameTH.Text = c.CatNameTH;
                        txtBoxNameEN.Text = c.CatNameEN; 
                        txtBoxDesc.Text = c.CatDesc;
                        txtBoxSortNo.Text = c.CatSort.ToString(); // if Cat Sort = 100 // Cat Remark Group
                        comboBoxPrinterName.SelectedValue = c.CatPrinterNo ;
                        txtBoxConsignment.Text = c.CatConsignmentPercent.ToString(); 
                        comboBoxPrinterType.Text = c.CatPrinterType;
                        txtBoxColour.Text = c.CatColour;
                        comboBoxFlagUse.Text = c.CatFlagUse;
                        comboBoxGroupCat.SelectedValue = c.GroupCatID;

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridViewAllMember_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridCatID = Int32.Parse(dataGridViewAllMember.Rows[e.RowIndex].Cells["CatID"].Value.ToString());

                comboBoxAllCat.SelectedValue = dataGridCatID;

                comboBoxAllCat.Visible = true;
                labelHeader.Text = "แก้ไขหมวดสินค้า";
                buttonAddTable.Text = "แก้ไขข้อมูล";
                radioButtonUpdateData.Checked = true;

            }
            catch (Exception ex)
            {

            }
        }

        private void txtBoxNameTH_TextChanged(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked)
              txtBoxNameEN.Text = txtBoxNameTH.Text;
        }

        private void buttonHelperColour_Click(object sender, EventArgs e)
        {
            string rgb = "";

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                rgb = colorDialog1.Color.R.ToString() + "," + colorDialog1.Color.G.ToString() + "," + colorDialog1.Color.B.ToString();

                txtBoxColour.Text = rgb + "|" + "B";
            }
        }
    }
    
}
