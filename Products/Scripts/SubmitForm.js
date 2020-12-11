
function PopupForm(url) {

    var formDiv = $('.modal-body');
    $.get(url)
        .done(function (response) {
            var modal = $(this);
            formDiv.html(response);
        });
}

function SubmitForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: "POST",
            url: form.action,
            data: $(form).serialize(),
            success: function (data) {
                if (data.success) {
                    $("#modalAdd").modal('hide');
                    dataTable.ajax.reload();
                }
            }
        });
    }
    return false;
}