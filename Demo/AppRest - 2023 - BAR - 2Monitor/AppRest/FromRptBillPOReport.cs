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
    public partial class FromRptBillPOReport : Form
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


        public string rptBillPO = "0";
        public int rptType = 0;

        public FromRptBillPOReport(Form frmlkFrom, int flagFrmClose, string poNo, int type)
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

            this.rptBillPO = poNo;
         //   this.rptType = type;

            

            //rd.Load(pathReport + reportName);
            //crConnectionInfo.ServerName = databaseServerName; // ODBC
            //crConnectionInfo.DatabaseName = databaseName;
            //crConnectionInfo.UserID = databaseUserName;
            //crConnectionInfo.Password = databasePassword;

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

            reportName = "Rpt_PrintBillPO.rpt";
             
            rd.Load(pathReport + reportName);
            crConnectionInfo.ServerName = databaseServerName; // ODBC
            crConnectionInfo.DatabaseName = databaseName;
            crConnectionInfo.UserID = databaseUserName;
            crConnectionInfo.Password = databasePassword;


            rd.SetParameterValue("@BillNo", 0);
            rd.SetParameterValue("@PONo", rptBillPO);
             
             
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
