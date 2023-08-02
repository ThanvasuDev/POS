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
    public partial class MainSales : MainTemplateS
    {
        GetDataRest gd;

        string salesDate;
        float salesTotal;

        TrnMax trnMax;


        string fromDate = "";
        string toDate = "";

        public MainSales(Form frmlkFrom, int flagFrmClose)
        {
            InitializeComponent();
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            

            this.Text = this.Text + " ( By : " + Login.userName + ")";

            buttonLinkSales.BackColor =  System.Drawing.Color.Gray;


            if (Login.isFrontPOS)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }

            this.Width = 1024;
            this.Height = 764;


            if (Login.isFrontWide)
            {
                this.Width = 1280;
            }

            labelSalesDate.Text = "";
            salesDate = "";
            salesTotal = 0;

            gd = new GetDataRest();
           
            try
            {
                getComboShift();
                dateTimePickerStartDate.Value = DateTime.Now.AddHours(-6);
                dateTimePickerEnd.Value = DateTime.Now.AddHours(-6);

                genSalesValues();

            }catch(Exception ex)
            {

            }

        }

        private void getComboShift()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List


            data.Add(new KeyValuePair<int, string>(-1, "Total Sales"));
            data.Add(new KeyValuePair<int, string>(0, "Current Shift"));
            data.Add(new KeyValuePair<int, string>(1, "Shift 1"));
            data.Add(new KeyValuePair<int, string>(2, "Shift 2"));
            data.Add(new KeyValuePair<int, string>(3, "Shift 3"));
            data.Add(new KeyValuePair<int, string>(4, "Shift 4"));

            // Clear the combobox
            comboBoxShiftID.DataSource = null;
            comboBoxShiftID.Items.Clear();

            // Bind the combobox
            comboBoxShiftID.DataSource = new BindingSource(data, null);
            comboBoxShiftID.DisplayMember = "Value";
            comboBoxShiftID.ValueMember = "Key";

            comboBoxShiftID.SelectedIndex = 0; 

        }


        private void genSalesValues()
        {


            fromDate = dateTimePickerStartDate.Value.ToString("yyyyMMdd");
            toDate = dateTimePickerEnd.Value.ToString("yyyyMMdd");


            gentxtTopGroupCat(); // 1
            gentxtTopCat(); // 2 
            gentxtTopZone(); // 4
            gentxtTopTable(); // 5
            gentxtTopPayment(); // 6  
            gentxtTopMenu(); // 3
            gentxtToday();

        }

        private void gentxtTopGroupCat()
        {
             

            List<SalesToday> topMenus = gd.getSalesToday(1, this.fromDate, this.toDate, this.shiftID);

            string strTopMenu = "";
            int i = 1;
             

            foreach (SalesToday s in topMenus)
            {
                strTopMenu += i.ToString() + ". " + s.SalesLable + " (" + s.SalesUnit.ToString("###,###.##") + ") :: " + s.SalesAmount.ToString("###,###.##") + " B." + "\n";
                this.salesTotal += s.SalesAmount;
                i++;
            }

            labelTopGroupCat.Text = strTopMenu;

        }

        private void gentxtTopCat()
        {
            List<SalesToday> topMenus = gd.getSalesToday(2 ,this.fromDate, this.toDate, this.shiftID);

            string strTopMenu = "";
            int i = 1;


            foreach (SalesToday s in topMenus)
            {
                strTopMenu += i.ToString() + ". " + s.SalesLable + " (" + s.SalesUnit.ToString("###,###.##") + ") :: " + s.SalesAmount.ToString("###,###.##") + " B." + "\n";
                this.salesTotal += s.SalesAmount;
                i++;
            }

            labelTopCat.Text = strTopMenu;

        }


        private void gentxtToday()
        {
            string strTopMenu = "";

            float salesTotal = 0;
            float ordersTotal = 0;

            try
            { 
                List<SalesToday> topMenus = gd.getSalesToday(0, "0", "0" , this.shiftID);


                int i = 1;


                foreach (SalesToday s in topMenus)
                {
                  //  strTopMenu += i.ToString() + ". " + s.SalesLable + " (" + s.SalesUnit.ToString("###,###.##") + ") :: " + s.SalesAmount.ToString("###,###.##") + " B." + "\n";

                    this.salesDate = s.SalesDate;
                    salesTotal += s.SalesAmount;
                    i++;
                }

                topMenus = gd.getSalesToday(7, this.fromDate, this.toDate, this.shiftID); // Order Amount 

                ordersTotal = topMenus[0].SalesAmount;

                labelSalesDate.Text = "Today :: " + this.salesDate + " >> Sales :: " + salesTotal.ToString("###,###.##") + " B. (Order :: "
                                      + ordersTotal.ToString("###,###.##") + " B.) >> Estimate Total :: " + (ordersTotal + salesTotal).ToString("###,###.##") + " B. ";
              

            }
            catch (Exception ex)
            {

                labelSalesDate.Text = "Today :: " + this.salesDate + " >> Sales :: " + salesTotal.ToString("###,###.##") + " B. (Order :: "
                      + ordersTotal.ToString("###,###.##") + " B.) >> Estimate Total :: " + (ordersTotal + salesTotal).ToString("###,###.##") + " B. ";
                
            }

        }



        private void gentxtTopMenu()
        {
            string strTopMenu = "";

            float salesTotal = 0; 

            try
            {
                List<SalesToday> topMenus = gd.getSalesToday(3, this.fromDate, this.toDate, this.shiftID);

               
                int i = 1;


                foreach (SalesToday s in topMenus)
                {
                    strTopMenu += i.ToString() + ". " + s.SalesLable + " (" + s.SalesUnit.ToString("###,###.##") + ") :: " + s.SalesAmount.ToString("###,###.##") + " B." + "\n";

                    this.salesDate = s.SalesDate;
                    salesTotal += s.SalesAmount;
                    i++;
                }

                labelTopMenu.Text = strTopMenu;
                 

            }catch(Exception ex){
                 
            }

        }

        private void gentxtTopZone()
        {
            List<SalesToday> topMenus = gd.getSalesToday(4, this.fromDate, this.toDate, this.shiftID);

            string strTopMenu = "";
            int i = 1;

            this.salesTotal = 0;

            foreach (SalesToday s in topMenus)
            {
                strTopMenu += i.ToString() + ". " + s.SalesLable + " (" + s.SalesUnit.ToString("###,###.##") + ") :: " + s.SalesAmount.ToString("###,###.##") + " B." + "\n";
                this.salesTotal += s.SalesAmount;
                i++;
            }

            labelTopZone.Text = strTopMenu;

            labelTotalSales.Text = "Total Sales : " + this.salesTotal.ToString("###,###.##") + " B.";

        }

        private void gentxtTopTable()
        {
            List<SalesToday> topMenus = gd.getSalesToday(5, this.fromDate, this.toDate, this.shiftID);

            string strTopMenu = "";
            int i = 1;


            foreach (SalesToday s in topMenus)
            {
                strTopMenu += i.ToString() + ". " + s.SalesLable + " (" + s.SalesUnit.ToString("###,###.##") + "): " + " \n\r   >>> " + s.SalesAmount.ToString("###,###.##") + " B." + "\n";
                this.salesTotal += s.SalesAmount;
                i++;
            }

            labelTopTable.Text = strTopMenu;

        }


        private void gentxtTopPayment()
        {
            List<SalesToday> topMenus = gd.getSalesToday(6, this.fromDate, this.toDate, this.shiftID);

            string strTopMenu = "";
            int i = 1;


            foreach (SalesToday s in topMenus)
            {
                strTopMenu += i.ToString() + ". " + s.SalesLable + " (" + s.SalesUnit.ToString("###,###.##") + ") :: " + s.SalesAmount.ToString("###,###.##") + " B." + "\n";
                this.salesTotal += s.SalesAmount;
                i++;
            }

            labelTopPayment.Text = strTopMenu;

        }

        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
           genSalesValues();
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
           genSalesValues();
        }

        private void radioBoxTotal_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Value = DateTime.Now;
            dateTimePickerEnd.Value = DateTime.Now;
        }

        private void radioBoxThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Value = DateTimeDayOfMonthExtensions.FirstDayOfMonth_AddMethod(DateTime.Now);
            dateTimePickerEnd.Value = DateTime.Now;
        }

        int shiftID = -1;

        private void comboBoxShiftID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.shiftID = Int32.Parse(comboBoxShiftID.SelectedValue.ToString());
                genSalesValues();
            }
            catch( Exception ex)
            {
                
            }
        }
    }
}
