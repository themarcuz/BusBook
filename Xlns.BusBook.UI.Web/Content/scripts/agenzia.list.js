/// <reference path="../../Scripts/jquery-1.5.1-vsdoc.js" />
$(function () {
    handleTilesCommands();
});

function beginInitialLetterFilter() {
    hideResult();
    clearSearchBox();
}

function clearSearchBox() {
    $("#q").val("");
}

function hideResult() {
    $("#elencoAgenzie").hide('fast');
}

function showResult() {
    $("#elencoAgenzie").show('fast');
    handleTilesCommands();
}

function handleTilesCommands() {
    $('[data-agenzia-id]').each(function () {
        var target = $(this);
        var id = target.attr('data-agenzia-id');
        target.click(function (e) {
            deleteAgenzia(id, target);
            return false;
        });
    });
}

function deleteAgenzia(idAgenzia, sender) {
    $.ajax({
        url: "/Agenzia/DeleteAjax/" + idAgenzia,
        cache: false,
        context: sender,
        success: function (data) { $(this).parents('.tileAgenzia').hide('slow'); },
        error: function () { alert("Impossibile eliminare l'agenzia"); }
    });   
}
