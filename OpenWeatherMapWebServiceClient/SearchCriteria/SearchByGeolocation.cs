using System;
using System.Linq;

namespace OpenWeatherMapWebServiceClient
{
    public class SearchByGeolocation : ICriteria
    {
        private string latitude;
        private string longtitude;

        public SearchByGeolocation(string latitude, string longtitude)
        {
            if (latitude == null)
            {
                throw new ArgumentNullException("latitude");
            }

            if (longtitude == null)
            {
                throw new ArgumentNullException("longtitude");
            }

            this.latitude = latitude;
            this.longtitude = longtitude;
        }

        public string GetQuery()
        {
            string query = String.Format("lat={0}&lon={1}", this.latitude, this.longtitude);
            return query;
        }
    }
}