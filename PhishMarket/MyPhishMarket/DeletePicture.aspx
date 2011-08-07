<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeletePicture.aspx.cs"
    Inherits="PhishMarket.MyPhishMarket.DeletePicture" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Image ID="imgImage" Height="600" Width="600" runat="server" />
    <br />
    <br />
    <center>
        <asp:Button ID="btnEditPicture" runat="server" Text="EDIT DETAILS" OnClick="btnEditPicture_Click" />
        <asp:Button runat="server" ID="btnYes" Text="DELETE" OnClick="btnYes_Click" />
        <br />
        <asp:Button runat="server" ID="btnNo" Text="CANCEL" OnClick="btnNo_Click" />
    </center>
    
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnShowId" runat="server" />
</asp:Content>
