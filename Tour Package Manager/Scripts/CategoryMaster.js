var CategoryAutoId = 0;
$(document).ready(function () {
    ImageUpload('CategoryImage', 'photoBox');

    $("#Save").click(function () {
        debugger;
        try {
            if (!validate()) {
                InsertUpdateCategory();
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
    if (searchParams.has('CategoryAutoId_Id')) {
        EditCategory(searchParams.get('CategoryAutoId_Id'));
    }
    else {
        bindUserStatus();
    }
});
function InsertUpdateCategory() {

    try {
        $.post("/Category/CategoryInsertUpdate", {
            CategoryAutoId: CategoryAutoId,
            CategoryName: $("#CategoryName").val(),
            StatusAutoId: $("#Status").val(),
            CategoryImage: $("#photoBox img").attr('src')

        }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                Swal.fire({
                    icon: 'success',
                    text: response.responseMessage,
                }).then(() => {
                    window.location.href = "/Category/Category_List";
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


function EditCategory(CategoryAutoId_Id) {
    CategoryAutoId = CategoryAutoId_Id;
    try {
        $.post("/Category/EditCategoryData", { CategoryAutoId: CategoryAutoId_Id }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                $("#CategoryName").val(response.data.CategoryName);
                bindUserStatus(response.data.StatusAutoId);
                $("#photoBox").empty();
                $("#photoBox").append(`<img onerror="this.onerror===null;this.src=funDefaultUserImg()" src="${response.data.CategoryImage}" alt="Selected Photo">`);
                $("#Save").text("Update");
            }
            else {
                window.location.href = "/";
            }
        });
    }
    catch (e) {
        alert("Error in DurationAutoId" + e.message);
    }
}

    

function validate() {
    let f = false;
    let CategoryName = $("#CategoryName").val();
    if (CategoryName == "" || CategoryName == undefined) {
        $("#CategoryName").addClass("border-danger");
        f = true;
    }
    else {
        $("#CategoryName").removeClass("border-danger");
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