
$(document).ready(function () {
    ShowLocation()

});

function ShowLocation() {
    try {
        debugger;
        $.post("/Location/ShowLocation", {}, function (data) {
            debugger;
            if (data) {
                debugger;
                $.each(data, function (i, item) {
                    $("#Data").append(`<tr>
                    <td style="text-align:center">${item.LocationAutoId}</td>
                    <td>${item.LocationName}</td>
                    <td>${item.StatusAutoId}</td>
                     <td style="text-align:center;">
                                    <img src="${item.LocationImage}" height="34" width="42" onerror="this.onerror === null; this.src = funDefaultToolImg();"/>
                      </td>
                    <td style="text-align:center">
                                <a onclick="Locationdelete(${item.LocationAutoId});"class="fa fa-trash-o" style='font-size: 20px;color:red;margin-right:10px;'></a>
                                <a href="/Location/Add_Location?LocationAutoId_Id=${item.LocationAutoId}" class="fa fa-edit bi bi-eye" style='font-size: 20px;color:Green;margin-right:10px'></a>
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



function Locationdelete(LocationAutoId) {
    try {
        // SweetAlert2 confirmation prompt
        Swal.fire({
            text: "Are you sure you want to delete this Location?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it",
            cancelButtonText: "Cancel"
        }).then((result) => {
            if (result.isConfirmed) {
                // Perform the AJAX request to delete the location
                $.post("/Location/Locationdelete", { LocationAutoId: LocationAutoId }, function (response) {
                    debugger;
                    // Ensure response is valid and check for success message
                    if (response && response.Message) {
                        // Success feedback
                        Swal.fire({
                            text: response.Message,
                            icon: "success",
                            confirmButtonColor: "#3085d6"
                        }).then(() => {
                            // Redirect after confirmation
                            window.location.href = "/Location/Location_List";
                        });
                    } else {
                        // If response does not contain a message, show a generic success message
                        Swal.fire({
                            text: "Location deleted successfully!",
                            icon: "success",
                            confirmButtonColor: "#3085d6"
                        }).then(() => {
                            window.location.href = "/Location/Location_List";
                        });
                    }
                }).fail(function () {
                    // Error feedback
                    Swal.fire({
                        title: "Error!",
                        text: "There was a problem deleting the Location.",
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
            text: "Error in Location deletion: " + e.message,
            icon: "error",
            confirmButtonColor: "#d33"
        });
    }
}
