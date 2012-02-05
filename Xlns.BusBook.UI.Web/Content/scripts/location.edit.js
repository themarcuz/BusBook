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
    if ($('#Location_Lat').val() == "" || $('#Location_Lng').val() == "") {
        showLocation('Italia', true);
    } else {
        var lat = $('#Location_Lat').val().replace(',', '.');
        var lng = $('#Location_Lng').val().replace(',', '.');
        var latlng = new google.maps.LatLng(lat, lng);
        geocoder.geocode({ 'latLng': latlng }, updateMap);
    }
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
    geocoder.geocode({ 'address': address }, updateMap);
}

function updateMap(results, status, clearSearchBox, dontZoom) {    
    if (status == google.maps.GeocoderStatus.OK) {
        if (results[0]) {
            map.setCenter(results[0].geometry.location);                                   
            if (!dontZoom) {
                map.fitBounds(results[0].geometry.viewport);
                //aumento un po' lo zoom 
                map.setZoom(map.getZoom() + 1);
            }
            //aggiorno il marker
            updateMarker(results[0].geometry.location);
            // imposta campi indirizzo
            updateAddressFields(results[0].address_components);
            $('#Location_Lng').val(results[0].geometry.location.lng());
            $('#Location_Lat').val(results[0].geometry.location.lat());
            loader.text(results[0].formatted_address);
            if (clearSearchBox) {
                $("#searchAddressField").val("");
            }
        }        
    } else {
        loader.show();
        loader.text(errorMessage);
    }
}

function updateAddressFromMarker(marker) {
    showLoader();
    $('#Location_Lng').val(marker.getPosition().lng());
    $('#Location_Lat').val(marker.getPosition().lat());    
    var lat = parseFloat(marker.getPosition().lat());
    var lng = parseFloat(marker.getPosition().lng());
    var latlng = new google.maps.LatLng(lat, lng);
    geocoder.geocode({ 'latLng': latlng }, function (results, status) { updateMap(results, status, true, true); });
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