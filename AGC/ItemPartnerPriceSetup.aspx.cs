using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AGC
{
    public partial class ItemPartnerPriceSetup : System.Web.UI.Page
    {
        cMaster oMaster = new cMaster();
        cTransaction oTransaction = new cTransaction();
        cSystem oSystem = new cSystem();
        cUtil oUtility = new cUtil();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //ViewState["BRANCHCODE"] = "";
                Display_Partners_List();
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


      



        //#endregion

       

        protected void lnkSave_Click(object sender, EventArgs e)
        {
          
            foreach (GridViewRow row in gvItems.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string itemCode = row.Cells[0].Text;
                    string partnerCode = ddPartnerList.SelectedValue.ToString();

                    TextBox txtPartnerPrice = (TextBox)row.Cells[2].FindControl("txtPartnerPrice");
                    TextBox txtSellingPrice = (TextBox)row.Cells[2].FindControl("txtSellingPrice");

                    double dPartnerPrice, dSellingPrice;
                    if (string.IsNullOrEmpty(txtPartnerPrice.Text))
                    { dPartnerPrice = 0; }
                    else
                    {
                        dPartnerPrice = Convert.ToInt32(txtPartnerPrice.Text);
                    }

                    if (string.IsNullOrEmpty(txtSellingPrice.Text))
                    { dSellingPrice = 0; }
                    else
                    {
                        dSellingPrice = Convert.ToInt32(txtSellingPrice.Text);
                    }



                    if (dPartnerPrice != 0)
                    {
                        oUtility.UPDATE_PARTNER_PRICE(partnerCode, itemCode, dPartnerPrice, dSellingPrice);
                    }

                }
            }


            ddPartnerList.SelectedIndex = 0;

            Display_Partner_Price(ddPartnerList.SelectedValue);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "<script>$('#modalSuccess').modal('show');</script>", false);
            lblSuccessMessage.Text = "Partner price successfully updated.";

     
        }


        private void Display_Partners_List()
        {
            DataTable dt = oMaster.GET_PARTNERS_LIST();

            ddPartnerList.DataSource = dt;

            ddPartnerList.DataTextField = dt.Columns["PartnerName"].ToString();
            ddPartnerList.DataValueField = dt.Columns["PartnerCode"].ToString();
            ddPartnerList.DataBind();

            ddPartnerList.Items.Insert(0, new ListItem("--SELECT PARTNER--"));
        }

        private void Display_Partner_Price(string _partnerCode)
        {
            DataTable dt = oUtility.GET_PARTNER_PRICE(_partnerCode);

            if (ddPartnerList.SelectedIndex > 0)
            {
                gvItems.DataSource = dt;
                lblPartnerName.Text = ddPartnerList.SelectedItem.ToString();
            }
            else
            {
                gvItems.DataSource = null;
                lblPartnerName.Text = "";
            }
            
            gvItems.DataBind();
            
            
            
        }

        protected void ddPartnerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Display_Partner_Price(ddPartnerList.SelectedValue);
        }
    }
}