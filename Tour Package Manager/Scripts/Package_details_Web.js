$(document).ready(function () {
    ShowPackageCategoryforwebsite();
    ShowRecentPackageforwebsite();
    const searchParams = new URLSearchParams(window.location.search);
    debugger;
    if (searchParams.has('Pacckageid')) {
        debugger;
        ShowPackageDetailforweb(searchParams.get('Pacckageid')), ShowDaysforwebsite(searchParams.get('Pacckageid'));


        
    }


});
//<span class="badge badge-primary badge-pill">98</span>


function ShowPackageDetailforweb(PackageAutoId) {
    try {
        $.post("/Packagelist/ShowPackage_detail", { PackageAutoId: PackageAutoId }, function (response) {
            debugger;
            
            if (response) {
                $("#Photo").attr("src", response.PackageImage);
                $("#price").html("₹"+ response.Price);
                $("#PackageName").html(response.PackageName);
                $("#dis").html(response.Discription);
                $("#shordis").html(response.ShortDiscription);
                $("#duration").html(response.DurationAutoId);
                $("#location").html(response.LocationAutoId);
                $("#category").html(response.CatagoryAutoId);
                $("#PackageEnd").html(response.PackageEnd);
                $("#PackageStart").html(response.PackageStart);
            }
           
        });
    }
    catch (e) {
        alert("Error in MissingToolId" + e.message);
    }
}


function ShowPackageCategoryforwebsite() {
    debugger;
    try {
        debugger;
        $.post("../Packagelist/Packagecategory", {},
            function (response) {
                debugger;
                if (response) {
                    debugger;
                    $("#Packagecategory").empty();
                    $.each(response, function (i, item) {
                        $("#Packagecategory").append(` 
                        <li class="d-flex justify-content-between align-items-center">
                                <a class="text-dark" href="/Packagelist/Packagelist?category=${item.CategoryAutoId}">
                                    <i class="fa fa-angle-right text-primary mr-2"></i>${item.CategoryName}
                                </a>
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

function ShowRecentPackageforwebsite() {
    debugger;
    try {
        debugger;
        $.post("../Packagelist/showRecentPackage", {},
            function (response) {
                debugger;
                if (response) {
                    debugger;
                    $("#RecentPackage").empty();
                    $.each(response, function (i, item) {
                        $("#RecentPackage").append(` 
                        
                    <a class="d-flex align-items-center text-decoration-none bg-white mb-3" href="/Packagelist/Package_details?Pacckageid=${item.PackageAutoId}">
                        <img class="img-fluid"style="width:30%" src="${item.PackageImage}" alt="">
                        <div class="pl-3">
                            <h6 class="m-1">${item.PackageName}</h6>
                            <small>${item.PackageStart}</small>
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
function ShowDaysforwebsite(PackageAutoId) {
    try {
        debugger;
        $.post("/Packagelist/ShowDaysforwebsite", { PackageAutoId: PackageAutoId }, function (response) {
            debugger;
            if (response) {
                $("#dayslist").empty();
                let accordionId = "scheduleAccordion"; // Unique accordion container ID

                $("#dayslist").append(`<div class="accordion" id="${accordionId}"></div>`);

                $.each(response, function (i, item) {
                    let collapseId = `collapseDay${item.DaysAutoId}`; // Unique ID for each collapse item

                    $(`#${accordionId}`).append(`
                        <div class="accordion-item">
                            <h2 class="accordion-header">
                                <button class="accordion-button collapsed" type="button" onclick="bindtask(${item.DaysAutoId})"
                                    data-bs-toggle="collapse" data-bs-target="#${collapseId}" aria-expanded="false" aria-controls="${collapseId}">
                                    ${item.DaysName}
                                </button>
                            </h2>
                            <div id="${collapseId}" class="accordion-collapse collapse" data-bs-parent="#${accordionId}">
                                <div class="accordion-body">
                                    Content for ${item.DaysName}
                                </div>
                            </div>
                        </div>
                    `);
                });
            }
        });
    }
    catch (e) {
        alert("Error: " + e.Message);
    }
}
function bindtask(DaysAutoId) {
    try {
        debugger;
        $.post("/Packagelist/Taskforwebsite", { DaysAutoId: DaysAutoId }, function (response) {
            debugger;
            if (response) {
                debugger;
                    $("#day3").empty();
                    $.each(response, function (i, item) {
                        $("#day3").append(`
                         <div class="accordion-body">
                                    <ul>
                                       ${item.TaskName}
                                    </ul>
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








