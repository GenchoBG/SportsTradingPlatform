function updateOdds(id, home, away, draw) {
    console.log(JSON.stringify({
        home: home,
        away: away,
        draw: draw
    }));

    $.ajax({
        url: `/Events/UpdateOdds/${id}`,
        type: 'put',
        contentType: 'application/json',
        data: JSON.stringify({
            home: home,
            away: away,
            draw: draw
        }),
        success: function (data) {
            console.log(data);
            console.log("updated."); // TODO: popup & fadeout
        },
        error: function (err) {
            console.log(err);
        }
    });
}