<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowReviews.aspx.cs" Inherits="PhishMarket.MyPhishMarket.ShowReviews"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript" src="/javascript/galleria/galleria-1.2.2.min.js"></script>

    <script type="text/javascript">

        //Absolutely needed for Galleria to work on this page
        Galleria.loadTheme('/javascript/galleria/classic/galleria.classic.min.js');

        //When the page loads, if there is a showId or showDate in the URL
        //  this function will load the photos for this show if available
        $(function() {

            var urlVars = $.getUrlVars();
            var showDate, showId;

            //This loop makes it not matter if letters 
            //  in the query string are uppercase or lower case.  This is so
            //  users can type in showDate or showdate or ShOwDaTe or any variation
            for (var i = 0; i < urlVars.length; i++) {

                var piece = urlVars[i];

                if (piece.toLowerCase() == "showdate") {
                    showDate = $.getUrlVar(piece);
                }

                if (piece.toLowerCase() == "showid") {
                    showId = $.getUrlVar(piece);
                }
            }

            callShowReviewsHandler(showId, showDate);

            return false;
        });
       
    </script>

</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%= ShowName %></h2>
    <asp:PlaceHolder ID="phTicketStub" runat="server" Visible="false">
        <br />
        <br />
        <asp:Image ID="imgTicketStub" runat="server" />
    </asp:PlaceHolder>
    <br />
    <br />
    <%--<div title="Click a song name to write a review about it." class="tTip" id="cloud1">
        <h3>
            Setlist</h3>
    </div>
    <asp:Repeater ID="rptSongs" runat="server" OnItemCommand="rptSongs_ItemCommand">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# GetSetNumber((short)Eval("Set.SetNumber"), (bool)Eval("Set.Encore")) %>
                </td>
                <td>
                    <a href='<%# LinkBuilder.SongReviewsLink((Guid)Eval("Song.SetSongId")) %>'>
                        <%# Eval("Song.SongName") %></a>
                </td>
                
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>--%>
    <br />
    <br />
    <%--<asp:HyperLink ID="lnkAddPictures" runat="server" Text="Add Pictures to this show here!"></asp:HyperLink>--%>
    Add your own <a href='<%= LinkBuilder.MyPicturesLink(ShowId) %>'>Pictures</a> or
    <a href='<%= LinkBuilder.MyPostersLink(ShowId) %>'>Posters</a> to this show!
    <div id="gallery">
    </div>
    <hr />
    <br />
    <table>
        <tr>
            <td style="font-size: 1.6em;">
                Reviews
            </td>
            <td>
                - <a href='<%= LinkBuilder.AnalysisLink(ShowId) %>'>Review this show HERE </a>
            </td>
        </tr>
    </table>
    <br />
    <asp:PlaceHolder ID="phNoReviews" runat="server" Visible="false">There are currently
        no reviews for this show. Be the first to review this show <a href='<%= LinkBuilder.AnalysisLink(ShowId) %>'>
            HERE </a></asp:PlaceHolder>
    <asp:Repeater ID="rptReviews" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    Review Date:<%# ((((PhishPond.Concrete.MyShow)Container.DataItem).NotesUpdatedDate)).Value.ToShortDateString()%><ajaxToolkit:Rating
                        ID="ajaxSongRating" runat="server" CurrentRating='<%# DetermineRating((((PhishPond.Concrete.MyShow)Container.DataItem).Rating)) %>'
                        MaxRating="5" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                        FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <%# (((PhishPond.Concrete.MyShow)Container.DataItem).Notes) %>
                </td>
            </tr>
            <tr>
                <td>
                    <hr class="horizontalRule1" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table></FooterTemplate>
    </asp:Repeater>
</asp:Content>
