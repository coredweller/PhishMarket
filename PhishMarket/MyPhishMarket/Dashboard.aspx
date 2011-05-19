<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="PhishMarket.MyPhishMarket.Dashboard"
    MasterPageFile="~/Master/Shadowed.Master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%--<div>--%>
    <%--<h2>
            My Phish Market</h2>--%>
    <%--<div>--%>
    <div>
        <h2 class="DashLeft">
            <div class="tTip" id="cloud1" title="Use this section to show your favorite things about Phish">
                My Profile
                <asp:Image ID="imgLeftArrow" runat="server" ImageUrl="/images/SmallLeftArrowLight.jpg" />
                <asp:Image ID="Image1" runat="server" ImageUrl="/images/SmallLeftArrowLight.jpg" /></div>
        </h2>
        <h3 class="DashLeft">
            <asp:HyperLink ID="lnkChangeProfile" runat="server" Text="Change your Profile HERE"></asp:HyperLink>
        </h3>
    </div>
    <div class="ClearBoth" style="">
        <%--<br />--%>
        <br />
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
                    Favorite Album:
                </td>
                <td>
                    <asp:Label ID="lblFavoriteAlbum" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Favorite Tour:
                </td>
                <td>
                    <asp:Label ID="lblFavoriteTour" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Favorite Live Show:
                </td>
                <td>
                    <asp:Label ID="lblFavoriteLiveShow" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Favorite Studio Song:
                </td>
                <td>
                    <asp:Label ID="lblFavoriteStudioSong" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <hr />
        <br />
        <br />
        <div class="tTip" id="Div1" title="Look and choose your posters, ticket stubs, pictures, analysis of shows, and favorite versions">
            <h2>
                My Stuff</h2>
        </div>
        <h4>
            <a href='<%= LinkBuilder.MyAnalysisLink() %>'>Analyze All Shows HERE</a></h4>
        <h4>
            <a href='<%= LinkBuilder.ProfileStep3Link() %>'>Choose Your Favorite Version of every
                song HERE</a>
        </h4>
        <table>
            <tr>
                <td>
                    <a href='<%= LinkBuilder.MyPostersLink() %>'>All My Posters
                </td>
            </tr>
            <tr>
                <td>
                    <asp:PlaceHolder ID="phMyPoster" runat="server" Visible="false">
                        <asp:Image ID="imgPoster" runat="server" />
                    </asp:PlaceHolder>
                    </a>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <a href='<%= LinkBuilder.MyTicketStubsLink() %>'>All My Ticket Stubs
                </td>
            </tr>--%>
           <%-- <tr>
                <td>
                    <asp:PlaceHolder ID="phTicketStub" runat="server" Visible="false">
                        <asp:Image ID="imgTicketStub" runat="server" />
                    </asp:PlaceHolder>
                    </a>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <a href='<%= LinkBuilder.MyPicturesLink() %>'>All My Show Pictures
                </td>
            </tr>
            <tr>
                <td>
                    <asp:PlaceHolder ID="phArt" runat="server" Visible="false">
                        <asp:Image ID="imgArt" runat="server" />
                    </asp:PlaceHolder>
                    </a>
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
        <div class="tTip" id="Div2" title="Add the shows you have attended. Then add the posters, ticket stubs, pictures, and analysis from each one">
            <h2 class="DashLeft">
                My Shows
                <asp:Image ID="Image2" runat="server" ImageUrl="/images/SmallLeftArrowLight.jpg" />
                <asp:Image ID="Image3" runat="server" ImageUrl="/images/SmallLeftArrowLight.jpg" />
            </h2>
        </div>
        <h3 class="DashLeft">
            <a href='<%= LinkBuilder.AddMyShowLink() %>'>Add the shows you have been to</a>
        </h3>
    </div>
    <br />
    <br />
    <br />
    <div class="ClearBoth">
        Tours:
        <asp:DropDownList ID="ddlTours" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnSelectTour" runat="server" Text="Select" OnClick="btnSelectTour_Click" />
    </div>
    <div>
        Tour:
        <%= TourName %>
    </div>
    <br />
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
                          <b>  <%# GetShowName((((PhishPond.Concrete.Show)Container.DataItem).VenueName), FormatDate((((PhishPond.Concrete.Show)Container.DataItem).ShowDate)))%></b></a>
                            
                            </div>
                    </td>
                    <%--<td>
                            <a href='<%# LinkBuilder.MyPicturesByShowLink((((PhishPond.Concrete.Show)Container.DataItem).ShowId)) %>'>Pictures</a>
                        </td>--%>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <%--<a href='<%# LinkBuilder.MyTicketStubsLink((((PhishPond.Concrete.Show)Container.DataItem).ShowId)) %>'>
                            Ticket Stubs</a>-        --%>
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
