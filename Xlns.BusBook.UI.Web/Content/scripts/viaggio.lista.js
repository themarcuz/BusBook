$(function () {    
    $(".hiddenTappaDetail").hide();
    $(".visualizzaDettaglioTappaLink")
            .each(function () {
                var target = $(this);
                var id = target.attr('data-viaggio-id');
                target.click(function () {
                    $("#hiddenTappaDetail" + id).toggle("fade");
                    return false;
                });
            });
});