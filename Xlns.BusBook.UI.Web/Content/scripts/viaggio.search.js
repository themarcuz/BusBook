/// <reference path="../../Scripts/jquery-1.5.1-vsdoc.js" />

function hideResult(idDiv) {
    $("#" + idDiv).hide('fast');
}

function showResult(idDiv) {
    $("#" + idDiv).show('fast');
}

function onFailure() {
    alert("Errore nella ricerca del viaggio!");
}

$(function () {
    $(".datepicker").datepicker(
                                    {
                                        dateFormat: "dd MM yy"
                                    });
    $(".datepicker").attr('readonly', true);
});
