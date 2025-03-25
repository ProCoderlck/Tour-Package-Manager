$(document).ready(function () {
    ShowGuide()

});
function ShowGuide() {
    try {
        debugger;
        $.post("/Guides/ShowGuide ", {}, function (response) {
            debugger;
            if (response.responseCode == 200) {
                console.log(response.data)
                if (response.data) {
                    $("#Data").empty();
                    $.each(response.data, function (i, item) {
                        $("#Data").append(`<tr>
                     <td style="text-align:center">
                                <a onclick="DeleteGuide(${item.GuideAutoId});"class="fa fa-trash-o" style='font-size: 20px;color:red;margin-right:10px;'></a>
                                <a href="/Guides/Add_Guides?GuideAutoId_Id=${item.GuideAutoId}" class="fa fa-edit bi bi-eye" style='font-size: 20px;color:Green;margin-right:10px'></a>
                    </td>
                    <td style="text-align:center">${item.GuideAutoId}</td>
                    <td style="text-align:center">${item.GuideName}</td>
                    <td style="text-align:center">${item.Designation}</td>
                     <td style="text-align:center">
                                    <img src="${item.Photo}" height="34" width="42" onerror="this.onerror === null; this.src = funDefaultToolImg();"/>
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


function DeleteGuide(GuideAutoId) {
    try {
        Swal.fire({
            text: 'Are you sure to delete this Guide ?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.post("/Guides/Guidedelete", { GuideAutoId: GuideAutoId }, function (response) {
                    if (response.responseCode == 200) {
                        Swal.fire({
                            icon: 'success',
                            text: response.responseMessage,
                        });
                        ShowGuide();
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
