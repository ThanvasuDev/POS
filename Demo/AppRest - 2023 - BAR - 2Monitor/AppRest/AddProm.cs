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
    public partial class AddProm : AddDataTemplate
    {
        GetDataRest gd;
        MainManage formMainManage;


        List<GroupCat> allGroupCats;  
        List<ProductGroup> allProductGroup;  
        List<Cat> allCats;  
        DataTable dataAllProductPT;

        // TAB 2 PROMOTION
        List<Prom> allProm;
        List<PromDetail> allPromDetail;  

        // DATA Search
        DataTable allProm_DATA;
        DataTable allProductGroup_DATA;

        public AddProm(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();

            this.Width = 1024;
            this.Height = 764;

            gd = new GetDataRest();

            try
            {

                //comboBoxAllCat.Visible = false;

                //selectedGruopCat = 1;




                dataAllProductPT = gd.getAllProduct(0);
                dataGridViewAllProduct.DataSource = dataAllProductPT;


                dataGridViewAllProduct.Columns[2].Visible = false;
                dataGridViewAllProduct.Columns[3].Visible = false;
                dataGridViewAllProduct.Columns[5].Visible = false;
                dataGridViewAllProduct.Columns[6].Visible = false;
                dataGridViewAllProduct.Columns[7].Visible = false;
            
                dataGridViewAllProduct.Columns[9].Visible = false;
                dataGridViewAllProduct.Columns[10].Visible = false;
                dataGridViewAllProduct.Columns[11].Visible = false;
                dataGridViewAllProduct.Columns[12].Visible = false;
                dataGridViewAllProduct.Columns[13].Visible = false;
                dataGridViewAllProduct.Columns[14].Visible = false;

                allCats = gd.getOrderCat(2);
                dataGridViewAllCat.DataSource = allCats;

                dataGridViewAllCat.Columns[2].Visible = false;
                dataGridViewAllCat.Columns[4].Visible = false;
                dataGridViewAllCat.Columns[5].Visible = false;
                dataGridViewAllCat.Columns[6].Visible = false;
                dataGridViewAllCat.Columns[7].Visible = false;
                dataGridViewAllCat.Columns[8].Visible = false;


                allGroupCats = gd.getAllGroupCat(1);
                dataGridViewAllGroupCat.DataSource = allGroupCats;

                //////

                allProductGroup = gd.getListProductGroup(0);
                allProductGroup_DATA = gd.getListProductGroup_DATA(0);

                dataGridViewResult.DataSource = allProductGroup_DATA;
                dataGridViewResult.Columns[1].Visible = false;


                getComboAllProductGroup();
                clearForm();

                /// TAB 2 PROMOTION

                dateTimePickerStartDate.Value = DateTime.Now;
                dateTimePickerEndDate.Value = DateTime.Now;

                allProm = gd.getListProm(0);
                allProm_DATA = gd.getListProm_DATA(0);

                dataGridViewAllProm.DataSource = allProm_DATA;
                getComboAllProm();

                getComboAllProductGroupPD();
                clearPDAddData();
                clearPTAddData();

            }
            catch (Exception ex) { }
        }    


        private void getComboAllProductGroup()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List
            data.Add(new KeyValuePair<int, string>(-1, "== เลือกกลุ่มสินค้า =="));

            foreach (ProductGroup c in allProductGroup)
            {
                data.Add(new KeyValuePair<int, string>(c.ProGroupID, c.ProgroupName));
            }


            // Clear the combobox
            comboBoxProductGroup.DataSource = null;
            comboBoxProductGroup.Items.Clear();

            // Bind the combobox
            comboBoxProductGroup.DataSource = new BindingSource(data, null);
            comboBoxProductGroup.DisplayMember = "Value";
            comboBoxProductGroup.ValueMember = "Key";

            comboBoxProductGroup.SelectedIndex = 0; 

        }




        private void buttonAddData_Click(object sender, EventArgs e)
        {

            try
            {

                string sProductGroupCode = txtBoxGroupCode.Text;
                string sProgroupName = txtBoxGroupName.Text ;
                string sProgroupGroupCatID = txtBoxPTGroupCatID.Text; 
                string sProgroupCatID =  txtBoxPTCatID.Text ;
                string sProgroupProductID  = textBoxPTProID.Text;
                string sProgroupFlaguse = comboBoxFlagUse.Text;
                  

                if (radioButtonAddData.Checked == true)
                {

                    if (MessageBox.Show("คุณต้องการเพิ่มข้อมูล " + sProgroupName + " หรือไม่ ?", "เพิ่ม " + sProgroupName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.insertNewGroupProduct(sProductGroupCode, sProgroupName, sProgroupGroupCatID, sProgroupCatID, sProgroupProductID, sProgroupFlaguse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert Product Group  : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่มข้อมุลกลุ่ม " + sProgroupName + ">> (Success)");
                            allProductGroup = gd.getListProductGroup(0);
                            dataGridViewResult.DataSource = allProductGroup;
                            getComboAllProductGroup();
                            getComboAllProductGroupPD();
                            clearForm();

                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("คุณต้องการแก้ไข " + sProgroupName + " หรือไม่ ?", "แก้ไข  " + sProgroupName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int sProductGroupID = Int32.Parse(comboBoxProductGroup.SelectedValue.ToString());
               
                        int result = gd.updateGroupProduct(sProductGroupID,sProductGroupCode, sProgroupName, sProgroupGroupCatID, sProgroupCatID, sProgroupProductID, sProgroupFlaguse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Product Group : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("แก้ไขข้อมูล : " + sProgroupName + ">> (Success)");
                            allProductGroup = gd.getListProductGroup(0);
                            dataGridViewResult.DataSource = allProductGroup;
                            getComboAllProductGroup();
                            getComboAllProductGroupPD();
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

      


        private void buttonClear_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void clearForm()
        {
            txtBoxGroupCode.Text = "G000";
            txtBoxGroupName.Text = "";
            txtBoxPTCatID.Text = "0";
            txtBoxPTGroupCatID.Text = "0";
            textBoxPTProID.Text = "0";
            radioButtonAddData.Checked = true;

        }

        private void ChangeUpdateFlag(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked == true)
            {
                comboBoxProductGroup.Visible = false;
                labelHeader.Text = "เพิ่มเมนูข้อมูล";
                buttonAddTable.Text = "เพิ่มข้อมูล";
            }
            else
            {
                comboBoxProductGroup.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูล";
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
         

        private void CommboxAllGroupProduct_Change(object sender, EventArgs e)
        {
            try
            {

                int dataProductGroupID = Int32.Parse(comboBoxProductGroup.SelectedValue.ToString());

                foreach (ProductGroup c in allProductGroup)
                {
                    if (c.ProGroupID == dataProductGroupID)
                    {
                        txtBoxGroupCode.Text = c.ProductGroupCode;
                        txtBoxGroupName.Text = c.ProgroupName;
                        txtBoxPTCatID.Text = c.ProgroupCatID;
                        txtBoxPTGroupCatID.Text = c.ProgroupGroupCatID;
                        textBoxPTProID.Text = c.ProgroupProductID;
                        comboBoxFlagUse.Text = c.ProgroupFlaguse;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridViewResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataProductGroupID = Int32.Parse(dataGridViewResult.Rows[e.RowIndex].Cells["ProGroupID"].Value.ToString());

                comboBoxProductGroup.SelectedValue = dataProductGroupID;

                comboBoxProductGroup.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูล";
                buttonAddTable.Text = "แก้ไขข้อมูล";
                radioButtonUpdateData.Checked = true;

            }
            catch (Exception ex)
            {

            }
        }

        private void txtBoxNameTH_TextChanged(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked == true)
            {
                txtBoxGroupName.Text = txtBoxGroupCode.Text;
            }
        } 


        private void buttonPTCatClose_Click(object sender, EventArgs e)
        {

            strProductID = "";
            selected = new List<String>();

            string val = "";
     

            try
            {

                foreach (DataGridViewRow row in dataGridViewAllCat.Rows)
                {
                    val = row.Cells["Check"].Value.ToString();

                    if (val == "1")
                    {
                        selected.Add(row.Cells["CatID"].Value.ToString());
                    }
                }

                int i = 1;

                foreach (string str in selected)
                {

                    strProductID += str;
                    i++;

                    if (i <= selected.Count)
                        strProductID += ",";

                }

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }
            finally
            {

                txtBoxPTCatID.Text = strProductID;
                panelCatProm.Visible = false;
            }
        }

        private void buttonClosePannelCatPro_Click(object sender, EventArgs e)
        {
            panelCatProm.Visible = false;
        }


        private void buttonSelectCat_Click(object sender, EventArgs e)
        {
            try
            {
                panelCatProm.Visible = true;

                string[] strProducts = null;

                if (txtBoxPTCatID.Text.Length > 1 || txtBoxPTCatID.Text != "0")
                    strProducts = txtBoxPTCatID.Text.Split(',');


                foreach (DataGridViewRow item in dataGridViewAllCat.Rows)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)item.Cells[0];

                    cell.Value = 0;
                }
                 

                for (int i = 0; i < dataGridViewAllCat.Rows.Count - 1; i++)
                {
                    DataGridViewRow row = dataGridViewAllCat.Rows[i];

                    foreach (string strIndex in strProducts)
                        if ((row.Cells["CatID"]).Value.ToString() == strIndex)
                            dataGridViewAllCat.Rows[i].Cells["Check"].Value = 1;

                }

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }
            finally
            {

            }

        }

        private void buttonPTCatIgnore_Click(object sender, EventArgs e)
        {
            txtBoxPTCatID.Text = "0";
        }

        private void buttonOpenProductProm_Click(object sender, EventArgs e)
        {
             
            if (panelProductPromotion.Visible == true)
                panelProductPromotion.Visible = false;
            else
                panelProductPromotion.Visible = true;


            string[] strProducts = null;

            if (textBoxPTProID.Text.Length > 1 || textBoxPTProID.Text != "0")
                strProducts = textBoxPTProID.Text.Split(',');

      
            foreach (DataGridViewRow item in dataGridViewAllProduct.Rows)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)item.Cells[0];

                cell.Value = 0;
            }

            try
            {


                for (int i = 0; i < dataGridViewAllProduct.Rows.Count - 1; i++)
                {
                    DataGridViewRow row = dataGridViewAllProduct.Rows[i];

                    foreach (string strIndex in strProducts)
                        if ((row.Cells["ProductID"]).Value.ToString() == strIndex)
                            dataGridViewAllProduct.Rows[i].Cells["CheckBox"].Value = 1;

                }

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }
            finally
            {

            }

        
        }

        private void buttonPTProductIgnore_Click(object sender, EventArgs e)
        {
            textBoxPTProID.Text = "0";
        }

         string strProductID = "";
         List<String> selected;


        private void buttonCloseUpdate_Click(object sender, EventArgs e)
        {
             strProductID = "";
             selected = new List<String>();

             string val = "";

            try
            { 

                foreach (DataGridViewRow row in dataGridViewAllProduct.Rows)
                {
                    val = row.Cells["CheckBox"].Value.ToString();

                    if (val == "1")
                    {
                          selected.Add(row.Cells["ProductID"].Value.ToString()); 
                    }
                } 

                int i = 1;

                foreach (string str in selected)
                {

                    strProductID += str;
                    i++;

                    if (i <= selected.Count)
                        strProductID += ",";

                }

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }
            finally
            {

                textBoxPTProID.Text = strProductID;
                panelProductPromotion.Visible = false; 
            }
        }

        private void buttonPClose_Click(object sender, EventArgs e)
        {
            panelProductPromotion.Visible = false;
        }

        private void textBoxPSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string srPName = textBoxPSearch.Text;

                if (srPName.Length > 0)
                {
                    this.dataAllProductPT.DefaultView.RowFilter = string.Format("ProductName like '*{0}*' ", srPName);

                }
                else
                {
                    this.dataAllProductPT.DefaultView.RowFilter = string.Format("ProductID > 4 ");
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void buttonClosePTGroupCatUpdate_Click(object sender, EventArgs e)
        {
            strProductID = "";
            selected = new List<String>();

            string val = "";

            try
            {

                foreach (DataGridViewRow row in dataGridViewAllGroupCat.Rows)
                {
                    val = row.Cells["CheckG"].Value.ToString();

                    if (val == "1")
                    {
                        selected.Add(row.Cells["GroupCatID"].Value.ToString());
                    }
                }

                int i = 1;

                foreach (string str in selected)
                {

                    strProductID += str;
                    i++;

                    if (i <= selected.Count)
                        strProductID += ",";

                }

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }
            finally
            {

                txtBoxPTGroupCatID.Text = strProductID;
                panelGroupCatProm.Visible = false;
            }
        }

        private void buttonPTGroupCatIgnore_Click(object sender, EventArgs e)
        {
            txtBoxPTGroupCatID.Text = "0";
        }

        private void buttonOpenGroupCatProm_Click(object sender, EventArgs e)
        {
            try
            {
                panelGroupCatProm.Visible = true;

                string[] strProducts = null;

                if (txtBoxPTGroupCatID.Text.Length > 1 || txtBoxPTGroupCatID.Text != "0")
                    strProducts = txtBoxPTGroupCatID.Text.Split(',');


                foreach (DataGridViewRow item in dataGridViewAllGroupCat.Rows)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)item.Cells[0];

                    cell.Value = 0;
                }


                for (int i = 0; i < dataGridViewAllGroupCat.Rows.Count - 1; i++)
                {
                    DataGridViewRow row = dataGridViewAllGroupCat.Rows[i];

                    foreach (string strIndex in strProducts)
                        if ((row.Cells["GroupCatID"]).Value.ToString() == strIndex)
                            dataGridViewAllGroupCat.Rows[i].Cells["CheckG"].Value = 1;

                }

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }




        /// -------------------- TAB 2 PROMOTION   ---------------- ////





        private void buttonClosePTGroupCat_Click(object sender, EventArgs e)
        {
            panelGroupCatProm.Visible = false;
        }

        private void buttonStartToday_Click(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Value = DateTime.Now;
        }

        private void buttonEndToday_Click(object sender, EventArgs e)
        {
            dateTimePickerEndDate.Value = DateTime.Now;
        }

        private void buttonAllday1_Click(object sender, EventArgs e)
        {
            textBoxTime1.Text = "00:00-00:00";
        }

        private void buttonAllday2_Click(object sender, EventArgs e)
        {
            textBoxTime2.Text = "00:00-00:00";
        }

        private void buttonAllday3_Click(object sender, EventArgs e)
        {
            textBoxTime3.Text = "00:00-00:00";
        }

        private void clearPTAddData()
        {
            textBoxPTCode.Text = "0";
            textBoxPTName.Text = "0";
            textBoxPTAmounts.Text = "0";
            comboBoxPTType.Text = "BEFORE";
            comboBoxPTCountItems.Text = "0";
            dateTimePickerStartDate.Value = DateTime.Now;
            dateTimePickerEndDate.Value = DateTime.Now;
            textBoxTime1.Text = "00:00-00:00";
            textBoxTime2.Text = "00:00-00:00";
            textBoxTime3.Text = "00:00-00:00";
            comboBoxPTProductGroup.SelectedIndex = 0;

            textBoxPTQtyLimit.Text = "0";
            textBoxPTQtyBalance.Text = "0";
            promCheckBoxDayW("MON;TUE;WED;THU;FRI;SAT;SUN;");
          //  findStrCheckBocDayW();

        }

        private void promCheckBoxDayW_Checkout(string strDayW)
        {
            try
            {
                string[] dw = strDayW.Split(';');
                CheckBox tbx;
                string ch = "";

              

                foreach (string d in dw)
                {
                    if (d.Length == 3)
                    {
                        ch = "checkBoxDayW_" + d.ToUpper();
                        tbx = this.Controls.Find(ch, true).FirstOrDefault() as CheckBox;
                        tbx.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void promCheckBoxDayW(string strDayW)
        {
            try
            {
                string[] dw = strDayW.Split(';');
                CheckBox tbx;
                string ch = "";

                promCheckBoxDayW_Checkout("MON;TUE;WED;THU;FRI;SAT;SUN;");

                foreach (string d in dw)
                {
                    if (d.Length == 3)
                    {
                        ch = "checkBoxDayW_" + d.ToUpper();
                        tbx = this.Controls.Find(ch, true).FirstOrDefault() as CheckBox;
                        tbx.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private string findStrCheckBocDayW()
        {
            string result = "";
            try
            {
              
                string strDayW = "MON;TUE;WED;THU;FRI;SAT;SUN;";

                string[] dw = strDayW.Split(';');
                CheckBox tbx;
                string ch = "";

                foreach (string d in dw)
                {
                    if (d.Length == 3)
                    {
                        ch = "checkBoxDayW_" + d.ToUpper();
                        tbx = this.Controls.Find(ch, true).FirstOrDefault() as CheckBox;

                        if (tbx.Checked)
                            result += d.ToUpper() + ";";
                    }
                
                }
            }
            catch (Exception ex)
            {
                return result;
            }

            return result;
        }
       

        private void buttonPTClear_Click(object sender, EventArgs e)
        {
            clearPTAddData();
        }

        private void getComboAllProm()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List
            data.Add(new KeyValuePair<int, string>(-1, "== เลือก Promotion =="));

            foreach (Prom c in allProm)
            {
                data.Add(new KeyValuePair<int, string>(c.PromID, c.PromCode + "-" + c.PromName));
            }


            // Clear the combobox
            comboBoxListPromotion.DataSource = null;
            comboBoxListPromotion.Items.Clear();

            // Bind the combobox
            comboBoxListPromotion.DataSource = new BindingSource(data, null);
            comboBoxListPromotion.DisplayMember = "Value";
            comboBoxListPromotion.ValueMember = "Key";

            comboBoxListPromotion.SelectedIndex = 0;

        }

        private void ChangeUpdateFlagProm(object sender, EventArgs e)
        {
            if (radioButtonAddDataProm.Checked == true)
            {
                comboBoxListPromotion.Visible = false;
                labelHeaderPromotion.Text = "เพิ่มเมนูข้อมูล";
                buttonPTAddData.Text = "เพิ่มข้อมูล";
            }
            else
            {
                comboBoxListPromotion.Visible = true;
                labelHeaderPromotion.Text = "แก้ไขข้อมูล";
                buttonPTAddData.Text = "แก้ไขข้อมูล";
            }
             
        }


        private void CommboxAllListPromotion_Change(object sender, EventArgs e)
        {
            try
            {
                int dataID = Int32.Parse(comboBoxListPromotion.SelectedValue.ToString());

                foreach (Prom c in allProm)
                {
                    if (c.PromID == dataID)
                    {
                        textBoxPTCode.Text  = c.PromCode;
                        textBoxPTName.Text  = c.PromName;
                        comboBoxPTType.Text = c.PromCheckitem;
                        comboBoxPTCountItems.Text = c.PromCountItem.ToString();
                        textBoxPTAmounts.Text = c.PromAmountBil.ToString();
                        dateTimePickerStartDate.Value = FuncString.ToDateTime(c.PromDatefrom, "yyyyMMdd").Date;
                        dateTimePickerEndDate.Value = FuncString.ToDateTime(c.PromDateTo, "yyyyMMdd").Date;
                        textBoxTime1.Text = c.Promtime1;
                        textBoxTime2.Text = c.Promtime2;
                        textBoxTime3.Text = c.Promtime3;
                        comboBoxProAuto.Text = c.PromAutoUse;
                        comboBoxPTProductGroup.SelectedValue = c.ProductGroupID;
                        labelPromotionName.Text = c.PromCode + "-" + c.PromName;

                        textBoxPTQtyLimit.Text = c.PromLimitItem.ToString();
                        textBoxPTQtyBalance.Text = c.PromBalanceItem.ToString();
                        promCheckBoxDayW(c.PromDayofWeek);
                    }
                }


                allPromDetail = gd.getPromDetail(dataID, 0);
                dataGridViewAllPromDetail.DataSource = allPromDetail;

                dataGridViewAllPromDetail.Columns[1].Visible = false;
                dataGridViewAllPromDetail.Columns[3].Visible = false; 

            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridViewProm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataID = Int32.Parse(dataGridViewAllProm.Rows[e.RowIndex].Cells["PromID"].Value.ToString());

                comboBoxListPromotion.SelectedValue = dataID;

                comboBoxListPromotion.Visible = true;
                labelHeaderPromotion.Text = "แก้ไขข้อมูล";
                buttonPTAddData.Text = "แก้ไขข้อมูล";
                radioButtonUpdateDataProm.Checked = true;
                 


            }
            catch (Exception ex)
            {

            }
        }

        private void buttonPTAddData_Click(object sender, EventArgs e)
        {
            try
            {

                 string promCode = textBoxPTCode.Text ;
                 string promName =  textBoxPTName.Text ;
                 string promCheckitem =   comboBoxPTType.Text;
                 int promCountItem = Int32.Parse(comboBoxPTCountItems.Text ); 
                 float promAmountBil = float.Parse(textBoxPTAmounts.Text );
                 int promProGroupID = Int32.Parse(comboBoxPTProductGroup.SelectedValue.ToString());
                 string dateFrom = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
                 string dateTo = dateTimePickerEndDate.Value.ToString("yyyyMMdd");
                 string promtime1 = textBoxTime1.Text  ;
                 string promtime2 = textBoxTime2.Text  ;
                 string promtime3 = textBoxTime3.Text  ;
                 string promAutoUse = comboBoxProAuto.Text ;

                 int promQtyLimits = Int32.Parse(textBoxPTQtyLimit.Text);
                 int promQtyBalance = Int32.Parse(textBoxPTQtyBalance.Text);   
                 string promDayW = findStrCheckBocDayW();


                if (radioButtonAddDataProm.Checked == true)
                {

                    if (MessageBox.Show("คุณต้องการเพิ่มข้อมูล " + promName + " หรือไม่ ?", "เพิ่ม " + promName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {


                        int result = gd.insertNewPromotion(promCode, promName, promCheckitem, promCountItem, promAmountBil, promAutoUse, promProGroupID, dateFrom, dateTo, promtime1, promtime2, promtime3, promQtyLimits,promQtyBalance,promDayW);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert Promotion  : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่มข้อมุลกลุ่ม " + promName + ">> (Success)");
                            allProm = gd.getListProm(0);
                            allProm_DATA = gd.getListProm_DATA(0);
                            dataGridViewAllProm.DataSource = allProm_DATA;
                            getComboAllProm();
                            clearPTAddData();

                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("คุณต้องการแก้ไข " + promName + " หรือไม่ ?", "แก้ไข " + promName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int promID = Int32.Parse(comboBoxListPromotion.SelectedValue.ToString());

                        int result = gd.updateNewPromotion(promID, promCode, promName, promCheckitem, promCountItem, promAmountBil, promAutoUse, promProGroupID, dateFrom, dateTo, promtime1, promtime2, promtime3, promQtyLimits, promQtyBalance, promDayW);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Promotion: Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("แก้ไขข้อมูล : " + promName + ">> (Success)");
                            allProm = gd.getListProm(0);
                            allProm_DATA = gd.getListProm_DATA(0);
                            dataGridViewAllProm.DataSource = allProm_DATA;
                            getComboAllProm();
                            clearPTAddData();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




       
        // Promotion Detail


        private void getComboAllProductGroupPD()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List
            data.Add(new KeyValuePair<int, string>(-1, "== เลือกกลุ่มสินค้า =="));

            foreach (ProductGroup c in allProductGroup)
            {
                if( c.ProgroupFlaguse == "Y")
                     data.Add(new KeyValuePair<int, string>(c.ProGroupID, c.ProgroupName));
            }


            // Clear the combobox
            comboBoxPDProductGroup.DataSource = null;
            comboBoxPDProductGroup.Items.Clear();

            // Bind the combobox
            comboBoxPDProductGroup.DataSource = new BindingSource(data, null);
            comboBoxPDProductGroup.DisplayMember = "Value";
            comboBoxPDProductGroup.ValueMember = "Key";


            comboBoxPTProductGroup.DataSource = null;
            comboBoxPTProductGroup.Items.Clear();

            comboBoxPTProductGroup.DataSource = new BindingSource(data, null);
            comboBoxPTProductGroup.DisplayMember = "Value";
            comboBoxPTProductGroup.ValueMember = "Key";

            comboBoxPTProductGroup.SelectedIndex = 0;
            comboBoxPDProductGroup.SelectedIndex = 0;

        }


        private void clearPDAddData()
        {
            textBoxPDSegNo.Text = "0";
            comboBoxPDProductGroup.SelectedIndex = 0;
            comboBoxPDSeqType.Text = "NormalPrice"; 
            textBoxPDSETPrice.Text = "0";
            textBoxPDDiscountAmt.Text = "0";
            textBoxPDDiscountPer.Text = "0";
        }

        private void buttonPDClear_Click(object sender, EventArgs e)
        {
            clearPDAddData();
        }

        private void buttonAddPDSequence_Click(object sender, EventArgs e)
        {
            panelPromDetail.Visible = true;
        }

        private void buttonPDClose_Click(object sender, EventArgs e)
        {
            panelPromDetail.Visible = false;
        }


        private void dataGridViewPromDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataID = Int32.Parse(dataGridViewAllPromDetail.Rows[e.RowIndex].Cells["PromSegNo"].Value.ToString());


                panelPromDetail.Visible = true;

                foreach (PromDetail c in allPromDetail)
                {
                    if (c.PromSegNo == dataID)
                    {
                        textBoxPDSegNo.Text = c.PromSegNo.ToString();
                        comboBoxPDProductGroup.SelectedValue = c.ProductGroupID;
                        comboBoxPDSeqType.Text = c.SetPriceGroup;
                        textBoxPDSETPrice.Text = c.SetPrice.ToString();
                        textBoxPDDiscountAmt.Text = c.DiscountAmtitem.ToString();
                        textBoxPDDiscountPer.Text = c.DiscountPeritem.ToString();
                    }
                }

                 
            }
            catch (Exception ex)
            {

            }
        }

        private void buttonPDAddData_Click(object sender, EventArgs e)
        {
            try
            {

                int promID = Int32.Parse(comboBoxListPromotion.SelectedValue.ToString());
                int segNo = Int32.Parse(textBoxPDSegNo.Text);
                int dataProductGroupID = Int32.Parse(comboBoxPDProductGroup.SelectedValue.ToString());
                string strPriceGroup = comboBoxPDSeqType.Text;
                float setPrice = float.Parse( textBoxPDSETPrice.Text ) ;
                float discountAmtitem = float.Parse(textBoxPDDiscountAmt.Text ) ;
                float discountPeritem = float.Parse(textBoxPDDiscountPer.Text);
                  

                if (MessageBox.Show("คุณต้องการเพิ่ม/แก้ไข " + labelPromotionName.Text + " ลำดับ " + segNo.ToString()  + " หรือไม่ ?", "แก้ไข " + labelPromotionName.Text + " ลำดับ " + segNo.ToString() , MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.updatePromDetail(promID, segNo, dataProductGroupID, strPriceGroup, setPrice, discountPeritem, discountAmtitem);

                    if (result <= 0)
                    {
                        MessageBox.Show("Error Update Pro Detail: Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("แก้ไขข้อมูล : " + labelPromotionName.Text + " ลำดับ " + segNo.ToString() + ">> (Success)");
                        allPromDetail = gd.getPromDetail(promID,0);
                        dataGridViewAllPromDetail.DataSource = allPromDetail;
                       
                        clearPDAddData();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxPTType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPTType.Text == "AFTER")
                comboBoxPTProductGroup.Visible = true;
            else
                comboBoxPTProductGroup.Visible = false;


        }

        private void textBoxProGroupSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string srPName = textBoxProGroupSearch.Text;

                if (srPName.Length > 0)
                {
                    this.allProductGroup_DATA.DefaultView.RowFilter = string.Format("ProGroupName like '*{0}*' ", srPName);

                }
                else
                {
                    this.allProductGroup_DATA.DefaultView.RowFilter = string.Format("1=1 ");
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxPromSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string srPName = textBoxPromSearch.Text;

                if (srPName.Length > 0)
                {
                    this.allProm_DATA.DefaultView.RowFilter = string.Format("PromName like '*{0}*' or PromCode like '*{0}*' ", srPName);

                }
                else
                {
                    this.allProm_DATA.DefaultView.RowFilter = string.Format("1=1 ");
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void textBoxPTQtyLimit_TextChanged(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked)
                textBoxPTQtyBalance.Text = textBoxPTQtyLimit.Text;
        }

    }
    
}
