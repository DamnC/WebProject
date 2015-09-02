<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Explore.aspx.cs" Inherits="Explore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" >
        function SaveWithParameter(parameter) {
            __doPostBack('audio', parameter)
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" runat="server">
    <asp:DataList ID="DataList1" runat="server" >
    <ItemTemplate>   
<div style="display:table-cell;vertical-align:middle;">
  <table>
  <tr>
    <td colspan="7" >
        <asp:HyperLink NavigateUrl='<%# "Audio.aspx?id="+Eval("ID") %>' ID="HyperLinkAudioName" runat="server" Text='<%# Eval("AUDIO_NAME") %>' Font-Bold="True"></asp:HyperLink></td>
    <td style="float:right">
        Uploaded:<asp:Label ID="LabelDate" runat="server" Text='<%# Eval("UP_DATE").ToString().Remove(10) %>'></asp:Label></td>   
  </tr>
  <tr>
    <td colspan="7">
        By: <asp:HyperLink NavigateUrl='<%# "User.aspx?id="+Users.GetIdByUsername(Eval("USERNAME").ToString()) %>' ID="HyperLinkNick" runat="server" Text='<%# Eval("NICK") %>' Font-Bold="True"></asp:HyperLink></td>
        <td></td>
  </tr>
  <tr>
    <td colspan="8"><audio runat="server" OnPlay="javascript:SaveWithParameter(this.title)" style="width:500px" controls="controls" preload="none" src='<%# "Data/"+ Eval("ID").ToString() + ".mp3" %>' title='<%#Eval("ID").ToString()%>' id="AudioFile"></audio></td> 
  </tr>

  <tr height=16px style="font-size:small">
    <td><asp:ImageButton ID="ImageButtonLike" runat="server" CausesValidation="False" ImageUrl='<%# DidUserLikePic() %>' OnClick="ImageButtonLike_Click" PostBackUrl="~/Explore.aspx" AlternateText='<%#Eval("ID").ToString()%>' /></td>
    <td><asp:ImageButton ID="ImageButtonDownload" runat="server" ImageUrl="Images/SongIcons/Download.png" OnClick="ImageButtonDownload_Click" AlternateText='<%#Eval("ID").ToString()%>'/></td>
    <td><img src="Images/SongIcons/Listens.png" /></td><td><%# AudioFiles.GetListens((int)Eval("ID")) %> </td>
    <td><img src="Images/SongIcons/HeartGrey.png" /></td><td><%# Likes.GetLikesForAudio((int)Eval("ID")) %></td>
    <td><img src="Images/SongIcons/Comment.png" /></td><td><%# Comments.GetNumberOfCommentsForAudio((int)Eval("ID")) %></td>   
  </tr>  
    </table>     
</div>
<br />
    </ItemTemplate>
    </asp:DataList>
<table>
  <tr>
    <th><asp:ImageButton ID="ImageButtonPrevPage" ImageUrl="~/Images/prev.png" OnClick="ImageButtonPrevPage_Click" runat="server" /></th>
    <th><asp:Label ID="LabelCurrentPage" runat="server" Text="Label"></asp:Label></th>
    <th><asp:ImageButton ID="ImageButtonNextPage" ImageUrl="~/Images/next.png" OnClick="ImageButtonNextPage_Click" runat="server" /></th>
  </tr>
</table>  
    </form>

</asp:Content>

