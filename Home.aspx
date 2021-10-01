<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="VR_Web_Project.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>דף הבית</title>
    <link rel="stylesheet" href="Stylesheets/HomeStyle.css"/>
</asp:Content>
<asp:Content ID="headerContent" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <nav>
        <ul class="links resetpadding">
            <li><a href="Home.aspx">בית</a></li>
            <li><a href="#information">מידע</a></li>
        </ul>
    </nav>
    <a class="cta buttons" href="Appointments.aspx">הזמנות</a>
</asp:Content>
<asp:Content ID="titleContent" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    <section>
        <img src="stars.png"/>
            <div id="title">
                <div class="text">
                    <h1 class="title">הזמנה לעולם אחר</h1>
                    <a class="title">מתחם מציאות מדומה לא מהעולם הזה</a>
                </div>
            </div>
        <img src="person.png" id="person"/>
     </section>

    <div class="sec">
        <img src="stars.png"/>
        <div class="secContent">
            <br /><br /><br />  
            <h1 id="information">מידע על מציאות מדומה</h1>
            <p>
                מציאות מדומה היא סימולציה של סביבה, המתבצעת באמצעות מחשב, קונסולה ייעודית או טלפון נייד, באופן הנותן למשתמש אשליה כי הוא נמצא בסביבה המציאותית אותה מדמה המחשב. מציאות מדומה משמשת לצורכי בידור, ללימוד ולאימון.
            </p>
            <br />
            <p>
                אצלנו במתחם תוכלו לשחק, לחקור ולצפות במגוון אפשרויות שמשקפי מציאות המדומה מציעות,
            </p>
            <br />
            
            <h1 class="title">בין המשחקים יש לנו את:</h1><br />
            <div class="games">
                <img class="game" src="Sources/beatsaber.jpg"/>
                <img class="game" src="Sources/minecraft.png"/>
                <img class="game" src="Sources/pistolwhip.jpg"/>
                <img class="game" src="Sources/superhot.jpg"/>
                <img class="game" src="Sources/coc.jpg"/>
                <img class="game" src="Sources/agario.png"/>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="info" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="Javascripts/HomeScript.js"></script>
</asp:Content>

