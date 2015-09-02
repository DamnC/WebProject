using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Upload : System.Web.UI.Page
{
    //redirects home if not logged in
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
            Response.Redirect("Home.aspx");
    }
    //fires when clicking button1, uploads file
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
            Label1.Text = AudioFiles.UploadFile(FileUpload1, TextBoxAudioName.Text.Replace("'",""), TextBoxDescription.Text, Session["ID"].ToString()) + "<br> Upload Successeful!";
        else
            Label1.Text = "You have not specified a file.";
    }

}