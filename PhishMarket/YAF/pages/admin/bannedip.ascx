<%@ Control Language="c#" CodeFile="bannedip.ascx.cs" AutoEventWireup="True" Inherits="YAF.Pages.Admin.bannedip" %>
<YAF:PageLinks runat="server" ID="PageLinks" />
<YAF:AdminMenu runat="server">
	<asp:Repeater ID="list" runat="server" OnItemCommand="list_ItemCommand">
		<HeaderTemplate>
			<table class="content" cellspacing="1" cellpadding="0" width="100%">
				<tr>
					<td class="header1" colspan="3">
						Banned IP Addresses</td>
				</tr>
				<tr>
					<td class="header2">
						Mask</td>
					<td class="header2">
						Since</td>
					<td class="header2">
						&nbsp;</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td class="post">
					<%# Eval("Mask") %>
				</td>
				<td class="post">
					<%# YafDateTime.FormatDateTime(Eval("Since")) %>
				</td>
				<td class="post">
					<asp:LinkButton runat="server" Text="Edit" CommandName='edit' CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
					|
					<asp:LinkButton runat="server" Text="Delete" CommandName='delete' CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			<tr>
				<td class="footer1" colspan="3">
					<asp:LinkButton runat="server" Text="Add" CommandName='add'></asp:LinkButton></td>
			</tr>
			</table>
		</FooterTemplate>
	</asp:Repeater>
</YAF:AdminMenu>
<YAF:SmartScroller ID="SmartScroller1" runat="server" />
