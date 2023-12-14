
function checkForm(form) {
    //Checking the username and password from the form the client filled up
    var checkUsername = document.getElementById("checkUsername");
    var checkPassword = document.getElementById("checkPassword");
    var msg = document.getElementById("msg");

    checkUsername.innerHTML = "";

    //if both forms are filled up, return true
    if (msg != null && msg != "") {
        msg.innerHTML = "";
    }
    //if username form is empty, return false
    if (form.username.value == "") {
        checkUsername.innerHTML = "Username cannot be null."
        form.username.focus();
        return false;
    }

    //if password form is empty, return false
    if (form.password.value == "") {
        checkPassword.innerHTML = "Password cannot be null."
        form.password.focus();
        return false;
    }
    return true;
}

window.onload = function () {
    //forms[0] means the first form in the page which will run this function.
    var myform = document.forms[0];
    var username = document.getElementById("usernameId");
    var password = document.getElementById("passwordId");

    //Submitting the form
    myform.onsubmit = function () {
        return checkForm(this);
    }
    //Checking the user Id from the form
    username.onblur = function () {
        return checkForm(myform);
    }
    //Checking the password from the form
    password.onblur = function () {
        return checkForm(myform);
    }



}
