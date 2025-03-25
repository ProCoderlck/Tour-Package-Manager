$(document).ready(function () {
    bindDuration(0);
    bindCategory(0);
    bindLocation(0);
    ShowPackageforwebsite();
    ShowLocationforwebsite();
    ShowBlogforweb();
    ShowTestimonialweb();
    ShowGuideforwebsite();

    $("#search").click(function () {
        DurationAutoId = $("#DurationName").val()
        CategoryAutoId = $("#CategoryName").val()
        LocationAutoId = $("#LocationName").val()
        window.location.href = "/Packagelist/Packagelist?duration=" + DurationAutoId + "&location=" + LocationAutoId + "&category=" + CategoryAutoId + "";
        //ShowPackageforwebsite(DurationAutoId, CategoryAutoId, LocationAutoId);
    });
});

function ShowPackageforwebsite() {
    debugger;
    try {
        debugger;
        $.post("/Package/ShowPackage", {},
            function (data) {
                debugger;
                if (data) {
                    debugger;
                    $("#PapularPackage").empty();
                    $.each(data, function (i, item) {
                        $("#PapularPackage").append(`<div class="col-lg-4 col-md-6 mb-4">
                   <div class="package-item bg-white mb-2">
                    <img class="img-fluid" src="${item.PackageImage}" alt="">
                    <div class="p-4">
                        <div class="d-flex justify-content-between mb-3">
                            <small class="m-0"><i class="fa fa-map-marker-alt text-primary mr-2"></i>${item.LocationAutoId}</small>
                            <small class="m-0"><i class="fa fa-calendar-alt text-primary mr-2"></i>${item.DurationAutoId}</small>
                            <small class="m-0"><i class="fa fa-user text-primary mr-2"></i>${item.CatagoryAutoId}</small>
                        </div>
                        <a class="h5 text-decoration-none" href="/Packagelist/Package_details?Pacckageid=${item.PackageAutoId}">${item.PackageName}</a>
                        <div class="border-top mt-4 pt-4">
                            <div class="d-flex justify-content-between">
                                <h6 class="m-0"><i class="fa fa-star text-primary mr-2"></i>4.5 <small>(250)</small></h6>
                                <h5 class="m-0">$${item.Price}</h5>
                            </div>
                        </div>
                    </div>
                </div>
            </div>`
                        );
                    });
                }
                else {
                    $('#PapularPackage').empty();
                    $('#PapularPackage').append('<tr><td colspan="9" style="text-align:center">No records found</td></tr>')
                }

            });
    }
    catch (e) {
        (alert("error" + e.Message))
    }
}
function ShowLocationforwebsite() {
    debugger;
    try {
        debugger;
        $.post("../Location/ShowLocation", {},
            function (data) {
                debugger;
                if (data) {
                    debugger;
                    $("#Location").empty();
                    $.each(data, function (i, item) {
                        $("#Location").append(`<div class="col-lg-4 col-md-6 mb-4">
                         <div class="destination-item position-relative overflow-hidden mb-2">
                             <img class="img-fluid" src="${item.LocationImage}" alt="">
                                 <a class="destination-overlay text-white text-decoration-none" href="/Packagelist/Packagelist?location=${item.LocationAutoId}">
                                     <h5 class="text-white">${item.LocationName}</h5>
                                 </a>
                         </div>
                     </div>`
                        );
                    });
                }


            });
    }
    catch (e) {
        (alert("error" + e.Message))
    }
}
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
                                    <a class="text-primary text-uppercase text-decoration-none" href="/BlogWeb/BlogDetail?blogid=${item.BlogAutoId}">${item.UserAutoId}</a>
                                    <span class="text-primary px-2">|</span>
                                    <a class="text-primary text-uppercase text-decoration-none" href="/BlogWeb/BlogDetail?blogid=${item.BlogAutoId}">${item.BlogCatagoryAutoId}</a>
                                </div>
                                <a class="h5 m-0 text-decoration-none" href="/BlogWeb/BlogDetail?blogid=${item.BlogAutoId}">${item.BlogTitle}</a>
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
function ShowTestimonialweb() {
    try {
        debugger;
        $.post("/TestimonialWeb/ShowTestimonialweb", {}, function (response) {
            debugger;

            if (response) {
                $.each(response, function (i, item) {
                    debugger;
                    $("#Testimonial").append(`

                        <div class="text-center pb-4">
                <img class="img-fluid mx-auto" src="${item.Photo}" style="width: 100px; height: 100px;">
                <div class="testimonial-text bg-white p-4 mt-n5">
                    <p class="mt-5">
                      ${item.Discription}
                    </p>
                    <h5 class="text-truncate">${item.Name}</h5>
                    <span>${item.Profession}</span>
                </div>
            </div>

                       
            ` );
                });
            }
            $(".testimonial-carousel").owlCarousel({
                autoplay: true,
                smartSpeed: 1500,
                margin: 30,
                dots: true,
                loop: true,
                center: true,
                responsive: {
                    0: {
                        items: 1
                    },
                    576: {
                        items: 1
                    },
                    768: {
                        items: 2
                    },
                    992: {
                        items: 3
                    }
                }
            });

        });
    }
    catch (e) {
        (alert("error" + e.Message))
    }
}


function ShowGuideforwebsite() {
    debugger;
    try {
        debugger;
        $.post("../Guide_Web/ShowGuideforWeb", {},
            function (data) {
                debugger;
                if (data) {
                    debugger;
                    $("#Guide").empty();
                    $.each(data, function (i, item) {
                        $("#Guide").append(`<div class="col-lg-3 col-md-4 col-sm-6 pb-2">
    <div class="team-item bg-white mb-4">
        <div class="team-img position-relative overflow-hidden">
            <img class="img-fluid w-100" src="${item.Photo}" alt="">
                <div class="team-social">
                    <a class="btn btn-outline-primary btn-square" href=""><i class="fab fa-twitter"></i></a>
                    <a class="btn btn-outline-primary btn-square" href=""><i class="fab fa-facebook-f"></i></a>
                    <a class="btn btn-outline-primary btn-square" href=""><i class="fab fa-instagram"></i></a>
                    <a class="btn btn-outline-primary btn-square" href=""><i class="fab fa-linkedin-in"></i></a>
                </div>
        </div>
        <div class="text-center py-4">
            <h5 class="text-truncate">${item.GuideName}</h5>
            <p class="m-0">${item.Designation}</p>
        </div>
    </div>
</div>`
                        );
                    });
                }


            });
    }
    catch (e) {
        (alert("error" + e.Message))
    }
}

