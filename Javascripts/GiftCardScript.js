// Get the modal
var modal = document.getElementById("TitlePlaceHolder_myModal");

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.className = "modal invisible";
    }
}

$("#emailButton").click(function () {
    if (document.getElementById("TitlePlaceHolder_popupInfo").innerHTML != "") {
    $.post("SendEmailAPI.aspx",
        {
            EmailAddr: document.getElementById("emailText").value,
            EmailSubject: "כרטיס מתנה",
            Code: document.getElementById("TitlePlaceHolder_popupInfo").innerHTML
        }, function (data, status) {
            alert("נשלח!");
    })
    }
});