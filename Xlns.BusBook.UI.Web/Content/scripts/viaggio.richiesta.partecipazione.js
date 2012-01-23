/// <reference path="../../Scripts/jquery-1.5.1-vsdoc.js" />

$("#dialogPartecipazione").dialog({
    autoOpen: true,
    height: 300,
    width: 350,
    modal: true,
    buttons: {
        "Conferma": function () {
            var bValid = true;
//            allFields.removeClass("ui-state-error");

            if (bValid) {
//                $("#users tbody").append("<tr>" +
//							"<td>" + name.val() + "</td>" +
//							"<td>" + email.val() + "</td>" +
//							"<td>" + password.val() + "</td>" +
//						"</tr>");
                $(this).dialog("close");
            }
        },
        "Annulla": function () {
            $(this).dialog("close");
        }
    },
    close: function () {
//        allFields.val("").removeClass("ui-state-error");
    }
});