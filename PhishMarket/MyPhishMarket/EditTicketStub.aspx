﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTicketStub.aspx.cs"
    Inherits="PhishMarket.MyPhishMarket.EditTicketStub" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3>
            Edit This Ticket Stub
        </h3>
    </div>
    <div>
        <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">You have successfully
            edited the ticket stub. </asp:PlaceHolder>
        <asp:PlaceHolder ID="phError" runat="server" Visible="false">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </asp:PlaceHolder>
    </div>
    <asp:PlaceHolder ID="phMain" runat="server" Visible="true">
        <div>
            <table>
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
                        <asp:DropDownList ID="ddlShow" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        PTBM:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkPTBM" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
        </div>
        <br />
        <br />
        <br />
        <br />
        <div>
            <asp:Image runat="server" ID="imgDisplayFull" />
        </div>
        <asp:HiddenField ID="hdnId" runat="server" Visible="false" />
    </asp:PlaceHolder>
</asp:Content>
