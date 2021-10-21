<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="VR_Web_Project.Payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>תשלום</title>
    <link rel="stylesheet" href="Stylesheets/PaymentStyle.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
        <nav>
        <ul class="links">
            <li><a href="Payment.aspx">תשלום</a></li>
            <!--<li><a href="#information">מידע</a></li>-->
        </ul>
    </nav>
    <a class="cta" href="Home.aspx">בית</a>
    <a class="cta" href="Appointments.aspx">הזמנות</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    <img src="stars.png" class="stars"/>
    <div class="formDiv">
        <form id="formLogin" onsubmit="return PayValid();" method="get">
            
            <div class="sidebyside">
                <div><h1 class="title">תשלום</h1></div>
             </div>

            <label for="textLogin">מספר כרטיס אשראי:</label><br />
            <input id="CCN" type="text" name="CCN" /><br />
            <a class="errorMsgs" id="ccnError"></a><br /> <!--- Error msg -->

            <label for="expireMonth">חודש תפוגה:</label> <br />
            <input id="month" type="text" name="month"/><br />
            <a class="errorMsgs" id="monthError"></a><br /> <!--- Error msg -->

            <label for="expireYear">שנת תפוגה:</label> <br />
            <input id="year" type="text" name="year"/><br />
            <a class="errorMsgs" id="yearError"></a><br /> <!--- Error msg -->

            <label for="firstName">שם פרטי:</label> <br />
            <input id="fn" type="text" name="fn"/><br />
            <a class="errorMsgs" id="fnError"></a><br /> <!--- Error msg -->

            <label for="lastName">שם משפחה:</label> <br />
            <input id="ln" type="text" name="ln"/><br />
            <a class="errorMsgs" id="lnError"></a><br /> <!--- Error msg -->
            
            <label for="cvv">CVV:</label> <br />
            <input id="cvv" type="text" name="cvv"/><br />
            <a class="errorMsgs" id="cvvError"></a><br /> <!--- Error msg -->
           
            <label for="giftcard">כרטיס מתנה:</label> <br />
            <input id="giftcard" type="text" name="cvv"/><br />
            <label id="giftCardInfo">(ימומש אם תקין)</label> <br />
           
            <label><%=PayStatus%></label><br /><br />
            <div class="center">
                <div class="backgroundButton"><input id="submitLogin" value="שלם" type="submit" name="submit" class="transparentButton" /></div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="Javascripts/PaymentScript.js"></script>
</asp:Content>
