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
    public partial class FromRptPurchase : Form
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


        public string rptCode = "0";
        public string rptType = "0"; 

        public FromRptPurchase(Form frmlkFrom, int flagFrmClose, string rptCode, string rptType )
        {
            InitializeComponent();
            if (flagFrmClose == 1)
            {
                frmlkFrom.Dispose();
            }

           

            branchID = Int32.Parse(ConfigurationSettings.AppSettings["BranchID"].ToString());
            pathReport = ConfigurationSettings.AppSettings["PathReport"].ToString();

            databaseServerName = ConfigurationSettings.AppSettings["DatabaseServerName"].ToString();
            databaseName = ConfigurationSettings.AppSettings["DatabaseName"].ToString();
            databaseUserName = ConfigurationSettings.AppSettings["DatabaseUserName"].ToString();
            databasePassword = ConfigurationSettings.AppSettings["DatabasePassword"].ToString();

            this.rptCode = rptCode;
            this.rptType = rptType; 
             

            viewReport();

        }

        public void viewReport()
        {

            rd = new ReportDocument();
            crConnectionInfo = new ConnectionInfo();

            branchID = Int32.Parse(ConfigurationSettings.AppSettings["BranchID"].ToString());
            pathReport = ConfigurationSettings.AppSettings["PathReport"].ToString();

            databaseServerName = ConfigurationSettings.AppSettings["DatabaseServerName"].ToString();
            databaseName = ConfigurationSettings.AppSettings["DatabaseName"].ToString();
            databaseUserName = ConfigurationSettings.AppSettings["DatabaseUserName"].ToString();
            databasePassword = ConfigurationSettings.AppSettings["DatabasePassword"].ToString();

            if (this.rptType == "PR")
                reportName = "Rpt_PrintDoc_PR.rpt";
            else if (this.rptType == "PO")
                reportName = "Rpt_PrintDoc_PO.rpt";
            else if (this.rptType == "GR")
                reportName = "Rpt_PrintDoc_GR.rpt";
             
      
             
            rd.Load(pathReport + reportName);
            crConnectionInfo.ServerName = databaseServerName; // ODBC
            crConnectionInfo.DatabaseName = databaseName;
            crConnectionInfo.UserID = databaseUserName;
            crConnectionInfo.Password = databasePassword;


            if (this.rptType == "PR")
            {
                rd.SetParameterValue("@PRCode", rptCode);
                rd.SetParameterValue("@PRStatus", 0);
                rd.SetParameterValue("@StoreID", 0);
            }
            else if (this.rptType == "PO")
            {
                rd.SetParameterValue("@POCode", rptCode);
                rd.SetParameterValue("@POStatus", 0);
                rd.SetParameterValue("@StoreID", 0);
            }
            else if (this.rptType == "GR")
            {
                rd.SetParameterValue("@GRCode", rptCode);
                rd.SetParameterValue("@GRStatus", 0);
                rd.SetParameterValue("@StoreID", 0);
            }

            rd.SetParameterValue("@BillNo", 0);


           
             
             
            SetDBLogonForReport(crConnectionInfo, rd);
            crystalReportViewer2.ReportSource = rd;

            crystalReportViewer2.Refresh();

            crystalReportViewer2.ParameterFieldInfo.Clear();

  
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
           // crystalReportViewer2.ReportSource = null;
            this.Close();
          
        }

  

        
        
    }
}
