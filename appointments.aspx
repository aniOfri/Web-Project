<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="Appointments.aspx.cs" Inherits="VR_Web_Project.appointments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <div>
        <h1>מחירון</h1>
        <ul>
			<li><a>2 משתתפים - 140 ש"ח למשתתף</a></li>
			<li><a>3 משתתפים - 120 ש"ח למשתתף</a></li>
			<li><a>4 משתתפים - 120 ש"ח למשתתף</a></li>
			<li><a>5 משתתפים - 115 ש"ח למשתתף</a></li>
			<li><a>6 משתתפים - 110 ש"ח למשתתף</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InfoPlaceHolder" runat="server">
</asp:Content>
