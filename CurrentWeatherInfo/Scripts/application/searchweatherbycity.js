$(document).ready(function () {

    $("a#weather-by-town-button").click(function () {
        var cityName = $("input#town-name").val();
        if (cityName != "") {
            $.ajax({
                url: '/Weather/ByCityHtml/?cityName=' + cityName,
            }).done(function (weatherHtml) {
                $("div#current-weather").html(weatherHtml);
            });
        }
    });
});

