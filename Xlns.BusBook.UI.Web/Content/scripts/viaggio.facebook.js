function graphStreamPublish() {
    var actionUrl = window.location;
    var postText = 'BB new trip: ' + actionUrl;
    FB.api('/me/feed', 'post', { message: postText }, function (response) {
        if (!response || response.error) { alert('Si è verificato un errore'); }
        else { alert('Post ID: ' + response.id); }
    });
}

function share() {
    var share = { method: 'stream.share', u: 'http://xlns.it/' };
    FB.ui(share, function (response) { console.log(response); });
}

function showStream() {
    FB.api('/me', function (response) {
        streamPublish(response.name, 'XLNS your way to be xcellent', 'xlsn', 'http://xlns.it', "visit xlns");
    });
}

function streamPublish(name, description, hrefTitle, hrefLink, userPrompt) {
    FB.ui({ method: 'stream.publish', message: '', attachment: { name: name, caption: '', description: (description), href: hrefLink }, action_links: [{ text: hrefTitle, href: hrefLink}], user_prompt_message: userPrompt }, function (response) { });
}