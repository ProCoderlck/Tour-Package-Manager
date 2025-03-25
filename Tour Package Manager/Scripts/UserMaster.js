var UserAutoId = 0;
$(document).ready(function () {

    ImageUpload('UserImage', 'photoBox');

    
    $("#Save").click(function () {
        debugger;
        try {
            if (!validate()) {
                InsertUpdateUser();
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
    const searchParams = new URLSearchParams(window.location.search);
    debugger;
    if (searchParams.has('UserAutoId_Id')) {
        EditUser(searchParams.get('UserAutoId_Id'));
    }
    else {
        bindUserStatus();
    }
});
function InsertUpdateUser() {

    try {
        $.post("/User/UserInsertUpdate", {
            UserAutoId: UserAutoId,
            UserName: $("#UserName").val(),
            MobileNo: $("#MobileNo").val(),
            Email: $("#Email").val(),
            Password: $("#Password").val(),
            UserImage: $("#photoBox img").attr('src'),

            StatusAutoId: $("#Status").val()
        }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                Swal.fire({
                    icon: 'success',
                    text: response.responseMessage,
                }).then(() => {
                    window.location.href = "/User/User_List";
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



function EditUser(UserAutoId_Id) {
    UserAutoId = UserAutoId_Id;
    try {
        $.post("/User/EditUserData", { UserAutoId: UserAutoId_Id }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                $("#UserName").val(response.data.UserName);
                $("#MobileNo").val(response.data.MobileNo);
                $("#Email").val(response.data.Email);
                $("#Password").val(response.data.Password);
                bindUserStatus(response.data.StatusAutoId);
                $("#photoBox").empty();
                $("#photoBox").append(`<img onerror="this.onerror===null;this.src=funDefaultUserImg()" src="${response.data.UserImage}" alt="Selected Photo">`);

                $("#Save").text("Update");
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
    let UserName = $("#UserName").val();
    if (UserName == "" || UserName == undefined) {
        $("#UserName").addClass("border-danger");
        f = true;
    }
    else {
        $("#UserName").removeClass("border-danger");
    }

    let MobileNo = $("#MobileNo").val();
    if (MobileNo == "" || MobileNo == undefined) {
        $("#MobileNo").addClass("border-danger");
        f = true;
    }
    else {
        $("#MobileNo").removeClass("border-danger");
    }
    let Email = $("#Email").val();
    if (Email == "" || Email == undefined) {
        $("#Email").addClass("border-danger");
        f = true;
    }
    else {
        $("#Email").removeClass("border-danger");
    }
    let Password = $("#Password").val();
    if (Password == "" || Password == undefined) {
        $("#Password").addClass("border-danger");
        f = true;
    }
    else {
        $("#Password").removeClass("border-danger");
    }
    let Status = $("#Status").val();
    if (Status == "" || Status == undefined) {
        $("#Status").addClass("border-danger");
        f = true;
    }
    else {
        $("#Status").removeClass("border-danger");
    }


    return f
}

    function togglePasswordVisibility() {
        var passwordField = document.getElementById('Password');
        var icon = document.getElementById('togglePassword').querySelector('i');

        if (passwordField.type === 'password') {
            passwordField.type = 'text';
            icon.classList.remove('fa-eye');
            icon.classList.add('fa-eye-slash');
        } else {
            passwordField.type = 'password';
            icon.classList.remove('fa-eye-slash');
            icon.classList.add('fa-eye');
        }
    }