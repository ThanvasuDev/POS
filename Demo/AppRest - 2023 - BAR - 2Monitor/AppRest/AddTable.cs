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
    public partial class AddTable : AddDataTemplate
    {
        GetDataRest gd;
        MainManage formMainManage;


        List<Zone> allZone;
        List<Tbl> allTableByZone;

        int selectedZone;


        public AddTable(Form frmlkFrom, int flagFrmClose)
        {
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            InitializeComponent();
            gd = new GetDataRest();

            this.Width = 1024;
            this.Height = 764;


            comboBoxAllTable.Visible = false;

            selectedZone = 1;

            allZone = gd.getAllZone();

            allTableByZone = gd.getTableByZone(selectedZone);
            dataGridViewAllMember.DataSource = allTableByZone;


            getComboAllZone();

        }


        private void getComboAllZone()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List


            foreach (Zone c in allZone)
            {
                data.Add(new KeyValuePair<int, string>(c.ZoneID, c.ZoneName));
            }


            // Clear the combobox
            comboBoxAllZone.DataSource = null;
            comboBoxAllZone.Items.Clear();

            // Bind the combobox
            comboBoxAllZone.DataSource = new BindingSource(data, null);
            comboBoxAllZone.DisplayMember = "Value";
            comboBoxAllZone.ValueMember = "Key";

            comboBoxAllZone.SelectedValue = selectedZone;
            getComboAllTable();

        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {

            try
            {
                string tableName = txtBoxName.Text;
                string tableDesc = txtBoxDesc.Text;
                string tableFlagUse = comboBoxFlagUse.Text;

                int tableID = Int32.Parse(comboBoxAllTable.SelectedValue.ToString());
                int zoneID = Int32.Parse(comboBoxAllZone.SelectedValue.ToString());




                Button but = (Button)sender; 
                string butName = but.Name;

                if (butName == "buttonDel")
                {
                    if (MessageBox.Show("คุณต้องการจะลบ " + tableName + " หรือไม่ ?", "เพิ่ม " + tableName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updsTable(tableID, zoneID, tableName, tableDesc, "D");

                        if (result <= 0)
                        {
                            throw new Exception("ไม่สามารถลบรายการได้ เนื่องจากมีในรายการขายแล้ว");
                        }
                        else
                        {
                            
                            allTableByZone = gd.getTableByZone(selectedZone);
                            dataGridViewAllMember.DataSource = allTableByZone;
                            getComboAllTable();
                            clearForm();

                            throw new Exception("ลบ : " + tableName + ">> (Success)");
                        }
                    }

                   
                }
                       




                if (radioButtonAddData.Checked == true)
                {

                    if (MessageBox.Show("คุณต้องการเพิ่มเมนูชื่อ " + tableName + " หรือไม่ ?", "เพิ่ม " + tableName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.instNewTable(zoneID, tableName, tableDesc, tableFlagUse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Insert New Product : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("เพิ่มเมนูชื่อ " + tableName + ">> (Success)");
                            allTableByZone = gd.getTableByZone(selectedZone);
                            dataGridViewAllMember.DataSource = allTableByZone;
                            getComboAllTable();
                            clearForm();

                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("คุณต้องการแก้ไขเมนูชื่อ " + tableName + " หรือไม่ ?", "เพิ่ม " + tableName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int result = gd.updsTable(tableID, zoneID, tableName, tableDesc, tableFlagUse);

                        if (result <= 0)
                        {
                            MessageBox.Show("Error Update Product : Please Try Again");
                        }
                        else
                        {
                            MessageBox.Show("แก้ไข : " + tableName + ">> (Success)"); 
                            allTableByZone = gd.getTableByZone(selectedZone);
                            dataGridViewAllMember.DataSource = allTableByZone;
                            getComboAllTable();
                            clearForm();
                        }
                    }

                }








            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void getComboAllTable()
        {
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();

            // Add data to the List

            data.Add(new KeyValuePair<int, string>(-1, "= เลือกแก้ไขหมวด ="));

            foreach (Tbl c in allTableByZone)
            {
                data.Add(new KeyValuePair<int, string>(c.TblID, c.TblName));
            }


            // Clear the combobox
            comboBoxAllTable.DataSource = null;
            comboBoxAllTable.Items.Clear();

            // Bind the combobox
            comboBoxAllTable.DataSource = new BindingSource(data, null);
            comboBoxAllTable.DisplayMember = "Value";
            comboBoxAllTable.ValueMember = "Key";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtBoxName.Text = "";
            txtBoxDesc.Text = "";
            comboBoxFlagUse.Text = "Y";
        }

        private void clearForm()
        {
            txtBoxName.Text = ""; 
            txtBoxDesc.Text = "";
            comboBoxFlagUse.Text = "Y";
            radioButtonAddData.Checked = true;
        }

        private void ChangeUpdateFlag(object sender, EventArgs e)
        {
            if (radioButtonAddData.Checked == true)
            {
                comboBoxAllTable.Visible = false;
                labelHeader.Text = "เพิ่มเมนูอาหาร";
                buttonAddTable.Text = "เพิ่มข้อมูล";
            }
            else
            {
                comboBoxAllTable.Visible = true;
                labelHeader.Text = "แก้ไขเมนูอาหาร";
                buttonAddTable.Text = "แก้ไขข้อมูล";
            }
        }



        private void buttonBackToManage_Click(object sender, EventArgs e)
        {
            LinkFormMainManage();
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

        private void ComboBoxAllZone_Change(object sender, EventArgs e)
        {
            try
            {
                selectedZone = Int32.Parse(comboBoxAllZone.SelectedValue.ToString());
                allTableByZone = gd.getTableByZone(selectedZone);
                dataGridViewAllMember.DataSource = allTableByZone;
                getComboAllTable();
            }
            catch (Exception ex)
            {

            }
        }

        private void CommboxAllTable_Change(object sender, EventArgs e)
        {
            try
            {

                int tableID = Int32.Parse(comboBoxAllTable.SelectedValue.ToString());

                foreach (Tbl c in allTableByZone)
                {
                    if (c.TblID == tableID)
                    {
                        txtBoxName.Text = c.TblName;
                        txtBoxDesc.Text = c.TblDesc;
                        comboBoxFlagUse.Text = c.TblFlagUse;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridViewAllMember_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int dataGridCatID = Int32.Parse(dataGridViewAllMember.Rows[e.RowIndex].Cells["TblID"].Value.ToString());

                comboBoxAllTable.SelectedValue = dataGridCatID;

                comboBoxAllTable.Visible = true;
                labelHeader.Text = "แก้ไขโต๊ะ";
                buttonAddTable.Text = "แก้ไขข้อมูล";
                radioButtonUpdateData.Checked = true;

            }
            catch (Exception ex)
            {

            }
        }
 
 

 
    }

}
