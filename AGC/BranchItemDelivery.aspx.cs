using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AGC
{
    public partial class BranchItemDelivery : System.Web.UI.Page
    {
        cMaster oMaster = new cMaster();
        cTransaction oTransaction = new cTransaction();
        cSystem oSystem = new cSystem();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Display_Branches();
                Display_Items();


                //Hide Div with Error Message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertErrorMessage').hide();</script>", false);

                //txtRemarks.Text = oSystem.GENERATE_SERIES_NUMBER_TRANS("DRB");
                //Prompt Success Message with print option.
              
                
            }

           // ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalPrint2').modal('show');</script>", false);
           
        }


        #region "Local Function"

        //List of Branch
        private void Display_Branches()
        {
            DataTable dt = oMaster.GET_BRANCH_LIST();

            //dt.Select("Branch_IsActive = '" + true + "'");
            DataView dv = dt.DefaultView;
            dv.RowFilter = "Branch_IsActive = '" + true + "'";
            
            ddBranches.DataSource = dv;
            ddBranches.DataTextField = dv.Table.Columns["branchName"].ToString();
            ddBranches.DataValueField = dv.Table.Columns["branchCode"].ToString();
            ddBranches.DataBind();

            ddBranches.Items.Insert(0, new ListItem("--SELECT BRANCH--"));
        }


        //Diplay on Gridview
        private void Display_Items()
        {
            DataTable dt = oMaster.GET_ITEMS_LIST();

            gvItems.DataSource = dt;
            gvItems.DataBind();
        }


        #endregion

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDeliveryDate.Text) || ddBranches.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertErrorMessage').hide();</script>", false);

                string sDRNUM = oSystem.GENERATE_SERIES_NUMBER_TRANS("DRB");
                //Save Delivery
                foreach (GridViewRow row in gvItems.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string itemCode = row.Cells[0].Text;
                      
                        TextBox txtQuantity = (TextBox)row.Cells[2].FindControl("txtItemQuantity");
                        int quantity;
                        if (string.IsNullOrEmpty(txtQuantity.Text))
                        { quantity = 0; }
                        else
                        {
                            quantity = Convert.ToInt32(txtQuantity.Text);
                        }

                        if (quantity != 0)
                        { 
                        oTransaction.INSERT_BRANCH_DELIVERY(ddBranches.SelectedValue, sDRNUM, Convert.ToDateTime(txtDeliveryDate.Text), txtRemarks.Text, itemCode, quantity);
                        }
                    }
                }

                //Hold for possible Print Directly
                Session["G_DRBNUM"] = sDRNUM;

                //UPDATE SERIES NUMBER
                oSystem.UPDATE_SERIES_NUMBER("DRB");

           
                //Clear
                txtDeliveryDate.Text = "";
                ddBranches.SelectedIndex = 0;
                txtRemarks.Text = "";
                Display_Items();


                PRINT_NOW("rep_BranchDeliveryReceiptSingle.aspx");
                
            }
            else
            {
                //Error message

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertErrorMessage').fadeToggle(2000);</script>", false);
                lblErrorMessage.Text = "Please fill up required input.";
            }
        }


        #region "PRINT AREA"
        private void PRINT_NOW(string url)
        {
            string s = "window.open('" + url + "', 'popup_window', 'width=1024, height=768, left=0, top=0, resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.Page.GetType(), "ReportScript", s, true);
        }






        #endregion

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            PRINT_NOW("rep_BranchDeliveryReceiptSingle.aspx");
        }
    }
}