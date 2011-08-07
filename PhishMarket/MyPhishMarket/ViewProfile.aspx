<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewProfile.aspx.cs" Inherits="PhishMarket.MyPhishMarket.ViewProfile"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript" src="/javascript/galleria/galleria-1.2.2.min.js"></script>

    <script type="text/javascript">

        //Absolutely needed for Galleria to work on this page
        Galleria.loadTheme('/javascript/galleria/classic/galleria.classic.js');

        //When the page loads, if there is a showId in the URL
        //  this function will load the pictures for this user if available
        $(function() {
            var userClientId = $('#<%= hdnUserId.ClientID %>');
            var userId = $(userClientId).val();

            callViewProfileHandler(userId);

            return false;
        });

    </script>

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2 title="Basic Information about the user." class="tTip" id="cloud3">
            View Profile
        </h2>
    </div>
    <asp:PlaceHolder ID="phNoProfileError" runat="server" Visible="false">This user does
        not have a profile </asp:PlaceHolder>
    <div>
        <table>
        <tr>
            <td>
                User Name:
            </td>
            <td>
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
            </td>
        </tr>
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
            <%--<tr>
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
            </tr>--%>
        </table>
    </div>
    <br />
    <br />
    <h2 title="The shows that this user has attended and their rating. Click the review to see it in full." class="tTip" id="cloud4">
        Shows Attended</h2>
    <%-- This section has a KeyValuePair of Show and MyShow so KEY = SHOW and VALUE = MYSHOW --%>
    <asp:Repeater ID="rptMyShows" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <strong>
                        <%# Eval("Key.VenueName") %>
                        -
                        <%# FormatDate((DateTime?)Eval("Key.ShowDate"))%></strong>
                    <ajaxToolkit:Rating ID="ajaxSongRating" runat="server" CurrentRating='<%# DetermineRating((int?)Eval("Value.Rating")) %>'
                        MaxRating="10" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                        ReadOnly="true" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" />
                </td>
            </tr>
            <asp:PlaceHolder ID="phNotes" runat="server" Visible='<%# !string.IsNullOrEmpty((string)Eval("Value.Notes")) %>'>
                <tr>
                    <td colspan="2">
                        <a href='<%# LinkBuilder.ViewFullShowReviewLink((Guid)Eval("Value.MyShowId")) %>'>
                            Click Here to Read the Review</a>
                    </td>
                </tr>

            </asp:PlaceHolder>
        </ItemTemplate>
        <FooterTemplate>
            </table></FooterTemplate>
    </asp:Repeater>
    <br /><br />
    <div id="gallery">
        <%--<img src="/../images/Shows/coretest2-634195715440294949.jpg" alt="Minibri" title="Title of all titles" />--%>
        <%--<img src="/images/Shows/coretest2-634195715440294949.jpg" alt="blah" />--%>
    </div>
    
    <asp:HiddenField ID="hdnUserId" runat="server" Visible="true" />
</asp:Content>
