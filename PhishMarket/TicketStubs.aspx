<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketStubs.aspx.cs" Inherits="PhishMarket.TicketStubs"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 title="Choose a year to see the ticket stubs from that year." class="tTip" id="cloud9">
        Ticket Stubs</h2>
    <br />
    <h3>
        <uc:YearSelector id="yearSelector" OnYearSelected="yearSelector_YearSelected" runat="server">
        </uc:YearSelector>
    </h3>
    <asp:Repeater ID="rptTicketStubs" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Image ID="imgTicketStub" runat="server" ImageUrl='<%# LinkBuilder.GetTicketStubLink((((PhishPond.Concrete.TicketStub)Container.DataItem).Photo.FileName))%>' />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table></FooterTemplate>
    </asp:Repeater>
</asp:Content>
