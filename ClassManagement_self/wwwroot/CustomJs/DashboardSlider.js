<script>
    $(document).ready(function () {
                  loadData();
                });
    $(document).on('click', '.btnNew', function () {
        $('#exampleModal').modal('show');
                });
    function loadData() {
                 var dashboardInfo = $('.txtFilterDashboardSliderInfo').val() || '';
                 var title = $('.txtFilterTitle').val() || '';
                 var subtitle = $('.txtFilterSubTitle').val() || '';
                 var orderKey = $('.txtFilterOrderKey').val() || '';


            $.ajax({
                contentType: "application/json; charset=utf-8",
            method: 'GET',
            url: '@Url.Action("GetAllData", "DashboardSlider")'
            + '?dashboardInfo=' + dashboardInfo
            + '&title=' + title
            + '&subtitle=' + subtitle
            + '&orderKey=' + orderKey,
            // Correctly construct the query string
            success: function (res) {
                                     debugger
            var dt = '';
            // Loop through the data returned from the server
            $.each(res.data, function (i, x) {
                dt += '<tr>';
            dt += '<td>' + (i + 1) + '</td>'; // Serial number
            dt += '<td>' + x.dashboardSliderInfo + '</td>'; // Dashboard Slider Info
            dt += '<td>' + x.title + '</td>'; // Title
            dt += '<td>' + x.subTitle + '</td>'; // SubTitle
            dt += '<td>' + x.backgroundImage + '</td>'; // Background Image
            dt += '<td>' + x.validFrom + '</td>'; // Valid From
            dt += '<td>' + x.validTo + '</td>';   // Valid To
            dt += '<td>' + x.orderKey + '</td>'; // Order Key
            dt += '<td>';
                dt += '<button type="button" class="btn btn-secondary btn-sm btnEdit" data-key="' + x.id + '"><i class="fa fa-edit"></i> EDIT</button>';
                dt += '<button type="button" class="btn btn-danger btn-sm ml-2 btnDelete" data-key="' + x.id + '"><i class="fa fa-trash"></i> DELETE</button>';
                dt += '</td>';
            dt += '</tr>';
                                     });

        // Update the HTML of the table body with the new rows
        $('.data-body').html(dt);
                                 },
        error: function (e) {
            console.log(e);
        },
        beforeSend: function () {
            // Optionally block the UI while the data is being retrieved
            $.blockUI();
        },
        complete: function () {
            // Optionally unblock the UI after the data is loaded
            $.unblockUI();
        }

                             });
                        }



$(document).on('click', '.btnSubmit', function () {
    var dsliderinfo = $('.txtDashboardSliderInfo').val();
    var title = $('.txtTitle').val();
    var subtitle = $('.txtSubTitle').val();
    var backgroundimage = $('.txtBackgroundImage').val();
    var validfrom = $('.txtValidFrom').val();
    var validTo = $('.txtValidTo').val();
    var orderkey = $('.txtOrderKey').val();
    var id = $('.hdnDashID').val();

    if (dsliderinfo == '') {
        toastr["error"]("I cannot proceed forward", "Please dashboard info :)");

    } else if (title == '') {
        toastr["error"]("Do I have to remind you to enter titlr also", "Please kindly fill all the form (:");
    }
    else if (subtitle == '') {
        toastr["error"]("Do I have to remind you to enter subtitlr also", "Please kindly fill all the form (:");
    }
    else if (validfrom == '') {
        toastr["error"]("Do I have to remind you to enter validfrom also", "Please kindly fill all the form (:");
    }
    else if (validTo == '') {
        toastr["error"]("Do I have to remind you to enter validto also", "Please kindly fill all the form (:");
    }
    else if (orderkey == '') {
        toastr["error"]("Enough!!!", "Please  fill  this orderkey too");
    }
    else {

        $.ajax({
            contentType: "application/json; charset=utf-8",
            method: 'POST',
            url: '@Url.Action("SaveData", "DashboardSlider")',
            data: JSON.stringify({
                DashboardSliderInfo: dsliderinfo,
                Title: title,
                SubTitle: subtitle,
                BackgroundImage: backgroundimage,
                ValidFrom: validfrom,
                ValidTo: validTo,
                OrderKey: orderkey,
                Id: id
            }),
            success: function (res) {

                if (res.success) {

                    toastr["success"]("Resource updated successfully.", "Success");

                    clearForm();
                    $('#exampleModal').modal('hide');
                }
                else {
                    toastr["success"]("Resource not updated ", "error");
                }
            },
            error: function (e) {
                console.log(e);
            },
            beforeSend: function () {
                // block ui
                $.blockUI();
            },
            complete: function () {
                // unblock ui
                $.unblockUI();
            }
        });
    }
});
function clearForm() {
    var dsliderinfo = $('.txtDashboardSliderInfo').val('');
    var title = $('.txtTitle').val('');
    var subtitle = $('.txtsubTitle').val('');
    var backgroundimage = $('.txtBackgroundImage').val('');
    var validfrom = $('.txtValidFrom').val('');
    var validTo = $('.txtValidTo').val('');
    var orderkey = $('.txtOrderKey').val('');
    var id = $('.hdnDashID').val('0');
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
});



function deleteData(id) {
    $.ajax({
        contentType: "application/json; charset=utf-8",
        method: 'get',
        // <-- Added missing comma here
        url: '@Url.Action("Delete", "DashboardSlider")?id=' + id,
        success: function (res) {
            if (res.success) {
                alert("delete");
                loadData();
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

$(document).on('click', '.btnSearch', function () {
    loadData();
});



$(document).on('click', '.btnEdit', function () {
    var btn = $(this);
    var id = $(this).data('key');
    $.ajax({
        contentType: "application/json; charset=utf-8",
        method: 'get',
        url: '@Url.Action("GetDataByID", "DashboardSlider")?id=' + id,

        success: function (res) {
            debugger;
            if (res.success) {
                let validFromDate = moment(res.data.validFrom).format('YYYY-MM-DD');
                $('#exampleModal').modal('show');
                debugger;
                $('.txtDashboardSliderInfo').val(res.data.dashboardSliderInfo);
                $('.txtTitle').val(res.data.title);
                $('.txtSubTitle').val(res.data.subTitle);
                $('.hdnDashID').val(res.data.id);
                $('.txtOrderKey').val(res.data.orderKey);
                $('.txtBackgroundImage').val(res.data.backgroundImage);
                $('.txtValidFrom').val(validFromDate);
                $('.txtValidTo').val(res.data.validTo);


            } else {
                toastr("!!!", res.message, "error");
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
}) </script>