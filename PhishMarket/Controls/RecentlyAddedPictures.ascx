<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentlyAddedPictures.ascx.cs"
    Inherits="PhishMarket.Controls.RecentlyAddedPictures" %>
<asp:Repeater ID="rptRecentlyAddedPictures" runat="server">
    <HeaderTemplate>
        <ul>
            <li title="The most recently added pictures below.  Click to experience the pictures!"
                class="tTip" id="cloud3">
                <h2>
                    Recently Added Pictures</h2>
                <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li><a href='<%# LinkBuilder.ShowReviewsLink(new Guid(((PhishMarket.Controls.ShowPhoto)Container.DataItem).ShowId)) %>'>
            <%# ((PhishMarket.Controls.ShowPhoto)Container.DataItem).NickName %> </a> - <%# ((PhishMarket.Controls.ShowPhoto)Container.DataItem).ShowDate %></li>
    </ItemTemplate>
    <FooterTemplate>
        </ul> </li> </ul>
    </FooterTemplate>
</asp:Repeater>
