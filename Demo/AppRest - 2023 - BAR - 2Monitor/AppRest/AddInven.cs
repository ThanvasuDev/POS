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
    public partial class AddInven : AddDataTemplate
    {
        GetDataRest gd;
        MainManage formMainManage;

        DataTable dataTableInvent;
        List<Inventory> allInventory; 

        public AddInven(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();


            gd = new GetDataRest();

            try
            {

                comboBoxAllInven.Visible = false;

                //    selectedGruopCat = 1;

                allInventory = gd.getAllInventory(0, "0", "0", "0");

                dataTableInvent = gd.getAllInvenData(0, "0", "0", "0");
                dataGridViewAllData.DataSource = dataTableInvent;
                getComboAllInvent();
                clearForm();


                dataGridViewAllData.Columns[0].HeaderText = "WHID";
                dataGridViewAllData.Columns[1].HeaderText = "WHName";
                dataGridViewAllData.Columns[2].Visible = false;
                dataGridViewAllData.Columns[3].HeaderText = "WHCode";
                dataGridViewAllData.Columns[4].HeaderText = "WHFullName";
                dataGridViewAllData.Columns[5].HeaderText = "WHAddr";
                dataGridViewAllData.Columns[6].HeaderText = "WHTaxID";
                dataGridViewAllData.Columns[7].HeaderText = "WHTelNo";
                dataGridViewAllData.Columns[8].HeaderText = "WHTelFax";
                dataGridViewAllData.Columns[9].Visible = false;

            }
            catch (Exception ex)
            {


            }
             
        
        }


        private void getComboAllInvent()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(-1, "= เลือกแก้ไขข้อมูล ="));

            foreach (Inventory c in allInventory)
            {
                data.Add(new KeyValuePair<int, string>(c.InventoryID, c.InventoryName));
            }


            // Clear the combobox
            comboBoxAllInven.DataSource = null;
            comboBoxAllInven.Items.Clear();

            // Bind the combobox
            comboBoxAllInven.DataSource = new BindingSource(data, null);
            comboBoxAllInven.DisplayMember = "Value";
            comboBoxAllInven.ValueMember = "Key";

            //comboBoxAllInven.SelectedValue = selectedGruopCat; 

        }



        private void buttonAddData_Click(object sender, EventArgs e)
        {

            try
            {

                int invenid = Int32.Parse(comboBoxAllInven.SelectedValue.ToString());

                
                string inventName = txtBoxInvenName.Text;
                string inventAddr = txtBoxInvenAddr.Text; 
                string inventTaxID = txtBoxInvenTaxID.Text;
                string inventTelNo = txtBoxInvenTelNo.Text;
                string inventRemark = txtBoxRemark.Text;
                string inventFlagSupplier = comboBoxFlagSupplier.Text;
                string inventFlagUse = comboBoxFlagUse.Text;

                
                string inventCode = txtBoxInvenCode.Text;
                string inventCompanyName = txtBoxInvenCompanyName.Text;
                string inventFaxNo = txtBoxInvenFaxNo.Text;
                float inventPercentGP = 0;//float.Parse(txtBoxPercentGP.Text);
                 


                if (radioButtonAddData.Checked == true)
                {

                    if (MessageBox.Show("คุณต้องการเพิ่มข้อมูล " + inventName + " หรือไม่ ?", "เพิ่ม " + inventName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.insertInven(inventName, inventCode, inventCompanyName, inventFlagSupplier, inventAddr, inventTaxID, inventTelNo, inventFaxNo, inventPercentGP, inventRemark, inventFlagUse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Inventory : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่มข้อมูล " + inventName + ">> (Success)");

                            allInventory = gd.getAllInventory(0, "0", "0","0");
                            dataTableInvent = gd.getAllInvenData(0, "0", "0", "0");
                            dataGridViewAllData.DataSource = dataTableInvent;
                            getComboAllInvent();
                            clearForm();

                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("คุณต้องการแก้ไขข้อมูล " + inventName + " หรือไม่ ?", "แก้ไข " + inventName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                       // int result = gd.updateInven(invenid, inventName, inventCode, inventCompanyName, inventFlagSupplier, inventAddr, inventTaxID, inventTelNo, inventFaxNo, inventPercentGP, inventRemark, inventFlagUse);

                        int result = gd.updateInven(invenid, inventName,inventFlagSupplier,inventCode,inventCompanyName,inventAddr, inventTaxID, inventTelNo, inventFaxNo, inventPercentGP, inventRemark, inventFlagUse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Inventory : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("แก้ไขข้อมูล " + inventName + ">> (Success)");

                            allInventory = gd.getAllInventory(0, "0", "0", "0");
                            dataTableInvent = gd.getAllInvenData(0, "0", "0", "0");
                            dataGridViewAllData.DataSource = dataTableInvent;
                            getComboAllInvent();
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

  

        private void button1_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void clearForm()
        {
            txtBoxInvenName.Text = "";
            txtBoxInvenAddr.Text = "";
            txtBoxInvenTaxID.Text = "";
            txtBoxInvenTelNo.Text = "";
            txtBoxRemark.Text = "";
            comboBoxFlagSupplier.Text = "Y";
            comboBoxFlagUse.Text = "Y";

            txtBoxInvenCode.Text = "";
            txtBoxInvenCompanyName.Text = "";
            txtBoxInvenFaxNo.Text = "";
            txtBoxPercentGP.Text = "0";



        }

        private void ChangeUpdateFlag(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked == true)
            {
                comboBoxAllInven.Visible = false;
                labelHeader.Text = "เพิ่มข้อมูล";
                buttonAddData.Text = "เพิ่มข้อมูล";
            }
            else
            {
                comboBoxAllInven.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูล";
                buttonAddData.Text = "แก้ไขข้อมูล";
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
 

  

        private void CommboxAllData_Change(object sender, EventArgs e)
        {
            try
            {

                int inventID = Int32.Parse(comboBoxAllInven.SelectedValue.ToString());

                foreach (Inventory c in allInventory)
                {
                    if (c.InventoryID == inventID)
                    {
                        txtBoxInvenName.Text = c.InventoryName;
                        txtBoxInvenAddr.Text = c.SuplierAddr;
                        txtBoxInvenTaxID.Text = c.SuplierTaxID;
                        txtBoxInvenTelNo.Text = c.SuplierTelNo;
                        txtBoxRemark.Text = c.Remark;
                        comboBoxFlagSupplier.Text = c.FlagSuplier;
                        comboBoxFlagUse.Text = c.FlagUse;


                        txtBoxInvenCode.Text = c.SuplierCode;
                        txtBoxInvenCompanyName.Text = c.SuplierCompanyName;
                        txtBoxInvenFaxNo.Text = c.SuplierFaxNo;
                        txtBoxPercentGP.Text = c.SuplierPercentGP.ToString();
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridViewAllData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridInventID = Int32.Parse(dataGridViewAllData.Rows[e.RowIndex].Cells["InventoryID"].Value.ToString());

                comboBoxAllInven.SelectedValue = dataGridInventID;

                comboBoxAllInven.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูล";
                buttonAddData.Text = "แก้ไขข้อมูล";
                radioButtonUpdateData.Checked = true;

            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxSRInventName_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string inventName = textBoxSRInventName.Text;
                string flagSupplier = comboBoxSRFlagSupplier.Text;


                if (inventName.Length > 0 && flagSupplier != "All")
                {
                    this.dataTableInvent.DefaultView.RowFilter = string.Format("InventoryName like '*{0}*' and FlagSuplier = '{1}' ", inventName, flagSupplier);
                   // MessageBox.Show("1");
                }
                else if (inventName.Length > 0 && flagSupplier == "All")
                {
                    this.dataTableInvent.DefaultView.RowFilter = string.Format("InventoryName like '*{0}*'  ", inventName);
                  //  MessageBox.Show("2");
                }
                else if (inventName.Length == 0 && flagSupplier != "All")
                {
                    this.dataTableInvent.DefaultView.RowFilter = string.Format("FlagSuplier = '{0}' ", flagSupplier);
                 //   MessageBox.Show("3");
                }
                else
                {
                    this.dataTableInvent.DefaultView.RowFilter = string.Format(" 1 = 1 ", inventName);
                  //  MessageBox.Show("4");
                }


            }
            catch (Exception ex)
            {

            }
        }

   

      
    }
    
}
