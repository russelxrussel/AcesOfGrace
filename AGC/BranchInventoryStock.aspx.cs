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
            if (dt.Rows.Count > 0)
            {
                gvItems.DataSource = dt;
                          }
            else
            {
                gvItems.DataSource = null;
              
            }

            gvItems.DataBind();
        }

       


        private void DisplayBranchList()
        {
            DataTable dt = oMaster.GET_BRANCH_LIST();

            ddBranch.DataSource = dt;
            ddBranch.DataTextField = dt.Columns["BranchName"].ToString();
            ddBranch.DataValueField = dt.Columns["BranchCode"].ToString();
            ddBranch.DataBind();

            ddBranch.Items.Insert(0, new ListItem("--Select Branch--"));
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
                

                //Display only item assign to this branch or partners
                //Display_Items();

              


            }

            if (e.CommandName == "View")
            {
                Session["G_BRANCHCODE"] = row.Cells[0].Text;
               // Session["G_DELIVERYDATE"] = Convert.ToDateTime(txtDeliveryDate.Text);
                //Display
                PRINT_NOW("rep_BranchDeliveryReceiptSingle.aspx");
                
            }
        }

        protected void ddBranch_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddBranch.SelectedIndex != 0)
            {
                ViewState["BRANCHCODE"] = ddBranch.SelectedValue;

                DisplayItems();

            }
            else
            {
                ViewState["BRANCHCODE"] = "";
            }
           
            // txtSearch.Text = "";

        }
    }
}