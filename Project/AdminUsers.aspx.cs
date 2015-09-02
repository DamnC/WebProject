using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminUsers : System.Web.UI.Page
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
    //Fires when updating a row
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int id = int.Parse((GridView1.Rows[e.RowIndex].Cells[2]).Text);
        string nick = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text;
        string password = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text;
        string realname = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[7].Controls[0])).Text;
        GridView1.EditIndex = -1;
        Users.UpdateUser(id,nick,password,realname);
        Refresh();
    }
    //Fires when deleting a row
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.RowIndex);
        int id = int.Parse(GridView1.Rows[rowIndex].Cells[2].Text);
        if (Session["ID"].ToString() != id.ToString() && !Users.IsAdmin(id))
        {
            Users.DeleteUser(id);
            Refresh();
        }
        else
            LabelError.Text = "Can't delete yourself or admin.";
    }
    //Fires when clicking update button
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
        GridView1.DataSource = Users.GetAllUsers();
        GridView1.DataBind();
    }
}