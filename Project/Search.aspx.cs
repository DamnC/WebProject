using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Search : System.Web.UI.Page
{
    public int currentpage;
    PagedDataSource pg;
    DataSet ds;
    string searchstring;
    //fires when page loads
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.GetPostBackEventReference(this, string.Empty);
        if (Request.QueryString["q"] != null) // check cuz shuoldn't be null
        {
            searchstring = Request.QueryString["q"];
            LabelUp.Text = "Search results for: " + searchstring;
            if (!IsPostBack)
            {
                this.currentpage = 0;
                Session["CurrentPageSearch"] = 0;
                PopulateDataList();             //Binding pg to datalist
            }
            else
            {
                this.currentpage = (int)(Session["CurrentPageSearch"]);
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
        {
            LabelUp.Text = "No search string specified";
        }
    }
    //refreshes data list
    protected void PopulateDataList()
    {
        pg = new PagedDataSource();
        ds = AudioFiles.Search(searchstring.Replace("'",""));
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
    }
    //returns pic for like button
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
        int id = int.Parse(((System.Web.UI.WebControls.Image)(sender)).AlternateText);
        Byte[] file = AudioFiles.Download(id);
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        context.Response.Clear();
        context.Response.ClearHeaders();
        context.Response.ClearContent();
        context.Response.AppendHeader("content-length", file.Length.ToString());
        context.Response.ContentType = "audio/mpeg";
        context.Response.AppendHeader("content-disposition", "attachment; filename=" + AudioFiles.GetFilenameById(id));
        context.Response.BinaryWrite(file);
        context.ApplicationInstance.CompleteRequest();

    }
    //goes to prev page
    public void ImageButtonPrevPage_Click(object sender, ImageClickEventArgs e)
    {
        Session["CurrentPageSearch"] = (int)Session["CurrentPageSearch"] - 1;
        this.currentpage = (int)Session["CurrentPageSearch"];
        PopulateDataList();
        int pageInd = this.currentpage + 1;
        this.LabelCurrentPage.Text = pageInd.ToString();
    }
    //goes to next page
    public void ImageButtonNextPage_Click(object sender, ImageClickEventArgs e)
    {
        Session["CurrentPageSearch"] = (int)Session["CurrentPageSearch"] + 1;
        this.currentpage = (int)Session["CurrentPageSearch"];
        PopulateDataList();
        int pageInd = this.currentpage + 1;
        this.LabelCurrentPage.Text = pageInd.ToString();
    }
}