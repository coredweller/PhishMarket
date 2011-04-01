<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSongsToSet.aspx.cs"
    Inherits="PhishMarket.TourPages.AddSongsToSet" MasterPageFile="~/Master/Shadowed.Master" %>

<%@ Register TagPrefix="uc" TagName="CreateSetControl" Src="~/Controls/CreateSetControl.ascx" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <uc:CreateSetControl ID="createSetControl" runat="server" />
    </div>
</asp:Content>
