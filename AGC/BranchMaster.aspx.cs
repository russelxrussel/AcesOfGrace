using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;  

namespace AGC
{
    public partial class BranchMaster : System.Web.UI.Page
    {
        cMaster oMaster = new cMaster();
        cUtil oUtil = new cUtil();

        
        private static bool B_ACTION = false;

        private static int machEquip_ID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
           
                Display_Branch_Area();
                Display_Supervisors_List();
                Display_Branch_Incharge_List();
                Display_Partners_List();
                Display_Branch_List();
                Display_Mode_Payment_List();
                Display_Status_List();
                Display_Days_List();

               


                panelBranchInput.Visible = false;
            }
        }

        
      

        protected void lnkNew_Click(object sender, EventArgs e)
        {
            panelBranchInput.Visible = true;
            panelBranchList.Visible = false;
            Clear_Inputs();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertMessage').hide();$('#alertMessageME').hide();$('#successMessageME').hide();</script>", false);


            lblContentText.Text = "Create New Branch Information";

            B_ACTION = false;
            panMachineEquipment.Enabled = false;


            //This will clear list
            //Display_Branch_Machine_Equipment_List(txtBranchCode.Text);
            gvMachineEquipment.DataSource = null;
            gvMachineEquipment.DataBind();

        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            panBranchEditName.Visible = false;
            lblBranchEditName.Visible = false;
            Response.Redirect(Request.RawUrl);
            
        }


        private void Display_Branch_Area()
        {
            DataTable dt = oUtil.GET_BRANCH_AREA();



            ddBranchArea.DataSource = dt;
            ddBranchArea.DataTextField = dt.Columns["AreaName"].ToString();
            ddBranchArea.DataValueField = dt.Columns["AreaID"].ToString();
            ddBranchArea.DataBind();

            ddBranchArea.Items.Insert(0, new ListItem("--SELECT AREA--"));
        }

        private void Display_Supervisors_List()
        {
            DataTable dt = oUtil.GET_SUPERVISOR_LIST();

            ddSupervisor.DataSource = dt;
            ddSupervisor.DataTextField = dt.Columns["SupervisorName"].ToString();
            ddSupervisor.DataValueField = dt.Columns["SupervisorID"].ToString();
            ddSupervisor.DataBind();

            ddSupervisor.Items.Insert(0, new ListItem("--SELECT SUPERVISOR--"));

        }

        private void Display_Partners_List()
        {
            DataTable dt = oMaster.GET_PARTNERS_LIST();

            ddPartner.DataSource = dt;

            ddPartner.DataTextField = dt.Columns["PartnerName"].ToString();
            ddPartner.DataValueField = dt.Columns["PartnerCode"].ToString();
            ddPartner.DataBind();

            ddPartner.Items.Insert(0, new ListItem("--SELECT PARTNER--"));
        }

        private void Display_Branch_Incharge_List()
        {
            DataTable dt = oUtil.GET_BRANCH_INCHARGE_LIST();

            ddPersonIncharge.DataSource = dt;
            ddPersonIncharge.DataTextField = dt.Columns["branchInchargeName"].ToString();
            ddPersonIncharge.DataValueField = dt.Columns["branchInchargeID"].ToString();
            ddPersonIncharge.DataBind();
            
        }

        private void Display_Mode_Payment_List()
        {
            DataTable dt = oUtil.GET_MODE_PAYMENT_LIST();

            ddModePayment.DataSource = dt;
            ddModePayment.DataTextField = dt.Columns["paymentModeName"].ToString();
            ddModePayment.DataValueField = dt.Columns["paymentModeCode"].ToString();
            ddModePayment.DataBind();

            ddModePayment.Items.Insert(0, new ListItem(""));
        }

        private void Display_Status_List()
        {
            DataTable dt = oUtil.GET_STATUS_LIST();


            ddStatus.DataSource = dt;
            ddStatus.DataTextField = dt.Columns["statusName"].ToString();
            ddStatus.DataValueField = dt.Columns["statusCode"].ToString();
            ddStatus.DataBind();
        }

        private void Display_Days_List()
        {
            DataTable dt = oUtil.GET_DAYS_LIST();

            ddPaymentDay.DataSource = dt;
            ddPaymentDay.DataTextField = dt.Columns["day"].ToString();
            ddPaymentDay.DataValueField = dt.Columns["day"].ToString();
            ddPaymentDay.DataBind();

            ddPaymentDay.Items.Insert(0, new ListItem("0"));
        }




        private void Display_Branch_List()
        {
            DataTable dt = oMaster.GET_BRANCH_LIST();

            gvBranchList.DataSource = dt;
            gvBranchList.DataBind();
        }

        private void Clear_Inputs()
        {
            txtBranchCode.Text = "";
            txtBranchName.Text = "";
            txtAddress.Text = "";
            txtContactNumber.Text = "";
            txtOpeningDate.Text = "";
            txtRemarks.Text = "";
            txtLessorName.Text = "";
            txtBranchCode.Enabled = true;
            txtBranchCode.Focus();

            ddStatus.Enabled = false;

            ddBranchArea.SelectedIndex = 0;
            ddPersonIncharge.SelectedIndex = 0;
            ddPartner.SelectedIndex = 0;
            ddSupervisor.SelectedIndex = 0;
            ddModePayment.SelectedIndex = 0;
        }

        protected void lnkSaveBranch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBranchCode.Text) || string.IsNullOrEmpty(txtBranchName.Text) || string.IsNullOrEmpty(txtOpeningDate.Text) || 
                ddBranchArea.SelectedIndex == 0 || ddSupervisor.SelectedIndex == 0 || ddPartner.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertMessage').fadeToggle(2000);</script>", false);
            }
            else
            {
                DateTime dtOpening = Convert.ToDateTime(txtOpeningDate.Text);
                int areaID = Convert.ToInt32(ddBranchArea.SelectedValue);
                int inchargeID = 1;
                
                int supervisorID = Convert.ToInt32(ddSupervisor.SelectedValue);

                //if (ddPersonIncharge.SelectedIndex != 0)
                //{
                    inchargeID =  Convert.ToInt32(ddPersonIncharge.SelectedValue);
                //}

                //Check action
                if (B_ACTION == false)
                {
                    oMaster.INSERT_BRANCH(txtBranchCode.Text, txtBranchName.Text, txtAddress.Text, txtContactNumber.Text, inchargeID, supervisorID, ddPartner.SelectedValue, dtOpening, areaID,
                                          txtLessorName.Text, ddModePayment.SelectedValue.ToString(), Convert.ToInt32(ddPaymentDay.Text), txtRemarks.Text);

                    lblSuccessMessage.Text = txtBranchName.Text.ToUpper() + " successfully created."; 
                }
                else {
                    //Status
                    bool bStatus;

                    if (ddStatus.SelectedIndex == 0)
                    {bStatus = true;}
                    else { bStatus = false; }

                    oMaster.UPDATE_BRANCH(txtBranchCode.Text, txtBranchName.Text, txtAddress.Text, txtContactNumber.Text, inchargeID, supervisorID, ddPartner.SelectedValue, dtOpening, areaID,
                                          txtLessorName.Text, ddModePayment.SelectedValue.ToString(), Convert.ToInt32(ddPaymentDay.Text),txtRemarks.Text, bStatus);

                    lblSuccessMessage.Text = txtBranchName.Text.ToUpper() + " successfully updated.";
                }

                Clear_Inputs();

                Display_Branch_List();

                //Response.Redirect(Request.RawUrl);
                panelBranchInput.Visible = false;
                panelBranchList.Visible = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertMessage').hide();$('#modalSuccess').modal('show');</script>", false);

            }
        }

     

        protected void lnkCancelNewBranch_Click(object sender, EventArgs e)
        {
            Clear_Inputs();
            panelBranchInput.Visible = false;
            panelBranchList.Visible = true;

            panBranchEditName.Visible = false;
       
        }

        protected void gvBranchList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                GridViewRow row = ((e.CommandSource as LinkButton).NamingContainer as GridViewRow);
                //IS_ACTION_CREATE = false;
                //panelInputs.Enabled = true;

                txtSearchBranch.Text = "";

                panelBranchList.Visible = false;
                panelBranchInput.Visible = true;
               
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertMessage').hide();$('#alertMessageME').hide();$('#successMessageME').hide();</script>", false);
              
                txtBranchCode.Enabled = false;
                txtBranchName.Focus();

                ddStatus.Enabled = true;

                txtBranchName.Text = row.Cells[2].Text;
                txtBranchCode.Text = row.Cells[1].Text;




                Display_Selected_Branch(row.Cells[1].Text);

                B_ACTION = true;

                lblContentText.Text = "<span class=\"fas fa-store-alt\"></span> Update Information" ;

                panBranchEditName.Visible = true;
                lblBranchEditName.Text = txtBranchName.Text;

                panMachineEquipment.Enabled = true;
                Display_Branch_Machine_Equipment_List(txtBranchCode.Text);

            }
        }

        private void Display_Selected_Branch(string _branchCode)
        {
            DataTable dt = new DataTable();
            dt = oMaster.GET_BRANCH_LIST();

            DataView dv = dt.DefaultView;
            dv.RowFilter = "BranchCode ='" + _branchCode + "'";

            if (dv.Count > 0)
            {
                DataRowView dvr = dv[0];
                
                txtAddress.Text = dvr.Row["branchAddress"].ToString();
                txtContactNumber.Text = dvr.Row["branchContactNumbers"].ToString();
                txtOpeningDate.Text = Convert.ToDateTime(dvr.Row["OpeningDate"]).ToShortDateString();
                txtLessorName.Text = dvr.Row["lessorName"].ToString();
                txtRemarks.Text = dvr.Row["Remarks"].ToString();
                

                if ((bool)dvr.Row["isActive"])
                {
                    ddStatus.SelectedIndex = 0;
                }
                else { ddStatus.SelectedIndex = 1; }


                if (!string.IsNullOrEmpty(ddBranchArea.SelectedValue = dvr.Row["areaID"].ToString()))
                {
                    ddBranchArea.SelectedValue = dvr.Row["areaID"].ToString();
                }
               

                if (!string.IsNullOrEmpty(dvr.Row["partnerCode"].ToString()))
                {

                    ddPartner.SelectedValue = dvr.Row["partnerCode"].ToString();
                }

                if (!string.IsNullOrEmpty(dvr.Row["supervisorID"].ToString()))
                {
                    ddSupervisor.SelectedValue = dvr.Row["supervisorID"].ToString();
                }

                if (!string.IsNullOrEmpty(dvr.Row["branchInchargeID"].ToString()))
                { 
                    ddPersonIncharge.SelectedValue = dvr.Row["branchInchargeID"].ToString();
                }

                if (!string.IsNullOrEmpty(dvr.Row["modePaymentCode"].ToString()))
                {
                    ddModePayment.SelectedValue = dvr.Row["modePaymentCode"].ToString();
                }


                if ((int)dvr.Row["paymentDay"] != 0)
                {
                    ddPaymentDay.SelectedValue = dvr.Row["paymentDay"].ToString();
                }
                else
                {
                    ddPaymentDay.SelectedIndex = 0;
                }
               

            }

            
        }

        private void Display_Branch_Machine_Equipment_List(string _branchCode)
        {
            DataTable dt = oMaster.GET_BRANCH_MACHINE_EQUIPMENT_LIST();

            DataView dv = dt.DefaultView;
            dv.RowFilter = "BranchCode ='" + _branchCode + "'";

            gvMachineEquipment.DataSource = dv;
            gvMachineEquipment.DataBind();

        }
       

        //Clear Machine Equipment
        private void clearMachineEquipmentInput()
        {
            // ddMachineEquipment.SelectedIndex = 0;
            //txtMEDescription.Text = "";
            //txtSerial.Text = "";
            //txtDateIssue.Text = "";
        }

        protected void gvMachineEquipment_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                gvMachineEquipment.EditIndex = rowIndex;

                Display_Branch_Machine_Equipment_List(txtBranchCode.Text);


                //Machine Equipment
                string machEquipCode = ((Label)gvMachineEquipment.Rows[rowIndex].FindControl("lblMachEquipCode")).Text;
                DropDownList ddMachineEquipName = ((DropDownList)gvMachineEquipment.Rows[rowIndex].FindControl("ddMachineEquipName"));

                DataTable dtME = oUtil.GET_MACHINE_EQUIPMENT_LIST();

                ddMachineEquipName.DataSource = dtME;

                ddMachineEquipName.DataTextField = dtME.Columns["MachEquipName"].ToString();
                ddMachineEquipName.DataValueField = dtME.Columns["MachEquipCode"].ToString();
                ddMachineEquipName.DataBind();

                ddMachineEquipName.SelectedValue = machEquipCode;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertMessageME').hide();$('#successMessageME').hide();</script>", false);

            }




            else if (e.CommandName == "Create")
            {

                DropDownList ddMachineEquipment = ((DropDownList)gvMachineEquipment.HeaderRow.FindControl("ddMachineEquipment"));
                TextBox txtAdditionalDescription = ((TextBox)gvMachineEquipment.HeaderRow.FindControl("txtMEDescription"));
                TextBox txtSerial = ((TextBox)gvMachineEquipment.HeaderRow.FindControl("txtSerial"));
                TextBox txtDateIssue = ((TextBox)gvMachineEquipment.HeaderRow.FindControl("txtDateIssue"));



                //Validate
                if (!string.IsNullOrEmpty(txtDateIssue.Text))
                {
                    DateTime dtDateIssue = Convert.ToDateTime(txtDateIssue.Text);
                    oMaster.INSERT_BRANCH_MACHINE_EQUIPMENT(txtBranchCode.Text, ddMachineEquipment.SelectedValue,
                                                            txtAdditionalDescription.Text, txtSerial.Text, dtDateIssue);

                    //Clear input
                    ddMachineEquipment.SelectedIndex = 0;
                    txtAdditionalDescription.Text = "";
                    txtSerial.Text = "";
                    txtDateIssue.Text = "";

                    Display_Branch_Machine_Equipment_List(txtBranchCode.Text);

                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "<script>$('#successMessageME').fadeToggle(2000);$('#alertMessageME').hide();</script>", false);
                    lblSuccessMessageText.Text = "New Machine and Equipment successfully added.";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "<script>$('#alertMessageME').fadeToggle(2000);$('#successMessageME').hide();</script>", false);
                    lblAlertMessageText.Text = "Please fill up required input";
                }

            }

            else if (e.CommandName == "CancelEdit")
            {
                gvMachineEquipment.EditIndex = -1;
                Display_Branch_Machine_Equipment_List(txtBranchCode.Text);
              
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertMessageME').hide();$('#successMessageME').hide();</script>", false);
            }

            else if (e.CommandName == "DeleteRow")
            {
                machEquip_ID = Convert.ToInt32(e.CommandArgument);
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#alertMessageME').hide();$('#successMessageME').hide();$('#modalConfirmDelete').modal('show');</script>", false);
                
            }


            //Update
            else if (e.CommandName == "UpdateRow")
            {
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

                int id = Convert.ToInt32(e.CommandArgument);

                string machEquipCode = ((DropDownList)gvMachineEquipment.Rows[rowIndex].FindControl("ddMachineEquipName")).SelectedValue;
                string addtDescription = ((TextBox)gvMachineEquipment.Rows[rowIndex].FindControl("txtDescription")).Text;
                string serial = ((TextBox)gvMachineEquipment.Rows[rowIndex].FindControl("txtSerial")).Text;
                string dateIssue = ((TextBox)gvMachineEquipment.Rows[rowIndex].FindControl("txtDateIssue")).Text;



                if (!string.IsNullOrEmpty(dateIssue))
                {

                    DateTime dtDateIssue = Convert.ToDateTime(dateIssue);

                    oMaster.UPDATE_BRANCH_MACHINE_EQUIPMENT(id, machEquipCode, addtDescription, serial, dtDateIssue);


                    gvMachineEquipment.EditIndex = -1;
                    Display_Branch_Machine_Equipment_List(txtBranchCode.Text);

                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "<script>$('#alertMessageME').hide();$('#successMessageME').fadeToggle(2000);</script>", false);
                    lblSuccessMessageText.Text = "Machine and Equipment successfully updated.";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "<script>$('#alertMessageME').fadeToggle(2000);$('#successMessageME').hide();</script>", false);
                    lblAlertMessageText.Text = "Please fill up required input.";
                }




            }

        }

        protected void lnkCancelMachineEquipment_Click(object sender, EventArgs e)
        {
        
            Response.Redirect(Request.RawUrl);
           
        }

        protected void gvMachineEquipment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DropDownList ddMachineEquipName = (e.Row.FindControl("ddMachineEquipment") as DropDownList); 

                DataTable dtME = oUtil.GET_MACHINE_EQUIPMENT_LIST();

                ddMachineEquipName.DataSource = dtME;

                ddMachineEquipName.DataTextField = dtME.Columns["MachEquipName"].ToString();
                ddMachineEquipName.DataValueField = dtME.Columns["MachEquipCode"].ToString();
                ddMachineEquipName.DataBind();
            }
        }

        protected void lnkConfirmDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDeletedRemarks.Text))
            {
                oMaster.DELETE_BRANCH_MACHINE_EQUIPMENT(machEquip_ID, txtDeletedRemarks.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "<script>$('#alertMessageME').hide();$('#successMessageME').hide();$('#modalConfirmDelete').modal('hide');$('body').removeClass('modal-open');$('.modal-backdrop').remove();</script>", false);

                gvMachineEquipment.EditIndex = -1;
                Display_Branch_Machine_Equipment_List(txtBranchCode.Text);
                txtDeletedRemarks.Text = "";
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "<script>$('#alertMessageME').hide();$('#successMessageME').hide();$('#modalConfirmDelete').modal('show');</script>", false);

            }

        }

        protected void lnkCloseDeleteMachineEquipment_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "<script>$('#alertMessageME').hide();$('#successMessageME').hide();$('#modalConfirmDelete').modal('hide');$('body').removeClass('modal-open');$('.modal-backdrop').remove();</script>", false);
        }
        
        
    }
}