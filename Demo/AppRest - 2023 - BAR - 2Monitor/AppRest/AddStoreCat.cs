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
    public partial class AddStoreCat : AddDataTemplate
    {
        GetDataRest gd;
        MainManage formMainManage;


      //  List<GroupCat> allGroupCats;
        List<StoreCat> allStoreCats;

        int storeCatID;


        public AddStoreCat(Form frmlkFrom, int flagFrmClose)
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

            allStoreCats = gd.getListAllStoreCat(); 
            dataGridViewAllMember.DataSource = allStoreCats;


            getComboAllCat();

        
        }


        //private void getComboAllGroupCat()
        //{
        //    List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

        //    // Add data to the List
             

        //    foreach (GroupCat c in allGroupCats)
        //    {
        //        data.Add(new KeyValuePair<int, string>(c.GroupCatID, c.GroupCatName));
        //    }


        //    // Clear the combobox
        //    comboBoxAllGroupCat.DataSource = null;
        //    comboBoxAllGroupCat.Items.Clear();

        //    // Bind the combobox
        //    comboBoxAllGroupCat.DataSource = new BindingSource(data, null);
        //    comboBoxAllGroupCat.DisplayMember = "Value";
        //    comboBoxAllGroupCat.ValueMember = "Key";

        //    comboBoxAllGroupCat.SelectedValue = selectedGruopCat ;
        //    getComboAllCat();

        //}

        private void buttonAddTable_Click(object sender, EventArgs e)
        {

            try
            {
                
                string StorecatName = txtBoxNameTH.Text;
                string storeCatDesc = txtBoxDesc.Text;  

              //  int storeCatID = Int32.Parse(comboBoxAllCat.SelectedValue.ToString()); 


                if (radioButtonAddData.Checked == true)
                {

                    if (MessageBox.Show("คุณต้องการเพิ่มเมนูชื่อ " + StorecatName + " หรือไม่ ?", "เพิ่ม " + StorecatName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.instNewStoreCat(StorecatName,storeCatDesc);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Product : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่มเมนูชื่อ " + StorecatName + ">> (Success)");

                            allStoreCats = gd.getListAllStoreCat();
                            dataGridViewAllMember.DataSource = allStoreCats;
                            getComboAllCat();
                            clearForm();

                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("คุณต้องการแก้ไขเมนูชื่อ " + StorecatName + " หรือไม่ ?", "เพิ่ม " + StorecatName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updsStoreCat(this.storeCatID, StorecatName, storeCatDesc);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Product : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("แก้ไข : " + StorecatName + ">> (Success)");
                            allStoreCats = gd.getListAllStoreCat();
                            dataGridViewAllMember.DataSource = allStoreCats; 
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

            foreach (StoreCat c in allStoreCats)
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

        }


        private void button1_Click(object sender, EventArgs e)
        {
            txtBoxNameTH.Text = ""; 
            txtBoxDesc.Text = "";   
        }

        private void clearForm()
        {
            txtBoxNameTH.Text = ""; 
            txtBoxDesc.Text = "";   
            radioButtonAddData.Checked = true;
        }

        private void ChangeUpdateFlag(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked == true)
            {
                comboBoxAllCat.Visible = false;
                labelHeader.Text = "เพิ่มเมนูหมวดอาหาร";
                buttonAddTable.Text = "เพิ่มข้อมูล";
            }
            else
            {
                comboBoxAllCat.Visible = true;
                labelHeader.Text = "แก้ไขหมวดอาหาร";
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

                this.storeCatID = Int32.Parse(comboBoxAllCat.SelectedValue.ToString());

                foreach (StoreCat c in allStoreCats)
                {
                    if (c.StoreCatID == storeCatID)
                    {
                        txtBoxNameTH.Text = c.StoreCatName; 
                        txtBoxDesc.Text = c.StoreCatDesc; 
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
    
}
