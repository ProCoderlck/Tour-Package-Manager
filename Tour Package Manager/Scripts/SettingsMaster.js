$(document).ready(function () {

    $("#Update").click(function () {
        debugger;
        try {
            if (!validate()) {
                SettingsUpdate();
            }
            else {
                Swal.fire({
                    icon: 'warning',
                    text: 'Please fill in all fields.'
                });
            }
        }
        catch {

        }
        return false;
    });
    
    EditSetting();
});

function SettingsUpdate() {

    try {
        $.post("/Settings/SettingsUpdate", {
            PhoneNumber: $("#PhoneNumber").val(),
            Email: $("#Email").val(),
            Location: $("#Location").val(),
            Insatgram: $("#Insatgram").val(),
            Facebook: $("#Facebook").val(),
            Twitter: $("#Twitter").val(),
            Youtube: $("#Youtube").val(),
            Linkddin: $("#Linkddin").val()
        }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                Swal.fire({
                    icon: 'success',
                    text: response.responseMessage,
                }).then(() => {
                    window.location.href = "/Settings/Add_Settings";
                });
            }

            else {
                window.location.href = "/";
            }
        });
    } catch (e) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: `Error in InsertUpdateTools: ${e.message}`,
        });
    }
}

function EditSetting() {
    try {
        $.post("/Settings/EditSettingsData", { }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                $("#PhoneNumber").val(response.data.PhoneNumber);
                $("#Email").val(response.data.Email);
                $("#Location").val(response.data.Location);
                $("#Insatgram").val(response.data.Insatgram);
                $("#Facebook").val(response.data.Facebook);
                $("#Twitter").val(response.data.Twitter);
                $("#Youtube").val(response.data.Youtube);
                $("#Linkddin").val(response.data.Linkddin);
            }
            else {
                window.location.href = "/";
            }
        });
    }
    catch (e) {
        alert("Error in MissingToolId" + e.message);
    }
}


function validate() {
    let f = false;
    let PhoneNumber = $("#PhoneNumber").val();
    if (PhoneNumber == "" || PhoneNumber == undefined) {
        $("#PhoneNumber").addClass("border-danger");
        f = true;
    }
    else {
        $("#PhoneNumber").removeClass("border-danger");
    }

    let Email = $("#Email").val();
    if (Email == "" || Email == undefined) {
        $("#Email").addClass("border-danger");
        f = true;
    }
    else {
        $("#Email").removeClass("border-danger");
    }
    let Location = $("#Location").val();
    if (Location == "" || Location == undefined) {
        $("#Location").addClass("border-danger");
        f = true;
    }
    else {
        $("#Location").removeClass("border-danger");
    }
    let Insatgram = $("#Insatgram").val();
    if (Insatgram == "" || Insatgram == undefined) {
        $("#Insatgram").addClass("border-danger");
        f = true;
    }
    else {
        $("#Insatgram").removeClass("border-danger");
    }
    let Facebook = $("#Facebook").val();
    if (Facebook == "" || Facebook == undefined) {
        $("#Facebook").addClass("border-danger");
        f = true;
    }
    else {
        $("#Facebook").removeClass("border-danger");
    }
    let Twitter = $("#Twitter").val();
    if (Twitter == "" || Twitter == undefined) {
        $("#Twitter").addClass("border-danger");
        f = true;
    }
    else {
        $("#Twitter").removeClass("border-danger");
    }

    let Youtube = $("#Youtube").val();
    if (Youtube == "" || Youtube == undefined) {
        $("#Youtube").addClass("border-danger");
        f = true;
    }
    else {
        $("#Youtube").removeClass("border-danger");
    }
    let Linkddin = $("#Linkddin").val();
    if (Linkddin == "" || Linkddin == undefined) {
        $("#Linkddin").addClass("border-danger");
        f = true;
    }
    else {
        $("#Linkddin").removeClass("border-danger");
    }
    return f
}