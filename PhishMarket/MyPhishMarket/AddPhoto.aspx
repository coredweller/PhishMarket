<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPhoto.aspx.cs" Inherits="PhishMarket.MyPhishMarket.AddPhoto"
    MasterPageFile="~/Master/Shadowed.Master" %>

<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Add Photo</h2>
    <div id="alertDiv" style="padding: 10px; float: left;">
    </div>
    <div>
        <asp:HyperLink ID="lnkReturn" runat="server" Text="Return to previous page"></asp:HyperLink>
    </div>
    <div>
        <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">You have successfully
            added a photo. </asp:PlaceHolder>
        <asp:PlaceHolder ID="phError" runat="server" Visible="false">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phNoImageSelectedError" runat="server" Visible="false">You must
            select an image to continue.</asp:PlaceHolder>
    </div>
    <br />
    <div>
        <%--<Upload:InputFile ID="uploadedFile" runat="server" />--%>
        <Upload:MultiFile ID="uploadedFiles" UseFlashIfAvailable="true" runat="server" />
        <Upload:ProgressBar ID="progressBarId" runat="server" Inline="true" />
    </div>
    <br />
    <div>
        <table>
            <%--<tr>
                <td>
                    Picture:
                </td>
                <td>
                    <%--<asp:FileUpload ID="fuPicture" runat="server" />
                    
                </td>
            </tr>--%>
            <tr>
                <td colspan="2">
                    <%--<asp:RequiredFieldValidator ID="rfvPicture" runat="server" ControlToValidate="fuPicture"
                        ErrorMessage="You must choose a picture" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <%--<tr>
                <td>
                    Photo Type:
                </td>
                <td>
                    <asp:DropDownList ID="ddlPhotoType" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td>
                    Nick Name:
                </td>
                <td>
                    <asp:TextBox ID="txtNickName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server"
                        ControlToValidate="txtNickName" ErrorMessage="You must choose a nick name"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Notes:
                </td>
                <td>
                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="6" Columns="40"
                        MaxLength="200"></asp:TextBox>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="rgConclusionValidator2" ControlToValidate="txtNotes"
                        ErrorMessage="Notes cannot exceed 200 characters" ValidationExpression="^[\s\S]{0,200}$"
                        runat="server" SetFocusOnError="true" />
                </td>
            </tr>
            <tr>
                <td>
                    Quality:
                </td>
                <td>
                    <asp:DropDownList ID="ddlQuality" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <%--<tr>
                <td>
                    Choose a tour
                </td>
                <td>
                    <asp:DropDownList ID="ddlTour" runat="server" Width="225px">
                    </asp:DropDownList>
                </td>
                <td rowspan="2">
                    <asp:Button ID="btnChooseTour" runat="server" CausesValidation="False" Text="Choose"
                        OnClick="btnChooseTour_Click" />
                </td>
                <td>
                    <asp:DropDownList ID="ddlShow" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <%--<tr>
                <td>
                    OR Enter Date
                </td>
                <td>
                    <asp:TextBox ID="txtShowDate" runat="server" Width="222px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    (Preferred format: MM/DD/YYYY)
                </td>
            </tr>--%>
            <%--<tr>
                <td>
                    Show:
                </td>
                <td>
                    <asp:DropDownList ID="ddlShow" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <%--<tr>
                <td>
                    Type:
                </td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server">
                        <asp:ListItem Text="Poster" Value="Poster"></asp:ListItem>
                        <asp:ListItem Text="Art" Value="Art"></asp:ListItem>
                        <asp:ListItem Text="Picture" Value="Picture"></asp:ListItem>
                        <asp:ListItem Text="Ticket Stub" Value="Ticket Stub"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>--%>
        </table>
    </div>
    <asp:PlaceHolder ID="phPosterExtras" runat="server" Visible="false">
        <table>
            <tr>
                <td>
                    Length:
                </td>
                <td>
                    <asp:TextBox ID="txtLength" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Width:
                </td>
                <td>
                    <asp:TextBox ID="txtWidth" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Creator:
                </td>
                <td>
                    <asp:TextBox ID="txtCreator" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Total Number:
                </td>
                <td>
                    <asp:TextBox ID="txtTotal" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Your Print Number:
                </td>
                <td>
                    <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Technique:
                </td>
                <td>
                    <asp:TextBox ID="txtTechnique" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Title:
                </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <div>
    </div>
    <div>
        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
        <asp:HiddenField ID="hdnShowId" Visible="false" runat="server" />
        <asp:HiddenField ID="hdnPhotoType" Visible="false" runat="server" />
    </div>
    <br />
    <br />
    <br />
    <br />
    <div>
        <asp:Image runat="server" ID="imgDisplayThumb" />
        <br />
        <asp:Image runat="server" ID="imgDisplayFull" />
    </div>
    <br />
    <br />
    <br />
    <asp:PlaceHolder ID="phEditPoster" runat="server" Visible="false">
        <asp:HyperLink ID="lnkEditPoster" runat="server" Text="Edit Poster"></asp:HyperLink>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phEditTicketStub" runat="server" Visible="false">
        <asp:HyperLink ID="lnkEditTicketStub" runat="server" Text="Edit Ticket Stub"></asp:HyperLink>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phEditArt" runat="server" Visible="false">
        <asp:HyperLink ID="lnkEditArt" runat="server" Text="Edit Art"></asp:HyperLink>
    </asp:PlaceHolder>
</asp:Content>
