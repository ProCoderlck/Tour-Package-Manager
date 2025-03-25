var BlogCategoryAutoId = 0;
$(document).ready(function () {
    ShowBlog();
    ShowBlogCategory();
    $("#Save").click(function () {
        debugger;
        try {
            if (!validate()) {
                BlogCategoryInsertUpdate();
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
    if (searchParams.has('BlogcategoryAutoId_Id')) {
        EditBlogCategory(searchParams.get('BlogcategoryAutoId_Id'));
    }
    else {
        bindUserStatus();
    }
    $("#categoryModal").modal({
        backdrop: "static",
        keyboard: false    

    });
});


function ShowBlog() {
    try {
        debugger;
        $.post("/Blog/ShowBlog ", {}, function (response) {
            debugger;
            if (response.responseCode == 200) {
                console.log(response.data)
                if (response.data) {
                    $("#Data").empty();
                    $.each(response.data, function (i, item) {
                        $("#Data").append(`<tr>
                     <td style="text-align:center">
                                <a onclick="DeleteBlog(${item.BlogAutoId});"class="fa fa-trash-o" style='font-size: 20px;color:red;margin-right:10px;'></a>
                                <a href="/Blog/Add_Blog?BlogAutoId_Id=${item.BlogAutoId}" class="fa fa-edit bi bi-eye" style='font-size: 20px;color:Green;margin-right:10px'></a>
                    </td>
                    <td style="text-align:center">${item.BlogAutoId}</td>
                    <td style="text-align:center">${item.BlogTitle}</td>
                    <td style="text-align:center">${item.BlogCatagoryAutoId}</td>
                    <td style="text-align:center">${item.UserAutoId}</td>
                     <td style="text-align:center;">
                                    <img src="${item.Photo}" height="34" width="42" onerror="this.onerror === null; this.src = funDefaultToolImg();"/>
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
function DeleteBlog(BlogAutoId) {
    try {
        Swal.fire({
            text: 'Are you sure to delete this user ?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.post("/Blog/Blogdelete", { BlogAutoId: BlogAutoId }, function (response) {
                    if (response.responseCode == 200) {
                        Swal.fire({
                            icon: 'success',
                            text: response.responseMessage,
                        });
                        ShowBlog();
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


//blog category js start---
function BlogCategoryInsertUpdate() {

    try {
        $.post("/Blog/BlogCategoryInsertUpdate", {
            BlogCategoryAutoId: BlogCategoryAutoId,
            CategoryName: $("#CategoryName").val(),
            StatusAotoId: $("#Status").val()
            
        }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                Swal.fire({
                    icon: 'success',
                    text: response.responseMessage,
                }).then(() => {
                    clearfunction();
                    ShowBlogCategory();
                    $("#categoryModal").modal("show");
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



function BlogCategorydelete(BlogCategoryAutoId) {
    try {
        Swal.fire({
            text: 'Are you sure to delete this user ?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.post("../Blog/BlogCategorydelete", { BlogCategoryAutoId: BlogCategoryAutoId }, function (response) {
                    if (response.responseCode == 200) {
                        Swal.fire({
                            icon: 'success',
                            text: response.responseMessage,
                        });
                        ShowBlogCategory();
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

function ShowBlogCategory() {
    try {
        debugger;
        $.post("/Blog/ShowBlogCategory ", {}, function (response) {
            debugger;
            if (response.responseCode == 200) {
                console.log(response.data)
                if (response.data) {
                    $("#BlogCategory").empty();
                    $.each(response.data, function (i, item) {
                        $("#BlogCategory").append(`<tr>
                     <td style="text-align:center">
                                <a onclick="BlogCategorydelete(${item.BlogCategoryAutoId});"class="fa fa-trash-o" style='font-size: 20px;color:red;margin-right:10px;'></a>
                                <a onclick="EditBlogCategory(${item.BlogCategoryAutoId})" class="fa fa-edit bi bi-eye" style='font-size: 20px;color:Green;margin-right:10px'></a>
                    </td>
                    <td style="text-align:center">${item.BlogCategoryAutoId}</td>
                    <td style="text-align:center">${item.CategoryName}</td>
                    <td style="text-align:center">${item.Status}</td>
                   
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
function EditBlogCategory(BlogcategoryAutoId_Id) {
    BlogCategoryAutoId = BlogcategoryAutoId_Id;
    try {
        $.post("/Blog/EditBlogCategoryData", { BlogCategoryAutoId: BlogcategoryAutoId_Id }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                $("#CategoryName").val(response.data.CategoryName);
                bindUserStatus(response.data.Status);
               
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
    let Status = $("#Status").val();
    if (Status == "" || Status == undefined) {
        $("#Status").addClass("border-danger");
        f = true;
    }
    else {
        $("#Status").removeClass("border-danger");
    }

    let CategoryName = $("#CategoryName").val();
    if (CategoryName == "" || CategoryName == undefined) {
        $("#CategoryName").addClass("border-danger");
        f = true;
    }
    else {
        $("#CategoryName").removeClass("border-danger");
    }
    return f
}
function clearfunction() {
    BlogCategoryAutoId = 0;
    $("#CategoryName").val("");
    $("#Status").val("");

}