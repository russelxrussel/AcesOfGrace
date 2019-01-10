using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AGC
{
    public partial class BranchStockAdjustmentPosting : System.Web.UI.Page
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

                DisplayForPosting();

              //  txtReturnDate.Text = oSystem.GET_SERVER_DATE_TIME().ToShortDateString();

            }

        
        }


        #region "Local Function"


        private void DisplayForPosting()
        {
            DataTable dt = oTransaction.GET_STOCK_ADJUSTMENT_FOR_POSTING();

            gvAdjustmentForPostingList.DataSource = dt;
            gvAdjustmentForPostingList.DataBind();
        }

        private void DisplayAdjustmentDetails(string _stockAdjustmentNum)
        {
            DataTable dt = oTransaction.GET_STOCK_ADJUSTMENT_ITEM_NOT_YET_POSTED();

            DataView dv = dt.DefaultView;
            dv.RowFilter = "stockAdjustmentNum = '" + _stockAdjustmentNum + "'";

            gvDRList.DataSource = dv;
            gvDRList.DataBind();
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

        protected void gvAdjustmentForPostingList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = ((e.CommandSource as LinkButton).NamingContainer as GridViewRow);
            
            ViewState["ADJUSTMENTNUM"] = row.Cells[0].Text;
            ViewState["ADJUSTMENTNAME"] = row.Cells[1].Text;
           


            if (e.CommandName == "Post")
            {

                oTransaction.UPDATE_BRANCH_STOCK_ADJUSTMENT_POSTING(ViewState["ADJUSTMENTNUM"].ToString());

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
                lblSuccessMessage.Text = "Adjustment #: " + ViewState["ADJUSTMENTNUM"].ToString() + " successfully posted.";

                DisplayForPosting();


            }

            else if (e.CommandName == "View")
            {

                DisplayAdjustmentDetails(ViewState["ADJUSTMENTNUM"].ToString());
                lblDRNUM.Text = ViewState["ADJUSTMENTNUM"].ToString() +"(" + ViewState["ADJUSTMENTNAME"].ToString() + ")";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalView').modal('show');</script>", false);
            }

            else if (e.CommandName == "CancelAdjustment")
            {
                              
               oTransaction.CANCEL_STOCK_ADJUSTMENT(ViewState["ADJUSTMENTNUM"].ToString());
               
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalError').modal('show');</script>", false);
                lblErrorMessage.Text = "Adjustment #: " + ViewState["ADJUSTMENTNUM"].ToString() + " successfully cancelled.";

                DisplayForPosting();

            }
        }




        protected void lnkSave_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(ViewState["BRANCHCODE"].ToString()))
            //{
            //    // ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertErrorMessage').hide();</script>", false);

            //    //string sBRINUM = oSystem.GENERATE_SERIES_NUMBER_TRANS("BRI");
            //    //Save Delivery
            //    foreach (GridViewRow row in gvDRList.Rows)
            //    {
            //        if (row.RowType == DataControlRowType.DataRow)
            //        {
            //            string deliveryNum = row.Cells[0].Text;
            //            string itemCode = row.Cells[1].Text;

            //            TextBox txtQuantity = (TextBox)row.Cells[3].FindControl("txtDeliveryQty");

            //            int quantity;
            //            if (string.IsNullOrEmpty(txtQuantity.Text))
            //            { quantity = 0; }
            //            else
            //            {
            //                quantity = Convert.ToInt32(txtQuantity.Text);
            //            }

            //            if (quantity != 0)
            //            {
            //                   oTransaction.UPDATE_DELIVERY_ADJUSTMENT(deliveryNum, itemCode, quantity);
            //            }
            //        }
            //    }

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
            //    lblSuccessMessage.Text = "Adjustment successfully updated.";


            //    // Response.Redirect(Request.RawUrl);

            //}
            //else
            //{
            //    //Error message

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalError').modal('show');</script>", false);
            //    lblErrorMessage.Text = "Please fill up required input.";

           

            //}
        }

        protected void lnkPostAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvAdjustmentForPostingList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string drnum = row.Cells[0].Text;

                    oTransaction.UPDATE_BRANCH_STOCK_ADJUSTMENT_POSTING(row.Cells[0].Text);

                    DisplayForPosting();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
                    lblSuccessMessage.Text = "All adjustment successfully posted.";

                }
            }
        }
    }
}