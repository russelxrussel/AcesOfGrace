using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AGC
{
    public partial class ItemBranchPriceSetup : System.Web.UI.Page
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



        #endregion






        //#region "PRINT AREA"
        //private void PRINT_NOW(string url)
        //{
        //    string s = "window.open('" + url + "', 'popup_window', 'width=1024, height=768, left=0, top=0, resizable=yes');";
        //    ScriptManager.RegisterClientScriptBlock(this, this.Page.GetType(), "ReportScript", s, true);
        //}



        protected void gvScheduleBranch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = ((e.CommandSource as LinkButton).NamingContainer as GridViewRow);

            if (e.CommandName == "Select")
            {

                ViewState["BRANCHCODE"] = row.Cells[0].Text;

                lblBranchName.Text = "Branch: <b>" + row.Cells[1].Text + "</b>";

                txtSearch.Text = "";

                Display_Branch_Price(ViewState["BRANCHCODE"].ToString());

            }


        }




        //#endregion



        protected void lnkSave_Click(object sender, EventArgs e)
        {
          
            foreach (GridViewRow row in gvItems.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string itemCode = row.Cells[0].Text;
                    string branchCode = ViewState["BRANCHCODE"].ToString();

                    TextBox txtBranchPrice = (TextBox)row.Cells[2].FindControl("txtBranchPrice");
                    TextBox txtSellingPrice = (TextBox)row.Cells[3].FindControl("txtSellingPrice");

                    double dBranchPrice, dSellingPrice;
                    if (string.IsNullOrEmpty(txtBranchPrice.Text))
                    { dBranchPrice = 0; }
                    else
                    {
                        dBranchPrice = Convert.ToInt32(txtBranchPrice.Text);
                    }

                    if (string.IsNullOrEmpty(txtSellingPrice.Text))
                    { dSellingPrice = 0; }
                    else
                    {
                        dSellingPrice = Convert.ToInt32(txtSellingPrice.Text);
                    }



                    if (dBranchPrice != 0)
                    {
                        oUtility.UPDATE_BRANCH_PRICE(branchCode, itemCode, dBranchPrice, dSellingPrice);
                    }

                }
            }


            //   ddPartnerList.SelectedIndex = 0;

            //   Display_Partner_Price(ddPartnerList.SelectedValue);
           // ViewState["BRANCHCODE"] = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
            lblSuccessMessage.Text = "Partner price successfully updated.";

     
        }




        private void Display_Branch_Price(string _branchCode)
        {
            DataTable dt = oUtility.GET_BRANCH_PRICE(_branchCode);

            if (dt.Rows.Count > 0)
            {
                gvItems.DataSource = dt;
            }
            else {
                gvItems.DataSource = null;
                lblBranchName.Text = "";
            }

            gvItems.DataBind();
            

        }



        private void DisplayBranchList()
        {
            DataTable dt = oMaster.GET_BRANCH_LIST();
            gvBranchList.DataSource = dt;
            gvBranchList.DataBind();
        }


    }
}