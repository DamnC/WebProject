<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Audio.aspx.cs" Inherits="Audio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>
    <asp:DataList ID="DataList1" runat="server">
    <ItemTemplate>
    <div style="display:table-cell;vertical-align:middle;">
  <table>
  <tr>
    <td colspan="7" ><asp:Label ID="LabelAudioName"  runat="server" Text='<%# Eval("AUDIO_NAME") %>' Font-Bold="True"></asp:Label></td>
    <td style="float:right">
        Uploaded:<asp:Label ID="LabelDate" runat="server" Text='<%# Eval("UP_DATE").ToString().Remove(10) %>'></asp:Label></td>   
  </tr>
  <tr>
    <td colspan="7">
        By: <asp:HyperLink NavigateUrl='<%# "User.aspx?id="+Users.GetIdByUsername(Eval("USERNAME").ToString()) %>' ID="HyperLinkNick" runat="server" Text='<%# Eval("NICK") %>' Font-Bold="True"></asp:HyperLink></td>
        <td></td>
  </tr>
  <tr>
    <td colspan="8"><audio runat="server" style="width:500px" controls="controls" preload="auto" src='<%# "Data/"+ Eval("ID").ToString() + ".mp3" %>' title='<%#Eval("ID").ToString()%>' id="AudioFile" autoplay="autoplay"></audio></td> 
  </tr>

  <tr height=16px style="font-size:small">
    <td><asp:ImageButton ID="ImageButtonLike" runat="server" CausesValidation="False" ImageUrl='<%# DidUserLikePic() %>' OnClick="ImageButtonLike_Click" AlternateText='<%#Eval("ID").ToString()%>' /></td>
    <td><asp:ImageButton ID="ImageButtonDownload" runat="server" ImageUrl="Images/SongIcons/Download.png" OnClick="ImageButtonDownload_Click" AlternateText='<%#Eval("ID").ToString()%>'/></td>
    <td><img src="Images/SongIcons/Listens.png" /></td><td><%# AudioFiles.GetListens((int)Eval("ID")) %> </td>
    <td><img src="Images/SongIcons/HeartGrey.png" /></td><td><%# Likes.GetLikesForAudio((int)Eval("ID")) %></td>
    <td><img src="Images/SongIcons/Comment.png" /></td><td><%# Comments.GetNumberOfCommentsForAudio((int)Eval("ID")) %></td>   
    
  </tr>
  <tr>
  <td colspan="8"><asp:Label ID="LabelDesc" runat="server" Text='<%#"Description: "+Eval("DESCRIPTION").ToString()%>'></asp:Label></td>
  </tr>
    </table>     
</div>
<br />
<br />
    </ItemTemplate>
    </asp:DataList>
     <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <ContentTemplate>
                <fieldset>
                <legend>Comments</legend>     
    <asp:DataList ID="DataListComments" runat="server">
    <ItemTemplate>
    <table style="width:500px">
    <tr>
    <th style="text-align:left;" colspan="3"><asp:Label ID="LabelCommentNick" runat="server" Text='<%# Eval("NICK").ToString() +" Says:" %>'></asp:Label></th>
    <td style="text-align:right;"><asp:Label ID="LabelCommentDate" runat="server" Text='<%# Eval("DATE_WRITTEN").ToString().Remove(10) %>'></asp:Label></td>
    </tr>
    <tr>
    <td style="text-align:left;" colspan="3"><asp:Label ID="LabelComment" runat="server" Text='<%# Eval("STRING") %>'></asp:Label></td>
    <td style="text-align:right;"></td>
    </tr>
    </table>
    <br />
    <br />
    </ItemTemplate>
    </asp:DataList>
    
<table>
  <tr>
    <th><asp:ImageButton ID="ImageButtonPrevPage" ImageUrl="~/Images/prev.png" OnClick="ImageButtonPrevPage_Click" runat="server" /></th>
    <th><asp:Label ID="LabelCurrentPage" runat="server" Text=""></asp:Label></th>
    <th><asp:ImageButton ID="ImageButtonNextPage" ImageUrl="~/Images/next.png" OnClick="ImageButtonNextPage_Click" runat="server" /></th>
  </tr>
</table> 
    <table style="width:500px;">
  <tr >
    <th style="Text-align:left"  colspan="2"><asp:Label ID="LabelCommentAdd" runat="server" Text="Add/Edit Comment! (Max 140 chars)"></asp:Label></th>
  </tr>
  <tr>
    <td ><asp:TextBox Width="450px" Height="25px" ID="TextBoxComment" runat="server" MaxLength="140"></asp:TextBox></td>
    <td ><asp:Button ID="ButtonComment" runat="server" Text="Submit" onclick="ButtonComment_Click" /></td>
  </tr>
    </table>
     </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>

