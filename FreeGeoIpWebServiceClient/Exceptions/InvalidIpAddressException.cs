using System;
using System.Linq;

namespace FreeGeoIpWebServiceClient
{
    [Serializable]
    public class InvalidIpAddressException : Exception
    {
        public InvalidIpAddressException() { }
        public InvalidIpAddressException(string message) : base(message) { }
        public InvalidIpAddressException(string message, Exception inner) : base(message, inner) { }
        protected InvalidIpAddressException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
