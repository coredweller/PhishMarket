<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectAlbumControl.ascx.cs"
    Inherits="PhishMarket.Controls.SelectAlbumControl" %>
<div>
    <table>
        <tr>
            <td>
                Choose an album:
            </td>
            <td>
                <asp:DropDownList ID="ddlAlbum" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnChooseAlbum" runat="server" Text="Choose Album" OnClick="btnChooseAlbum_Click" />
            </td>
        </tr>
    </table>
</div>
<div>
    <asp:Repeater ID="rptSongs" runat="server" OnItemCommand="rptSongs_ItemCommand">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# (((PhishPond.Concrete.Song)Container.DataItem).SongName) %>
                </td>
                <td>
                    <asp:LinkButton ID="lnkChooseSong" runat="server" CommandName="CHOOSE" CommandArgument='<%# (((PhishPond.Concrete.Song)Container.DataItem).SongId) %>'
                        Text="Choose"></asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table></FooterTemplate>
    </asp:Repeater>
</div>
