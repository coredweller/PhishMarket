<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentShowReviews.ascx.cs"
    Inherits="PhishMarket.Controls.RecentShowReviews" %>
<asp:Repeater ID="rptRecentShowReviews" runat="server">
    <HeaderTemplate>
        <ul>
            <li>
                <h2>
                    Recent Show Reviews</h2>
                <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li><a href='<%# LinkBuilder.ViewProfileLink((Guid)Eval("MyShow.UserId")) %>'>
            <%# ShortDescription((string)Eval("MyShow.Notes")) %></a> - <%# ((DateTime)Eval("Show.ShowDate")).ToShortDateString() %></li>
    </ItemTemplate>
    <FooterTemplate>
        </ul> </li> </ul>
    </FooterTemplate>
</asp:Repeater>
