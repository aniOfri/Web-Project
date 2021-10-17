$("td").click(function () {
    if (this.innerText.includes("(")) {
        $.post("GetRecieptAPI.aspx",
            {
                AppointmentId: this.innerText.split(" ")[2]
            }, function (data, status) {
                alert(data);
            })
        }
});