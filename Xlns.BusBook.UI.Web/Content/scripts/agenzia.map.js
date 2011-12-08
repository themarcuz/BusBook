var map;
var geocoder;
var marker;
var loader;
var errorMessage = "L'identificazione geografica non è avvenuta";
var loadingMessage = "Caricamento...";

function initializeMap() {
    var options = {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        panControl: false,
        zoomControl: true,
        mapTypeControl: true,
        scaleControl: false,
        streetViewControl: true,
        overviewMapControl: false
    };
    map = new google.maps.Map(document.getElementById("map_canvas"), options);
    geocoder = new google.maps.Geocoder();
    showLocation('Italia', false);
}

$(function () {
    loader = $("#loaderMessage");
    loader.hide();
    initializeMap();
    $("#searchAddressField").keyup(function (event) {
        if (event.keyCode == 13) {
            $("#searchAddressButton").click();
        }
    });
    $('#submitButton').click(function (evt) {
        evt.preventDefault();        
        var $form = $('#salvaAgenziaForm');
        if ($form.valid()) {
            $form.submit();
        }
    });
});


function showLoader() {
    loader.text(loadingMessage);
    loader.show();
}

function refineLocation() {
    var address = $("#Location_Nation").val() + ", "
                        + $("#Location_Region").val() + ", "
                        + $("#Location_Province").val() + ", "
                        + $("#Location_City").val() + ", "
                        + $("#Location_CAP").val() + ", "
                        + $("#Location_Street").val() + ", "
                        + $("#Location_Number").val();
    $("#searchAddressField").val("");
    showLocation(address, true);
}

function showLocation(address, showMarker) {
    if (!address) return;
    showLoader();
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);
            map.fitBounds(results[0].geometry.viewport);
            if (showMarker) {
                //aumento un po' lo zoom
                map.setZoom(map.getZoom() + 1);
                //aggiorno il marker
                updateMarker(results[0].geometry.location);
                // imposta campi indirizzo
                updateAddressFields(results[0].address_components);
                $('#Location_Lng').val(results[0].geometry.location.lng());
                $('#Location_Lat').val(results[0].geometry.location.lat());                
            }
            loader.hide();
        } else {
            loader.text(errorMessage);
        }
    });
}

function updateAddressFromMarker(marker) {
    showLoader();
    $('#Location_Lng').val(marker.getPosition().lng());
    $('#Location_Lat').val(marker.getPosition().lat());    
    var lat = parseFloat(marker.getPosition().lat());
    var lng = parseFloat(marker.getPosition().lng());
    var latlng = new google.maps.LatLng(lat, lng);
    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            if (results[0]) {
                updateMarker(latlng);
                updateAddressFields(results[0].address_components);
                $("#searchAddressField").val("");
            }
            loader.hide();
        } else {
            loader.text(errorMessage);
        }
    });
}

function updateMarker(location) {
    cleanupMarker();
    marker = new google.maps.Marker({
        map: map,
        position: location,
        draggable: true
    });
    google.maps.event.addListener(marker, 'dragend', function () {
        updateAddressFromMarker(marker);
    });
}

function cleanupMarker() {
    if (marker) marker.setMap(null);
}

function resetAddressFields() {
    $("#Location_Street").val("");
    $("#Location_Nation").val("");
    $("#Location_Region").val("");
    $("#Location_Province").val("");
    $("#Location_CAP").val("");
    $("#Location_Street").val("");
    $("#Location_City").val("");
    /*$("#Location_Number").val("");*/
}

function updateAddressFields(components) {
    resetAddressFields();
    $.each(components, function (index, value) {
        if (value.types[0] != "undefined") {
            if (value.types[0] == 'street') { $("#Location_Street").val(value.long_name); }
            else if (value.types[0] == 'country') { $("#Location_Nation").val(value.long_name); }
            else if (value.types[0] == 'administrative_area_level_1') { $("#Location_Region").val(value.long_name); }
            else if (value.types[0] == 'administrative_area_level_2') { $("#Location_Province").val(value.short_name); }
            else if (value.types[0] == 'postal_code') { $("#Location_CAP").val(value.long_name); }
            else if (value.types[0] == 'route') { $("#Location_Street").val(value.long_name); }
            else if (value.types[0] == 'locality') { $("#Location_City").val(value.long_name); }
            else if (value.types[0] == 'street_number') {
                $("#Location_Number").val("");                
                $("#Location_Number").val(value.long_name);
            }

        }
    });
}