<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YearSelector.ascx.cs"
    Inherits="PhishMarket.Controls.YearSelector" %>
<asp:LinkButton ID="lnk2011" OnCommand="YearClicked" CommandArgument="2011" runat="server"
    Text="2011"></asp:LinkButton>&nbsp;|&nbsp;<asp:LinkButton ID="lnk2010" OnCommand="YearClicked"
        CommandArgument="2010" runat="server" Text="2010"></asp:LinkButton>&nbsp;|&nbsp;<asp:LinkButton
            ID="lnk2009" OnCommand="YearClicked" CommandArgument="2009" runat="server" Text="2009"></asp:LinkButton>