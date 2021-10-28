// Input Validation Variables
var finalReturn = false,
    ccn = document.getElementById("CCN"),
    month = document.getElementById("expireMonth"),
    year = document.getElementById("expireYear"),
    fn = document.getElementById("firstName"),
    ln = document.getElementById("lastName"),
    cvv = document.getElementById("cvv"),
    ccnInvalid = document.getElementById("ccnError");
    monthInvalid = document.getElementById("monthError");
    yearInvalid = document.getElementById("yearError");
    fnInvalid = document.getElementById("fnError");
    lnInvalid = document.getElementById("lnError");
    cvvInvalid = document.getElementById("cvvError");

// Input Validation Function
function PayValid() {
    // too short ccn
    if (ccn.value.length < 16) {
        ccnInvalid.innerHTML = "קלט שגוי.";
        ccn.style.border = "2px dashed blue";
        ccnInvalid.style.display = "inline";
        finalReturn = false;
    }
    else {
        ccn.style.border = "";
        ccnInvalid.style.display = "none";

        // Too long ccn
        if (ccn.value.length > 16) {
            ccnInvalid.innerHTML = "קלט שגוי.";
            ccn.style.border = "2px dashed blue";
            ccnInvalid.style.display = "inline";
            finalReturn = false;
        }
        else {
            ccn.style.border = "";
            ccnInvalid.style.display = "none";

            // too short month
            if (month.value.length < 2) {
                monthInvalid.innerHTML = "קלט שגוי.";
                month.style.border = "2px dashed blue";
                monthInvalid.style.display = "inline";
                finalReturn = false;
            }
            else {
                month.style.border = "";
                monthInvalid.style.display = "none";

                // Too long month
                if (month.value.length > 2) {
                    monthInvalid.innerHTML = "קלט שגוי.";
                    month.style.border = "2px dashed blue";
                    monthInvalid.style.display = "inline";
                    finalReturn = false;
                }
                else {
                    // Too short year
                    if (year.value.length < 2) {
                        yearInvalid.innerHTML = "קלט שגוי.";
                        year.style.border = "2px dashed blue";
                        yearInvalid.style.display = "inline";
                        finalReturn = false;
                    }
                    else {
                        year.style.border = "";
                        yearInvalid.style.display = "none";
                        finalReturn = true;

                        // Too long year
                        if (year.value.length > 2) {
                            yearInvalid.innerHTML = "קלט שגוי.";
                            year.style.border = "2px dashed blue";
                            yearInvalid.style.display = "inline";
                            finalReturn = false;
                        }
                        else {
                            // empty fn
                            if (fn.value.length == 0) {
                                fnInvalid.innerHTML = "קלט שגוי.";
                                fn.style.border = "2px dashed blue";
                                fnInvalid.style.display = "inline";
                                finalReturn = false;
                            }
                            else {
                                fn.style.border = "";
                                fnInvalid.style.display = "none";
                                finalReturn = true;

                                // empty ln
                                if (ln.value.length == 0) {
                                    lnInvalid.innerHTML = "קלט שגוי.";
                                    ln.style.border = "2px dashed blue";
                                    lnInvalid.style.display = "inline";
                                    finalReturn = false;
                                }
                                else {
                                    ln.style.border = "";
                                    lnInvalid.style.display = "none";
                                    finalReturn = true;

                                    // empty cvv
                                    if (cvv.value.length != 3) {
                                        cvvInvalid.innerHTML = "קלט שגוי.";
                                        cvv.style.border = "2px dashed blue";
                                        cvvInvalid.style.display = "inline";
                                        finalReturn = false;
                                    }
                                    else {
                                        cvv.style.border = "";
                                        cvvInvalid.style.display = "none";
                                        finalReturn = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return finalReturn;
}