<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SongReviews.aspx.cs" Inherits="PhishMarket.MyPhishMarket.SongReviews"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%= ShowName %></h2>
    <br />
    <table>
        <tr>
            <td>
                <h3>
                    <%= SongName %></h3>
            </td>
            <td>
                -
                <asp:HyperLink ID="lnkReviewShow" runat="server" Text="Review this song yourself"></asp:HyperLink>
            </td>
        </tr>
    </table>
    <br />
    <hr />
    <br />
    <h3>
        Reviews</h3>
    <br />
    <asp:PlaceHolder ID="phNoReviews" runat="server" Visible="false">
        There are currently no reviews for this song. <asp:HyperLink ID="lnkNoReviews" runat="server" Text="Review this song yourself"></asp:HyperLink>
    </asp:PlaceHolder>
    <asp:Repeater ID="rptReviews" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                
                    Review Date:<%# ((((PhishPond.Concrete.Analysis)Container.DataItem).UpdatedDate)).Value.ToShortDateString()%><ajaxToolkit:Rating ID="ajaxSongRating" runat="server" CurrentRating='<%# DetermineRating((int?)Eval("Rating")) %>'
                        MaxRating="5" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                        FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                
                    <%# (((PhishPond.Concrete.Analysis)Container.DataItem).Notes) %>
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
