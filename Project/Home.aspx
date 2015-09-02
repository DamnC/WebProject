<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css"> 
        .postbody-text {margin-left: 50px; margin-right: 50px;}
    </style>
</asp:Content>
<asp:Content  ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="font-family:Contra; font-size:large;">
    <p>Message of the day: <%Response.Write(Dal.ExecuteScalar("SELECT MESSAGE FROM SITE_STRINGS")); %></p>
    <p style="text-align:left">This site is from me, to you.</p>
    <br />
    <p style="text-align:left">Here, you can upload your favorite sounds, download and listen to them at any time, and at any place.</p>
    <p style="text-align:left">This site uses advanced technologies, including webservices, allwoing us to provide file storage and sharing solutions.</p>
    <p style="float:right; position:relative; top:150px">Dan, site owner.</p>
<br />
</div>
<br />
</asp:Content>

