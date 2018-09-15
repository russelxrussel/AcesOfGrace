using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AGC
{
    public partial class BranchDeliveryAdjustment : System.Web.UI.Page
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

              //  txtReturnDate.Text = oSystem.GET_SERVER_DATE_TIME().ToShortDateString();

            }

        
        }


        #region "Local Function"

        //List of Branch


        //Diplay on Gridview
        private void DisplayDeliveryNotYetPosted(string _branchCode)
        {
            DataTable dt = oTransaction.GET_DELIVERY_NOT_YET_POSTED();

            DataView dv = dt.DefaultView;
            dv.RowFilter = "branchCode = '" + _branchCode +"'";

            gvDRList.DataSource = dv;
            gvDRList.DataBind();
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

                //if (!string.IsNullOrEmpty(txtReturnDate.Text) || !string.IsNullOrWhiteSpace(txtReturnDate.Text))
                //{
                    ViewState["BRANCHCODE"] = row.Cells[0].Text;

                    lblBranchNameStock.Text = "Branch Stock: <b>" + row.Cells[1].Text + "</b>";

                    txtSearch.Text = "";

                    DisplayDeliveryNotYetPosted(ViewState["BRANCHCODE"].ToString());

                    //Display Usage Stock that already encoded
                    //DisplayEncodeReturnStock(Convert.ToDateTime(txtReturnDate.Text), ViewState["BRANCHCODE"].ToString());
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalError').modal('show');</script>", false);
                //    lblErrorMessage.Text = "Please fill up date input.";
                //}


            }

          
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewState["BRANCHCODE"].ToString()))
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertErrorMessage').hide();</script>", false);

                //string sBRINUM = oSystem.GENERATE_SERIES_NUMBER_TRANS("BRI");
                //Save Delivery
                foreach (GridViewRow row in gvDRList.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string deliveryNum = row.Cells[0].Text;
                        string itemCode = row.Cells[1].Text;

                        TextBox txtQuantity = (TextBox)row.Cells[3].FindControl("txtDeliveryQty");

                        int quantity;
                        if (string.IsNullOrEmpty(txtQuantity.Text))
                        { quantity = 0; }
                        else
                        {
                            quantity = Convert.ToInt32(txtQuantity.Text);
                        }

                        if (quantity != 0)
                        {
                               oTransaction.UPDATE_DELIVERY_ADJUSTMENT(deliveryNum, itemCode, quantity);
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
                lblSuccessMessage.Text = "Adjustment successfully updated.";


                // Response.Redirect(Request.RawUrl);

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