<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="PhishMarket.Controls.Login" %>
<br />
<asp:HyperLink runat="server" ID="lnkCreateUser" NavigateUrl="~/CreateUser.aspx"
    Text="New User?"></asp:HyperLink>
OR If you are a member have you
<asp:HyperLink ID="lnkForgotPassword" runat="server" Text="Forgotten your Password?" NavigateUrl="~/ForgotPassword.aspx"></asp:HyperLink>
<br />
<br />
<div>
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <asp:Login runat="server" ID="lcont" OnLoggedIn="Login_LoggedIn">
            </asp:Login>
        </AnonymousTemplate>
        <RoleGroups>
            <asp:RoleGroup Roles="Registered">
                <ContentTemplate>
                    Welcome back!
                </ContentTemplate>
            </asp:RoleGroup>
            <asp:RoleGroup Roles="Administrators">
                <ContentTemplate>
                    Welcome Administrator
                </ContentTemplate>
            </asp:RoleGroup>
            <asp:RoleGroup Roles="Moderator">
                <ContentTemplate>
                    Welcome Mods!
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
        <LoggedInTemplate>
            You are logged in!! But, wait, you are not a member of any roles.
        </LoggedInTemplate>
    </asp:LoginView>
</div>
