<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WantedList.aspx.cs" Inherits="PhishMarket.MyPhishMarket.WantedListPage"
    MasterPageFile="~/Master/Shadowed.Master" %>


<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3>
            Wanted List</h3>
    </div>
    <div>
        <asp:Repeater ID="rptWantedList" runat="server" OnItemCommand="rptWantedList_ItemCommand">
            <HeaderTemplate>
                <ol>
            </HeaderTemplate>
            <ItemTemplate>
            <li> <%# Eval("Song.SongName") %> </li>
            </ItemTemplate>
            <FooterTemplate>
                </ol></FooterTemplate>
        </asp:Repeater>
    </div>
    <br />
    <br />
    <br />
    <asp:PlaceHolder ID="phAddSongSuccess" runat="server" Visible="false">
     You have successfully added a song to your wanted list.
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phAddSongError" runat="server" Visible="false">
     There has been an error adding a song to your wanted list.
    
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phDuplicateError" runat="server" Visible="false">
        You already have that song added to your wanted list. Please choose a different song.
    </asp:PlaceHolder>
    
    <uc:SelectAlbum runat="server" id="selectAlbum" OnSongSelected="selectAlbum_SongSelected"></uc:SelectAlbum>
    <br />
    <br />
    <br />
        <div>
        <h3>Archived List</h3>
    </div>
    <div>
        <asp:Repeater ID="rptArchive" runat="server">
            <HeaderTemplate>
                </ol>
            </HeaderTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <FooterTemplate>
                </ol></FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
