<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reviews.aspx.cs" Inherits="PhishMarket.Reviews" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 title="Choose a year. Then decide which show you want to see others' reviews or want to review yourself."
        class="tTip" id="cloud9">
        Show Reviews and Analysis</h2>
    <br />
    <h3>
        <uc:YearSelector id="yearSelector" OnYearSelected="yearSelector_YearSelected" runat="server">
        </uc:YearSelector>
    </h3>
    <br />
    <br />
    <div>
        <asp:Repeater ID="rptShows" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <div style="font-weight:bold; font-size:large;">
                    <%# (((PhishPond.Concrete.Show)Container.DataItem).VenueName) %>
                    -
                    <%# FormatDate((((PhishPond.Concrete.Show)Container.DataItem).ShowDate))%>
                </div>
                <div style="font-weight:bold;">
                    <a href='<%# LinkBuilder.AnalysisLink((((PhishPond.Concrete.Show)Container.DataItem).ShowId)) %>'>
                        Write a Review</a>&nbsp;OR&nbsp;<a href='<%# LinkBuilder.ShowReviewsLink((((PhishPond.Concrete.Show)Container.DataItem).ShowId)) %>'>
                            See All Reviews</a>
                </div>
                <hr class="horizontalRule1" style="width: 400px; float: left;" />
                <br />
                <br />
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>