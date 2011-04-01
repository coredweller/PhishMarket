<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatePost.aspx.cs" Inherits="PhishMarket.Admin.CreatePost"
    MasterPageFile="~/Master/Shadowed.Master" ValidateRequest="false" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create Post</h2>
    <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">You have successfully
        added a post. </asp:PlaceHolder>
    <asp:PlaceHolder ID="phError" runat="server" Visible="false">There has been an error.
        Please try again later. </asp:PlaceHolder>
    <table>
        <tr>
            <td>
                Title:
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                TitleUrl:
            </td>
            <td>
                <asp:TextBox ID="txtTitleUrl" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                PostedDate:
            </td>
            <td>
                <asp:TextBox ID="txtPostedDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                PostedBy:
            </td>
            <td>
                <asp:TextBox ID="txtPostedBy" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Entry:
            </td>
            <td>
                <asp:TextBox ID="txtEntry" TextMode="MultiLine" Rows="5" Columns="22" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
