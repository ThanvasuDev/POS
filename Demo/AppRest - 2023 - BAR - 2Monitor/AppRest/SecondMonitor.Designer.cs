namespace AppRest
{
    partial class SecondMonitor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecondMonitor));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
             


            this.textBoxVAT = new System.Windows.Forms.TextBox();
            this.textBoxServiceCharge = new System.Windows.Forms.TextBox();
            this.textBoxSalesTotal = new System.Windows.Forms.TextBox();
            this.textBoxSubTotal = new System.Windows.Forms.TextBox();
            this.textBoxDiscountTotal = new System.Windows.Forms.TextBox();
            this.panelOrder = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
       //     this.wmpVideo = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panelOrder.SuspendLayout();
        //    ((System.ComponentModel.ISupportInitialize)(this.wmpVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
         //   this.splitContainer1.Panel1.Controls.Add(this.wmpVideo);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.splitContainer1.Panel2.Controls.Add(this.panelSummary);
            this.splitContainer1.Panel2.Controls.Add(this.panelOrder);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 729);
            this.splitContainer1.SplitterDistance = 452;
            this.splitContainer1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(452, 729);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.Silver;
            this.panelSummary.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSummary.Controls.Add(this.splitContainer2);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSummary.Location = new System.Drawing.Point(0, 531);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(552, 198);
            this.panelSummary.TabIndex = 15;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.label11);
            this.splitContainer2.Panel1.Controls.Add(this.label10);
            this.splitContainer2.Panel1.Controls.Add(this.label8);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textBoxVAT);
            this.splitContainer2.Panel2.Controls.Add(this.textBoxServiceCharge);
            this.splitContainer2.Panel2.Controls.Add(this.textBoxSalesTotal);
            this.splitContainer2.Panel2.Controls.Add(this.textBoxSubTotal);
            this.splitContainer2.Panel2.Controls.Add(this.textBoxDiscountTotal);
            this.splitContainer2.Size = new System.Drawing.Size(548, 194);
            this.splitContainer2.SplitterDistance = 258;
            this.splitContainer2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(154, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 26);
            this.label2.TabIndex = 18;
            this.label2.Text = "VAT 7%";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(22, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 26);
            this.label1.TabIndex = 17;
            this.label1.Text = "Service Charge 10%";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(134, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 26);
            this.label11.TabIndex = 16;
            this.label11.Text = "รวมเป็นเงิน";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.Location = new System.Drawing.Point(3, 151);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(249, 31);
            this.label10.TabIndex = 11;
            this.label10.Text = "ลูกค้าต้องชำระทั้งหมด";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(175, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 26);
            this.label8.TabIndex = 6;
            this.label8.Text = "ส่วนลด";
            // 
            // textBoxVAT
            // 
            this.textBoxVAT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVAT.BackColor = System.Drawing.Color.White;
            this.textBoxVAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBoxVAT.ForeColor = System.Drawing.SystemColors.MenuText;
            this.textBoxVAT.Location = new System.Drawing.Point(9, 116);
            this.textBoxVAT.Name = "textBoxVAT";
            this.textBoxVAT.ReadOnly = true;
            this.textBoxVAT.Size = new System.Drawing.Size(268, 32);
            this.textBoxVAT.TabIndex = 17;
            this.textBoxVAT.Text = "0";
            this.textBoxVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxServiceCharge
            // 
            this.textBoxServiceCharge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServiceCharge.BackColor = System.Drawing.Color.White;
            this.textBoxServiceCharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBoxServiceCharge.ForeColor = System.Drawing.SystemColors.MenuText;
            this.textBoxServiceCharge.Location = new System.Drawing.Point(9, 80);
            this.textBoxServiceCharge.Name = "textBoxServiceCharge";
            this.textBoxServiceCharge.ReadOnly = true;
            this.textBoxServiceCharge.Size = new System.Drawing.Size(268, 32);
            this.textBoxServiceCharge.TabIndex = 16;
            this.textBoxServiceCharge.Text = "0";
            this.textBoxServiceCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxSalesTotal
            // 
            this.textBoxSalesTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSalesTotal.BackColor = System.Drawing.Color.Blue;
            this.textBoxSalesTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBoxSalesTotal.ForeColor = System.Drawing.Color.White;
            this.textBoxSalesTotal.Location = new System.Drawing.Point(10, 151);
            this.textBoxSalesTotal.Name = "textBoxSalesTotal";
            this.textBoxSalesTotal.ReadOnly = true;
            this.textBoxSalesTotal.Size = new System.Drawing.Size(267, 38);
            this.textBoxSalesTotal.TabIndex = 12;
            this.textBoxSalesTotal.Text = "0";
            this.textBoxSalesTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxSubTotal
            // 
            this.textBoxSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSubTotal.BackColor = System.Drawing.Color.White;
            this.textBoxSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBoxSubTotal.ForeColor = System.Drawing.SystemColors.MenuText;
            this.textBoxSubTotal.Location = new System.Drawing.Point(9, 9);
            this.textBoxSubTotal.Name = "textBoxSubTotal";
            this.textBoxSubTotal.ReadOnly = true;
            this.textBoxSubTotal.Size = new System.Drawing.Size(268, 32);
            this.textBoxSubTotal.TabIndex = 15;
            this.textBoxSubTotal.Text = "0";
            this.textBoxSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxDiscountTotal
            // 
            this.textBoxDiscountTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDiscountTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textBoxDiscountTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBoxDiscountTotal.ForeColor = System.Drawing.SystemColors.MenuText;
            this.textBoxDiscountTotal.Location = new System.Drawing.Point(10, 44);
            this.textBoxDiscountTotal.Name = "textBoxDiscountTotal";
            this.textBoxDiscountTotal.ReadOnly = true;
            this.textBoxDiscountTotal.Size = new System.Drawing.Size(267, 32);
            this.textBoxDiscountTotal.TabIndex = 10;
            this.textBoxDiscountTotal.Text = "0";
            this.textBoxDiscountTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panelOrder
            // 
            this.panelOrder.AutoScroll = true;
            this.panelOrder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelOrder.BackColor = System.Drawing.Color.Black;
            this.panelOrder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelOrder.Controls.Add(this.listView1);
            this.panelOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrder.ImeMode = System.Windows.Forms.ImeMode.Close;
            this.panelOrder.Location = new System.Drawing.Point(0, 0);
            this.panelOrder.Name = "panelOrder";
            this.panelOrder.Size = new System.Drawing.Size(552, 729);
            this.panelOrder.TabIndex = 16;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Margin = new System.Windows.Forms.Padding(24);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(548, 725);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 2500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // wmpVideo
            // 
            //this.wmpVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.wmpVideo.Enabled = true;
            //this.wmpVideo.Location = new System.Drawing.Point(0, 0);
            //this.wmpVideo.Name = "wmpVideo";
            //this.wmpVideo.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmpVideo.OcxState")));
            //this.wmpVideo.Size = new System.Drawing.Size(452, 729);
            //this.wmpVideo.TabIndex = 1;
            // 
            // SecondMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.splitContainer1);
            this.Name = "SecondMonitor";
            this.Text = "SecondMonitor";
            this.Load += new System.EventHandler(this.SecondMonitor_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelSummary.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panelOrder.ResumeLayout(false);
        //    ((System.ComponentModel.ISupportInitialize)(this.wmpVideo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxSubTotal;
        private System.Windows.Forms.TextBox textBoxDiscountTotal;
        private System.Windows.Forms.TextBox textBoxSalesTotal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panelOrder;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxVAT;
        private System.Windows.Forms.TextBox textBoxServiceCharge;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;

        private System.Windows.Forms.Label LabelOrder;
        // private AxWMPLib.AxWindowsMediaPlayer wmpVideo;
    }
}