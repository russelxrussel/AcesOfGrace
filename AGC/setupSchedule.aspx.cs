using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AGC
{
    public partial class setupSchedule : System.Web.UI.Page
    {
        cUtil oUtility = new cUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayBranchSchedule();
            }
        }

     

        private void DisplayBranchSchedule()
        {
            DataTable dt = oUtility.GET_BRANCH_SCHEDULE_LIST();

            gvScheduleSetup.DataSource = dt;
            gvScheduleSetup.DataBind();

        }




        //private bool ItemExist(string _code)
        //{
        //    bool x = false;

        //    DataTable dt = oUtility.GET_LOCATION_AREA_LIST();
        //    DataView dv = dt.DefaultView;

        //    dv.RowFilter = "LACode = '" + _code + "'";

        //    if (dv.Count > 0)
        //    {
        //        x = true;
        //    }
        //    else
        //    {
        //        x = false;
        //    }

        //    return x;
        //}

        protected void btnEditItem_Click(object sender, EventArgs e)
        {
            //THIS WILL GET THE CELL VALUE OF SELECTED ROW IN GRIDVIEW VIA COMMAND WITHIN TEMPLATE FIELDS.
            var selEdit = (Control)sender;
            GridViewRow r = (GridViewRow)selEdit.NamingContainer;
            int schedID = Convert.ToInt32(r.Cells[0].Text);
            string selBranch = r.Cells[1].Text;

            CheckBox chkM = (CheckBox)r.Cells[2].FindControl("chkM");
            CheckBox chkT = (CheckBox)r.Cells[3].FindControl("chkT");
            CheckBox chkW = (CheckBox)r.Cells[4].FindControl("chkW");
            CheckBox chkTh = (CheckBox)r.Cells[5].FindControl("chkTh");
            CheckBox chkF = (CheckBox)r.Cells[6].FindControl("chkF");
            CheckBox chkSa = (CheckBox)r.Cells[7].FindControl("chkSa");
            CheckBox chkS = (CheckBox)r.Cells[8].FindControl("chkS");




            // oUtility.UPDATE_BRANCH_SCHEDULE(branchID, chkM.Checked, chkT.Checked, chkW.Checked, chkTh.Checked, chkF.Checked, chkSa.Checked, chkS.Checked, "TEST USER");

            oUtility.UPDATE_DELIVERY_BRANCH_SCHEDULE(schedID, chkM.Checked, chkT.Checked, chkW.Checked, chkTh.Checked, chkF.Checked, chkSa.Checked, chkS.Checked, "TEST USER");
            lblSuccessMessage.Text = selBranch + " successfully updated.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);

            DisplayBranchSchedule();
            txtSearch.Text = "";
        }

        protected void U_Search_Click(object sender, EventArgs e)
        {

            //if (string.IsNullOrEmpty(txtSearch.Text))
            //{
            //    DisplayBranchSchedule();
            //}
            //else
            //{

            //    DisplayBranches_PARAM(txtSearch.Text);
            //}
        }

        private void DisplayBranches_PARAM(string _searchInput)
        {
            //DataTable dt = oUtility.GET_BRANCH_SCHEDULE_LIST();

            //DataView dv = dt.DefaultView;

            //dv.RowFilter = "BranchName like '%" + _searchInput + "%'";

            //if (dv.Count > 0)
            //{

            //    gvScheduleSetup.DataSource = dv;
            //    gvScheduleSetup.DataBind();
            //}
            //else
            //{
            //    gvScheduleSetup.DataSource = dt;
            //    gvScheduleSetup.DataBind();
            //}

        }
    }
}