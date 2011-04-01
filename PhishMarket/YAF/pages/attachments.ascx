<%@ Control Language="c#" CodeFile="attachments.ascx.cs" AutoEventWireup="True" Inherits="YAF.Pages.attachments" %>
<YAF:PageLinks runat="server" ID="PageLinks" />
<div class="DivTopSeparator"></div>
<table class="content" width="100%" cellspacing="1" cellpadding="0">
	<tr>
		<td class="header1" colspan="3">
			<YAF:LocalizedLabel ID="Title" LocalizedTag="TITLE" runat="server" />
		</td>
	</tr>
	<asp:Repeater runat="server" ID="List">
		<HeaderTemplate>
			<tr>
				<td class="header2">
					<YAF:LocalizedLabel ID="Filename" LocalizedTag="FILENAME" runat="server" />
				</td>
				<td class="header2" align="right">
					<YAF:LocalizedLabel ID="Size" LocalizedTag="SIZE" runat="server" />
				</td>
				<td class="header2">
					&nbsp;</td>
			</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td class="post">
					<%# Eval( "FileName") %>
				</td>
				<td class="post" align="right">
					<%# Eval( "Bytes") %>
				</td>
				<td class="post">
					<asp:LinkButton runat="server" OnLoad="Delete_Load" CommandName="delete" CommandArgument='<%# Eval( "AttachmentID") %>'><%# GetText("DELETE") %></asp:LinkButton>
				</td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
	<tr>
		<td class="header2">
			<YAF:LocalizedLabel ID="UploadTitle" LocalizedTag="UPLOAD_TITLE" runat="server" />
		</td>
		<td class="header2">
			&nbsp;</td>
		<td class="header2">
			&nbsp;</td>
	</tr>
	<tr>
		<td class="postheader">
			<YAF:LocalizedLabel ID="SelectFile" LocalizedTag="SELECT_FILE" runat="server" />
		</td>
		<td class="post">
			<input type="file" id="File" class="pbutton" runat="server" /></td>
		<td class="post">
			<asp:Button runat="server" CssClass="pbutton" ID="Upload" /></td>
	</tr>
	<tr>
		<td class="header2">
			<YAF:LocalizedLabel ID="ExtensionTitle" LocalizedTag="ALLOWED_EXTENSIONS" runat="server" />
		</td>
		<td class="header2">
			&nbsp;</td>
		<td class="header2">
			&nbsp;</td>
	</tr>
	<tr>
		<td class="post" colspan="3">
			<asp:Label ID="ExtensionsList" runat="server"></asp:Label></td>
	</tr>
	<tr class="footer1">
		<td colspan="3" align="center">
			<asp:Button runat="server" CssClass="pbutton" ID="Back" /></td>
	</tr>
</table>
<div id="DivSmartScroller">
    <YAF:SmartScroller id="SmartScroller1" runat="server" />
</div>
