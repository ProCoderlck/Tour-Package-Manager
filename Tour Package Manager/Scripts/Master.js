function bindUserStatus(StatusAutoId) {
    debugger;
    $.post("/User/getStatusList", function (data) {
        if (data) {
            $("#Status").empty();
            $("#Status").append('<option value="">--Select Status--</option>');
            $.each(data, function (key, value) {
                var isSelected = '';
                if (parseInt(StatusAutoId) == parseInt(value.StatusAutoId)) {
                    isSelected = 'selected';
                }
                $("#Status").append(`<option value='${value.StatusAutoId}' ${isSelected}> ${value.StatusName}</option>`)
            });
            /*if (StatusAutoId > -1) {
                $("#Status").val(StatusAutoId);
            }*/         
        }
    });
}

function bindDuration(DurationAutoId) {
    $.post("/Package/getDurationList", {}, function (data) {
        if (data) {
            $("#DurationName").empty();
            $("#DurationName").append('<option value="0">--Select Duration--</option>');
            $.each(data, function (i, Duration) {
                var isSelected = '';
                if (parseInt(DurationAutoId) == parseInt(Duration.DurationAutoId)) {
                    isSelected = 'selected';
                }
                $("#DurationName").append('<option value="' + Duration.DurationAutoId + '" ' + isSelected + '>' + Duration.DurationName + '</option>');
            });
            /*if (DurationAutoId > 0) {
                $("#DurationName").val(DurationAutoId);
            }*/
        }
    });
}

function bindCategory(CategoryAutoId) {
 
    $.post("/Package/getCategoryList", {}, function (data) {
        if (data) {
            $("#CategoryName").empty();
            $("#CategoryName").append('<option value="0">--Select Category--</option>');
            $.each(data, function (i, Category) {
                var isSelected = '';
                if (parseInt(CategoryAutoId) == parseInt(Category.CategoryAutoId)) {
                    isSelected = 'selected';
                }
                $("#CategoryName").append('<option value="' + Category.CategoryAutoId + '" ' + isSelected + '>' + Category.CategoryName + '</option>');
            });
            /*if (CategoryAutoId > 0) {
                $("#CategoryName").val(CategoryAutoId);
            }*/
        }
    });
}


function bindLocation(LocationAutoId) {
    $.post("/Package/getLocationList", {}, function (data) {
        if (data) {
            $("#LocationName").empty();
            $("#LocationName").append('<option value="0">--Select Location--</option>');
            $.each(data, function (i, Location) {
                var isSelected = '';
                if (parseInt(LocationAutoId) == parseInt(Location.LocationAutoId)) {
                    isSelected = 'selected';
                }
                $("#LocationName").append('<option value="' + Location.LocationAutoId + '" ' + isSelected +  '>' + Location.LocationName + '</option >');
            });
            //if (LocationAutoId > 0) {
            //    $("#LocationName").val(LocationAutoId);
            //}
        }
    });
}

function bindBlogCategory(BlogCategoryAutoId) {
    $.post("/Blog/getBlogcategoryList", {}, function (data) {
        if (data) {
            $("#BlogName").empty();
            $("#BlogName").append('<option value="0">--Select Caategory--</option>');
            $.each(data, function (i, category) {
                $("#BlogName").append('<option value="' + category.BlogCategoryAutoId + '">' + category.CategoryName + '</option>');
            });
            if (BlogCategoryAutoId > 0) {
                $("#BlogName").val(BlogCategoryAutoId);
            }
        }
    });
}
function bindUser(UserAutoId) {
    $.post("/Blog/getUserList", {}, function (data) {
        if (data) {
            $("#UserName").empty();
            $("#UserName").append('<option value="0">--Select User--</option>');
            $.each(data, function (i, User) {
                $("#UserName").append('<option value="' + User.UserAutoId + '">' + User.UserName + '</option>');
            });
            if (UserAutoId > 0) {
                $("#UserName").val(UserAutoId);
            }
        }
    });
}


function ImageUpload(FileUpload, Image) {
    const fileInput = document.getElementById(FileUpload);
    const photoBox = document.getElementById(Image);
    fileInput.addEventListener('change', function () {
        const file = fileInput.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                /*photoBox.css("display", "block");*/
                photoBox.innerHTML = `<img onerror="this.onerror===null;this.src=firoj()" src="${e.target.result}" alt="Selected Photo">`;
            }
            reader.readAsDataURL(file);
        } else {
            /*  photoBox.css("display", "block");*/
            photoBox.textContent = 'No image selected';
        }
    });
}
function clearRequiredMsg(e) {
    var Stocktype = $(e).val();
    if (Stocktype) {
        $(e).removeClass("border-danger")
    }
}
