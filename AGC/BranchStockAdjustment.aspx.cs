using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AGC
{
    public partial class BranchStockAdjustment : System.Web.UI.Page
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

                
                DisplayStockAdjustmentList();

                DisplayBranchList();

               
            }

           

        }


        #region "Local Function"

        //List of Branch


        //Diplay on Gridview
        private void DisplayItems()
        {
            DataTable dt = oTransaction.GET_BRANCH_STOCK(ViewState["BRANCHCODE"].ToString());

            DataView dv = dt.DefaultView;
            //dv.RowFilter = "itemCategoryCode ='IO'";

            gvItems.DataSource = dv;
            gvItems.DataBind();
        }

      

        private void DisplayBranchList()
        {
            DataTable dt = oMaster.GET_BRANCH_LIST();
            gvBranchList.DataSource = dt;
            gvBranchList.DataBind();
        }


     
        private void DisplayStockAdjustmentList()
        {
            DataTable dt = oUtility.GET_STOCK_ADJUSTMENT_LIST();

            ddAdjustmentType.DataSource = dt;
            ddAdjustmentType.DataTextField = dt.Columns["AdjustmentName"].ToString();
            ddAdjustmentType.DataValueField = dt.Columns["AdjustmentCode"].ToString();
            ddAdjustmentType.DataBind();

            ddAdjustmentType.Items.Insert(0, new ListItem("--SELECT Adjustment--"));
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

                if (!string.IsNullOrEmpty(txtAdjustmentDate.Text) && !string.IsNullOrWhiteSpace(txtAdjustmentDate.Text) && ddAdjustmentType.SelectedIndex != 0)
                {
                    ViewState["BRANCHCODE"] = row.Cells[0].Text;
                    ViewState["BRANCHNAME"] = row.Cells[1].Text;
                    lblBranchNameStock.Text = "Branch Stock to Adjust: <b>" + ViewState["BRANCHNAME"].ToString() + "</b>";

                    txtSearch.Text = "";
                   
                    DisplayItems();

                    //Check if this branch encoded and not yet posted adjustment in selected date.
                    if (oTransaction.CHECK_EXIST_STOCK_ADJUSTMENT_ENTRY(ViewState["BRANCHCODE"].ToString()))
                    {
                        lnkViewPendingAdjustment.Visible = true;
                    }
                    else
                    {
                        lnkViewPendingAdjustment.Visible = false;
                    }

                    //Display Usage Stock that already encoded
                    //DisplayEncodeUsageStock(Convert.ToDateTime(txtAdjustmentDate.Text), ViewState["BRANCHCODE"].ToString());
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
            if (!string.IsNullOrEmpty(txtAdjustmentDate.Text) && !string.IsNullOrWhiteSpace(txtAdjustmentDate.Text) && !string.IsNullOrEmpty(ViewState["BRANCHCODE"].ToString()))
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertErrorMessage').hide();</script>", false);

                string sBSANUM = oSystem.GENERATE_SERIES_NUMBER_TRANS("BSA");
                //Save Delivery
                foreach (GridViewRow row in gvItems.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string itemCode = row.Cells[0].Text;

                        TextBox txtQuantity = (TextBox)row.Cells[2].FindControl("txtAdjustmentQty");
                        int quantity;
                        if (string.IsNullOrEmpty(txtQuantity.Text))
                        { quantity = 0; }
                        else
                        {
                            quantity = Convert.ToInt32(txtQuantity.Text);
                        }

                        if (quantity != 0)
                        {
                            oTransaction.INSERT_UPDATE_BRANCH_ADJUSTMENT_STOCK(ViewState["BRANCHCODE"].ToString(), sBSANUM, ddAdjustmentType.SelectedValue, Convert.ToDateTime(txtAdjustmentDate.Text), txtRemarks.Text, itemCode, quantity);
                        }
                    }
                }


                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
                lblSuccessMessage.Text = "Branch Adjustment successfully process and waiting for posting.";


                //Reset
                DisplayStockAdjustmentList();
                DisplayBranchList();


                lnkViewPendingAdjustment.Visible = false;
               
                ViewState["BRANCHCODE"] = "";
                gvItems.DataSource = null;
                gvItems.DataBind();

            }
            else
            {
                //Error message

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalError').modal('show');</script>", false);
                lblErrorMessage.Text = "Please fill up required input.";

           

            }
        }

        protected void lnkViewPendingAdjustment_Click(object sender, EventArgs e)
        {
            //DisplayDeliveryDetails(ViewState["DELIVERYNUM"].ToString());
            //lblDRNUM.Text = ViewState["DELIVERYNUM"].ToString();

            DataTable dt = oTransaction.GET_STOCK_ADJUSTMENT_ITEM_NOT_YET_POSTED();
            DataView dv = dt.DefaultView;

            dv.RowFilter = "branchCode = '" + ViewState["BRANCHCODE"].ToString() + "'";

            gvStockAdjustmentForPosting.DataSource = dv;
            gvStockAdjustmentForPosting.DataBind();

            lblBranchNameAdjustment.Text = ViewState["BRANCHNAME"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalStockAdjustmentForPosting').modal('show');</script>", false);
        }
    }
}