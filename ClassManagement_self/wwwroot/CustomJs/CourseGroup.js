$(document).ready(function () {
    loadData();
});
$(document).on('click', '.btnNew', function () {
    $('#formModal').modal('show');
})

$(document).on('click', '.btnSubmit', function () {

    var obj = {
        CourseGroupID: +$('.hdnCourseGroupID').val(),
        Title: $('.txtTitle').val(),
        SubTitle: $('.txtSubTitle').val(),


    }
    if (obj.Title == '') {
        showToast("Stop", "please enter Tax name", "error");
    }
    else if (obj.SubTitle == '') {
        showToast("Stop", "please enter TaxCode", "error");
    }


    else {
        $.ajax({
            contentType: "application/json; charset=utf-8",
            method: 'post',
            url: SaveUrl,
            data: JSON.stringify(obj),
            success: function (res) {
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

function loadData() {
    var Title = $('.txtFilterTitle').val() || '';
    var SubTitle = $('.txtFilterSubTitle').val() || '';
    $.ajax({
        contentType: "application/json; charset=utf-8",
        method: 'get',
        url: GetDataUrl +'?Title=' + Title + '&SubTitle=' + SubTitle ,
        success: function (res) {
            var xyz = '';
            $.each(res.data, function (i, x) {
                xyz += '<tr>';
                xyz += '<td>' + (i + 1) + '</td>';
                xyz += '<td>' + x.title + '</td>';
                xyz += '<td>' + x.subTitle + '</td>';
                xyz += '<td><button type="button" class="btn btn-secondary btn-sm btnEdit " data-key="' + x.courseGroupID + '"><i class="fa fa-edit"></i> EDIT</button>';
                xyz += '<button type="button" class="btn btn-danger btn-sm btnDelete ms-2 " data-key="' + x.courseGroupID + '"> <i class="fa fa-trash"></i> DELETE</button ></td > ';
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
$(document).on('click', '.btnEdit', function () {
    var btn = $(this);
    var id = $(this).data('key');
    $.ajax({
        contentType: "application/json; charset=utf-8",
        method: 'get',
        url: GetDataByID + '?id=' + id,
        success: function (res) {
                
            if (res.success) {
                $('#formModal').modal('show');
                $('.hdnCourseGroupID').val(id);
                $('.txtTitle').val(res.data.title);
                $('.txtSubTitle').val(res.data.subTitle);
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
function deleteData(id) {
    $.ajax({
        contentType: "application/json; charset=utf-8",
        method: 'get',
        url: DeleteDataUrl +'?id=' + id,
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
function clearForm() {

    $('.txtTitle').val('');
    $('.txtSubTitle').val('');
    $('.hdnCourseGroupID').val('0');

}
$(document).on('click', '.btnCancel', function () {
    $.confirm({
        title: 'Confirm!',
        type: 'red',
        content: 'Are you sure to clear form?',
        buttons: {
            confirm: function () {
                clearForm();
            },
            cancel: function () {

            },
        }
    });
});
$(document).on('click', '.btnSearch', function () {
    loadData();
})