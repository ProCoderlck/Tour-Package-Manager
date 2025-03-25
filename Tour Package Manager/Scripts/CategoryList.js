
$(document).ready(function () {
    ShowCategory()

});

function ShowCategory() {
    try {
        debugger;
        $.post("/Category/ShowCategory", {}, function (response) {
            debugger;
            if (response.responseCode == 200) {
                console.log(response.data)
                if (response.data) {
                    debugger;
                    $("#Data").empty();
                    $.each(response.data, function (i, item) {
                        $("#Data").append(`<tr>
                    <td style="text-align:center">${item.CategoryAutoId}</td>
                    <td>${item.CategoryName}</td>
                    <td>${item.StatusAutoId}</td>
                     <td style="text-align:center;">
                                    <img src="${item.CategoryImage}" height="34" width="42" onerror="this.onerror === null; this.src = funDefaultToolImg();"/>
                      </td>
                    <td style="text-align:center">
                                <a onclick="DeleteCategory(${item.CategoryAutoId});"class="fa fa-trash-o" style='font-size: 20px;color:red;margin-right:10px;'></a>
                                <a href="/Category/Add_Category?CategoryAutoId_Id=${item.CategoryAutoId}" class="fa fa-edit bi bi-eye" style='font-size: 20px;color:Green;margin-right:10px'></a>
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


function DeleteCategory(CategoryAutoId) {
    try {
        Swal.fire({
            text: 'Are you sure to delete this Category ?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.post("/Category/Categorydelete", { CategoryAutoId: CategoryAutoId }, function (response) {
                    if (response.responseCode == 200) {
                        Swal.fire({
                            icon: 'success',
                            text: response.responseMessage,
                        });
                        ShowCategory();
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
