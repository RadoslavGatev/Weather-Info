﻿using System;
using System.Linq;

namespace FreeGeoIpWebServiceClient
{
    public class LocationInfo
    {
        public string ip { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string region_code { get; set; }
        public string region_name { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string metro_code { get; set; }
        public string area_code { get; set; }
    }
}
