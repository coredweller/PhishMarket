<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JQueryTestForm.aspx.cs" Inherits="PhishMarket.JQueryTestForm" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {

            // Add the page method call as an onclick handler for the div.
            $("#lnkClick").click(function() {
                $.ajax({
                    type: "POST",
                    url: "Default.aspx/GetName",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        $("#txtItem").val(msg.d.firstName + " " + msg.d.lastName);
                    }
                });
            });
        });
    </script>

    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HyperLink ID="lnkClick" runat="server" Text="Click Link"></asp:HyperLink>
    Div is under here:
    <div id="Name">
        <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
