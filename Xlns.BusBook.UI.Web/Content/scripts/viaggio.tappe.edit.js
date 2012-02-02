var tappaDialog;

function ShowLoadingElement() {
    $('#ListaTappeEditabili').html('<img alt="loading" src="@Url.Content("~/Content/img/loading.gif")" width="24px" height="24px"/>');
}

function CloseDialog() {
    tappaDialog.dialog("close");
}

$(function () {
    tappaDialog = $('#EditTappaDialog');
    tappaDialog.dialog(
    {
        modal: true,
        autoOpen: false,
        width: '800',
        resizable: false
    });
});
function aggiungiTappa(tipoTappa) {
    $.ajax({
        url: '@Url.Action("CreateTappa")' + "?tipo=" + tipoTappa + "&idViaggio=@Model.Id",
        cache: false,
        success: function (result) {
            tappaDialog.html(result);
            $('#submitButton').show();
            tappaDialog.dialog("open");
        },
        failure: function () {
            tappaDialog.html("Impossibile recuperare l'editor delle tappe");
            $('#submitButton').hide();
        }
    });
}

function modificaTappa(idTappa) {
    $.ajax({
        url: '@Url.Action("EditTappa")' + "?id=" + idTappa,
        cache: false,
        success: function (result) {
            tappaDialog.html(result);
            $('#submitButton').show();
            tappaDialog.dialog("open");
        },
        failure: function () {
            tappaDialog.html("Impossibile recuperare l'editor delle tappe");
            $('#submitButton').hide();
        }
    });
}