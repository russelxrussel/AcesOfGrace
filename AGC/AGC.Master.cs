using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace AGC
{
    public partial class AGC : System.Web.UI.MasterPage
    {
        cSystem oSystem = new cSystem();
        protected void Page_Load(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            DataTable dt = oSystem.GET_USER_MENU().Tables[0];
            DataRow[] parentMenus = dt.Select("ParentMenuId = 0 or ParentMenuId is null");

            string unorderelist = generateMenus(parentMenus, dt, sb);


            var sb2 = new StringBuilder();
            sb2.Append("<ul class=\"navbar-nav\">");
            //sb2.Append("<ul  class=\"navbar-nav mr-auto\">");
            sb2.Append(unorderelist);
            sb2.Append("</ul>");
            myDiv.InnerHtml = sb2.ToString();
        }

        private string generateMenus(DataRow[] menu, DataTable dt, StringBuilder sb)
        {
            // sb.Append("<ul  class=\"nav navbar-nav\">");
            if (menu.Length > 0)
            {
                string line = "";

                foreach (DataRow dr in menu)
                {
                    bool flgMenuChild = (bool)dr["flgChild"];

                    string urlPosition = dr["Position"].ToString();
                    string urlText = dr["URL"].ToString();
                    string menuText = dr["MenuText"].ToString();
                    string menuID = dr["MenuID"].ToString();
                    string parentID = dr["ParentMenuID"].ToString();



                    //Condition will be true if menu have parent.
                    if (flgMenuChild)
                    {
                        if (urlPosition == "TOP") //Main Menu
                        {
                            line = string.Format(@"<li class=""nav-item dropdown""><a href=""{0}"" class=""nav-link"" data-toggle=""dropdown""> {1} <span class=""fas fa-caret-down""></span></a>", urlText, menuText, @"</li>");
                        }
                        //else //SubMenu Children
                        //{
                        //    line = string.Format(@"<li class=""nav-item""><a href=""{0}"" class=""dropdown-toggle"" data-toggle=""dropdown""> {1}</a>", urlText, menuText, @"</li>");
                        //}




                    }
                    else
                    {

                        if (urlPosition == "MID") //Main Menu Children
                        {

                            line = string.Format(@"<li class=""nav-item""><a href=""{0}"" class=""nav-link""><span class=""fas fa-globe text-primary""></span> {1}</a>", urlText, menuText, @"</li>");
                        }

                    }

                 
                    sb.Append(line);






                    //Recursive 


                    DataRow[] subMenu = dt.Select(String.Format("ParentMenuId = {0}", menuID));
                    if (subMenu.Length > 0 && !menuID.Equals(parentID))
                    {

                        var subMenuBuilder = new StringBuilder();
                        sb.Append("<ul class=\"dropdown-menu multi-level\">");
                        sb.Append(generateMenus(subMenu, dt, subMenuBuilder));
                        sb.Append("</ul>");
                    }



                } //End of Foreach

            }


            return sb.ToString();

        }


    }
}