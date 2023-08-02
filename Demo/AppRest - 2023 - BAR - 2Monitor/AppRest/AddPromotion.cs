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
    public partial class AddPromotion : AddDataTemplate
    {
        GetDataRest gd; 
        List<Promotion> allPromotion;

        MainManage formMainManage;

        List<DiscountGroup> dg;
        string syntaxDesc = "";


        public AddPromotion(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();

            this.Width = 1024;
            this.Height = 764;

            gd = new GetDataRest();

            /*
             * 
             FOR:Disc=:10:8:2:8:0:0
                 
             [1] > D1 = Discount Live Product
             [2] > DR = Discount Food Drink
             [3] > DA = Discount Alcohol
             [4] > DS = Discount Super
             [5] > DB = Discount Baht
             [6] > เลือกสินค้าอื่นด้วย = ProductID
           
           */

            dg = gd.getAllDiscountGroup();

            syntaxDesc = "FOR:Disc=:10:8:2:8:0:0"  + "\n\r";


            foreach (DiscountGroup d in dg)
            {
                syntaxDesc += "[" + d.DiscountGroupID.ToString() + "] > D" + d.DiscountGroupID.ToString() + " = " + d.DiscountGroupNameEN + "\n";
            }

            syntaxDesc += @"[5] > DB = Discount Baht
[6] > เลือกสินค้าอื่นด้วย = ProductID";

            richTextBoxSynTaxCode.Text = syntaxDesc;


            clearForm();

        }


        private void getComboAllPromotion()
        {
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<string, string>("0", "= เลือกโปรโมชั่น ="));

            foreach (Promotion c in allPromotion)
            {
                data.Add(new KeyValuePair<string, string>(c.PromotionCode, c.PromotionCode + '-' + c.PromotionDesc));
            }


            // Clear the combobox
            comboBoxAllPromotion.DataSource = null;
            comboBoxAllPromotion.Items.Clear();

            // Bind the combobox
            comboBoxAllPromotion.DataSource = new BindingSource(data, null);
            comboBoxAllPromotion.DisplayMember = "Value";
            comboBoxAllPromotion.ValueMember = "Key";

        }


     


        private void clearForm()
        {
            textBoxPromotionCode.Text = ""; 
            textBoxPromotionDesc.Text = "";
            txtBoxCouponSynTax.Text = "FOR:Disc=:0:0:0:0:0:0";
            radioButtonAddData.Checked = true; 

            comboBoxFlagUse.SelectedIndex = 0;
             

            comboBoxAllPromotion.Visible = false;

            allPromotion = gd.getAllPromotion("0");
            dataGridViewAllPromotion.DataSource = allPromotion;


            getComboAllPromotion(); 

            
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {

            clearForm();
        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {

            try
            {
           

               string promotionCode = textBoxPromotionCode.Text;
               string promotionDesc = textBoxPromotionDesc.Text;
               string promotionSyntax =  txtBoxCouponSynTax.Text  ;


               string flagUse = comboBoxFlagUse.Text;

               if (promotionCode.Trim() == "")
                   throw new Exception("กรุณาเกรอกรหัส promotionCode ");
                 


                          
               if (radioButtonAddData.Checked == true)
               {

                   if (MessageBox.Show("คุณต้องการจะเพิ่ม : " + promotionCode + " :  " + promotionDesc + " หรือไม่ ?", "เพิ่ม " + promotionCode, MessageBoxButtons.YesNo) == DialogResult.Yes)
                   {
                       int result = gd.instPromotion(promotionCode, promotionDesc,promotionSyntax, flagUse);

                       if (result <= 0)
                       {
                           throw new Exception("Error Insert New Promotion : Please Try Again");
                       }
                       else
                       {
                           MessageBox.Show("เพิ่ม : " + promotionCode + " : ชื่อ " + promotionDesc + " >> (Success)");
                         
                           clearForm();
                       }  
                   }
               }
                else
                 {
                     if (MessageBox.Show("คุณต้องการจะแก้ไข : " + promotionCode + " :  " + promotionDesc + " หรือไม่ ?", "เพิ่ม " + promotionCode, MessageBoxButtons.YesNo) == DialogResult.Yes)
                     {
                         int result = gd.updsPromotion(promotionCode, promotionDesc,promotionSyntax, flagUse);

                         if (result <= 0)
                         {
                             throw new Exception("Error Update Promotion : Please Try Again");
                         }
                         else
                         {
                             MessageBox.Show("แก้ไข : " + promotionCode + " : ชื่อ " + promotionDesc + " >> (Success)");

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
                comboBoxAllPromotion.Visible = false;
                labelHeader.Text = "เพิ่มข้อมูลโปรโมชั้น" ;
                buttonAddTable.Text = "เพิ่มข้อมูล";
            }
            else
            {
                comboBoxAllPromotion.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูลโปรโมชั้น";
                buttonAddTable.Text = "แก้ไขข้อมูล";
                textBoxPromotionCode.ReadOnly = true;
            }
        }

        private void CommboxAllMemCard_Change(object sender, EventArgs e)
        {
            try
            {

                string promotionCode =  comboBoxAllPromotion.SelectedValue.ToString();

                foreach (Promotion c in allPromotion)
                {
                    if (c.PromotionCode == promotionCode)
                    {
                        textBoxPromotionCode.Text = c.PromotionCode;
                        textBoxPromotionDesc.Text = c.PromotionDesc;
                        txtBoxCouponSynTax.Text = c.PromotionSyntax;

                        comboBoxFlagUse.SelectedValue = c.FlagUse; 
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonSyntax_Click(object sender, EventArgs e)
        {
            if (panelSynax.Visible == true)
                panelSynax.Visible = false;
            else
                panelSynax.Visible = true;
        }

        private void dataGridViewAllPromotion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                string dataGridID = dataGridViewAllPromotion.Rows[e.RowIndex].Cells["PromotionCode"].Value.ToString();

                comboBoxAllPromotion.SelectedValue = dataGridID;

                comboBoxAllPromotion.Visible = true;
                labelHeader.Text = "แก้ไขข้อมูลโปรโมชั้น";
                buttonAddTable.Text = "แก้ไขข้อมูล";
                radioButtonUpdateData.Checked = true;

            }
            catch (Exception ex)
            {

            }
        }

       


  

     





        

    }
}
