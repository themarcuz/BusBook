/// <reference path="../../Scripts/jquery-1.5.1-vsdoc.js" />

function Tile(IdAgenzia) {
    $('#TileDivAgenzia' + IdAgenzia).find('[data-delete-command]').each(function () {
        var target = $(this);
        target.click(function (e) {
            $.ajax({
                url: "/Agenzia/DeleteAjax/" + IdAgenzia,
                cache: false,
                context: target,
                success: function (data) { $(this).parents('.tileElement').hide('slow'); },
                error: function () { alert("Impossibile eliminare l'agenzia"); }
            });
            return false;
        });
    });    
}
    