let eventsCount;
let perPage = 5;
let currentPage = 0;
let search;
const maxButtons = 9;

$(window).on("load", function () {
    loadEvents(currentPage, search, perPage);
    getEventsCount(search);
    loadStatisticks();
});

function loadStatisticks() {
    $.ajax({
        url: '/Events/GetEventsPerLeagueStatistics',
        type: 'get',
        success: function (data) {
            console.log(data);
        },
        error: function (err) {
            console.log(err);
        }
    });

    $.ajax({
        url: '/Events/GetEventsPerSportStatistics',
        type: 'get',
        success: function (data) {
            console.log(data);
        },
        error: function (err) {
            console.log(err);
        }
    });
}

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

    createPageButtons();
}

function searchUpdate() {
    $("#searchButton").attr("disabled", true);
    search = $("#searchText").val();
    currentPage = 0;

    new Promise((resolve) => {
        loadEvents(currentPage, search, perPage);
        getEventsCount(search);
        resolve();
    }).then(function () {
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

function createPageButtons() {
    $("#pageNumbers").html('');
    let buttons = [];
    let pagesCount = Math.ceil(eventsCount / perPage);
    for (let i = 0; i < pagesCount; i++) {
        buttons.push($(`<button>`).text(i + 1).on("click", function () {
            currentPage = i;
            loadEvents(currentPage, search, perPage);
            checkButtons();
        }));
    }
    console.log(buttons);

    if (buttons.length <= maxButtons) {
        for (var button of buttons) {
            $("#pageNumbers").append(button);
        }
    } else {
        for (var i = 0; i < maxButtons / 3; i++) {
            $("#pageNumbers").append(buttons[i]);
        }

        $("#pageNumbers").append($('<span>').text('...'));

        let total = maxButtons / 3;
        let left = Math.floor(total / 2);
        let right = Math.floor(total / 2);

        let middleButtons = [];
        

        for (let i = currentPage - left; i <= currentPage + right; i++) {
            if (i >= maxButtons / 3 && i < pagesCount - maxButtons / 3) {
                middleButtons.push(buttons[i]);
            }
        }

        for (let button of middleButtons) {
            $("#pageNumbers").append(button);
        }
        if (middleButtons.length > 0) {
            $("#pageNumbers").append($('<span>').text('...'));
        }

        for (let i = pagesCount - maxButtons / 3; i < pagesCount; i++) {
            $("#pageNumbers").append(buttons[i]);
        }
    }
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
        let icon = getIcon(message.sportName);
        $("#messagesBody")
            .append($('<tr>')
                .append($("<td>").html(icon))
                .append($("<td>").text(message.name))
                .append($("<td>").text(message.date))
                .append($("<td>").text(message.leagueName))
                .attr('onclick', "window.location=" + `"/Events/Details/${message.id}"` + ";"));
    }
}

function getIcon(sport) {
    switch (sport) {
        case 'Soccer':
            return '<i class="far fa-futbol"></i>';
        case 'BasketBall':
            return '<i class="fas fa-basketball-ball"></i>';
        case 'Tennis':
            return '<ion-icon name="tennisball"></ion-icon>';
        case 'Baseball':
            return '<i class="fas fa-baseball-ball"></i>';
        case 'Ice Hockey':
            return '<i class="fas fa-hockey-puck"></i>';
        case 'Golf':
            return '<i class="fas fa-golf-ball"></i>';
        case 'Volleyball':
            return '<i class="fas fa-volleyball-ball"></i>';
        case 'Table Tennis':
            return '<i class="fas fa-table-tennis"></i>';
        case 'E-Sports':
            return '<i class="fas fa-gamepad"></i>';
    }
    return 'a';
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
