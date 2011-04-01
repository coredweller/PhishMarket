<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateSong.aspx.cs" Inherits="PhishMarket.Admin.CreateSong"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create Song</h2>
        
        <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">
            You have successfully added a song.
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phError" runat="server" Visible="false">
            There has been an error.  Please try again later.
        </asp:PlaceHolder>
    <table>
        <tr>
            <td>
                Song Name:
            </td>
            <td>
                <asp:TextBox ID="txtSongName" runat="server"></asp:TextBox>
            </td>
        </tr>
        
        
        <tr>
            <td>
                Special Apprearances:
            </td>
            <td>
                <asp:TextBox ID="txtSpecialAppearances" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Album:
            </td>
            <td>
                <asp:TextBox ID="txtAlbum" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Order:
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlOrder"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Length:
            </td>
            <td>
                <asp:TextBox ID="txtLength" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                JamStyle:
            </td>
            <td>
                <asp:DropDownList ID="ddlJamType" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Cover:
            </td>
            <td>
                <asp:CheckBox ID="chkCover" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Abbreviation:
            </td>
            <td>
                <asp:TextBox ID="txtAbbreviation" runat="server"></asp:TextBox>
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
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    
    </table>
</asp:Content>
