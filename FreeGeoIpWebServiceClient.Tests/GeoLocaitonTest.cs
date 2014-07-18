using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FreeGeoIpWebServiceClient;
using System.Threading.Tasks;

namespace FreeGeoIpWebServiceClient.Tests
{
    [TestClass]
    public class GeoLocaitonTest
    {
        private FGIServiceClient serviceClient;
        private GeoLocaitonTest()
        {
            serviceClient = new FGIServiceClient();
        }

        [TestMethod]
        public async void TestForIp()
        {
            string ipAddress = "88.87.19.21";
            var geoIpTask = serviceClient.GetGeoIpInfoAsync(
                ipAddress, FreeGeoIpWebServiceClient.ResultType.Json);

            await Task.WhenAll(geoIpTask);

            string returnedIpAddres = geoIpTask.Result.ip;
            Assert.AreEqual(ipAddress, returnedIpAddres);
        }
    }
}
