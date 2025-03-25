var GuideAutoId = 0;
$(document).ready(function () {

    ImageUpload('Photo', 'photoBox');
    $("#Save").click(function () {
        debugger;
        try {
            if (!validate()) {
                InsertUpdateGuide();
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
    if (searchParams.has('GuideAutoId_Id')) {
        EditGuideData(searchParams.get('GuideAutoId_Id'));
    }

});
function InsertUpdateGuide() {

    try {
        $.post("/Guides/GuideInsertUpdate", {
            GuideAutoId: GuideAutoId,
            GuideName: $("#GuideName").val(),
            Designation: $("#Designation").val(),
            Photo: $("#photoBox img").attr('src')

        }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                Swal.fire({
                    icon: 'success',
                    text: response.responseMessage,
                }).then(() => {
                    window.location.href = "/Guides/Guides_List";
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

function EditGuideData(GuideAutoId_Id) {
    GuideAutoId = GuideAutoId_Id;
    try {
        $.post("/Guides/EditGuideData", { GuideAutoId: GuideAutoId_Id }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                $("#GuideName").val(response.data.GuideName);
                $("#Designation").val(response.data.Designation);
                $("#photoBox").empty();
                $("#photoBox").append(`<img onerror="this.onerror===null;this.src=funDefaultUserImg()" src="${response.data.Photo}" alt="Selected Photo">`);
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
    let GuideName = $("#GuideName").val();
    if (GuideName == "" || GuideName == undefined) {
        $("#GuideName").addClass("border-danger");
        f = true;
    }
    else {
        $("#GuideName").removeClass("border-danger");
    }


    let Designation = $("#Designation").val();
    if (Designation == "" || Designation == undefined) {
        $("#Designation").addClass("border-danger");
        f = true;
    }
    else {
        $("#Designation").removeClass("border-danger");
    }

   


    return f
}
