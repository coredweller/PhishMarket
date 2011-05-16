<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowReviews.aspx.cs" Inherits="PhishMarket.MyPhishMarket.ShowReviews"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript" src="/javascript/galleria/galleria-1.2.2.min.js"></script>

    <script type="text/javascript">

        //Absolutely needed for Galleria to work on this page
        Galleria.loadTheme('/javascript/galleria/classic/galleria.classic.min.js');

        //When the page loads, if there is a showId in the URL
        //  this function will load the pictures for this user if available
        $(function() {

            var showId = $.getUrlVar('showId');

            callShowReviewsHandler(showId);

            return false;
        });
       
    </script>

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%= ShowName %></h2>    
    <%--<div>
        <ajaxToolkit:SlideShowExtender ID="SlideShowExtender1" runat="server" TargetControlID="imgPhotos"
            SlideShowServiceMethod="GetShowPictures" SlideShowServicePath="/SlideService.asmx"
            UseContextKey="true" AutoPlay="false" NextButtonID="btnNext" PlayButtonText="Play"
            StopButtonText="Stop" PreviousButtonID="btnPrev" PlayButtonID="btnPlay" Loop="true"
            ImageTitleLabelID="lblTitle" ImageDescriptionLabelID="lblDescription" />
        <br />
        <br />
        <asp:Image ID="imgPhotos" runat="server" />
        <br />
        <br />
    </div>--%>
    
    <%--<div>
        <center>
            <asp:Label ID="lblTitle" runat="server"></asp:Label><br />
            <asp:Label ID="lblDescription" runat="server"></asp:Label><br />
        </center>
        <center>
            <asp:Button ID="btnPrev" Text="Prev" runat="server" />
            <asp:Button ID="btnPlay" Text="Play" runat="server" />
            <asp:Button ID="btnNext" Text="Next" runat="server" />
        </center>
    </div>--%>
    <br /><br />
    <asp:Image ID="imgTicketStub" runat="server" />
    <br />
    <br />
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
                <%--<td>
                        <asp:LinkButton ID="lnkEditNotes" runat="server" CommandArgument='<%# Eval("Song.SetSongId") %>'
                            CommandName="EDITNOTES" Text='<%# Eval("Song.SongName") %>'></asp:LinkButton>
                    </td>
                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                    <td>
                        <ajaxToolkit:Rating ID="ajaxSongRating" runat="server" CurrentRating='<%# DetermineRating((int?)Eval("Analysis.Rating")) %>'
                            MaxRating="5" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                            FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" OnChanged="ajaxSongRating_Changed"
                            Tag='<%# Eval("Song.SetSongId") %>' />
                    </td>
                    &nbsp;&nbsp;&nbsp;
                    <td>
                        <%# GetShortenedNote((string)Eval("Analysis.Notes")) %>
                    </td>--%>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <br />
    <br />
    
    
    <div id="gallery">
    </div>
    <hr />
    <br />
    <h3>
        Reviews</h3>
    <br />
    <asp:PlaceHolder ID="phNoReviews" runat="server" Visible="false">There are currently
        no reviews for this show. </asp:PlaceHolder>
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
                    ------------------
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table></FooterTemplate>
    </asp:Repeater>
</asp:Content>
