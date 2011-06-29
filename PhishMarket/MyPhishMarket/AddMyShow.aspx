<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMyShow.aspx.cs" Inherits="PhishMarket.MyPhishMarket.AddMyShow"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>
            Add Shows to My Shows</h2>
        <br />
        <%--<div>
            <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">You have successfully
                added a show to My Shows </asp:PlaceHolder>
            <asp:PlaceHolder ID="phError" runat="server" Visible="false">I am sorry but an error
                has occurred. Please try your request again. </asp:PlaceHolder>
            <asp:PlaceHolder ID="phAlreadyAdded" runat="server" Visible="false">This show has already
                been added to My Shows. Please choose another show. </asp:PlaceHolder>
        </div>--%>
        <asp:PlaceHolder ID="phPhishNet" runat="server" Visible="false">
        <br />
        <br />
        <div class="tTip" id="cloud1" title="Enter your Phish.Net User Name to retrieve your shows">
            <h3>
                Import your shows from phish.net!</h3>
            Phish.Net User Name:
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            <asp:Button ID="btnGetShows" runat="server" Text="Get Shows" OnClick="btnGetShows_Click" />
        </div>
        <br />
        <br />
        </asp:PlaceHolder>
        <br />
        <div class="tTip" id="cloud1" title="Choose a year and pick shows from that year"
            style="font-size: large;">
            <h3>
                Choose a year!</h3>
            <uc:YearSelector id="yearSelector" OnYearSelected="yearSelector_YearSelected" runat="server">
                    </uc:YearSelector>
        </div>
        <br />
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="font-size: larger;">
                    <asp:Repeater ID="rptShows" runat="server" OnItemCommand="rptShows_ItemCommand">
                        <HeaderTemplate>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="padding-bottom: 5px;">
                                <td>
                                    <asp:ImageButton ID="lnkAdd" runat="server" CommandName="ADD" ImageUrl="/images/buttons/AddGreen.JPG"
                                        CommandArgument='<%# (((PhishPond.Concrete.Show)Container.DataItem).ShowId) %>'>
                                    </asp:ImageButton>
                                </td>
                                <td>
                                    <%# (((PhishPond.Concrete.Show)Container.DataItem).GetShowName()) %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rptShows" EventName="ItemCommand" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="hdnBindFrom" runat="server" Visible="false" />
</asp:Content>
