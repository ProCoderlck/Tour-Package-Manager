
$(document).ready(function () {



    $("#sentmessage").click(function () {
        debugger;
    
        try {
            if (!validate()) {
                InsertUpdateContact();
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
  
});

function InsertUpdateContact() {
    debugger;
    try {
        $.post("/ContactWeb/SentMessage", {
            Name: $("#Name").val(),
            Email: $("#Email").val(),
            Subject: $("#Subject").val(),
            ContactMessage: $("#ContactMessage").val()
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
                        clearMessagebox();
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

function clearMessagebox() {
    $("#Name").val("");
    $("#Email").val("");
    $("#Subject").val("");
    $("#ContactMessage").val("");
}
function validate() {
    let f = false;
    let Name = $("#Name").val();
    if (Name == "" || Name == undefined) {
        $("#Name").addClass("border-danger");
        f = true;
    }
    else {
        $("#Name").removeClass("border-danger");
    }
    let Email = $("#Email").val();
    if (Email == "" || Email == undefined) {
        $("#Email").addClass("border-danger");
        f = true;
    }
    else {
        $("#Email").removeClass("border-danger");
    }
    let Subject = $("#Subject").val();
    if (Subject == "" || Subject == undefined) {
        $("#Subject").addClass("border-danger");
        f = true;
    }
    else {
        $("#Subject").removeClass("border-danger");
    }

   


    return f
}