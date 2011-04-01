<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PredictTour.aspx.cs" Inherits="PhishMarket.TourPages.PredictTour"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div>
            Tours:
            <asp:DropDownList ID="ddlTours" runat="server">
            </asp:DropDownList>
            <asp:Button ID="btnSelectTour" runat="server" Text="Select Tour" OnClick="btnSelectTour_Click" />
        </div>
        <div>
            Tour:
            <%= TourName %>
        </div>
        <div>
            <asp:Repeater ID="rptShows" runat="server">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                
                    <li><a href='<%# LinkBuilder.PredictShowLink((((PhishPond.Concrete.Show)Container.DataItem).ShowId)) %>'>
                        <%# (((PhishPond.Concrete.Show)Container.DataItem).VenueName) %>
                        -
                        <%# (((PhishPond.Concrete.Show)Container.DataItem).ShowDate.Value).ToShortDateString() %></a> </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
