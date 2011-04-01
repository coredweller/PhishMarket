<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminMenu.ascx.cs" Inherits="PhishMarket.Controls.AdminMenu" %>

<div>
    <ul>
        <li><a href='<%= LinkBuilder.AdminCreateTourLink() %>' id="lnkCreateTour">Create Tour</a></li>
        <li><a href='<%= LinkBuilder.AdminCreateShowLink() %>' id="A1">Create Show</a></li>
        <li><a href='<%= LinkBuilder.AdminCreateSetLink() %>' id="A2">Create Set</a></li>
        <li><a href='<%= LinkBuilder.AdminCreateSongLink() %>' id="A3">Create Song</a></li>
        <li><a href='<%= LinkBuilder.AdminCreateTopicLink() %>' id="A4">Create Topic</a></li>
        <li><a href='<%= LinkBuilder.AdminCreatePostLink() %>' id="A5">Create Post</a></li>
        <li><a href='<%= LinkBuilder.AdminCreateGuessWholeShowLink() %>' id="A6">Create Guess Whole Show</a></li>
    </ul>
</div>