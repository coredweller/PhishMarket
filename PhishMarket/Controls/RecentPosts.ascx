<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentPosts.ascx.cs"
    Inherits="PhishMarket.Controls.RecentPosts" %>
<div>
    <asp:Repeater ID="rptRecentPosts" runat="server">
        <HeaderTemplate>
            <ul>
                <li>
                    <h2>
                        Recent Posts</h2>
                    <ul>
        </HeaderTemplate>
        <ItemTemplate>
            <li><a href=''>Aliquam libero</a></li>
        </ItemTemplate>
        <FooterTemplate>
            </ul> </li> </ul>
        </FooterTemplate>
    </asp:Repeater>
</div>
