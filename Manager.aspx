<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="Manager.aspx.cs" Inherits="VR_Web_Project.Manager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>דף מנהל</title>
    <link rel="stylesheet" href="ManagerStyle.css"/>
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
    <form runat="server">
    <img src="stars.png" class="stars"/>
    <div class="grid">
        <h1 class="schedule">לוח זמנים</h1>
        <asp:DataGrid ID="grid" runat="server" AutoGenerateColumns="true"></asp:DataGrid>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
