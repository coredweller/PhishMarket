<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyPosters.aspx.cs" Inherits="PhishMarket.MyPhishMarket.MyPosters"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript" src="/javascript/galleria/galleria-1.2.2.min.js"></script>

    <script type="text/javascript">

        //Absolutely needed for Galleria to work on this page
        Galleria.loadTheme('/javascript/galleria/classic/galleria.classic.min.js');

        //When the page loads, if there is a showId in the URL
        //  this function will load the pictures for this user if available
        $(function() {
            var userClientId = $('#<%= hdnUserId.ClientID %>');
            var userId = $(userClientId).val();

            var showId = $.getUrlVar('showId');

            callMyPostersHandler(showId, userId);

            return false;
        });

        //When the Show Pictures button is pressed this fires to show
        //  the pictures for this user if available
        function showPosters() {

            var showClientId = $('#<%= ddlShows.ClientID %>');
            var showId = $(showClientId).val();

            var userClientId = $('#<%= hdnUserId.ClientID %>');
            var userId = $(userClientId).val();

            callMyPostersHandler(showId, userId);

            return false;
        }
    </script>

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>
            My Posters
        </h2>
    </div>
    <div>
        <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">You have successfully
            edited your poster. </asp:PlaceHolder>
        <asp:PlaceHolder ID="phError" runat="server" Visible="false">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phNoTicketStubsError" runat="server" Visible="false">You have no
            posters. Please go to <a href='<%= LinkBuilder.AddPhotoLink() %>'></a>to add some.
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phRemoveError" runat="server" Visible="false">
            There was an error removing the poster.
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phRemoveSuccess" runat="server" Visible="false">
            You have successfully removed the poster from you collecton.
        </asp:PlaceHolder>
        
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
        <table>
            <%--<tr>
                <td>
                    
                </td>
                <td>
                    <asp:ImageButton ID="btnAddOther" runat="server" ImageUrl="/images/buttons/AddPostersFromOthers.gif" OnClick="btnAddOther_Click" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:ImageButton ID="btnAddPicture" runat="server" ImageUrl="/images/buttons/AddNewPoster.gif" OnClick="btnAddPicture_Click" />
                    
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
                    <%--<asp:Button ID="btnShowFromTour" runat="server" Text="Show My Posters from Tour"
                        OnClick="btnShowFromTour_Click" />--%>
                </td>
                <td>
                    <button id="btnShowBrih2" onclick="return showPosters();">
                        Show Posters</button>
                </td>
            </tr>
        </table>
    </div>
     <h4>
        Tip 1: Click the thumbnails or arrows to change pictures.
        <br />
        Tip 2: Click the large image to go to a page to delete it.</h4>
    <div id="gallery">
    <%--<img src="/../images/Shows/coretest2-634195715440294949.jpg" alt="Minibri" title="Title of all titles" />--%>
        <%--<img src="/images/Shows/coretest2-634195715440294949.jpg" alt="blah" />--%>
    </div>
    <br />
    <br />
    <br />
    <p>
        NOTE: If using IE you may need to clear the cache if you add or delete a picture
        to see the most accurate set of pictures. Or you could use Firefox ;)
    </p>
    
    <%--<asp:PlaceHolder ID="phMain" runat="server" Visible="true">
        <div>
            <asp:Repeater ID="rptPoster" runat="server" OnItemCommand="rptPoster_ItemCommand">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Image ID="imgArt" runat="server" ImageUrl='<%# LinkBuilder.GetImageLink((((PhishPond.Concrete.Poster)Container.DataItem).PhotoId.Value)) %>' />
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkRemove" CommandArgument='<%# (((PhishPond.Concrete.Poster)Container.DataItem).PosterId) %>' CommandName="REMOVE" runat="server" Text="Remove"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            by
                            <%# (((PhishPond.Concrete.Poster)Container.DataItem).Creator) %>&nbsp;&nbsp;<%# (((PhishPond.Concrete.Poster)Container.DataItem).Number)%>\<%# (((PhishPond.Concrete.Poster)Container.DataItem).Total)%>&nbsp;&nbsp;<%# (((PhishPond.Concrete.Poster)Container.DataItem).Length)%>&nbsp;x&nbsp;<%# (((PhishPond.Concrete.Poster)Container.DataItem).Width) %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# (((PhishPond.Concrete.Poster)Container.DataItem).Notes) %>
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
    </asp:PlaceHolder>--%>
    <div id="hdnDiv">
        <asp:HiddenField ID="hdnUserId" runat="server" Visible="true" />
    </div>
</asp:Content>
