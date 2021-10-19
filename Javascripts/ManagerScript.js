// Get the modal
var modal = document.getElementById("myModal");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

var p = document.getElementsByClassName("popupData")[0];

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

$("td").click(function () {
    if (this.innerText.includes("(")) {
        $.post("GetRecieptAPI.aspx",
            {
                AppointmentId: this.innerText.split(" ")[2]
            }, function (data, status) {
                console.log(data);

                data = JSON.parse(data);

                let str = "";
                str += "תאריך הזמנה: " + data["date"] + " \n ";
                str += "שעת הזמנה: " + data["time"] + " \n ";
                str += "מחיר: " + data["price"] + " \n ";
                str += "כרטיס אשראי: " + data["ccn"] + "* * * * \n ";
                str += "שם פרטי: " + data["firstname"] + " \n ";
                str += "שם משפחה: " + data["lastname"] + " \n";;

                p.innerText = str;

                modal.style.display = "block";
            })
        }
});