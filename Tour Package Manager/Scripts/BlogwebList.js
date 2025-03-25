$(document).ready(function () {
    ShowBlogforweb()

});


function ShowBlogforweb() {
    try {
        debugger;
        $.post("/BlogWeb/ShowBlog ", {}, function (response) {
            debugger;
            if (response) {
                $.each(response, function (i, item) {
                    $("#BlogList").append(`  
                    <div class="col-md-4 mb-4 pb-2">
                        <div class="blog-item">
                            <div class="position-relative">
                                <img class="img-fluid w-100" src="${item.Photo}" alt="">
                                <div class="blog-date">
                                    <h6 class="font-weight-bold mb-n1">${item.Day}</h6>
                                    <small class="text-white text-uppercase">${item.Month}</small>
                                </div>
                            </div>
                            <div class="bg-white p-4">
                                <div class="d-flex mb-2">
                                    <a class="text-primary text-uppercase text-decoration-none"href="/BlogWeb/BlogDetail?blogid=${item.BlogAutoId}">${item.UserAutoId}</a>
                                    <span class="text-primary px-2">|</span>
                                    <a class="text-primary text-uppercase text-decoration-none" href="/BlogWeb/BlogDetail?blogid=${item.BlogAutoId}">${item.BlogCatagoryAutoId}</a>
                                </div>
                                <a class="h5 m-0 text-decoration-none" href="/BlogWeb/BlogDetail?blogid=${item.BlogAutoId}">${item.Discription}</a>
                            </div>
                        </div>
                    </div>
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
