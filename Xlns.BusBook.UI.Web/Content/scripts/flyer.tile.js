/// <reference path="../../Scripts/jquery-1.5.1-vsdoc.js" />

function Tile(IdFlyer) {
    $('#TileDivFlyer' + IdFlyer).find('[data-delete-command]').each(function () {
        var target = $(this);
        target.click(function (e) {
            $.ajax({
                url: "/Flyer/DeleteAjax/" + IdFlyer,
                cache: false,
                context: target,
                success: function (data) { $(this).parents('.tileElement').hide('slow'); },
                error: function () { alert("Impossibile eliminare il flyer"); }
            });
            return false;
        });
    });    
}
    