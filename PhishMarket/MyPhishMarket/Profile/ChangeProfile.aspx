<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeProfile.aspx.cs"
    Inherits="PhishMarket.MyPhishMarket.ChangeProfile" MasterPageFile="~/Master/Shadowed.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>
            Change Your Profile</h2>
    </div>
    <br />
    <div style="padding-bottom: 10px;">
        <div class="tTip" id="cloud1" title="Step 1: Basic information and favorite studio elements.">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgLinkStep1" runat="server" OnClick="imgLinkStep1_Click" ImageUrl="/images/LawnBoyCoverSmall.jpg" />
                    </td>
                    <td>
                        <div style="font-size: 100px;">
                            STEP 1
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div style="padding-bottom: 10px;">
        <div class="tTip" id="Div1" title="Step 2: Favorite live elements.">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgLinkStep2" runat="server" OnClick="imgLinkStep2_Click" ImageUrl="/images/PhishShowSmall.jpg" />
                    </td>
                    <td>
                        <div style="font-size: 100px;">
                            STEP 2
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%--<div>
        <div class="tTip" id="Div2" title="Step 3: Choose your favorite live version of each song.">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgLinkStep3" runat="server" OnClick="imgLinkStep3_Click" ImageUrl="/images/LivePhish1CoverSmall.jpg" />
                    </td>
                    <td>
                        <div style="font-size: 100px;">
                            STEP 3
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>--%>
</asp:Content>
