using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AGC
{
    public partial class Home : System.Web.UI.Page
    {
        cSystem oSystem = new cSystem();
        cMaster oMaster = new cMaster();
        cTransaction oTransaction = new cTransaction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartDate.Text = oSystem.GET_SERVER_DATE_TIME().ToShortDateString();
                txtEndDate.Text = oSystem.GET_SERVER_DATE_TIME().ToShortDateString();

                displayItemList();
                txtTopNumber.Text = "5";

                displayTopBranchSale(ddItemList.SelectedValue, Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToInt32(txtTopNumber.Text));
            }

        }

        //private void displayBranchList()
        //{
        //    DataTable dt = oMaster.GET_BRANCH_LIST();

        //    ddBranchList.DataSource = dt;
        //    ddBranchList.DataTextField = dt.Columns["branchName"].ToString();
        //    ddBranchList.DataValueField = dt.Columns["branchCode"].ToString();
        //    ddBranchList.DataBind();

        //    ddBranchList.Items.Insert(0, new ListItem("--Select Branch--"));


        //}

        private void displayItemList()
        {
            DataTable dt = oMaster.GET_ITEMS_LIST();

            DataView dv = dt.DefaultView;
            dv.RowFilter = "ItemCategoryCode = 'IS'";
            dv.Sort = "arr";

            ddItemList.DataSource = dv;
            ddItemList.DataTextField = dv.Table.Columns["ItemName"].ToString();
            ddItemList.DataValueField = dv.Table.Columns["ItemCode"].ToString();
            ddItemList.DataBind();

           // ddItemList.Items.Insert(0, new ListItem("--Select Item--"));
            
        }

        private void displayTopBranchSale(string _itemCode, DateTime _startDate, DateTime _endDate, int _topNumber)
        {
            DataTable dt = oTransaction.GET_BRANCH_TOP_SALES(_itemCode, _startDate, _endDate);
            

            if (dt.Rows.Count > 0)
            {
                DataTable dtTop = dt.Rows.Cast<DataRow>().Take(_topNumber).CopyToDataTable();
                gvTopSaleBranch.DataSource = dtTop;

            }
            else
            {
                gvTopSaleBranch.DataSource = null;
            }

            gvTopSaleBranch.DataBind();

        }

        protected void lnkPreview_Click(object sender, EventArgs e)
        {
            displayTopBranchSale(ddItemList.SelectedValue, Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToInt32(txtTopNumber.Text));
        }
    }
}