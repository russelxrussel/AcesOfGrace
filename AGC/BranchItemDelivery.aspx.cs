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
        cUtil oUtility = new cUtil();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                //Hide Div with Error Message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertErrorMessage').hide();</script>", false);

                //txtRemarks.Text = oSystem.GENERATE_SERIES_NUMBER_TRANS("DRB");
                //Prompt Success Message with print option.

                txtDeliveryDate.Text = oSystem.GET_SERVER_DATE_TIME().ToShortDateString();

                DisplayBranchDeliverySchedule(Convert.ToDateTime(txtDeliveryDate.Text));

                ViewState["BRANCHCODE"] = "";

            }

            // ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalPrint2').modal('show');</script>", false);

        }


        #region "Local Function"

        //List of Branch


        //Diplay on Gridview
        private void Display_Items()
        {
            DataTable dt = oMaster.GET_ITEMS_LIST();
            gvItems.DataSource = dt;
            gvItems.DataBind();

            DisplayDeliveredBranch();


        }

        //private void Display_Items_Edit(DateTime _deliveryDate, string _branchCode)
        //{
        //    DataTable dt = oTransaction.GET_BRANCH_DELIVERY(_deliveryDate);
        //    DataView dv = dt.DefaultView;

        //    dv.RowFilter = "deliveryDate ='" + _deliveryDate + "' and branchCode ='" + _branchCode + "'";

        //    gvItems.DataSource = dv;
        //    gvItems.DataBind();
            
        //}


        #endregion

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(txtDeliveryDate.Text) || txtDeliveryDate.Text.Trim().Length != 0 || !string.IsNullOrEmpty(Session["BRANCHCODE"].ToString()))
            {
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertErrorMessage').hide();</script>", false);

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

                            oTransaction.INSERT_BRANCH_DELIVERY(ViewState["BRANCHCODE"].ToString(), sDRNUM, Convert.ToDateTime(txtDeliveryDate.Text), txtRemarks.Text, itemCode, quantity);
                        }
                    }
                }

                //Hold for possible Print Directly
                //Session["G_DRBNUM"] = sDRNUM;

                //UPDATE SERIES NUMBER
                oSystem.UPDATE_SERIES_NUMBER("DRB");


                //Clear
              
                txtRemarks.Text = "";
                Display_Items();

                ViewState["BRANCHCODE"] = "";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
                lblSuccessMessage.Text = "Creating new delivery successfully process.";

                //PRINT_NOW("rep_BranchDeliveryReceiptSingle.aspx");

            }
            else
            {
                //Error message

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalError').modal('show');</script>", false);
                lblErrorMessage.Text = "Please fill up required input.";

               
            }
        }


        private void DisplayBranchDeliverySchedule(DateTime _selectedDate)
        {
            DataTable dt = oUtility.GET_BRANCH_SCHEDULE_LIST();
            DataView dv = new DataView(dt);


            switch (_selectedDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dv.RowFilter = "M = 1";
                    break;

                case DayOfWeek.Tuesday:
                    dv.RowFilter = "T = 1";
                    break;

                case DayOfWeek.Wednesday:
                    dv.RowFilter = "W = 1";
                    break;

                case DayOfWeek.Thursday:
                    dv.RowFilter = "Th = 1";
                    break;

                case DayOfWeek.Friday:
                    dv.RowFilter = "F = 1";
                    break;

                case DayOfWeek.Saturday:
                    dv.RowFilter = "Sa = 1";
                    break;

                case DayOfWeek.Sunday:
                    dv.RowFilter = "S = 1";
                    break;

            }

            gvScheduleBranch.DataSource = dv;
            gvScheduleBranch.DataBind();

        }

        //Display all branch and identify list of Branch that had been schedule.
        private void DisplayDeliveredBranch()
        {
            foreach (GridViewRow row in gvScheduleBranch.Rows)
            {

                string branchCode = row.Cells[0].Text;

                LinkButton lnkNewDelivery = row.FindControl("lnkNewDelivery") as LinkButton;
                LinkButton lnkView = row.FindControl("lnkView") as LinkButton;
          

                if (oTransaction.CHECK_EXIST_DELIVERY_ENTRY(Convert.ToDateTime(txtDeliveryDate.Text), branchCode))
                {
                    lnkNewDelivery.Enabled = false;
                    lnkNewDelivery.CssClass = "disabledLink";
                    lnkNewDelivery.Visible = false;
                    lnkView.Visible = true;
                    //lnkEditDelivery.Visible = true;
                }




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
                Display_Items();

                ViewState["BRANCHCODE"] = row.Cells[0].Text;

                lblDeliveryBranchName.Text = "Delivery to " + "<b>" + row.Cells[1].Text + "</b>";

                txtSearch.Text = "";

            }

           else if (e.CommandName == "View")
            {
                Session["G_BRANCHCODE"] = row.Cells[0].Text;
                Session["G_DELIVERYDATE"] = Convert.ToDateTime(txtDeliveryDate.Text);
                //Display
                PRINT_NOW("rep_BranchDeliveryReceiptSingle.aspx");
                
            }

          //else if (e.CommandName == "Edit")
          //  {
               
          //  }

        }

        protected void lnkSearchDate_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayBranchDeliverySchedule(Convert.ToDateTime(txtDeliveryDate.Text));
            }
            catch
            {

            }


        }

        protected void gvScheduleBranch_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            DisplayDeliveredBranch();

        }



      
    }
}