$(document).ready(function () {
    ShowTestimonialweb()

});


function ShowTestimonialweb() {
    try {
        debugger;
        $.post("/TestimonialWeb/ShowTestimonialweb ", {}, function (response) {
            debugger;
            if (response.responseCode == 200) {
                console.log(response.data)
                if (response.data) {
                    $.each(response.data, function (i, item) {
                        $("#Data").append(`<tr>
                     <td style="text-align:center">
                                <a onclick="DeleteTestimonial(${item.TestimonialAutoId});"class="fa fa-trash-o" style='font-size: 20px;color:red;margin-right:10px;'></a>
                                <a href="/Blog/Add_Blog?BlogAutoId_Id=${item.TestimonialAutoId}" class="fa fa-edit bi bi-eye" style='font-size: 20px;color:Green;margin-right:10px'></a>
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
