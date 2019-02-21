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
                    
                    var htmlString = '<tr data-id="' + response.Data.Id +'">' +
                        '<td><b>' + response.Data.Qualification + ' In ' + response.Data.CourseStudies + '</b></td>' +
                        '<td>' + response.Data.FromDateFormat + ' - ' + response.Data.ToDateFormat + '</td>' +
                        '<td>' + response.Data.Institution + '</td> ' +
                        '<td><i class="far fa-edit ml-5"></i> <a class="edit-action" href="#">Edit</a></td>' +
                        '<td class="del"><i class="far fa-trash-alt ml-5"></i> Delete</td>' +
                        '</tr>';

                    var id = response.Data.Id;
                    var rowItem = jQuery('#tableEducation tbody tr[data-id="' + id + '"]');
                    if (jQuery(rowItem).length) {
                        // update
                        jQuery(rowItem).replaceWith(htmlString);
                    }

                    else {
                        // insert
                        if (jQuery('#tableEducation').hasClass('empty'))
                            jQuery('#tableEducation tbody').html(htmlString);
                        else
                            jQuery('#tableEducation tbody').append(htmlString);
                    }
                    alert(response.Message);
                    jQuery('#form-edu')[0].reset();
                    jQuery('#form-edu').addClass('hide-form')

                },
                error: function (xhr,response) {
                    GetErrorMessage(xhr);
                }
            });
          
        }
    });

    jQuery('#tableEducation').on('click', '.edit-action', function () {
        alert();
        var id = $(this).closest('tr').attr('data-id');
        // cal back end to get data
        jQuery.ajax({
            type: 'GET',
            url: '/api/applicant/Education/' + id,
            success: function (response) {

                jQuery('#form-edu input[name="Institution"]').val(response.Data.Institution);
                jQuery('#form-edu input[name="Qualification"]').val(response.Data.Qualification);
                jQuery('#form-edu input[name="FromDate"]').val(response.Data.FromDateFormat);
                jQuery('#form-edu input[name="ToDate"]').val(response.Data.ToDateFormat);
                jQuery('#form-edu input[name="CourseStudies"]').val(response.Data.CourseStudies);
                jQuery('#form-edu input[name="Id"]').val(response.Data.Id);
                jQuery('#form-edu').removeClass('hide-form');
                // bind hidden field with id
            },
            error: function (xhr, response) {
                GetErrorMessage(xhr);
            }
        });

    });

    jQuery('#tableEducation').on('click', '.delete-action', function () {

        var id = $(this).closest('tr').attr('data-id');

        if (confirm("Are you sure you want to delete this?")) {
            jQuery.ajax({
                type: 'DELETE',
                url: '/api/applicant/Education/' + id,
                success: function (response) {
                    var rowItem = jQuery('#tableEducation tbody tr[data-id="' + id + '"]');
                    if (jQuery(rowItem).length) {
                        jQuery(rowItem).remove();
                    }
                    // bind hidden field with id
                },
                error: function (xhr, response) {
                    GetErrorMessage(xhr);
                }
            });
        }
        

    });
    
});



//JQuery for Experience 
jQuery(document).ready(function () {
    jQuery('.new-experience').on('click', function () {
        jQuery('#form-exp').removeClass('hide-form');
    });

    jQuery('.close').on('click', function () {
        jQuery(this).closest('form').addClass('hide-form');
        jQuery(this).closest('form')[0].reset();
    });


    // Add new education form

    jQuery("#form-exp").validate({
        //rules: {
        //    Institute: {
        //        required: true
        //    },
        //},
        submitHandler: function (form) {

            jQuery.ajax({
                type: 'POST',
                url: '/api/applicant/AddExperience',
                data: jQuery(form).serialize(),
                success: function (responses) {

                    var htmlString = '<tr data-id="' + responses.Data.Id + '">' +
                        '<td>' + responses.Data.Title + '</td>' +
                        '<td>' + responses.Data.Company + '</td> ' +
                        '<td>' + responses.Data.FromDateFormat + ' - ' + responses.Data.ToDateFormat + '</td>' +                       
                        '<td><i class="far fa-edit ml-5"></i> <a class="edit-action" href="#">Edit</a></td>' +
                        '<td class="del"><i class="far fa-trash-alt ml-5"></i> Delete</td>' +
                        '</tr>';

                    var id = responses.Data.Id;
                    var rowItem = jQuery('#tableExperience tbody tr[data-id="' + id + '"]');
                    if (jQuery(rowItem).length) {
                        // update
                        jQuery(rowItem).replaceWith(htmlString);
                    }

                    else {
                        // insert
                        if (jQuery('#tableExperience').hasClass('empty'))
                            jQuery('#tableExperience tbody').html(htmlString);
                        else
                            jQuery('#tableExperience tbody').append(htmlString);
                    }
                    alert(responses.Message);
                    jQuery('#form-exp')[0].reset();
                    jQuery('#form-exp').addClass('hide-form')

                },
                error: function (xhr, responses) {
                    GetErrorMessage(xhr);
                }
            });

        }
    });

    jQuery('#tableExperience').on('click', '.edit-action', function () {
        alert();
        var id = $(this).closest('tr').attr('data-id');
        // cal back end to get data
        jQuery.ajax({
            type: 'GET',
            url: '/api/applicant/Experience/' + id,
            success: function (responses) {

                jQuery('#form-exp input[name="Title"]').val(responses.Data.Title);
                jQuery('#form-exp input[name="FromDate"]').val(response.Data.FromDateFormat);
                jQuery('#form-exp input[name="ToDate"]').val(response.Data.ToDateFormat);
                jQuery('#form-exp input[name="Company"]').val(response.Data.Company);
                jQuery('#form-exp input[name="Id"]').val(responses.Data.Id);
                jQuery('#form-exp').removeClass('hide-form');
                // bind hidden field with id
            },
            error: function (xhr, responses) {
                GetErrorMessage(xhr);
            }
        });

    });

    jQuery('#tableExperience').on('click', '.delete-action', function () {

        var id = $(this).closest('tr').attr('data-id');

        if (confirm("Are you sure you want to delete this?")) {
            jQuery.ajax({
                type: 'DELETE',
                url: '/api/applicant/Experience/' + id,
                success: function (responses) {
                    var rowItem = jQuery('#tableExperience tbody tr[data-id="' + id + '"]');
                    if (jQuery(rowItem).length) {
                        jQuery(rowItem).remove();
                    }
                    // bind hidden field with id
                },
                error: function (xhr, responses) {
                    GetErrorMessage(xhr);
                }
            });
        }


    });

});



//JQuery for Skill
jQuery(document).ready(function () {
    jQuery('.new-skill').on('click', function () {
        jQuery('#form-skill').removeClass('hide-form');
    });

    jQuery('.close').on('click', function () {
        jQuery(this).closest('form').addClass('hide-form');
        jQuery(this).closest('form')[0].reset();
    });


    // Add new education form

    jQuery("#form-skill").validate({
        //rules: {
        //    Institute: {
        //        required: true
        //    },
        //},
        submitHandler: function (form) {

            jQuery.ajax({
                type: 'POST',
                url: '/api/applicant/AddSkills',
                data: jQuery(form).serialize(),
                success: function (responses) {

                    var htmlString = '<tr data-id="' + responses.Data.Id + '">' +
                        '<td>' + responses.Data.SkillTitle + '</td>' +
                        '<td>' + responses.Data.Skilllevel + '</td> ' +
                        '<td><i class="far fa-edit ml-5"></i> <a class="edit-action" href="#">Edit</a></td>'+
                        '<td class="del"><i class="far fa-trash-alt ml-5"></i> Delete</td>' +
                        '</tr>';

                    var id = responses.Data.Id;
                    var rowItem = jQuery('#tableSkill tbody tr[data-id="' + id + '"]');
                    if (jQuery(rowItem).length) {
                        // update
                        jQuery(rowItem).replaceWith(htmlString);
                    }

                    else {
                        // insert
                        if (jQuery('#tableSkill').hasClass('empty'))
                            jQuery('#tableSkill tbody').html(htmlString);
                        else
                            jQuery('#tableSkill tbody').append(htmlString);
                    }
                    alert(responses.Message);
                    jQuery('#form-skill')[0].reset();
                    jQuery('#form-skill').addClass('hide-form')

                },
                error: function (xhr, responses) {
                    GetErrorMessage(xhr);
                }
            });

        }
    });

    jQuery('#tableSkill').on('click', '.edit-action', function () {
        alert();
        var id = $(this).closest('tr').attr('data-id');
        // cal back end to get data
        jQuery.ajax({
            type: 'GET',
            url: '/api/applicant/Skills/' + id,
            success: function (responses) {

                jQuery('#form-skill input[name="SkillTitle"]').val(responses.Data.SkillTitle);
                jQuery('#form-skill input[name="Id"]').val(responses.Data.Id);
                jQuery('#form-skill input[name="Skilllevel"]').val(response.Data.SkillLevel);
                
                jQuery('#form-skill').removeClass('hide-form');
                // bind hidden field with id
            },
            error: function (xhr, responses) {
                GetErrorMessage(xhr);
            }
        });

    });

    jQuery('#tableSkill').on('click', '.delete-action', function () {

        var id = $(this).closest('tr').attr('data-id');

        if (confirm("Are you sure you want to delete this?")) {
            jQuery.ajax({
                type: 'DELETE',
                url: '/api/applicant/Skills/' + id,
                success: function (responses) {
                    var rowItem = jQuery('#tableSKill tbody tr[data-id="' + id + '"]');
                    if (jQuery(rowItem).length) {
                        jQuery(rowItem).remove();
                    }
                    // bind hidden field with id
                },
                error: function (xhr, responses) {
                    GetErrorMessage(xhr);
                }
            });
        }


    });

});






function GetErrorMessage(xhrResponse) {
    if (xhrResponse.status === 0) {
        alert('Not connect.\n Verify Network.');
    } else if (xhrResponse.status === 404) {
        alert('Requested page not found. [404]');
    } else if (xhrResponse.status === 500) {
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
