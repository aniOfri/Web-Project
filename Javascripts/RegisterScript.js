// Input Validation Variables
var finalReturn = false,
    textLog = document.getElementById("textRegister"),
    phoneReg = document.getElementById("phoneRegister"),
    passRegister = document.getElementById("passRegister"),
    passRegister2 = document.getElementById("passRegister2"),
    textInvalid = document.getElementById("regTextError");
phoneInvalid = document.getElementById("regPhoneError");
passInvalid = document.getElementById("regPassError");
passConfInvalid = document.getElementById("regPassConfError");
terms = document.getElementById("terms");
termsInvalid = document.getElementById("termsError");

// Input Validation Function
function RegValid() {
    // Empty username 
    if (textLog.value.length == 0) {
        textInvalid.innerHTML = "שם משתמש לא יכול להיות ריק";
        textLog.style.border = "2px dashed blue";
        textInvalid.style.display = "inline";
        finalReturn = false;
    }
    else {
        textLog.style.border = "";
        textInvalid.style.display = "none";

        // Too short username
        if (textLog.value.length < 3) {
            textInvalid.innerHTML = "שם משתמש לא יכול להיות קצר מ3 אותיות";
            textLog.style.border = "2px dashed blue";
            textInvalid.style.display = "inline";
            finalReturn = false;
        }
        else {
            // Too long username
            if (textLog.value.length > 9) {
                textInvalid.innerHTML = "שם משתמש לא יכול להיות ארוך מ8 אותיות.";
                textLog.style.border = "2px dashed blue";
                textInvalid.style.display = "inline";
                finalReturn = false;
            }
            else {
                textLog.style.border = "";
                textInvalid.style.display = "none";

                // Empty phone number
                if (phoneReg.value.length == 0) {
                    phoneInvalid.innerHTML = "מספר טלפון לא יכול להיות ריק";
                    phoneReg.style.border = "2px dashed blue";
                    phoneInvalid.style.display = "inline";
                    finalReturn = false;
                }
                else {
                    phoneReg.style.border = "";
                    phoneInvalid.style.display = "none";

                    // Validate phone number
                    if (validatePhone(phoneReg.value)) {
                        phoneInvalid.innerHTML = "מספר טלפון שגוי";
                        phoneReg.style.border = "2px dashed blue";
                        phoneInvalid.style.display = "inline";
                        finalReturn = false;
                    }
                    else {
                        phoneReg.style.border = "";
                        phoneInvalid.style.display = "none";

                        // Empty Password
                        if (passRegister.value.length == 0) {
                            passInvalid.innerHTML = "סיסמה לא יכולה להיות ריקה";
                            passRegister.style.border = "2px dashed blue";
                            passInvalid.style.display = "inline";
                            finalReturn = false;
                        }
                        else {
                            passRegister.style.border = "";
                            passInvalid.style.display = "none";

                            // Too short password
                            if (passRegister.value.length < 5) {
                                passInvalid.innerHTML = "סיסמה לא יכול להיות קצרה מ5 אותיות";
                                passRegister.style.border = "2px dashed blue";
                                passInvalid.style.display = "inline";
                                finalReturn = false;
                            }
                            else {
                                // Too long password
                                if (passRegister.value.length > 16) {
                                    passInvalid.innerHTML = "סיסמה לא יכול להיות ארוכה מ16";
                                    passRegister.style.border = "2px dashed blue";
                                    passInvalid.style.display = "inline";
                                    finalReturn = false;
                                }
                                else {
                                    passRegister.style.border = "";
                                    passInvalid.style.display = "none";

                                    if (passRegister.value != passRegister2.value) {
                                        passConfInvalid.innerHTML = "סיסמאות לא תואמות";
                                        passRegister.style.border = "2px dashed blue";
                                        passRegister2.style.border = "2px dashed blue";
                                        passConfInvalid.style.display = "inline";
                                        finalReturn = false;
                                    }
                                    else {
                                        passRegister2.style.border = "";
                                        passConfInvalid.style.display = "none";
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

function validatePhone(phone) {
    return !/^05[0-9]\d{7}$/.test(phone)
}