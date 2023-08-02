using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;


namespace AppRest
{
    public partial class FormTESTAPI : Form
    {

        API api;


        public FormTESTAPI(Form frmlkFrom, int flagFrmClose)
        {
            InitializeComponent();
            if (flagFrmClose == 1)
            {

                frmlkFrom.Dispose();
            }

            api = new API();
        }

     

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string jobID = "";
            Root rt;

            try
            {
                jobID = api.AImemRegisterStart(TextBoxInput.Text ); 
                TextBoxOutput.Text = jobID;

                rt = api.AImemRegister(jobID);

                if (rt.data.wait == true)
                {
                    //  Loop
                    MessageBox.Show("Request Again !!");
                }
                else
                {
                  
                    MessageBox.Show("Register Success !!");
                }
                 
 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonRegister_Click_1(object sender, EventArgs e)
        {

            string jobID = "";
            Root rt ;

            try
            {


                rt = api.AImemRegister(TextBoxOutput.Text);

                if (rt.data.wait == true)
                {
                    //  Loop
                    MessageBox.Show("Request Again !!");


                }
                else
                {
                    if (rt.data.result.data.reference_id == TextBoxInput.Text)
                    {
                        MessageBox.Show("Register Success");
                        TextBoxOutput2.Text = rt.data.result.data.reference_id;
                    }
                }
                 
            }
            catch (Exception ex)
            {
                TextBoxOutput2.Text = ex.Message;
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonIdenStart_Click(object sender, EventArgs e)
        {
            string jobID = "";
            Root rt;

            try
            {
                jobID = api.AImemIdentificationStart();
                textBoxIdenJobID.Text = jobID;

                rt = api.AImemIdentification(jobID);

                if (rt.data.wait == true)
                {
                    //  Loop
                    MessageBox.Show("Request Iden Again !!");
                }
                else
                {
                    textBoxIdenMemID.Text =  rt.data.result.data.reference_id;
                    MessageBox.Show("Iden Success !!");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonIden_Click(object sender, EventArgs e)
        {
            string jobID = "";
            Root rt;

            try
            { 
                rt = api.AImemIdentification(textBoxIdenJobID.Text);

                if (rt.data.wait == true)
                {
                    //  Loop
                    MessageBox.Show("Request Iden Again !!");
                }
                else
                {
                    textBoxIdenMemID.Text = rt.data.result.data.reference_id;
                    MessageBox.Show("Iden Success !!");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}
