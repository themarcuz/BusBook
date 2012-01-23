/// <reference path="../../Scripts/jquery-1.5.1-vsdoc.js" />

function Tile(IdUtente) {
    $('#TileDivUtente' + IdUtente).find('[data-delete-command]').each(function () {
        var target = $(this);
        target.click(function (e) {
            $.ajax({
                url: "/Utente/DeleteAjax/" + IdUtente,
                cache: false,
                context: target,
                success: function (data) { $(this).parents('.tileElement').hide('slow'); },
                error: function () { alert("Impossibile eliminare l'utente"); }
            });
            return false;
        });
    });    
}
    