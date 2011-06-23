<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditSongNames.aspx.cs"
    Inherits="PhishMarket.Admin.EditSongNames" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript">
        function confirmDelete() {
            var agree = confirm("Are you sure you want to delete this SetSong?");
            if (agree)
                return true;
            else
                return false;
        }
    </script>

</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        Edit Song Names</h3>
    <br />
    <div>
        <h4>
            Type in a song name or part of a song name to search for the SetSongs' SongName
            that contain that string.
        </h4>
    </div>
    <div>
        <asp:TextBox ID="txtSearchSongName" runat="server"></asp:TextBox><asp:Button ID="btnSearchSongName"
            runat="server" OnClick="btnSearchSongName_Click" Text="Search By Song Name" />
    </div>
    <hr />
    <br />
    <div>
        <h4>
            From this list you can Fix the SongName for just the SetSong (leave check mark UNCHECKED)
            or for the SetSong and Song. You can also delete this SetSong and its Song by clicking
            the Delete button. BE CAREFUL IT IS FINAL.
        </h4>
    </div>
    <div>
        <asp:Repeater ID="rptSongs" OnItemCommand="rptSongs_ItemCommand" runat="server" OnItemCreated="rptSongs_ItemCreated">
            <HeaderTemplate>
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# (((PhishPond.Concrete.SetSong)Container.DataItem).SongName)%>
                    </td>
                    <td style="padding-left: 10px;">
                        <asp:LinkButton runat="server" CommandName="FIX" Text="Fix" CommandArgument='<%# (((PhishPond.Concrete.SetSong)Container.DataItem).SetSongId)%>'></asp:LinkButton>
                    </td>
                    <td style="padding-left: 10px;">
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DELETE" Text="Delete"
                            CommandArgument='<%# (((PhishPond.Concrete.SetSong)Container.DataItem).SetSongId)%>'></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
    </div>
    <br />
    <br />
    <div>
        Song:
        <asp:TextBox ID="txtSongName" runat="server"></asp:TextBox>
        <asp:Button ID="btnFixSetSong" runat="server" OnClick="btnFixSetSong_Click" Text="Fix Set Song" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox ID="chkSongToo" runat="server" />Change Song too? (Leave unchecked
        if you only want SetSong affected)
    </div>
    <asp:HiddenField ID="hdnSetSongIdToFix" runat="server" Visible="false" />
</asp:Content>
