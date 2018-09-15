using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AGC
{
    public partial class BranchStockTransfer : System.Web.UI.Page
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

                DisplayBranchItemsSource(ddSource.SelectedValue);
                DisplayBranchItemsDestination(ddDestination.SelectedValue);

                //  txtReturnDate.Text = oSystem.GET_SERVER_DATE_TIME().ToShortDateString();

            }

        
        }


        #region "Local Function"

        private void DisplayBranchItemsSource(string _branchCode)
        {
            DataTable dt = oTransaction.GET_BRANCH_STOCK(_branchCode);

            gvItemsSource.DataSource = dt;
            gvItemsSource.DataBind();
        }

        private void DisplayBranchItemsDestination(string _branchCode)
        {
            DataTable dt = oTransaction.GET_BRANCH_STOCK(_branchCode);

            gvItemDestination.DataSource = dt;
            gvItemDestination.DataBind();
        }

        private void DisplayBranchList()
        {
            DataTable dt = oMaster.GET_BRANCH_LIST();

            ddSource.DataSource = dt;
            ddSource.DataTextField = dt.Columns["BranchName"].ToString();
            ddSource.DataValueField = dt.Columns["BranchCode"].ToString();
            ddSource.DataBind();



            ddDestination.DataSource = dt;
            ddDestination.DataTextField = dt.Columns["BranchName"].ToString();
            ddDestination.DataValueField = dt.Columns["BranchCode"].ToString();
            ddDestination.DataBind();

           
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

   
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTransferDate.Text) || !string.IsNullOrWhiteSpace(txtTransferDate.Text))
            {
                //2nd level of filter not same source and destination
                if (ddSource.SelectedValue != ddDestination.SelectedValue)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalError').modal('show');</script>", false);
                    lblErrorMessage.Text = "The same Source and Destination Branch is not allowed.";
                }
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertErrorMessage').hide();</script>", false);

                //string sBRINUM = oSystem.GENERATE_SERIES_NUMBER_TRANS("BRI");
                //Save Delivery
                //foreach (GridViewRow row in gvDRList.Rows)
                //{
                //    if (row.RowType == DataControlRowType.DataRow)
                //    {
                //        string deliveryNum = row.Cells[0].Text;
                //        string itemCode = row.Cells[1].Text;

                //        TextBox txtQuantity = (TextBox)row.Cells[3].FindControl("txtDeliveryQty");

                //        int quantity;
                //        if (string.IsNullOrEmpty(txtQuantity.Text))
                //        { quantity = 0; }
                //        else
                //        {
                //            quantity = Convert.ToInt32(txtQuantity.Text);
                //        }

                //        if (quantity != 0)
                //        {
                //            oTransaction.UPDATE_DELIVERY_ADJUSTMENT(deliveryNum, itemCode, quantity);
                //        }
                //    }
                //}

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
                //lblSuccessMessage.Text = "Adjustment successfully updated.";


                // Response.Redirect(Request.RawUrl);

            }
            else
            {
                //Error message

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalError').modal('show');</script>", false);
                lblErrorMessage.Text = "Please fill up transfer date.";
                
            }
        }

        protected void ddSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayBranchItemsSource(ddSource.SelectedValue.ToString());
        }

        protected void ddDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayBranchItemsDestination(ddDestination.SelectedValue);
        }
    }
}