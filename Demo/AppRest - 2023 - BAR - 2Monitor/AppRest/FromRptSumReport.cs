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
    public partial class FromRptSumReport : Form
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
        public int rptProductSupID = 0;



        public FromRptSumReport(Form frmlkFrom, int flagFrmClose, string rptFromDate, string rptToDate,int rptProductSupID, int type)
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

            this.rptFromDate = rptFromDate;
            this.rptToDate = rptToDate;
            this.rptType = type;
            this.rptProductSupID = rptProductSupID;

            viewReport();

        }

        public void viewReport()
        {

            if (rptType == 0)
                reportName = "Rpt_Sum_SalesByProduct.rpt";
            if (rptType == 1)
                reportName = "Rpt_Sum_SalesProductSupplier.rpt";
            if (rptType == 2)
                reportName = "Rpt_Sum_SalesByBill.rpt";
            if (rptType == 5)
                reportName = "Rpt_Sum_SalesByDate.rpt";
            if (rptType == 6)
                reportName = "Rpt_Sum_SalesByMonth.rpt";
            if (rptType == 7)
                reportName = "Rpt_Sum_SalesPaymentTAX_GroupDate.rpt";
            if (rptType == 8)
                reportName = "Rpt_Sum_SalesPaymentTAX_GroupDate_Type.rpt";
            if (rptType == 9)
                reportName = "Rpt_Sum_SalesPaymentTAX.rpt";

            if (rptType == 101)
                reportName = "Rpt_Sum_StockReport_QTY.rpt";
            if (rptType == 102)
                reportName = "Rpt_Sum_StockReport_VAL.rpt";


            crystalReportViewer2.Refresh();

            rd.Load(pathReport + reportName);
            crConnectionInfo.ServerName = databaseServerName; // ODBC
            crConnectionInfo.DatabaseName = databaseName;
            crConnectionInfo.UserID = databaseUserName;
            crConnectionInfo.Password = databasePassword;


            rd.SetParameterValue("@FromDate", rptFromDate);
            rd.SetParameterValue("@ToDate", rptToDate);
            rd.SetParameterValue("@BillNo", 0);


            if (rptType == 0 || rptType == 2 || rptType == 5 || rptType == 6)
            {
                rd.SetParameterValue("@BranchID", this.branchID);
            }

            if (rptType == 1)
            { 
                rd.SetParameterValue("@SupplierID", rptProductSupID);
            }

            if (rptType == 101 || rptType == 102 )
            {
                rd.SetParameterValue("@InvID", 0);
            }


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
