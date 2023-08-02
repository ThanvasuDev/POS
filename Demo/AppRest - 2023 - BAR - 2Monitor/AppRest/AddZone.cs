using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppRest
{
    public partial class AddZone : AddDataTemplate
    {
        GetDataRest gd;
        MainManage formMainManage;
         
        List<Zone> allzones;

        int zoneID;
        List<POSPrinter> posPrinters;

        public AddZone(Form frmlkFrom, int flagFrmClose)
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

       //     selectedGruopCat = 1;

            allzones = gd.getAllZone(); 
            dataGridViewAllMember.DataSource = allzones;

            posPrinters = new List<POSPrinter>();
            posLoadPrinter();

            getComboAllCat();
            getComboAllPrinter();


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

            comboBoxPrinterChecker.DataSource = null;
            comboBoxPrinterChecker.Items.Clear();

            // Bind the combobox
            comboBoxPrinterChecker.DataSource = new BindingSource(data, null);
            comboBoxPrinterChecker.DisplayMember = "Value";
            comboBoxPrinterChecker.ValueMember = "Key";

        }



        private void buttonAddTable_Click(object sender, EventArgs e)
        {

            try
            {
                
                string zoneName = txtBoxNameTH.Text;
                string zoneDesc = txtBoxDesc.Text; 
                string flagServiceCharge = comboBoxServiceCharge.Text;
                int zoneSort = Int32.Parse(comboBoxZoneSort.Text);
                int zonePriceID = Int32.Parse(comboBoxPriceID.Text);
                string zoneColour = txtBoxColour.Text;
                string zoneVAT = comboBoxZoneVAT.Text;
                string zoneTYPE = comboBoxZoneTYPE.Text;
                int zonePrinterName = Int32.Parse(comboBoxPrinterName.SelectedValue.ToString());
                string zonePrinterType = comboBoxPrinterType.Text;

                int zonePrinterCheckerNo= Int32.Parse(comboBoxPrinterChecker.SelectedValue.ToString());
                string zoneRemark = txtBoxRemark.Text;

                if (radioButtonAddData.Checked == true)
                {

                    if (MessageBox.Show("คุณต้องการเพิ่มเมนูชื่อ " + zoneName + " หรือไม่ ?", "เพิ่ม " + zoneName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.instNewZone(zoneName, zoneDesc, flagServiceCharge,zoneSort,zoneColour,zonePriceID, zoneVAT, zoneTYPE , zonePrinterName , zonePrinterType ,zonePrinterCheckerNo , zoneRemark);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Zone : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่มข้อมูลชื่อ " + zoneName + ">> (Success)");

                            allzones = gd.getAllZone();
                            dataGridViewAllMember.DataSource = allzones;
                            getComboAllCat();
                            clearForm();

                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("คุณต้องการแก้ไขเมนูชื่อ " + zoneName + " หรือไม่ ?", "เพิ่ม " + zoneName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updsZone(this.zoneID, zoneName, zoneDesc, flagServiceCharge, zoneSort, zoneColour, zonePriceID, zoneVAT, zoneTYPE, zonePrinterName, zonePrinterType, zonePrinterCheckerNo, zoneRemark);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Product : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("แก้ไข : " + zoneName + ">> (Success)");
                            allzones = gd.getAllZone();
                            dataGridViewAllMember.DataSource = allzones; 
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

            data.Add(new KeyValuePair<int, string>(-1, "= เลือกแก้ไขโซน ="));

            foreach (Zone c in allzones)
            {
                data.Add(new KeyValuePair<int, string>(c.ZoneID, c.ZoneName));
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
            txtBoxNameTH.Text = ""; 
            txtBoxDesc.Text = "";
            txtBoxColour.Text = "255,255,255|B";
            comboBoxZoneSort.Text = "1";
            comboBoxPriceID.Text = "1";
            comboBoxServiceCharge.Text = "N";
            comboBoxZoneVAT.Text = "INVAT";
            comboBoxZoneTYPE.Text = "#CUST";

            comboBoxPrinterName.SelectedIndex = 0;
            comboBoxPrinterType.SelectedIndex = 0;

            comboBoxPrinterChecker.SelectedIndex = 0;
            txtBoxRemark.Text = "#SEND #CHECK #BILL";

        }

        private void clearForm()
        {
            txtBoxNameTH.Text = ""; 
            txtBoxDesc.Text = "";
            txtBoxColour.Text = "255,255,255|B";
            comboBoxZoneSort.Text = "1";
            comboBoxPriceID.Text = "1";
            comboBoxServiceCharge.Text = "N";
            comboBoxZoneVAT.Text = "INVAT";
            comboBoxZoneTYPE.Text = "#CUST";
            radioButtonAddData.Checked = true; 

            comboBoxPrinterName.SelectedIndex = 0;
            comboBoxPrinterType.SelectedIndex = 0;

            comboBoxPrinterChecker.SelectedIndex = 0;
            txtBoxRemark.Text = "#SEND #CHECK #BILL";
              

        }

        private void ChangeUpdateFlag(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked == true)
            {
                comboBoxAllCat.Visible = false;
                labelHeader.Text = "เพิ่มโซน";
                buttonAddTable.Text = "เพิ่มข้อมูล";
            }
            else
            {
                comboBoxAllCat.Visible = true;
                labelHeader.Text = "แก้ไขโซน";
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
         

        private void CommboxAllCat_Change(object sender, EventArgs e)
        {
            try
            {

                this.zoneID = Int32.Parse(comboBoxAllCat.SelectedValue.ToString());

                foreach (Zone c in allzones)
                {
                    if (c.ZoneID == zoneID)
                    {
                        txtBoxNameTH.Text = c.ZoneName; 
                        txtBoxDesc.Text = c.ZoneDesc;
                        comboBoxServiceCharge.Text = c.FlagServiceCharge;
                        txtBoxColour.Text = c.ZoneColour;
                        comboBoxPriceID.Text = c.ZonePriceID.ToString();
                        comboBoxZoneSort.Text = c.ZoneSort.ToString();
                        comboBoxZoneVAT.Text = c.ZoneVAT;
                        comboBoxZoneTYPE.Text = c.ZoneType;

                        comboBoxPrinterName.SelectedValue =  c.ZonePrinterNo ; 
                        comboBoxPrinterType.Text = c.ZonePrinterType;

                        comboBoxPrinterChecker.SelectedValue = c.ZonePrinterCheckerNo;
                        txtBoxRemark.Text = c.ZoneRemark;

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

                int dataGridID = Int32.Parse(dataGridViewAllMember.Rows[e.RowIndex].Cells["ZoneID"].Value.ToString());

                comboBoxAllCat.SelectedValue = dataGridID; 
                comboBoxAllCat.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูล";
                buttonAddTable.Text = "แก้ไขข้อมูล";
                radioButtonUpdateData.Checked = true;

            }
            catch (Exception ex)
            {

            }
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
