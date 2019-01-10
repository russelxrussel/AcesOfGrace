using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AGC
{
    public partial class BranchDeliveryPosting : System.Web.UI.Page
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
            DataTable dt = oTransaction.GET_DELIVERY_FOR_POSTING();
            gvDeliveryForPostingList.DataSource = dt;
            gvDeliveryForPostingList.DataBind();
        }

        private void DisplayDeliveryDetails(string _deliveryNum)
        {
            DataTable dt = oTransaction.GET_DELIVERY_NOT_YET_POSTED();

            DataView dv = dt.DefaultView;
            dv.RowFilter = "deliveryNum = '" + _deliveryNum + "'";

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

        protected void gvScheduleBranch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = ((e.CommandSource as LinkButton).NamingContainer as GridViewRow);


            ViewState["DELIVERYNUM"] = row.Cells[0].Text;

            if (e.CommandName == "Post")
            {
                        
              oTransaction.UPDATE_BRANCH_DELIVERY_POSTING(ViewState["DELIVERYNUM"].ToString());

              DisplayForPosting();

              ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
              lblSuccessMessage.Text = "Delivery Number: " + ViewState["DELIVERYNUM"].ToString() + " successfully posted.";
               
                //Response.Redirect(Request.RawUrl);

            }

            else if (e.CommandName == "View")
            {

                DisplayDeliveryDetails(ViewState["DELIVERYNUM"].ToString());
                lblDRNUM.Text = ViewState["DELIVERYNUM"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalView').modal('show');</script>", false);
            }

            else if (e.CommandName == "Cancel")
            {

                oTransaction.CANCEL_BRANCH_DELIVERY(ViewState["DELIVERYNUM"].ToString());
                //  DisplayForPosting();
                Response.Redirect(Request.RawUrl);
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
            foreach (GridViewRow row in gvDeliveryForPostingList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string drnum = row.Cells[0].Text;

                    oTransaction.UPDATE_BRANCH_DELIVERY_POSTING(row.Cells[0].Text);

                    DisplayForPosting();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
                    lblSuccessMessage.Text = "All delivery successfully posted.";

                }
            }
        }
    }
}