<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateTopic.aspx.cs" Inherits="PhishMarket.Admin.CreateTopic" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
 <h2>
        Create Topic</h2>
        
        <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">
            You have successfully added a topic.
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phError" runat="server" Visible="false">
            There has been an error.  Please try again later.
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phTypeError" runat="server" Visible="false">
            You can only choose a Show type OR a Tour type please deselect 1 to continue.
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phChooseTypeError" runat="server" Visible="false">
            Please choose a Type.  You can only choose one but you need 1 to continue.
        </asp:PlaceHolder>
    <table>
        <tr>
            <td>
                Topic Name:
            </td>
            <td>
                <asp:TextBox ID="txtTopicName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Show Type:
            </td>
            <td>
                <asp:DropDownList ID="ddlShowType" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Tour Type:
            </td>
            <td>
                <asp:DropDownList ID="ddlTourType" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Start Date:
            </td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                End Date:
            </td>
            <td>
                <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
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