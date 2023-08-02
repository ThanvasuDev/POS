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
    public partial class AddMember : AddDataTemplate
    {
        GetDataRest gd;
        List<Member> allMembers;
        DataTable dataTableAllMember;

        MainManage formMainManage;

        List<Tbl> allTableByZone;

        List<Cat> allCatByGruopCats;

        


        public AddMember(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();
            gd = new GetDataRest();

            comboBoxAllMember.Visible = false;

            allMembers = gd.getListAllUser();
         //   dataGridViewAllMember.DataSource = allMembers;

            dataTableAllMember = gd.getDataAllMember(); 
            dataGridViewAllMember.DataSource = dataTableAllMember;

            //allMemStatus = gd.getListAllMemStatus();
            //comboBoxStatus.DataSource = allMemStatus;

            getComboAllMember();


            allTableByZone = gd.getTableByZone(10); // Zone Food Park
            allCatByGruopCats = gd.getCatByGroupCat(10); // Cat Food Park


            getComboAllFoodPark();
            getComboAllCateogy();

        }


        private void getComboAllMember()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(-1, "= เลือกแก้ไขสมาชิก ="));

            foreach (Member c in allMembers)
            {
                data.Add(new KeyValuePair<int, string>(c.UserID, c.UserName));
            }


            // Clear the combobox
            comboBoxAllMember.DataSource = null;
            comboBoxAllMember.Items.Clear();

            // Bind the combobox
            comboBoxAllMember.DataSource = new BindingSource(data, null);
            comboBoxAllMember.DisplayMember = "Value";
            comboBoxAllMember.ValueMember = "Key";

        }


        private void getComboAllFoodPark()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "= Use All Food Park ="));

            foreach (Tbl c in allTableByZone)
            {
                data.Add(new KeyValuePair<int, string>(c.TblID, c.TblName));
            }


            // Clear the combobox
            ComboBoxTable.DataSource = null;
            ComboBoxTable.Items.Clear();

            // Bind the combobox
            ComboBoxTable.DataSource = new BindingSource(data, null);
            ComboBoxTable.DisplayMember = "Value";
            ComboBoxTable.ValueMember = "Key";

        }


        private void getComboAllCateogy()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "= Use All Category ="));

            foreach (Cat c in allCatByGruopCats)
            {
                data.Add(new KeyValuePair<int, string>(c.CatID, c.CatName));
            }


            // Clear the combobox
            comboBoxCategory.DataSource = null;
            comboBoxCategory.Items.Clear();

            // Bind the combobox
            comboBoxCategory.DataSource = new BindingSource(data, null);
            comboBoxCategory.DisplayMember = "Value";
            comboBoxCategory.ValueMember = "Key";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtBoxUserName.Text = "";
            txtBoxPassword.Text = "";
            txtBoxName.Text = "";
            txtBoxPhone.Text = "";
            txtBoxAddress.Text = "";
            //ComboBoxTable.SelectedIndex = 0;
            //comboBoxCategory.SelectedIndex = 0;
            comboBoxStatus.Text = "Cashier";
            comboBoxFlagUse.Text = "Y";
        }

        private void clearForm()
        {
            txtBoxUserName.Text = "";
            txtBoxPassword.Text = "";
            txtBoxName.Text = "";
            txtBoxPhone.Text = "";
            txtBoxAddress.Text = "";
            //ComboBoxTable.SelectedIndex = 0;
            //comboBoxCategory.SelectedIndex = 0;
            comboBoxStatus.Text = "Cashier";
            comboBoxFlagUse.Text = "Y";
            radioButtonAddData.Checked = true;
        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {

            try
            {
                string userName = txtBoxUserName.Text;
                string password = txtBoxPassword.Text;
                string name = txtBoxName.Text;
                string tel = txtBoxPhone.Text;
                string address = txtBoxAddress.Text;
                int workRate = 0;
                string status = comboBoxStatus.Text;
                string flagUse = comboBoxFlagUse.Text;
                int workShift = 0;

                int userID = Int32.Parse(comboBoxAllMember.SelectedValue.ToString());


                if (radioButtonAddData.Checked == true)
                {

                    if (MessageBox.Show("คุณต้องการจะเพิ่ม : " + status + " : ชื่อ " + name + " หรือไม่ ?", "เพิ่ม " + status, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.instNewMember(userName, password, name, tel, address, status, workRate, flagUse, workShift);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Member : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่ม : " + status + " : ชื่อ " + name + " >> (Success)");
                            allMembers = gd.getListAllUser();
                            dataTableAllMember = gd.getDataAllMember();
                            dataGridViewAllMember.DataSource = dataTableAllMember;

                            getComboAllMember();
                            clearForm();
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("คุณต้องการจะแก้ไข : " + status + " : ชื่อ " + name + " หรือไม่ ?", "แก้ไข " + status, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updsMember(userID, userName, password, name, tel, address, status, workRate, flagUse, workShift);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Member : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("แก้ไข : " + status + " : ชื่อ " + name + " >> (Success)");
                            allMembers = gd.getListAllUser();
                            dataTableAllMember = gd.getDataAllMember();
                            dataGridViewAllMember.DataSource = dataTableAllMember;
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
                labelHeader.Text = "เพิ่มข้อมูลสมาชิก" ;
                buttonAddTable.Text = "เพิ่มข้อมูล";
            }
            else
            {
                comboBoxAllMember.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูลสมาชิก";
                buttonAddTable.Text = "แก้ไขข้อมูล";
            }
        }

        private void CommboxAllProduct_Change(object sender, EventArgs e)
        {
            try
            {

                int userID = Int32.Parse(comboBoxAllMember.SelectedValue.ToString());

                foreach (Member c in allMembers)
                {
                    if (c.UserID == userID)
                    {
                        txtBoxUserName.Text = c.UserName;
                        txtBoxPassword.Text = c.Password;
                        txtBoxName.Text = c.Name;
                        txtBoxPhone.Text = c.Tel;
                        txtBoxAddress.Text = c.Address;
                        ComboBoxTable.SelectedValue = c.WorkRate; // Table
                        comboBoxCategory.SelectedValue = c.WorkShift; // Category
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

                int dataGridproductID = Int32.Parse(dataGridViewAllMember.Rows[e.RowIndex].Cells["UserID"].Value.ToString());

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
                        dataTableAllMember.DefaultView.RowFilter = string.Format("Name like '*{0}*' ", srMemName);

                    }
                    else
                    {
                        if (strSearchMemCard.Length > 0)
                            this.dataTableAllMember.DefaultView.RowFilter = string.Format("UserName = '{0}' ", strSearchMemCard);
                        else if (srMemTel.Length > 0)
                            this.dataTableAllMember.DefaultView.RowFilter = string.Format("Tel = '{0}' ", srMemTel);
                        else
                            this.dataTableAllMember.DefaultView.RowFilter = string.Format(" 1 = 1 ");

                    }
                }
                else
                {
                    if (srMemName.Length > 0)
                    {
                        dataTableAllMember.DefaultView.RowFilter = string.Format("Name like '*{0}*' and Status =  '{1}' ", srMemName, srLevelName);

                    }
                    else
                    {
                        if (strSearchMemCard.Length > 0)
                            this.dataTableAllMember.DefaultView.RowFilter = string.Format("UserName = '{0}'  and Status =  '{1}' ", strSearchMemCard, srLevelName);
                        else if (srMemTel.Length > 0)
                            this.dataTableAllMember.DefaultView.RowFilter = string.Format("Tel = '{0}'  and Status =  '{1}' ", srMemTel, srLevelName);
                        else
                            this.dataTableAllMember.DefaultView.RowFilter = string.Format(" Status =  '{0}' ", srLevelName);

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
         
        private void comboBoxSRLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchMemCard();
        }

         

    }
}
