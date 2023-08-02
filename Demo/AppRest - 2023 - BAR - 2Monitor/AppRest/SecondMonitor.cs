using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Configuration;


namespace AppRest
{
    public partial class SecondMonitor : Form
    {
        private int _maximumImages = 1;
        int i = 0;

        public DisplayData DisplayData
        {
            set
            {
                if (value != null)
                {
                    DisplayOrder(value);
                 //   wmpVideo.Visible = false;
                   // pictureBox1.Visible = false;
                }
                else
                {
                    listView1.Clear();

                    var zero = 0;
                    textBoxSubTotal.Text = zero.ToString("###,###.#0");
                    textBoxDiscountTotal.Text = zero.ToString("###,###.#0");
                    textBoxServiceCharge.Text = zero.ToString("###,###.#0");
                    textBoxVAT.Text = zero.ToString("###,###.#0");
                    textBoxSalesTotal.Text = zero.ToString("###,###.#0");

                 //   wmpVideo.Visible = true;
                    pictureBox1.Visible = true;
                }
            }
        }

        private void DisplayOrder(DisplayData value)
        {
            //*** ListView Header
            this.listView1.Clear();
            listView1.HideSelection = false;
            listView1.Columns.Add("No.", 64, HorizontalAlignment.Center);
            listView1.Columns.Add("Descripton", 342, HorizontalAlignment.Left);
            listView1.Columns.Add("Qty", 64, HorizontalAlignment.Right);
            listView1.Columns.Add("Amount", 200, HorizontalAlignment.Right);
            listView1.FullRowSelect = true;
            listView1.View = View.Details;

            //*** ListView Row
            for (int i = 0; i < value.Orders.Count; i++)
            {

                var item = value.Orders[i];
                if (  ! (item.ProductID > 10 && item.ProductID < 100) )
                {
                    var products = item.ProductName.Split('|');
                    string[] arr = new string[4];
                    arr[0] = (i + 1).ToString();
                    arr[1] = products[0];
                    arr[2] = item.OrderQTY.ToString();
                    arr[3] = item.OrderAmount.ToString();
                    var lvi = new ListViewItem(arr);
                    this.listView1.Items.Add(lvi);


                    AddRemarkItem(products, ref arr, ref lvi);
                }
            }

            textBoxSubTotal.Text = value.SubTotal.ToString("###,###.#0");
            textBoxDiscountTotal.Text = value.DiscountTotal.ToString("###,###.#0");
            textBoxServiceCharge.Text = value.ServiceCharge.ToString("###,###.#0");
            textBoxVAT.Text = value.Vat.ToString("###,###.#0");
            textBoxSalesTotal.Text = value.SalesTotal.ToString("###,###.#0");
        }

        private void AddRemarkItem(string[] products, ref string[] arr, ref ListViewItem lvi)
        {
            if (!string.IsNullOrEmpty(products[1]))
            {
                var productRemarks = products[1].Remove(0, 1).Split('+');
                foreach (var remark in productRemarks)
                {
                    arr = new string[4];
                    arr[0] = "";
                    arr[1] = string.Format("+ {0}", remark);
                    arr[2] = "";
                    arr[3] = "";
                    lvi = new ListViewItem(arr);
                    this.listView1.Items.Add(lvi);
                }
            }
        }

        public SecondMonitor()
        {
            InitializeComponent();
        }

        private void SecondMonitor_Load(object sender, EventArgs e)
        {
            //DisplayVideo();

            //_maximumImages = Directory.GetFiles(@"C:\POS\images").Length;
            //if (File.Exists(@"C:\POS\images\image" + i + ".jpg"))
            //    pictureBox1.Image = Image.FromFile(@"C:\POS\images\image" + i + ".jpg");

            pictureBox1.Image = global::AppRest.Properties.Resources.Logo_New;

            timer1.Enabled = true;
        }

        //private void DisplayVideo()
        //{
        //    if (File.Exists(@"C:\POS\videos\video1.mp4"))
        //    {
        //        wmpVideo.uiMode = "none";
        //        wmpVideo.settings.autoStart = true;
        //        wmpVideo.settings.setMode("loop", true);
        //        wmpVideo.URL = @"C:\POS\videos\video1.mp4";
        //    }
        //    else
        //        wmpVideo.Visible = false;
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            //'Set this formula to increment i by 1
            i += 1;

            //'Set a validation of pictures
            if (i > _maximumImages)
                i = 0;

            //'Set to Load the image.
            if (File.Exists(@"C:\POS\images\image" + i + ".jpg"))
                pictureBox1.Image = Image.FromFile(@"C:\POS\images\image" + i + ".jpg");
        }
    }
    public class DisplayData
    {
        public List<Order> Orders { get; set; }
        public float SubTotal { get; set; }
        public float DiscountTotal { get; set; }
        public float SalesTotal { get; set; }
        public float ServiceCharge { get; set; }
        public float Vat { get; set; }
    }
}
