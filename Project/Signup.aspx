<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Signup.aspx.cs" Inherits="Signup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" runat="server" >
<table lang="en" dir="ltr">
  <tr>
    <td>Name:</td>
    <td>
        <asp:TextBox ID="TextBoxRealName" runat="server"></asp:TextBox>
      &nbsp;</td>
    <td>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
            ControlToValidate="TextBoxRealName" ErrorMessage="Only Engilsh And Numbers " 
            ValidationExpression="^[a-zA-Z0-9]+$"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRealName" runat="server" 
            ErrorMessage="Must Fill" ControlToValidate="TextBoxRealName"></asp:RequiredFieldValidator>
      </td>
  </tr>
  <tr>
    <td>Nick Name:</td>
    <td>
        <asp:TextBox ID="TextBoxNickName" runat="server"></asp:TextBox>
      </td>
    <td>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
            ControlToValidate="TextBoxNickName" ErrorMessage="Only Engilsh And Numbers " 
            ValidationExpression="^[a-zA-Z0-9]+$"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNickName" runat="server" 
            ErrorMessage="Must Fill" ControlToValidate="TextBoxNickName"></asp:RequiredFieldValidator>
      </td>
  </tr>
    <tr>
    <td>Email:</td>
    <td>
        <asp:TextBox ID="TextBoxMail" runat="server"></asp:TextBox>
        </td>
    <td>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="TextBoxMail" ErrorMessage="Must Fill"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidatorMail" runat="server" 
            ErrorMessage="Invalid Mail" ControlToValidate="TextBoxMail" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
    <td>Username:</td>
    <td>
        <asp:TextBox ID="TextBoxUsername" runat="server"></asp:TextBox>
        </td>
    <td>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
            ControlToValidate="TextBoxUsername" ErrorMessage="Only Engilsh And Numbers " 
            ValidationExpression="^[a-zA-Z0-9]+$"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="TextBoxUsername" ErrorMessage="Must Fill"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
    <td>Password:</td>
    <td>
        <asp:TextBox ID="TextBoxPassword" TextMode="Password" runat="server"></asp:TextBox>
        </td>
    <td>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
            ControlToValidate="TextBoxPassword" ErrorMessage="Only Engilsh And Numbers " 
            ValidationExpression="^[a-zA-Z0-9]+$"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="TextBoxPassword" ErrorMessage="Must Fill"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="TextBoxPassword" ErrorMessage="At least 6 chars" 
            ValidationExpression="^.{6,}$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
    <td>Retype password:</td>
    <td>
        <asp:TextBox ID="TextBoxPassword2" TextMode="Password" runat="server"></asp:TextBox>
        </td>
    <td>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ControlToValidate="TextBoxPassword2" ErrorMessage="Must Fill "></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToCompare="TextBoxPassword" ControlToValidate="TextBoxPassword2" 
            ErrorMessage="Not the same"></asp:CompareValidator>
        </td>
    </tr>
  <tr>
    <td></td>
    <td>
       <center> 
           <asp:Button  ID="ButtonSubmit" runat="server" Text="Signup" 
               onclick="ButtonSubmit_Click" /></center>
      </td>
    <td>
        <asp:Label ID="LabelExistError" runat="server"></asp:Label>
      </td>
  </tr>
</table>
    </form>
</asp:Content>

