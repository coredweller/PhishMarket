<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAnalysis.aspx.cs" Inherits="PhishMarket.MyPhishMarket.MyAnalysis"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <h3 title="Choose a tour. Then decide which show you want to see others' reviews or want to review yourself." class="tTip" id="cloud9">
        Show Reviews and Analysis</h3>
    <br />
    <br />
    <div>
        Tours:
        <asp:DropDownList ID="ddlTours" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnSelectTour" runat="server" Text="Select Tour" OnClick="btnSelectTour_Click" />
    </div>
    <%--<div>
            Tour:
            <%= TourName %>
        </div>--%>
    <div>
        <asp:Repeater ID="rptShows" runat="server">
            <HeaderTemplate>
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# (((PhishPond.Concrete.Show)Container.DataItem).VenueName) %>
                        -
                        <%# FormatDate((((PhishPond.Concrete.Show)Container.DataItem).ShowDate))%>
                    </td>
                    <td>
                        <a href='<%# LinkBuilder.AnalysisLink((((PhishPond.Concrete.Show)Container.DataItem).ShowId)) %>'>
                            Write a Review</a>&nbsp;OR&nbsp;
                    </td>
                    <td>
                        <a href='<%# LinkBuilder.ShowReviewsLink((((PhishPond.Concrete.Show)Container.DataItem).ShowId)) %>'>
                            See All Reviews</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
