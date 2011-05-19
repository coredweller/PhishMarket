<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindForShow.aspx.cs" Inherits="PhishMarket.MyPhishMarket.FindForShow"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div>
    <asp:PlaceHolder ID="phAlreadyAddedError" runat="server" Visible="false">
        This is already in your collection.
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phError" runat="server" Visible="false">
        There was an error with your request.  Please try again.
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phAddSuccess" runat="server" Visible="false">
        You have successfully added to your collection
    </asp:PlaceHolder>
</div>
    <ajaxToolkit:Accordion ID="ajaxAccordian" runat="server" FadeTransitions="true" TransitionDuration="250"
        FramesPerSecond="40" RequireOpenedPane="false" SelectedIndex="0" Width="700" HeaderCssClass="accordionHeader"
        ContentCssClass="accordionContent">
        <Panes>
            <ajaxToolkit:AccordionPane runat="server">
                <Header>
                    POSTERS</Header>
                <Content>
                <a href='<%= LinkBuilder.MyPostersLink(ShowIdStr) %>'>Go back to My Posters</a>
                <br />
                <br />
                <asp:PlaceHolder ID="phNoPosters" runat="server" Visible="false">
                    No one has added a poster for this show yet.  Go here to <asp:HyperLink ID="lnkNoPosters" runat="server" Text="Add your own"></asp:HyperLink>
                </asp:PlaceHolder>
                    <asp:Repeater ID="rptPosters" runat="server" OnItemCommand="rptPosters_ItemCommand">
                        <HeaderTemplate>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                        
                                <tr>
                                    <td>
                                        <asp:Image ID="imgArt" runat="server" ImageUrl='<%# LinkBuilder.GetImageLink((Guid)Eval("Poster.PhotoId")) %>' />
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkAddPoster" runat="server" CommandArgument='<%# Eval("Poster.PosterId") %>'
                                            CommandName="ADDPOSTER" Text="Add Poster"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        by
                                        <%# Eval("Poster.Creator") %>&nbsp;&nbsp;<%# Eval("Poster.Number")%>\<%# Eval("Poster.Total")%>&nbsp;&nbsp;<%# Eval("Poster.Length")%>&nbsp;x&nbsp;<%# Eval("Poster.Width")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%# Eval("Poster.Notes")%>
                                    </td>
                                </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </Content>
            </ajaxToolkit:AccordionPane>
            <%--<ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server">
                <Header>
                    TICKET STUBS</Header>
                <Content>
                <a href='<%= LinkBuilder.MyTicketStubsLink(ShowIdStr) %>'>Go back to My Ticket Stubs</a>
                <br />
                <br />
                
                <asp:PlaceHolder ID="phNoTicketStubs" runat="server" Visible="false">
                    No one has added a ticket stub for this show yet.  Go here to <asp:HyperLink ID="lnkNoTicketStubs" runat="server" Text="Add your own"></asp:HyperLink>
                </asp:PlaceHolder>
                    <asp:Repeater ID="rptTicketStubs" runat="server" OnItemCommand="rptTicketStubs_ItemCommand">
                        <HeaderTemplate>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Image ID="imgArt" runat="server" ImageUrl='<%# LinkBuilder.GetImageLink((Guid)Eval("TicketStub.PhotoId")) %>' />
                                </td>
                                <td>
                                        <asp:LinkButton ID="lnkAddTicketStub" runat="server" CommandArgument='<%# Eval("TicketStub.TicketStubId") %>'
                                            CommandName="ADDTICKETSTUB" Text="Add Ticket Stub"></asp:LinkButton>
                                    </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <%# Eval("TicketStub.Notes")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </Content>
            </ajaxToolkit:AccordionPane>--%>
            <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server">
                <Header>
                    PICTURES</Header>
                <Content>
                <a href='<%= LinkBuilder.MyPicturesLink(ShowIdStr) %>'>Go back to My Pictures</a>
                <br />
                <br />
                
                <asp:PlaceHolder ID="phNoArt" runat="server" Visible="false">
                    No one has added pictures for this show yet.  Go here to <asp:HyperLink ID="lnkNoArt" runat="server" Text="Add your own"></asp:HyperLink>
                </asp:PlaceHolder>
                    <asp:Repeater ID="rptArt" runat="server" OnItemCommand="rptArt_ItemCommand">
                        <HeaderTemplate>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Image ID="imgArt" runat="server" ImageUrl='<%# LinkBuilder.GetImageLink((Guid)Eval("Art.PhotoId")) %>' />
                                </td>
                                <td>
                                        <asp:LinkButton ID="lnkAddArt" runat="server" CommandArgument='<%# Eval("Art.ArtId") %>'
                                            CommandName="ADDART" Text="Add Picture"></asp:LinkButton>
                                    </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <%# Eval("Art.Notes")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </Content>
            </ajaxToolkit:AccordionPane>
        </Panes>
    </ajaxToolkit:Accordion>
    <asp:HiddenField runat="server" Visible="false" ID="hdnShowId" />
</asp:Content>
