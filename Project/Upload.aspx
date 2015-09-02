<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<form id="form1" runat="server">
    <div>
    <table style="width:100%; height:100%">
  <tr>
    <td>Audio Name:</td>
    <td>
        <asp:TextBox ID="TextBoxAudioName" runat="server"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td>Description:</td>
    <td>
        <asp:TextBox ID="TextBoxDescription" runat="server" ></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td><asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="FileUpload1" ErrorMessage="Only mp3 files are allowed!" 
            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.mp3|.MP3|.Mp3)$"></asp:RegularExpressionValidator>
      </td>
    <td><asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload File" /></td>
  </tr>
  <tr>
    <td colspan="2"><asp:Label ID="Label1" runat="server"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="TextBoxAudioName" ErrorMessage="  Audio Name Required!"></asp:RequiredFieldValidator>
      </td>
  </tr>
</table>             
    </div>
    </form>
</asp:Content>

