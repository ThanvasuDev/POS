namespace AppRest
{
    partial class AddTable
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonDel = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxFlagUse = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comboBoxAllTable = new System.Windows.Forms.ComboBox();
            this.radioButtonUpdateData = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.radioButtonAddData = new System.Windows.Forms.RadioButton();
            this.labelHeader = new System.Windows.Forms.Label();
            this.txtBoxDesc = new System.Windows.Forms.TextBox();
            this.txtBoxName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonAddTable = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.dataGridViewAllMember = new System.Windows.Forms.DataGridView();
            this.comboBoxAllZone = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllMember)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.buttonDel);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.comboBoxFlagUse);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.labelHeader);
            this.panel1.Controls.Add(this.txtBoxDesc);
            this.panel1.Controls.Add(this.txtBoxName);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.buttonAddTable);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(626, 145);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 538);
            this.panel1.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(152, 487);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 40);
            this.label3.TabIndex = 30;
            this.label3.Text = "** การลบข้อมูลจะทำได้ก็ต่อเมื่อ\r\n    ไม่เคยทำรายการขายนี้ เท่านั้น";
            // 
            // buttonDel
            // 
            this.buttonDel.BackColor = System.Drawing.Color.DimGray;
            this.buttonDel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonDel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonDel.ForeColor = System.Drawing.Color.White;
            this.buttonDel.Location = new System.Drawing.Point(215, 312);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(104, 39);
            this.buttonDel.TabIndex = 29;
            this.buttonDel.Text = "ลบข้อมูล";
            this.buttonDel.UseVisualStyleBackColor = false;
            this.buttonDel.Click += new System.EventHandler(this.buttonAddTable_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(65, 185);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 25);
            this.label10.TabIndex = 28;
            this.label10.Text = "FlagUse :";
            // 
            // comboBoxFlagUse
            // 
            this.comboBoxFlagUse.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.comboBoxFlagUse.FormattingEnabled = true;
            this.comboBoxFlagUse.Items.AddRange(new object[] {
            "Y",
            "N"});
            this.comboBoxFlagUse.Location = new System.Drawing.Point(169, 183);
            this.comboBoxFlagUse.Name = "comboBoxFlagUse";
            this.comboBoxFlagUse.Size = new System.Drawing.Size(46, 29);
            this.comboBoxFlagUse.TabIndex = 3;
            this.comboBoxFlagUse.Text = "Y";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.Controls.Add(this.comboBoxAllTable);
            this.panel3.Controls.Add(this.radioButtonUpdateData);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.radioButtonAddData);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(366, 48);
            this.panel3.TabIndex = 25;
            // 
            // comboBoxAllTable
            // 
            this.comboBoxAllTable.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.comboBoxAllTable.FormattingEnabled = true;
            this.comboBoxAllTable.Location = new System.Drawing.Point(192, 9);
            this.comboBoxAllTable.Name = "comboBoxAllTable";
            this.comboBoxAllTable.Size = new System.Drawing.Size(162, 29);
            this.comboBoxAllTable.TabIndex = 26;
            this.comboBoxAllTable.SelectedIndexChanged += new System.EventHandler(this.CommboxAllTable_Change);
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
            this.labelHeader.Location = new System.Drawing.Point(119, 54);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(174, 25);
            this.labelHeader.TabIndex = 9;
            this.labelHeader.Text = "เพิ่มข้อมูลโต๊ะอาหาร";
            // 
            // txtBoxDesc
            // 
            this.txtBoxDesc.BackColor = System.Drawing.Color.Maroon;
            this.txtBoxDesc.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtBoxDesc.ForeColor = System.Drawing.SystemColors.Window;
            this.txtBoxDesc.Location = new System.Drawing.Point(166, 137);
            this.txtBoxDesc.Name = "txtBoxDesc";
            this.txtBoxDesc.Size = new System.Drawing.Size(191, 30);
            this.txtBoxDesc.TabIndex = 2;
            // 
            // txtBoxName
            // 
            this.txtBoxName.BackColor = System.Drawing.Color.Maroon;
            this.txtBoxName.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtBoxName.ForeColor = System.Drawing.SystemColors.Window;
            this.txtBoxName.Location = new System.Drawing.Point(166, 89);
            this.txtBoxName.Name = "txtBoxName";
            this.txtBoxName.Size = new System.Drawing.Size(191, 30);
            this.txtBoxName.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(36, 139);
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
            this.button1.Location = new System.Drawing.Point(215, 267);
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
            this.buttonAddTable.Location = new System.Drawing.Point(89, 267);
            this.buttonAddTable.Name = "buttonAddTable";
            this.buttonAddTable.Size = new System.Drawing.Size(100, 84);
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
            this.label2.Location = new System.Drawing.Point(27, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Table Name :";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.dataGridViewAllMember);
            this.panel2.Controls.Add(this.comboBoxAllZone);
            this.panel2.Location = new System.Drawing.Point(5, 147);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(614, 538);
            this.panel2.TabIndex = 32;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(212, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 25);
            this.label11.TabIndex = 14;
            this.label11.Text = "All Table";
            // 
            // dataGridViewAllMember
            // 
            this.dataGridViewAllMember.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewAllMember.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewAllMember.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAllMember.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridViewAllMember.Location = new System.Drawing.Point(4, 32);
            this.dataGridViewAllMember.Name = "dataGridViewAllMember";
            this.dataGridViewAllMember.Size = new System.Drawing.Size(603, 489);
            this.dataGridViewAllMember.TabIndex = 0;
            this.dataGridViewAllMember.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAllMember_CellClick);
            // 
            // comboBoxAllZone
            // 
            this.comboBoxAllZone.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.comboBoxAllZone.FormattingEnabled = true;
            this.comboBoxAllZone.Location = new System.Drawing.Point(311, 1);
            this.comboBoxAllZone.Name = "comboBoxAllZone";
            this.comboBoxAllZone.Size = new System.Drawing.Size(238, 29);
            this.comboBoxAllZone.TabIndex = 26;
            this.comboBoxAllZone.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAllZone_Change);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(5, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 25);
            this.label1.TabIndex = 33;
            this.label1.Text = "เพิ่มโต๊ะ  / ช่องทางการขาย";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(417, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 25);
            this.label5.TabIndex = 34;
            this.label5.Text = "Zone";
            // 
            // AddTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 687);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AddTable";
            this.Text = "AddTable";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllMember)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox comboBoxAllTable;
        private System.Windows.Forms.RadioButton radioButtonUpdateData;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton radioButtonAddData;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.TextBox txtBoxDesc;
        private System.Windows.Forms.TextBox txtBoxName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonAddTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridViewAllMember;
        private System.Windows.Forms.ComboBox comboBoxAllZone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxFlagUse;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Label label3;
    }
}