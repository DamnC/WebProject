using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public string welcome="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null) { welcome = "Welcome, Guest."; } else { welcome = "Welcome, " + Session["Username"].ToString() + "."; }
    }
}
