using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;

namespace AppRest
{
    public partial class FromRptQuotation : Form
    {
        private ReportDocument rd = new ReportDocument();
        private ConnectionInfo crConnectionInfo = new ConnectionInfo();

        int branchID;
        string pathReport;
        string reportName;
        string databaseServerName;
        string databaseName;
        string databaseUserName;
        string databasePassword;

        public string rptFromDate = "";
        public string rptToDate = "";
        public int rptType = 0;


        
        public int tableID;
        public int BranchID;
        public int MemID;
        public string CreateBy;
        public int VATType;
        public float Discount;
        public int Count;

        public FromRptQuotation(Form frmlkFrom, int flagFrmClose,int tableID,int BranchID,int MemID,string CreateBy,int VATType,float Discount,int Count)
        {
            InitializeComponent();
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }

            crystalReportViewer2.ReportSource = null;

            branchID = Int32.Parse(ConfigurationSettings.AppSettings["BranchID"].ToString());
            pathReport = ConfigurationSettings.AppSettings["PathReport"].ToString();

            databaseServerName = ConfigurationSettings.AppSettings["DatabaseServerName"].ToString();
            databaseName = ConfigurationSettings.AppSettings["DatabaseName"].ToString();
            databaseUserName = ConfigurationSettings.AppSettings["DatabaseUserName"].ToString();
            databasePassword = ConfigurationSettings.AppSettings["DatabasePassword"].ToString();

            
            this.tableID = tableID;
            this.BranchID = BranchID;
            this.MemID = MemID;
            this.CreateBy = CreateBy;
            this.VATType = VATType;
            this.Discount = Discount;
            this.Count = Count;

            viewReport();

        }

        public void viewReport()
        {


            reportName = "Rpt_PrintDoc_Quotation.rpt";  

            crystalReportViewer2.Refresh();

            rd.Load(pathReport + reportName);
            crConnectionInfo.ServerName = databaseServerName; // ODBC
            crConnectionInfo.DatabaseName = databaseName;
            crConnectionInfo.UserID = databaseUserName;
            crConnectionInfo.Password = databasePassword;



            rd.SetParameterValue("@BillNo", Count);
            rd.SetParameterValue("@tableID", tableID);
            rd.SetParameterValue("@BranchID", BranchID);
            rd.SetParameterValue("@MemID", MemID);
            rd.SetParameterValue("@CreateBy", CreateBy);
            rd.SetParameterValue("@VATType", VATType);
            rd.SetParameterValue("@Discount", Discount); 

            SetDBLogonForReport(crConnectionInfo, rd);
            crystalReportViewer2.ReportSource = rd;

            //crystalReportViewer2.Refresh();
            //crystalReportViewer2.ParameterFieldInfo.Clear();
             
        }

        private void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {
            Tables tables = reportDocument.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
 



    }
}
