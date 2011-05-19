<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewProfile.aspx.cs" Inherits="PhishMarket.MyPhishMarket.ViewProfile"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2 title="Basic Information about the user." class="tTip" id="cloud3">
            View Profile
        </h2>
    </div>
    <asp:PlaceHolder ID="phNoProfileError" runat="server" Visible="false">This user does
        not have a profile </asp:PlaceHolder>
    <div>
        <table>
        <tr>
            <td>
                User Name:
            </td>
            <td>
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
            </td>
        </tr>
            <tr>
                <td>
                    Name:
                </td>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Email:
                </td>
                <td>
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Favorite Album:
                </td>
                <td>
                    <asp:Label ID="lblFavoriteAlbum" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Favorite Tour:
                </td>
                <td>
                    <asp:Label ID="lblFavoriteTour" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Favorite Live Show:
                </td>
                <td>
                    <asp:Label ID="lblFavoriteLiveShow" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Favorite Studio Song:
                </td>
                <td>
                    <asp:Label ID="lblFavoriteStudioSong" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <h2 title="The shows that this user has attended and their rating. Click the review to see it in full." class="tTip" id="cloud4">
        Shows Attended</h2>
    <%-- This section has a KeyValuePair of Show and MyShow so KEY = SHOW and VALUE = MYSHOW --%>
    <asp:Repeater ID="rptMyShows" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <strong>
                        <%# Eval("Key.VenueName") %>
                        -
                        <%# FormatDate((DateTime?)Eval("Key.ShowDate"))%></strong>
                    <ajaxToolkit:Rating ID="ajaxSongRating" runat="server" CurrentRating='<%# DetermineRating((int?)Eval("Value.Rating")) %>'
                        MaxRating="5" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                        ReadOnly="true" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" />
                </td>
            </tr>
            <asp:PlaceHolder ID="phNotes" runat="server" Visible='<%# !string.IsNullOrEmpty((string)Eval("Value.Notes")) %>'>
                <tr>
                    <td colspan="2">
                        &nbsp;&nbsp;Review: <a href='<%# LinkBuilder.ViewFullShowReviewLink((Guid)Eval("Value.MyShowId")) %>'>
                            <%# ShortDescription((string)Eval("Value.Notes"), 300) %></a>
                    </td>
                </tr>
            </asp:PlaceHolder>
        </ItemTemplate>
        <FooterTemplate>
            </table></FooterTemplate>
    </asp:Repeater>
    <br />
    <br />
    <br />
    <div>
        <ajaxToolkit:Accordion ID="ajaxAccordion" runat="server" FadeTransitions="true" TransitionDuration="250"
            FramesPerSecond="40" RequireOpenedPane="false" SelectedIndex="0" HeaderCssClass="accordionHeader"
            ContentCssClass="accordionContent">
            <Panes>
                <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server">
                    <Header>
                       <h2> POSTERS</h2></Header>
                    <Content>
                        <asp:PlaceHolder ID="phMain" runat="server" Visible="true">
                            <div>
                                <asp:Repeater ID="rptPoster" runat="server">
                                    <HeaderTemplate>
                                        <table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                            
                                                <asp:Image ID="imgArt1" runat="server" ImageUrl='<%# LinkBuilder.GetImageLink( (((PhishPond.Concrete.Poster)Container.DataItem).PhotoId) ) %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                by
                                                <%# (((PhishPond.Concrete.Poster)Container.DataItem).Creator) %>&nbsp;&nbsp;<%# (((PhishPond.Concrete.Poster)Container.DataItem).Number) %>\<%# (((PhishPond.Concrete.Poster)Container.DataItem).Total) %>&nbsp;&nbsp;<%# (((PhishPond.Concrete.Poster)Container.DataItem).Length) %>&nbsp;x&nbsp;<%# (((PhishPond.Concrete.Poster)Container.DataItem).Width) %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%# (((PhishPond.Concrete.Poster)Container.DataItem).Notes)%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table></FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </asp:PlaceHolder>
                    </Content>
                </ajaxToolkit:AccordionPane>
               <%-- <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server">
                    <Header>
                        <h2>TICKET STUBS</h2></Header>
                    <Content>
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="true">
                            <div>
                                <asp:Repeater ID="rptTicketStubs" runat="server">
                                    <HeaderTemplate>
                                        <table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                            
                                                <asp:Image ID="imgArt2" runat="server" ImageUrl='<%# LinkBuilder.GetImageLink( (((PhishPond.Concrete.TicketStub)Container.DataItem).PhotoId) ) %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%# (((PhishPond.Concrete.TicketStub)Container.DataItem).Notes)%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table></FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </asp:PlaceHolder>
                    </Content>
                </ajaxToolkit:AccordionPane>--%>
                <ajaxToolkit:AccordionPane ID="AccordionPane3" runat="server">
                    <Header>
                        <h2>PICTURES</h2></Header>
                    <Content>
                        <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible="true">
                            <div>
                                <asp:Repeater ID="rptArt" runat="server">
                                    <HeaderTemplate>
                                        <table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                            
                                                <asp:Image ID="imgArt3" runat="server" ImageUrl='<%# LinkBuilder.GetImageLink( (((PhishPond.Concrete.Art)Container.DataItem).PhotoId) ) %>' />
                                            </td>
                                        </tr>
                             
                                        <tr>
                                            <td>
                                                <%# (((PhishPond.Concrete.Art)Container.DataItem).Notes)%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table></FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </asp:PlaceHolder>
                    </Content>
                </ajaxToolkit:AccordionPane>
            </Panes>
        </ajaxToolkit:Accordion>
    </div>
</asp:Content>
