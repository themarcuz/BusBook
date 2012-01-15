var errorMsg = "L'identificazione geografica non è avvenuta";
var hidLatId = "hiddenLat";
var hidLngId = "hiddenLng";
var mapId = "map_canvas";
var loaderId = "loaderMessage";


function initializeLocation(errorMessage, hiddenLatId, hiddenLngId, mapDivId, loaderMessageSpanId) {    
    var loader = $("#" + loaderMessageSpanId);
    loader.hide();
    var lat = $('#' + hiddenLatId).val().replace(',', '.');
    var lng = $('#' + hiddenLngId).val().replace(',', '.');
    var latlng = new google.maps.LatLng(lat, lng);
    var options = {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        draggable: true,
        panControl: false,
        zoomControl: false,
        mapTypeControl: false,
        scaleControl: false,
        streetViewControl: false,
        overviewMapControl: false
    };
    var map = new google.maps.Map(document.getElementById(mapDivId), options);
    var geocoder = new google.maps.Geocoder();
    //alert("Posizione: " + lat + " - " + lng);
    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            if (results[0]) {
                map.setCenter(results[0].geometry.location);
                map.fitBounds(results[0].geometry.viewport);
                //aumento un po' lo zoom
                map.setZoom(map.getZoom() + 1);
                marker = new google.maps.Marker({
                    map: map,
                    position: latlng,
                    draggable: false
                });
            }
        } else {
            loader.show();
            loader.text(errorMessage);
        }        
    });
    
}