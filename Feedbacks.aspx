<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="Feedbacks.aspx.cs" Inherits="VR_Web_Project.Feedbacks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ביקורות</title>
    <link rel="stylesheet" href="Stylesheets/FeedbackStyle.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
        <nav>
        <ul class="links">
            <li><a href="Feedback.aspx">ביקורות</a></li>
            <!--<li><a href="#information">מידע</a></li>-->
        </ul>
    </nav>
    <a class="cta" href="Home.aspx">בית</a>
    <a class="cta" href="Appointments.aspx">הזמנות</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
        <img src="Sources/stars.png" class="stars"/>
        <div class="wrapper">
            <div class="feedbacks">
                <h1>ביקורות:</h1>
                <div class="buttons">
                    <asp:Button runat="server" CustomParameter="-1" Text="<" OnClick="Feedback_Click"/>
                    <asp:Button runat="server" CustomParameter="1" Text=">" OnClick="Feedback_Click"/>
                </div>
                <asp:Panel ID="feedBackArea" runat="server"></asp:Panel>
            </div>
            <div class="feedback">
                <h1>השאר ביקורת:</h1>

                <label for="stars">דירוג בכוכבים:</label> <br />
                <div class="starsFeedback">
                    <a>1</a>
                    <input id="starsFeedback1" type="radio" name="stars" value="1"/><br />
                    <input id="starsFeedback2" type="radio" name="stars" value="2"/><br />
                    <input id="starsFeedback3" type="radio" name="stars" value="3"/><br />
                    <input id="starsFeedback4" type="radio" name="stars" value="4"/><br />
                    <input id="starsFeedback5" type="radio" name="stars" value="5"/><br />
                    <a>5</a>
                </div>

                <a class="errorMsgs" id="starsError"></a><br /> <!--- Error msg -->
            
                <label for="content">תוכן:</label> <br />
                <textarea id="content" name="content" maxlength="120"></textarea><br />
                <a class="errorMsgs" id="cvvError"></a><br /> <!--- Error msg -->
           
                <label><%=FeedbackStatus%></label><br /><br />
                <div class="center">
                    <div class="backgroundButton"><input id="submitLogin" value="שלח" type="submit" name="submit" class="transparentButton" /></div>
                </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
