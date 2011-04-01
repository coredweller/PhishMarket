<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSetsToShow.aspx.cs"
    Inherits="PhishMarket.Admin.AddSetsToShow" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:HyperLink ID="lnkReturn" runat="server" Text="Return to last page"></asp:HyperLink>
    <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">You have successfully
        added a show. </asp:PlaceHolder>
    <asp:PlaceHolder ID="phError" runat="server" Visible="false">There has been an error.
        Please try again later. </asp:PlaceHolder>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <h2>
                    List of sets in your show</h2>
                <div>
                    <asp:Repeater ID="rptSets" runat="server" OnItemCommand="rptSets_ItemCommand">
                        <HeaderTemplate>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# (((PhishPond.Concrete.Set)Container.DataItem).SetNumber) %>
                                </td>
                                <td>
                                    <%# (((PhishPond.Concrete.Set)Container.DataItem).SetId) %>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkChange" runat="server" CommandName="CHANGE" CommandArgument='<%# (((PhishPond.Concrete.Set)Container.DataItem).SetId) %>'
                                        Text="Change Songs"></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:PlaceHolder ID="phUp" runat="server" Visible='<%# (((PhishPond.Concrete.Set)Container.DataItem).SetNumber) != 1 %>'>
                                        <asp:LinkButton ID="lnkUp" runat="server" CommandName="UP" CommandArgument='<%# (((PhishPond.Concrete.Set)Container.DataItem).SetId) %>'
                                            Text="Up"></asp:LinkButton>
                                    </asp:PlaceHolder>
                                </td>
                                <td>
                                    <asp:PlaceHolder ID="phDown" runat="server" Visible='<%# (((PhishPond.Concrete.Set)Container.DataItem).SetNumber) != FinalSetNumber %>'>
                                        <asp:LinkButton ID="lnkDown" runat="server" CommandName="DOWN" CommandArgument='<%# (((PhishPond.Concrete.Set)Container.DataItem).SetId) %>'
                                            Text="Down"></asp:LinkButton>
                                    </asp:PlaceHolder>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkRemove" runat="server" CommandName="REMOVE" CommandArgument='<%# (((PhishPond.Concrete.Set)Container.DataItem).SetId) %>'
                                        Text="Remove"></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </div>
                <br />
                <br />
                <br />
                <div>
                    <asp:LinkButton ID="lnkAddSetToShow" runat="server" Text="Add a Set to the Show" OnClick="lnkAddSetToShow_Click"></asp:LinkButton>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rptSets" EventName="ItemCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdnId" runat="server" Visible="false" />
</asp:Content>
