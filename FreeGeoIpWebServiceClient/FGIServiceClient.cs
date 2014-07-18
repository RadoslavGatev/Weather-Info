using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FreeGeoIpWebServiceClient
{
    /// <summary>
    /// This is Service Client class for the web service freegoip.net
    /// </summary>
    public class FGIServiceClient
    {
        /// <summary>
        /// http://freegeoip.net/{format}/{ip_or_hostname}
        /// </summary>
        private const string ServiceUrlTemplate = "http://freegeoip.net/{0}/{1}";

        private string serviceUrlWithQuery;

        private ResultType resultType = ResultType.Json;

        /// <summary>
        /// Returns LocationInfo object with geolocation information based on the IP Address
        /// </summary>
        /// <param name="ipAddress">Ip address</param>
        /// <param name="resultType">Defines the type of the returned result</param>
        /// <param name="cancelToken">Timeout</param>
        public async Task<LocationInfo> GetGeoIpInfoAsync(string ipAddress, ResultType resultType = ResultType.Json,
      CancellationToken cancelToken = default(CancellationToken))
        {
            if (ipAddress == null)
            {
                throw new ArgumentNullException("ipAddress", "The parameter must not be null!");
            }
            string ipV4Version;
            ipV4Version = IpUtility.GetIpV4Addres(ipAddress);

            this.resultType = resultType;
            makeQueryString(ipV4Version);

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(this.serviceUrlWithQuery, cancelToken);
                return (await response.Content.ReadAsAsync<LocationInfo>());
            }
        }

        /// <summary>
        /// Returns Raw string with geolocation information based on the IP Address
        /// </summary>
        /// <param name="ipAddress">Ip address</param>
        /// <param name="resultType">Defines the type of the returned result</param>
        /// <param name="cancelToken">Timeout</param>
        public async Task<string> GetGeoIpInfoRawAsync(string ipAddress, ResultType resultType = ResultType.Json,
CancellationToken cancelToken = default(CancellationToken))
        {
            if (ipAddress == null)
            {
                throw new ArgumentNullException("ipAddress", "The parameter must not be null!");
            }

            string ipV4Version;
            ipV4Version = IpUtility.GetIpV4Addres(ipAddress);

            this.resultType = resultType;
            makeQueryString(ipV4Version);

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(this.serviceUrlWithQuery, cancelToken);
                return (await response.Content.ReadAsStringAsync());
            }
        }

        private void makeQueryString(string ipAddress)
        {
            this.serviceUrlWithQuery = String.Format(ServiceUrlTemplate.ToString(), getResultTypeName(), ipAddress);
        }

        private string getResultTypeName()
        {
            string selectedType = System.Enum.GetName(typeof(ResultType), this.resultType);
            string selectedTypeLowerCase = selectedType.ToLowerInvariant();
            return selectedTypeLowerCase;
        }
    }
}