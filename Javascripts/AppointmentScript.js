$("input.transparentButton").hover(function () {
    if ($(this)[0]["defaultValue"].includes("לא זמין")) {
            $(this).parent().css("background-color", "#34495e");
            $(this).prop('disabled', true);
        }
});

