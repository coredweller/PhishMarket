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
    <div>
        <table>
            <tr>
                <td>
                    <uc:YearSelector id="yearSelector" OnYearSelected="yearSelector_YearSelected" runat="server">
                    </uc:YearSelector>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlShows" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
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
    <br />
    <asp:PlaceHolder ID="phAddShow" runat="server" Visible="false">
        <div>
            <h4>
                <asp:LinkButton ID="lnkAddMyShow" runat="server" OnClick="lnkAddMyShow_Click"></asp:LinkButton>
                <br />That will allow you to add pictures to this show.
            </h4>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phAddPicture" runat="server" Visible="true">
        <asp:LinkButton OnClick="btnAddPicture_Click" runat="server" Text="Add a new Picture here!"></asp:LinkButton></asp:PlaceHolder>
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
        <asp:HiddenField ID="hdnShowId" runat="server" Visible="false" />
    </div>
</asp:Content>
