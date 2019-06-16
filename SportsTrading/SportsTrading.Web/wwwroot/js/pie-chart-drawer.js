google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawSportsChart);
google.charts.setOnLoadCallback(drawLeaguesChart);

function drawSportsChart(rawData) {
    var chart = new google.visualization.PieChart(document.getElementById('sports-piechart'));
   
    var options = {
        title: 'Sports'
    };

    var arr = [
        ['Sport', 'EventsCount']
    ];

    for (var key in rawData) {
        arr.push([key, rawData[key]]);
    }

    var data = google.visualization.arrayToDataTable(arr);

    chart.draw(data, options);
}

function drawLeaguesChart(rawData) {
    var chart = new google.visualization.PieChart(document.getElementById('leagues-piechart'));
    
    var options = {
        title: 'Leagues'
    };

    var arr = [
        ['League', 'EventsCount']
    ];

    for (var key in rawData) {
        arr.push([key, rawData[key]]);
    }

    var data = google.visualization.arrayToDataTable(arr);

    chart.draw(data, options);
}

$(window).on("load", function () {
    $.ajax({
        url: '/Events/GetEventsPerLeagueStatistics',
        type: 'get',
        success: function (data) {
            drawLeaguesChart(data);
        },
        error: function (err) {
            console.log(err);
        }
    });

    $.ajax({
        url: '/Events/GetEventsPerSportStatistics',
        type: 'get',
        success: function (data) {
            drawSportsChart(data);
        },
        error: function (err) {
            console.log(err);
        }
    });
});
