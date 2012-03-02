/// <reference path="../../Scripts/jquery-1.5.1-vsdoc.js" />

function Tile(IdFlyer) {
    $('#TileDivFlyer' + IdFlyer).find('[data-delete-command]').each(function () {
        var target = $(this);
        target.click(function (e) {
            $("#dialog-confirm").dialog({
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Continua": function () {
                        $(this).dialog("close");
                        Delete(IdFlyer, target);
                        Update(target);
                    },
                    "Annulla": function () {
                        $(this).dialog("close");
                    }
                }
            });

            return false;
        });
    });
}

function Delete(IdFlyer, target) {
    $.ajax({
        url: "/Flyer/DeleteAjax/" + IdFlyer,
        cache: false,
        context: target,
        success: function (data) { $(this).parents('.tileElement').hide('slow'); },
        error: function () { alert("Impossibile eliminare il flyer"); }
    });
}

function Update(target) {
    $.ajax({
        url: "/Flyer/ShowTileAjax",
        cache: false,
        context: target,
        success: function (data) { $('#topFlyers').append(data); },
        error: function () { alert("Impossibile aggiornare i top flyers"); }
    });
}

function DetailAjax(IdFlyer) {
    $.ajax({
        url: "/Flyer/ShowSelected/" + IdFlyer,
        cache: false,
        context:$('#TileDivFlyer' + IdFlyer),
        success: function (data) {$('#viaggi').html(data); },
        error: function () { alert("Impossibile visualizzare il dettaglio del flyer"); }
    });
}