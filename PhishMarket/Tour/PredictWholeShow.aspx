<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PredictWholeShow.aspx.cs"
    Inherits="PhishMarket.TourPages.PredictWholeShow" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
        <%--
    Guess as many songs as you would like. For each song that they play no matter where
        in the show you will get a point. But if you guess one that is not played you lose
        a point. So don't guess too many or you might be more wrong than right.--%>
    <asp:PlaceHolder ID="phAddSongs" runat="server" Visible="false">
        <div>
            <h2>
                List of songs in your set</h2>
            <asp:Repeater ID="rptSongList" runat="server">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("Order") %>
                        </td>
                        <td>
                            <%# Eval("Song.SongName") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
        <br /><br /><br />
        <%--<div>
        <h2>
            Leave yourself notes here.
        </h2>
            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="6" Columns="40"></asp:TextBox>
        </div>
        <br /><br /><br />--%>
        <div>
        <h2>
            Click here to add songs to your set.
        </h2>
            <asp:HyperLink ID="lnkAddSongsToSet" runat="server" Text="Change Your Set!"></asp:HyperLink>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phGetScore" runat="server">
        <asp:HyperLink ID="lnkGetScore" runat="server" Text="Get Score"></asp:HyperLink>
    </asp:PlaceHolder>
    <asp:HiddenField ID="hdnId" runat="server" Visible="false" />
</asp:Content>
