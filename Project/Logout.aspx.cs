using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    //Fires on page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID"] != null)
        {
            Session["Username"] = null;
            Session["ID"] = null;
            Session["Admin"] = null;
            Application["connected"] = int.Parse(Application["connected"].ToString()) - 1;
        }
        Response.Redirect("Home.aspx");
    }
}