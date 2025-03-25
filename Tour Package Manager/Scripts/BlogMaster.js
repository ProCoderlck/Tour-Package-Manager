var BlogAutoId = 0;
$(document).ready(function () {

    ImageUpload('Photo', 'photoBox');


    $("#Save").click(function () {
        debugger;
        try {
            if (!validate()) {
                InsertUpdateBlog();
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
    if (searchParams.has('BlogAutoId_Id')) {
        EditBlog(searchParams.get('BlogAutoId_Id'));
    }
    else {
        bindBlogCategory();
        bindUser();
    }
});
function InsertUpdateBlog() {  
   
    try {
        $.post("/Blog/BlogInsertUpdate", {
            BlogAutoId: BlogAutoId,
            BlogTitle: $("#BlogTitle").val(),
            UserAutoId: $("#UserName").val(),
            BlogCatagoryAutoId: $("#BlogName").val(),
            Discription: $("#Discription").val(),
            Date: $("#Date").val(),
            Photo: $("#photoBox img").attr('src')

        }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                Swal.fire({
                    icon: 'success',
                    text: response.responseMessage,
                }).then(() => {
                    window.location.href = "/Blog/Blog_List";
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

function EditBlog(BlogAutoId_Id) {
    BlogAutoId = BlogAutoId_Id;
    try {
        $.post("/Blog/EditBlogData", { BlogAutoId: BlogAutoId_Id }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                $("#BlogTitle").val(response.data.BlogTitle);
                $("#Discription").val(response.data.Discription);
                $("#Date").val(response.data.Date);
                bindBlogCategory(response.data.BlogCatagoryAutoId);
                bindUser(response.data.UserAutoId);
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
    let BlogTitle = $("#BlogTitle").val();
    if (BlogTitle == "" || BlogTitle == undefined) {
        $("#BlogTitle").addClass("border-danger");
        f = true;
    }
    else {
        $("#BlogTitle").removeClass("border-danger");
    }

    let Discription = $("#Discription").val();
    if (Discription == "" || Discription == undefined) {
        $("#Discription").addClass("border-danger");
        f = true;
    }
    else {
        $("#Discription").removeClass("border-danger");
    }
    let Date = $("#Date").val();
    if (Date == "" || Date == undefined) {
        $("#Date").addClass("border-danger");
        f = true;
    }
    else {
        $("#Date").removeClass("border-danger");
    }

    let UserName = $("#UserName").val();
    if (UserName == "0" || UserName == undefined) {
        $("#UserName").addClass("border-danger");
        f = true;
    }
    else {
        $("#UserName").removeClass("border-danger");
    }

    let BlogName = $("#BlogName").val();
    if (BlogName == "0" || BlogName == undefined) {
        $("#BlogName").addClass("border-danger");
        f = true;
    }
    else {
        $("#BlogName").removeClass("border-danger");
    }

    return f
}
