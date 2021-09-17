<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VR_Web_Project.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>התחברות</title>
    <link rel="stylesheet" href="LoginStyle.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
        <nav>
        <ul class="links">
            <li><a href="Login.aspx">התחברות</a></li>
            <!--<li><a href="#information">מידע</a></li>-->
        </ul>
    </nav>
    <a class="cta" href="Home.aspx">בית</a>
    <a class="cta" href="Appointments.aspx">הזמנות</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    <img src="stars.png" class="stars"/>
    <div class="formDiv">
        <form id="formLogin" onsubmit="return LogValid();" method="get">
            
            <div class="sidebyside">
                <div><h1 class="title">התחברות</h1></div>
                <div><a href="Register.aspx"><h2>או הרשמה</h2></a></div>
             </div>

            <label for="textLogin">שם משתמש:</label><br />
            <input id="textLogin" type="text" name="name" /><br />
            <a class="errorMsgs" id="logTextError"></a><br /><br /> <!--- Error msg -->
            
            <label for="passLogin">סיסמה:</label> <br />
            <input id="passLogin" type="password" name="password"/><br />
            <a class="errorMsgs" id="logPassError"></a><br /> <!--- Error msg -->
            
            <label><%=LogStatus%></label><br /><br />
            <div class="center">
                <div class="backgroundButton"><input id="submitLogin" type="submit" name="submit" class="transparentButton" /></div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="LoginScript.js"></script>
</asp:Content>
