using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;

namespace AGC
{
    public partial class repSalesSummary : System.Web.UI.Page
    {
        ReportDocument oReportDocument = new ReportDocument();

        cSystem oSystem = new cSystem();
        cMaster oMaster = new cMaster();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartDate.Text = oSystem.GET_SERVER_DATE_TIME().ToShortDateString();
                txtEndDate.Text = oSystem.GET_SERVER_DATE_TIME().ToShortDateString();

                displayBranchList();
            }

            displayReport();
        }


        private void displayBranchList()
        {
            DataTable dt = oMaster.GET_BRANCH_LIST();

            ddBranchList.DataSource = dt;
            ddBranchList.DataTextField = dt.Columns["branchName"].ToString();
            ddBranchList.DataValueField = dt.Columns["branchCode"].ToString();
            ddBranchList.DataBind();

            ddBranchList.Items.Insert(0, new ListItem("--Select Branch--"));

           
        }
        private void displayReport()
        {
            //IDENTIFY WHAT TYPE OF REPORT NEED TO DISPLAY

            DateTime dtStartDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime dtEndDate = Convert.ToDateTime(txtEndDate.Text);

            ParameterRangeValue myRangeValue = new ParameterRangeValue();
            myRangeValue.StartValue = dtStartDate; //txtDateStart.Text;
            myRangeValue.EndValue = dtEndDate;

            if (ddBranchList.SelectedIndex > 0)
            {
                oReportDocument.Load(Server.MapPath("~/Reports/repBranchSalesSummary.rpt"));
                oReportDocument.SetParameterValue("paramBranchCode", ddBranchList.SelectedValue);
            }
            else
            {
                oReportDocument.Load(Server.MapPath("~/Reports/repSalesSummaryTotal.rpt"));
            }

            oReportDocument.SetParameterValue("paramDateRange", myRangeValue);
            oReportDocument.SetDatabaseLogon("sa", "p@ssw0rd"); // Supply user credentials

            CrystalReportViewer1.ReportSource = oReportDocument;
        }

        protected void lnkPreview_Click(object sender, EventArgs e)
        {
            displayReport();
        }
    }
}