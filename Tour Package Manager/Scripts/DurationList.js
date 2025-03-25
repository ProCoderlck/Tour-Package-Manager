
$(document).ready(function () {
    ShowDuration()

});

function ShowDuration() {
    try {
        debugger;
        $.post("/Duration/ShowDuration", {}, function (data) {
            debugger;
            if (data) {
                debugger;
                $.each(data, function (i, item) {
                    $("#Data").append(`<tr>
                    <td style="text-align:center">${item.DurationAutoId}</td>
                    <td>${item.DurationName}</td>
                    <td>${item.StatusAutoId}</td>
                    <td style="text-align:center">
                                <a onclick="DeleteDuration(${item.DurationAutoId});"class="fa fa-trash-o" style='font-size: 20px;color:red;margin-right:10px;'></a>
                                <a href="/Duration/Add_Duration?DurationAutoId_Id=${item.DurationAutoId}" class="fa fa-edit bi bi-eye" style='font-size: 20px;color:Green;margin-right:10px'></a>
                    </td>
                    </tr>`
                    );
                });
            }
        });
    }
    catch (e) {
        (alert("error" + e.Message))
    }
}

function DeleteDuration(DurationAutoId) {
    try {
        // SweetAlert2 confirmation prompt
        Swal.fire({
            text: "Are you sure you want to delete this Duration?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it",
            cancelButtonText: "Cancel"
        }).then((result) => {
            if (result.isConfirmed) {
                // Perform the AJAX request to delete the duration
                $.post("/Duration/Durationdelete", { DurationAutoId: DurationAutoId })
                    .done(function (response) {
                        debugger;
                        if (response && response.Message) {
                            // Success feedback
                            Swal.fire({
                                text: response.Message,
                                icon: "success",
                                confirmButtonColor: "#3085d6"
                            }).then(() => {
                                // Redirect after clicking OK
                                window.location.href = "/Duration/Duration_List";
                            });
                        } else {
                            Swal.fire({
                                title: "Error!",
                                text: "No response message received. The deletion may have failed.",
                                icon: "error",
                                confirmButtonColor: "#d33"
                            });
                        }
                    })
                    .fail(function () {
                        // Error feedback
                        Swal.fire({
                            title: "Error!",
                            text: "There was a problem deleting the duration.",
                            icon: "error",
                            confirmButtonColor: "#d33"
                        });
                    });
            }
        });
    } catch (e) {
        // Catch and show errors
        Swal.fire({
            title: "Error!",
            text: "Error in DeleteDuration: " + e.message,
            icon: "error",
            confirmButtonColor: "#d33"
        });
    }
}


