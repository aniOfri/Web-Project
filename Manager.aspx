<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="Manager.aspx.cs" Inherits="VR_Web_Project.Manager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>דף מנהל</title>
    <link rel="stylesheet" href="Stylesheets/ManagerStyle.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
        <nav>
        <ul class="links">
            <li><a href="Manager.aspx">לוח זמנים</a></li>
            <!--<li><a href="#information">מידע</a></li>-->
        </ul>
    </nav>
    <a class="cta" href="Home.aspx">בית</a>
    <a class="cta" href="Appointments.aspx">הזמנות</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    <img src="Sources/stars.png" class="stars"/>
    <img src="Sources/stars.png" class="stars2"/>
            <!-- The Modal -->
    <div id="myModal" class="modal">

      <!-- Modal content -->
      <div class="modal-content">
        <div class="modal-header">
            <span class="close"></span>
            <h2 class="popupTitle">מידע על ההזמנה:</h2>
        </div>
        <span class="close"></span>
        <p class="popupData"></p>
      </div>

    </div>
    <div class="wrapper">
        <div class="grid">
            <h1 class="schedule">לוח זמנים</h1>
            <div class="buttons">
                <asp:Button runat="server" CustomParameter="-7" Text="<" OnClick="Calendar_Click"/>
                <asp:Button runat="server" CustomParameter="0" Text="היום" OnClick="Calendar_Click"/>
                <asp:Button runat="server" CustomParameter="7" Text=">" OnClick="Calendar_Click"/>
            </div>
            <asp:DataGrid ID="grid" runat="server" AutoGenerateColumns="true"></asp:DataGrid>
        </div>
        <div class="management">
            <h1>משתמשים:</h1>
            <div class="buttons">
                <asp:Button runat="server" CustomParameter="-1" Text="<" OnClick="Users_Click"/>
                <asp:Button runat="server" CustomParameter="1" Text=">" OnClick="Users_Click"/>
            </div>
            <asp:Panel CssClass="users" ID="usersArea" runat="server"></asp:Panel>
        </div>
    </div>
    <div class="wrapper wrapper2">
        <div class="management">
            <h1>קבלות של כרטיסי מתנה:</h1>
            <div class="buttons">
                <asp:Button runat="server" CustomParameter="-1" Text="<" OnClick="GiftCard_Click"/>
                <asp:Button runat="server" CustomParameter="1" Text=">" OnClick="GiftCard_Click"/>
            </div>
            <asp:Panel CssClass="users" ID="giftCardArea" runat="server"></asp:Panel>
        </div>
        <div class="management">
            <h1>קבלות של הזמנות:</h1>
            <div class="buttons">
                <asp:Button runat="server" CustomParameter="-1" Text="<" OnClick="Users_Click"/>
                <asp:Button runat="server" CustomParameter="1" Text=">" OnClick="Users_Click"/>
            </div>
            <asp:Panel CssClass="users" ID="appointmentsArea" runat="server"></asp:Panel>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="Javascripts/ManagerScript.js"></script>
</asp:Content>
