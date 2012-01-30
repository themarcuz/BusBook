/// <reference path="../../Scripts/jquery-1.5.1-vsdoc.js" />

function beginInitialLetterFilter() {
    hideResult();
    clearSearchBox();
}

function clearSearchBox() {
    $("#q").val("");
}

function hideResult() {
    $("#elencoUtenti").hide('fast');
}

function showResult() {
    $("#elencoUtenti").show('fast');
    handleTilesCommands();
}