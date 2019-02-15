jQuery(document).ready(function () {
    jQuery('.new-education').on('click', function () {
        jQuery('#form-edu').removeClass('hide-form');
    });

    jQuery('.close').on('click', function () {
        jQuery(this).closest('form').addClass('hide-form');
        jQuery(this).closest('form')[0].reset();
    });


    // Add new education form

    jQuery("#form-edu").validate({
        //rules: {
        //    Institute: {
        //        required: true
        //    },
        //},
        submitHandler: function (form) {
            
            jQuery.ajax({
                type: 'POST',
                url: '/api/applicant/AddOrUpdate',
                data: jQuery(form).serialize(),
                success: function (response) {
                    console.log(response);
                    var htmlString = '<tr data-id="' + response.Id +'">' +
                        '<td>' + response.Data.CourseStudies + '</td>' +
                        '<td>' + response.Data.ToDate+'</td> ' +
                        '<td></td>' +
                        '<td></td>' +
                        '<td></td>' +
                        '</tr>';

                    var id = response.Data.Id;
                    var rowItem = jQuery('#tableEducation tbody tr[data-id="' + id + '"]');
                    if (jQuery(rowItem).length) {
                        // update
                        jQuery(rowItem).replaceWith(htmlString);
                    }

                    else {
                        // insert
                        jQuery('#tableEducation tbody').append(htmlString);
                    }
                    alert(response.Message);

                },
                error: function (xhr,response) {
                    GetErrorMessage(xhr);
                }
            });
          
        }
    });

    jQuery('#tableEducation').on('click', '.edit-action', function () {
        var id = $(this).closest('tr').attr('data-id');
        // cal back end to get data
        jQuery.ajax({
            type: 'GET',
            url: 'api/Applicant/ApplicantResumeProfile/' + id,
            success: function (response) {

                jQuery('#qualificationName').val(response.Data.fullName);

                // bind hidden field with id
            },
            error: function (data) {

            }
        });

    });


    jQuery('#tableEducation').on('click', '.edit-action', function () {
        var id = $(this).closest('tr').attr('data-id');
        // cal back end to get data
        jQuery.ajax({
            type: 'GET',
            url: 'api/Applicant/ApplicantResumeProfile/' + id,
            success: function (response) {

                jQuery('#qualificationName').val(response.Data.fullName);

                // bind hidden field with id
            },
            error: function (data) {

            }
        });

    });
});


function GetErrorMessage(xhrResponse) {
    if (xhrResponse.status === 0) {
        alert('Not connect.\n Verify Network.');
    } else if (xhrResponse.status == 404) {
        alert('Requested page not found. [404]');
    } else if (xhrResponse.status == 500) {
        alert('Internal Server Error [500].');
    } else if (exception === 'parsererror') {
        alert('Requested JSON parse failed.');
    } else if (exception === 'timeout') {
        alert('Time out error.');
    } else if (exception === 'abort') {
        alert('Ajax request aborted.');
    } else {
        alert('Uncaught Error.\n' + xhrResponse.responseText);
    }
}

function myFunction() {
    var x = document.getElementById("form-edu");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}

function ourFunction() {
    var x = document.getElementById("show-edu-table");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}
