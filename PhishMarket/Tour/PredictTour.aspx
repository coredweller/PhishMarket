﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PredictTour.aspx.cs" Inherits="PhishMarket.TourPages.PredictTour"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div>
            <asp:Repeater ID="rptShows" runat="server">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li><a href='<%# LinkBuilder.PredictShowLink((((PhishPond.Concrete.Show)Container.DataItem).ShowId)) %>'>
                        <%# (((PhishPond.Concrete.Show)Container.DataItem).VenueName) %>
                        -
                        <%# (((PhishPond.Concrete.Show)Container.DataItem).ShowDate.Value).ToShortDateString() %></a>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
