using System;
using System.Linq;
using System.Net;

namespace FreeGeoIpWebServiceClient
{
    public static class IpUtility
    {
        public static string GetIpV4Addres(string ipAddress)
        {
            try
            {
                IPAddress ipAddressResult;
                ipAddressResult = IPAddress.Parse(ipAddress);
                return ipAddressResult.MapToIPv4().ToString();
            }
            catch (FormatException fe)
            {
                throw new InvalidIpAddressException("The provided IP Address is not valid", fe);
            }
        }
    }
}
