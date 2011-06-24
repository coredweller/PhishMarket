<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="PhishMarket.Stats.Report"
    MasterPageFile="~/Master/Shadowed.Master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>
            Statistics Report</h2>
    </div>
    <br />
    <div>
        <h4>
            Choose how you want to view the songs.
            <br />
            Either by the album (or Live ONLY) or by the first letter of the song's name</h4>
    </div>
    <br />
    <div>
        <table>
            <tr>
                <td>
                    <div class="tTip" id="cloud1" title="Choose an album to see the songs from that album, or 'Live ONLY' for songs not on an album (ex: Gumbo)">
                        By Album:
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAlbum" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="btnChooseAlbum" runat="server" Text="Choose Album" OnClick="btnChooseAlbum_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    OR
                </td>
            </tr>
            <tr>
                <td>
                    <div class="tTip" id="cloud2" title="Or choose a letter to see all songs that start with that letter.">
                        By Letter:
                    </div>
                </td>
                <td>
                    <asp:Repeater ID="rptLetterList" runat="server" OnItemCommand="rptLetterList_ItemCommand">
                        <HeaderTemplate>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CommandArgument='<%# Container.DataItem %>' Text='<%# Container.DataItem %>'></asp:LinkButton>
                            <asp:Label runat="server" Text='<%# Container.DataItem != "Z" ? " | " : "" %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <asp:PlaceHolder ID="phReport" runat="server" Visible="false">
        <table>
            <tr>
                <td>
                    <div class="tTip" id="cloud3" title="Now choose a report from the right to see the stats you came here for!"
                        style="clear: both;">
                        Choose Report:</div>
                </td>
                <td>
                    <div class="tTip" id="cloud4" title="This report shows how many people chose that version as their favorite of all that song's versions.">
                        <asp:LinkButton ID="lnkFavorite" runat="server" CommandName="Favorite" Text="Most Favorite"
                            OnClick="Report_Click"></asp:LinkButton></div>
                </td>
                <td>
                    |
                </td>
                <td>
                    <div class="tTip" id="cloud5" title="This report shows the version of the song that was rated the highest of all the song's versions.">
                        <asp:LinkButton ID="lnkHighestRated" runat="server" CommandName="Highest" Text="Highest Rated"
                            OnClick="Report_Click"></asp:LinkButton></div>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <br />
    <asp:PlaceHolder ID="phFavorite" runat="server" Visible="false">
        <div>
            <div class="tTip" id="Div2" title="This report shows how many people chose that version as their favorite of all that song's versions.">
                <table>
                    <tr>
                        <td>
                            <h4>
                                Most Favorite Versions</h4>
                        </td>
                        <td>
                            &nbsp;-&nbsp;<a href='<%= LinkBuilder.ProfileStep3Link() %>'>Go pick your own favorites now!</a>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Repeater ID="rptFavorites" runat="server">
                <HeaderTemplate>
                    <table>
                        <th align="left" style="padding-bottom: 10px;">
                            Song Name
                        </th>
                        <th>
                        </th>
                        <th align="left" style="padding-bottom: 10px;">
                            Favorite Version
                        </th>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# (((PhishPond.Concrete.LiveSongInfo)Container.DataItem).SongName)%>
                        </td>
                        <td>
                        </td>
                        <td>
                            <%# DetermineShow((((PhishPond.Concrete.LiveSongInfo)Container.DataItem).FavoriteShowInfo)) %>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr class="horizontalRule1" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phHighest" runat="server" Visible="false">
        <div>
            <div class="tTip" id="Div1" title="This report shows the version of the song that was rated the highest of all the song's versions.">
                <table>
                    <tr>
                        <td>
                            <h4>
                                Highest Rated</h4>
                        </td>
                        <%--<td>
                            &nbsp;-&nbsp;<a href='<%= LinkBuilder.MyAnalysisLink() %>'>Go rate the songs yourself!</a>
                        </td>--%>
                    </tr>
                </table>
            </div>
            <asp:Repeater ID="rptHighestRanked" runat="server">
                <HeaderTemplate>
                    <table>
                        <th align="left" style="padding-bottom: 10px;">
                            Song Name
                        </th>
                        <th align="left" style="padding-bottom: 10px; padding-right: 20px;">
                            Rating
                        </th>
                        <th align="left" style="padding-bottom: 10px;">
                            Show
                        </th>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# (((PhishPond.Concrete.LiveSongInfo)Container.DataItem).SongName)%>
                        </td>
                        <td align="center" style="padding-right: 20px;">
                            <%# (((PhishPond.Concrete.LiveSongInfo)Container.DataItem).HighestRating)%>
                        </td>
                        <td>
                            <%# DetermineShow((((PhishPond.Concrete.LiveSongInfo)Container.DataItem).HighestRatedShowInfo)) %>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr class="horizontalRule1" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </asp:PlaceHolder>
</asp:Content>
