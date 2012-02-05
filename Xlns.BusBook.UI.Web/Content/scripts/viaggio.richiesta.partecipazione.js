/// <reference path="../../Scripts/jquery-1.5.1-vsdoc.js" />

$("#dialogPartecipazione").dialog({
			height: 540,
			width: 1000,
			modal: true,
			buttons: {
				"Chiudi": function() {
					$( this ).dialog( "close" );
				}
			}
		});


