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
    <form runat="server">
    <img src="stars.png" class="stars"/>
    <div class="prices">
        <h1 class="prices pricelist">מחירון</h1>
        <ul class="pricelist">
			<li><a>1 משתתפים - 140 ש"ח למשתתף </a></li>
			<li><a>2 משתתפים - 120 ש"ח למשתתף </a></li>
			<li><a>3 משתתפים - 115 ש"ח למשתתף</a></li>
			<li><a>4 משתתפים - 110 ש"ח למשתתף</a></li>
			<li><a>5 משתתפים - 105 ש"ח למשתתף</a></li>
			<li><a>6 משתתפים - 100 ש"ח למשתתף</a></li>
        </ul>
    </div>
     <br />
        
    <div class="ddls">
    <asp:DataGrid ID="grid" runat="server" AutoGenerateColumns="true"></asp:DataGrid>
    <ul class="menu cf">
        <li>
            <asp:Label CssClass="choose" runat="server" ID="label1" Text="מספר משתתפים"
                onmouseover="
                {
                timeDDL.className = 'submenu submenu2 timeDDL Invis';
                dateDDL.className = 'submenu dateDDL Invis';
                particDDL.className = 'submenu particDDL Visible';
                }"
                ></asp:Label>
            <ul class="submenu particDDL Invis" id="particDDL">
                <li class="option"><asp:Button runat="server" CustomParameter="0" text="משתתף אחד" CssClass="transparentButton" OnClick="ParticipantsOrder"/></li>
                <li class="option"><asp:Button runat="server" CustomParameter="1" text="זוג משתתפים" CssClass="transparentButton" OnClick="ParticipantsOrder"/></li>
                <li class="option"><asp:Button runat="server" CustomParameter="2" text="שלושה משתתפים" CssClass="transparentButton" OnClick="ParticipantsOrder"/></li>
                <li class="option"><asp:Button runat="server" CustomParameter="3" text="ארבעה משתתפים" CssClass="transparentButton" OnClick="ParticipantsOrder"/></li>
                <li class="option"><asp:Button runat="server" CustomParameter="4" text="חמישה משתתפים" CssClass="transparentButton" OnClick="ParticipantsOrder"/></li>
                <li class="option"><asp:Button runat="server" CustomParameter="5" text="שישה משתתפים" CssClass="transparentButton" OnClick="ParticipantsOrder"/></li>
            </ul>
        </li>
    </ul>
    <ul class="menu cf">
        <li>
            <asp:Label CssClass="choose" runat="server" ID="label2" Text="הזמנה תאריך"
                onmouseover="
                {
                timeDDL.className = 'submenu submenu2 timeDDL Invis';
                dateDDL.className = 'submenu dateDDL Visible';
                particDDL.className = 'submenu particDDL Invis';
                }"
                ></asp:Label>
            <ul class="submenu dateDDL Invis" id="dateDDL">
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
    <ul class="menu cf">
        <li>
            <asp:Label CssClass="choose" runat="server" ID="label4" Text="הזמנת זמן"
                onmouseover="
                {
                particDDL.className = 'submenu particDDL Invis'
                dateDDL.className = 'submenu dateDDL Invis'
                timeDDL.className = 'submenu submenu2 timeDDL Visible'
                }"
                ></asp:Label>
            <ul class="submenu submenu2 timeDDL Invis" id="timeDDL">
                <li class="noBackground">
                        <ul class="inline">
                            <li class="option center"><asp:Button runat="server" ID="time1" CustomParameter="0" text="1" CssClass="transparentButton" OnClick="TimeOrder"/></li>
                            <li class="option center"><asp:Button runat="server" ID="time2" CustomParameter="1" text="1" CssClass="transparentButton" OnClick="TimeOrder"/></li>
                        </ul> 
                 </li>
                <li class="noBackground">
                        <ul class="inline">
                            <li class="option center"><asp:Button runat="server" ID="time3" CustomParameter="2" text="1" CssClass="transparentButton" OnClick="TimeOrder"/></li>
                            <li class="option center"><asp:Button runat="server" ID="time4" CustomParameter="3" text="1" CssClass="transparentButton" OnClick="TimeOrder"/></li>
                        </ul> 
                 </li>
                <li class="noBackground">
                        <ul class="inline">
                            <li class="option center"><asp:Button runat="server" ID="time5" CustomParameter="4" text="1" CssClass="transparentButton" OnClick="TimeOrder"/></li>
                            <li class="option center"><asp:Button runat="server" ID="time6" CustomParameter="5" text="1" CssClass="transparentButton" OnClick="TimeOrder"/></li>
                        </ul> 
                 </li>
                <li class="noBackground">
                        <ul class="inline">
                            <li class="option center"><asp:Button runat="server" ID="time7" CustomParameter="6" text="1" CssClass="transparentButton" OnClick="TimeOrder"/></li>
                            <li class="option center"><asp:Button runat="server" ID="time8" CustomParameter="7" text="1" CssClass="transparentButton" OnClick="TimeOrder"/></li>
                        </ul> 
                 </li>
                 <li class="noBackground">
                        <ul class="inline">
                            <li class="option center"><asp:Button runat="server" ID="time9" CustomParameter="8" text="1" CssClass="transparentButton" OnClick="TimeOrder"/></li>
                            <li class="option center"><asp:Button runat="server" ID="time10" CustomParameter="9" text="1" CssClass="transparentButton" OnClick="TimeOrder"/></li>
                        </ul> 
                 </li>
                <li class="noBackground">
                        <ul class="inline">
                            <li class="option center"><asp:Button runat="server" ID="time11" CustomParameter="10" text="1" CssClass="transparentButton" OnClick="TimeOrder"/></li>
                            <li class="option center"><asp:Button runat="server" ID="time12" CustomParameter="11" text="1" CssClass="transparentButton" OnClick="TimeOrder"/></li>
                        </ul> 
                 </li>
                <li class="noBackground">
                        <ul class="inline">
                            <li class="option center"><asp:Button runat="server" ID="time13" CustomParameter="12" text="1" CssClass="transparentButton" OnClick="TimeOrder"/></li>
                            <li class="option center"><asp:Button runat="server" ID="time14" CustomParameter="13" text="1" CssClass="transparentButton" OnClick="TimeOrder"/></li>
                        </ul> 
                 </li></ul>
        </li>
    </ul>
     <ul class="continue">
        <li>
            <asp:Label runat="server" CssClass="AspChoose"><asp:Label runat="server" id="label3" CssClass="span2"><asp:Button runat="server"  Text="הבא" CssClass="transparentButton2 transparentButton" OnClick="Order" /></asp:Label></asp:Label>
        </li>
    </ul>
    </div>
    </form>

 </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="AppointmentScript.js"></script>
</asp:Content>
