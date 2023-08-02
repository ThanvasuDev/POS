using System;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace AppRest
{
    public partial class MainManage : MainTemplateS
    {
        AddMember formAddMember;
        AddCustomer formAddCustomer;
        AddProduct formAddProduct;
        AddCat formAddCategory;
        AddTable formAddTable;
        AddCustPayment formAddCustPayment;
        
        AddMemCard formAddMemCard;
        AddPromotion formAddPromotion;
        AddMemCardRenew formAddMemCardRenew;
        AddCoupon formAddCoupon;
        AddBillRemark formAddBillRemark;

        AddZone formAddZone;

        AddStore formAddStore;
        AddStock formAddStock;
        AddMapSS formAddMapSS;
        AddStoreCat formAddStoreCat;

        AddMemPrice formAddMemPrice;

        string cashDrawerPassword;
        string printerCashName;

        AddInven formAddInven;
        AddSupplier formAddSupplier;
        AddPO formAddPO;

        AddProm formAddProm;

        AddProductBarcodeSS formAddProductBarcodeSS;


        public MainManage(Form frmlkFrom, int flagFrmClose)
        {
            InitializeComponent();
            if (flagFrmClose == 1)
            {
             
                frmlkFrom.Dispose();
            }

            buttonLinkManage.BackColor = System.Drawing.Color.Gray;
            cashDrawerPassword = ConfigurationSettings.AppSettings["CashDrawerPass"];
            printerCashName = ConfigurationSettings.AppSettings["PrinterNameCash"].ToString();
            printDocument1.PrinterSettings.PrinterName = this.printerCashName;


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
        }

        private void buttonAddMember_Click(object sender, EventArgs e)
        {
            LinkFormAddMember();
        }
         

        private void LinkFormAddMember()
        {
           // Cursor.Current = Cursors.WaitCursor;
            if (formAddMember == null)
            {
                formAddMember = new AddMember(this, 0);
            }
          //  Cursor.Current = Cursors.Default;
            if (formAddMember.ShowDialog() == DialogResult.OK)
            {
                formAddMember.Dispose();
                formAddMember = null;
            }
        }

        private void buttonAddMenu_Click(object sender, EventArgs e)
        {
            LinkFormAddProduct();
        }


        private void LinkFormAddProduct()
        {
          //  Cursor.Current = Cursors.WaitCursor;
            if (formAddProduct == null)
            {
                formAddProduct = new AddProduct(this, 0);
            }
           // Cursor.Current = Cursors.Default;
            if (formAddProduct.ShowDialog() == DialogResult.OK)
            {
                formAddProduct.Dispose();
                formAddProduct = null;
            }
        }

        private void buttonCategory_Click(object sender, EventArgs e)
        {
            LinkFormAddCategory();
        }

        private void LinkFormAddCategory()
        {
           // Cursor.Current = Cursors.WaitCursor;
            if (formAddCategory == null)
            {
                formAddCategory = new AddCat(this, 0);
            }
            //Cursor.Current = Cursors.Default;
            if (formAddCategory.ShowDialog() == DialogResult.OK)
            {
                formAddCategory.Dispose();
                formAddCategory = null;
            }


        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {
            LinkFormAddTable();
        }

        private void LinkFormAddTable()
        {
           // Cursor.Current = Cursors.WaitCursor;
            if (formAddTable == null)
            {
                formAddTable = new AddTable(this, 0);
            }
          //  Cursor.Current = Cursors.Default;
            if (formAddTable.ShowDialog() == DialogResult.OK)
            {
                formAddTable.Dispose();
                formAddTable = null;
            }
        }
           

        private void buttonAddCreditCust_Click(object sender, EventArgs e)
        {
             LinkFormAddCustPayment();
        }

        private void LinkFormAddCustPayment()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddCustPayment == null)
            {
                formAddCustPayment = new AddCustPayment(this, 0,0);
            }
           //   Cursor.Current = Cursors.Default;
            if (formAddCustPayment.ShowDialog() == DialogResult.OK)
            {
                formAddCustPayment.Dispose();
                formAddCustPayment = null;
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

        private void buttonOpenCashDrawer_Click(object sender, EventArgs e)
        {

        }

        private void print(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Brush brush = new SolidBrush(Color.Black);
            Font fontBody = new Font("Tahoma", 8);

            e.Graphics.DrawString(" ", fontBody, brush, 0, 0); 
        }

        private void KeyPassFinish(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)13)
            //{
            //    this.buttonOpenCashDrawer_Click(txtBoxCashDrawerPass, e);
            //}
        }

        private void buttonAddMemCard_Click(object sender, EventArgs e)
        {
            LinkFormAddMemCard();
        }

        private void LinkFormAddMemCard()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddMemCard == null)
            {
                formAddMemCard = new AddMemCard(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddMemCard.ShowDialog() == DialogResult.OK)
            {
                formAddMemCard.Dispose();
                formAddMemCard = null;
            }
        }

        private void buttonAddPromotion_Click(object sender, EventArgs e)
        {
            LinkFormAddPromotion();
        }

        private void LinkFormAddPromotion()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddPromotion == null)
            {
                formAddPromotion = new AddPromotion(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddPromotion.ShowDialog() == DialogResult.OK)
            {
                formAddPromotion.Dispose();
                formAddPromotion = null;
            }
        }

        private void buttonMemCardRenew_Click(object sender, EventArgs e)
        {
            LinkFormAddMemCardRenew();
        }

        private void LinkFormAddMemCardRenew()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddMemCardRenew == null)
            {
                formAddMemCardRenew = new  AddMemCardRenew(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddMemCardRenew.ShowDialog() == DialogResult.OK)
            {
                formAddMemCardRenew.Dispose();
                formAddMemCardRenew = null;
            }
        }

        private void buttonEndDays_Click(object sender, EventArgs e)
        {
            LinkFormAddMemPrice();
        }

        private void LinkFormAddMemPrice()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddMemPrice == null)
            {
                formAddMemPrice = new AddMemPrice(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddMemPrice.ShowDialog() == DialogResult.OK)
            {
                formAddMemPrice.Dispose();
                formAddMemPrice = null;
            }
        }

        private void buttonAddCoupon_Click(object sender, EventArgs e)
        {
            LinkFormAddCoupon();
        }

        private void LinkFormAddCoupon()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddCoupon == null)
            {
                formAddCoupon = new AddCoupon(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddCoupon.ShowDialog() == DialogResult.OK)
            {
                formAddCoupon.Dispose();
                formAddCoupon = null;
            }
        }

        private void buttonAddBillRemark_Click(object sender, EventArgs e)
        {
            LinkFormAddBillRemark();
        }

        private void LinkFormAddBillRemark()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddBillRemark == null)
            {
                formAddBillRemark = new AddBillRemark(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddBillRemark.ShowDialog() == DialogResult.OK)
            {
                formAddBillRemark.Dispose();
                formAddBillRemark = null;
            }
        }

        private void buttonAddZone_Click(object sender, EventArgs e)
        {
            LinkFormAddZone();
        }

        private void LinkFormAddZone()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddZone == null)
            {
                formAddZone = new AddZone(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddZone.ShowDialog() == DialogResult.OK)
            {
                formAddZone.Dispose();
                formAddZone = null;
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

        private void buttonMatchSS_Click(object sender, EventArgs e)
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

        private void buttonAddPO_Click(object sender, EventArgs e)
        {
            LinkFormAddPO();
        }

        private void LinkFormAddPO()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddPO == null)
            {
                formAddPO = new AddPO(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddPO.ShowDialog() == DialogResult.OK)
            {
                formAddPO.Dispose();
                formAddPO = null;
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

        private void LinkFormAddInven()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddInven == null)
            {
                formAddInven = new AddInven(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddInven.ShowDialog() == DialogResult.OK)
            {
                formAddInven.Dispose();
                formAddInven = null;
            }
        }

        private void buttonAddProm_Click(object sender, EventArgs e)
        {
            LinkFormAddProm();
        }

        private void LinkFormAddProm()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddProm == null)
            {
                formAddProm = new AddProm(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddProm.ShowDialog() == DialogResult.OK)
            {
                formAddProm.Dispose();
                formAddProm = null;
            }
        }

        private void buttonAddPrintBarProduct_Click(object sender, EventArgs e)
        {
            LinkFormAddProductBarcode();
        } 

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

        private void buttonAddCustomer_Click(object sender, EventArgs e)
        {
            LinkFormAddCustomer();
        }

        private void LinkFormAddCustomer()
        {
            //  Cursor.Current = Cursors.WaitCursor;
            if (formAddCustomer == null)
            {
                formAddCustomer = new AddCustomer(this, 0);
            }
            // Cursor.Current = Cursors.Default;
            if (formAddCustomer.ShowDialog() == DialogResult.OK)
            {
                formAddCustomer.Dispose();
                formAddCustomer = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LinkFormMainWork();
        }


        MainWork formAddWork;


        private void LinkFormMainWork()
        {
            //  Cursor.Current = Cursors.WaitCursor;
            if (formAddWork== null)
            {
                formAddWork = new MainWork(this, 0);
            }
            // Cursor.Current = Cursors.Default;
            if (formAddWork.ShowDialog() == DialogResult.OK)
            {
                formAddWork.Dispose();
                formAddWork = null;
            }
        }

        private void buttonAddPC_Purchase_Click(object sender, EventArgs e)
        {
            LinkFormAddPC_Purchase();
        }


        AddPCPurchase formAddPC_Purchase;


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


        AddGRN formAddPCGR;


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

        AddPC_AllRpt formAddPC_Report;

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

        private void buttonGroupCat_Click(object sender, EventArgs e)
        {
            LinkFormAddGroupCat();
        }

        AddGroupCat formAddGroupCat;

        private void LinkFormAddGroupCat()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddGroupCat == null)
            {
                formAddGroupCat = new AddGroupCat(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddGroupCat.ShowDialog() == DialogResult.OK)
            {
                formAddGroupCat.Dispose();
                formAddGroupCat = null;
            }
        }

        private void buttonAddProfitLoss_Click(object sender, EventArgs e)
        {
            LinkFormAddProfitandLoss();
        }

        AddProfitLoss formAddProfitLoss;

        private void LinkFormAddProfitandLoss()
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formAddProfitLoss == null)
            {
                formAddProfitLoss = new AddProfitLoss(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formAddProfitLoss.ShowDialog() == DialogResult.OK)
            {
                formAddProfitLoss.Dispose();
                formAddProfitLoss = null;
            }
        }

        FormTESTAPI formTestAPI;


        private void buttonLinkAPI_Click(object sender, EventArgs e)
        {
            // Cursor.Current = Cursors.WaitCursor;
            if (formTestAPI == null)
            {
                formTestAPI = new FormTESTAPI(this, 0);
            }
            //   Cursor.Current = Cursors.Default;
            if (formTestAPI.ShowDialog() == DialogResult.OK)
            {
                formTestAPI.Dispose();
                formTestAPI = null;
            }
        }


    }
}
