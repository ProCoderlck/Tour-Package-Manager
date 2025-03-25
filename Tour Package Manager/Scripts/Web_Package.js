$(document).ready(function () {

    const searchParams = new URLSearchParams(window.location.search);
    debugger;
    if (searchParams.has('duration') || searchParams.has('location') || searchParams.has('category')) {
        var DurationAutoId = 0, CategoryAutoId = 0, LocationAutoId = 0;
        if (searchParams.has('duration')) {
            bindDuration(searchParams.get('duration'));
            DurationAutoId = searchParams.get('duration')
        }
        if (searchParams.has('location')) {
            bindLocation(searchParams.get('location'));
            LocationAutoId = searchParams.get('location')
        }
        if (searchParams.has('category')) {
            bindCategory(searchParams.get('category'));
            CategoryAutoId = searchParams.get('category')
        }
        ShowPackageforwebsite(DurationAutoId, CategoryAutoId, LocationAutoId);
    }
    else {
        bindDuration(0);
        bindCategory(0);
        bindLocation(0);
        ShowPackageforwebsite();
    }



    $("#search").click(function () {
        DurationAutoId = $("#DurationName").val()
        CategoryAutoId = $("#CategoryName").val()
        LocationAutoId = $("#LocationName").val()
        window.location.href = "/Packagelist/Packagelist?duration=" + DurationAutoId + "&location=" + LocationAutoId + "&category=" + CategoryAutoId + "";
        //ShowPackageforwebsite(DurationAutoId, CategoryAutoId, LocationAutoId);
    });
});

function ShowPackageforwebsite(DurationAutoId = 0, CategoryAutoId = 0, LocationAutoId = 0) {
    debugger;
    try {
        debugger;
        $.post("/Package/ShowPackage",
            {
                DurationAutoId: DurationAutoId,
                CategoryAutoId: CategoryAutoId,
                LocationAutoId: LocationAutoId
            },
            function (data) {
                debugger;
                if (data) {
                    debugger;
                    $("#packegae_list_container").empty();
                    $.each(data, function (i, item) {
                        $("#packegae_list_container").append(`<div class="col-lg-4 col-md-6 mb-4">
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
                    $('#packegae_list_container').empty();
                    $('#packegae_list_container').append('<tr><td colspan="9" style="text-align:center">No records found</td></tr>')
                }

            });
    }
    catch (e) {
        (alert("error" + e.Message))
    }
}










