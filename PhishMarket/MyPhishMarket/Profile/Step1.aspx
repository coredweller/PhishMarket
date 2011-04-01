<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step1.aspx.cs" Inherits="PhishMarket.MyPhishMarket.ProfilePages.Step1"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>
            Step 1: Basic</h2>
    </div>
    <br />
    <div id="alertDiv" style="padding: 10px; float: left;">
    </div>
    <div>
        <table>
            <tr>
                <td>
                    Name:
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Email:
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Favorite Album:
                </td>
                <td>
                    <asp:DropDownList ID="ddlFavoriteAlbums" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Favorite Studio Song:
                </td>
                <td>
                    <asp:DropDownList ID="ddlFavoriteStudioSong" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td colspan="2">
                    <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="/images/buttons/greySaveButton.jpg"
                        OnClick="btnSubmit_Click" />
                    <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />--%>
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <asp:ImageButton ID="btnPrevious" runat="server" ImageUrl="/images/buttons/previousWhiteButton.gif"
                        OnClick="btnPrevious_Click" />
                    <%--<asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="btnPrevious_Click" />--%>
                </td>
                <td>
                    <asp:ImageButton ID="btnNext" runat="server" ImageUrl="/images/buttons/nextWhiteButton.gif"
                        OnClick="btnNext_Click" />
                    <%--<asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
