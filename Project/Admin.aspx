<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" runat="server">
<table>
  <tr>
    <th colspan="2" style="font-size:larger">Editors</th>
  </tr>
  <tr>
    <th>Audio files editor:</th>
    <td><a href="AdminAudio.aspx">Audio Editor</a></td>
  </tr>
  <tr>
    <th>Users editor:</th>
    <td><a href="AdminUsers.aspx">Users Editor</a></td>
  </tr>
  <tr>
    <th>Homepage Message:</th>
    <td><asp:TextBox ID="TextBoxMessage" runat="server" Text=""></asp:TextBox></td>
  </tr>
  <tr>
  <td colspan="2" style="text-align:center">
      <asp:Button ID="ButtonMessage" runat="server" Text="Submit Message" 
          onclick="ButtonMessage_Click" /></td>
  </tr>
</table>
<br />
<br />
<table>
  <tr>
    <th colspan="2" style="font-size:larger">Stats:</th>
  </tr>
  <tr>
    <td>Visitors:</td>
    <td><%Response.Write(Application["visitors"].ToString()); %></td>
  </tr>
    <tr>
    <td>Visits:</td>
    <td><%Response.Write(Application["visits"].ToString()); %></td>
  </tr>
  <tr>
    <td>Connected Users:</td>
    <td><%Response.Write(Application["connected"].ToString()); %></td>
  </tr>
  <tr>
    <td>Number of Users:</td>
    <td><%Response.Write(Dal.ExecuteScalar("SELECT COUNT(*) FROM USERS")); %></td>
  </tr>
  <tr>
    <td>Total Downloads:</td>
    <td><% Response.Write(new ServiceReference1.WebServiceSoapClient().GetNumberOfDownloads().ToString()); %></td>
  </tr>
    <tr>
    <td>Total Listens:</td>
    <td><% Response.Write(AudioFiles.GetSumOfListens()); %></td>
  </tr>
</table>
</form>
</asp:Content>

