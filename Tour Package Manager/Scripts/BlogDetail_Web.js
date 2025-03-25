$(document).ready(function () {
    ShowblogCategoryforwebsite(); 
    ShowRecentblogforwebsite();
    const searchParams = new URLSearchParams(window.location.search);
    debugger;
    if (searchParams.has('blogid')) {
        ShowBlogDetailforweb(searchParams.get('blogid')), ShowBlogDetailUserforweb(searchParams.get('blogid'));
    }
   
});


function ShowBlogDetailforweb(BlogDetail) {
    try {
        $.post("/BlogWeb/ShowBlogdetail", { BlogAutoId: BlogDetail }, function (response) {
            debugger;
            if (response) {
                $("#Photo").attr("src",response.Photo);
                $("#Day").html(response.Day);
                $("#Month").html(response.Month);
                $("#BlogTitle").html(response.BlogTitle);
                $("#Discription").html(response.Discription);
                $("#BlogCatagoryAutoId").html(response.BlogCatagoryAutoId);
                $("#UserAutoId").html(response.UserAutoId);
            }
           
        });
    }
    catch (e) {
        alert("Error in MissingToolId" + e.message);
    }
}
function ShowBlogDetailUserforweb(BlogDetail) {
    try {
        $.post("/BlogWeb/ShowBlogdetail", { BlogAutoId: BlogDetail }, function (response) {
            debugger;
            if (response) {
                $("#dis").html(response.Discription);
                $("#Username").html(response.UserAutoId);
                $("#UserImage").attr("src", response.UserImage);
            }
           
        });
    }
    catch (e) {
        alert("Error in MissingToolId" + e.message);
    }
}


function ShowblogCategoryforwebsite() {
    debugger;
    try {
        debugger;
        $.post("../BlogWeb/blogcategory", {},
            function (response) {
                debugger;
                if (response) {
                    debugger;
                    $("#blogcategory").empty();
                    $.each(response, function (i, item) {
                        $("#blogcategory").append(` 
                        <li class="d-flex justify-content-between align-items-center">
                                <a class="text-dark" href="#">
                                    <i class="fa fa-angle-right text-primary mr-2"></i>${item.CategoryName}
                                </a>
                                <span class="badge badge-primary badge-pill">98</span>
                            </li>`
                        );
                    });
                }


            });
    }
    catch (e) {
        (alert("error" + e.Message))
    }
}

function ShowRecentblogforwebsite() {
    debugger;
    try {
        debugger;
        $.post("../BlogWeb/showblogtitle", {},
            function (response) {
                debugger;
                if (response) {
                    debugger;
                    $("#blogtitle").empty();
                    $.each(response, function (i, item) {
                        $("#blogtitle").append(` 
                        
                    <a class="d-flex align-items-center text-decoration-none bg-white mb-3" href="">
                        <img class="img-fluid"style="width:30%" src="${item.Photo}" alt="">
                        <div class="pl-3">
                            <h6 class="m-1">${item.BlogTitle}</h6>
                            <small>${item.Date}</small>
                        </div>
                    </a>
                   `
                        );
                    });
                }


            });
    }
    catch (e) {
        (alert("error" + e.Message))
    }
}




