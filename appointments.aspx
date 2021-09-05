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

     <ul>
        <li>
            <asp:Label runat="server" CssClass="AspChoose"><asp:Label runat="server" id="label3" CssClass="span2"><asp:Button runat="server"  Text="הבא" CssClass="transparentButton2 transparentButton"  /></asp:Label></asp:Label>
        </li>
    </ul>
        
    <div class="ddls">

    <ul class="menu cf">
        <li>
            <h1 class="choose"><span><asp:Label runat="server" ID="label1" Text="מספר משתתפים"></asp:Label></span></h1>
            <ul class="submenu">
                <li class="option"><asp:Button runat="server" CustomParameter="אחד" text="משתתף אחד" CssClass="transparentButton" OnClick="ParticipantsOrder"/></li>
                <li class="option"><asp:Button runat="server" CustomParameter="זוג" text="זוג משתתפים" CssClass="transparentButton" OnClick="ParticipantsOrder"/></li>
                <li class="option"><asp:Button runat="server" CustomParameter="שלושה" text="שלושה משתתפים" CssClass="transparentButton" OnClick="ParticipantsOrder"/></li>
                <li class="option"><asp:Button runat="server" CustomParameter="ארבעה" text="ארבעה משתתפים" CssClass="transparentButton" OnClick="ParticipantsOrder"/></li>
                <li class="option"><asp:Button runat="server" CustomParameter="חמישה" text="חמישה משתתפים" CssClass="transparentButton" OnClick="ParticipantsOrder"/></li>
                <li class="option"><asp:Button runat="server" CustomParameter="שישה" text="שישה משתתפים" CssClass="transparentButton" OnClick="ParticipantsOrder"/></li>
            </ul>
        </li>
    </ul>
        <ul class="menu cf">
        <li>
            <h1 class="choose"><span><asp:Label runat="server" ID="label2" Text="הזמנה מראש"></asp:Label></span></h1>
            <ul class="submenu">
                <li class="option"><asp:Button runat="server" ID="date1" CustomParameter="1"  text="יום" CssClass="transparentButton" OnClick="DayOrder"/></li>
                <li class="option"><asp:Button runat="server" ID="date2" CustomParameter="2"  text="יומיים" CssClass="transparentButton" OnClick="DayOrder"/></li>
                <li class="option"><asp:Button runat="server" ID="date3" CustomParameter="3"  text="שלושה ימים" CssClass="transparentButton" OnClick="DayOrder"/></li>
                <li class="option"><asp:Button runat="server" ID="date4" CustomParameter="4"  text="ארבעה ימים" CssClass="transparentButton" OnClick="DayOrder"/></li>
                <li class="option"><asp:Button runat="server" ID="date5" CustomParameter="5"  text="חמישה ימים" CssClass="transparentButton" OnClick="DayOrder"/></li>
                <li class="option"><asp:Button runat="server" ID="date6" CustomParameter="6"  text="שישה ימים" CssClass="transparentButton" OnClick="DayOrder"/></li>
                <li class="option"><asp:Button runat="server" ID="date7" CustomParameter="7"  text="שבעה ימים" CssClass="transparentButton" OnClick="DayOrder"/></li>
            </ul>
        </li>
    </ul>
    </div>
    </div>


 </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
