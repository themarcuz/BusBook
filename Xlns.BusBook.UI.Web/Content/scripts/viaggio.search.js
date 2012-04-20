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

function toggleDiv(idDiv) {
    $("#" + idDiv).slideToggle('fast');
}

//tappe

var tappaDialog;

function ShowLoadingElement(divId) {
    $('#' + divId).html('<img alt="loading" src="@Url.Content("~/Content/img/loading.gif")" width="24px" height="24px"/>');
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

function aggiungiTappa(urlAction, tipoTappa) {
    $.ajax({
        url: urlAction + "?tipo=" + tipoTappa,
        cache: false,
        success: function (result) {
            tappaDialog.html(result);
            $('#submitButton').show();
            tappaDialog.dialog("open");
            initializeMap();
            showLocation("Italia", true);
        },
        error: function () {
            alert("Impossibile recuperare l'editor delle tappe");
            $('#submitButton').hide();
        }
    });
}

//function modificaTappa(urlAction, idTappa) {
//    $.ajax({
//       // url: '@Url.Action("EditTappa")' + "?id=" + idTappa,
//        url: urlAction + "?id=" + idTappa,
//        cache: false,
//        success: function (result) {
//            tappaDialog.html(result);
//            $('#submitButton').show();
//            tappaDialog.dialog("open");
//        },
//        failure: function () {
//            tappaDialog.html("Impossibile recuperare l'editor delle tappe");
//            $('#submitButton').hide();
//        }
//    });
//}

function cancellaTappa(updateId) {
    $('#' + updateId + 'CAP').val("");
    $('#' + updateId + 'City').val("");
    $('#' + updateId + 'Lat').val("");
    $('#' + updateId + 'Lng').val("");
    $('#' + updateId + 'Nation').val("");
    $('#' + updateId + 'Number').val("");
    $('#' + updateId + 'Province').val("");
    $('#' + updateId + 'Region').val("");
    $('#' + updateId + 'Street').val("");

    $('#' + updateId + 'Link').text("");
    $('#' + updateId + 'Div').hide('fast');
}

function addSearchTappa(updateId) {
   var cap = $('#Location_CAP').val();
   var city = $('#Location_City').val();
   var lat = $('#Location_Lat').val();
   var lng = $('#Location_Lng').val();
   var nation = $('#Location_Nation').val();
   var number = $('#Location_Number').val();
   var province = $('#Location_Province').val();
   var region = $('#Location_Region').val();
   var street = $('#Location_Street').val();

//   alert(city + "|" + nation + "|" + province + "|" + region);

   if(city !="" && nation != "" && province != "" && region != ""){
       $('#' + updateId  + 'CAP').val(cap);
       $('#' + updateId + 'City').val(city);
       $('#' + updateId + 'Lat').val(lat);
       $('#' + updateId + 'Lng').val(lng);
       $('#' + updateId + 'Nation').val(nation);
       $('#' + updateId + 'Number').val(number);
       $('#' + updateId + 'Province').val(province);
       $('#' + updateId + 'Region').val(region);
       $('#' + updateId + 'Street').val(street);

       $('#' + updateId + 'Link').text(city + " (" + province + ") " + region + " - " + nation);
       $('#' + updateId + 'Div').show('fast');
       CloseDialog();
   }
   
}