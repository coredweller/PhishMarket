<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowAnalysis.aspx.cs" Inherits="PhishMarket.MyPhishMarket.ShowAnalysis"
    MasterPageFile="~/Master/Shadowed.Master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>
            Show Analysis</h2>
    </div>
    <div>
        <h3>
            <asp:Label runat="server" ID="lblShow"></asp:Label></h3>
        <asp:PlaceHolder ID="phMyShowRating" runat="server" Visible="false">
            <ajaxToolkit:Rating ID="ajaxShowRating" runat="server" MaxRating="5" StarCssClass="ratingStar"
                WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar"
                OnChanged="ajaxShowRating_Changed" />
        </asp:PlaceHolder>
    </div>
    <asp:PlaceHolder ID="phMyShow" runat="server" Visible="false">
        <br />
        <%--<br />--%>
        <asp:PlaceHolder ID="phMyShowSuccess" runat="server" Visible="false">You have successfully
            saved a review for this show. </asp:PlaceHolder>
        <asp:PlaceHolder ID="phMyShowFailure" runat="server" Visible="false">There was an error
            saving your review.</asp:PlaceHolder>
        <asp:PlaceHolder ID="phMyShowReviewTooLong" runat="server" Visible="false">Your review
            was too long. Please keep it under 1000 characters.</asp:PlaceHolder>
        <%--
        Rate the show:
            <ajaxToolkit:Rating ID="ajaxShowRating" runat="server" MaxRating="5" StarCssClass="ratingStar"
                WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar"
                OnChanged="ajaxShowRating_Changed" />--%>
        <br />
        <div>
            Review:<br />
            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Columns="50" Rows="7"></asp:TextBox><br />
            <asp:Button ID="btnSubmitShowNotes" runat="server" Text="Submit Show Notes" OnClick="btnSubmitShowNotes_Click" />
        </div>
    </asp:PlaceHolder>
    
    <asp:PlaceHolder ID="phNotMyShow" runat="server" Visible="true">
    <br />
    <asp:PlaceHolder ID="phNotMyShowFailure" runat="server" Visible="false">There was an error adding this show to My Shows.</asp:PlaceHolder>
        <div>
            You currently do not have this added to My Shows yet.  Click <asp:LinkButton ID="AddMyShow" OnClick="btnAddMyShow_Click" runat="server" Text="HERE"></asp:LinkButton> to add this to My Shows.
        </div>
    </asp:PlaceHolder>
    <br />
    <br />
    <h2>
        Setlist Analysis</h2>
    <asp:PlaceHolder ID="phRatingSuccess" runat="server" Visible="false">You have successfully
        rated this song. </asp:PlaceHolder>
    <asp:PlaceHolder ID="phRatingError" runat="server" Visible="false">There was an error
        saving your rating.</asp:PlaceHolder>
    <%--<div>--%>
    <asp:Repeater ID="rptSongs" runat="server" OnItemCommand="rptSongs_ItemCommand">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# GetSetNumber((short)Eval("Set.SetNumber"), (bool)Eval("Set.Encore")) %>
                </td>
                <td>
                    <asp:LinkButton ID="lnkEditNotes" runat="server" CommandArgument='<%# Eval("Song.SetSongId") %>'
                        CommandName="EDITNOTES" Text='<%# Eval("Song.SongName") %>'></asp:LinkButton>
                </td>
                &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                <td>
                    <ajaxToolkit:Rating ID="ajaxSongRating" runat="server" CurrentRating='<%# DetermineRating((int?)Eval("Analysis.Rating")) %>'
                        MaxRating="5" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                        FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" OnChanged="ajaxSongRating_Changed"
                        Tag='<%# Eval("Song.SetSongId") %>' />
                    <%--<%# OutputRating((double?)Eval("Analysis.Rating")) %>--%>
                </td>
                &nbsp;&nbsp;&nbsp;
                <td>
                    <%# GetShortenedNote((string)Eval("Analysis.Notes")) %>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <%--</div>
    <br />
    <br />--%>
    <asp:PlaceHolder ID="phGeneralError" runat="server" Visible="false">
        <br />
        Sorry there was an error. Please click another song and try again.<br />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phUpdatingSuccess" runat="server" Visible="false">
        <br />
        You have successfully updated your analysis<br />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phNewSuccess" runat="server" Visible="false">
        <br />
        You have successfully added a new analysis
        <br />
    </asp:PlaceHolder>
    <br />
    <div>
        Edit notes for&nbsp;<asp:Label ID="lblSetSongName" runat="server"></asp:Label>
    </div>
    <div>
        <asp:TextBox ID="txtSetSongNotes" runat="server" TextMode="MultiLine" Columns="30"
            Rows="5"></asp:TextBox>
        <br />
        <asp:Button ID="btnSubmitNotes" runat="server" Text="Submit Song Notes" OnClick="btnSubmitNotes_Click" />
        <asp:HiddenField ID="hdnSetSongId" runat="server" Visible="false" />
        <asp:HiddenField ID="hdnMyShowId" runat="server" Visible="false" />
    </div>
</asp:Content>
