<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <form id="form1" runat="server">
    <div>

    <table>
    <tr>
    <td colspan="2" style="text-align:center">
        <asp:Label ID="LabelError" runat="server" Text=""></asp:Label></td>
    </tr>
  <tr>
    <td>
        <asp:Label ID="Label1" runat="server" Text="Username:"></asp:Label>
      </td>
    <td>
        <asp:TextBox ID="TextBoxUsername" runat="server"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td>
        <asp:Label ID="Label2" runat="server" Text="Password:"></asp:Label>
      </td>
    <td>
        <asp:TextBox ID="TextBoxPassword" TextMode="Password" runat="server"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td colspan="2" style="text-align:center">
        <asp:Button ID="Submit" runat="server" Text="Login" onclick="Submit_Click" />
      </td>
  </tr>
</table>
    
    </div>
    </form>
</asp:Content>

