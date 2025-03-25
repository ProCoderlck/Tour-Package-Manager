var DurationAutoId = 0;
$(document).ready(function () {



    $("#Save").click(function () {
        debugger;
        try {
            if (!validate()) {
                InsertUpdateDuration();
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
    if (searchParams.has('DurationAutoId_Id')) {
        EditDuration(searchParams.get('DurationAutoId_Id'));
    }
    else {
        bindUserStatus();
    }
});

function InsertUpdateDuration() {
    debugger;
    try {
        $.post("/Duration/DurationInsertUpdate", {
            DurationAutoId: DurationAutoId,
            DurationName: $("#DurationName").val(),
            StatusAutoId: $("#Status").val()
        })
            .done(function (data) {
                debugger;
                if (data && data.Message) {
                    // Show success message before redirecting
                    Swal.fire({
                        text: data.Message,
                        icon: "success",
                        confirmButtonColor: "#3085d6"
                    }).then(() => {
                        window.location.href = "/Duration/Duration_List"; // Redirect after OK
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
                // Show error message
                Swal.fire({
                    title: "Error!",
                    text: "Failed to save duration: " + error,
                    icon: "error",
                    confirmButtonColor: "#d33"
                });
            });
    } catch (e) {
        // Catch and show exceptions
        Swal.fire({
            title: "Error!",
            text: "Exception occurred: " + e.message,
            icon: "error",
            confirmButtonColor: "#d33"
        });
    }
}

function EditDuration(DurationAutoId_Id) {
    DurationAutoId = DurationAutoId_Id;
    try {
        $.post("/Duration/EditDurationData", { DurationAutoId: DurationAutoId_Id }, function (response) {
            debugger;
            if (response) {
                $("#DurationName").val(response.DurationName);
                bindUserStatus(response.StatusAutoId);
                $("#Save").text("Update");
            }
        });
    }
    catch (e) {
        alert("Error in DurationAutoId" + e.message);
    }
}
function validate() {
    let f = false;
    let DurationName = $("#DurationName").val();
    if (DurationName == "" || DurationName == undefined) {
        $("#DurationName").addClass("border-danger");
        f = true;
    }
    else {
        $("#DurationName").removeClass("border-danger");
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