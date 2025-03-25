
$(document).ready(function () {
    ShowPackage();
});


function ShowPackage() {
    try {
        debugger;
        $.post("/Package/ShowPackage", {}, function (data) {
            debugger;
            if (data) {
                $.each(data, function (i, item) {
                    $("#Data").append(`<tr> 
                     <td style="text-align:center">
                                <a onclick="Packagedelete(${item.PackageAutoId});"class="fa fa-trash-o" style='font-size: 20px;color:red;margin-right:10px;'></a>
                                <a href="/Package/Add_Package?PackageAutoId_Id=${item.PackageAutoId}" class="fa fa-edit bi bi-eye" style='font-size: 20px;color:Green;margin-right:10px'></a>
                    </td>
                    <td style="text-align:center">${item.PackageAutoId}</td>  
                   
                    <td>${item.DurationAutoId}</td>
                    <td>${item.CatagoryAutoId}</td>
                    <td>${item.LocationAutoId}</td>
                     <td>${item.PackageName}</td>

                   <td style="text-align:center">${item.Price}</td>

                       <td style="text-align:center;">
                                    <img src="${item.PackageImage}" height="34" width="42" onerror="this.onerror === null; this.src = funDefaultToolImg();"/>
                      </td>
                    <td>${item.StatusAutoId}</td>
                   
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

function Packagedelete(PackageAutoId) {
    try {
        // SweetAlert2 confirmation prompt
        Swal.fire({
            text: "Are you sure you want to delete this package?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it",
            cancelButtonText: "Cancel"
        }).then((result) => {
            if (result.isConfirmed) {
                // Perform the AJAX request to delete the package
                $.post("/Package/Packagedelete", { PackageAutoId: PackageAutoId })
                    .done(function (response) {
                        debugger;
                        if (response && response.Message) {
                            // Success feedback
                            Swal.fire({
                                text: response.Message,
                                icon: "success",
                                confirmButtonColor: "#3085d6"
                            }).then(() => {
                                // Redirect after showing success message
                                window.location.href = "/Package/Package_List";
                            });
                        } else {
                            // Handle unexpected response
                            Swal.fire({
                                title: "Error!",
                                text: "Unexpected response from server.",
                                icon: "error",
                                confirmButtonColor: "#d33"
                            });
                        }
                    })
                    .fail(function (xhr, status, error) {
                        // Error feedback
                        Swal.fire({
                            title: "Error!",
                            text: "There was a problem deleting the package: " + error,
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
            text: "Exception occurred: " + e.message,
            icon: "error",
            confirmButtonColor: "#d33"
        });
    }
}
