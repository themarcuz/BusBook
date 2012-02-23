/// <reference path="../../Scripts/jquery-1.5.1-vsdoc.js" />
$('input[type=checkbox]').click(function (e) {
    var errMsg = "Errore: aggiornamento flyer fallito";

    if ($(this).is(':checked'))
        errMsg = "Impossibile aggiungere il viaggio al flyer";
    else
        errMsg = "Impossibile rimuovere il viaggio dal flyer";

    $.ajax({
        type: 'POST',
        url: "/Flyer/ToggleViaggio",
        cache: false,
        data: { idViaggio: this.id },
        context: $(this),
        error: function () { alert(errMsg); }
    });
});
