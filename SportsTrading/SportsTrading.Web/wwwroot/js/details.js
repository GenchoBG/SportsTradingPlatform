function addGraph(chartNumber, homeTeamOdds, drawOdds, awayTeamOdds) {
    new Chartist.Bar(`.ct-chart${chartNumber}`, {
        labels: ['Home Team Odds', 'Draw Odds', 'Away Team Odds'],
        series: [
            [homeTeamOdds, drawOdds, awayTeamOdds]
        ]
    }/*, {
            axisY: {
                labelInterpolationFnc: function (value) {
                    return value + '%'
                }
            }
        }*/);
}
