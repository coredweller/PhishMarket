<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketStubs.aspx.cs" Inherits="PhishMarket.TicketStubs"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3>
            Ticket Stubs</h3>
    </div>
    <div>
        <uc:YearSelector id="yearSelector" OnYearSelected="yearSelector_YearSelected" runat="server">
        </uc:YearSelector>
    </div>
    <asp:Repeater ID="rptTicketStubs" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><asp:Image ID="imgTicketStub" runat="server" ImageUrl='<%# LinkBuilder.GetTicketStubLink((((PhishPond.Concrete.TicketStub)Container.DataItem).Photo.FileName))%>' /></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table></FooterTemplate>
    </asp:Repeater>
</asp:Content>
