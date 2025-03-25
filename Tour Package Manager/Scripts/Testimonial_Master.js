var TestimonialAutoId = 0;
$(document).ready(function () {

    ImageUpload('Photo', 'photoBox');
    $("#Save").click(function () {
        debugger;
        try {
            if (!validate()) {
                InsertUpdateTestimonial();
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
    if (searchParams.has('Testimonial_Id')) {
        EditTestimonailData(searchParams.get('Testimonial_Id'));
    }
   
});
function InsertUpdateTestimonial() {
  
    try {
        $.post("/Testimonial/TestimonialInsertUpdate", {
            TestimonialAutoId: TestimonialAutoId,
            Name: $("#Name").val(),
            Profession: $("#Profession").val(),
            Discription: $("#Discription").val(),
            Photo: $("#photoBox img").attr('src')

        }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                Swal.fire({
                    icon: 'success',
                    text: response.responseMessage,
                }).then(() => {
                    window.location.href = "/Testimonial/Testimonial_List";
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

function EditTestimonailData(Testimonial_Id) {
    TestimonialAutoId = Testimonial_Id;
    try {
        $.post("/Testimonial/EditTestimonailData", { TestimonialAutoId: Testimonial_Id }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                $("#Name").val(response.data.Name);
                $("#Profession").val(response.data.Profession);
                $("#Discription").val(response.data.Discription);
                $("#photoBox").empty();
                $("#photoBox").append(`<img onerror="this.onerror===null;this.src=funDefaultUserImg()" src="${response.data.Photo}" alt="Selected Photo">`);
                $("#Save").text("Update");
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
    let Name = $("#Name").val();
    if (Name == "" || Name == undefined) {
        $("#Name").addClass("border-danger");
        f = true;
    }
    else {
        $("#Name").removeClass("border-danger");
    }

    let Discription = $("#Discription").val();
    if (Discription == "" || Discription == undefined) {
        $("#Discription").addClass("border-danger");
        f = true;
    }
    else {
        $("#Discription").removeClass("border-danger");
    }

    let Profession = $("#Profession").val();
    if (Profession == "" || Profession == undefined) {
        $("#Profession").addClass("border-danger");
        f = true;
    }
    else {
        $("#Profession").removeClass("border-danger");
    }
   

    return f
}
