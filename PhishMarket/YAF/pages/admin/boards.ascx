<%@ Control language="c#" CodeFile="boards.ascx.cs" AutoEventWireup="True" Inherits="YAF.Pages.Admin.boards" %>





<YAF:PageLinks runat="server" id="PageLinks"/>

<YAF:adminmenu runat="server">

<table cellspacing=1 cellpadding=0 width="100%" class=content>
<tr>
	<td class=header1 colspan="3">Boards</td>
</tr>
<tr>
	<td class=header2>ID</td>
	<td class=header2>Name</td>
	<td class=header2>&nbsp;</td>
</tr>

<asp:repeater id=List runat=server>
<ItemTemplate>
	<tr>
		<td class=post><%# Eval( "BoardID") %></td>
		<td class=post><%# Eval( "Name") %></td>
		<td class=post align="center">
			<asp:linkbutton runat=server commandname=edit commandargument='<%# Eval( "BoardID") %>'>Edit</asp:linkbutton>
			|
			<asp:linkbutton onload="Delete_Load" runat=server commandname=delete commandargument='<%# Eval( "BoardID") %>'>Delete</asp:linkbutton>
		</td>
	</tr>
</ItemTemplate>
</asp:repeater>

<tr class="footer1">
    <td colSpan="3"><asp:LinkButton id=New runat="server" text="New Board"/></td>
</tr>
</table>

</YAF:adminmenu>

<YAF:SmartScroller id="SmartScroller1" runat = "server" />
