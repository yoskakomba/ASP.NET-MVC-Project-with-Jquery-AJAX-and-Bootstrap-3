// FUNCTION TO ADD NEW PRODUCT
function jqueryAjaxPost(form)
{
    $.validator.unobtrusive.parse(form);
    if ($(form).valid())
    {

        $.ajax({

            type: "POST",
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                
                $("#myViewAll").html(response);
                confirm("Successfuly Submitted");
                
            }
        })

    }
}


// FUNCTION TO EDIT NEW PRODUCT
function Edit(url) {
    $.ajax({
        type: "GET",
        url: url,
        success: function(response) {

            $("#myModalEdit").html(response);
            //$("#myModal").html('Edit');
            //$("#modal-body").html('Edit');

        }
    });
}

function Delete(url) {
    $.ajax({
        type: "POST",
        url: url,
        success: function(response) {

            $("#myModalDelete").html(response);
            //$("#myModal").html('Edit');
            //$("#modal-body").html('Edit');

        }
    });
}
