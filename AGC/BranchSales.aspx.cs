using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AGC
{
    public partial class BranchSales : System.Web.UI.Page
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

                txtSalesDate.Text = oSystem.GET_SERVER_DATE_TIME().ToShortDateString();

            }

        
        }


        #region "Local Function"

        //List of Branch


        //Diplay on Gridview
        private void DisplayItems()
        {
            DataTable dt = oTransaction.GET_BRANCH_STOCK(ViewState["BRANCHCODE"].ToString());

            DataView dv = dt.DefaultView;
            dv.RowFilter = "itemCategoryCode ='IS'";

            gvItems.DataSource = dv;
            gvItems.DataBind();
        }

        private void DisplayEncodedSales(DateTime _salesDate, string _branchCode)
        {
            DataTable dt = oTransaction.GET_BRANCH_SALES_BY_DATE(Convert.ToDateTime(txtSalesDate.Text));
            DataView dv = dt.DefaultView;
            dv.RowFilter = "BranchCode ='" + _branchCode + "'";

            gvItemSalesDone.DataSource = dv;
            gvItemSalesDone.DataBind();
        }

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

                if (!string.IsNullOrEmpty(txtSalesDate.Text) || !string.IsNullOrWhiteSpace(txtSalesDate.Text))
                {
                    ViewState["BRANCHCODE"] = row.Cells[0].Text;

                    lblBranchNameStock.Text = "<b> Sales of :" + row.Cells[1].Text + "</b>";

                    txtSearch.Text = "";

                    DisplayItems();

                    //DISPLAY IF BRANCH ALREADY ENCODED ON SELECTED DATE
                    DisplayEncodedSales(Convert.ToDateTime(txtSalesDate.Text), ViewState["BRANCHCODE"].ToString());
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
            if (!string.IsNullOrEmpty(txtSalesDate.Text) || txtSalesDate.Text.Trim().Length != 0 || !string.IsNullOrEmpty(Session["BRANCHCODE"].ToString()))
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertErrorMessage').hide();</script>", false);

                string sSBNUM = oSystem.GENERATE_SERIES_NUMBER_TRANS("SB");
                //Save Delivery
                foreach (GridViewRow row in gvItems.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string itemCode = row.Cells[0].Text;

                        TextBox txtQuantity = (TextBox)row.Cells[2].FindControl("txtConsumeStock");
                        int quantity;
                        if (string.IsNullOrEmpty(txtQuantity.Text))
                        { quantity = 0; }
                        else
                        {
                            quantity = Convert.ToInt32(txtQuantity.Text);
                        }

                        if (quantity != 0)
                        {

                            oTransaction.INSERT_BRANCH_SALES(ViewState["BRANCHCODE"].ToString(), sSBNUM, Convert.ToDateTime(txtSalesDate.Text), "", itemCode, quantity);
                            //oTransaction.INSERT_CONSUME_BRANCH_ITEM(ViewState["BRANCHCODE"].ToString(), sICNUM, Convert.ToDateTime(txtSalesDate.Text), "", itemCode, quantity);
                        }
                    }
                }

                ////Hold for possible Print Directly
                //Session["G_DRBNUM"] = sDRNUM;

                //UPDATE SERIES NUMBER
                //oSystem.UPDATE_SERIES_NUMBER("SB");




                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
                lblSuccessMessage.Text = "Branch Sales succesfully process.";

                DisplayEncodedSales(Convert.ToDateTime(txtSalesDate.Text), ViewState["BRANCHCODE"].ToString());
                //Response.Redirect(Request.RawUrl);

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