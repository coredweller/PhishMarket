<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateTicketStub.aspx.cs"
    Inherits="PhishMarket.Admin.CreateTicketStub" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <uc:YearSelector id="yearSelector" OnYearSelected="yearSelector_YearSelected" runat="server">
                    </uc:YearSelector>
    </div>
    <br />
    <div>
        <asp:DropDownList ID="ddlShows" runat="server">
        </asp:DropDownList>
    </div>
    <br />
    <br />
    <div>
    File: <asp:FileUpload ID="fuPicture" runat="server" />
        <%--File Name:
        <asp:TextBox ID="txtFileName" runat="server"></asp:TextBox>--%>
        <br />
        PTBM:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox ID="chkPTBM" runat="server" />
    </div>
    <br />
    <br />
    <div>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
    </div>
    
    <br />
    <br />
    <br />
    <asp:Image runat="server" ID="imgTheImage" />
</asp:Content>
