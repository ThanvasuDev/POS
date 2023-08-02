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
    public partial class AddCustomer : AddDataTemplate
    {
        GetDataRest gd;
        List<Customer> allCustomer;
        //List<string> allMemStatus;

        MainManage formMainManage;

        //List<Tbl> allTableByZone;

        //List<Cat> allCatByGruopCats;

        DataTable dataTableAllCust;

        


        public AddCustomer(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();
            gd = new GetDataRest();

            this.Width = 1024;
            this.Height = 764;


            dataTableAllCust = gd.getDataAllCustomer();
            dataGridViewAllMember.DataSource = dataTableAllCust;

            comboBoxAllMember.Visible = false;

            allCustomer = gd.getListAllCustomer();  

            getComboAllMember();
             
            txtBoxTitle.Text = "บริษัท";  

            if(Login.userStatus == "Admin")
                comboBoxStatus.Visible = true;
             

        }


        private void getComboAllMember()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(-1, "= เลือกแก้ไขลูกค้า="));

            foreach (Customer c in allCustomer)
            {
                data.Add(new KeyValuePair<int, string>(c.CustID, c.Title + "" + c.Name));
            }


            // Clear the combobox
            comboBoxAllMember.DataSource = null;
            comboBoxAllMember.Items.Clear();

            // Bind the combobox
            comboBoxAllMember.DataSource = new BindingSource(data, null);
            comboBoxAllMember.DisplayMember = "Value";
            comboBoxAllMember.ValueMember = "Key";

        }

         

        private void button1_Click(object sender, EventArgs e)
        {
            txtBoxCustCode.Text = "";
            txtBoxTAXID.Text = "";
            txtBoxTitle.Text = "";
            txtBoxName.Text = "";
            txtBoxTel.Text = "";
            txtBoxAddress.Text = ""; 
            comboBoxStatus.Text = "Customer";
            comboBoxFlagUse.Text = "Y";
        }

        private void clearForm()
        {
            txtBoxCustCode.Text = "";
            txtBoxTAXID.Text = "";
            txtBoxTitle.Text = "";
            txtBoxName.Text = "";
            txtBoxTel.Text = "";
            txtBoxAddress.Text = ""; 
            comboBoxStatus.Text = "Customer";
            comboBoxFlagUse.Text = "Y";
            radioButtonAddData.Checked = true;
        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {

            try
            {
                string custCode = txtBoxCustCode.Text;
                string taxID = txtBoxTAXID.Text;
                string title = txtBoxTitle.Text;
                string name = txtBoxName.Text;
                string tel = txtBoxTel.Text;
                string address = txtBoxAddress.Text; 
                string status = comboBoxStatus.Text;
                string flagUse = comboBoxFlagUse.Text; 

                int custID = Int32.Parse(comboBoxAllMember.SelectedValue.ToString());


                if (radioButtonAddData.Checked == true)
                {

                    if (MessageBox.Show("คุณต้องการจะเพิ่ม  ชื่อ " + title + name + " หรือไม่ ?", "เพิ่ม " + status, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.instNewCustomer(custCode, taxID, title, name, tel, address, status, flagUse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Member : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่ม : ชื่อ " + title + name + " >> (Success)");
                            allCustomer = gd.getListAllCustomer();
                          //  dataGridViewAllMember.DataSource = allCustomer;

                            dataTableAllCust = gd.getDataAllCustomer();
                            dataGridViewAllMember.DataSource = dataTableAllCust;

                            getComboAllMember();
                            clearForm();
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("คุณต้องการจะแก้ไข : ชื่อ " + title + name + " หรือไม่ ?", "แก้ไข " + status, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                          int result = gd.updsCustomer(custID, custCode, taxID, title, name, tel, address, status, flagUse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Customer : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่ม : ชื่อ " + title + name + " >> (Success)");
                            allCustomer = gd.getListAllCustomer();
                          //  dataGridViewAllMember.DataSource = allCustomer;

                            dataTableAllCust = gd.getDataAllCustomer();
                            dataGridViewAllMember.DataSource = dataTableAllCust;

                            getComboAllMember();
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

        private void ChangeUpdateFlag(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked == true)
            {
                comboBoxAllMember.Visible = false;
                labelHeader.Text = "เพิ่มข้อมูลลูกค้า" ;
                buttonAddTable.Text = "เพิ่มข้อมูล";
            }
            else
            {
                comboBoxAllMember.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูลลูกค้า";
                buttonAddTable.Text = "แก้ไขข้อมูล";
            }
        }

        private void CommboxAllProduct_Change(object sender, EventArgs e)
        {
            try
            {

                int userID = Int32.Parse(comboBoxAllMember.SelectedValue.ToString());

                foreach (Customer c in allCustomer)
                {
                    if (c.CustID == userID)
                    {
                        txtBoxCustCode.Text = c.CustCode;
                        txtBoxTAXID.Text = c.TaxID;
                        txtBoxTitle.Text = c.Title;
                        txtBoxName.Text = c.Name;
                        txtBoxTel.Text = c.Tel;
                        txtBoxAddress.Text = c.Address;
                        comboBoxStatus.Text = c.Status;
                        comboBoxFlagUse.Text = c.FlagUse;
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

                int dataGridproductID = Int32.Parse(dataGridViewAllMember.Rows[e.RowIndex].Cells["CustID"].Value.ToString());

                comboBoxAllMember.SelectedValue = dataGridproductID;

                comboBoxAllMember.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูล";
                buttonAddTable.Text = "แก้ไขข้อมูล";
                radioButtonUpdateData.Checked = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void searchMemCard()
        {

            try
            {

                string srMemName = textBoxSRMemName.Text;
                string strSearchMemCard = textBoxStrSearchMemCardtoTable.Text;
                string srMemTel = textBoxSRTel.Text;
                string srLevelName = comboBoxSRLevel.Text;

                if (comboBoxSRLevel.SelectedIndex == 0)
                {

                    if (srMemName.Length > 0)
                    {
                        dataTableAllCust.DefaultView.RowFilter = string.Format("Name like '*{0}*' ", srMemName);

                    }
                    else
                    {
                        if (strSearchMemCard.Length > 0)
                            this.dataTableAllCust.DefaultView.RowFilter = string.Format("TaxID = '{0}' ", strSearchMemCard);
                        else if (srMemTel.Length > 0)
                            this.dataTableAllCust.DefaultView.RowFilter = string.Format("Tel = '{0}' ", srMemTel);
                        else
                            this.dataTableAllCust.DefaultView.RowFilter = string.Format(" 1 = 1 ");

                    }
                }
                else
                {
                    if (srMemName.Length > 0)
                    {
                        dataTableAllCust.DefaultView.RowFilter = string.Format("Name like '*{0}*' and Status =  '{1}' ", srMemName, srLevelName);

                    }
                    else
                    {
                        if (strSearchMemCard.Length > 0)
                            this.dataTableAllCust.DefaultView.RowFilter = string.Format("TaxID = '{0}'  and Status =  '{1}' ", strSearchMemCard, srLevelName);
                        else if (srMemTel.Length > 0)
                            this.dataTableAllCust.DefaultView.RowFilter = string.Format("Tel = '{0}'  and Status =  '{1}' ", srMemTel, srLevelName);
                        else
                            this.dataTableAllCust.DefaultView.RowFilter = string.Format(" Status =  '{0}' ", srLevelName);

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxStrSearchMemCardtoTable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                searchMemCard();
            }
        }

        private void textBoxSRMemName_TextChanged(object sender, EventArgs e)
        {
            searchMemCard();
        }

        private void textBoxSRTel_TextChanged(object sender, EventArgs e)
        {
            searchMemCard();
        }

        private void comboBoxStatusSRC_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchMemCard();
        }
         

    }
}
