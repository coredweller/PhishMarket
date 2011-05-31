<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateSetControl.ascx.cs"
    Inherits="PhishMarket.Controls.CreateSetControl" %>
<asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">You have successfully
    added songs to a set. </asp:PlaceHolder>
<asp:PlaceHolder ID="phError" runat="server" Visible="false">There has been an error.
    Please try again later. </asp:PlaceHolder>
<asp:HyperLink ID="lnkReturn" runat="server" Text="Return to last page"></asp:HyperLink>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
<div>
    <h2>
        Choose songs from this list:</h2>
    <br />
    <br />
    <asp:ListBox ID="lstSongs" Rows="15" runat="server" SelectionMode="Single"></asp:ListBox>
    SEGUE?&nbsp;<asp:CheckBox ID="chkSegue" runat="server" />
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
</div>
<br />
<br />
<br />

        <div>
            <h2>
                List of songs in your set</h2>
            <asp:Repeater ID="rptSongList" runat="server" OnItemCommand="rptSongList_ItemCommand">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# (((PhishPond.Concrete.SetSong)Container.DataItem).Order) %>
                        </td>
                        <td>
                            <%# (((PhishPond.Concrete.SetSong)Container.DataItem).Song.SongName) %>
                        </td>
                        <td> 
                            <asp:PlaceHolder ID="phUp" runat="server" Visible='<%# (((PhishPond.Concrete.SetSong)Container.DataItem).Order) != 1 %>'>
                                <asp:LinkButton ID="lnkUp" runat="server" CommandName="UP" CommandArgument='<%# (((PhishPond.Concrete.SetSong)Container.DataItem).SetSongId) %>'
                                    Text="Up"></asp:LinkButton>
                            </asp:PlaceHolder>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="phDown" runat="server" Visible='<%# (((PhishPond.Concrete.SetSong)Container.DataItem).Order) != FinalOrder %>'>
                                <asp:LinkButton ID="lnkDown" runat="server" CommandName="DOWN" CommandArgument='<%# (((PhishPond.Concrete.SetSong)Container.DataItem).SetSongId) %>'
                                    Text="Down"></asp:LinkButton>
                            </asp:PlaceHolder>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkRemove" runat="server" CommandName="REMOVE" CommandArgument='<%# (((PhishPond.Concrete.SetSong)Container.DataItem).SetSongId) %>'
                                Text="Remove"></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="rptSongList" EventName="ItemCommand" />
        <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<asp:HiddenField ID="hdnId" runat="server" Visible="false" />
