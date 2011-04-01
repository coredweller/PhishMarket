<%@ Control Language="c#" CodeFile="posts.ascx.cs" AutoEventWireup="True" Inherits="YAF.Pages.posts" %>
<%@ Register TagPrefix="YAF" TagName="DisplayPost" Src="../controls/DisplayPost.ascx" %>
<%@ Register TagPrefix="YAF" TagName="DisplayAd" Src="../controls/DisplayAd.ascx" %>
<YAF:PageLinks runat="server" ID="PageLinks" />
<a id="top" name="top"></a>
<asp:Repeater ID="Poll" runat="server" Visible="false">
	<HeaderTemplate>
		<table class="content" cellspacing="1" cellpadding="0" width="100%">
			<tr>
				<td class="header1" colspan="3">
					<YAF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="question" />
					:
					<%# GetPollQuestion() %>
					<%# GetPollIsClosed() %>
				</td>
			</tr>
			<tr>
				<td class="header2">
					<YAF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="choice" />
				</td>
				<td class="header2" align="center" width="10%">
					<YAF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="votes" />
				</td>
				<td class="header2" width="40%">
					<YAF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="statistics" />
				</td>
			</tr>
	</HeaderTemplate>
	<ItemTemplate>
		<tr>
			<td class="post">
				<YAF:MyLinkButton runat="server" Enabled="<%#CanVote%>" CommandName="vote" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ChoiceID") %>'
					Text='<%# HtmlEncode(General.BadWordReplace(Convert.ToString(DataBinder.Eval(Container.DataItem, "Choice")))) %>' /></td>
			<td class="post" align="center">
				<%# DataBinder.Eval(Container.DataItem, "Votes") %>
			</td>
			<td class="post">
				<nobr>
					<img alt="" src="<%# GetThemeContents("VOTE","LCAP") %>"><img alt="" src='<%# GetThemeContents("VOTE","BAR") %>'
						height="12" width='<%# VoteWidth(Container.DataItem) %>%'><img src='<%# GetThemeContents("VOTE","RCAP") %>'></nobr>
				<%# DataBinder.Eval(Container.DataItem,"Stats") %>
				%</td>
		</tr>
	</ItemTemplate>
	<FooterTemplate>
		</table><br />
	</FooterTemplate>
</asp:Repeater>
<table class='command' cellspacing='0' cellpadding='0' width='100%'>
	<tr>
		<td align="left">
			<YAF:Pager runat="server" ID="Pager" UsePostBack="False" />
		</td>
		<td>
			<YAF:ThemeButton ID="MoveTopic1" runat="server" CssClass="yafcssbigbutton" TextLocalizedTag="BUTTON_MOVETOPIC"
				TitleLocalizedTag="BUTTON_MOVETOPIC_TT" OnClick="MoveTopic_Click" />
			<YAF:ThemeButton ID="UnlockTopic1" runat="server" CssClass="yafcssbigbutton" TextLocalizedTag="BUTTON_UNLOCKTOPIC"
				TitleLocalizedTag="BUTTON_UNLOCKTOPIC_TT" OnClick="UnlockTopic_Click" />
			<YAF:ThemeButton ID="LockTopic1" runat="server" CssClass="yafcssbigbutton" TextLocalizedTag="BUTTON_LOCKTOPIC"
				TitleLocalizedTag="BUTTON_LOCKTOPIC_TT" OnClick="LockTopic_Click" />
			<YAF:ThemeButton ID="DeleteTopic1" runat="server" CssClass="yafcssbigbutton" TextLocalizedTag="BUTTON_DELETETOPIC"
				TitleLocalizedTag="BUTTON_DELETETOPIC_TT" OnClick="DeleteTopic_Click" OnLoad="DeleteTopic_Load" />
			<YAF:ThemeButton ID="NewTopic1" runat="server" CssClass="yafcssbigbutton" TextLocalizedTag="BUTTON_NEWTOPIC"
				TitleLocalizedTag="BUTTON_NEWTOPIC_TT" OnClick="NewTopic_Click" />
			<YAF:ThemeButton ID="PostReplyLink1" runat="server" CssClass="yafcssbigbutton" TextLocalizedTag="BUTTON_POSTREPLY"
				TitleLocalizedTag="BUTTON_POSTREPLY_TT" OnClick="PostReplyLink_Click" />
		</td>
	</tr>
</table>
<table class="content" cellspacing="1" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3" style="padding:0px">
			<table border="0" cellpadding="0" cellspacing="0" width="100%" class="header1">
				<tr class="header1">
					<td class="header1Title">
						<asp:Label ID="TopicTitle" runat="server" /></td>
					<td align="right">
						<asp:HyperLink ID="OptionsLink" runat="server">
							<YAF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="Options" />
						</asp:HyperLink>
						<asp:PlaceHolder runat="server" ID="ViewOptions">&middot;
							<asp:HyperLink ID="ViewLink" runat="server">
								<YAF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="View" />
							</asp:HyperLink>
						</asp:PlaceHolder>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr class="header2">
		<td colspan="3" align="right" class="header2links">
			<asp:LinkButton ID="PrevTopic" CssClass="header2link" runat="server" OnClick="PrevTopic_Click">
				<YAF:LocalizedLabel ID="LocalizedLabel7" runat="server" LocalizedTag="prevtopic" />
			</asp:LinkButton>
			&middot;
			<asp:LinkButton ID="NextTopic" CssClass="header2link" runat="server" OnClick="NextTopic_Click">
				<YAF:LocalizedLabel ID="LocalizedLabel8" runat="server" LocalizedTag="nexttopic" />
			</asp:LinkButton>
			<div runat="server" visible="false">
				<asp:LinkButton ID="TrackTopic" CssClass="header2link" runat="server" OnClick="TrackTopic_Click">
					<YAF:LocalizedLabel ID="LocalizedLabel9" runat="server" LocalizedTag="watchtopic" />
				</asp:LinkButton>
				&middot;
				<asp:LinkButton ID="EmailTopic" CssClass="header2link" runat="server" OnClick="EmailTopic_Click">
					<YAF:LocalizedLabel ID="LocalizedLabel10" runat="server" LocalizedTag="emailtopic" />
				</asp:LinkButton>
				&middot;
				<asp:LinkButton ID="PrintTopic" CssClass="header2link" runat="server" OnClick="PrintTopic_Click">
					<YAF:LocalizedLabel ID="LocalizedLabel11" runat="server" LocalizedTag="printtopic" />
				</asp:LinkButton>
				&middot;
				<asp:HyperLink ID="RssTopic" CssClass="header2link" runat="server">
					<YAF:LocalizedLabel ID="LocalizedLabel12" runat="server" LocalizedTag="rsstopic" />
				</asp:HyperLink>
			</div>
		</td>
	</tr>
	<asp:Repeater ID="MessageList" runat="server" OnItemCreated="MessageList_OnItemCreated">
		<ItemTemplate>
			<%# GetThreadedRow(Container.DataItem) %>
			<YAF:DisplayPost ID="DisplayPost1" runat="server" DataRow="<%# Container.DataItem %>" Visible="<%#IsCurrentMessage(Container.DataItem)%>"
				IsThreaded="<%#IsThreaded%>" />
            <YAF:DisplayAd ID="DisplayAd" runat="server" Visible="False" />		    
		</ItemTemplate>
		<AlternatingItemTemplate>
			<%# GetThreadedRow(Container.DataItem) %>
			<YAF:DisplayPost ID="DisplayPostAlt" runat="server" DataRow="<%# Container.DataItem %>" IsAlt="True"
				Visible="<%#IsCurrentMessage(Container.DataItem)%>" IsThreaded="<%#IsThreaded%>" />
		    <YAF:DisplayAd ID="DisplayAd" runat="server" Visible="False" />
		</AlternatingItemTemplate>
	</asp:Repeater>
	<asp:PlaceHolder ID="QuickReplyPlaceHolder" runat="server">
		<tr>
			<td colspan="3" class="post" style="padding: 0px;">
				<YAF:DataPanel runat="server" ID="DataPanel1" AllowTitleExpandCollapse="true" TitleStyle-CssClass="header2"
					TitleStyle-Font-Bold="true" Collapsed="true">
					<div class="post quickReplyLine" id="QuickReplyLine" runat="server">
					</div>
					<div id="CaptchaDiv" align="center" visible="false" runat="server">
						<br />
						<table class="content">
							<tr>
								<td class="header2">
									<YAF:LocalizedLabel ID="LocalizedLabel13" runat="server" LocalizedTag="Captcha_Image" />
								</td>
							</tr>
							<tr>
								<td class="post" align="center">
									<asp:Image ID="imgCaptcha" runat="server" AlternateText="Captcha" /></td>
							</tr>
							<tr>
								<td class="post">
									<YAF:LocalizedLabel ID="LocalizedLabel14" runat="server" LocalizedTag="Captcha_Enter" />
									<asp:TextBox ID="tbCaptcha" runat="server" /></td>
							</tr>
						</table>
						<br />
					</div>
					<div align="center" style="margin: 7px;">
						<asp:Button ID="QuickReply" CssClass="pbutton" runat="server" />
					</div>
				</YAF:DataPanel>
			</td>
		</tr>
	</asp:PlaceHolder>
	<YAF:ForumUsers ID="ForumUsers1" runat="server" />
</table>
<table class="command" cellspacing="0" cellpadding="0" width="100%">
	<tr>
		<td align="left">
			<YAF:Pager runat="server" id="PagerBottom" LinkedPager="Pager" UsePostBack="false" />
		</td>
		<td>
			<YAF:ThemeButton ID="MoveTopic2" runat="server" CssClass="yafcssbigbutton" TextLocalizedTag="BUTTON_MOVETOPIC"
				TitleLocalizedTag="BUTTON_MOVETOPIC_TT" OnClick="MoveTopic_Click" />
			<YAF:ThemeButton ID="UnlockTopic2" runat="server" CssClass="yafcssbigbutton" TextLocalizedTag="BUTTON_UNLOCKTOPIC"
				TitleLocalizedTag="BUTTON_UNLOCKTOPIC_TT" OnClick="UnlockTopic_Click" />
			<YAF:ThemeButton ID="LockTopic2" runat="server" CssClass="yafcssbigbutton" TextLocalizedTag="BUTTON_LOCKTOPIC"
				TitleLocalizedTag="BUTTON_LOCKTOPIC_TT" OnClick="LockTopic_Click" />
			<YAF:ThemeButton ID="DeleteTopic2" runat="server" CssClass="yafcssbigbutton" TextLocalizedTag="BUTTON_DELETETOPIC"
				TitleLocalizedTag="BUTTON_DELETETOPIC_TT" OnClick="DeleteTopic_Click" OnLoad="DeleteTopic_Load" />
			<YAF:ThemeButton ID="NewTopic2" runat="server" CssClass="yafcssbigbutton" TextLocalizedTag="BUTTON_NEWTOPIC"
				TitleLocalizedTag="BUTTON_NEWTOPIC_TT" OnClick="NewTopic_Click" />
			<YAF:ThemeButton ID="PostReplyLink2" runat="server" CssClass="yafcssbigbutton" TextLocalizedTag="BUTTON_POSTREPLY"
				TitleLocalizedTag="BUTTON_POSTREPLY_TT" OnClick="PostReplyLink_Click" />
		</td>
	</tr>
</table>

<YAF:PageLinks runat="server" ID="PageLinksBottom" LinkedPageLinkID="PageLinks" />

<asp:PlaceHolder ID="ForumJumpHolder" runat="server">
	<div id="DivForumJump">
		<YAF:LocalizedLabel ID="ForumJumpLabel" runat="server" LocalizedTag="FORUM_JUMP" />
		&nbsp;<YAF:ForumJump ID="ForumJump1" runat="server" />
	</div>
</asp:PlaceHolder>
<div id="DivPageAccess" class="smallfont">
	<YAF:PageAccess ID="PageAccess1" runat="server" />
</div>
<div id="DivSmartScroller">
	<YAF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
<asp:UpdatePanel ID="PopupMenuUpdatePanel" runat="server">
<ContentTemplate>
<YAF:PopMenu runat="server" ID="OptionsMenu" Control="OptionsLink" />
<span id="WatchTopicID" runat="server" visible="false"></span>
</ContentTemplate>
</asp:UpdatePanel>
<YAF:PopMenu runat="server" ID="ViewMenu" Control="ViewLink" />