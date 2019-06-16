let eventsCount;
let perPage = 5;
let currentPage = 0;
let search;

$(window).on("load", function () {
    loadEvents(currentPage, search, perPage);
    getEventsCount(search);
});

function checkButtons() {
    if (currentPage == 0) {
        $("#prevButton").attr("disabled", "disabled");
    } else {
        $("#prevButton").removeAttr("disabled");
    }

    if (currentPage + 1 == Math.ceil(eventsCount / perPage)) {
        $("#nextButton").attr("disabled", "disabled");
    } else {
        $("#nextButton").removeAttr("disabled");
    }
}

function searchUpdate() {
    $("#searchButton").attr("disabled", true);
    search = $("#searchText").val();
    currentPage = 0;

    new Promise((resolve) => {
        loadEvents(currentPage, search, perPage);
        getEventsCount(search);
        resolve();
    }).then(function() {
        $("#searchButton").removeAttr("disabled");
    });
}

function getEventsCount(search) {
    $.ajax({
        url: '/Events/GetEventsCount' + getUrlAttributes(null, search, null),
        type: 'get',
        success: function (count) {
            console.log(count);
            eventsCount = count;
            $("#pageCount").text(`Page ${currentPage + 1}/${Math.ceil(count / perPage)}`);
            $("#nextButton").removeAttr("disabled");
            $("#nextButton").unbind("click");
            $("#nextButton").bind("click", function (e) {
                e.preventDefault();
                console.log("next");
                currentPage++;
                loadEvents(currentPage, search, perPage);
                $("#pageCount").text(`Page ${currentPage + 1}/${Math.ceil(count / perPage)}`);
                checkButtons();
            });
            $("#prevButton").unbind("click");
            $("#prevButton").removeAttr("disabled");
            $("#prevButton").on("click", function () {
                currentPage--;
                loadEvents(currentPage, search, perPage);
                $("#pageCount").text(`Page ${currentPage + 1}/${Math.ceil(count / perPage)}`);
                checkButtons();                
            });
            checkButtons();
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function getUrlAttributes(page, search, perPage) {
    url = "";

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

    return url;
}

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
    url += getUrlAttributes(page, search, perPage);

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
