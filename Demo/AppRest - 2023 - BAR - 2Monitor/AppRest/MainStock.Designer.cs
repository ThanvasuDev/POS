namespace AppRest
{
    partial class MainStock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxAllCat = new System.Windows.Forms.ComboBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.comboBoxPeriod = new System.Windows.Forms.ComboBox();
            this.comboBoxDate = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkReCalStock = new System.Windows.Forms.CheckBox();
            this.textBoxSrcStoreName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonScanBarcodeforSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewResult = new System.Windows.Forms.DataGridView();
            this.buttonAddPC_Report = new System.Windows.Forms.Button();
            this.buttonAddPC_Purchase = new System.Windows.Forms.Button();
            this.buttonAddPC_GRN = new System.Windows.Forms.Button();
            this.buttonAddSupplier = new System.Windows.Forms.Button();
            this.buttonAddStock = new System.Windows.Forms.Button();
            this.buttonAddMapSS = new System.Windows.Forms.Button();
            this.buttonAddStore = new System.Windows.Forms.Button();
            this.buttonAddStoreCat = new System.Windows.Forms.Button();
            this.buttonAddTF = new System.Windows.Forms.Button();
            this.buttonAddPrintBarProduct = new System.Windows.Forms.Button();
            this.buttonExportData = new System.Windows.Forms.Button();
            this.buttonReportThermal = new System.Windows.Forms.Button();
            this.printDayThermal = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLinkMain
            // 
            this.buttonLinkMain.FlatAppearance.BorderColor = System.Drawing.Color.Bisque;
            this.buttonLinkMain.FlatAppearance.BorderSize = 50;
            // 
            // buttonLinkManage
            // 
            this.buttonLinkManage.FlatAppearance.BorderColor = System.Drawing.Color.Bisque;
            this.buttonLinkManage.FlatAppearance.BorderSize = 50;
            // 
            // buttonLinkSummary
            // 
            this.buttonLinkSummary.FlatAppearance.BorderColor = System.Drawing.Color.Bisque;
            this.buttonLinkSummary.FlatAppearance.BorderSize = 50;
            // 
            // buttonLinkCashCard
            // 
            this.buttonLinkCashCard.FlatAppearance.BorderColor = System.Drawing.Color.Bisque;
            this.buttonLinkCashCard.FlatAppearance.BorderSize = 50;
            // 
            // buttonLinkStock
            // 
            this.buttonLinkStock.FlatAppearance.BorderColor = System.Drawing.Color.Bisque;
            this.buttonLinkStock.FlatAppearance.BorderSize = 50;
            // 
            // buttonLinkOrder
            // 
            this.buttonLinkOrder.FlatAppearance.BorderColor = System.Drawing.Color.Bisque;
            this.buttonLinkOrder.FlatAppearance.BorderSize = 50;
            // 
            // buttonLinkSales
            // 
            this.buttonLinkSales.FlatAppearance.BorderColor = System.Drawing.Color.Bisque;
            this.buttonLinkSales.FlatAppearance.BorderSize = 50;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(198, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 32);
            this.label2.TabIndex = 13;
            this.label2.Text = "ข้อมูลสรุปคลังสินค้า";
            // 
            // comboBoxAllCat
            // 
            this.comboBoxAllCat.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.comboBoxAllCat.FormattingEnabled = true;
            this.comboBoxAllCat.Location = new System.Drawing.Point(581, 45);
            this.comboBoxAllCat.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxAllCat.Name = "comboBoxAllCat";
            this.comboBoxAllCat.Size = new System.Drawing.Size(230, 36);
            this.comboBoxAllCat.TabIndex = 29;
            this.comboBoxAllCat.Text = "==== Store Category ====";
            this.comboBoxAllCat.SelectedIndexChanged += new System.EventHandler(this.comboBoxStatus_SelectedIndexChanged);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(581, 89);
            this.comboBoxStatus.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(230, 36);
            this.comboBoxStatus.TabIndex = 30;
            this.comboBoxStatus.Text = "==== Stock Status ====";
            this.comboBoxStatus.SelectedIndexChanged += new System.EventHandler(this.comboBoxStatus_SelectedIndexChanged);
            // 
            // comboBoxPeriod
            // 
            this.comboBoxPeriod.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.comboBoxPeriod.FormattingEnabled = true;
            this.comboBoxPeriod.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBoxPeriod.Items.AddRange(new object[] {
            "==== Select Type ====",
            "Master",
            "Visa",
            "AMEX",
            "Other"});
            this.comboBoxPeriod.Location = new System.Drawing.Point(204, 45);
            this.comboBoxPeriod.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxPeriod.Name = "comboBoxPeriod";
            this.comboBoxPeriod.Size = new System.Drawing.Size(192, 36);
            this.comboBoxPeriod.TabIndex = 33;
            this.comboBoxPeriod.Text = "Select Period";
            this.comboBoxPeriod.SelectionChangeCommitted += new System.EventHandler(this.comboBoxPeriod_SelectionChangeCommitted);
            // 
            // comboBoxDate
            // 
            this.comboBoxDate.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.comboBoxDate.FormattingEnabled = true;
            this.comboBoxDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBoxDate.Items.AddRange(new object[] {
            "==== Select Type ====",
            "Master",
            "Visa",
            "AMEX",
            "Other"});
            this.comboBoxDate.Location = new System.Drawing.Point(204, 85);
            this.comboBoxDate.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDate.Name = "comboBoxDate";
            this.comboBoxDate.Size = new System.Drawing.Size(192, 36);
            this.comboBoxDate.TabIndex = 32;
            this.comboBoxDate.Text = "Select Date ";
            this.comboBoxDate.SelectedIndexChanged += new System.EventHandler(this.comboBoxDate_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DimGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(416, 48);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 73);
            this.button1.TabIndex = 34;
            this.button1.Text = "แสดงข้อมูล";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkReCalStock
            // 
            this.checkReCalStock.AutoSize = true;
            this.checkReCalStock.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.checkReCalStock.ForeColor = System.Drawing.Color.White;
            this.checkReCalStock.Location = new System.Drawing.Point(425, 13);
            this.checkReCalStock.Margin = new System.Windows.Forms.Padding(4);
            this.checkReCalStock.Name = "checkReCalStock";
            this.checkReCalStock.Size = new System.Drawing.Size(163, 32);
            this.checkReCalStock.TabIndex = 35;
            this.checkReCalStock.Text = "Refresh Stock";
            this.checkReCalStock.UseVisualStyleBackColor = true;
            // 
            // textBoxSrcStoreName
            // 
            this.textBoxSrcStoreName.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBoxSrcStoreName.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBoxSrcStoreName.ForeColor = System.Drawing.Color.Black;
            this.textBoxSrcStoreName.Location = new System.Drawing.Point(828, 89);
            this.textBoxSrcStoreName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSrcStoreName.Name = "textBoxSrcStoreName";
            this.textBoxSrcStoreName.Size = new System.Drawing.Size(251, 36);
            this.textBoxSrcStoreName.TabIndex = 54;
            this.textBoxSrcStoreName.TextChanged += new System.EventHandler(this.textBoxSrcStoreName_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(848, 48);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(202, 28);
            this.label12.TabIndex = 53;
            this.label12.Text = "Search Name / Code";
            // 
            // buttonScanBarcodeforSearch
            // 
            this.buttonScanBarcodeforSearch.BackColor = System.Drawing.Color.DimGray;
            this.buttonScanBarcodeforSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonScanBarcodeforSearch.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonScanBarcodeforSearch.ForeColor = System.Drawing.Color.White;
            this.buttonScanBarcodeforSearch.Location = new System.Drawing.Point(871, 107);
            this.buttonScanBarcodeforSearch.Margin = new System.Windows.Forms.Padding(4);
            this.buttonScanBarcodeforSearch.Name = "buttonScanBarcodeforSearch";
            this.buttonScanBarcodeforSearch.Size = new System.Drawing.Size(85, 36);
            this.buttonScanBarcodeforSearch.TabIndex = 51;
            this.buttonScanBarcodeforSearch.Text = "Scan";
            this.buttonScanBarcodeforSearch.UseVisualStyleBackColor = false; 
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dataGridViewResult);
            this.panel1.Controls.Add(this.buttonAddTF);
            this.panel1.Controls.Add(this.buttonScanBarcodeforSearch);
            this.panel1.Location = new System.Drawing.Point(200, 133);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1131, 613);
            this.panel1.TabIndex = 12;
            // 
            // dataGridViewResult
            // 
            this.dataGridViewResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewResult.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridViewResult.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewResult.ColumnHeadersHeight = 29;
            this.dataGridViewResult.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridViewResult.Location = new System.Drawing.Point(2, 4);
            this.dataGridViewResult.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewResult.Name = "dataGridViewResult";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewResult.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewResult.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.dataGridViewResult.Size = new System.Drawing.Size(1121, 593);
            this.dataGridViewResult.TabIndex = 3;
            // 
            // buttonAddPC_Report
            // 
            this.buttonAddPC_Report.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonAddPC_Report.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddPC_Report.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonAddPC_Report.ForeColor = System.Drawing.Color.Black;
            this.buttonAddPC_Report.Image = global::AppRest.Properties.Resources.Report;
            this.buttonAddPC_Report.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddPC_Report.Location = new System.Drawing.Point(665, 822);
            this.buttonAddPC_Report.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddPC_Report.Name = "buttonAddPC_Report";
            this.buttonAddPC_Report.Size = new System.Drawing.Size(190, 60);
            this.buttonAddPC_Report.TabIndex = 76;
            this.buttonAddPC_Report.Text = "สรุปการสั่งซื้อสินค้า\r\nPurchasing";
            this.buttonAddPC_Report.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddPC_Report.UseVisualStyleBackColor = false;
            this.buttonAddPC_Report.Click += new System.EventHandler(this.buttonAddPC_Report_Click);
            // 
            // buttonAddPC_Purchase
            // 
            this.buttonAddPC_Purchase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonAddPC_Purchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddPC_Purchase.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonAddPC_Purchase.ForeColor = System.Drawing.Color.Black;
            this.buttonAddPC_Purchase.Image = global::AppRest.Properties.Resources.Document;
            this.buttonAddPC_Purchase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddPC_Purchase.Location = new System.Drawing.Point(1094, 822);
            this.buttonAddPC_Purchase.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddPC_Purchase.Name = "buttonAddPC_Purchase";
            this.buttonAddPC_Purchase.Size = new System.Drawing.Size(190, 60);
            this.buttonAddPC_Purchase.TabIndex = 75;
            this.buttonAddPC_Purchase.Text = "ใบขอซื้อ / ใบสั่งซื้อ\r\nPR / PO Paper\r\n";
            this.buttonAddPC_Purchase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddPC_Purchase.UseVisualStyleBackColor = false;
            this.buttonAddPC_Purchase.Click += new System.EventHandler(this.buttonAddPC_Purchase_Click);
            // 
            // buttonAddPC_GRN
            // 
            this.buttonAddPC_GRN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonAddPC_GRN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddPC_GRN.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonAddPC_GRN.ForeColor = System.Drawing.Color.Black;
            this.buttonAddPC_GRN.Image = global::AppRest.Properties.Resources.Identification_Documents;
            this.buttonAddPC_GRN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddPC_GRN.Location = new System.Drawing.Point(880, 822);
            this.buttonAddPC_GRN.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddPC_GRN.Name = "buttonAddPC_GRN";
            this.buttonAddPC_GRN.Size = new System.Drawing.Size(190, 60);
            this.buttonAddPC_GRN.TabIndex = 74;
            this.buttonAddPC_GRN.Text = "ใบรับสินค้า\r\nGoods Receipt";
            this.buttonAddPC_GRN.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddPC_GRN.UseVisualStyleBackColor = false;
            this.buttonAddPC_GRN.Click += new System.EventHandler(this.buttonAddPC_GRN_Click);
            // 
            // buttonAddSupplier
            // 
            this.buttonAddSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonAddSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddSupplier.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonAddSupplier.ForeColor = System.Drawing.Color.Black;
            this.buttonAddSupplier.Image = global::AppRest.Properties.Resources.Group;
            this.buttonAddSupplier.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddSupplier.Location = new System.Drawing.Point(449, 822);
            this.buttonAddSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddSupplier.Name = "buttonAddSupplier";
            this.buttonAddSupplier.Size = new System.Drawing.Size(190, 60);
            this.buttonAddSupplier.TabIndex = 73;
            this.buttonAddSupplier.Text = "ผู้จำหน่าย\r\nSupplier\r\n";
            this.buttonAddSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddSupplier.UseVisualStyleBackColor = false;
            this.buttonAddSupplier.Click += new System.EventHandler(this.buttonAddSupplier_Click);
            // 
            // buttonAddStock
            // 
            this.buttonAddStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonAddStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddStock.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonAddStock.ForeColor = System.Drawing.Color.Black;
            this.buttonAddStock.Image = global::AppRest.Properties.Resources.Stock;
            this.buttonAddStock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddStock.Location = new System.Drawing.Point(665, 754);
            this.buttonAddStock.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddStock.Name = "buttonAddStock";
            this.buttonAddStock.Size = new System.Drawing.Size(190, 60);
            this.buttonAddStock.TabIndex = 72;
            this.buttonAddStock.Text = "นำเข้าคลังสินค้า\r\nStock Moving\r\n";
            this.buttonAddStock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddStock.UseVisualStyleBackColor = false;
            this.buttonAddStock.Click += new System.EventHandler(this.buttonAddStock_Click);
            // 
            // buttonAddMapSS
            // 
            this.buttonAddMapSS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonAddMapSS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddMapSS.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonAddMapSS.ForeColor = System.Drawing.Color.Black;
            this.buttonAddMapSS.Image = global::AppRest.Properties.Resources.Download;
            this.buttonAddMapSS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddMapSS.Location = new System.Drawing.Point(880, 754);
            this.buttonAddMapSS.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddMapSS.Name = "buttonAddMapSS";
            this.buttonAddMapSS.Size = new System.Drawing.Size(190, 60);
            this.buttonAddMapSS.TabIndex = 71;
            this.buttonAddMapSS.Text = "Map คลังสินค้า\r\nMatch Stock";
            this.buttonAddMapSS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddMapSS.UseVisualStyleBackColor = false;
            this.buttonAddMapSS.Click += new System.EventHandler(this.buttonAddMapSS_Click);
            // 
            // buttonAddStore
            // 
            this.buttonAddStore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonAddStore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddStore.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonAddStore.ForeColor = System.Drawing.Color.Black;
            this.buttonAddStore.Image = global::AppRest.Properties.Resources.Pay_Customer;
            this.buttonAddStore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddStore.Location = new System.Drawing.Point(1094, 754);
            this.buttonAddStore.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddStore.Name = "buttonAddStore";
            this.buttonAddStore.Size = new System.Drawing.Size(190, 60);
            this.buttonAddStore.TabIndex = 70;
            this.buttonAddStore.Text = "คลังสินค้า\r\nInventory\r\n";
            this.buttonAddStore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddStore.UseVisualStyleBackColor = false;
            this.buttonAddStore.Click += new System.EventHandler(this.buttonAddStore_Click);
            // 
            // buttonAddStoreCat
            // 
            this.buttonAddStoreCat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonAddStoreCat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddStoreCat.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonAddStoreCat.ForeColor = System.Drawing.Color.Black;
            this.buttonAddStoreCat.Image = global::AppRest.Properties.Resources.Factory;
            this.buttonAddStoreCat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddStoreCat.Location = new System.Drawing.Point(449, 754);
            this.buttonAddStoreCat.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddStoreCat.Name = "buttonAddStoreCat";
            this.buttonAddStoreCat.Size = new System.Drawing.Size(190, 60);
            this.buttonAddStoreCat.TabIndex = 69;
            this.buttonAddStoreCat.Text = "กลุ่มคลังสินค้า\r\nInventory Group\r\n";
            this.buttonAddStoreCat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddStoreCat.UseVisualStyleBackColor = false;
            this.buttonAddStoreCat.Click += new System.EventHandler(this.buttonAddStoreCat_Click);
            // 
            // buttonAddTF
            // 
            this.buttonAddTF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonAddTF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddTF.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonAddTF.ForeColor = System.Drawing.Color.Black;
            this.buttonAddTF.Image = global::AppRest.Properties.Resources.Sales_Performance;
            this.buttonAddTF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddTF.Location = new System.Drawing.Point(458, 138);
            this.buttonAddTF.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddTF.Name = "buttonAddTF";
            this.buttonAddTF.Size = new System.Drawing.Size(190, 60);
            this.buttonAddTF.TabIndex = 77;
            this.buttonAddTF.Text = "เบิกคลังสินค้า\r\nTransfer Stock\r\n";
            this.buttonAddTF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddTF.UseVisualStyleBackColor = false;
            this.buttonAddTF.Visible = false;
            this.buttonAddTF.Click += new System.EventHandler(this.buttonAddTF_Click);
            // 
            // buttonAddPrintBarProduct
            // 
            this.buttonAddPrintBarProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonAddPrintBarProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddPrintBarProduct.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonAddPrintBarProduct.ForeColor = System.Drawing.Color.Black;
            this.buttonAddPrintBarProduct.Image = global::AppRest.Properties.Resources.Barcode_Scanner;
            this.buttonAddPrintBarProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddPrintBarProduct.Location = new System.Drawing.Point(236, 792);
            this.buttonAddPrintBarProduct.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddPrintBarProduct.Name = "buttonAddPrintBarProduct";
            this.buttonAddPrintBarProduct.Size = new System.Drawing.Size(190, 60);
            this.buttonAddPrintBarProduct.TabIndex = 70;
            this.buttonAddPrintBarProduct.Text = "พิมพ์บาร์โค๊ดสินค้า\r\nBarcode Printing\r\n";
            this.buttonAddPrintBarProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddPrintBarProduct.UseVisualStyleBackColor = false;
            this.buttonAddPrintBarProduct.Click += new System.EventHandler(this.buttonAddPrintBarProduct_Click);
            // 
            // buttonExportData
            // 
            this.buttonExportData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonExportData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExportData.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonExportData.ForeColor = System.Drawing.Color.Black;
            this.buttonExportData.Image = global::AppRest.Properties.Resources.Microsoft_Excel;
            this.buttonExportData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExportData.Location = new System.Drawing.Point(1110, 4);
            this.buttonExportData.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExportData.Name = "buttonExportData";
            this.buttonExportData.Size = new System.Drawing.Size(190, 60);
            this.buttonExportData.TabIndex = 109;
            this.buttonExportData.Text = "Export";
            this.buttonExportData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExportData.UseVisualStyleBackColor = false;
            this.buttonExportData.Click += new System.EventHandler(this.buttonExportToExcel_Click);
            // 
            // buttonReportThermal
            // 
            this.buttonReportThermal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonReportThermal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReportThermal.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonReportThermal.ForeColor = System.Drawing.Color.Black;
            this.buttonReportThermal.Image = global::AppRest.Properties.Resources.Print;
            this.buttonReportThermal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonReportThermal.Location = new System.Drawing.Point(1110, 65);
            this.buttonReportThermal.Margin = new System.Windows.Forms.Padding(4);
            this.buttonReportThermal.Name = "buttonReportThermal";
            this.buttonReportThermal.Size = new System.Drawing.Size(190, 60);
            this.buttonReportThermal.TabIndex = 110;
            this.buttonReportThermal.Text = "Balance";
            this.buttonReportThermal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonReportThermal.UseVisualStyleBackColor = false;
            this.buttonReportThermal.Click += new System.EventHandler(this.buttonReportThermal_Click);
            // 
            // printDayThermal
            // 
            this.printDayThermal.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDayThermal_PrintPage);
            // 
            // MainStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 891);
            this.Controls.Add(this.buttonReportThermal);
            this.Controls.Add(this.buttonAddPrintBarProduct);
            this.Controls.Add(this.buttonExportData);
            this.Controls.Add(this.buttonAddPC_Report);
            this.Controls.Add(this.buttonAddPC_Purchase);
            this.Controls.Add(this.buttonAddPC_GRN);
            this.Controls.Add(this.buttonAddSupplier);
            this.Controls.Add(this.buttonAddStock);
            this.Controls.Add(this.buttonAddMapSS);
            this.Controls.Add(this.buttonAddStore);
            this.Controls.Add(this.buttonAddStoreCat);
            this.Controls.Add(this.textBoxSrcStoreName);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.checkReCalStock);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBoxPeriod);
            this.Controls.Add(this.comboBoxDate);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.comboBoxAllCat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MainStock";
            this.Text = "Main Stock";
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.comboBoxAllCat, 0);
            this.Controls.SetChildIndex(this.comboBoxStatus, 0);
            this.Controls.SetChildIndex(this.comboBoxDate, 0);
            this.Controls.SetChildIndex(this.comboBoxPeriod, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.checkReCalStock, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.textBoxSrcStoreName, 0);
            this.Controls.SetChildIndex(this.buttonAddStoreCat, 0);
            this.Controls.SetChildIndex(this.buttonAddStore, 0);
            this.Controls.SetChildIndex(this.buttonAddMapSS, 0);
            this.Controls.SetChildIndex(this.buttonAddStock, 0);
            this.Controls.SetChildIndex(this.buttonAddSupplier, 0);
            this.Controls.SetChildIndex(this.buttonAddPC_GRN, 0);
            this.Controls.SetChildIndex(this.buttonAddPC_Purchase, 0);
            this.Controls.SetChildIndex(this.buttonAddPC_Report, 0);
            this.Controls.SetChildIndex(this.buttonExportData, 0);
            this.Controls.SetChildIndex(this.buttonAddPrintBarProduct, 0);
            this.Controls.SetChildIndex(this.buttonReportThermal, 0);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridViewResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxAllCat;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.ComboBox comboBoxPeriod;
        private System.Windows.Forms.ComboBox comboBoxDate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkReCalStock;
        private System.Windows.Forms.TextBox textBoxSrcStoreName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonScanBarcodeforSearch;
        private System.Windows.Forms.Button buttonAddPC_Report;
        private System.Windows.Forms.Button buttonAddPC_Purchase;
        private System.Windows.Forms.Button buttonAddPC_GRN;
        private System.Windows.Forms.Button buttonAddSupplier;
        private System.Windows.Forms.Button buttonAddStock;
        private System.Windows.Forms.Button buttonAddMapSS;
        private System.Windows.Forms.Button buttonAddStore;
        private System.Windows.Forms.Button buttonAddStoreCat;
        private System.Windows.Forms.Button buttonAddTF;
        private System.Windows.Forms.Button buttonAddPrintBarProduct;
        private System.Windows.Forms.Button buttonExportData;
        private System.Windows.Forms.Button buttonReportThermal;
        private System.Drawing.Printing.PrintDocument printDayThermal;
    }
}