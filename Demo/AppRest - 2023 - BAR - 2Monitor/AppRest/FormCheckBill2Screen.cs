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
    public partial class FormCheckBill2Screen : Form
    {
  

        int keyTime;

        public FormCheckBill2Screen() { InitializeComponent(); }

        //public FormCheckBill2Screen(Form frmlkFrom, int flagFrmClose, int totalSalesFromOrder, int amtFromCustomer,  int amtChange)
        //{
        //    InitializeComponent();
        //    if (flagFrmClose == 1)
        //    {
        //        frmlkFrom.Dispose();
        //    }
        //    totalsales.Enabled = false;
        //    change.Enabled = false;
        //    totalsales.Text = totalSalesFromOrder.ToString();   //FormMain.totalCheckbill.ToString();

        //    this.ControlBox = false;

        //    totalsales.Text = totalSalesFromOrder.ToString("###,###");
        //    fromCust.Text = amtFromCustomer.ToString("###,###");
        //    change.Text = amtChange.ToString("###,###");

        //    if (change.Text.Length == 0)
        //        change.Text = "0";
        //}

        private void button1_Click(object sender, EventArgs e)
        { 
            this.Close();
        }

        private void fromCust_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Close();
        }
 

      

       

    }
}
