﻿<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VR_Web_Project.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>התחברות</title>
    <link rel="stylesheet" href="LoginStyle.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
        <nav>
        <ul class="links">
            <li><a href="Appointments.aspx">מחירים</a></li>
            <!--<li><a href="#information">מידע</a></li>-->
        </ul>
    </nav>
    <a class="cta" href="Home.aspx">בית</a>
    <a class="cta" href="Appointments.aspx">הזמנות</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
        <form id="formLogin" onsubmit="return LogValid();" method="get">
            <h1 class="title">התחברות</h1>
            <label for="textLogin">Username:</label><br />
            <input id="textLogin" type="text" name="name" />
            <a class="errorMsgs" id="logTextError"></a><br /> <!--- Error msg -->
            
            <label for="passLogin">Password:</label> <br />
            <input id="passLogin" type="password" name="password"/>
            <a class="errorMsgs" id="logPassError"></a><br /><br /> <!--- Error msg -->
            
            <input id="submitLogin" type="submit" name="submit" />
        </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="LoginScript.js"></script>
</asp:Content>