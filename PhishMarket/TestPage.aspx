<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="PhishMarket.TestPage"
    MasterPageFile="~/Master/Shadowed.Master" %>

<%--<%@ Register TagPrefix="uc" TagName="YearSelector" Src="/Controls/YearSelector.ascx" %>--%>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div id="alertDiv" style="padding: 10px; float: left;">
    </div>
    <uc:YearSelector id="yearSelector" OnYearSelected="yearSelector_YearSelected" runat="server">
    </uc:YearSelector>
</asp:Content>
