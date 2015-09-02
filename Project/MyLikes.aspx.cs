using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

public partial class MyLikes : System.Web.UI.Page
{
    public int currentpage;
    PagedDataSource pg;
    DataSet ds;
    //fires when page loads
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID"] != null)
        {
            if (!IsPostBack)
            {
                this.currentpage = 0;
                Session["CurrentPageLikes"] = 0;
                PopulateDataList();             //Binding pg to datalist
            }
            else
            {
                this.currentpage = (int)(Session["CurrentPageLikes"]);
                string idPlay = Request["__EVENTARGUMENT"];
                string target = Request["__EVENTTARGET"];
                if (target == "audio")
                {
                    Response.Redirect("Audio.aspx?id=" + idPlay);
                }
            }
            int pageInd = this.currentpage + 1; //Page number
            LabelCurrentPage.Text = pageInd.ToString();
        }
        else
            Response.Redirect("Home.aspx");
    }
    //refreshes datalist
    protected void PopulateDataList()
    {
        pg = new PagedDataSource();
        ds = Likes.GetLikesForUser(int.Parse(Session["id"].ToString()));
        if (ds.Tables[0].Rows.Count > 0)
        {
            pg.AllowPaging = true;
            pg.DataSource = ds.Tables[0].DefaultView;

            pg.PageSize = 5;
            pg.CurrentPageIndex = this.currentpage;
            this.ImageButtonNextPage.Enabled = !pg.IsLastPage;
            this.ImageButtonPrevPage.Enabled = !pg.IsFirstPage;
            DataList1.DataSource = pg;
            DataList1.DataBind();
        }
        else
            LabelLikes.Text = "You didn't like any audios :(";
    }
    //returns image for like button
    protected string DidUserLikePic()
    {
        if (Session["ID"] != null)
        {
            if (Likes.DidUserLikeAudio((int)Session["ID"], (int)Eval("ID")))
            {
                return "~/Images/SongIcons/LikeButton2.PNG";
            }
        }
        return "~/Images/SongIcons/LikeButton.PNG";
    }
    //likes/dislikes
    public void ImageButtonLike_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["ID"] != null)
        {
            if (Likes.DidUserLikeAudio((int)Session["ID"], int.Parse(((System.Web.UI.WebControls.Image)(sender)).AlternateText))) //AlternateText contains audio id
            {
                Likes.Unlike((int)Session["ID"], int.Parse(((System.Web.UI.WebControls.Image)(sender)).AlternateText));
                ((System.Web.UI.WebControls.Image)(sender)).ImageUrl = "~/Images/SongIcons/LikeButton.PNG";
                PopulateDataList();
            }
            else
            {
                Likes.Like((int)Session["ID"], int.Parse(((System.Web.UI.WebControls.Image)(sender)).AlternateText));
                ((System.Web.UI.WebControls.Image)(sender)).ImageUrl = "~/Images/SongIcons/LikeButton2.PNG";
                PopulateDataList();
            }
        }
        else
        {
            Response.Redirect("Login.aspx?m=You need an account to like audios");
        }
    }
    //downloads file
    public void ImageButtonDownload_Click(object sender, ImageClickEventArgs e)
    {
        string webserviceurl = "";
        Response.Redirect(webserviceurl + "?id=" + ((System.Web.UI.WebControls.Image)(sender)).AlternateText); //redirects to webservice to download file
    }
    //goes to prev page
    public void ImageButtonPrevPage_Click(object sender, ImageClickEventArgs e)
    {
        Session["CurrentPageLikes"] = (int)Session["CurrentPageLikes"] - 1;
        this.currentpage = (int)Session["CurrentPageLikes"];
        PopulateDataList();
        int pageInd = this.currentpage + 1;
        this.LabelCurrentPage.Text = pageInd.ToString();
    }
    //goes to next page
    public void ImageButtonNextPage_Click(object sender, ImageClickEventArgs e)
    {
        Session["CurrentPageLikes"] = (int)Session["CurrentPageLikes"] + 1;
        this.currentpage = (int)Session["CurrentPageLikes"];
        PopulateDataList();
        int pageInd = this.currentpage + 1;
        this.LabelCurrentPage.Text = pageInd.ToString();
    }
}