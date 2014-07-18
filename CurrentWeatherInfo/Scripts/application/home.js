(function ($) {

    var app = $.sammy('#content', function () {
        this.get('#GetWeatherDataAndParseIt/', function (context) {
            context.app.swap('');
            this.load('Weather/ByIp')
                    .then(function (weather) {
                        var jsonParsedWeather = JSON.parse(weather);

                        this.log("longtitude " + jsonParsedWeather.coord.lon);
                        context.render('content/templates/weather.txt', { coord: jsonParsedWeather.coord })
                   .appendTo(context.$element());
                    });
        });

        this.get('#GetCurrentWeather/', function (context) {
            this.load('Weather/ByIpHtml')
                    .then(function (weatherHtml) {
                        $("div#current-weather").html(weatherHtml);
                    });
        });
    });

    $(function () {
        app.run();
    });

})(jQuery);