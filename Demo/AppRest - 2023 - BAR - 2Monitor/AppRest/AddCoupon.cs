using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace AppRest
{
    public partial class AddCoupon : AddDataTemplate
    {
        GetDataRest gd;
        MainManage formMainManage;


      //  List<GroupCat> allGroupCats;
        List<Coupon> allCoupon;
        DataTable allCoupon_DATA;

        int couponID;


        public AddCoupon(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();


            this.Width = 1024;
            this.Height = 764;

            gd = new GetDataRest();

            comboBoxAllCoupon.Visible = false;

       //     selectedGruopCat = 1;

            setDefault();
             
        }


        private void setDefault()
        {

            allCoupon = gd.getAllCoupon(0, "000","000");
            allCoupon_DATA = gd.getAllCoupon_DATA(0, "000", "000");
            dataGridViewAllData.DataSource = allCoupon_DATA;

            dateTimePickerStartDate.Value = DateTime.Now;
            dateTimePickerEnd.Value =  DateTime.Now.AddMonths(3);

            txtBoxCouponCode.Text = "";
            txtBoxCouponDesc.Text = "";
            txtBoxCouponName.Text = "";
            txtBoxCouponRemark.Text = "";
            txtBoxCouponSynTax.Text = "FOR:Disc=:0:0:0:0:0:0:0:0";

            comboBoxUpdateType.SelectedIndex = 0;
            ComboboxCouponFlagUsed.Text = "N";
            ComboboxFlagUse.Text = "Y";

            comboBoxUpdateType.SelectedIndex = 0;


            comboBoxAllCoupon.Visible = false;
            radioButtonAddData.Checked = true;

            getComboAllCoupon();

            //flagSave = 0;
            //FlagSaveChange();

            panelSynax.Visible = false;



        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {

            try
            {

                string couponFromDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
                string couponToDate = dateTimePickerEnd.Value.ToString("yyyyMMdd");

                string couponCode = txtBoxCouponCode.Text;
                string couponDesc = txtBoxCouponDesc.Text;
                string couponName = txtBoxCouponName.Text;
                string couponRemark = txtBoxCouponRemark.Text;
                string couponSynTax = txtBoxCouponSynTax.Text;

                string couponUpdateType = comboBoxUpdateType.Text;
                string couponFlagUsed = ComboboxCouponFlagUsed.Text;
                string flagUse =  ComboboxFlagUse.Text  ;
                 
;

                this.couponID = Int32.Parse(comboBoxAllCoupon.SelectedValue.ToString()); 


                if (radioButtonAddData.Checked == true)
                {

                    if (MessageBox.Show("คุณต้องการเพิ่มคูปอง " + couponName + " หรือไม่ ?", "เพิ่ม " + couponName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.instNewCoupon(couponCode, couponName, couponDesc, couponFromDate, couponToDate, couponSynTax, Login.userID, couponRemark, couponFlagUsed, flagUse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert Coupon : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่มคูปอง " + couponName + ">> (Success)");

                            setDefault();

                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("คุณต้องการแก้ไขคูปองชื่อ " + couponName + " หรือไม่ ?", "เพิ่ม " + couponName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updsCoupon(this.couponID,couponCode, couponName, couponDesc, couponFromDate, couponToDate, couponSynTax, Login.userID, couponRemark, couponFlagUsed, flagUse,couponUpdateType);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Coupon: Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("แก้ไข : " + couponName + ">> (Success)");
                            setDefault();
                        }
                    }

                }

                searchCoupon();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void getComboAllCoupon()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(-1, "= เลือกแก้ไขคูปอง ="));

            foreach (Coupon c in allCoupon)
            {
                data.Add(new KeyValuePair<int, string>(c.CouponID, c.CouponCode + " (" + c.CouponName + ")"));
            }


            // Clear the combobox
            comboBoxAllCoupon.DataSource = null;
            comboBoxAllCoupon.Items.Clear();

            // Bind the combobox
            comboBoxAllCoupon.DataSource = new BindingSource(data, null);
            comboBoxAllCoupon.DisplayMember = "Value";
            comboBoxAllCoupon.ValueMember = "Key";

        }


        private void button1_Click(object sender, EventArgs e)
        {
            clearForm(); 
        }

        private void clearForm()
        {
            dateTimePickerStartDate.Value = DateTime.Now;
            dateTimePickerStartDate.Value = DateTime.Now.AddMonths(3);

            txtBoxCouponCode.Text = "";
            txtBoxCouponDesc.Text = "";
            txtBoxCouponName.Text = "";
            txtBoxCouponRemark.Text = "";
            txtBoxCouponSynTax.Text = "FOR:Disc=:0:0:0:0:0:0:0";

            comboBoxUpdateType.SelectedIndex = 0;
            ComboboxCouponFlagUsed.Text = "N";
            ComboboxFlagUse.Text = "Y"; 
            radioButtonAddData.Checked = true;
        }

        private void ChangeUpdateFlag(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked == true)
            {
                comboBoxAllCoupon.Visible = false;
                labelHeader.Text = "เพิ่มคูปอง";
                buttonAddTable.Text = "เพิ่มข้อมูล";
                txtBoxCouponCode.ReadOnly = false;
            }
            else
            {
                comboBoxAllCoupon.Visible = true;
                labelHeader.Text = "แก้ไขคูปอง";
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
         

        private void CommboxAllCoupon_Change(object sender, EventArgs e)
        {
            try
            {

                this.couponID = Int32.Parse(comboBoxAllCoupon.SelectedValue.ToString());

                foreach (Coupon c in allCoupon)
                {
                    if (c.CouponID == this.couponID)
                    {
                        DateTime dts =   DateTime.ParseExact(c.CouponFromDate, "dd/MM/yyyy",
                                       new CultureInfo("en-CA"));
                        DateTime dte = DateTime.ParseExact(c.CouponToDate, "dd/MM/yyyy",
                                       new CultureInfo("en-CA"));

                        dateTimePickerStartDate.Value = dts;
                        dateTimePickerEnd.Value = dte;

                        txtBoxCouponCode.Text = c.CouponCode;
                        txtBoxCouponCode.ReadOnly = true;
                        txtBoxCouponDesc.Text = c.CouponDesc;
                        txtBoxCouponName.Text = c.CouponName;
                        txtBoxCouponRemark.Text = c.CouponRemark;
                        txtBoxCouponSynTax.Text = c.CouponSynTax;
                         
                        ComboboxCouponFlagUsed.Text = c.CouponUsed;
                        ComboboxFlagUse.Text = c.CouponFlagUse;                       
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

        private void dataGridViewAllData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridCatID = Int32.Parse(dataGridViewAllData.Rows[e.RowIndex].Cells["CouponID"].Value.ToString());

                comboBoxAllCoupon.SelectedValue = dataGridCatID;

                comboBoxAllCoupon.Visible = true;
                labelHeader.Text = "แก้ไขคูปอง";
                buttonAddTable.Text = "แก้ไขข้อมูล";
                radioButtonUpdateData.Checked = true;

            }
            catch (Exception ex)
            {

            }
        }
         


        private void searchCoupon()
        {

            try
            {

                string srcCouponCode = textBoxSrcCouponCode.Text;
                string srcCouponName = textBoxSrcCouponName.Text;
                string srcFlagCouponUsed = comboBoxSrcFlaCouponUsed.Text;


                if (srcFlagCouponUsed == "ALL")
                {

                    if (srcCouponCode.Length > 0 && srcCouponName.Length > 0) 
                        allCoupon_DATA.DefaultView.RowFilter = string.Format("CouponCode like '*{0}*' and CouponName like '*{1}*' ", srcCouponCode, srcCouponName);
                    else if (srcCouponCode.Length > 0 && srcCouponName.Length == 0)
                        allCoupon_DATA.DefaultView.RowFilter = string.Format("CouponCode like '*{0}*'   ", srcCouponCode);
                    else if (srcCouponCode.Length == 0 && srcCouponName.Length > 0)
                        allCoupon_DATA.DefaultView.RowFilter = string.Format("CouponName like '*{0}*'   ", srcCouponName);
                    else
                        allCoupon_DATA.DefaultView.RowFilter = string.Format(" 1 = 1 ");
                }
                else
                {
                    if (srcCouponCode.Length > 0 && srcCouponName.Length > 0)
                        allCoupon_DATA.DefaultView.RowFilter = string.Format("CouponCode like '*{0}*' and CouponName like '*{1}*' and CouponUsed = '{2}' ", srcCouponCode, srcCouponName, srcFlagCouponUsed);
                    else if (srcCouponCode.Length > 0 && srcCouponName.Length == 0)
                        allCoupon_DATA.DefaultView.RowFilter = string.Format("CouponCode like '*{0}*' and CouponUsed = '{1}'  ", srcCouponCode, srcFlagCouponUsed);
                    else if (srcCouponCode.Length == 0 && srcCouponName.Length > 0)
                        allCoupon_DATA.DefaultView.RowFilter = string.Format("CouponName like '*{0}*' and CouponUsed = '{1}'  ", srcCouponName, srcFlagCouponUsed);
                    else
                        allCoupon_DATA.DefaultView.RowFilter = string.Format("CouponUsed = '{0}' ", srcFlagCouponUsed);                   

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxSrcCouponCode_TextChanged(object sender, EventArgs e)
        {
            searchCoupon();
        }

        private void textBoxSrcCouponName_TextChanged(object sender, EventArgs e)
        {
            searchCoupon();
        }

        private void comboBoxSrcFlaCouponUsed_SelectedValueChanged(object sender, EventArgs e)
        {
            searchCoupon();
        }

        private void buttonNonUsed_Click(object sender, EventArgs e)
        {
            try
            {
                string couponCode = txtBoxCouponCode.Text;
                int result = gd.updsCoupon_nonUsed(couponCode);

                if (result <= 0)
                {
                    MessageBox.Show("Error Update Non Used Coupon: Please Try Again");
                }
                else
                {
                    MessageBox.Show("แก้ไข : " + couponCode + ">> Non Used (Success)");
                    setDefault();
                }

                searchCoupon();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }












    }
    
}
