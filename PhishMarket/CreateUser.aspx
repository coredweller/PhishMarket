<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="PhishMarket.CreateUser"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:CreateUserWizard runat="server" OnContinueButtonClick="createControl_ContinueButtonClick" ID="createControl" OnCreatedUser="createControl_CreatedUser">
    </asp:CreateUserWizard>
</asp:Content>
