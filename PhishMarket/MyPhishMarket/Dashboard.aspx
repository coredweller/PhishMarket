<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="PhishMarket.MyPhishMarket.Dashboard"
    MasterPageFile="~/Master/Shadowed.Master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="tTip" id="cloud1" title="Use this section to show your favorite things about Phish">
        My Profile</h2>
    <b>
        <asp:HyperLink ID="lnkChangeProfile" runat="server" Text="Change Profile"></asp:HyperLink>&nbsp|&nbsp<asp:HyperLink
            ID="lnkViewProfile" runat="server" Text="View Profile"></asp:HyperLink></b>
    <br />
    <br />
    <div>
        <table>
            <tr>
                <td>
                    Name:
                </td>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Email:
                </td>
                <td>
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Favorite Year
                </td>
                <td>
                    <asp:Label ID="lblFavoriteYear" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Favorite 3.0 Year
                </td>
                <td>
                    <asp:Label ID="lblFavorite3Year" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Favorite Season to see Phish:
                </td>
                <td>
                    <asp:Label ID="lblFavoriteSeason" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Favorite Type of Phish Run:
                </td>
                <td>
                    <asp:Label ID="lblFavoriteRun" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Favorite Album:
                </td>
                <td>
                    <asp:Label ID="lblFavoriteAlbum" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <hr />
    <br />
    <br />
    <div>
        <h2 class="tTip" id="Div2" title="These shows you have been to or written reviews for. You can add posters, pictures, and analysis for each one.">
            My Shows
        </h2>
        <b><a href='<%= LinkBuilder.AddMyShowLink() %>'>Add shows to My Shows HERE!</a> </b>
    </div>
    <br /><br />
    <div>
    <b>
        <uc:YearSelector id="yearSelector" OnYearSelected="yearSelector_YearSelected" runat="server">
        </uc:YearSelector>
        </b>
    </div>
    <br /><br />
    <div>
        <asp:Repeater ID="rptShows" runat="server" OnItemCommand="rptShows_ItemCommand">
            <HeaderTemplate>
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <div class="tTip" id="Div3" title="Delete this show from your list?">
                            <asp:ImageButton ID="lnkDelete" runat="server" Text="Delete Show" ImageUrl="/images/buttons/xbutton2.png"
                                CommandName="DELETE" CommandArgument='<%# (((PhishPond.Concrete.Show)Container.DataItem).ShowId) %>'>
                            </asp:ImageButton>
                        </div>
                    </td>
                    <td>
                        <div class="tTip" id="Div1" title="Click to see reviews of this show!">
                            <a href='<%# LinkBuilder.ShowReviewsLink((((PhishPond.Concrete.Show)Container.DataItem).ShowId)) %>'>
                                <b>
                                    <%# (((PhishPond.Concrete.Show)Container.DataItem).GetShowName()) %></b></a>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <a href='<%# LinkBuilder.MyPostersLink((((PhishPond.Concrete.Show)Container.DataItem).ShowId)) %>'>
                            Posters</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='<%# LinkBuilder.MyPicturesLink((((PhishPond.Concrete.Show)Container.DataItem).ShowId)) %>'>
                                Show Pictures</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='<%# LinkBuilder.AnalysisLink((((PhishPond.Concrete.Show)Container.DataItem).ShowId)) %>'>
                                    Analysis</a>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <hr class="horizontalRule1" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
    </div>
    <%--</div>--%>
</asp:Content>
