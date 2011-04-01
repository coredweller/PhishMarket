<%@ Page Language="c#" CodeFile="error.aspx.cs" AutoEventWireup="True" Inherits="YAF.error" %>
<html>
<head>
<title>Forum Error</title>
<link type="text/css" rel="stylesheet" href="resources/forum.css" />
<link type="text/css" rel="stylesheet" href="themes/yafpro/theme.css" />
</head>
<body>
<div class="yafnet">
<table class="content" width="100%" cellspacing="1" cellpadding="0">
  <tr>
    <td class="header1">
      Forum Error</td>
  </tr>
  <tr>
    <td class="post" align="center" style="font-size:9pt;color:#990000;">
      <br />
      <asp:Label ID="ErrorMsg" Enabled="true" runat="server" />
      <br /><br />
    </td>
  </tr>
  <tr>
    <td class="footer1" align="center">
      <a href="Default.aspx">Try Again</a>
    </td>
  </tr>
</table>
</div>
</body>
</html>