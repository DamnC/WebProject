using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Admin"] == null || !(bool)Session["Admin"])
            Response.Redirect("Home.aspx");
        if (!IsPostBack)
        {
            TextBoxMessage.Text = Dal.ExecuteScalar("SELECT MESSAGE FROM SITE_STRINGS").ToString();
        }
    }
    protected void ButtonMessage_Click(object sender, EventArgs e)
    {
        Dal.ExecuteNonQuery(string.Format("UPDATE SITE_STRINGS SET MESSAGE='{0}'", @TextBoxMessage.Text.ToString()));
    }
}