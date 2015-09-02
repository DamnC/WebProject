<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminUsers.aspx.cs" Inherits="AdminUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" runat="server">
    <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>
    <asp:GridView ID="GridView1" runat="server" 
        onrowdeleting="GridView1_RowDeleting" 
        onrowupdating="GridView1_RowUpdating" AllowPaging="True" 
        onrowediting="GridView1_RowEditing"
        AutoGenerateColumns="False" 
        onpageindexchanging="GridView1_PageIndexChanging" 
        onrowcancelingedit="GridView1_RowCancelingEdit">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True"/>
            <asp:BoundField DataField="NICK" HeaderText="Nickname"/>
            <asp:BoundField DataField="MAIL" HeaderText="Mail" ReadOnly="True"/>
            <asp:BoundField DataField="PASSWORD" HeaderText="Password"/>
            <asp:BoundField DataField="USERNAME" HeaderText="Username" ReadOnly="True"/>
            <asp:BoundField DataField="REALNAME" HeaderText="Real Name"/>
            <asp:BoundField DataField="ISADMIN" HeaderText="Admin" ReadOnly="True"/>
        </Columns>
    </asp:GridView>
    <asp:AccessDataSource ID="AccessDataSource1" runat="server">
    </asp:AccessDataSource>
    </form>
</asp:Content>

