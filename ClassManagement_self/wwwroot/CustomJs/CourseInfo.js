//time picker



$(document).ready(function () {
    $('.courseTime').clockTimePicker();
    $('.txtFilterCourseTime').clockTimePicker();
    //take the time value from  by
    /*$('.courseTime').clockTimePicker('val');*/
    //set the time as:
    /* $('.courseTime').clockTimePicker('val', '08:00');*/

    loadData();
    //Load Course Group
    loadCourseGroup(); 
    loadTrainer();

});
$(document).on('click', '.btnNew', function () {
    $('#formModal').modal('show');

})

//CK Editor
let ckEditorInstance;

ClassicEditor
    .create(document.querySelector('#editor'))
    .then(editor => {
        ckEditorInstance = editor;  // Store the editor instance for later use
    })
    .catch(error => {
        console.error(error);
    });

    /*
    -- get data from CK editor --
const content = ckEditorInstance.getData();

    -- set data in CK Editor --
    const newContent = '<h2>This is the new content set from jQuery!</h2>';
            ckEditorInstance.setData(newContent); 
    */

//end of CK Editor
$(document).on('click', '.btnSearch', function () {
    loadData();
})
$(document).on('click', '.btnSubmit', function () {
    const content = ckEditorInstance.getData();
    
        var obj = {
            InfoID: +$('.hdnCourseInfoID').val(),
            Title: $('.txtTitle').val(),
            CourseGroupID: +$('.ddlCourseGroup').val(),
            ShortDescription: $('.txtShortDescription').val(),
            TotalPrice: +$('.txtTotalPrice').val(),
            CourseTime: $('.courseTime').clockTimePicker('val'),
            MaxStudent: +$('.MaxStudents').val(),
            DailyDuration: $('.DailyDuration').val(),
            DetailDEscription: content,  // Corrected here
            TrainerID: +$('.ddlTrainer').val(),
            Thumbnail: imageName
        };



    

    if (obj.Title == '') {
        showToast("Stop", "please enter Title", "error");
    }
    else if (obj.CourseGroupID == 0||'') {
        showToast("Stop", "please select Course Group", "error");
    } else if (obj.ShortDescription == '') {
        showToast("Stop", "please enter Short description", "error");
    }else if (obj.TotalPrice == '') {
        showToast("Stop", "please enter total Price", "error");
    } else if (obj.CourseTime == '') {
        showToast("Stop", "please enter course Time", "error");
    } else if (obj.MaxStudents == '') {
        showToast("Stop", "please enter max Students", "error");
    } else if (obj.DailyDuration == '') {
        showToast("Stop", "please enter daily duration", "error");
    } else if (obj.DetailDEscription == '') {
        showToast("Stop", "please enter detailed description", "error");
    } else if (obj.Trainer == 0 || '') {
        showToast("Stop", "please select trainer", "error");
    }

    else {
        $.ajax({
            contentType: "application/json; charset=utf-8",
            method: 'post',
            url: SaveUrl,
            data: JSON.stringify(obj),
            success: function (res) {
                debugger;
                if (res.success) {
                    showToast("!!!", res.message, "success");
                    clearForm();
                    $('#formModal').modal('hide');
                    loadData();
                } else {
                    showToast("!!!", res.message, "error");
                }
            },
            error: function (e) {
                console.log(e);
            },
            beforeSend: function () {
                // block ui
                //$.blockUI();
            },
            complete: function () {
                // unblock ui
                //$.unblockUI();
            }


        });
    }
});
$(document).on('click', '.btnCancel', function () {
    $.confirm({
        title: 'Confirm!',
        type: 'red',
        content: 'Are you sure to clear form?',
        buttons: {
            confirm: function () {
                clearForm();
                $('#formModal').modal('hide');
            },
            cancel: function () {

            },
        }
    });
}); // <- T
$(document).on('click', '.btnEdit', function () {
    var btn = $(this);
    var id = $(this).data('key');
    $.ajax({
        contentType: "application/json; charset=utf-8",
        method: 'get',
        url: GetDataByID +'?id=' + id,
        success: function (res) {

            if (res.success) {
                $('#formModal').modal('show');
                $('.hdnCourseInfoID').val(id);
                $('.txtTitle').val(res.data.title);
                $('.ddlCourseGroup').val(res.data.courseGroupID);
                $('.txtShortDescription').val(res.data.shortDescription);
                $('.txtTotalPrice').val(res.data.totalPrice);
                $('.courseTime').clockTimePicker('val', res.data.courseTime);
                $('.MaxStudents').val(res.data.maxStudent);
                $('.DailyDuration').val(res.data.dailyDuration);
                /*debugger;*/
                ckEditorInstance.setData(res.data.detailDEscription);
                $('.ddlTrainer').val(res.data.trainerID);
                imageName = res.data.thumbnail;
                
            } else {
                showToast("!!!", res.message, "error");
            }
        },
        error: function (e) {
            console.log(e);
        },
        beforeSend: function () {
            $.blockUI();
        },
        complete: function () {
            $.unblockUI();
        }
    });

});
function loadData() {
   
    $.ajax({
        contentType: "application/json; charset=utf-8",
        method: 'get',
        url: GetDataUrl,
        success: function (res) {
            var xyz = '';
            $.each(res.data, function (i, x) {
                xyz += '<tr>';
                xyz += '<td>' + (i + 1) + '</td>';
                xyz += '<td>' + x.title + '</td>';
                xyz += '<td>' + x.courseGroup + '</td>';
                xyz += '<td>' + x.shortDescription + '</td>';
                xyz += '<td>' + x.totalPrice + '</td>';
                xyz += '<td>' + x.courseTime + '</td>';
                xyz += '<td>' + x.maxStudent + '</td>';
                xyz += '<td>' + x.dailyDuration + '</td>';
                xyz += '<td><img src="/satishna/' + x.thumbnail + '" alt="" width="50" height="50" /></td>';
                xyz += '<td>' + x.trainerName + '</td>';
                xyz += '<td><button type="button" class="btn btn-secondary btn-sm btnEdit" data-key="' + x.infoID + '"><i class="fa fa-edit"></i> EDIT</button>';
                xyz += '<button type="button" class="btn btn-danger btn-sm ml-2 btnDelete" data-key="' + x.infoID + '"> <i class="fa fa-trash"></i> DELETE</button ></td > ';
                xyz += '</tr>';
            });

            $('.data-body').html(xyz);
        },
        error: function (e) {
            console.log(e);
        },
        beforeSend: function () {
            $.blockUI();
        },
        complete: function () {
            $.unblockUI();
        }
    });
}
$(document).on('click', '.btnDelete', function () {
    var btn = $(this);
    $.confirm({
        title: 'Confirm!',
        type: 'red',
        content: 'Are you sure to delete?',
        buttons: {
            confirm: () => {
                var id = $(btn).data('key')
                deleteData(id);
            },
            cancel: function () {

            },
        }
    });
})

function loadCourseGroup() {
    $.ajax({
        contentType: "application/json; charset=utf-8",
        method: 'get',
        url: GetCourseGroup,
        success: function (response) {
            /*var res = JSON.parse(response.message);*/
            var res = response.data;
            var xyz = '<option value="0">--select--</option>';
            $.each(res, function (i, x) {
                xyz += '<option value="' + x.courseGroupID + '" >' + x.title + '</option>';
            });

            $('.ddlCourseGroup').html(xyz);
            $('.ddlFilterCourseGroup').html(xyz);
        },
        error: function (e) {
            console.log(e);
        },
        beforeSend: function () {
            $.blockUI();
        },
        complete: function () {
            $.unblockUI();
        }
    });
}

function clearForm() {
    $('.hdnCourseInfoID').val(0);
    $('.txtTitle').val('');
    $('.ddlCourseGroup').val(0);
    $('.txtShortDescription').val('');
    $('.txtTotalPrice').val('');
    $('.courseTime').clockTimePicker('val', '00:00');
    $('.MaxStudents').val('');
    $('.DailyDuration').val('');
    ckEditorInstance.setData(''); 
    $('.ddlTrainer').val(0);
    imageName = '';
}

function deleteData(id) {
    $.ajax({
        contentType: "application/json; charset=utf-8",
        method: 'get',
        url: DeleteDataUrl + '?id=' + id,
        success: function (res) {
            if (res.success) {
                showToast("!!!", res.message, "success");
                loadData();

            } else {
                showToast("!!!", res.message, "error");
            }
        },
        error: function (e) {
            console.log(e);
        },
        beforeSend: function () {
            $.blockUI();
        },
        complete: function () {
            $.unblockUI();
        }
    });
}

    function loadTrainer() {
        $.ajax({
            contentType: "application/json; charset=utf-8",
            method: 'get',
            url: GetTrainer,
            success: function (response) {
                /*var res = JSON.parse(response.message);*/
                var res = response.data;
                var xyz = '<option value="0">--select--</option>';
                $.each(res, function (i, x) {
                    xyz += '<option value="' + x.trainerID + '" >' + x.fullName + '</option>';
                });

                $('.ddlTrainer').html(xyz);
                $('.ddlFilterTrainer').html(xyz);
            },
            error: function (e) {
                console.log(e);
            },
            beforeSend: function () {
                $.blockUI();
            },
            complete: function () {
                $.unblockUI();
            }
        });
    }