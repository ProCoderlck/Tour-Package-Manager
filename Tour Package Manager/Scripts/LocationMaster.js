var LocationAutoId = 0;
$(document).ready(function () {

    ImageUpload('LocationImage', 'photoBox');

    
    $("#Save").click(function () {
        debugger;
        try {
            if (!validate()) {
                InsertUpdateLocation();
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
    if (searchParams.has('LocationAutoId_Id')) {
        EditLocation(searchParams.get('LocationAutoId_Id'));
    }
    else {
        bindUserStatus();
    }
});

function InsertUpdateLocation() {
    debugger;
    try {
        $.post("/Location/LocationInsertUpdate", {
            LocationAutoId: LocationAutoId,
            LocationName: $("#LocationName").val(),
            StatusAutoId: $("#Status").val(),
            LocationImage: $("#photoBox img").attr('src')

        })
            .done(function (data) {
                debugger;
                if (data && data.Message) {
                    Swal.fire({
                     
                        text: data.Message,
                        icon: "success",
                        confirmButtonColor: "#3085d6"
                    }).then(() => {
                        // Redirect after the user clicks OK
                        window.location.href = "/Location/Location_List";
                    });
                } else {
                    Swal.fire({
                        title: "Error!",
                        text: "Unexpected response from server.",
                        icon: "error",
                        confirmButtonColor: "#d33"
                    });
                }
            })
            .fail(function (xhr, status, error) {
                Swal.fire({
                    title: "Error!",
                    text: "Failed to update location: " + error,
                    icon: "error",
                    confirmButtonColor: "#d33"
                });
            });
    } catch (e) {
        Swal.fire({
            title: "Error!",
            text: "Exception occurred: " + e.message,
            icon: "error",
            confirmButtonColor: "#d33"
        });
    }
}


function EditLocation(LocationAutoId_Id) {
    LocationAutoId = LocationAutoId_Id;
    try {
        $.post("/Location/EditLocationData", { LocationAutoId: LocationAutoId_Id }, function (response) {
            debugger;
            if (response) {
                $("#LocationName").val(response.LocationName);
                bindUserStatus(response.StatusAutoId);
                $("#photoBox").empty();
                $("#photoBox").append(`<img onerror="this.onerror===null;this.src=funDefaultUserImg()" src="${response.LocationImage}" alt="Selected Photo">`);
                $("#Save").text("Update");
            }
        });
    }
    catch (e) {
        alert("Error in LocationAutoId" + e.message);
    }
}


function validate() {
    let f = false;
    let LocationName = $("#LocationName").val();
    if (LocationName == "" || LocationName == undefined) {
        $("#LocationName").addClass("border-danger");
        f = true;
    }
    else {
        $("#LocationName").removeClass("border-danger");
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