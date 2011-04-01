<%@ Control Language="C#" AutoEventWireup="true" CodeFile="extensions.ascx.cs" Inherits="YAF.Pages.Admin.extensions" %>
<YAF:PageLinks runat="server" ID="PageLinks" />
<YAF:AdminMenu runat="server" ID="Adminmenu1">
    <asp:Repeater ID="list" runat="server">
        <HeaderTemplate>
            <table class="content" cellspacing="1" cellpadding="0" width="100%">
                <tr>
                    <td class="header1" colspan="2">
                        <asp:Label ID="ExtensionTitle" runat="server" OnLoad="ExtensionTitle_Load">File Extensions</asp:Label></td>
                </tr>
                <tr>
                    <td class="header2" width="90%">
                        File Extensions</td>
                    <td class="header2">
                        &nbsp;</td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="post">
                    <b>*.<%# HtmlEncode(Eval("extension")) %></b></td>
                <td class="post">
                    <asp:LinkButton runat="server" Text="Edit" CommandName="edit" CommandArgument='<%# Eval("extensionId") %>'
                        ID="Linkbutton1">
                    </asp:LinkButton>
                    |
                    <asp:LinkButton runat="server" Text="Delete" OnLoad="Delete_Load" CommandName="delete"
                        CommandArgument='<%# Eval("extensionId") %>' ID="Linkbutton2">
                    </asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td class="footer1" colspan="3">
                    <asp:LinkButton runat="server" Text="Add" CommandName='add' ID="Linkbutton3"></asp:LinkButton>
                    |
                    <asp:LinkButton runat="server" Text="Import from XML" CommandName='import' ID="Linkbutton5"></asp:LinkButton>
                    |
                    <asp:LinkButton runat="server" Text="Export to XML" CommandName='export' ID="Linkbutton4"></asp:LinkButton>
                </td>
            </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</YAF:AdminMenu>
<YAF:SmartScroller ID="SmartScroller1" runat="server" />
