<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetScore.aspx.cs" Inherits="PhishMarket.TourPages.GetScore" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<asp:HyperLink ID="lnkReturn" runat="server" Text="Return to last page"></asp:HyperLink>
<div>
<h2> <%= TopicName %> </h2>
    <div>
            <h3>
                List of songs in your set</h3>
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
</div>
<br /><br /><br />
<asp:PlaceHolder ID="phGuessWholeShowScore" runat="server" Visible="false">
    <div>
        <h3>Guess Whole Show Score</h3>
        <div>
            <asp:Label ID="lblGuessWholeShowScore" runat="server"></asp:Label>
        </div>
    </div>
</asp:PlaceHolder>

</asp:Content>