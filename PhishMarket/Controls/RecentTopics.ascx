<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentTopics.ascx.cs" Inherits="PhishMarket.Controls.RecentTopics" %>

<div>
    <asp:Repeater ID="rptRecentTopics" runat="server">
        <HeaderTemplate>
            <ul>
                <li title="The most recently posted topics below.  Click to post your own reply!" class="tTip" id="cloud3">
                    <h2>
                        Recent Topics</h2>
                    <ul>
        </HeaderTemplate>
        <ItemTemplate>
            <li><a href='<%# LinkBuilder.YafTopicLink( (((Yaf.Concrete.yafGetRecentTopicsResult)Container.DataItem).TopicID) ) %>'><%# (((Yaf.Concrete.yafGetRecentTopicsResult)Container.DataItem).Topic) %></a></li>
        </ItemTemplate>
        <FooterTemplate>
            </ul> </li> </ul>
        </FooterTemplate>
    </asp:Repeater>
</div>