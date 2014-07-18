using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenWeatherMapWebServiceClient
{
    /// <summary>
    /// This is Service Client class for the web service Open Weather Map api
    /// </summary>
    public class OWMServiceClient
    {
        private const string ServiceUrl = "http://api.openweathermap.org/data/2.5/weather";
        private const string DefaultSearchQuery = "?units=metric&lang=en";

        private ICriteria searchCriteria;
        private string serviceUrlWithQuery;

        private ResultType resultType = ResultType.Json;

        public OWMServiceClient()
        {
            refreshQueryString();
        }

        /// <summary>
        /// This method provide a different criteria support for the web service
        /// Example: You can search for the current weather by providing langtitude and longtitude, or City Name
        /// </summary>
        /// <param name="searchCriteria">Use SearchByCity or SearchByGeolocation</param>
        public void SetSearchCriteriaStrategy(ICriteria searchCriteria)
        {
            this.searchCriteria = searchCriteria;
        }

        /// <param name="resultType">Defines the type of the returned result</param>
        /// <param name="cancelToken">Timeout</param>
        /// <returns>Returns Raw string with the defined resultType</returns>
        public async Task<string> GetWeatherAsync(ResultType resultType = ResultType.Json,
      CancellationToken cancelToken = default(CancellationToken))
        {
            this.resultType = resultType;
            refreshQueryString();
            finalizeTheServiceQuery();
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(this.serviceUrlWithQuery, cancelToken);
                return (await response.Content.ReadAsStringAsync());
            }

        }

        private void finalizeTheServiceQuery()
        {
            if (this.searchCriteria == null)
            {
                throw new SearchStrategyNotAssignedException("You must call SetSearchCriteriaStrategy" +
                    "(ICriteria searchCriteria) method to assign a search strategy before you attempt " +
                    "to finalizeTheServiceQuey()!");
            }

            StringBuilder stringBuilder = new StringBuilder(this.serviceUrlWithQuery);
            stringBuilder.Append("&");
            stringBuilder.Append(getResultTypeQuery());
            stringBuilder.Append("&");
            stringBuilder.Append(searchCriteria.GetQuery());

            this.serviceUrlWithQuery = stringBuilder.ToString();
        }

        private string getResultTypeQuery()
        {
            string selectedType = System.Enum.GetName(typeof(ResultType), this.resultType);
            string selectedTypeLowerCase = selectedType.ToLowerInvariant();
            string query = String.Format("mode={0}", selectedTypeLowerCase);
            return query;
        }

        private void refreshQueryString()
        {
            this.serviceUrlWithQuery = ServiceUrl + DefaultSearchQuery;
        }
    }
}