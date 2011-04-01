<%@ Control Language="c#" CodeFile="bannedip_edit.ascx.cs" AutoEventWireup="True"
	Inherits="YAF.Pages.Admin.bannedip_edit" %>
<YAF:PageLinks runat="server" ID="PageLinks" />
<YAF:AdminMenu runat="server">
	<table class="content" width="100%" cellspacing="1" cellpadding="0">
		<tr>
			<td class="header1" colspan="2">
				Edit Banned IP Address</td>
		</tr>
		<tr>
			<td class="postheader" width="50%">
				<b>Mask:</b><br />
				The ip address to ban. You can use wildcards (127.0.0.*).</td>
			<td class="post" width="50%">
				<asp:TextBox ID="mask" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="footer1" colspan="2" align="center">
				<asp:Button ID="save" runat="server" Text="Save" OnClick="save_Click"></asp:Button>
				<asp:Button ID="cancel" runat="server" Text="Cancel" OnClick="cancel_Click"></asp:Button>
			</td>
		</tr>
	</table>
</YAF:AdminMenu>
<YAF:SmartScroller ID="SmartScroller1" runat="server" />
