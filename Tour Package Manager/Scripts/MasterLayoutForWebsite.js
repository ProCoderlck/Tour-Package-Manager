$(document).ready(function () {
    ShowHeaders();
    ShowFooter();
});

function ShowHeaders() {
    try {
        debugger;
        $.post("/MasterLayout/ShowHeaders ", {}, function (response) {
            debugger;
            if (response) {
                $.each(response, function (i, item) {
                    $("#Header").append(` <div class="col-lg-6 text-center text-lg-left mb-2 mb-lg-0">
                    <div class="d-inline-flex align-items-center">
                        <p><i class="fa fa-envelope mr-2"></i>${item.Email}</p>
                        <p class="text-body px-3">|</p>
                        <p><i class="fa fa-phone-alt mr-2"></i>+91${item.PhoneNumber}</p>
                    </div>
                </div>
                <div class="col-lg-6 text-center text-lg-right">
                    <div class="d-inline-flex align-items-center">
                        <a class="text-primary px-3" href="${item.Facebook}"target="_blank">
                            <i class="fab fa-facebook-f"></i>
                        </a>
                        <a class="text-primary px-3" href="${item.Twitter}"target="_blank" title='hello'>
                            <i class="fab fa-twitter"></i>
                        </a>
                        <a class="text-primary px-3" href="${item.Linkddin}"target="_blank">
                            <i class="fab fa-linkedin-in"></i>
                        </a>
                        <a class="text-primary px-3" href="${item.Insatgram}"target="_blank">
                            <i class="fab fa-instagram"></i>
                        </a>
                        <a class="text-primary pl-3" href="${item.Youtube}"target="_blank">
                            <i class="fab fa-youtube"></i>
                        </a>
                    </div>
                </div> `
                    );
                });
            }



        });
    }
    catch (e) {
        (alert("error" + e.Message))
    }
}
function ShowFooter() {
    try {
        debugger;
        $.post("/MasterLayout/ShowFooter", {}, function (response) {
            debugger;
            if (response) {
                $("#Email").html(response.Email);
                $("#PhoneNumber").html("+91" + response.PhoneNumber);
                $("#Locationn").html(response.Location);
                $("#Facebook").attr("href", response.Facebook);
                $("#Instagram").attr("href", response.Insatgram);
                $("#Twitter").attr("href", response.Twitter);
                $("#LinkedIn").attr("href", response.Linkddin);
            }
        });
    }
    catch (e) {
        alert("Error: " + e.message);
    }
}

