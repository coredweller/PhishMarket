<%--
<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/Master/Tattoo.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script runat="server">
        public void Page_Error(object sender, System.EventArgs e)
        {
            Exception x = Server.GetLastError();
            YAF.Classes.Data.DB.eventlog_create(YafContext.Current.PageUserID, this, x);
            YAF.Classes.Utils.CreateMail.CreateLogEmail(x);
        }		
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <yaf:forum runat="server" id="forum"></yaf:forum>
</asp:Content>--%>





<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" %>
<script runat="server">
    public void Page_Error( object sender, System.EventArgs e )
    {
        Exception x = Server.GetLastError();
        YAF.Classes.Data.DB.eventlog_create( YafContext.Current.PageUserID, this, x );
        YAF.Classes.Utils.CreateMail.CreateLogEmail( x );
    }		
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="YafHead" runat="server">
    <meta name="Description" content="A bulletin board system written in ASP.NET" />
    <meta name="Keywords" content="Yet Another Forum.net, Forum, ASP.NET, BB, Bulletin Board, opensource" />
    <title>This title is overwritten</title>
</head>
<body>
    <img src="~/images/YAFLogo.jpg" runat="server" alt="YetAnotherForum" id="imgBanner" /><br/>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <YAF:Forum runat="server" ID="forum"></YAF:Forum>
    </form>
</body>
</html>

