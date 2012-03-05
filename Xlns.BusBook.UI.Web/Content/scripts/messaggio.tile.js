/// <reference path="../../Scripts/jquery-1.5.1-vsdoc.js" />

function Tile(IdMessaggio) {
    $('#TileDivMessaggio' + IdMessaggio).find('[data-delete-command]').each(function () {
        var target = $(this);
        target.click(function (e) {
            $.ajax({
                url: "/Messaggio/Delete/" + IdMessaggio,
                cache: false,
                context: target,
                success: function (data) { $(this).parents('.tileElement').hide('slow'); },
                error: function () { alert("Impossibile eliminare il messaggio"); }
            });
            return false;
        });
    });    
}
    