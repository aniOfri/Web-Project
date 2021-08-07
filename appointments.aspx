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
    <div class="title">
        <h1 class="title">מחירון</h1>
        <ul>
			<li><a>2 משתתפים - [מחיר] ש"ח למשתתף </a></li>
			<li><a>3 משתתפים - [מחיר אחר] ש"ח למשתתף</a></li>
			<li><a>4 משתתפים - [מחיר אחר] ש"ח למשתתף</a></li>
			<li><a>5 משתתפים - [עוד מחיר] ש"ח למשתתף</a></li>
			<li><a>6 משתתפים - [מחיר שונה מהמחיר הראשון] ש"ח למשתתף</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InfoPlaceHolder" runat="server">
</asp:Content>
