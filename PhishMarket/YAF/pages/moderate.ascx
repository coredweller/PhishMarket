<%@ Control Language="c#" CodeFile="moderate.ascx.cs" AutoEventWireup="True" Inherits="YAF.Pages.moderate0" %>
<YAF:PageLinks runat="server" ID="PageLinks" />
<table class="content" cellspacing="1" cellpadding="0" width="100%">
	<tr>
		<td class="header1" colspan="4">
			<YAF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="MEMBERS" />
		</td>
	</tr>
	<tr class="header2">
		<td>
			<YAF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="USER" />
		</td>
		<td align="center">
			<YAF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="ACCEPTED" />
		</td>
		<td>
			<YAF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="ACCESSMASK" />
		</td>
		<td>
			&nbsp;</td>
	</tr>
	<asp:Repeater runat="server" ID="UserList">
		<ItemTemplate>
			<tr class="post">
				<td>
					<%# Eval("Name") %>
				</td>
				<td align="center">
					<%# Eval("Accepted") %>
				</td>
				<td>
					<%# Eval("Access") %>
				</td>
				<td>
					<asp:LinkButton runat="server" Text='<%#GetText("EDIT")%>' CommandName="edit" CommandArgument='<%# Eval("UserID") %>' />
					|
					<asp:LinkButton runat="server" Text='<%#GetText("REMOVE")%>' OnLoad="DeleteUser_Load"
						CommandName="remove" CommandArgument='<%# Eval("UserID") %>' />
				</td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
	<tr class="footer1">
		<td colspan="4">
			<asp:LinkButton runat="server" ID="AddUser" Text="Invite User" /></td>
	</tr>
</table>
<br />
<YAF:Pager ID="PagerTop" runat="server" OnPageChange="PagerTop_PageChange" UsePostBack="True" />
<table class="content" cellspacing="1" cellpadding="0" width="100%">
	<tr>
		<td class="header1" colspan="7">
			<YAF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="title" />
		</td>
	</tr>
	<tr>
		<td class="header2" width="1%">
			&nbsp;</td>
		<td class="header2" align="left">
			<YAF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="topics" />
		</td>
		<td class="header2" align="left" width="15%">
			<YAF:LocalizedLabel ID="LocalizedLabel7" runat="server" LocalizedTag="topic_starter" />
		</td>
		<td class="header2" align="center" width="7%">
			<YAF:LocalizedLabel ID="LocalizedLabel8" runat="server" LocalizedTag="replies" />
		</td>
		<td class="header2" align="center" width="7%">
			<YAF:LocalizedLabel ID="LocalizedLabel9" runat="server" LocalizedTag="views" />
		</td>
		<td class="header2" align="center" width="15%">
			<YAF:LocalizedLabel ID="LocalizedLabel10" runat="server" LocalizedTag="lastpost" />
		</td>
		<td class="header2" width="100">
			&nbsp;</td>
	</tr>
	<asp:Repeater ID="topiclist" runat="server">
		<ItemTemplate>
			<YAF:TopicLine runat="server" DataRow="<%# Container.DataItem %>">
				<td class="postheader" align="center">
					<YAF:ThemeButton ID="DeleteTopic" runat="server" CssClass="yafcssbigbutton" TextLocalizedTag="BUTTON_DELETETOPIC"
						TitleLocalizedTag="BUTTON_DELETETOPIC_TT" OnLoad="Delete_Load" CommandArgument='<%# Eval( "TopicID") %>'
						CommandName='delete' />
				</td>
			</YAF:TopicLine>
		</ItemTemplate>
	</asp:Repeater>
	<tr>
		<td class="footer1" colspan="7">
			&nbsp;</td>
	</tr>
</table>
<YAF:Pager ID="PagerBottom" runat="server" LinkedPager="PagerTop" UsePostBack="True" />
<div id="DivSmartScroller">
	<YAF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
