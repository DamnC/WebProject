using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Signup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    //fires when clicking submit button
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string error = Users.AddUser(TextBoxNickName.Text, TextBoxMail.Text, TextBoxPassword.Text, TextBoxUsername.Text, TextBoxRealName.Text);
            if (error != "1")
                LabelExistError.Text = error;
            else
            {
                Response.Redirect("Login.aspx?m=Signup Successful!");
            }
        }
    }
}