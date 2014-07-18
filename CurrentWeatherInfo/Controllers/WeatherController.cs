using System;
using System.Linq;
using System.Web.Mvc;
using FreeGeoIpWebServiceClient;
using OpenWeatherMapWebServiceClient;
using System.Threading;
using System.Threading.Tasks;

namespace CurrentWeatherInfo.Controllers
{
    public class WeatherController : Controller
    {

        //
        // GET: /GetWeather/
        [AsyncTimeout(500)]
        [HandleError(ExceptionType = typeof(TimeoutException), View = "TimeoutError")]
        public async Task<ActionResult> GetWeather(CancellationToken cancellationToken)
        {
            var serviceClient = new OWMServiceClient();
            serviceClient.SetSearchCriteriaStrategy(new SearchByCity("Veliko Turnovo, bg"));
            var task = serviceClient.GetWeatherAsync(
                OpenWeatherMapWebServiceClient.ResultType.Json, cancellationToken);
            await Task.WhenAll(task);

            return getJsonConentResult(task.Result.ToString());
        }

        //
        // GET: /ByIp/
        [AsyncTimeout(1500)]
        [HandleError(ExceptionType = typeof(TimeoutException), View = "TimeoutError")]
        public async Task<ActionResult> ByIp(CancellationToken cancellationToken)
        {
            var ipServiceClient = new FGIServiceClient();
            string ipAddress = getClientIp();
            var ipAddressTask = ipServiceClient.GetGeoIpInfoAsync(
                ipAddress, FreeGeoIpWebServiceClient.ResultType.Json,
                cancellationToken);
            await Task.WhenAll(ipAddressTask);

            var weatherServiceClient = new OWMServiceClient();
            weatherServiceClient.SetSearchCriteriaStrategy(
                new SearchByGeolocation(
                    ipAddressTask.Result.latitude,
                    ipAddressTask.Result.longitude
                    ));

            var weatherTask = weatherServiceClient.GetWeatherAsync(OpenWeatherMapWebServiceClient.ResultType.Json,
                cancellationToken);

            await Task.WhenAll(weatherTask);

            return getJsonConentResult(weatherTask.Result.ToString());
        }

        //
        // GET: /ByIpHtml/
        [AsyncTimeout(5000)]
        [HandleError(ExceptionType = typeof(TimeoutException), View = "TimeoutError")]
        public async Task<ActionResult> ByIpHtml(CancellationToken cancellationToken)
        {
            var ipServiceClient = new FGIServiceClient();
            string ipAddress = getClientIp();
            var ipAddressTask = ipServiceClient.GetGeoIpInfoAsync(
                ipAddress, FreeGeoIpWebServiceClient.ResultType.Json,
                cancellationToken);
            await Task.WhenAll(ipAddressTask);

            var weatherServiceClient = new OWMServiceClient();
            weatherServiceClient.SetSearchCriteriaStrategy(
                new SearchByGeolocation(
                    ipAddressTask.Result.latitude,
                    ipAddressTask.Result.longitude
                    ));

            var weatherTask = weatherServiceClient.GetWeatherAsync(OpenWeatherMapWebServiceClient.ResultType.Html,
                cancellationToken);

            await Task.WhenAll(weatherTask);

            return Content(weatherTask.Result.ToString());
        }

        //
        // GET: /ByCityHtml/
        [AsyncTimeout(1500)]
        [HandleError(ExceptionType = typeof(TimeoutException), View = "TimeoutError")]
        [HttpGet]
        public async Task<ActionResult> ByCityHtml(string cityName, CancellationToken cancellationToken)
        {
            var weatherServiceClient = new OWMServiceClient();
            weatherServiceClient.SetSearchCriteriaStrategy(
                new SearchByCity(cityName));

            var weatherTask = weatherServiceClient.GetWeatherAsync(OpenWeatherMapWebServiceClient.ResultType.Html,
                cancellationToken);

            await Task.WhenAll(weatherTask);

            return Content(weatherTask.Result.ToString());
        }

        [NonAction]
        private string getClientIp()
        {
            return "88.87.29.81";
            //TODO Review this in production?
            string stringIpAddress;
            stringIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (stringIpAddress == null)
            {
                stringIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                return null;
            }

            return stringIpAddress;
        }

        [NonAction]
        private ContentResult getJsonConentResult(string json)
        {
            return Content(json, "application/json");
        }

    }
}
