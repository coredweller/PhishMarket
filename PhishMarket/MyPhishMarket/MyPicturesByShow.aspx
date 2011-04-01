<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyPicturesByShow.aspx.cs"
    Inherits="PhishMarket.MyPhishMarket.MyPicturesByShow" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <a href='<%= LinkBuilder.AddPhotoLink(Request.Url.ToString()) %>'>Add Photo</a><br />
    <br />
    <div>
        <%--<ajaxToolkit:SlideShowExtender ID="PhishShowPhotoSlideShow" runat="server" TargetControlID="imgPhotos"
            SlideShowServiceMethod="GetSlides"
            AutoPlay="true" NextButtonID="nextButton" PlayButtonText="Play" PreviousButtonID="prevButton"
            PlayButtonID="playButton" Loop="true">
        </ajaxToolkit:SlideShowExtender>--%>
        <ajaxToolkit:slideshowextender id="SlideShowExtender1" runat="server" targetcontrolid="imgPhotos"
            slideshowservicemethod="GetSlides" SlideShowServicePath="/SlideService.asmx"
            usecontextkey="true" autoplay="true" nextbuttonid="btnNext" playbuttontext="Play"
            stopbuttontext="Stop" previousbuttonid="btnPrev" playbuttonid="btnPlay" loop="true"
            imagetitlelabelid="lblTitle" imagedescriptionlabelid="lblDescription" />
        <br />
        <br />
        <asp:Image ID="imgPhotos" runat="server" />
        <%--Height="300" /> Style="width: auto; border: solid 1px #000000;" />--%>
        <br />
        <br />
        <center><asp:Label ID="lblTitle" runat=server></asp:Label><br />
    <asp:Label ID="lblDescription" runat=server></asp:Label><br /></center>
    <center>
    <asp:Button ID="btnPrev" Text="Prev" runat="server" />
    <asp:Button ID="btnPlay" Text="Play" runat="server" />
    <asp:Button ID="btnNext" Text="Next" runat="server" />
    </center>
    </div>
    <br />
    <br />
    <div>
        <asp:Repeater ID="rptPhotos" runat="server">
            <HeaderTemplate>
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# LinkBuilder.GetImageLink((((PhishPond.Concrete.Photo)Container.DataItem).PhotoId)) %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
