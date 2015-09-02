using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Audio : System.Web.UI.Page
{
    public int currentpage;
    PagedDataSource pg;
    DataSet ds;
    //fires on page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"]!=null) // check cuz shuoldn't be null
        {
            if (Session["ID"] == null)
            {
                HideCommentAdding();
            }
            int audio_id = int.Parse(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                this.currentpage = 0;
                Session["CurrentPageComments"] = 0;
                RefreshSong(audio_id);
                ShowComments(audio_id);
            }
            else
            {
                this.currentpage = (int)(Session["CurrentPageComments"]);
            }
            int pageInd = this.currentpage + 1; //Page number
            LabelCurrentPage.Text = pageInd.ToString();
        }
        else
        {
            LabelError.Text = "No Id Specified";
        }

    }
    //refreshes data list of song
    protected void RefreshSong(int audio_id)
    {
        if (Session[audio_id.ToString()] == null) //adds listen if user didn't listen
        {
            AudioFiles.AddListen(audio_id);
            Session[audio_id.ToString()] = true;
        }
        DataList1.DataSource = AudioFiles.GetFileById(audio_id);
        DataList1.DataBind();
    }
    //refreshes data list of comments
    protected void ShowComments(int audio_id)
    {
        pg = new PagedDataSource();
        ds = Comments.GetCommentsByAudioId(audio_id);
        if (ds.Tables[0].Rows.Count > 0)
        {
            pg.AllowPaging = true;
            pg.DataSource = ds.Tables[0].DefaultView;
            pg.PageSize = 4;
            pg.CurrentPageIndex = this.currentpage;
            this.ImageButtonNextPage.Enabled = !pg.IsLastPage;
            this.ImageButtonPrevPage.Enabled = !pg.IsFirstPage;
            DataListComments.DataSource = pg;
            DataListComments.DataBind();
        }
    }
    //returns picture for like button
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
    // likes/dislikes
    public void ImageButtonLike_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["ID"] != null)
        {
            if (Likes.DidUserLikeAudio((int)Session["ID"], int.Parse(((System.Web.UI.WebControls.Image)(sender)).AlternateText))) //AlternateText contains audio id
            {
                Likes.Unlike((int)Session["ID"], int.Parse(((System.Web.UI.WebControls.Image)(sender)).AlternateText));
                ((System.Web.UI.WebControls.Image)(sender)).ImageUrl = "~/Images/SongIcons/LikeButton.PNG";
                RefreshSong(int.Parse(((System.Web.UI.WebControls.Image)(sender)).AlternateText));
            }
            else
            {
                Likes.Like((int)Session["ID"], int.Parse(((System.Web.UI.WebControls.Image)(sender)).AlternateText));
                ((System.Web.UI.WebControls.Image)(sender)).ImageUrl = "~/Images/SongIcons/LikeButton2.PNG";
                RefreshSong(int.Parse(((System.Web.UI.WebControls.Image)(sender)).AlternateText));
            }
        }
        else
        {
            Session["CurrentlyListening"] = int.Parse(((System.Web.UI.WebControls.Image)(sender)).AlternateText);
            Response.Redirect("Login.aspx?m=You need an account to like audios");
        }
    }
    //downloads audio
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
        Session["CurrentPageComments"] = (int)Session["CurrentPageComments"] - 1;
        this.currentpage = (int)Session["CurrentPageComments"];
        ShowComments(int.Parse(Request.QueryString["id"]));
        int pageInd = this.currentpage + 1;
        this.LabelCurrentPage.Text = pageInd.ToString();
    }
    //goes to next page
    public void ImageButtonNextPage_Click(object sender, ImageClickEventArgs e)
    {
        Session["CurrentPageComments"] = (int)Session["CurrentPageComments"] + 1;
        this.currentpage = (int)Session["CurrentPageComments"];
        ShowComments(int.Parse(Request.QueryString["id"]));
        int pageInd = this.currentpage + 1;
        this.LabelCurrentPage.Text = pageInd.ToString();
    }
    //submits a comment
    protected void ButtonComment_Click(object sender, EventArgs e)
    {
        string comment = TextBoxComment.Text.ToString();
        int audio_id = int.Parse(Request.QueryString["id"].ToString());
        int user_id = int.Parse(Session["ID"].ToString());
        DateTime date = DateTime.Now;
        if (Comments.DoesCommentExist(user_id, audio_id))
        {
            Comments.UpdateComment(user_id, audio_id, date, comment);
        }
        else
        {
            Comments.InsertComment(user_id, audio_id, date, comment);
        }
        ShowComments(int.Parse(Request.QueryString["id"].ToString()));
    }
    //hides option of commenting
    protected void HideCommentAdding()
    {
        LabelCommentAdd.Text = "You need to signin in order to add a comment!";
        TextBoxComment.Visible = false;
        ButtonComment.Visible = false;
    }
}