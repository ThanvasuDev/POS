namespace AppRest
{
    partial class AddStoreCat
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.dataGridViewAllMember = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comboBoxAllCat = new System.Windows.Forms.ComboBox();
            this.radioButtonUpdateData = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.radioButtonAddData = new System.Windows.Forms.RadioButton();
            this.labelHeader = new System.Windows.Forms.Label();
            this.txtBoxDesc = new System.Windows.Forms.TextBox();
            this.txtBoxNameTH = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonAddTable = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllMember)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(-1, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "เพิ่มกลุ่มวัตถุดิบ / คลังสินค้า";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.dataGridViewAllMember);
            this.panel2.Location = new System.Drawing.Point(2, 144);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(614, 538);
            this.panel2.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(213, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(174, 25);
            this.label11.TabIndex = 14;
            this.label11.Text = "All Store Category";
            // 
            // dataGridViewAllMember
            // 
            this.dataGridViewAllMember.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewAllMember.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewAllMember.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAllMember.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridViewAllMember.Location = new System.Drawing.Point(4, 32);
            this.dataGridViewAllMember.Name = "dataGridViewAllMember";
            this.dataGridViewAllMember.Size = new System.Drawing.Size(603, 489);
            this.dataGridViewAllMember.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.labelHeader);
            this.panel1.Controls.Add(this.txtBoxDesc);
            this.panel1.Controls.Add(this.txtBoxNameTH);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.buttonAddTable);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(622, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 538);
            this.panel1.TabIndex = 28;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.Controls.Add(this.comboBoxAllCat);
            this.panel3.Controls.Add(this.radioButtonUpdateData);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.radioButtonAddData);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(366, 44);
            this.panel3.TabIndex = 25;
            // 
            // comboBoxAllCat
            // 
            this.comboBoxAllCat.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.comboBoxAllCat.FormattingEnabled = true;
            this.comboBoxAllCat.Location = new System.Drawing.Point(192, 9);
            this.comboBoxAllCat.Name = "comboBoxAllCat";
            this.comboBoxAllCat.Size = new System.Drawing.Size(162, 29);
            this.comboBoxAllCat.TabIndex = 26;
            this.comboBoxAllCat.SelectedIndexChanged += new System.EventHandler(this.CommboxAllCat_Change);
            // 
            // radioButtonUpdateData
            // 
            this.radioButtonUpdateData.AutoSize = true;
            this.radioButtonUpdateData.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radioButtonUpdateData.Location = new System.Drawing.Point(91, 22);
            this.radioButtonUpdateData.Name = "radioButtonUpdateData";
            this.radioButtonUpdateData.Size = new System.Drawing.Size(97, 23);
            this.radioButtonUpdateData.TabIndex = 25;
            this.radioButtonUpdateData.Text = "แก้ไขข้อมูล";
            this.radioButtonUpdateData.UseVisualStyleBackColor = true;
            this.radioButtonUpdateData.CheckedChanged += new System.EventHandler(this.ChangeUpdateFlag);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label12.Location = new System.Drawing.Point(7, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 21);
            this.label12.TabIndex = 10;
            this.label12.Text = "Option : ";
            // 
            // radioButtonAddData
            // 
            this.radioButtonAddData.AutoSize = true;
            this.radioButtonAddData.Checked = true;
            this.radioButtonAddData.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radioButtonAddData.Location = new System.Drawing.Point(90, 1);
            this.radioButtonAddData.Name = "radioButtonAddData";
            this.radioButtonAddData.Size = new System.Drawing.Size(87, 23);
            this.radioButtonAddData.TabIndex = 24;
            this.radioButtonAddData.TabStop = true;
            this.radioButtonAddData.Text = "เพิ่มข้อมูล";
            this.radioButtonAddData.UseVisualStyleBackColor = true;
            this.radioButtonAddData.CheckedChanged += new System.EventHandler(this.ChangeUpdateFlag);
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Century Gothic", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labelHeader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelHeader.Location = new System.Drawing.Point(130, 52);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(239, 25);
            this.labelHeader.TabIndex = 9;
            this.labelHeader.Text = "เพิ่มกลุ่มวัตถุดิบ / คลังสินค้า";
            // 
            // txtBoxDesc
            // 
            this.txtBoxDesc.BackColor = System.Drawing.Color.Maroon;
            this.txtBoxDesc.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtBoxDesc.ForeColor = System.Drawing.SystemColors.Window;
            this.txtBoxDesc.Location = new System.Drawing.Point(168, 138);
            this.txtBoxDesc.Name = "txtBoxDesc";
            this.txtBoxDesc.Size = new System.Drawing.Size(191, 30);
            this.txtBoxDesc.TabIndex = 3;
            // 
            // txtBoxNameTH
            // 
            this.txtBoxNameTH.BackColor = System.Drawing.Color.Maroon;
            this.txtBoxNameTH.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtBoxNameTH.ForeColor = System.Drawing.SystemColors.Window;
            this.txtBoxNameTH.Location = new System.Drawing.Point(168, 89);
            this.txtBoxNameTH.Name = "txtBoxNameTH";
            this.txtBoxNameTH.Size = new System.Drawing.Size(191, 30);
            this.txtBoxNameTH.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(38, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 25);
            this.label7.TabIndex = 13;
            this.label7.Text = "Description :";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DimGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(214, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 39);
            this.button1.TabIndex = 5;
            this.button1.Text = "ล้างข้อมูล";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonAddTable
            // 
            this.buttonAddTable.BackColor = System.Drawing.Color.DimGray;
            this.buttonAddTable.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonAddTable.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonAddTable.ForeColor = System.Drawing.Color.White;
            this.buttonAddTable.Location = new System.Drawing.Point(89, 207);
            this.buttonAddTable.Name = "buttonAddTable";
            this.buttonAddTable.Size = new System.Drawing.Size(100, 39);
            this.buttonAddTable.TabIndex = 4;
            this.buttonAddTable.Text = "เพิ่มข้อมูล";
            this.buttonAddTable.UseVisualStyleBackColor = false;
            this.buttonAddTable.Click += new System.EventHandler(this.buttonAddTable_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(0, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Store Cat Name :";
            // 
            // AddStoreCat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Name = "AddStoreCat";
            this.Text = "AddCat";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllMember)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridViewAllMember;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox comboBoxAllCat;
        private System.Windows.Forms.RadioButton radioButtonUpdateData;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton radioButtonAddData;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.TextBox txtBoxDesc;
        private System.Windows.Forms.TextBox txtBoxNameTH;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonAddTable;
        private System.Windows.Forms.Label label2;
    }
}