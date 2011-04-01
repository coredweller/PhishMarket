<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PredictShow.aspx.cs" Inherits="PhishMarket.TourPages.PredictShow"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>
            Predict the Show</h2>
        <table>
            <tr>
                <td>
                    <a href='<%= WholeShowLink %>'>Predict Songs</a>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <a href='<%= SetBasedShowLink %>'>Predict with Sets</a>
                </td>
            </tr>--%>
        </table>
    </div>
</asp:Content>
