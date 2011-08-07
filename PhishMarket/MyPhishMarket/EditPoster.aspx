<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPoster.aspx.cs" Inherits="PhishMarket.MyPhishMarket.EditPoster"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3>
            Edit This Poster
        </h3>
    </div>
    <div>
        <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">You have successfully
            edited the poster. </asp:PlaceHolder>
        <asp:PlaceHolder ID="phError" runat="server" Visible="false">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </asp:PlaceHolder>
    </div>
    <asp:PlaceHolder ID="phMain" runat="server" Visible="true">
        <div>
            <table>
                <tr>
                    <td>
                        Notes:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="6" Columns="40"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Show:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlShow" runat="server">
                        </asp:DropDownList>
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
                        Total Number of Prints:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTotal" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTotal"
                            ErrorMessage="Numbers only please" ValidationExpression="[0-9]*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Your Print Number:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumber"
                            ErrorMessage="Numbers only please" ValidationExpression="[0-9]*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Width:
                    </td>
                    <td>
                        <asp:TextBox ID="txtWidth" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtWidth"
                            ErrorMessage="Decimals only please" ValidationExpression="^[-+]?[0-9]*\.?[0-9]+$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Length:
                    </td>
                    <td>
                        <asp:TextBox ID="txtLength" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtLength"
                            ErrorMessage="Decimals only please" ValidationExpression="^[-+]?[0-9]*\.?[0-9]+$"></asp:RegularExpressionValidator>
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
                        Status:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Release Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtReleaseDate" runat="server"></asp:TextBox>
                        (Preferred format: MM/DD/YYYY)
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtReleaseDate"
                            ErrorMessage="Dates only please" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
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
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
        </div>
        <br />
        <br />
        <br />
        <br />
        <div>
            <asp:Image runat="server" Height="600" Width="600" ID="imgDisplayFull" />
        </div>
        <asp:HiddenField ID="hdnId" runat="server" Visible="false" />
    </asp:PlaceHolder>
</asp:Content>
