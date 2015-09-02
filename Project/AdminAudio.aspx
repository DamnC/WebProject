<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminAudio.aspx.cs" Inherits="AudioAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" runat="server">
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
            <asp:BoundField DataField="NICK" HeaderText="Nickname" ReadOnly="True"/>
            <asp:BoundField DataField="AUDIO_NAME" HeaderText="Audio Name"/>
            <asp:BoundField DataField="LISTENS" HeaderText="Listens"/>
            <asp:BoundField DataField="UP_DATE" HeaderText="Up Date"/>
            <asp:BoundField DataField="FILE_NAME" HeaderText="File Name"/>
            <asp:BoundField DataField="DESCRIPTION" HeaderText="Description"/>
        </Columns>
    </asp:GridView>
    <asp:AccessDataSource ID="AccessDataSource1" runat="server">
    </asp:AccessDataSource>
    </form>
</asp:Content>

