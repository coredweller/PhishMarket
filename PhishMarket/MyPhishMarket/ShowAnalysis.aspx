<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowAnalysis.aspx.cs" Inherits="PhishMarket.MyPhishMarket.ShowAnalysis"
    MasterPageFile="~/Master/Shadowed.Master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2 title="Rate the show in stars and write a review about it below. Click the show to see all reviews for this show!" class="tTip"
            id="cloud1">
            Show Analysis</h2>
    </div>
    <div id="alertDiv" style="padding: 10px; float: left;">
    </div>
    <h3>
        <a href='<%= LinkBuilder.ShowReviewsLink(ShowId) %>'>
            <asp:Label runat="server" ID="lblShow"></asp:Label></a></h3>
    <br />
    <asp:PlaceHolder ID="phMyShowRating" runat="server" Visible="false">
        <table>
            <tr>
                <td>
                    Rate Show:
                </td>
                <td>
                    <ajaxToolkit:Rating ID="ajaxShowRating" runat="server" MaxRating="5" StarCssClass="ratingStar"
                        WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar"
                        OnChanged="ajaxShowRating_Changed" />
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phMyShow" runat="server" Visible="false">
        <div>
            &nbsp;Review Show:<br />
            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Columns="50" Rows="7"></asp:TextBox><br />
            <asp:Button ID="btnSubmitShowNotes" runat="server" Text="Submit Show Notes" OnClick="btnSubmitShowNotes_Click" />
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phNotMyShow" runat="server" Visible="true">
        <br />
        <asp:PlaceHolder ID="phNotMyShowFailure" runat="server" Visible="false">There was an
            error adding this show to My Shows.</asp:PlaceHolder>
        <div>
            You currently do not have this added to My Shows yet. Click
            <asp:LinkButton ID="AddMyShow" OnClick="btnAddMyShow_Click" runat="server" Text="HERE"></asp:LinkButton>
            to add this to My Shows.
        </div>
    </asp:PlaceHolder>
    <br />
    <br />
    <h2 title="Click a song to edit your notes for that version and rate this version in stars." class="tTip" id="cloud1">
        Setlist Analysis</h2>
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
    <br />
    <div style="font-size: larger;">
        Edit notes for&nbsp;<b><asp:Label ID="lblSetSongName" runat="server"></asp:Label></b>
    </div>
    <div>
        <asp:TextBox ID="txtSetSongNotes" runat="server" TextMode="MultiLine" Columns="50"
            Rows="7"></asp:TextBox>
        <br />
        <asp:Button ID="btnSubmitNotes" runat="server" Text="Submit Song Notes" OnClick="btnSubmitNotes_Click" />
        <asp:HiddenField ID="hdnSetSongId" runat="server" Visible="false" />
        <asp:HiddenField ID="hdnMyShowId" runat="server" Visible="false" />
        <asp:HiddenField ID="hdnShowId" runat="server" Value="false" />
    </div>
</asp:Content>
