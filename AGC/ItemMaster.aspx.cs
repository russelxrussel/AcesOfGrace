using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;  

namespace AGC
{
    public partial class ItemMaster : System.Web.UI.Page
    {
        cMaster oMaster = new cMaster();
        cUtil oUtil = new cUtil();
        cSystem oSystem = new cSystem();
        cAccounting oAccounting = new cAccounting();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Display_Item_List();
                Display_Item_UOM_List();
                Display_Item_Category_List();

                Display_Payable_Account_List();

                Display_Status_List();
                


                panelItemInput.Visible = false;
            }
        }

        
      

        protected void lnkNew_Click(object sender, EventArgs e)
        {
            panelItemInput.Visible = true;
            panelItemList.Visible = false;
            Clear_Inputs();

            lblContentText.Text = "Create New Product Item";


        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            panItemEditName.Visible = false;
            lblItemEditName.Visible = false;
            Response.Redirect(Request.RawUrl);
        }


        private void Display_Item_UOM_List()
        {
            DataTable dt = oUtil.GET_ITEM_UOM_LIST();

            ddUOM.DataSource = dt;

            ddUOM.DataTextField = dt.Columns["uomDescription"].ToString();
            ddUOM.DataValueField = dt.Columns["uomCode"].ToString();
            ddUOM.DataBind();
            
        }

        private void Display_Item_Category_List()
        {
            DataTable dt = oUtil.GET_ITEM_CATEGORY_LIST();

            ddItemCategory.DataSource = dt;
            ddItemCategory.DataTextField = dt.Columns["itemCategoryName"].ToString();
            ddItemCategory.DataValueField = dt.Columns["itemCategoryCode"].ToString();
            ddItemCategory.DataBind();
        }

        private void Display_Payable_Account_List()
        {
            DataTable dt = oAccounting.GET_PAYABLE_ACCOUNT_LIST();

            ddPayableAccount.DataSource = dt;
            ddPayableAccount.DataTextField = dt.Columns["payAccName"].ToString();
            ddPayableAccount.DataValueField = dt.Columns["payAccCode"].ToString();
            ddPayableAccount.DataBind();
        }

        private void Display_Status_List()
        {
            DataTable dt = oUtil.GET_STATUS_LIST();


            ddStatus.DataSource = dt;
            ddStatus.DataTextField = dt.Columns["statusName"].ToString();
            ddStatus.DataValueField = dt.Columns["statusCode"].ToString();
            ddStatus.DataBind();
        }

      




        private void Display_Item_List()
        {
            DataTable dt = oMaster.GET_ITEMS_LIST();

            gvItemList.DataSource = dt;
            gvItemList.DataBind();
        }

        private void Clear_Inputs()
        {
            txtItemCode.Text = "";
            txtItemName.Text = "";
          
            txtItemCode.Enabled = true;
            txtItemCode.Focus();

            ddStatus.Enabled = false;

            ddStatus.SelectedIndex = 0;
            ddItemCategory.SelectedIndex = 0;
            ddUOM.SelectedIndex = 0;
            ddPayableAccount.SelectedIndex = 0;

        }

        protected void lnkSaveBranch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCode.Text) && !string.IsNullOrEmpty(txtItemName.Text))
            {
                //SAVE AND UPDATE
                oMaster.INSERT_UPDATE_ITEM(txtItemCode.Text, txtItemName.Text, ddItemCategory.SelectedValue, ddUOM.SelectedValue, ddStatus.SelectedValue, ddPayableAccount.SelectedValue, oSystem.GET_SERVER_DATE_TIME());

                lblSuccessMessage.Text = "Item successfully updated.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
            }

            else
            {

                lblErrorMessage.Text = "Required field must fill up.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalError').modal('show');</script>", false);

            }
          
        }

     

        protected void lnkCancelNewBranch_Click(object sender, EventArgs e)
        {
            Clear_Inputs();
            panelItemInput.Visible = false;
            panelItemList.Visible = true;

            panItemEditName.Visible = false;
       
        }

        protected void gvBranchList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                GridViewRow row = ((e.CommandSource as LinkButton).NamingContainer as GridViewRow);
              
                txtSearchItem.Text = "";

                panelItemList.Visible = false;
                panelItemInput.Visible = true;
               
                 
                txtItemCode.Enabled = false;
                txtItemName.Focus();

                ddStatus.Enabled = true;

                txtItemName.Text = row.Cells[2].Text;
                txtItemCode.Text = row.Cells[1].Text;
                
                Display_Selected_Item(row.Cells[1].Text);

             
                lblContentText.Text = "<span class=\"fas fa-store-alt\"></span> Update Information" ;

                panItemEditName.Visible = true;
                lblItemEditName.Text = txtItemName.Text;

              
         

            }
        }

        private void Display_Selected_Item(string _itemCode)
        {
            DataTable dt = new DataTable();
            dt = oMaster.GET_ITEMS_LIST();

            DataView dv = dt.DefaultView;
            dv.RowFilter = "ItemCode ='" + _itemCode + "'";

            if (dv.Count > 0)
            {
                DataRowView dvr = dv[0];
                
              
                //txtUnitCost.Text = dvr.Row["unitCost"].ToString();
                //txtUnitPrice.Text = dvr.Row["unitPrice"].ToString();

                ddStatus.SelectedValue = dvr.Row["statusCode"].ToString();


                if (!string.IsNullOrEmpty(ddUOM.SelectedValue = dvr.Row["itemUOMInventory"].ToString()) || !string.IsNullOrWhiteSpace(ddUOM.SelectedValue = dvr.Row["itemUOMInventory"].ToString()))
                {
                    ddUOM.SelectedValue = dvr.Row["itemUOMInventory"].ToString();
                }

                if (!string.IsNullOrEmpty(ddItemCategory.SelectedValue = dvr.Row["itemCategoryCode"].ToString()))
                {
                    ddItemCategory.SelectedValue = dvr.Row["itemCategoryCode"].ToString();
                }

                if (!string.IsNullOrEmpty(ddPayableAccount.SelectedValue = dvr.Row["payAcccode"].ToString()))
                {
                    ddPayableAccount.SelectedValue = dvr.Row["payAcccode"].ToString();
                }
                

            }

            
        }
        
        
    }
}