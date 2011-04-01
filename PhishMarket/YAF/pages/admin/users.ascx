<%@ Control Language="c#" CodeFile="users.ascx.cs" AutoEventWireup="True" Inherits="YAF.Pages.Admin.users" %>
<YAF:PageLinks runat="server" ID="PageLinks" />
<YAF:AdminMenu runat="server" ID="Adminmenu1">
  <table class="content" cellspacing="0" cellpadding="0" width="100%">
    <tr>
      <td class="post" valign="top">
        <table cellspacing="0" cellpadding="0" width="100%">
          <tr>
            <td class="header2" colspan="5">
              <b>Filter</b></td>
          </tr>
          <tr class="post">
            <td>
              Role:</td>
            <td>
              Rank:</td>
            <td>
              Name Contains:</td>
            <td>
              Email Contains:</td>
            <td width="99%">
              &nbsp;</td>
          </tr>
          <tr class="post">
            <td>
              <asp:DropDownList ID="group" runat="server">
              </asp:DropDownList></td>
            <td>
              <asp:DropDownList ID="rank" runat="server">
              </asp:DropDownList></td>
            <td>
              <asp:TextBox ID="name" runat="server"></asp:TextBox></td>
            <td>
              <asp:TextBox ID="Email" runat="server"></asp:TextBox></td>
            <td align="right">
              <asp:Button ID="search" runat="server" OnClick="search_Click" Text="Search"></asp:Button></td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
  <br/>
  <YAF:Pager ID="PagerTop" runat="server" OnPageChange="PagerTop_PageChange" UsePostBack="True" />
  <table class="content" cellspacing="1" cellpadding="0" width="100%">
    <tr>
      <td class="header1" colspan="7">
        Users</td>
    </tr>
    <tr>
      <td class="header2">
        Name</td>
      <td class="header2">
        Email</td>
      <td class="header2">
        Rank</td>
      <td class="header2" align="center">
        Posts</td>
      <td class="header2" align="center">
        Approved</td>
      <td class="header2">
        Last Visit</td>
      <td class="header2">
        &nbsp;</td>
    </tr>
    <asp:Repeater ID="UserList" runat="server" OnItemCommand="UserList_ItemCommand">
      <ItemTemplate>
        <tr>
          <td class="post">
            <asp:LinkButton ID="NameEdit" runat="server" CommandName="edit" CommandArgument='<%# Eval("UserID") %>'
              Text='<%# BBCode.EncodeHTML( Eval("Name").ToString() ) %>' /></td>
          <td class="post">
            <%# DataBinder.Eval(Container.DataItem,"Email") %>
          </td>
          <td class="post">
            <%# Eval("RankName") %>
          </td>
          <td class="post" align="center">
            <%# Eval( "NumPosts") %>
          </td>
          <td class="post" align="center">
            <%# BitSet(Eval( "Flags"),2) %>
          </td>
          <td class="post">
            <%# YafDateTime.FormatDateTime((System.DateTime)((System.Data.DataRowView)Container.DataItem)["LastVisit"]) %>
          </td>
          <td class="post" align="center">
            <asp:LinkButton runat="server" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserID") %>'
              ID="Linkbutton3" name="Linkbutton1">Edit</asp:LinkButton>
            |
            <asp:LinkButton OnLoad="Delete_Load" runat="server" CommandName="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserID") %>'
              ID="Linkbutton4" name="Linkbutton2">Delete</asp:LinkButton>
          </td>
        </tr>
      </ItemTemplate>
    </asp:Repeater>
    <tr>
      <td class="footer1" colspan="7">
        <b><asp:LinkButton ID="NewUser" OnClick="NewUser_Click" runat="server">New User</asp:LinkButton></b></td>
    </tr>
  </table>
  <YAF:Pager ID="PagerBottom" runat="server" LinkedPager="PagerTop" UsePostBack="True" />
</YAF:AdminMenu>
<YAF:SmartScroller ID="SmartScroller1" runat="server" />
