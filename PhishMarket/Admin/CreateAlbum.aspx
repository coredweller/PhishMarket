<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAlbum.aspx.cs" Inherits="PhishMarket.Admin.CreateAlbum"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>
            Create Album</h2>
    </div>
    <br />
    <div id="alertDiv" style="padding: 10px; float: left;">
    </div>
    <div>
        <table>
            <tr>
                <td>
                    Album Name:
                </td>
                <td>
                    <asp:TextBox ID="txtAlbumName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Year Released:
                </td>
                <td>
                    <asp:DropDownList ID="ddlYearReleased" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
    </div>
</asp:Content>
