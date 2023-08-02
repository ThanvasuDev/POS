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
    public partial class Standby : Form
    {
         
        GetDataRest gd;
        int loginErrorCount;
        MainTable formMainTable;

        //public static int userID = 0;
        //public static string userName = "";
        //public static string userPassword = "";
        //public static string userStatus = "";
        public static string isTablet = "";

        int butType = 1;
        int loginByCard = 0;


        public Standby(Form frmlkFrom, int flagFrmClose)
        {
            
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose(); 
            } 

        
            InitializeComponent();
            genDefault();

            gd = new GetDataRest();
            loginErrorCount = 0;

            isTablet = ConfigurationSettings.AppSettings["IsTablet"];

            if (isTablet == "Y")
            {
                Member member = gd.checkAuthentication("admin", "admin");

                Login.userID = member.UserID;
                Login.userName = member.UserName;
                Login.userStatus = member.Status; 

                LinkFormMainTable();
            }

            buttonType1.BackColor = System.Drawing.Color.Gold;

            labelRestName.Text = ConfigurationSettings.AppSettings["RestName"];



            if (Login.flagLogoSQ.ToLower() == "y")
            {
                panellogo.Width = 100;
                panellogo.Height = 100;
            }
            else
            {
                panellogo.Width = 200;
                panellogo.Height = 100;
                panellogo.Location = new Point(472-50, 251);
            }

            if (Login.isFrontPOS)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }

            this.Width = 1024;
            this.Height = 768;
            if (Login.isFrontWide)
            {
                this.Width = 1280;

                this.panelHeader.Location = new System.Drawing.Point(150, 0);
                this.panelFooter.Location = new System.Drawing.Point(150, 680);

                panelTAB1.Location = new System.Drawing.Point(240, 340);
                panelTAB2.Location = new System.Drawing.Point(545, 350);
                buttonExit.Location = new System.Drawing.Point(950, 370);

                panellogo.Visible = false;

               labelRestName.Location = new System.Drawing.Point(250, 164);
            }


        }

        private void genDefault()
        {
            TxtFooter.Text = "Copy Right @ " + ConfigurationSettings.AppSettings["RestName"];
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            txtBoxUserName.Text = "";
            txtBoxPassword.Text = "";
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            Member member;

            try
            {

                string userName = txtBoxUserName.Text;
                string password = txtBoxPassword.Text;

                string cardCode = textBoxCardCode.Text;

                if (loginByCard == 0)
                {

                    txtBoxUserName.Text = "";
                    txtBoxPassword.Text = "";

                    txtBoxUserName.Focus();
                    member = gd.checkAuthentication(userName, password);
                }
                else
                {
                    textBoxCardCode.Focus();
                    member = gd.checkAuthentication("CCard" + cardCode, cardCode);

                }

             

                if (member.UserID > 0)
                {
                    Login.userID = member.UserID;
                    Login.userName = member.UserName;
                    Login.userPassword = password;
                    Login.userStatus = member.Status;
                    Login.userTableID = member.WorkRate;
                    Login.userCatID = member.WorkShift;
                     
                    LinkFormMainTable();
                }
                else
                {
                    MessageBox.Show("Login Fail Please Check UserName or Password");
                    loginErrorCount++;
                }

                if (loginErrorCount == 3)
                {
                    MessageBox.Show("Login Error More than 3 times");
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }

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

        private void KeyPassFinish(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.buttonLogin_Click(buttonLogin, e);
            }
        }

        private void ButtonNum_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            string numstr = "";

            try
            {
                defaultColButOrderNo();
                bt.BackColor = System.Drawing.Color.FromArgb(255, 255, 128);

                numstr = Int32.Parse(bt.Name.Replace("button_", " ").Trim()).ToString();

                if (this.butType == 1)
                {
                    if (numstr == "13")
                        txtBoxUserName.Text = txtBoxUserName.Text.Substring(0, txtBoxUserName.Text.Length - 1);
                    else
                        txtBoxUserName.Text += numstr;
                }
                else if (this.butType == 2)
                {
                    if (numstr == "13")
                        txtBoxPassword.Text = txtBoxPassword.Text.Substring(0, txtBoxPassword.Text.Length - 1);
                    else
                        txtBoxPassword.Text += numstr;
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void defaultColButOrderNo()
        {
            button_01.BackColor = System.Drawing.Color.White;
            button_02.BackColor = System.Drawing.Color.White;
            button_03.BackColor = System.Drawing.Color.White;
            button_04.BackColor = System.Drawing.Color.White;
            button_05.BackColor = System.Drawing.Color.White;
            button_06.BackColor = System.Drawing.Color.White;
            button_07.BackColor = System.Drawing.Color.White;
            button_08.BackColor = System.Drawing.Color.White;
            button_09.BackColor = System.Drawing.Color.White;
            button_00.BackColor = System.Drawing.Color.White;
            button_13.BackColor = System.Drawing.Color.White;
        }

        private void buttonType_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            string numstr = "";

            try
            {
                defaultColButType();
                bt.BackColor = System.Drawing.Color.Gold;

                this.butType = Int32.Parse(bt.Name.Replace("buttonType", " ").Trim());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void defaultColButType()
        {
            buttonType1.BackColor = System.Drawing.Color.White;
            buttonType2.BackColor = System.Drawing.Color.White;
        }

        private void ScanCard_Click(object sender, EventArgs e)
        {
            textBoxCardCode.Focus();
        }

        private void textBoxCardCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 && textBoxCardCode.TextLength > 0)
            {
                this.loginByCard = 1;
                this.buttonLogin_Click(buttonLogin, e);
            }
        }

        private void txtBoxUserName_MouseClick(object sender, MouseEventArgs e)
        {
            this.butType = 1;

            defaultColButType();
            buttonType1.BackColor = System.Drawing.Color.Gold;
        }

        private void txtBoxPassword_MouseClick(object sender, MouseEventArgs e)
        {
            this.butType = 2;
            defaultColButType();
            buttonType2.BackColor = System.Drawing.Color.Gold;
        }


        private void buttonExit_Click(object sender, EventArgs e)
        {
            FuncString.displayPOSMonitorSecLine("      Thank you     ", ConfigurationSettings.AppSettings["RestName"].Replace("+", "").Trim());

            Application.Exit();
        }
       

    }
}
