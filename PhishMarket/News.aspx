<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="PhishMarket.News" 
MasterPageFile="~/Master/Shadowed.Master" %>--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="PhishMarket.News"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="rptPosts" runat="server">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="post">
                <h2 class="title">
                    <%--Make 2 place holders 1 that has a url and 1 that doesnt if TitleUrl is null --%>
                    <a href='<%# (((PhishPond.Concrete.Post)Container.DataItem).TitleUrl) %>'>
                        <%# (((PhishPond.Concrete.Post)Container.DataItem).Title) %>
                    </a>
                </h2>
                <p class="byline">
                    <small>Posted on
                        <%# (((PhishPond.Concrete.Post)Container.DataItem).PostedDate) %>
                        by
                        <%# (((PhishPond.Concrete.Post)Container.DataItem).PostedBy) %></small>
                </p>
                <div class="entry">
                    <p>
                        <%# (((PhishPond.Concrete.Post)Container.DataItem).Entry) %>
                    </p>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
