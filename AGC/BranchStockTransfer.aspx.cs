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
                    string transferNum = oSystem.GENERATE_SERIES_NUMBER_TRANS("BIT");
                    foreach (GridViewRow row in gvItemsSource.Rows)
                    {
                        string itemCode = row.Cells[0].Text;
                        TextBox txtQty = row.FindControl("txtTransferQty") as TextBox;

                        int QtyTransfer = 0;

                        if (!string.IsNullOrEmpty(txtQty.Text) || !string.IsNullOrWhiteSpace(txtQty.Text))
                        {
                            QtyTransfer = Convert.ToInt32(txtQty.Text);

                            oTransaction.INSERT_BRANCH_TRANSFER_INVENTORY(ddSource.SelectedValue, ddDestination.SelectedValue,
                                                                      transferNum, Convert.ToDateTime(txtTransferDate.Text), "", itemCode, QtyTransfer);

                        }



                    }

                    //Refresh record.
                    DisplayBranchList();
                    DisplayBranchItemsSource(ddSource.SelectedValue);
                    DisplayBranchItemsDestination(ddDestination.SelectedValue);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
                    lblSuccessMessage.Text = "Branch Inventory successfully transferred.";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalError').modal('show');</script>", false);
                    lblErrorMessage.Text = "The same Source and Destination Branch is not allowed.";
                }
               

            }
            else
            {
                //Error message

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalError').modal('show');</script>", false);
                lblErrorMessage.Text = "Please fill up transfer date.";
                
            }
        }

       

        protected void gvItemsSource_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in gvItemsSource.Rows)
            {

                int iAvailableQty = Convert.ToInt32(row.Cells[2].Text);
                TextBox txtQty = row.FindControl("txtTransferQty") as TextBox;


                //Validate if the branch available quantity is not zero
                if (iAvailableQty > 0)
                {
                    txtQty.Enabled = true;
                }
                else
                {
                    txtQty.Enabled = false;
                }


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