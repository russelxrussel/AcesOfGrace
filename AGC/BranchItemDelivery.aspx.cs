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

                txtDeliveryDate.Text = DateTime.Today.ToShortDateString();

                DisplayBranchDeliverySchedule(Convert.ToDateTime(txtDeliveryDate.Text));

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
        }


        #endregion

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDeliveryDate.Text) || !string.IsNullOrEmpty(Session["BRANCHCODE"].ToString()))
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
                             
                       oTransaction.INSERT_BRANCH_DELIVERY(Session["BRANCHCODE"].ToString(), sDRNUM, Convert.ToDateTime(txtDeliveryDate.Text), txtRemarks.Text, itemCode, quantity);
                        }
                    }
                }

                //Hold for possible Print Directly
                Session["G_DRBNUM"] = sDRNUM;

                //UPDATE SERIES NUMBER
                oSystem.UPDATE_SERIES_NUMBER("DRB");

           
                //Clear
                txtDeliveryDate.Text = "";
                txtRemarks.Text = "";
                Display_Items();

                Session["BRANCHCODE"] = "";

                //PRINT_NOW("rep_BranchDeliveryReceiptSingle.aspx");

            }
            else
            {
                //Error message

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertErrorMessage').fadeToggle(2000);</script>", false);
                //lblErrorMessage.Text = "Please fill up required input.";
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
            if (e.CommandName == "Select")
            {

                GridViewRow row = ((e.CommandSource as LinkButton).NamingContainer as GridViewRow);
                //IS_ACTION_CREATE = false;
                //panelInputs.Enabled = true;

                Display_Items();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertMessage').hide();$('#alertMessageME').hide();$('#successMessageME').hide();</script>", false);

                //txtBranchCode.Enabled = false;
                //txtBranchName.Focus();

                //ddStatus.Enabled = true;

                //txtBranchName.Text = row.Cells[2].Text;
                //txtBranchCode.Text = row.Cells[1].Text;


                Session["BRANCHCODE"] = row.Cells[0].Text;

                //Display_Selected_Branch(row.Cells[1].Text);

                //B_ACTION = true;

                //lblContentText.Text = "<span class=\"fas fa-store-alt\"></span> Update Information";

                //panBranchEditName.Visible = true;
                //lblBranchEditName.Text = txtBranchName.Text;

                //panMachineEquipment.Enabled = true;
                //Display_Branch_Machine_Equipment_List(txtBranchCode.Text);

            }
        }

        protected void lnkSearchDate_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayBranchDeliverySchedule(Convert.ToDateTime(txtDeliveryDate.Text));
            }
            catch {

            }

          
        }

        protected void gvScheduleBranch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //DataTable dt = oTransaction.
            //DataView dv = dt.DefaultView;

            //foreach (GridViewRow row in gvBranchList.Rows)
            //{
            //    string branchCode = row.Cells[0].Text;

            //    TextBox txtQuantity = (TextBox)row.Cells[1].FindControl("txtDistrubutedQty");
            //    TextBox txtDateTrans = (TextBox)row.Cells[2].FindControl("txtDate");

            //    //if (checkExistDistribution(lblPTBNum.Text, branchCode))

            //    //{

            //    dv.RowFilter = "BranchCode ='" + branchCode + "' and ptbNum ='" + lblPTBNum.Text + "' and branch_ItemCode ='" + lblBranchItemCode.Text + "'";

            //    if (dv.Count > 0)
            //    {

            //        foreach (DataRowView dvr in dv)
            //        {

            //            //Retrieve Item Inputs

            //            txtQuantity.Text = Convert.ToInt32(dvr.Row["QuantityReceived"]).ToString();
            //            txtDateTrans.Text = Convert.ToDateTime(dvr.Row["DateReceived"]).ToShortDateString();

            //            txtQuantity.Enabled = false;
            //            txtDateTrans.Enabled = false;

            //            // e.Row.BackColor = System.Drawing.Color.Red;

            //        }

            //        //txtQuantity.Text = _RECEIVEDITEM.ToString();
            //        //txtDateTrans.Text = _DATERECEIVED.ToShortDateString();



        }
    }
}