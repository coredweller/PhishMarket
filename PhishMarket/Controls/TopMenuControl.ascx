<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenuControl.ascx.cs"
    Inherits="PhishMarket.Controls.TopMenuControl" %>
<li title="Get the latest PhishMarket News here!" class="tTip" id="cloud1"><a href="/News.aspx"
    class="first">What's New</a></li>
<%--<li title="Your Profile and everything Phish that you like here!" class="tTip" id="cloud2">
    <a href="/MyPhishMarket/Dashboard.aspx">My PhishMarket</a></li>
    

<%--<li title="Pick your favorite versions of every song here!" class="tTip"
    id="cloud8"><a href="/MyPhishMarket/Profile/Step3.aspx">Favorites</a></li>--%>

<li title="Write a review or see all the reviews for any show!" class="tTip"
    Id="cloud7"><a href='<%= LinkBuilder.ReviewsLink() %>'>Reviews</a></li>    
<li title="The Phish Phorum to discuss the best in many categories!" class="tTip"
    id="cloud3"><a href="/Default.aspx">Phorum</a></li>


<%--<li title="See Stats about many different Phish categories derived from user's choices!"

    class="tTip" id="cloud4"><a href="/Stats/Report.aspx">Stats</a></li>--%>
<li title="About Me and how to contact me." class="tTip" id="cloud5"><a href="/AboutUs.aspx">
    About</a></li>
    
    
<%--<li title="See you soon!" class="tTip" id="cloud6"><a href="/Logout.aspx">Log Out</a></li>--%>
