<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateSet.aspx.cs" Inherits="PhishMarket.Admin.CreateSet"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">You have successfully
            added a set. </asp:PlaceHolder>
        <asp:PlaceHolder ID="phError" runat="server" Visible="false">There has been an error.
            Please try again later. </asp:PlaceHolder>
        <h2>
            Modify a Set</h2>
        <asp:DropDownList ID="ddlSets" runat="server">
        </asp:DropDownList>
        <br /><br />
        <asp:Button ID="btnModifySet" runat="server" OnClick="btnModifySet_Click" Text="Modify Set" />
        <br />
        <br />
        ------------------------- OR ----------------------------
        <br />
        <br />
        <h2>
            Create Set</h2>
        <table>
            <tr>
                <td>
                    Set Number:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSetNumber" runat="server">
                        <asp:ListItem Text="Please select a number" Value="0"></asp:ListItem>
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Encore:
                </td>
                <td>
                    <asp:CheckBox ID="chkEncore" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Official:
                </td>
                <td>
                    <asp:CheckBox ID="chkOfficial" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Notes:
                </td>
                <td>
                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="6" Columns="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Show:
                </td>
                <td>
                    <asp:DropDownList ID="ddlShows" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        </div>
        <br />
        <br />
        <asp:PlaceHolder ID="phAddSongs" runat="server" Visible="false">
            <asp:HyperLink ID="lnkAddSongsToSet" runat="server" Text="Add Songs to the Set you just created!"></asp:HyperLink>
            <br />
            <asp:HyperLink ID="lnkAddSetToGuess" runat="server" Text="Add this Set to a new GuessWholeShow"></asp:HyperLink>
        </asp:PlaceHolder>
    </div>
</asp:Content>
