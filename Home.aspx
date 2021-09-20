<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="VR_Web_Project.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>דף הבית</title>
    <link rel="stylesheet" href="Stylesheets/HomeStyle.css"/>
</asp:Content>
<asp:Content ID="headerContent" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <nav>
        <ul class="links">
            <li><a href="Home.aspx">בית</a></li>
            <li><a href="#information">מידע</a></li>
        </ul>
    </nav>
    <a class="cta" href="Appointments.aspx">הזמנות</a>
</asp:Content>
<asp:Content ID="titleContent" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    <section>
        <img src="stars.png"/>
            <div id="title">
                <div class="text">
                    <h1 class="title">[כותרת מציאות מדומה]</h1>
                    <a>[טקסט נוסף בית מציאות מדומה]</a>
                </div>
            </div>
        <img src="person.png" id="person"/>
     </section>

    <div class="sec">
        <img src="stars.png"/>
        <div class="secContent">
            <h1 id="information">[מידע על מציאות מדומה]</h1>
            <p>לורם איפסום דולור סיט אמט, קונסקטורר אדיפיסינג אלית קולורס מונפרד אדנדום סילקוף, מרגשי ומרגשח. עמחליף נולום ארווס סאפיאן - פוסיליס קוויס, אקווזמן קוואזי במר מודוף. אודיפו בלאסטיק מונופץ קליר, בנפת נפקט למסון בלרק - וענוף לורם איפסום דולור סיט אמט, קונסקטורר אדיפיסינג אלית. סת אלמנקום ניסי נון ניבאה. דס איאקוליס וולופטה דיאם. וסטיבולום אט דולור, קראס אגת לקטוס וואל אאוגו וסטיבולום סוליסי טידום בעליק. קולהע צופעט למרקוח איבן איף, ברומץ כלרשט מיחוצים. קלאצי סחטיר בלובק. תצטנפל בלינדו למרקל אס לכימפו, דול, צוט ומעיוט - לפתיעם ברשג - ולתיעם גדדיש. קוויז דומור ליאמום בלינך רוגצה. לפמעט
                <br /><br />
        קוואזי במר מודוף. אודיפו בלאסטיק מונופץ קליר, בנפת נפקט למסון בלרק - וענוף לפרומי בלוף קינץ תתיח לרעח. לת צשחמי צש בליא, מנסוטו צמלח לביקו ננבי, צמוקו בלוקריה שיצמה ברורק.
                <br /><br />
        הועניב היושבב שערש שמחויט - שלושע ותלברו חשלו שעותלשך וחאית נובש ערששף. זותה מנק הבקיץ אפאח דלאמת יבש, כאנה ניצאחו נמרגי שהכים תוק, הדש שנרא התידם הכייר וק.
            </p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="info" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="Javascripts/HomeScript.js"></script>
</asp:Content>

