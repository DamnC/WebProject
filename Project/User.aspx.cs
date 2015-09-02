using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class User : System.Web.UI.Page
{
    public int currentpage;
    PagedDataSource pg;
    DataSet ds;
    int user_id;
    //fires when page loads
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.GetPostBackEventReference(this, string.Empty);
        if (Request.QueryString["id"] != null) // check cuz shuoldn't be null
        {
            user_id = int.Parse(Request.QueryString["id"]);
            LabelUp.Text = Users.GetUserNickById(user_id)+"'s Sound Page";
            if (!IsPostBack)
            {
                this.currentpage = 0;
                Session["CurrentPageUser"] = 0;
                PopulateDataList(user_id);
            }
            else
            {
                this.currentpage = (int)(Session["CurrentPageUser"]);
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
            LabelUp.Text = "No Id Specified";
        }
    }
    //refreshes data list
    protected void PopulateDataList(int user_id)
    {
        pg = new PagedDataSource();
        ds = AudioFiles.GetAllFilesByUser(user_id);
        if (ds.Tables[0].Rows.Count>0)
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
            LabelNoAudio.Text = "This user didn't upload any files :(";

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
                PopulateDataList(user_id);
            }
            else
            {
                Likes.Like((int)Session["ID"], int.Parse(((System.Web.UI.WebControls.Image)(sender)).AlternateText));
                ((System.Web.UI.WebControls.Image)(sender)).ImageUrl = "~/Images/SongIcons/LikeButton2.PNG";
                PopulateDataList(user_id);
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
        Session["CurrentPageUser"] = (int)Session["CurrentPageUser"] - 1;
        this.currentpage = (int)Session["CurrentPageUser"];
        PopulateDataList(user_id);
        int pageInd = this.currentpage + 1;
        this.LabelCurrentPage.Text = pageInd.ToString();
    }
    //goes to next page
    public void ImageButtonNextPage_Click(object sender, ImageClickEventArgs e)
    {
        Session["CurrentPageUser"] = (int)Session["CurrentPageUser"] + 1;
        this.currentpage = (int)Session["CurrentPageUser"];
        PopulateDataList(user_id);
        int pageInd = this.currentpage + 1;
        this.LabelCurrentPage.Text = pageInd.ToString();
    }
}