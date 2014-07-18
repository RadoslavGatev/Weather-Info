using System;
using System.Linq;

namespace OpenWeatherMapWebServiceClient
{
    public interface ICriteria
    {
        string GetQuery();
    }
}
