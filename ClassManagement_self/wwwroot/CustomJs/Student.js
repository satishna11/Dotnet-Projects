
$(document).ready(function () {
    loadData();
});
$(document).on('click', '.btnNew', function () {
    $('#formModal').modal('show');
})
$(document).on('click', '.btnSearch', function () {
    loadData();
});
$(document).on('click', '.btnSubmit', function () {
    
    var obj = {
        StudentID: +$('.hdnStudentID').val(),
        FullName: $('.txtFullName').val(),
        Email: $('.txtEmail').val(),
        Contact: $('.Contact').val(),
        Photo: imageName,
    }
    if (obj.FullName == '') {
        showToast("Stop", "please enter Fullname", "error");
    } else if (obj.Email == '') {
        showToast("Stop", "please enter Email", "error");
    } else if (obj.Contact == '') {
        showToast("Stop", "please enter Conatct", "error");
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
                $('.hdnStudentID').val(id);
                $('.txtFullName').val(res.data.fullName);
                $('.txtEmail').val(res.data.email);
                $('.Contact').val(res.data.contact);
                imageName = res.data.photo;

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
    var fname = $('.txtFilterFullName"').val() || '';
    var email = $('.filterEmail"').val() || '';
    var contact = $('.txtFilterContact"').val() || '';
    $.ajax({
        contentType: "application/json; charset=utf-8",
        method: 'get',
        url: GetDataUrl,
        success: function (res) {
            var xyz = '';
            $.each(res.data, function (i, x) {
                xyz += '<tr>';
                xyz += '<td>' + (i + 1) + '</td>';
                xyz += '<td>' + x.fullName + '</td>';
                xyz += '<td>' + x.email + '</td>';
                xyz += '<td>' + x.contact + '</td>';
                xyz += '<td><img src="/sarbendra/' + x.photo + '" alt="" width="50" height="50" /></td>';
                xyz += '<td><button type="button" class="btn btn-secondary btn-sm btnEdit" data-key="' + x.studentID + '"><i class="fa fa-edit"></i> EDIT</button>';
                xyz += '<button type="button" class="btn btn-danger btn-sm ml-2 btnDelete" data-key="' + x.studentID + '"> <i class="fa fa-trash"></i> DELETE</button ></td > ';
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

function clearForm() {  
    $('.hdnStudentID').val(0);
    $('.txtFullName').val('');
    $('.txtEmail').val('');
    $('.Contact').val('');
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
