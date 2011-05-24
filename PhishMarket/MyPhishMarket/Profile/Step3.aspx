<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step3.aspx.cs" Inherits="PhishMarket.MyPhishMarket.ProfilePages.Step3"
    MasterPageFile="~/Master/Shadowed.Master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <div>
        <h3>
            Step 3: Favorite Versions</h3>
    </div>
    <div id="alertDiv" style="padding: 10px; float: left;">
    </div>
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
    <br />
    <asp:PlaceHolder ID="phFavoriteChoice" runat="server" Visible="false">
        <div>
            <table>
                <tr>
                    <td>
                        Choose your favorite version of the song:
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlFavoriteChoice" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:PlaceHolder>
    <br /><br />
    <div>
        <asp:Repeater ID="rptSongs" runat="server" OnItemCommand="rptSongs_ItemCommand">
            <HeaderTemplate>
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# (((PhishPond.Concrete.profileGetFavoriteVersionsResult)Container.DataItem).SongName) %>
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkChooseSong" runat="server" CommandName="CHOOSE" CommandArgument='<%# (((PhishPond.Concrete.profileGetFavoriteVersionsResult)Container.DataItem).SongId) %>'
                            Text="Choose"></asp:LinkButton>
                    </td>
                    <td>
                        <%# GetSongName((((PhishPond.Concrete.profileGetFavoriteVersionsResult)Container.DataItem).SetSongLength), (((PhishPond.Concrete.profileGetFavoriteVersionsResult)Container.DataItem).ShowDate), (((PhishPond.Concrete.profileGetFavoriteVersionsResult)Container.DataItem).City), (((PhishPond.Concrete.profileGetFavoriteVersionsResult)Container.DataItem).State)) %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
    </div>
    
</asp:Content>
