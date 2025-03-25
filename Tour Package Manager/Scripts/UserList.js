
$(document).ready(function () {
    ShowUser()

});


function ShowUser() {
    try {
        debugger;
        $.post("/User/ShowUser", {}, function (response) {
            debugger;
            if (response.responseCode == 200) {
                console.log(response.data)
                if (response.data) {
                    $("#Data").empty();
                    $.each(response.data, function (i, item) {
                        $("#Data").append(`<tr>
                    <td style="text-align:center">${item.UserAutoId}</td>
                    <td style="text-align:center">${item.UserName}</td>
                    <td style="text-align:center">${item.MobileNo}</td>
                    <td style="text-align:center">${item.Email}</td>
                    <td style="text-align:center">${item.StatusAutoId}</td>
                     <td style="text-align:center;">
                                    <img src="${item.UserImage}" height="34" width="42" onerror="this.onerror === null; this.src = funDefaultToolImg();"/>
                      </td>
                    <td style="text-align:center">
                                <a onclick="DeleteUser(${item.UserAutoId});"class="fa fa-trash-o" style='font-size: 20px;color:red;margin-right:10px;'></a>
                                <a href="/User/Add_User?UserAutoId_Id=${item.UserAutoId}" class="fa fa-edit bi bi-eye" style='font-size: 20px;color:Green;margin-right:10px'></a>
                    </td>
                    </tr>`
                        );
                    });
                }
            }

            else {
                window.location.href = "/";
            }
        });
    }
    catch (e) {
        (alert("error" + e.Message))
    }
}


function DeleteUser(UserAutoId) {
    try {
        Swal.fire({
            text: 'Are you sure to delete this user ?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.post("../User/Userdelete", { UserAutoId: UserAutoId }, function (response) {
                    if (response.responseCode == 200) {
                        Swal.fire({
                            icon: 'success',
                            text: response.responseMessage,
                        });
                        ShowUser();
                    }
                    
                    else {
                        window.location.href = "/";
                    }
                }).fail(function () {
                    Swal.fire({
                        title: 'Error!',
                        text: 'Failed to delete the item.',
                        icon: 'error',
                        confirmButtonText: 'OK'
                    });
                });
            }
        });
    } catch (e) {
        Swal.fire({
            title: 'Error!',
            text: 'Error in DeleteToolData: ' + e.message,
            icon: 'error',
            confirmButtonText: 'OK'
        });
    }
}