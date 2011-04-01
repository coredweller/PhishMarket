<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ForumActiveDiscussion.ascx.cs"
    Inherits="YAF.Controls.ForumActiveDiscussion" %>
<asp:UpdatePanel ID="UpdateStatsPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table border="0" class="content" cellspacing="1" cellpadding="0" width="100%">
            <tr>
                <td class="header1" colspan="2">
                    <YAF:CollapsibleImage ID="CollapsibleImage" runat="server" BorderWidth="0" Style="vertical-align: middle"
                        PanelID='ActiveDiscussions' AttachedControlID="ActiveDiscussionPlaceHolder" OnClick="CollapsibleImage_OnClick" />&nbsp;&nbsp;<YAF:LocalizedLabel
                            ID="ActiveDiscussionHeader" runat="server" LocalizedTag="ACTIVE_DISCUSSIONS" />
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="ActiveDiscussionPlaceHolder">
                <tr>
                    <td class="header2" colspan="2">
                        <YAF:LocalizedLabel ID="LatestPostsHeader" runat="server" LocalizedTag="LATEST_POSTS" />
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="LatestPosts" OnItemDataBound="LatestPosts_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td class="post">
                                &nbsp;<b><asp:HyperLink ID="TextMessageLink" runat="server" /></b>
                                <YAF:LocalizedLabel ID="ByLabel" runat="server" LocalizedTag="BY" />
                                <YAF:UserLink ID="LastUserLink" runat="server" />
                                (<asp:HyperLink ID="ForumLink" runat="server" />)
                            </td>
                            <td class="post" style="width: 30em; text-align: right;">
                                <asp:Label ID="LastPostedDateLabel" runat="server" />
                                <asp:HyperLink ID="ImageMessageLink" runat="server">
                                    <YAF:ThemeImage ID="LastPostedImage" runat="server" Style="border: 0" />
                                </asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:PlaceHolder>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
