using System;
using System.Linq;

namespace OpenWeatherMapWebServiceClient
{
    public class SearchByCity : ICriteria
    {
        private string cityName;

        public SearchByCity(string cityName)
        {
            if (cityName == null)
            {
                throw new ArgumentNullException("cityName");
            }
            this.cityName = cityName;
        }

        public string GetQuery()
        {
            string query = "q=" + this.cityName;
            return query;
        }
    }
}