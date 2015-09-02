using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AudioAdmin : System.Web.UI.Page
{
    //Fires on page load
    protected void Page_Load(object sender, EventArgs e) 
    {
        if (Session["Admin"] == null || !(bool)Session["Admin"])
            Response.Redirect("Home.aspx");
        if (!IsPostBack)
        {
            Refresh();
        }
    }
    //Fires on updating a row
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int id = int.Parse((GridView1.Rows[e.RowIndex].Cells[2]).Text);
        string audio_name = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text;
        int listens = int.Parse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text);
        DateTime UpDate = DateTime.Parse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].Controls[0])).Text);
        string filename = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[7].Controls[0])).Text;
        string desc = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[8].Controls[0])).Text;
        GridView1.EditIndex = -1;
        AudioFiles.UpdateAudio(id, audio_name, listens, UpDate, filename, desc);
        Refresh();
    }
    //Fires on deleting a row
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.RowIndex);
        int id = int.Parse(GridView1.Rows[rowIndex].Cells[2].Text);
        AudioFiles.DeleteFile(id);
        Refresh();
    }
    //Fires when clicking editing button
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        Refresh();
    }
    //Fires when clicking cancel button
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Refresh();
    }
    //Fires when changing page
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Refresh();
    }
    //Refreshes the Grid
    protected void Refresh()
    {
        GridView1.DataSource = AudioFiles.GetFilesAdmin();
        GridView1.DataBind();
    }
}