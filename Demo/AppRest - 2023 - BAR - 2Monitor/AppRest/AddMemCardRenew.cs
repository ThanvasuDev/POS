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
    public partial class AddMemCardRenew : AddDataTemplate
    {
        GetDataRest gd;
        List<MemCard> allMemCard;
        List<Promotion> allPromotion;

        MainManage formMainManage;

        string memCardID;
        MemCard mc;
        string flagExpire;

        DataTable dp;


        public AddMemCardRenew(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();
            gd = new GetDataRest();


            clearForm();

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

        private void clearForm()
        {


           // comboBoxAllMember.Visible = false;

            this.memCardID = "0";
            mc = new MemCard();
            this.flagExpire = "N";

            allMemCard = gd.getAllMemCard("0");
            dataGridViewAllMemCard.DataSource = allMemCard;

            dataGridViewAllMemCard.Columns[0].Visible = false;
            dataGridViewAllMemCard.Columns[1].Visible = false;

            allPromotion = gd.getAllPromotion("0");

            getComboAllMemCard();

            textBoxStrSearchMemCard.Text = "";

            

            buttonRenew.Enabled = false;
            panelMemDetail.Visible = false;
            panelMemPoint.Visible = false;

            clearMemCard();

            textBoxStrSearchMemCard.Focus();

            
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

        private void textBoxStrSearchMemCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                searchMemCard();
            }

            if (e.KeyChar == (char)8)
            {
                clearMemCard();
            }
        }


        private void searchMemCard()
        {
            string strSearchMemCard = textBoxStrSearchMemCard.Text;

            clearMemCard();

            if (strSearchMemCard.Length > 1)
            {
                mc = gd.SelMemCard_Search(strSearchMemCard, "N");
            }
            else
            {
                this.memCardID = "0"; 
            }

            try
            {

                if (mc == null)
                { 
                     throw new Exception("ไม่พบเลขสมาชิก / เบอร์โทรนี้ / ยังไม่ได้สมัครสมาชิก");
                }

                this.memCardID = mc.MemCardID;

                textBoxMemCardID.Text = mc.MemCardID;
                textBoxMemCardName.Text = mc.MemCardName;
                textBoxMemCardTel.Text = mc.Tel;

                this.flagExpire = mc.FlagExpire;

                //if (this.flagExpire == "Y")
                //{
                //    textBoxMemCardStatus.Text = "บัตรหมดอายุแล้ว";
                //    MessageBox.Show("สถานะบัตร : บัตรหมดอายุแล้ว กรุณาต่ออายุบัตรก่อน");
                //}
                //else
                //{
                //    textBoxMemCardStatus.Text = "บัตรยังไม่หมดอายุ";
                //}
                 
             //   textBoxMemCardExDate.Text = mc.ExpireDate.ToString();
                textBoxMemCardPoint.Text = mc.Point.ToString();

             //   textBoxRestRenew.Text = mc.CreateByRest.ToString();

                if (this.memCardID != "0")
                {
                    panelMemDetail.Visible = true;
                    panelMemPoint.Visible = true;

                    dp = gd.getTrnPointByMemCard(this.memCardID);
                    dataGridViewPoint.DataSource = dp;

                    //if (this.flagExpire == "Y")
                    //   buttonRenew.Enabled = true; 
                    //else 
                    //    buttonRenew.Enabled = false; 

                    /// Select Point

                }
                else
                {

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearMemCard()
        {
            textBoxMemCardID.Text = "";
            textBoxMemCardName.Text = "";
            textBoxMemCardTel.Text = "";
            textBoxMemCardStatus.Text = "";
            textBoxMemCardExDate.Text = "";
            textBoxMemCardPoint.Text = "";
            textBoxRestRenew.Text = "";



            buttonRenew.Enabled = false;
            panelMemDetail.Visible = false;
            panelMemPoint.Visible = false;
        }


        private void buttonSearchMemCard_Click(object sender, EventArgs e)
        {
            searchMemCard();
        }

        private void buttonRenew_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("คุณต้องการจะต่ออายุบัตรสมาชิก : " + this.memCardID + " : ชื่อ " + textBoxMemCardName.Text + " หรือไม่ ?", "ต่ออายุบัตรสมาชิกบัตร " + this.memCardID, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int result = gd.updsMemCardRenew(this.memCardID);

                    if (result <= 0)
                    {
                        throw new Exception("Error Renew Member Card : Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("ต่ออายุบัตรสมาชิก : " + this.memCardID + " : ชื่อ " + textBoxMemCardName.Text + " >> (Success)");

                        clearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

  

    }
}
