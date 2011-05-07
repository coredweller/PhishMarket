<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PhishMarket.Login"
    MasterPageFile="~/Master/Shadowed.Master" %>

<%@ Register TagPrefix="uc" TagName="Login" Src="~/Controls/Login.ascx" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<br /><br />
    <uc:Login ID="Login1" runat="Server"></uc:Login>
    <br /><br /><br />
</asp:Content>
