/// <reference path="../../Scripts/jquery-1.5.1-vsdoc.js" />

function beginInitialLetterFilter() {
    hideResult();
    clearSearchBox();
}

function clearSearchBox() {
    $("#q").val("");
}

function hideResult() {
    $("#elencoAgenzie").hide('fast');
}

function showResult() {
    $("#elencoAgenzie").show('fast');
    handleTilesCommands();
}