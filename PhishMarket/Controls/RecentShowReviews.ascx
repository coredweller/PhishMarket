﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentShowReviews.ascx.cs"
    Inherits="PhishMarket.Controls.RecentShowReviews" %>
<asp:Repeater ID="rptRecentShowReviews" runat="server">
    <HeaderTemplate>
        <ul>
            <li title="The most recently reviewed shows below.  Click to see the reviews!" class="tTip" id="cloud3">
                <h2>
                    Recent Show Reviews</h2>
                <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li><a href='<%# LinkBuilder.ShowReviewsLink((DateTime)Eval("Show.ShowDate")) %>'>
            <%# ShortDescription((string)Eval("MyShow.Notes")) %></a> - <%# ((DateTime)Eval("Show.ShowDate")).ToShortDateString() %></li>
    </ItemTemplate>
    <FooterTemplate>
        </ul> </li> </ul>
    </FooterTemplate>
</asp:Repeater>
