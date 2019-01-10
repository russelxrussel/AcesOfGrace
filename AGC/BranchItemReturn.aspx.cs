using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AGC
{
    public partial class BranchItemReturn : System.Web.UI.Page
    {
        cMaster oMaster = new cMaster();
        cTransaction oTransaction = new cTransaction();
        cSystem oSystem = new cSystem();
        cUtil oUtility = new cUtil();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                ViewState["BRANCHCODE"] = "";
                DisplayBranchList();
                txtReturnDate.Text = oSystem.GET_SERVER_DATE_TIME().ToShortDateString();

            }

        
        }


        #region "Local Function"

        //List of Branch


        //Diplay on Gridview
        private void DisplayItems()
        {
            DataTable dt = oTransaction.GET_BRANCH_STOCK(ViewState["BRANCHCODE"].ToString());

            //DataView dv = dt.DefaultView;
            //dv.RowFilter = "itemCategoryCode ='IO'";

            gvItems.DataSource = dt;
            gvItems.DataBind();
        }

        //private void DisplayEncodeReturnStock(DateTime _usageDate, string _branchCode)
        //{
        //    DataTable dt = oTransaction.GET_STOCK_USAGE_BY_DATE(Convert.ToDateTime(txtReturnDate.Text));
        //    DataView dv = dt.DefaultView;
        //    dv.RowFilter = "BranchCode ='" + _branchCode + "'";

        //    gvStockUsageDone.DataSource = dv;
        //    gvStockUsageDone.DataBind();
        //}

        private void DisplayBranchList()
        {
            DataTable dt = oMaster.GET_BRANCH_LIST();
            gvBranchList.DataSource = dt;
            gvBranchList.DataBind();
        }


        #endregion

       


     
     
        #region "PRINT AREA"
        private void PRINT_NOW(string url)
        {
            string s = "window.open('" + url + "', 'popup_window', 'width=1024, height=768, left=0, top=0, resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.Page.GetType(), "ReportScript", s, true);
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            PRINT_NOW("rep_BranchDeliveryReceiptSingle.aspx");
        }




        #endregion

        protected void gvScheduleBranch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = ((e.CommandSource as LinkButton).NamingContainer as GridViewRow);

            if (e.CommandName == "Select")
            {

                if (!string.IsNullOrEmpty(txtReturnDate.Text) || !string.IsNullOrWhiteSpace(txtReturnDate.Text))
                {
                    ViewState["BRANCHCODE"] = row.Cells[0].Text;

                    lblBranchNameStock.Text = "Branch: <b>" + row.Cells[1].Text + "</b>";

                    txtSearch.Text = "";

                    DisplayItems();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalError').modal('show');</script>", false);
                    lblErrorMessage.Text = "Please fill up date input.";
                }


            }

          
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReturnDate.Text) && !string.IsNullOrWhiteSpace(txtReturnDate.Text) && !string.IsNullOrEmpty(ViewState["BRANCHCODE"].ToString()) && !string.IsNullOrWhiteSpace(txtReturnDate.Text))
            {
                string sBRINUM = oSystem.GENERATE_SERIES_NUMBER_TRANS("BRI");
                //Save Delivery
                foreach (GridViewRow row in gvItems.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string itemCode = row.Cells[0].Text;

                        TextBox txtQuantity = (TextBox)row.Cells[2].FindControl("txtReturnItem");
                        int quantity;
                        if (string.IsNullOrEmpty(txtQuantity.Text))
                        { quantity = 0; }
                        else
                        {
                            quantity = Convert.ToInt32(txtQuantity.Text);
                        }

                        if (quantity != 0)
                        {
                            oTransaction.INSERT_BRANCH_RETURN_ITEM(ViewState["BRANCHCODE"].ToString(), sBRINUM, Convert.ToDateTime(txtReturnDate.Text), txtRemarks.Text, itemCode, quantity);
                        }
                    
                    }
                }

                //Refresh
                ViewState["BRANCHCODE"] = "";
                DisplayBranchList();
                DisplayItems();
                txtReturnDate.Text = oSystem.GET_SERVER_DATE_TIME().ToShortDateString();


                //Response.Redirect(Request.RawUrl);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
                lblSuccessMessage.Text = "Branch returned item successfully process.";

            }
            else
            {
                //Error message

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalError').modal('show');</script>", false);
                lblErrorMessage.Text = "Please fill up required input.";

           

            }
        }
    }
}