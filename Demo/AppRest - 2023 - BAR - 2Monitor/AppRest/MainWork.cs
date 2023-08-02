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
    public partial class MainWork : MainTemplate
    {
        GetDataRest gd;
        List<MemWork> mw;
        AddMember formAddMember;
        

        public MainWork(Form frmlkFrom, int flagFrmClose)
        {
            InitializeComponent();
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }
            this.Text = this.Text + " ( By : " + Login.userName + ")";

            buttonLinkCashCard.BackColor = System.Drawing.Color.Brown;

            this.Width = 1024;
            this.Height = 764;
            if (Login.isFrontWide)
            {
                this.Width = 1280;
            }


            gd = new GetDataRest();
            genObjMemWork();
        }

        private void genObjMemWork()
        {
            PanelWorkin.Controls.Clear();
            DelOrderPN.Controls.Clear();


            this.mw = gd.getWorkInOut();

            int userID;
            string userName;
            string status;
            int workShift;
            string workIn;
            string workOut;
            float workHour;

            string strWorking = "";

            int workShirtBefore = 1; 

            Button bMemWorkIN;

            int sizeX = 87;
            int sizeY = 40;
            int yy = 7;

            int i = 0;
            int j = 0;

            foreach (MemWork m in this.mw)
            {

                bMemWorkIN = new Button();

                userID = m.UserID;
                userName = m.UserName;
                status = m.Status;
                workShift = m.WorkShift;
                workIn = m.WorkIN;
                workOut = m.WorkOut;
                workHour = m.WorkHour;


                if (workIn.Length > 0)
                {
                    if (workOut.Length > 0)
                    {
                        bMemWorkIN.BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        bMemWorkIN.BackColor = System.Drawing.Color.Lime;
                    }
                }
                else
                {
                    bMemWorkIN.BackColor = System.Drawing.Color.White;
                }

                if (workShift != workShirtBefore)
                {
                    i = ((i % yy) + 2) * yy;
                }

                bMemWorkIN.Cursor = System.Windows.Forms.Cursors.Default;
                bMemWorkIN.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                bMemWorkIN.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                bMemWorkIN.ForeColor = System.Drawing.Color.Black;
                bMemWorkIN.Location = new System.Drawing.Point(1 + (sizeX * (i % yy)), 1 + (sizeY * (i / yy)));
                bMemWorkIN.Name = userID.ToString();

                bMemWorkIN.Size = new System.Drawing.Size(sizeX, sizeY);
                bMemWorkIN.TabIndex = 1;
                bMemWorkIN.Text = userName + " (" + userID.ToString() + ")";
                bMemWorkIN.UseVisualStyleBackColor = false;
                bMemWorkIN.Click += new System.EventHandler(this.bMemWorkIN_Click);

                PanelWorkin.Controls.Add(bMemWorkIN);

                Button bDel;
                int sizeXX = 40;
                int sizeYY = 15;
                int yyy = 1;

                if (workIn.Length > 0)
                {
                    strWorking += (j+1).ToString() + "." + userName + " (" + userID.ToString() + ")" + " In : " + workIn + " , Out : " + workOut + "\n";


                    bDel = new Button();

                    bDel.BackColor = System.Drawing.Color.Red;
                    bDel.Font = new System.Drawing.Font("Century Gothic", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    bDel.ForeColor = System.Drawing.Color.Black;
                    bDel.Location = new System.Drawing.Point(1 + (sizeXX * (j % yyy)), 1 + (sizeYY * (j / yyy)));
                    bDel.Name = userID.ToString();
                    bDel.Size = new System.Drawing.Size(sizeXX, sizeYY);
                    bDel.TabIndex = 4;
                    bDel.Text = userName;
                    bDel.UseVisualStyleBackColor = false;
                    bDel.Click += new System.EventHandler(this.bDel_Click);

                    DelOrderPN.Controls.Add(bDel);



                    j++;
                }
                
                
                i++;

                workShirtBefore = workShift;
            }

            txtWorking.Text = strWorking;
         
        }


        private void bMemWorkIN_Click(object sender, EventArgs e)
        {

            Button bClick = (Button)sender;

            int flagWork = 0; // 1 = In , 2 = Out

            if (bClick.BackColor == System.Drawing.Color.White)
            {
                flagWork = 1;
            }
            else if (bClick.BackColor == System.Drawing.Color.Lime)
            {
                flagWork = 2;
            }


            int userID = Int32.Parse(bClick.Name);
            string userName = bClick.Text;

            if (MessageBox.Show("คุณแน่ใจว่าจะลงเวลางานให้ " + userName + " นี้หรือไม่ ? ", "ลงเวลาเข้า / ออกงาน", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (flagWork > 0)
                {

                    int result = gd.instWorkInOut(userID, flagWork);

                    if (result <= 0)
                        MessageBox.Show("Error Delete Check In / Out");
                }
                else
                {
                    MessageBox.Show("กรุณาอย่ากดออกงานซ้ำ !!!");
                }
            }

            genObjMemWork();
        }

        private void bDel_Click(object sender, EventArgs e)
        {

            Button bClick = (Button)sender;

            int userID = Int32.Parse(bClick.Name);
            string userName = bClick.Text;
            if (MessageBox.Show("คุณต้องการจะลบเวลา " + userName + " นี้หรือไม่ ? ", "ลบออร์เดอร์นี้", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int result = gd.delWorkInOut(userID);

                if (result <= 0)
                    MessageBox.Show("Error Delete Check In / Out");

            }

            genObjMemWork();
        }


        private void buttonAddMember_Click(object sender, EventArgs e)
        {
            LinkFormAddMember();
        }


        private void LinkFormAddMember()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (formAddMember == null)
            {
                formAddMember = new AddMember(this, 1);
            }
            Cursor.Current = Cursors.Default;
            if (formAddMember.ShowDialog() == DialogResult.OK)
            {
                formAddMember.Dispose();
                formAddMember = null;
            }
        }
    }
}
