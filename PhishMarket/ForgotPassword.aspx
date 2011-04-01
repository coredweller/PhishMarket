<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs"
    Inherits="PhishMarket.ForgotPassword" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" SubmitButtonText="Get Password"
            SubmitButtonType="Link">
            <MailDefinition From="administrator@PhishMarket.com" Subject="Your new password" />
        </asp:PasswordRecovery>
    </div>
</asp:Content>
