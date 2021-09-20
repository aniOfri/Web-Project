<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="VR_Web_Project.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>הרשמה</title>
    <link rel="stylesheet" href="Stylesheets/RegisterStyle.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <nav>
        <ul class="links">
            <li><a href="Register.aspx">הרשמה</a></li>
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
                <div><h1 class="title">הרשמה</h1></div>
                <div><a href="Login.aspx"><h2>או התחברות</h2></a></div>
             </div>

            <label for="textRegister">שם משתמש:</label><br />
            <input id="textRegister" type="text" name="name"/><br />
            <a class="errorMsgs" id="regTextError"></a><br /> <!--- Error msg -->

            <label for="textRegister">מספר טלפון:</label><br />
            <input id="phoneRegister" type="text" name="phone"/><br />
            <a class="errorMsgs" id="regPhoneError"></a><br /> <!--- Error msg -->
            
            <label for="passRegister">סיסמה:</label><br />
            <input id="passRegister" type="password" name="password"/><br />
            <a class="errorMsgs" id="regPassError"></a><br /> <!--- Error msg -->
            
            <label for="passRegister2">אישור סיסמה:</label><br />
            <input id="passRegister2" type="password" /><br />
            <a class="errorMsgs" id="regPassConfError"></a><br /><br /> <!--- Error msg -->
            
            <label><%=RegStatus%></label><br /><br />
            <div class="center">
                <div class="backgroundButton"><input id="submitLogin" value="הרשם" type="submit" name="submit" class="transparentButton" /></div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="Javascripts/RegisterScript.js"></script>
</asp:Content>
