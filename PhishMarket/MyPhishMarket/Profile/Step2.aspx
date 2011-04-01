<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step2.aspx.cs" Inherits="PhishMarket.MyPhishMarket.ProfilePages.Step2"
    MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript">
        function dynamic_Select(tourId) {


            $.getJSON("jsonreturner.aspx", { t: tourId }, function(data) {

                $("select[id$=ddlFavoriteLiveShow] > option").remove();

                $.each(data.records, function() {

                    //                    alert("<option value=\"" + this['ID'] + "\">" + this['Show'] + "</option>");
                    //$("#ddlFavoriteLiveShow").append("<option value=\"" + this['ID'] + "\">" + this['Show'] + "</option>");
                    //                    $("#ddlFuckMe").append("<option value=\"" + this['ID'] + "\">" + this['Show'] + "</option>");
                    //$("#ddlFuckMe").append($("<option></option>").val(this['ID']).html(this['Show']));

                    $("<option>").attr("value", this['ID'])
                                                        .text(this['Show'])
                                                        .appendTo("#ddlFavoriteLiveShow");

                });



            });


        }  
    </script>

</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>
            Step 2: Live</h2>
    </div>
    <br />
    <div id="alertDiv" style="padding: 10px; float: left;">
    </div>
    <div style="position: relative;">
        <h3>
            Favorite Tour:</h3>
        <asp:DropDownList ID="ddlFavoriteTour" runat="server" Width="225px">
        </asp:DropDownList>
        <br />
        <br />
        <br />
        <br />
        <h3>
            Favorite Live Show:</h3>
        <b>Current Selection:
            <asp:Label ID="lblCurrentSelection" runat="server"></asp:Label></b>
        <br />
        <br />
        If you want to change your selection then:
        <br />
        <table>
            <tr>
                <td>
                    <%--<asp:DropDownList ID="ddlFavoriteLiveShowTour" runat="server" Width="225px">
                    </asp:DropDownList>--%>
                    <select id="ddlFavoriteLiveShowTour" runat="server" onchange="dynamic_Select(this.value)">
                    </select>
                </td>
                <%--<td rowspan="2">
                    <asp:Button ID="btnChooseTour" runat="server" Text="Choose" OnClick="btnChooseTour_Click" />
                </td>--%>
                <td>
                    <%--<select id="ddlFavoriteLiveShow" runat="server">
                    </select>--%>
                    <select id="ddlFavoriteLiveShow" name="ddlFavoriteLiveShow">
                    </select>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table>
            <tr>
                <td colspan="2">
                    <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="/images/buttons/greySaveButton.jpg"
                        OnClick="btnSubmit_Click" />
                    <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />--%>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <asp:ImageButton ID="btnPrevious" runat="server" ImageUrl="/images/buttons/previousWhiteButton.gif"
                        OnClick="btnPrevious_Click" />
                    <%--<asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="btnPrevious_Click" />--%>
                </td>
                <td>
                    <asp:ImageButton ID="btnNext" runat="server" ImageUrl="/images/buttons/nextWhiteButton.gif"
                        OnClick="btnNext_Click" />
                    <%--<asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
