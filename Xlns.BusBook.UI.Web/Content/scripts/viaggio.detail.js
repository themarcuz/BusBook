var errorMessage = "L'identificazione geografica non è avvenuta";
var loader;
var geocoder;
var map;
var debug = false;

$(function () {
    drawTrip();
    //fakeMap();
});

/*
function fakeMap() {
initialize();
    
var locations = getAllLocations();
var center = getCenter(locations);
geocoder.geocode({ 'latLng': center }, function (results, status) {
if (status == google.maps.GeocoderStatus.OK) {
if (results[0]) {
map.setCenter(results[0].geometry.location);
map.fitBounds(results[0].geometry.viewport);
//aumento un po' lo zoom
//map.setZoom(map.getZoom() + 1);
//setMarkers(locations);
}
} else {
loader.show();
loader.text(errorMessage);
}
});
}
*/

function drawTrip() {
    initialize();
    var locations = getAllLocations();
    var center = getCenter(locations);
    var bounds = getBounds(center, locations);
    geocoder.geocode({ 'latLng': center }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            if (results[0]) {
                map.setCenter(results[0].geometry.location);
                map.fitBounds(bounds);
                //aumento un po' lo zoom
                //map.setZoom(map.getZoom() + 1);
                setRouteInMap(locations);
                setMarkers(locations);
            }
        } else {
            loader.show();
            loader.text(errorMessage);
        }
    });
}

function initialize() {
    loader = $("#loaderMessage");
    loader.hide();
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
}

// recupera le coordinate delle tappe del viaggio
function getAllLocations() {
    var locations = new Array();
    var i = 0;
    $("[data-tappa-id]").each(function () {
        var tappaId = $(this).attr("data-tappa-id");
        if (debug)
            alert("Id tappa: " + tappaId);
        var lat = $(this).find("[name=tbTappa" + tappaId + "Lat]").val().replace(',', '.');
        var lng = $(this).find("[name=tbTappa" + tappaId + "Lng]").val().replace(',', '.');
        if (debug)
            alert("Coordinate : Lat: " + lat + " - Lng: " + lng);
        var latlng = new google.maps.LatLng(lat, lng);
        locations[i] = latlng;
        i++;
    });
    if (debug)
        alert("individuate " + i + " locations");
    return locations;
}

function getCenter(locations) {
    var numberOfLocations = locations.length;
    var sumOfLats = 0;
    var sumOfLngs = 0;
    for (var i = 0; i < numberOfLocations; i++) {
        sumOfLats += locations[i].lat();
        sumOfLngs += locations[i].lng();
    }
    if (debug) {
        alert("Somma Lat: " + sumOfLats);
        alert("Somma Lng: " + sumOfLngs);
    }
    var center = new google.maps.LatLng(sumOfLats / numberOfLocations, sumOfLngs / numberOfLocations);
    if (debug)
        alert("Centro del viaggio: " + center.lat() + " - " + center.lng());
    return center;
}

function setMarkers(locations) {
    for (var i = 0; i < locations.length; i++) {
        new google.maps.Marker({
            map: map,
            position: locations[i],
            draggable: false
        });
    }
}

function getBounds(center, locations) {
    bounds = new google.maps.LatLngBounds(center, center);
    for (var i = 0; i < locations.length; i++) {
        bounds.extend(locations[i]);
    }
    return bounds;
}

function setRouteInMap(locations) {
    directionsService = new google.maps.DirectionsService();
    directionsDisplay = new google.maps.DirectionsRenderer({
        suppressMarkers: true,
        suppressInfoWindows: true
    });
    directionsDisplay.setMap(map);
    var request = {
        origin: locations[0],
        destination: locations[locations.length - 1],
        waypoints: getWayPoints(locations),
        travelMode: google.maps.DirectionsTravelMode.DRIVING
    };
    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
            distance = "La lunghezza approssimativa del viaggio è di " + response.routes[0].legs[0].distance.text;
            distance += " e il tempo stimato di percorrenza è " + response.routes[0].legs[0].duration.text;
            loader.show();
            loader.text(distance)
        }
    });

}

function getWayPoints(locations) {
    var wayPoints = new Array();
    for (var i = 1; i < locations.length - 1; i++) {
        wayPoints[i-1] = {
            location: locations[i],
            stopover: false
        };
    }
    return wayPoints;
}