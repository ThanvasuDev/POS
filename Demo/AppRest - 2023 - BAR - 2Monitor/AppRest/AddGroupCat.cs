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
    public partial class AddGroupCat : AddDataTemplate
    {
        GetDataRest gd;
        MainManage formMainManage;

        List<DiscountGroup> allDiscountGroups; 
        List<GroupCat> allGroupCats; 

        int selectedGruopCat;  

        public AddGroupCat(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();

            this.Height = 764;
            this.Width = 1024;
             
            gd = new GetDataRest();

            comboBoxAllGroupCat.Visible = false;

            selectedGruopCat = 1;

            allGroupCats = gd.getAllGroupCat(0);
            dataGridViewAllData.DataSource = allGroupCats; 
            getComboAllGroupCat();


            allDiscountGroups = gd.getAllDiscountGroup();

             clearForm();
             getComboAllDiscountGroup();
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

        }


        private void getComboAllDiscountGroup()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(0, "= Select Discount Group ="));

            foreach (DiscountGroup dg in allDiscountGroups)
            { 
                data.Add(new KeyValuePair<int, string>(dg.DiscountGroupID, dg.DiscountGroupName)); 
            } 
 
            data.Add(new KeyValuePair<int, string>(99, "= Delete Group Category ="));


            // Clear the combobox
            comboBoxDiscountGroup.DataSource = null;
            comboBoxDiscountGroup.Items.Clear();

            // Bind the combobox
            comboBoxDiscountGroup.DataSource = new BindingSource(data, null);
            comboBoxDiscountGroup.DisplayMember = "Value";
            comboBoxDiscountGroup.ValueMember = "Key";

          //  comboBoxAllGroupCat.SelectedValue = selectedGruopCat;
          

        }
         
        private void buttonAddTable_Click(object sender, EventArgs e)
        {

            try
            {
               
                string groupCatNameTH = txtBoxNameTH.Text;
                string groupCatNameEN = txtBoxNameEN.Text;

                int discountgroupID = Int32.Parse(comboBoxDiscountGroup.SelectedValue.ToString());

                string groupCatColour = txtBoxColour.Text;
                int groupCatSort = Int32.Parse(txtBoxSortNo.Text);
                string groupCatFlagUse = comboBoxFlagUse.Text;

                if (radioButtonAddData.Checked == true)
                {

                    if (MessageBox.Show("คุณต้องการเพิ่มกลุ่มสินค้า " + groupCatNameTH + " หรือไม่ ?", "เพิ่ม " + groupCatNameTH, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.instNewGroupCat(groupCatNameTH, groupCatNameTH, groupCatNameEN, discountgroupID, groupCatColour, groupCatSort, groupCatFlagUse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Group Cat : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่มกลุ่มสินค้า " + groupCatNameTH + ">> (Success)");

                            allGroupCats = gd.getAllGroupCat(0);
                            dataGridViewAllData.DataSource = allGroupCats;
                            getComboAllGroupCat();
                            clearForm();

                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("คุณต้องการแก้ไขกลุ่มสินค้า " + groupCatNameTH + " หรือไม่ ?", "เพิ่ม " + groupCatNameTH, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updsGroupCat(selectedGruopCat, groupCatNameTH, groupCatNameTH, groupCatNameEN, discountgroupID , groupCatColour, groupCatSort, groupCatFlagUse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Group Cat : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("แก้ไขหมวดสินค้า : " + groupCatNameTH + ">> (Success)");
                            allGroupCats = gd.getAllGroupCat(0);
                            dataGridViewAllData.DataSource = allGroupCats;
                            getComboAllGroupCat();
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
            txtBoxNameTH.Text = "";
            txtBoxNameEN.Text = "";  
            comboBoxDiscountGroup.SelectedValue = 0; 
            txtBoxColour.Text = "255,255,255|B";
            txtBoxSortNo.Text = "99";
            comboBoxFlagUse.Text  = "Y"; 

            radioButtonAddData.Checked = true;
            
        }

        private void ChangeUpdateFlag(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked == true)
            {
                comboBoxAllGroupCat.Visible = false;
                labelHeader.Text = "เพิ่มเหมวดสินค้า";
                buttonAddTable.Text = "เพิ่มข้อมูล";
            }
            else
            {
                comboBoxAllGroupCat.Visible = true;
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
                allGroupCats = gd.getAllGroupCat(0);
                dataGridViewAllData.DataSource = allGroupCats;
                getComboAllGroupCat();
            }
            catch (Exception ex)
            {

            }
        }

        private void CommboxAllCat_Change(object sender, EventArgs e)
        {
            try
            {

                selectedGruopCat = Int32.Parse(comboBoxAllGroupCat.SelectedValue.ToString());

                foreach (GroupCat c in allGroupCats)
                {
                    if (c.GroupCatID == selectedGruopCat)
                    {
                        txtBoxNameTH.Text = c.GroupCatNameTH;
                        txtBoxNameEN.Text = c.GroupCatNameEN;  
                        comboBoxDiscountGroup.SelectedValue = c.DisCountGroupID;
                        txtBoxColour.Text = c.GroupCatColour;
                        txtBoxSortNo.Text = c.GroupCatSort.ToString();
                        comboBoxFlagUse.Text = c.GroupCatFlagUse;
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

                int dataGridCatID = Int32.Parse(dataGridViewAllData.Rows[e.RowIndex].Cells["GroupCatID"].Value.ToString());

                comboBoxAllGroupCat.SelectedValue = dataGridCatID;

                comboBoxAllGroupCat.Visible = true;
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
            if( radioButtonAddData.Checked )
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
