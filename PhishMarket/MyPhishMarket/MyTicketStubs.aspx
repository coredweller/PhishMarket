<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyTicketStubs.aspx.cs"
    Inherits="PhishMarket.MyPhishMarket.MyTicketStubs" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3>
            My Ticket Stubs
        </h3>
    </div>
    <div>
        <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">You have successfully
            edited the ticket stub. </asp:PlaceHolder>
        <asp:PlaceHolder ID="phError" runat="server" Visible="false">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phNoTicketStubsError" runat="server" Visible="false">You have no
            ticket stubs. Please go to <a href='<%= LinkBuilder.AddPhotoLink() %>'></a>to add
            some. </asp:PlaceHolder>
        <asp:PlaceHolder ID="phRemoveError" runat="server" Visible="false">There was an error
            removing the ticket stub. </asp:PlaceHolder>
        <asp:PlaceHolder ID="phRemoveSuccess" runat="server" Visible="false">You have successfully
            removed the ticket stub from you collecton. </asp:PlaceHolder>
    </div>
    <br />
    <br />
    <div>
        <asp:ImageButton ID="btnAddOther" runat="server" ImageUrl="/images/buttons/AddPicturesFromOthers.gif"
            OnClick="btnAddOther_Click" />
        <asp:ImageButton ID="btnAddPicture" runat="server" ImageUrl="/images/buttons/AddNewPicture.gif"
            OnClick="btnAddPicture_Click" />
    </div>
    <br />
    <br />
    <div>
        <table>
            <%--<tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnAddOther" runat="server" Text="Add Ticket Stubs from others for this show"
                        OnClick="btnAddOther_Click" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnAddPicture" runat="server" Text="Add New Ticket Stub for this show"
                        OnClick="btnAddPicture_Click" />
                </td>
            </tr>--%>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlTours" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTours_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <%--<td><asp:Button ID="btnGetShows" runat="server" Text="Get Shows" OnClick="btnGetShows_Click" /></td>--%>
                <td>
                    <asp:DropDownList ID="ddlShows" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:Button ID="btnShowFromTour" runat="server" Text="Show My Ticket Stubs from Tour"
                        OnClick="btnShowFromTour_Click" />--%>
                </td>
                <td>
                    <asp:Button ID="btnShowFromShow" runat="server" Text="Show My Ticket Stubs from Show"
                        OnClick="btnShowFromShow_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:PlaceHolder ID="phMain" runat="server" Visible="true">
        <div>
            <asp:Repeater ID="rptTicketStubs" runat="server" OnItemCommand="rptTicketStubs_ItemCommand">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Image ID="imgArt" runat="server" ImageUrl='<%# LinkBuilder.GetImageLink((((PhishPond.Concrete.TicketStub)Container.DataItem).PhotoId)) %>' />
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkRemove" CommandArgument='<%# (((PhishPond.Concrete.TicketStub)Container.DataItem).TicketStubId) %>'
                                CommandName="REMOVE" runat="server" Text="Remove"></asp:LinkButton>
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                            by
                            <%# (((PhishPond.Concrete.TicketStub)Container.DataItem).Creator) %>&nbsp;&nbsp;<%# (((PhishPond.Concrete.TicketStub)Container.DataItem).Number) %>\<%# (((PhishPond.Concrete.TicketStub)Container.DataItem).Total) %>&nbsp;&nbsp;<%# (((PhishPond.Concrete.TicketStub)Container.DataItem).Length) %>&nbsp;x&nbsp;<%# (((PhishPond.Concrete.TicketStub)Container.DataItem).Width) %>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <%# (((PhishPond.Concrete.TicketStub)Container.DataItem).Notes) %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
            <br />
            <asp:PlaceHolder ID="phNoImages" runat="server" Visible="false">
                <h3>
                    You have no images for this show!
                </h3>
                <br />
                <h4>
                    Add a new picture of your own or pick one someone else has added.
                    <br />
                    Use the buttons above!</h4>
            </asp:PlaceHolder>
        </div>
    </asp:PlaceHolder>
</asp:Content>
