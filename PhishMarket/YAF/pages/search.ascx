<%@ Control Language="c#" CodeFile="search.ascx.cs" AutoEventWireup="True" Inherits="YAF.Pages.search" %>
<%@ Register Namespace="nStuff.UpdateControls" assembly="nStuff.UpdateControls" TagPrefix="nStuff" %>
<YAF:PageLinks runat="server" ID="PageLinks" />
<script type="text/javascript">
function EndRequestHandler(sender, args) {
   $find('LoadingModal').hide();
}
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
</script>

<nStuff:UpdateHistory runat="server" ID="UpdateHistory" OnNavigate="OnUpdateHistoryNavigate" />

<table class="content" cellspacing="1" cellpadding="0" width="100%">
    <tr>
        <td class="header1" colspan="2">
            <YAF:LocalizedLabel runat="server" LocalizedTag="title" />
        </td>
    </tr>
    <tr>
        <td class="postheader" colspan="2" align="center">
            <asp:DropDownList ID="listForum" runat="server" />
            <asp:DropDownList ID="listResInPage" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="postheader" width="35%" align="right">
            <YAF:LocalizedLabel runat="server" LocalizedTag="postedby" />
        </td>
        <td class="postheader" align="left">
            <asp:TextBox ID="txtSearchStringFromWho" runat="server" Width="350px" />
            <asp:DropDownList ID="listSearchFromWho" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="postheader" width="35%" align="right">
            <YAF:LocalizedLabel runat="server" LocalizedTag="posts" />
        </td>
        <td class="postheader" align="left">
            <asp:TextBox ID="txtSearchStringWhat" runat="server" Width="350px" />
            <asp:DropDownList ID="listSearchWhat" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="postheader" colspan="2" align="center">
            <asp:Button ID="btnSearch" CssClass="pbutton" runat="server" OnClick="btnSearch_Click" OnClientClick="$find('LoadingModal').show(); return true;" />
        </td>
    </tr>
</table>
<br />
<asp:UpdatePanel ID="SearchUpdatePanel" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
    </Triggers>
    <ContentTemplate>
        <YAF:Pager runat="server" ID="Pager" />
        
            <asp:Repeater ID="SearchRes" runat="server">
                <HeaderTemplate>
                    <table class="content" cellspacing="1" cellpadding="0" width="100%">
                    <tr>
                        <td class="header1" colspan="2">
                            <YAF:LocalizedLabel runat="server" LocalizedTag="RESULTS" />
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="header2">
                        <td colspan="2">
                            <b>
                                <YAF:LocalizedLabel runat="server" LocalizedTag="topic" />
                            </b><a href="<%# YafBuildLink.GetLink(ForumPages.posts,"t={0}",DataBinder.Eval(Container.DataItem, "TopicID")) %>">
                                <%# DataBinder.Eval(Container.DataItem, "Topic") %>
                            </a>
                        </td>
                    </tr>
                    <tr class="postheader">
                        <td width="140px" id="NameCell" valign="top">
                            <a name="<%# DataBinder.Eval(Container.DataItem, "MessageID") %>" /><b><a href="<%# YafBuildLink.GetLink(ForumPages.profile,"u={0}",DataBinder.Eval(Container.DataItem, "UserID")) %>">
                                <%# HtmlEncode(DataBinder.Eval(Container.DataItem, "Name")) %>
                            </a></b>
                        </td>
                        <td width="80%" class="postheader">
                            <b>
                                <YAF:LocalizedLabel runat="server" LocalizedTag="POSTED" />
                            </b>
                            <%# YafDateTime.FormatDateTime( ( System.DateTime ) DataBinder.Eval( Container.DataItem, "Posted" ) )%>
                        </td>
                    </tr>
                    <tr class="post">
                        <td width="140px">
                            &nbsp;</td>
                        <td width="80%">
                            <YAF:MessagePostData ID="MessagePostPrimary" runat="server" ShowAttachments="false" ShowSignature="false" DataRow="<%# ( System.Data.DataRowView )Container.DataItem %>"></YAF:MessagePostData>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="header2">
                        <td colspan="2">
                            <b>
                                <YAF:LocalizedLabel runat="server" LocalizedTag="topic" />
                            </b><a href="<%# YafBuildLink.GetLink(ForumPages.posts,"t={0}",DataBinder.Eval(Container.DataItem, "TopicID")) %>">
                                <%# DataBinder.Eval(Container.DataItem, "Topic") %>
                            </a>
                        </td>
                    </tr>
                    <tr class="postheader">
                        <td width="140px" id="NameCell" valign="top">
                            <a name="<%# DataBinder.Eval(Container.DataItem, "MessageID") %>" /><b><a href="<%# YafBuildLink.GetLink(ForumPages.profile,"u={0}",DataBinder.Eval(Container.DataItem, "UserID")) %>">
                                <%# HtmlEncode(Convert.ToString(DataBinder.Eval(Container.DataItem, "Name"))) %>
                            </a></b>
                        </td>
                        <td width="80%" class="postheader">
                            <b>
                                <YAF:LocalizedLabel runat="server" LocalizedTag="POSTED" />
                            </b>
                            <%# YafDateTime.FormatDateTime( ( System.DateTime ) DataBinder.Eval( Container.DataItem, "Posted" ) )%>
                        </td>
                    </tr>
                    <tr class="post_alt">
                        <td width="140px">
                            &nbsp;</td>
                        <td width="80%">
                            <YAF:MessagePostData ID="MessagePostAlt" runat="server" ShowAttachments="false" ShowSignature="false" DataRow="<%# ( System.Data.DataRowView )Container.DataItem %>"></YAF:MessagePostData>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td class="footer1" colspan="2">
                            &nbsp;</td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <asp:PlaceHolder ID="NoResults" runat="Server" Visible="false">
                <table class="content" cellspacing="1" cellpadding="0" width="100%">
                <tr>
                    <td class="header1" colspan="2">
                        <YAF:LocalizedLabel runat="server" LocalizedTag="RESULTS" />
                    </td>
                </tr>
                <tr>
                    <td class="postheader" colspan="2" align="center">
                        <br />
                        <YAF:LocalizedLabel runat="server" LocalizedTag="NO_SEARCH_RESULTS" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="footer1" colspan="2">
                        &nbsp;</td>
                </tr>
                </table>
            </asp:PlaceHolder>
        <YAF:Pager ID="Pager1" runat="server" LinkedPager="Pager" />
    </ContentTemplate>
</asp:UpdatePanel>
<YAF:ModalNotification ID="LoadingModal" BehaviorID="LoadingModal" runat="server" OkButtonVisible="false" />
<div id="DivSmartScroller">
    <YAF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
