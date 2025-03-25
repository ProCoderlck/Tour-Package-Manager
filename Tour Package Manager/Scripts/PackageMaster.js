var PackageAutoId = 0; var DaysAutoId = 0; var TaskAutoId = 0;
$(document).ready(function () {
    ImageUpload('PackageImage', 'photoBox');
    ImageUpload('PackagePdf', 'photoBoxpdf');

    $("#Save").click(function () {
        debugger;
        try {
            if (!validate()) {
                InsertUpdatePackage();
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
    $("#Savedays").click(function () {
        debugger;
        try {
            if (!validatefordays()) {
                InsertUpdateDays();
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
    $("#SaveTask").click(function () {
        debugger;
        try {
            if (!validateforTask()) {
                InsertUpdateTask();
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
    if (searchParams.has('PackageAutoId_Id')) {
        EditPackage(searchParams.get('PackageAutoId_Id'));
    }
    else {
        bindUserStatus();
        bindDuration();
        bindCategory();
        bindLocation();
    }

    $("#categoryModal").modal({
        backdrop: "static",
        keyboard: false

    });

    $("#TaskModal").modal({
        backdrop: "static",
        keyboard: false

    });

});

function openSecondModal() {
    var firstModal = new bootstrap.Modal(document.getElementById('categoryModal'));
    var secondModal = new bootstrap.Modal(document.getElementById('TaskModal'));

    firstModal.show();
    secondModal.hide();
}
function InsertUpdatePackage()  {
    debugger;
    try {
        $.post("/Package/PackageInsertUpdate", {
            PackageAutoId: PackageAutoId,
            PackageName: $("#PackageName").val(),
            ShortDiscription: $("#ShortDiscription").val(),
            Discription: $("#Discription").val(),
            Price: $("#Price").val(),
            DurationAutoId: $("#DurationName").val(),
            CatagoryAutoId: $("#CategoryName").val(),
            LocationAutoId: $("#LocationName").val(),
            PackagePdf: $("#photoBoxpdf img").attr('src'),
            PackageImage: $("#photoBox img").attr('src'),
            PackageStart: $("#PackageStart").val(),
            PackageEnd: $("#PackageEnd").val(),
            StatusAutoId: $("#Status").val()
        }, function (data) {
            debugger;
            if (data.Message != "") {
                Swal.fire({
                    text: data.Message,
                    icon: "success",
                    confirmButtonText: "OK"
                }).then(() => {

                    window.location.href = `/Package/Add_Package?PackageAutoId_Id=${data.PakageId}`;
                 
                });
            }
        }).fail(function (xhr, status, error) {
            Swal.fire({
                title: "Error!",
                text: "Something went wrong: " + error,
                icon: "error",
                confirmButtonText: "OK"
            });
        });
    } catch (e) {
        Swal.fire({
            title: "Error!",
            text: "Exception: " + e.message,
            icon: "error",
            confirmButtonText: "OK"
        });
    }

}

function EditPackage(PackageAutoId_Id) {
    PackageAutoId = PackageAutoId_Id;
    try {
        $.post("/Package/EditPackage", { PackageAutoId: PackageAutoId_Id }, function (response) {

            debugger;
            if (response.DurationAutoId > 0) {
                bindUserStatus(response.StatusAutoId);
                bindDuration(response.DurationAutoId);
                bindCategory(response.CatagoryAutoId);
                bindLocation(response.LocationAutoId);
                $("#PackageName").val(response.PackageName);
                $("#Price").val(response.Price);
                $("#ShortDiscription").val(response.ShortDiscription);
                $("#Discription").val(response.Discription);
                $("#PackageStart").val(response.PackageStart);
                $("#PackageEnd").val(response.PackageEnd);
                $("#photoBox").empty();
                $("#photoBox").append(`<img onerror="this.onerror===null;this.src=funDefaultUserImg()" src="${response.PackageImage}" alt="Selected Photo">`);
                $("#photoBoxpdf").empty();
                $("#photoBoxpdf").append(`<img onerror="this.onerror===null;this.src=funDefaultUserImg()" src="${response.PackagePdf}" alt="Selected Photo">`);
                $("#Save").text("Update");
                ShowDays();
                $("#days").show();
            }
        });
    }
    catch (e) {
        alert("Error in MissingToolId" + e.message);
    }

}

function validate() {
    let f = false;
    let name = $("#PackageName").val();
    if (name == "" || name == undefined) {
        $("#PackageName").addClass("border-danger");
        f = true;
    }
    else {
        $("#PackageName").removeClass("border-danger");
    }

    let Price = $("#Price").val();
    if (Price == "") {
        $("#Price").addClass("border-danger");
        f = true;
    }
    else {
        $("#Price").removeClass("border-danger");
    }

    let Durationdrop = $("#DurationName").val();
    if (Durationdrop == "0" || Durationdrop == undefined) {
        $("#DurationName").addClass("border-danger");
        f = true;
    }
    else {
        $("#DurationName").removeClass("border-danger");
    }

    let PackageStart = $("#PackageStart").val();
    if (PackageStart == "" || PackageStart == undefined) {
        $("#PackageStart").addClass("border-danger");
        f = true;
    }
    else {
        $("#PackageStart").removeClass("border-danger");
    }

    let PackageEnd = $("#PackageEnd").val();
    if (PackageEnd == "" || PackageEnd == undefined) {
        $("#PackageEnd").addClass("border-danger");
        f = true;
    }
    else {
        $("#PackageStart").removeClass("border-danger");
    }

    let Category = $("#CategoryName").val();
    if (Category == "0" || Category == undefined) {
        $("#CategoryName").addClass("border-danger");
        f = true;
    }
    else {
        $("#CategoryName").removeClass("border-danger");
    }

    let usertype = $("#LocationName").val();
    if (usertype == "0" || usertype == undefined) {
        $("#LocationName").addClass("border-danger");
        f = true;
    }
    else {
        $("#LocationName").removeClass("border-danger");
    }

    let Status = $("#Status").val();
    if (Status == "" || Status == undefined) {
        $("#Status").addClass("border-danger");
        f = true;
    }
    else {
        $("#Status").removeClass("border-danger");
    }

    return f
}

function validatefordays() {
    let f = false;


    let DaysName = $("#DaysName").val();
    if (DaysName == "" || DaysName == undefined) {
        $("#DaysName").addClass("border-danger");
        f = true;
    }
    else {
        $("#DaysName").removeClass("border-danger");
    }

    
    return f
}
function validateforTask() {
    let f = false;


    let TaskName = $("#TaskName").val();
    if (TaskName == "" || TaskName == undefined) {
        $("#TaskName").addClass("border-danger");
        f = true;
    }
    else {
        $("#TaskName").removeClass("border-danger");
    }

   

    return f
}
function cleardays() {
    //alert("cleardays")
    DaysAutoId = 0;
    $("#DaysName").val("");
    $("#Savedays").text("Save");
    $("#Task").hide();
}

function clearTask() {
    //alert("cleardays")
    TaskAutoId = 0;
    $("#TaskName").val("");
  //  $("#Savedays").text("Save");
  /*  $("#Task").hide();*/
}
function InsertUpdateDays() {

    try {
        $.post("/Package/InsertUpdateDays", {
            DaysAutoId: DaysAutoId,
            DaysName: $("#DaysName").val(), 
            PackageAutoId: PackageAutoId


        }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                Swal.fire({
                    icon: 'success',
                    text: response.responseMessage,
                }).then(() => {
                  
                        
                    EditdayData(response.data)
                
                    $("#categoryModal").modal("show");

                    ShowDays();   
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


function ShowDays() {
    try {
        debugger;
        $.post("/Package/ShowDays ", { PackageAutoId:PackageAutoId }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                console.log(response.data)
                if (response.data) {
                    $("#Data").empty();
                    $.each(response.data, function (i, item) {
                        $("#Data").append(`<tr>
                     <td style="text-align:center">
                                <a onclick="Daydelete(${item.DaysAutoId});"class="fa fa-trash-o" style='font-size: 20px;color:red;margin-right:10px;'></a>
                              <a href="#" onclick="EditdayData(${item.DaysAutoId});" class="fa fa-edit bi bi-eye" style="font-size: 20px; color: Green; margin-right: 10px;" data-bs-toggle="modal" data-bs-target="#categoryModal">
</a>                    </td>
                    <td style="text-align:center">${item.DaysAutoId}</td>
                    <td style="text-align:center">${item.DaysName}</td>
                    <td style="text-align:center">${item.TaskCount}</td>
                  
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

function Daydelete(DaysAutoId) {
    try {
        Swal.fire({
            text: 'Are you sure to delete this Days ?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.post("/Package/Daydelete", { DaysAutoId: DaysAutoId }, function (response) {
                    if (response.responseCode == 200) {
                        Swal.fire({
                            icon: 'success',
                            text: response.responseMessage,
                        });
                        ShowDays();
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
function EditdayData(DaysAutoId_Id) {
    DaysAutoId = DaysAutoId_Id;
    try {
        $.post("/Package/EditdayData", { DaysAutoId: DaysAutoId_Id }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                $("#DaysName").val(response.data.DaysName);
                $("#Packagedropdown").val(response.data.PackageAutoId);
                $("#Savedays").text("Update");
                ShowTask();
          
                $("#Task").show();
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

function clearTask() {
    //alert("cleardays")
    TaskAutoId = 0;
    $("#TaskName").val("");
    $("#SaveTask").text("Save");
    /*  $("#Task").hide();*/
}
function InsertUpdateTask() {

    try {
        $.post("/Package/InsertUpdateTask", {
            TaskAutoId: TaskAutoId,
            TaskName: $("#TaskName").val(),
            DaysAutoId: DaysAutoId


        }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                Swal.fire({
                    icon: 'success',
                    text: response.responseMessage,
                }).then(() => {
                    clearTask();
                    $("#TaskModal").modal("hide");
                    $("#categoryModal").modal("show");
                    ShowTask();
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

function ShowTask() {
    try {
        debugger;
        $.post("/Package/ShowTask ", { DaysAutoId: DaysAutoId }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                console.log(response.data)
                if (response.data) {
                    debugger;
                    $("#Datatask").empty();
                    $.each(response.data, function (i, item) {
                        $("#Datatask").append(`<tr>
                     <td style="text-align:center">
                                <a onclick="Daydelete(${item.TaskAutoId});"class="fa fa-trash-o" style='font-size: 20px;color:red;margin-right:10px;'></a>
                              <a href="#" onclick="EditTaskData(${item.TaskAutoId});" class="fa fa-edit bi bi-eye" style="font-size: 20px; color: Green; margin-right: 10px;" data-bs-toggle="modal" data-bs-target="#TaskModal">
</a>                    </td>
                    <td style="text-align:center">${item.TaskAutoId}</td>
                    <td style="text-align:center">${item.TaskName}</td>
                  
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

function Daydelete(TaskAutoId) {
    try {
        Swal.fire({
            text: 'Are you sure to delete this Task ?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.post("/Package/Taskdelete", { TaskAutoId: TaskAutoId }, function (response) {
                    if (response.responseCode == 200) {
                        Swal.fire({
                            icon: 'success',
                            text: response.responseMessage,
                        });
                        ShowTask();
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

function EditTaskData(TaskAutoId_Id) {
    TaskAutoId = TaskAutoId_Id;
    try {
        $.post("/Package/EditTaskData", { TaskAutoId: TaskAutoId_Id }, function (response) {
            debugger;
            if (response.responseCode == 200) {
                $("#TaskName").val(response.data.TaskName);
                $("#Packagedropdown").val(response.data.PackageAutoId);
                $("#SaveTask").text("Update");
                
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
