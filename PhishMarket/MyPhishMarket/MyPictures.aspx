<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyPictures.aspx.cs" Inherits="PhishMarket.MyPhishMarket.MyPictures"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript" src="/../../javascript/galleria/galleria-1.2.2.min.js"></script>

    <script type="text/javascript">

        Galleria.loadTheme('/../../javascript/galleria/classic/galleria.classic.min.js');


//        $('#gallery').galleria({

//            extend: function(options) {

//                Galleria.log(this) // the gallery instance
//                Galleria.log(options) // the gallery options

//                // listen to when an image is shown
//                this.bind('image', function(e) {

//                    Galleria.log(e) // the event object may contain custom objects, in this case the main image
//                    Galleria.log(e.imageTarget) // the current image

//                    // lets make galleria open a lightbox when clicking the main image:
//                    $(e.imageTarget).click(this.proxy(function() {
//                        this.openLightbox();
//                    }));
//                });
//            }
//        });

//        $('#gallery').galleria({

//            extend: function(options) {

//                $('#lnkRemove').click(function() {
//                    alert(this);
//                });

//            }
//        });

        $(function() {
            var userClientId = $('#<%= hdnUserId.ClientID %>');
            var userId = $(userClientId).val();

            var showId = $.getUrlVar('showId');

            callMyPictureHandler(showId, userId);

            return false;
        });

        function showPictures() {

            var showClientId = ('#<%= ddlShows.ClientID %>');
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
    <div>
        <table>
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
    <div id="gallery">
    </div>
    
    <asp:PlaceHolder ID="phMain" runat="server" Visible="true">
        <div>
            <asp:Repeater ID="rptArt" runat="server" OnItemCommand="rptArt_ItemCommand">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Image ID="imgArt" runat="server" ImageUrl='<%# LinkBuilder.GetImageLink((((PhishPond.Concrete.Art)Container.DataItem).PhotoId)) %>' />
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkRemove" CommandArgument='<%# (((PhishPond.Concrete.Art)Container.DataItem).ArtId) %>'
                                CommandName="REMOVE" runat="server" Text="Remove"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# (((PhishPond.Concrete.Art)Container.DataItem).Notes) %>
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
    <div id="hdnDiv">
        <asp:HiddenField ID="hdnUserId" runat="server" Visible="true" />
    </div>
</asp:Content>
