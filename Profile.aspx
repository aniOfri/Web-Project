<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="VR_Web_Project.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>פרופיל</title>
    <link rel="stylesheet" href="Stylesheets/ProfileStyle.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <nav>
        <ul class="links">
            <li><a href="Profile.aspx">פרופיל</a></li>
        </ul>
    </nav>
    <a class="cta" href="Home.aspx">בית</a>
    <a class="cta" href="Appointments.aspx">הזמנות</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    <img src="stars.png" class="stars"/>
    <div class="data">
        <asp:Label ID="username" runat="server"></asp:Label>
    </div>
    <div class="wrapper">
        <div class="passwordChange">
            <h1>שינוי סיסמה:</h1>
            <form id="formChangePass" onsubmit="return passValid();" method="get">

            <label for="oldPass">סיסמה ישנה:</label><br />
            <input id="oldPass" type="password" name="oldPass"/><br />
            <a class="errorMsgs" id="oldPassError"></a><br /> <!--- Error msg -->

            <label for="newPass">סיסמה חדשה:</label><br />
            <input id="newPass" type="password" name="newPass"/><br />
            <a class="errorMsgs" id="newPassError"></a><br /> <!--- Error msg -->

            <label><%=PasswordChangeLog %></label><br/><br/>
            <div class="center">
                <div class="backgroundButton"><input id="submitChange" value="הרשם" type="submit" name="submit" class="transparentButton"/></div>
            </div>

            </form>
        </div>
        <div class="appointmentsView">
            <h1>הזמנות:</h1>
            <asp:Panel ID="appointmentsArea" runat="server"></asp:Panel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
