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
            console.log("updated.");
            location.reload(); 
        },
        error: function (err) {
            console.log(err);
        }
    });
}

$(document).ready(function () {
    $("#changeFormat").unbind("click");
    $("#changeFormat").bind("click", function () {
        let current = getCookie("format");
        if (current == "american") {
            document.cookie = "format=decimal";
        } else {
            document.cookie = "format=american";
        }
        updateOddsFormat();
    });

    let current = getCookie("format");

    if (current == "american") {
        let away = Number($("#awayOdds").text());
        let home = Number($("#homeOdds").text());
        let draw = Number($("#drawOdds").text());

        $("#awayOdds").text(((away - 1) * 100).toFixed(2));
        $("#homeOdds").text(((home - 1) * 100).toFixed(2));
        $("#drawOdds").text(((draw - 1) * 100).toFixed(2));
    }
});

function updateOddsFormat() {
    let current = getCookie("format");
    let away = Number($("#awayOdds").text());
    let home = Number($("#homeOdds").text());
    let draw = Number($("#drawOdds").text());

    console.log(draw);

    if (current == "american") {
        $("#awayOdds").text(((away - 1) * 100).toFixed(2));
        $("#homeOdds").text(((home - 1) * 100).toFixed(2));
        $("#drawOdds").text(((draw - 1) * 100).toFixed(2));
    } else {                                  
        $("#awayOdds").text(((away / 100) + 1).toFixed(2));
        $("#homeOdds").text(((home / 100) + 1).toFixed(2));
        $("#drawOdds").text(((draw / 100) + 1).toFixed(2));
    }
}

$("#drawOdds").bind("click", () => formify("drawOdds"));
$("#awayOdds").bind("click", () => formify("awayOdds"));
$("#homeOdds").bind("click", () => formify("homeOdds"));

function formify(id) {
    let element = $(`#${id}`);
    element.unbind("click");

    let text = element.text();

    element.text("");
    element.append($("<input>").attr("placeholder", "Edit odds...").attr("type", "number").css("width", "100px").css("display", "inherit").val(text)).on("keyup", function(event) {
        if (event.key === "Enter") {
            let val = $(`#${id} input`).val();
            console.log(val);

            let away = Number($("#awayOdds").text());
            let home = Number($("#homeOdds").text());
            let draw = Number($("#drawOdds").text());

            if (Number.isNaN(away) || away == 0) {
                away = val;
            }
            if (Number.isNaN(home) || home == 0) {
                home = val;
            }
            if (Number.isNaN(draw) || draw == 0) {
                draw = val;
            }

            console.log(away);
            console.log(home);
            console.log(draw);

            updateOdds($("#eventId").text(), home, away, draw);
        }
    });
}


