using System;
using System.Linq;

namespace OpenWeatherMapWebServiceClient
{
    [Serializable]
    public class SearchStrategyNotAssignedException : Exception
    {
        public SearchStrategyNotAssignedException() { }
        public SearchStrategyNotAssignedException(string message) : base(message) { }
        public SearchStrategyNotAssignedException(string message, Exception inner) : base(message, inner) { }
        protected SearchStrategyNotAssignedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}