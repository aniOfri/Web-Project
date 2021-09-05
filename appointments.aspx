<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="Appointments.aspx.cs" Inherits="VR_Web_Project.appointments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>הזמנות</title>
    <link rel="stylesheet" href="AppointmentStyle.css"/>
</asp:Content>
<asp:Content ID="headerContent" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <nav>
        <ul class="links">
            <li><a href="Appointments.aspx">מחירים</a></li>
            <!--<li><a href="#information">מידע</a></li>-->
        </ul>
    </nav>
    <a class="cta" href="Home.aspx">בית</a>
</asp:Content>
<asp:Content ID="pricesContent" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    <img src="stars.png" class="stars"/>
    <div class="prices">
        <h1 class="prices">מחירון</h1>
        <ul>
			<li><a>1 משתתפים - 140 ש"ח למשתתף </a></li>
			<li><a>2 משתתפים - 120 ש"ח למשתתף </a></li>
			<li><a>3 משתתפים - 115 ש"ח למשתתף</a></li>
			<li><a>4 משתתפים - 110 ש"ח למשתתף</a></li>
			<li><a>5 משתתפים - 105 ש"ח למשתתף</a></li>
			<li><a>6 משתתפים - 100 ש"ח למשתתף</a></li>
        </ul>

    <div class="ddls">
    <ul class="menu cf">
        <li>
            <h1 class="choose"><span><asp:Label runat="server" ID="label1" Text="בחר"></asp:Label></span></h1>
            <ul class="submenu">
                <li class="option"><asp:Button runat="server" text="משתתף אחד" CssClass="transparentButton" OnClick="One_Click"/></li>
                <li class="option"><asp:Button runat="server" text="זוג משתתפים" CssClass="transparentButton" OnClick="Two_Click"/></li>
                <li class="option"><asp:Button runat="server" text="שלושה משתתפים" CssClass="transparentButton" OnClick="Three_Click"/></li>
                <li class="option"><asp:Button runat="server" text="ארבעה משתתפים" CssClass="transparentButton" OnClick="Four_Click"/></li>
                <li class="option"><asp:Button runat="server" text="חמישה משתתפים" CssClass="transparentButton" OnClick="Five_Click"/></li>
                <li class="option"><asp:Button runat="server" text="שישה משתתפים" CssClass="transparentButton" OnClick="Six_Click"/></li>
            </ul>
        </li>
    </ul>
        <ul class="menu cf">
        <li>
            <h1 class="choose"><span><asp:Label runat="server" ID="label2" Text="בחר"></asp:Label></span></h1>
            <ul class="submenu">
                <li class="option"><asp:Button runat="server" text="משתתף אחד" CssClass="transparentButton" OnClick="One_Click"/></li>
                <li class="option"><asp:Button runat="server" text="זוג משתתפים" CssClass="transparentButton" OnClick="Two_Click"/></li>
                <li class="option"><asp:Button runat="server" text="שלושה משתתפים" CssClass="transparentButton" OnClick="Three_Click"/></li>
                <li class="option"><asp:Button runat="server" text="ארבעה משתתפים" CssClass="transparentButton" OnClick="Four_Click"/></li>
                <li class="option"><asp:Button runat="server" text="חמישה משתתפים" CssClass="transparentButton" OnClick="Five_Click"/></li>
                <li class="option"><asp:Button runat="server" text="שישה משתתפים" CssClass="transparentButton" OnClick="Six_Click"/></li>
            </ul>
        </li>
    </ul>
    </div>
    </div>


 </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
