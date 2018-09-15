using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AGC
{
    public partial class BranchInventoryStock : System.Web.UI.Page
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

            }

        
        }


        #region "Local Function"

        //List of Branch


        //Diplay on Gridview
        private void DisplayItems()
        {
            DataTable dt = oTransaction.GET_BRANCH_STOCK(ViewState["BRANCHCODE"].ToString());
            gvItems.DataSource = dt;
            gvItems.DataBind();
        }

        private void DisplayBranchList()
        {
            DataTable dt = oMaster.GET_BRANCH_LIST();
            gvBranchList.DataSource = dt;
            gvBranchList.DataBind();
        }


        #endregion

       


     
        //Display all branch and identify list of Branch that had been schedule.
        private void DisplayDeliveredBranch()
        {
            foreach (GridViewRow row in gvBranchList.Rows)
            {

                string branchCode = row.Cells[0].Text;

                LinkButton lnkNewDelivery = row.FindControl("lnkNewDelivery") as LinkButton;
                LinkButton lnkView = row.FindControl("lnkView") as LinkButton;

                ////TextBox txtQuantity = (TextBox)row.Cells[1].FindControl("txtDistrubutedQty");
                ////TextBox txtDateTrans = (TextBox)row.Cells[2].FindControl("txtDate");


                //if (oTransaction.CHECK_EXIST_DELIVERY_ENTRY(Convert.ToDateTime(txtDeliveryDate.Text), branchCode))
                //{
                //    lnkNewDelivery.Enabled = false;
                //    lnkNewDelivery.CssClass = "disabledLink";
                //    lnkNewDelivery.Visible = false;
                //    lnkView.Visible = true;
                //}




            }
        }

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
                

                //Display only item assign to this branch or partners
                //Display_Items();

                ViewState["BRANCHCODE"] = row.Cells[0].Text;

                lblBranchNameStock.Text = "Branch Stock: <b>" + row.Cells[1].Text + "</b>";

                txtSearch.Text = "";

                DisplayItems();


            }

            if (e.CommandName == "View")
            {
                Session["G_BRANCHCODE"] = row.Cells[0].Text;
               // Session["G_DELIVERYDATE"] = Convert.ToDateTime(txtDeliveryDate.Text);
                //Display
                PRINT_NOW("rep_BranchDeliveryReceiptSingle.aspx");
                
            }
        }
        

       


      
    }
}