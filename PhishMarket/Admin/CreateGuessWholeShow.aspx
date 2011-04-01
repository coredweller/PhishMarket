<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateGuessWholeShow.aspx.cs"
    Inherits="PhishMarket.Admin.CreateGuessWholeShow" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">You have successfully
        created a GuessWholeShow. </asp:PlaceHolder>
    <asp:PlaceHolder ID="phError" runat="server" Visible="false">There has been an error.
        Please try again later. </asp:PlaceHolder>
    <asp:PlaceHolder ID="phNoSet" runat="server" Visible="false">You must choose a SetId
        from the drop down list or have it passed in through the query via CreateSet.aspx
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phNoTopic" runat="server" Visible="false">You have to pick a topic
        to create a GuessWholeShow. </asp:PlaceHolder>
    <asp:PlaceHolder ID="phCreate" runat="server" Visible="true">
        <div>
            <h2>
                Create A Guess Whole Show
            </h2>
            <div>
                Topics:
                <asp:DropDownList ID="ddlTopics" runat="server">
                </asp:DropDownList>
            </div>
            <div>
                Official:
                <asp:CheckBox ID="chkOfficial" runat="server" />
            </div>
            <div>
                Choose a Set from here:
                <asp:DropDownList ID="ddlSets" runat="server">
                </asp:DropDownList>
                Or leave this blank to use the one passed in the query string.
            </div>
            <div>
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:HiddenField ID="hdnSetId" runat="server" Visible="false" />
</asp:Content>
