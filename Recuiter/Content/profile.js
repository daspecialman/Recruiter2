jQuery(document).ready(function () {
    

    jQuery('.new-education').on('click', function () {
        jQuery('#form-edu').removeClass('hide-form');
    });
    
   
    //jQuery('.close').on('click', function () {
    //    jQuery(this).closest('form').addClass('hide-form');
    //    jQuery(this).closest('form')[0].reset();
    //});

    jQuery('.new-experience').on('click', function () {
        jQuery('#form-experience').removeClass('hide-experience');
    });


    //jQuery('.close').on('click', function () {
    //    jQuery(this).closest('form').addClass('hide-experience');
    //    jQuery(this).closest('form')[0].reset();
    //});

    jQuery('.new-skill').on('click', function () {
        jQuery('#form-skill').removeClass('hide-skill');
    });


    //jQuery('.close').on('click', function () {
    //    jQuery(this).closest('form').addClass('hide-skill');
    //    jQuery(this).closest('form')[0].reset();
    //});

    jQuery('#tableEducation').on('click', '.edit-action', function () {
        var id = $(this).closest('tr').attr('data-id');
        // cal back end to get data
        jQueryajax({
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
