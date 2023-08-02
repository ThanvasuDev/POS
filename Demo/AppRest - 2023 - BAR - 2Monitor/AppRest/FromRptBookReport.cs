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
    public partial class FromRptBookReport : Form
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


        public int bookID; 
        public string fromDate;
        public string toDate; 
        public int flagConfirm;
        public int flagActive; 
        public int zoneID;
        public int tableID;



        public FromRptBookReport(Form frmlkFrom, int flagFrmClose, int bookID, string fromDate, string toDate, int flagConfirm, int flagActive, int zoneID, int tableID)
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

            this.bookID = bookID;
            this.fromDate = fromDate;
            this.toDate = toDate;
            this.flagConfirm = flagConfirm;
            this.flagActive = flagActive;
            this.zoneID = zoneID;
            this.tableID = tableID;

            viewReport();

        }

        public void viewReport()
        {


            reportName = "Rpt_PrintBooking.rpt";  

            crystalReportViewer2.Refresh();

            rd.Load(pathReport + reportName);
            crConnectionInfo.ServerName = databaseServerName; // ODBC
            crConnectionInfo.DatabaseName = databaseName;
            crConnectionInfo.UserID = databaseUserName;
            crConnectionInfo.Password = databasePassword;



            rd.SetParameterValue("@BillNo", 0);
            rd.SetParameterValue("@BookID", bookID);
            rd.SetParameterValue("@BookDate", fromDate);
            rd.SetParameterValue("@BookToDate", toDate);
            rd.SetParameterValue("@FlagConfirm", flagConfirm);
            rd.SetParameterValue("@FlagActive", flagActive);
            rd.SetParameterValue("@ZoneID", zoneID);
            rd.SetParameterValue("@TableID", tableID);

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
