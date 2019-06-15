$(window).on("load", function () {
    loadEvents();
});

function appendEvents(messages) {
    $("#messagesBody").html("");
    for (let message of messages) {
        $("#messagesBody")
            .append($('<tr>')
                .append($("<td>").text(message.name))
                .append($("<td>").text(message.sportName))
                .append($("<td>").text(message.leagueName)));
    }
}

function loadEvents(page, search, perPage) {
    url = '/Events/GetEvents';
    attrs = [page, search, perPage].filter(a => a);
    if (attrs.length > 0) {
        url += '?';
        if (page) {
            url += 'page=' + page + '&';
        }
        if (search !== undefined) {
            url += 'search=' + search + '&';
        }
        if (perPage !== undefined) {
            url += 'eventsPerPage=' + perPage + '&';
        }
    }

    console.log(url);

    $.ajax({
        url: url,
        type: 'get',
        success: function (data) {
            appendEvents(data);
        },
        error: function (err) {
            console.log(err);
        }
    });
}
