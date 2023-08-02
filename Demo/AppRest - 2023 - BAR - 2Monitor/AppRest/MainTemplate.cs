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
    public partial class MainTemplate : Form
    {
        MainTable formMainTable;
        MainSales formMainSales;
        MainOrder formMainOrder;  // ใช้่ ชื่อ เป็น Bill แทน
        MainStock formMainStock;
        MainWork formMainWork;
        MainManage formMainManage;
        MainSummary formMainSummary;
        Standby formStandby;

        MainCashCard formMainCashCard;


        public MainTemplate()
        {
            InitializeComponent();
            genDefault();

            this.Width = 1024; 
            if (Login.isFrontWide)
            {
                buttonExit.Width = 360;
                buttonExit.TextAlign = ContentAlignment.MiddleCenter;
            }

        }

        private void genDefault()
        {
            TxtFooter.Text = "Copy Right @ " + ConfigurationSettings.AppSettings["RestName"] + " " +  ConfigurationSettings.AppSettings["TabletName"];
          //  txtUserName.Text = "User:" + Login.userName + " " + ConfigurationSettings.AppSettings["TabletName"];

            ControlBox = false;
            this.Text = this.Text + "( By : " + Login.userName + ")";

            string status = Login.userStatus;

            string isTablet = ConfigurationSettings.AppSettings["IsTablet"];

            if (Login.userStatus.ToLower() == "work" || Login.userStatus.ToLower() == "captain")
            {
            //    buttonLinkSales.Visible = false;
                buttonLinkOrder.Visible = false;
                buttonLinkStock.Visible = false;
                buttonLinkSummary.Visible = false;
                buttonLinkCashCard.Visible = false;
                buttonLinkManage.Visible = false;

            }

            if (Login.userStatus.ToLower() == "stock")
            {
                buttonLinkSales.Visible = false;
                buttonLinkOrder.Visible = false;
              //  buttonLinkStock.Visible = false;
                buttonLinkSummary.Visible = false;
                buttonLinkCashCard.Visible = false;
                buttonLinkManage.Visible = false;

            }
             

            if (Login.userStatus.ToLower() == "cashier")
            {
             //    buttonLinkSales.Visible = false;
            //     buttonLinkOrder.Visible = false;
           //     buttonLinkStock.Visible = false;
             //   buttonLinkSummary.Visible = false;
            //    buttonLinkCashCard.Visible = false;
                buttonLinkManage.Visible = false;

            }

            if (Login.userStatus.ToLower() == "manager")
            {
                buttonLinkManage.Visible = false; 

            }

            if (isTablet == "Y")
            {
                buttonLinkSales.Visible = false;
                buttonLinkOrder.Visible = false;
                buttonLinkStock.Visible = false;
                buttonLinkSummary.Visible = false;
                buttonLinkManage.Visible = false;
                buttonLinkCashCard.Visible = false;

            }

            if (Login.isFrontPOS)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
             

               if (this.GetType() != typeof(Standby))
                   LinkFormStandby();

           //}

        }



        private void buttonLinkMain_Click(object sender, EventArgs e)
        {
            if (this.GetType() != typeof(MainTable))
                LinkFormMainTable();
        }
         

        private void buttonLinkSales_Click(object sender, EventArgs e)
        {
            if (this.GetType() != typeof(MainSales))
                LinkFormMainSales();
        }

        private void buttonLinkOrder_Click(object sender, EventArgs e)
        {
            if (this.GetType() != typeof(MainOrder))
                LinkFormMainOrder();
        }

        private void buttonLinkStock_Click(object sender, EventArgs e)
        {
            if (this.GetType() != typeof(MainStock))
                LinkFormMainStock();
        }

        private void buttonLinkCashCard_Click(object sender, EventArgs e)
        {
            if (this.GetType() != typeof(MainCashCard))
                LinkFormMainBooking();
        }

        private void buttonLinkWork_Click(object sender, EventArgs e)
        {

        }

        private void buttonLinkSummary_Click(object sender, EventArgs e)
        {
            if (this.GetType() != typeof(MainSummary))
                LinkFormMainSummary();
        }

        private void buttonLinkManage_Click(object sender, EventArgs e)
        {
            if (this.GetType() != typeof(MainManage))
                LinkFormMainManage();
        }

        private void LinkFormMainTable()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formMainTable == null)
            {
                formMainTable = new MainTable(this, 1);
            }
            Cursor.Current = Cursors.Default;
            if (formMainTable.ShowDialog() == DialogResult.OK)
            {
                formMainTable.Dispose();
                formMainTable = null;
            }
        }

        private void LinkFormMainSales()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formMainSales == null)
            {
                formMainSales = new MainSales(this, 1);
            }
            Cursor.Current = Cursors.Default;
            if (formMainSales.ShowDialog() == DialogResult.OK)
            {
                formMainSales.Dispose();
                formMainSales = null;
            }
        }

        private void LinkFormMainOrder()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formMainOrder == null)
            {
                formMainOrder = new MainOrder(this, 1);
            }
            Cursor.Current = Cursors.Default;
            if (formMainOrder.ShowDialog() == DialogResult.OK)
            {
                formMainOrder.Dispose();
                formMainOrder = null;
            }
        }

        private void LinkFormMainWork()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formMainWork == null)
            {
                formMainWork = new MainWork(this, 1);
            }
            Cursor.Current = Cursors.Default;
            if (formMainWork.ShowDialog() == DialogResult.OK)
            {
                formMainWork.Dispose();
                formMainWork = null;
            }
        }

        private void LinkFormMainCashCard()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formMainCashCard == null)
            {
                formMainCashCard = new MainCashCard(this, 1);
            }
            Cursor.Current = Cursors.Default;
            if (formMainCashCard.ShowDialog() == DialogResult.OK)
            {
                formMainCashCard.Dispose();
                formMainCashCard = null;
            }
        }

        MainBooking formMainBooking;

        private void LinkFormMainBooking()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formMainBooking == null)
            {
                formMainBooking = new MainBooking(this, 1);
            }
            Cursor.Current = Cursors.Default;
            if (formMainBooking.ShowDialog() == DialogResult.OK)
            {
                formMainBooking.Dispose();
                formMainBooking = null;
            }
        }


        private void LinkFormMainStock()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formMainStock == null)
            {
                formMainStock = new MainStock(this, 1);
            }
            Cursor.Current = Cursors.Default;
            if (formMainStock.ShowDialog() == DialogResult.OK)
            {
                formMainStock.Dispose();
                formMainStock = null;
            }
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

        private void LinkFormMainSummary()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formMainSummary == null)
            {
                formMainSummary = new MainSummary(this, 1);
            }
            Cursor.Current = Cursors.Default;
            if (formMainSummary.ShowDialog() == DialogResult.OK)
            {
                formMainSummary.Dispose();
                formMainSummary = null;
            }
        }


        private void LinkFormStandby()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formStandby == null)
            {
                formStandby = new Standby(this, 1);
            }
            Cursor.Current = Cursors.Default;
            if (formStandby.ShowDialog() == DialogResult.OK)
            {
                formStandby.Dispose();
                formStandby = null;
            }
        }



    }
}
