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
    public partial class FormCheckBill : Form
    {
        public string amtFromCustomer;
        public string amtFromChange;
        public int paytype;

        int keyTime;

        public FormCheckBill(Form frmlkFrom, int flagFrmClose, int totalSalesFromOrder,string checkTableName)
        {
            InitializeComponent();
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            totalsales.Enabled = false;
            change.Enabled = false;
            totalsales.Text = totalSalesFromOrder.ToString();   //FormMain.totalCheckbill.ToString();

            this.ControlBox = false;
            fromCust.Focus();

            keyTime = 0;
            paytype = 0;
            labelTableName.Text = checkTableName;


            if (Login.userStatus == "FoodPark")
            {
                buttonCash.Enabled = false;
                buttonEqualBill.Enabled = false;
                ButtonClear.Enabled = false;
                buttonCreditCard.Enabled = false;
                buttonCreditCust.Enabled = false;
                fromCust.Enabled = false;
                buttonCashCard.Focus();
            }


            string flagPrintCash = ConfigurationSettings.AppSettings["FlagPrintCash"].ToString();

            if (flagPrintCash.ToLower() == "y")
                radioButtonFinalPrintBill.Checked = true;
            else
                radioButtonFinalPrintBill.Checked = false;


           // radioButtonFinalNoBill.Checked = true;

        }

        private void buttonCash_Click(object sender, EventArgs e)
        {

            try
            {
                if (Int32.Parse(change.Text) >= 0 && Int32.Parse(fromCust.Text) > 0)
                {
                    paytype = 1;

                    this.Close();
                }
                else
                {
                    throw new Exception(" เงินที่รับจากลูกค้าต้องมากกว่าราคาสินค้า ");
                }

            }
            catch (Exception ex)
            {

                fromCust.Text = "0";
                change.Text = "0";
                MessageBox.Show(ex.Message);
            }
        }

        private void change_TextChange(object sender, EventArgs e)
        {
            string amtFromCust = "";

            try
            {
                if (fromCust.Text.Trim().Length == 0)
                {
                    amtFromCust = "0";
                }
                else
                {
                    amtFromCust = fromCust.Text;
                    amtFromCustomer = amtFromCust;
                }

                change.Text = ((int)(Int32.Parse(amtFromCust) - Int32.Parse(totalsales.Text))).ToString();
                amtFromChange = change.Text;
            }
            catch (Exception ex)
            {

                fromCust.Text = "0";
                change.Text = "0";
                MessageBox.Show(ex.Message);
            }
        }

        private void KeyPassFinish(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.buttonCash_Click(buttonCash, e);
            }
        }


        private void ButtonNoClick(object sender, EventArgs e)
        {
            Button bt = (Button)sender;

            string txt = "";

            keyTime++;

            float oldAmount = 0;

            try
            {
                defaultColButOrderNo();
                bt.BackColor = System.Drawing.Color.Orange;

                oldAmount = float.Parse(fromCust.Text.ToString());

                if (bt.Name.Substring(0, 5) == "bTBK_")
                {

                    txt = bt.Name.Replace("bTBK_", " ").Trim();
                    oldAmount += float.Parse(txt);

                    fromCust.Text = oldAmount.ToString();


                }
                else
                {

                    txt = bt.Name.Replace("button_", " ").Trim();

                    if (keyTime == 1)
                        fromCust.Text = txt;
                    else
                        fromCust.Text += txt;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void defaultColButOrderNo()
        {
            button_1.BackColor = System.Drawing.Color.FromArgb(128,255,128);
            button_2.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_3.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_4.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_5.BackColor = System.Drawing.Color.FromArgb(128,255,128);
            button_6.BackColor = System.Drawing.Color.FromArgb(128,255,128);
            button_7.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_8.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_9.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_0.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_00.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            button_000.BackColor = System.Drawing.Color.FromArgb(128,255,128);
            buttonEqualBill.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            fromCust.Text = "0";
            keyTime = 0;
        }

        private void buttonEqualBill_Click(object sender, EventArgs e)
        {
            buttonEqualBill.BackColor = System.Drawing.Color.Orange;
            fromCust.Text = totalsales.Text;
        }

        private void buttonCreditCard_Click(object sender, EventArgs e)
        {
            paytype = 2;
            this.Close();
        }

        private void buttonCreditCust_Click(object sender, EventArgs e)
        {
            paytype = 3;
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCashCard_Click(object sender, EventArgs e)
        {
            paytype = 4;
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            paytype = 5;
            this.Close();
        }

        private void buttonWaitPayment_Click(object sender, EventArgs e)
        {
            paytype = 6;
            this.Close();
        }

        private void radioButtonPayTypeOther_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPayTypeOther.Checked)
            {
                buttonCash.Visible = false;
                buttonEqualBill.Visible = false;

            }
            else
            {
                buttonCash.Visible = true;
                buttonEqualBill.Visible = true;
            }
        }

        private void radioButtonPayTypeOne_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPayTypeOne.Checked)
            {
                buttonCash.Visible = true;
                buttonEqualBill.Visible = true;

                labelChange.Text = "คงเหลือ";

            }
        }

        private void fromCust_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    if (Int32.Parse(change.Text) >= 0 && Int32.Parse(fromCust.Text) > 0)
                    {
                        paytype = 1;

                        this.Close();
                    }
                    else
                    {
                        throw new Exception(" เงินที่รับจากลูกค้าต้องมากกว่าราคาสินค้า ");
                    }

                }
                catch (Exception ex)
                {

                    fromCust.Text = "0";
                    change.Text = "0";
                    MessageBox.Show(ex.Message);
                }
            } 
        }


    }
}
