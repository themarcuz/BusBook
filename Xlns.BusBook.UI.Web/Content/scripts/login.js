/// <reference path="../../Scripts/jquery-1.5.1-vsdoc.js" />

function startOperation() {
    hideUserInfoBox();
}

function completeOperation() {
    showUserInfoBox();
}

function failure() {
    alert('Errore nella procedura di Login/Logout');
    //showLoginBox();
}

function hideUserInfoBox() {
    $("#userInfoBox").hide('fast');
}

function showUserInfoBox() {
    $("#userInfoBox").show('fast');
}