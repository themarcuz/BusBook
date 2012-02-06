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