using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThaiNationalIDCard;

namespace AppRest
{
    public partial class AddMemCard : AddDataTemplate
    {
        GetDataRest gd;
        List<MemCard> allMemCard;
        List<Promotion> allPromotion;
        DataTable datatableMemCard;
        MainManage formMainManage;


        public AddMemCard(Form frmlkFrom, int flagFrmClose)
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

                clearForm();
                idcard = new ThaiIDCard();
            }
            catch (Exception ex)
            {

            }
        }


        private void getComboAllMemCard()
        {
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<string, string>("0", "= เลือกแก้ไขข้อมูลบัตรสมาชิก ="));

            foreach (MemCard c in allMemCard)
            {
                data.Add(new KeyValuePair<string, string>(c.MemCardID, c.MemCardID + ' ' + c.MemCardName));
            }


            // Clear the combobox
            comboBoxAllMember.DataSource = null;
            comboBoxAllMember.Items.Clear();

            // Bind the combobox
            comboBoxAllMember.DataSource = new BindingSource(data, null);
            comboBoxAllMember.DisplayMember = "Value";
            comboBoxAllMember.ValueMember = "Key";

        }


        private void getComboAllPromotion()
        {
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<string, string>("0", "= เลือกเลเวลสมาชิก ="));

            foreach (Promotion c in allPromotion)
            {
                data.Add(new KeyValuePair<string, string>(c.PromotionCode, c.PromotionCode + '-' + c.PromotionDesc));
            }


            // Clear the combobox
            comboBoxPromotion.DataSource = null;
            comboBoxPromotion.Items.Clear();

            // Bind the combobox
            comboBoxPromotion.DataSource = new BindingSource(data, null);
            comboBoxPromotion.DisplayMember = "Value";
            comboBoxPromotion.ValueMember = "Key";

            comboBoxPromotion.SelectedIndex = 1;

        }

        private void getComboAllLevel()
        {
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<string, string>("0", "= ค้นหาตาม Level ="));

            foreach (Promotion c in allPromotion)
            {
                data.Add(new KeyValuePair<string, string>(c.PromotionCode, c.PromotionCode));
            }


            // Clear the combobox
            comboBoxSRLevel.DataSource = null;
            comboBoxSRLevel.Items.Clear();

            // Bind the combobox
            comboBoxSRLevel.DataSource = new BindingSource(data, null);
            comboBoxSRLevel.DisplayMember = "Value";
            comboBoxSRLevel.ValueMember = "Key";

        }


        private void clearForm()
        {


            allMemCard = gd.getAllMemCard("0");

            datatableMemCard = gd.getAllMemCardDataTable("0");
            dataGridViewAllData.DataSource = datatableMemCard;

           // dataGridViewAllMemCard.DataSource = allMemCard;

            dataGridViewAllData.Columns[0].Visible = false;
            dataGridViewAllData.Columns[1].Visible = false;
            dataGridViewAllData.Columns[2].Visible = false;

            allPromotion = gd.getAllPromotion("0");

            getComboAllMemCard();
            getComboAllPromotion();
            getComboAllLevel();

            txtBoxName.Text = "";
            txtBoxAddress.Text = "";
            txtBoxTel.Text = "";
            textBoxEmail.Text = "";
            textBoxMemCode.Text = "";
            textBoxcardID.Text = "";
            radioButtonAddData.Checked = true;
            radioSexMale.Checked = true;

            comboBoxCarier.SelectedIndex = 0;
            comboBoxPromotion.SelectedIndex = 0;

            dateTimePickerAppDate.Value = DateTime.Now;
            dateTimePickerBirthDate.Value = DateTime.Now;

            comboBoxAllMember.Visible = false;

            
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {

            clearForm();
        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {

            try
            {
           

               string memcardID =  comboBoxAllMember.SelectedValue.ToString();

               string memcardName =  txtBoxName.Text ;
               string address = txtBoxAddress.Text  ;
               string tel =  txtBoxTel.Text;
               string email = textBoxEmail.Text;
               memcardID = textBoxMemCode.Text;


               string cardID = textBoxcardID.Text; //textBoxcardID.Text;

                string sex = "F";

                if ( radioSexMale.Checked == true)
                    sex = "M";

               DateTime  appDate = (DateTime) dateTimePickerAppDate.Value ;
               DateTime  birthDate = (DateTime) dateTimePickerBirthDate.Value ;

               string promotionCode = comboBoxPromotion.SelectedValue.ToString();

               if (promotionCode == "0")
                   throw new Exception("กรุณาเลือก Promotion ให้ บัตรนี้");

               if (memcardID.Trim() == "")
                   throw new Exception("กรุณาเลือกกรอกรหัส Member Card ให้ บัตรนี้");

               if (FuncString.IsNumeric(tel) == false)
                   throw new Exception("เบอร์โทร. กรุณากรอกเป็นตัวเลขเท่านั้น");
                          
               if (radioButtonAddData.Checked == true)
               {

                   if (MessageBox.Show("คุณต้องการจะเพิ่ม : " + memcardID + " : ชื่อ " + memcardName + " หรือไม่ ?", "เพิ่ม " + memcardID, MessageBoxButtons.YesNo) == DialogResult.Yes)
                   {
                       int result = gd.instMemCard(memcardID, cardID, memcardName, sex, birthDate, tel, address, email, 0, promotionCode, "0", appDate, appDate);

                       if (result <= 0)
                       {
                           throw new Exception("Error Insert New Member Card : Please Try Again");
                       }
                       else
                       {
                           MessageBox.Show("เพิ่ม : " + memcardID + " : ชื่อ " + memcardName + " >> (Success)");
                         
                           clearForm();
                       }  
                   }
               }
                else
                 {
                     if (MessageBox.Show("คุณต้องการจะแก้ไข : " + memcardID + " : ชื่อ " + memcardName + " หรือไม่ ?", "เพิ่ม " + memcardID, MessageBoxButtons.YesNo) == DialogResult.Yes)
                     {
                         int result = gd.updsMemCard(memcardID, cardID, memcardName, sex, birthDate, tel, address, email, 0, promotionCode, "0", appDate, appDate);

                         if (result <= 0)
                         {
                             throw new Exception("Error Update New Member Card : Please Try Again");
                         }
                         else
                         {
                             MessageBox.Show("แก้ไข : " + memcardID + " : ชื่อ " + memcardName + " >> (Success)");
                            
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

        private void CommboxAllMemCard_Change(object sender, EventArgs e)
        {
            try
            {

                string memCardID =  comboBoxAllMember.SelectedValue.ToString();

                foreach (MemCard c in allMemCard)
                {
                    if (c.MemCardID == memCardID)
                    {
                        txtBoxName.Text = c.MemCardName;
                        txtBoxAddress.Text = c.Address;
                        txtBoxTel.Text = c.Tel;
                        textBoxEmail.Text = c.Email;
                        textBoxMemCode.Text = c.MemCardID;
                        textBoxcardID.Text = c.CardID; 

                        if (c.Sex == "M")
                            radioSexMale.Checked = true;
                        else
                            radioSexFMale.Checked = true;

                        comboBoxPromotion.SelectedValue = c.PromotionCode;

                        dateTimePickerAppDate.Value = c.ApplicationDate;
                        dateTimePickerBirthDate.Value = c.BirthDate;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridViewAllMemCard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                string dataGridMemD = dataGridViewAllData.Rows[e.RowIndex].Cells["MemCardID"].Value.ToString();

                comboBoxAllMember.SelectedValue = dataGridMemD;

                comboBoxAllMember.Visible = true;
                labelHeader.Text = "แก้ไขสมาชิก";
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
                string srLevelName = comboBoxSRLevel.SelectedValue.ToString();


                if (comboBoxSRLevel.SelectedIndex == 0)
                {

                    if (srMemName.Length > 0)
                    {
                        datatableMemCard.DefaultView.RowFilter = string.Format("Name like '*{0}*' ", srMemName);

                    }
                    else
                    {
                        if (strSearchMemCard.Length > 0)
                            this.datatableMemCard.DefaultView.RowFilter = string.Format("MemCardID = '{0}' ", strSearchMemCard);
                        else if (srMemTel.Length > 0)
                            this.datatableMemCard.DefaultView.RowFilter = string.Format("Tel = '{0}' ", srMemTel);
                        else
                            this.datatableMemCard.DefaultView.RowFilter = string.Format(" 1 = 1 ");

                    }

                }
                else
                {
                    if (srMemName.Length > 0)
                    {
                        datatableMemCard.DefaultView.RowFilter = string.Format("Name like '*{0}*' and PromotionCode = '{1}'", srMemName,srLevelName);

                    }
                    else
                    {
                        if (strSearchMemCard.Length > 0)
                            this.datatableMemCard.DefaultView.RowFilter = string.Format("MemCardID = '{0}' and PromotionCode = '{1}'", strSearchMemCard, srLevelName);
                        else if (srMemTel.Length > 0)
                            this.datatableMemCard.DefaultView.RowFilter = string.Format("Tel = '{0}' and PromotionCode = '{1}'", srMemTel, srLevelName);
                        else
                            this.datatableMemCard.DefaultView.RowFilter = string.Format(" PromotionCode = '{0}' ", srLevelName);

                    }

                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); 

                //textBoxStrSearchMemCardtoTable.Text = "";
                //textBoxStrSearchMemCardtoTable.Focus();
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

        private void buttonViewMaeterial_Export_Click(object sender, EventArgs e)
        {
            try
            {
                ExportData.Excel_FromDataTable_DT(dataGridViewAllData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private ThaiIDCard idcard ;

        private void buttonReadIDCard_Click(object sender, EventArgs e)
        {
            try
            {
                //Clear 
                textBoxcardID.Text = "";

                string addr = "";

                LabelStatus.Text = "Reading...";
               // Refresh();
                Personal personal = idcard.readAll();
                if (personal != null)
                {
                    textBoxcardID.Text = personal.Citizenid;
                    dateTimePickerBirthDate.Value = personal.Birthday;

                    if (personal.Sex == "1")
                        radioSexMale.Checked = true;
                    else
                        radioSexFMale.Checked = false;

                    if (radioButtonTH.Checked)
                        txtBoxName.Text = personal.Th_Prefix + personal.Th_Firstname + " " + personal.Th_Lastname;
                    else
                        txtBoxName.Text = personal.En_Prefix + personal.En_Firstname + " " + personal.En_Lastname;


                    txtBoxAddress.Text = personal.Address; 


                    //lbl_th_prefix.Text = personal.Th_Prefix;
                    //lbl_th_firstname.Text = personal.Th_Firstname;
                    //lbl_th_lastname.Text = personal.Th_Lastname;

                    //lbl_en_prefix.Text = personal.En_Prefix;
                    //lbl_en_firstname.Text = personal.En_Firstname;
                    //lbl_en_lastname.Text = personal.En_Lastname;
                    //lbl_issue.Text = personal.Issue.ToString("dd/MM/yyyy");
                    //lbl_expire.Text = personal.Expire.ToString("dd/MM/yyyy");

                    //LogLine(personal.Address);
                    //LogLine(personal.addrHouseNo); // บ้านเลขที่ 
                    //LogLine(personal.addrVillageNo); // หมู่ที่
                    //LogLine(personal.addrLane); // ซอย
                    //LogLine(personal.addrRoad); // ถนน
                    //LogLine(personal.addrTambol);
                    //LogLine(personal.addrAmphur);
                    //LogLine(personal.addrProvince);
                    //LogLine(personal.Issuer);
                }
                else if (idcard.ErrorCode() > 0)
                {
                    MessageBox.Show(idcard.Error());
                }
                else
                {
                    MessageBox.Show("Catch all");
                }

                LabelStatus.Text = "Complete Read";
            }
            catch (Exception ex)
            {
                LabelStatus.Text = "Reading..Fail";
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonClearAddr_Click(object sender, EventArgs e)
        {
            txtBoxAddress.Text = "";
        }

        private void buttonNewMemCardID_Click(object sender, EventArgs e)
        {
            try
            { 
                textBoxMemCode.Text = gd.getNextMemCard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }





        

    }
}
