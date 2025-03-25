$(document).ready(function () {
    ShowTestimonial()

});


function ShowTestimonial() {
    try {
        debugger;
        $.post("/Testimonial/ShowTestimonial ", {}, function (response) {
            debugger;
            if (response.responseCode == 200) {
                console.log(response.data)
                if (response.data) {
                    $("#Data").empty();
                    $.each(response.data, function (i, item) {
                        $("#Data").append(`<tr>
                     <td style="text-align:center">
                                <a onclick="DeleteTestimonial(${item.TestimonialAutoId});"class="fa fa-trash-o" style='font-size: 20px;color:red;margin-right:10px;'></a>
                                <a href="/Testimonial/Add_Testimonial?Testimonial_Id=${item.TestimonialAutoId}" class="fa fa-edit bi bi-eye" style='font-size: 20px;color:Green;margin-right:10px'></a>
                    </td>
                    <td style="text-align:center">${item.Name}</td>
                    <td style="text-align:center">${item.Discription}</td>
                    <td style="text-align:center">${item.Profession}</td>
                     <td style="text-align:center">
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

function DeleteTestimonial(TestimonialAutoId) {
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
                $.post("/Testimonial/Testimonialdelete", { TestimonialAutoId: TestimonialAutoId }, function (response) {
                    if (response.responseCode == 200) {
                        Swal.fire({
                            icon: 'success',
                            text: response.responseMessage,
                        });
                        ShowTestimonial();
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
