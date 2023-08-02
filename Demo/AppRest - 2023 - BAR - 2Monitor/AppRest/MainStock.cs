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
    public partial class MainStock : MainTemplateS
    {

        DataTable dataStock;
        DataTable dataStockFull;

        DataTable dataStockForm;
 

        GetDataRest gd;


        string pathExportResultStockName;
        string pathExportResultName;


        List<StoreCat> allStoreCat;

        int selectStoreCat = 0;
        string selectStoreStatus = "0";

        string dateString = "0";


        string trnPeriodSect;
        string trnDateSect;

        TrnMax trnMax;

        List<TrnDate> listPeriods;
        List<TrnDate> listDates;

         
        AddProductBarcodeSS formAddProductBarcodeSS;

        public MainStock(Form frmlkFrom, int flagFrmClose)
        { 
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();

            buttonLinkStock.BackColor = System.Drawing.Color.Gray;

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

            try
            {

                gd = new GetDataRest();


                trnMax = gd.getTrnMax_Stock();

                trnPeriodSect = trnMax.MaxPeriod;
                trnDateSect = trnMax.MaxDate; 

                listPeriods = gd.getTrnPeriod_Stock();
                listDates = gd.getTrnDate_Stock(); 

                getComboAllPeriod();

                dateString = comboBoxDate.SelectedValue.ToString();

                dataStock = gd.getStockAnalyze(dateString, 1 , 0);
 

                dataGridViewResult.DataSource = dataStock;

                dataGridViewResult.Columns[0].Visible = false;
                dataGridViewResult.Columns[1].HeaderText = "วัน";
                dataGridViewResult.Columns[2].Visible = false;
                dataGridViewResult.Columns[3].Visible = false; //.HeaderText = "คลัง";
                dataGridViewResult.Columns[4].Visible = false;
                dataGridViewResult.Columns[5].HeaderText = "หมวด";
                dataGridViewResult.Columns[6].Visible = false;
                dataGridViewResult.Columns[7].HeaderText = "ชื่อคลังสินค้า";
                dataGridViewResult.Columns[8].HeaderText = "เริ่มวัน";
                dataGridViewResult.Columns[9].HeaderText = "นำเข้า";
                dataGridViewResult.Columns[10].HeaderText = "นำออก";
                dataGridViewResult.Columns[11].HeaderText = "รวมตั้งต้น";
                dataGridViewResult.Columns[12].HeaderText = "ขายไป";
                dataGridViewResult.Columns[13].HeaderText = "คงเหลือ";
                dataGridViewResult.Columns[14].HeaderText = "KPI";
                dataGridViewResult.Columns[15].HeaderText = "คงเหลือ (2)";
                dataGridViewResult.Columns[16].HeaderText = "หน่วย (2)";
                dataGridViewResult.Columns[17].HeaderText = "สถานะ";
                dataGridViewResult.Columns[18].Visible = false; // .HeaderText = "InvCode";
                dataGridViewResult.Columns[19].HeaderText = "InvCode";
                dataGridViewResult.Columns[20].HeaderText = "ต้นทุนรวม";

               
                 
                pathExportResultStockName = ConfigurationSettings.AppSettings["PathExportResultStockName"].ToString();
                pathExportResultName = ConfigurationSettings.AppSettings["PathExportResultName"].ToString();

                allStoreCat = gd.getListAllStoreCat();
                getComboAllStoreCat();
                getComboAllStoreStatus();



                 
            }
            catch (Exception ex)
            {
                      
            } 

        }


        private void  refreshDataStock()
        {
            try
            {
                dateString = comboBoxDate.SelectedValue.ToString();
                 

                int checkRecal = 0;


                if (checkReCalStock.Checked == true)
                    checkRecal = 1;

                dataStock = gd.getStockAnalyze(dateString, checkRecal, 0);

               // dataStock = dataStockFull;

                dataGridViewResult.DataSource = dataStock;

                dataGridViewResult.Columns[0].Visible = false;
                dataGridViewResult.Columns[1].HeaderText = "วัน";
                dataGridViewResult.Columns[2].Visible = false;
                dataGridViewResult.Columns[3].Visible = false; //.HeaderText = "คลัง";
                dataGridViewResult.Columns[4].Visible = false;
                dataGridViewResult.Columns[5].HeaderText = "หมวด";
                dataGridViewResult.Columns[6].Visible = false;
                dataGridViewResult.Columns[7].HeaderText = "ชื่อคลังสินค้า";
                dataGridViewResult.Columns[8].HeaderText = "เริ่มวัน";
                dataGridViewResult.Columns[9].HeaderText = "นำเข้า";
                dataGridViewResult.Columns[10].HeaderText = "นำออก";
                dataGridViewResult.Columns[11].HeaderText = "รวมตั้งต้น";
                dataGridViewResult.Columns[12].HeaderText = "ขายไป";
                dataGridViewResult.Columns[13].HeaderText = "คงเหลือ";
                dataGridViewResult.Columns[14].HeaderText = "KPI (หน่วยเล็ก)";
                dataGridViewResult.Columns[15].HeaderText = "คงเหลือ (หน่วยใหญ่)";
                dataGridViewResult.Columns[16].HeaderText = "หน่วยใหญ่";
                dataGridViewResult.Columns[17].HeaderText = "สถานะ";
                dataGridViewResult.Columns[18].Visible = false; // .HeaderText = "InvCode";
                dataGridViewResult.Columns[19].HeaderText = "InvCode";
                dataGridViewResult.Columns[20].HeaderText = "ต้นทุนรวม";

     
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }

        }


      

        private void getComboAllPeriod()
        {
            try
            {

                List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();

                // Add data to the List

                foreach (TrnDate c in listPeriods)
                {
                    data.Add(new KeyValuePair<string, string>(c.PeriodID, c.Period));
                }


                // Clear the combobox
                comboBoxPeriod.DataSource = null;
                comboBoxPeriod.Items.Clear();

                // Bind the combobox
                comboBoxPeriod.DataSource = new BindingSource(data, null);
                comboBoxPeriod.DisplayMember = "Value";
                comboBoxPeriod.ValueMember = "Key";

                comboBoxPeriod.SelectedIndex = 0;
                getComboAllDate();
            }
            catch (Exception Ex)
            {
                // MessageBox.Show(Ex.Message);
            }

        }


        private void getComboAllDate()
        {
            try
            {

                List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();

                // Add data to the List

                string periodID = comboBoxPeriod.SelectedValue.ToString();

                foreach (TrnDate c in listDates)
                {
                    if (periodID == c.PeriodID)
                        data.Add(new KeyValuePair<string, string>(c.DateID, c.Date));
                }


                // Clear the combobox
                comboBoxDate.DataSource = null;
                comboBoxDate.Items.Clear();

                // Bind the combobox
                comboBoxDate.DataSource = new BindingSource(data, null);
                comboBoxDate.DisplayMember = "Value";
                comboBoxDate.ValueMember = "Key";

               // comboBoxDate.SelectedIndex = 0; 
            }
            catch (Exception Ex)
            {
                //  MessageBox.Show(Ex.Message);
            }

        }


        private void getComboAllStoreCat()
        {
            try
            {
                List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

                // Add data to the List
                data.Add(new KeyValuePair<int, string>(0, "==== All Store Category ===="));

                foreach (StoreCat c in allStoreCat)
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

                comboBoxAllCat.SelectedIndex = 0;

                //comboBoxAllCat.SelectedValue = selectedStoreCat; 
            }
            catch (Exception ex)
            {

            }

        } 

        private void getComboAllStoreStatus()
        {
            try
            {
                List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

                // Add data to the List
                data.Add(new KeyValuePair<int, string>(0, "==== All Status ===="));
                data.Add(new KeyValuePair<int, string>(1, "Low Stock"));
                data.Add(new KeyValuePair<int, string>(2, "Balance Stock"));
                data.Add(new KeyValuePair<int, string>(3, "Over Stock"));

               

                // Clear the combobox
                comboBoxStatus.DataSource = null;
                comboBoxStatus.Items.Clear();

                // Bind the combobox
                comboBoxStatus.DataSource = new BindingSource(data, null);
                comboBoxStatus.DisplayMember = "Value";
                comboBoxStatus.ValueMember = "Key";

                comboBoxStatus.SelectedIndex = 0;

                //comboBoxAllCat.SelectedValue = selectedStoreCat; 
            }
            catch (Exception ex)
            {

            }

        }

        private void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportData.Excel_FromDataTable_DT(dataGridViewResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

               

                if ( (comboBoxStatus.SelectedIndex > 0) && (comboBoxAllCat.SelectedIndex > 0) )
                {
                    this.dataStock.DefaultView.RowFilter = string.Format("Status = '{0}' and StoreCatName = '{1}'", comboBoxStatus.Text, comboBoxAllCat.Text);
                }  
                else if  ( (comboBoxStatus.SelectedIndex == 0) && (comboBoxAllCat.SelectedIndex > 0) )
                {
                    this.dataStock.DefaultView.RowFilter = string.Format("StoreCatName = '{0}'", comboBoxAllCat.Text);
                }
                else if ((comboBoxStatus.SelectedIndex > 0) && (comboBoxAllCat.SelectedIndex == 0))
                {
                    this.dataStock.DefaultView.RowFilter = string.Format("Status = '{0}'", comboBoxStatus.Text);
                }
                else
                {
                    this.dataStock.DefaultView.RowFilter = string.Format("StockStart >= 0", 1);  // All
                }


                dataGridViewResult.Columns[0].Visible = false;
                dataGridViewResult.Columns[2].Visible = false;
                dataGridViewResult.Columns[3].Visible = false; //.HeaderText = "คลัง";
                dataGridViewResult.Columns[4].Visible = false;
                dataGridViewResult.Columns[6].Visible = false;
                dataGridViewResult.Columns[18].Visible = false; // .HeaderText = "InvCode";
  

            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            refreshDataStock(); 

        }

 

        private void comboBoxDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshDataStock();
        }

        private void comboBoxPeriod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getComboAllDate();
        }

      


        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = ((DataTable)dgv.DataSource).Copy();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (!column.Visible)
                {
                    dt.Columns.Remove(column.Name);
                }
            }
            return dt;
        }

        private void textBoxSrcStoreName_TextChanged(object sender, EventArgs e)
        {

            try
            {

                string srPName = textBoxSrcStoreName.Text;


                if (srPName.Length > 0)
                {
                    this.dataStock.DefaultView.RowFilter = string.Format("Store like '*{0}*' or  OrderAt like '*{0}*' ", srPName);

                }
                else
                {
                    this.dataStock.DefaultView.RowFilter = string.Format(" 1 = 1 ", srPName);
                }


            }
            catch (Exception ex)
            {

            }


        }

        
         
       

        /// <summary>
        /// link Sheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        AddStoreCat formAddStoreCat;
        AddStore formAddStore;
        AddStock formAddStock;
        AddMapSS formAddMapSS; 
        AddSupplier formAddSupplier;
        AddPCPurchase formAddPC_Purchase;
        AddGRN formAddPCGR;
        AddPC_AllRpt formAddPC_Report;
        AddPO_Branch formAddPO_Branch;

        private void buttonAddStoreCat_Click(object sender, EventArgs e)
        {
            LinkFormStoreCat();
        }

        private void LinkFormStoreCat()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddStoreCat == null)
            {
                formAddStoreCat = new AddStoreCat(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddStoreCat.ShowDialog() == DialogResult.OK)
            {
                formAddStoreCat.Dispose();
                formAddStoreCat = null;
            }
        }

        private void buttonAddStore_Click(object sender, EventArgs e)
        {
            LinkFormAddStore();
        }

        private void LinkFormAddStore()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddStore == null)
            {
                formAddStore = new AddStore(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddStore.ShowDialog() == DialogResult.OK)
            {
                formAddStore.Dispose();
                formAddStore = null;
            }
        }

        private void buttonAddMapSS_Click(object sender, EventArgs e)
        {
            LinkFormAddMapSS();
        }

        private void LinkFormAddMapSS()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddMapSS == null)
            {
                formAddMapSS = new AddMapSS(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddMapSS.ShowDialog() == DialogResult.OK)
            {
                formAddMapSS.Dispose();
                formAddMapSS = null;
            }
        }

        private void buttonAddStock_Click(object sender, EventArgs e)
        {
            LinkFormAddStock();
        }

        private void LinkFormAddStock()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddStock == null)
            {
                formAddStock = new AddStock(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddStock.ShowDialog() == DialogResult.OK)
            {
                formAddStock.Dispose();
                formAddStock = null;
            }
        }

        private void buttonAddSupplier_Click(object sender, EventArgs e)
        {
            LinkFormAddSupplier();
        }

        private void LinkFormAddSupplier()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddSupplier == null)
            {
                formAddSupplier = new AddSupplier(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddSupplier.ShowDialog() == DialogResult.OK)
            {
                formAddSupplier.Dispose();
                formAddSupplier = null;
            }
        }

        private void buttonAddPC_Purchase_Click(object sender, EventArgs e)
        {
            LinkFormAddPC_Purchase();
        }

         


        private void LinkFormAddPC_Purchase()
        {
            //  Cursor.Current = Cursors.WaitCursor;
            if (formAddPC_Purchase == null)
            {
                formAddPC_Purchase = new AddPCPurchase(this, 0);
            }
            // Cursor.Current = Cursors.Default;
            if (formAddPC_Purchase.ShowDialog() == DialogResult.OK)
            {
                formAddPC_Purchase.Dispose();
                formAddPC_Purchase = null;
            }
        }

        private void buttonAddPC_GRN_Click(object sender, EventArgs e)
        {
            LinkFormAddPCGR();
        }
         

        private void LinkFormAddPCGR()
        {
            //  Cursor.Current = Cursors.WaitCursor;
            if (formAddPCGR == null)
            {
                formAddPCGR = new AddGRN(this, 0);
            }
            // Cursor.Current = Cursors.Default;
            if (formAddPCGR.ShowDialog() == DialogResult.OK)
            {
                formAddPCGR.Dispose();
                formAddPCGR = null;
            }
        }

        private void buttonAddPC_Report_Click(object sender, EventArgs e)
        {
            LinkFormAddPC_Report();
        } 

        private void LinkFormAddPC_Report()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddPC_Report == null)
            {
                formAddPC_Report = new AddPC_AllRpt(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddPC_Report.ShowDialog() == DialogResult.OK)
            {
                formAddPC_Report.Dispose();
                formAddPC_Report = null;
            }
        }

        private void buttonAddTF_Click(object sender, EventArgs e)
        {
            LinkFormAddPO_Branch();
        }

        private void LinkFormAddPO_Branch()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddPO_Branch == null)
            {
                formAddPO_Branch = new AddPO_Branch(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddPO_Branch.ShowDialog() == DialogResult.OK)
            {
                formAddPO_Branch.Dispose();
                formAddPO_Branch = null;
            }
        }

        private void buttonAddPrintBarProduct_Click(object sender, EventArgs e)
        {
            LinkFormAddProductBarcode();
        }

      //  AddProductBarcodeSS formAddProductBarcodeSS;

        private void LinkFormAddProductBarcode()
        {
            //  Cursor.Current = Cursors.WaitCursor;
            if (formAddProductBarcodeSS == null)
            {
                formAddProductBarcodeSS = new AddProductBarcodeSS(this, 0);
            }
            // Cursor.Current = Cursors.Default;
            if (formAddProductBarcodeSS.ShowDialog() == DialogResult.OK)
            {
                formAddProductBarcodeSS.Dispose();
                formAddProductBarcodeSS = null;
            }
        }

        int noofPage = 0;
        int pageProductPrint = 0;

        private void buttonReportThermal_Click(object sender, EventArgs e)
        {
            // printDayThermal.Print();

            noofPage = dataGridViewResult.Rows.Count / 50 + 1;

            for (pageProductPrint = 1; pageProductPrint <= noofPage; pageProductPrint++)
                printDayThermal.Print();


        }

        private void printDayThermal_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {

                int x = 0;
                int y = 0;


                Brush brush = new SolidBrush(Color.Black);
                Font fontHeader = new Font("ARAIL", 12);
                Font fontTable = new Font("ARAIL", 11);
                Font fontSubHeader = new Font("ARAIL", 9);
                Font fontBody = new Font("ARAIL", 9);
                Font fontBodylist = new Font("ARAIL", 9);
                Font fontNum = new Font("Consolas", 9);





                Branch branch = gd.getBranchDesc();

                Bitmap imgHeader = global::AppRest.Properties.Resources.Logo_New;




                e.Graphics.DrawString(branch.BranchNameTH, fontTable, brush, x + 0, y);
                y += 25;



                e.Graphics.DrawString("Rpt : Balance Stock", fontSubHeader, brush, x + 10, y);
                y += 15;
                e.Graphics.DrawString("Data Date : " + comboBoxDate.Text, fontSubHeader, brush, x + 10, y);
                y += 15;


                DateTime dt = DateTime.Now;

                string strDate = String.Format("{0:dd/MM/yyyy}", dt);
                string strTime = String.Format("{0:HH:mm:ss}", dt);



                e.Graphics.DrawString("POS # : " + Login.posBranchID, fontSubHeader, brush, x + 10, y);
                y += 15;
                e.Graphics.DrawString("Cashier : " + Login.userName, fontSubHeader, brush, x + 10, y);
                y += 15;
                e.Graphics.DrawString("Print Date : " + strDate, fontSubHeader, brush, x + 10, y);
                y += 15;
                e.Graphics.DrawString("Print Time : " + strTime, fontSubHeader, brush, x + 10, y);
                y += 15;

                //e.Graphics.DrawString("Page : " + pageProductPrint.ToString() + "/" + noofPage.ToString(), fontSubHeader, brush, x + 10, y);
                //y += 15;



                int i = 1;



                string str1 = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";

                string lb = "";
                float val = 0;
                int ii = 1;

                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 15;

                e.Graphics.DrawString("       Inventory                               Balance    Check ", fontSubHeader, brush, x, y);


                y += 15;
                e.Graphics.DrawString("-------------------------------------------------------------------", fontSubHeader, brush, x, y);
                y += 15;



                string invName = "";
                float invBalanceBigQty = 0;
                string bigUnit = "";
                List<string> txtPrint;
                int len = 35;


                int iii = 1;
                int pagestartindex = 0;
                int pageEndindex = 0;

                pagestartindex = 50 * (pageProductPrint - 1) + 1;
                pageEndindex = 50 * pageProductPrint;


                foreach (DataGridViewRow row in dataGridViewResult.Rows)
                {

                    if (iii >= pagestartindex && iii <= pageEndindex)
                    {


                        invName = row.Cells["Store"].Value.ToString();
                        invBalanceBigQty = float.Parse(row.Cells["BalanceBigUnitEndDay"].Value.ToString());
                        bigUnit = row.Cells["BigUnit"].Value.ToString();

                        str1 = i.ToString() + ". " + invName.Trim(); ;

                        str2 = invBalanceBigQty.ToString("###,###.#0");
                        str2 = String.Format("{0,8}", str2);
                        str4 = bigUnit;


                        e.Graphics.DrawString(str2, fontNum, brush, x + 170, y);

                        e.Graphics.DrawString(str4, fontBodylist, brush, x + 230, y);

                        txtPrint = FuncString.WordWrap(str1, len);
                        str1 = "";

                        foreach (string op in txtPrint)
                        {
                            e.Graphics.DrawString(op, fontBodylist, brush, x + 2, y);
                            y += 13;
                        }



                    }

                    i++;
                    iii++;

                }



            }
            catch (Exception ex)
            {


            }
        }

     


 
    }
}
