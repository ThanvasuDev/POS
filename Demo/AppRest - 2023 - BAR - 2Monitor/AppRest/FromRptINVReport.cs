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
    public partial class FromRptINVReport : Form
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

         
        public int rptType = 0;
        public int rptCustID = 0;
        public string rptPayBillID = "";
         

        public FromRptINVReport(Form frmlkFrom, int flagFrmClose, int rptCustID, string rptPayBillID, int type)
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

            this.rptCustID = rptCustID;
            this.rptPayBillID = rptPayBillID;
            this.rptType = type;

            viewReport();

        }

        public void viewReport()
        {

            if (rptType == 1)
              reportName = "Rpt_CreditCust_INV.rpt";
            else if (rptType == 2)
                reportName = "Rpt_CreditCust_INV_ALL.rpt";


            crystalReportViewer2.Refresh();

            rd.Load(pathReport + reportName);
            crConnectionInfo.ServerName = databaseServerName; // ODBC
            crConnectionInfo.DatabaseName = databaseName;
            crConnectionInfo.UserID = databaseUserName;
            crConnectionInfo.Password = databasePassword;

             
            rd.SetParameterValue("@CustID", rptCustID);
            rd.SetParameterValue("@PayBillID", rptPayBillID);
            rd.SetParameterValue("@BillNo", 0);
            rd.SetParameterValue("@CreateBy", Login.userName);


            SetDBLogonForReport(crConnectionInfo, rd);
            crystalReportViewer2.ReportSource = rd;

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
            this.Close();

        }





    }
}
