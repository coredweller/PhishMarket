<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateTour.aspx.cs" Inherits="PhishMarket.Admin.CreateTour"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create Tour</h2>
    <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">You have successfully
        added a tour. </asp:PlaceHolder>
    <asp:PlaceHolder ID="phError" runat="server" Visible="false">There has been an error.
        Please try again later. </asp:PlaceHolder>
    <table>
        <tr>
            <td>
                Tour Name:
            </td>
            <td>
                <asp:TextBox ID="txtTourName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Start Date:
            </td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
            </td>
            <td>
                (Preferred format: MM/DD/YYYY)
            </td>
        </tr>
        <tr>
            <td>
                End Date:
            </td>
            <td>
                <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
            </td>
            <td>
                (Preferred format: MM/DD/YYYY)
            </td>
        </tr>
        <tr>
            <td>
                Official:
            </td>
            <td>
                <asp:CheckBox ID="chkOfficial" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
