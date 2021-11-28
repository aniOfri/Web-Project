<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="GiftCard.aspx.cs" Inherits="VR_Web_Project.GiftCard1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>קניית כרטיס מתנה</title>
    <link rel="stylesheet" href="Stylesheets/GiftCardStyle.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <nav>
        <ul class="links">
            <li><a href="GiftCard.aspx">כרטיס מתנה</a></li>
        </ul>
    </nav>
    <a class="cta" href="Home.aspx">בית</a>
    <a class="cta" href="Appointments.aspx">הזמנות</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    <img src="stars.png" class="stars"/>
    
    <div id="myModal" class="modal invisible" runat="server">
      <div class="modal-content">
        <div class="modal-header">
            <span class="close"></span>
            <h2 class="popupTitle">קוד כרטיס המתנה (חד פעמי):</h2>
        </div>
            <asp:Label id="popupInfo" runat="server"/>
            <h2 class="info">מומלץ לצלם!</h2>
      </div>
    </div>
    
    <br />
    <div class="formDiv">
            <div class="sidebyside">
                <div><h1 class="title">קניית כרטיס מתנה</h1></div>
                <div><a href="Appointments.aspx"><h2>או הזמנה רגילה</h2></a></div>
             </div><br />
            <div id="choices">
                <div id="giftCard1" class="radio-inline">
                <label>משתתף אחד</label><br />
                <img class="game" src=""/><br />
                    <input type="radio" id="radioGiftCard1"
                        name="giftCard" value="140₪">
                    <label for="radioGiftCard1">140₪</label>
                </div>
                <div id="giftCard2" class="radio-inline">
                <label>שני משתתפים</label><br />
                <img class="game" src=""/><br />
                    <input type="radio" id="radioGiftCard2"
                        name="giftCard" value="240₪">
                    <label for="radioGiftCard2">240₪</label>
                </div>
                    <div id="giftCard3" class="radio-inline">
                <label>שלושה משתתפים</label><br />
                <img class="game" src=""/><br />
                    <input type="radio" id="radioGiftCard3"
                        name="giftCard" value="345₪">
                    <label for="radioGiftCard3">345₪</label>
                </div>
                <div id="giftCard4" class="radio-inline">
                <label>ארבעה משתתפים</label><br />
                <img class="game" src=""/><br />
                    <input type="radio" id="radioGiftCard4"
                        name="giftCard" value="440₪">
                    <label for="radioGiftCard4">440₪</label>
                </div>
                <div id="giftCard5" class="radio-inline">
                <label>חמישה משתתפים</label><br />
                <img class="game" src=""/><br />
                    <input type="radio" id="radioGiftCard5"
                        name="giftCard" value="525₪">
                    <label for="radioGiftCard5">525₪</label>
                </div>
                <div id="giftCard6" class="radio-inline">
                <label>שישה משתתפים</label><br />
                <img class="game" src=""/><br />
                    <input type="radio" id="radioGiftCard6"
                        name="giftCard" value="600₪">
                    <label for="radioGiftCard6">600₪</label>
                </div>
            </div>

            <br /><br />
            <div class="center">
                <div class="backgroundButton"><input id="submit" value="קנה" type="submit" name="submit" class="transparentButton" /></div>
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="Javascripts/GiftCardScript.js"></script>
</asp:Content>
