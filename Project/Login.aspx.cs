using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    //fires when page loads
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelError.Text = Request.QueryString["m"];
    }
    //fires when submit button is clicked
    protected void Submit_Click(object sender, EventArgs e)
    {
        if (Users.LoginSuccessful(TextBoxUsername.Text, TextBoxPassword.Text))
        {
            Session["Username"] = TextBoxUsername.Text;
            Session["ID"] = Users.GetIdByUsername(TextBoxUsername.Text);
            Session["Admin"] = Users.IsAdmin(int.Parse(Session["ID"].ToString()));
            Application["connected"] = int.Parse(Application["connected"].ToString()) + 1;
            if (Session["CurrentlyListening"] == null)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                Response.Redirect("Audio.aspx?id=" + Session["CurrentlyListening"]);
            }
        }
        else
            LabelError.Text = "Incorrect Login";
    }
}