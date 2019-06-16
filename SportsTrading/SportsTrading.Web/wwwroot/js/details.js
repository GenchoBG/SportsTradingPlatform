function addGraph(chartNumber, homeTeamOdds, awayTeamOdds, drawOdds) {
    new Chartist.Bar(`.ct-chart${chartNumber}`, {
        labels: ['Home Team Odds', 'Away Team Odds', 'Draw Odds'],
        series: [
            [homeTeamOdds, awayTeamOdds, drawOdds]
        ]
    }/*, {
            axisY: {
                labelInterpolationFnc: function (value) {
                    return value + '%'
                }
            }
        }*/);
}
