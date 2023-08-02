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
    public partial class AddDataTemplate : Form
    {
        public AddDataTemplate()
        {
            InitializeComponent();
            genDefault();

            this.Width = 1024;
            this.Height = 764;

            if (Login.isFrontPOS)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }

        }

        private void genDefault()
        {
            TxtFooter.Text = "Copy Right @ " + ConfigurationSettings.AppSettings["RestName"];
        }

        private void buttonBackToManage_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
