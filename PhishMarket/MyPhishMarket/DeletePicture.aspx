<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeletePicture.aspx.cs"
    Inherits="PhishMarket.MyPhishMarket.DeletePicture" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <br />
        <br />
        <h2>
            Delete Picture?
        </h2>
        <br />
        <asp:Image ID="imgImage" runat="server" />
        <br />
        <center>
            <asp:Button runat="server" ID="btnYes" Text="YES" OnClick="btnYes_Click" />
            <asp:Button runat="server" ID="btnNo" Text="NO" OnClick="btnNo_Click" />
        </center>
    </div>
    <asp:HiddenField ID="hdnId" runat="server" />
</asp:Content>
