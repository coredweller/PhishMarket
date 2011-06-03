<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyPictures.aspx.cs" Inherits="PhishMarket.MyPhishMarket.MyPictures"
    MasterPageFile="~/Master/Shadowed.Master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="Head" runat="server">

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

            callMyPictureHandler(showId, userId);

            return false;
        });

        //When the Show Pictures button is pressed this fires to show
        //  the pictures for this user if available
        function showPictures() {

            var showClientId = $('#<%= ddlShows.ClientID %>');
            var showId = $(showClientId).val();

            var userClientId = $('#<%= hdnUserId.ClientID %>');
            var userId = $(userClientId).val();

            callMyPictureHandler(showId, userId);

            return false;
        }
    </script>

</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>
            My Pictures
        </h2>
    </div>
    <div id="alertDiv" style="padding: 10px; float: left;">
    </div>
    <div>
        <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">You have successfully
            edited your picture. </asp:PlaceHolder>
        <asp:PlaceHolder ID="phError" runat="server" Visible="false">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phNoTicketStubsError" runat="server" Visible="false">You have no
            show pictures. Please go to <a href='<%= LinkBuilder.AddPhotoLink() %>'></a>to add
            some. </asp:PlaceHolder>
        <asp:PlaceHolder ID="phRemoveError" runat="server" Visible="false">There was an error
            removing the picture from your collection. </asp:PlaceHolder>
        <asp:PlaceHolder ID="phRemoveSuccess" runat="server" Visible="false">You have successfully
            removed the picture from you collecton. </asp:PlaceHolder>
    </div>
    <br />
    <br />
    <div>
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="/images/buttons/AddPicturesFromOthers.gif"
            OnClick="btnAddOther_Click" />
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="/images/buttons/AddNewPicture.gif"
            OnClick="btnAddPicture_Click" />
    </div>
    <br />
    <br />
    <asp:PlaceHolder ID="phAddShow" runat="server" Visible="false">
        <div>
            <h4>
                <a href='<%= LinkBuilder.AddMyShowLink(ShowId) %>'>Click Here to add this show to My
                    Shows so you can add posters here.</a></h4>
        </div>
    </asp:PlaceHolder>
    <div>
        <table>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlTours" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTours_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlShows" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:Button ID="btnShowFromTour" runat="server" Text="Show My Art from Tour"
                        OnClick="btnShowFromTour_Click" />--%>
                </td>
                <td>
                    <button id="btnShowBrih2" onclick="return showPictures();">
                        Show Pictures</button>
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
    <div id="hdnDiv">
        <asp:HiddenField ID="hdnUserId" runat="server" Visible="true" />
    </div>
</asp:Content>
