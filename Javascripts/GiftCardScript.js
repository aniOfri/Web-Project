// Get the modal
var modal = document.getElementById("TitlePlaceHolder_myModal");

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.className = "modal invisible";
    }
}   