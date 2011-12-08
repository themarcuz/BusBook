var errorMessage = "L'identificazione geografica non è avvenuta";

function initialize() {
    var loader = $("#loaderMessage");
    loader.hide();
    var lat = $('#hiddenLat').val().replace(',', '.');
    var lng = $('#hiddenLng').val().replace(',', '.');

    var latlng = new google.maps.LatLng(lat, lng);
    var options = {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        panControl: false,
        zoomControl: true,
        mapTypeControl: true,
        scaleControl: false,
        streetViewControl: true,
        overviewMapControl: false
    };
    var map = new google.maps.Map(document.getElementById("map_canvas"), options);
    var geocoder = new google.maps.Geocoder();
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

$(function () {
    initialize();
}); 
   