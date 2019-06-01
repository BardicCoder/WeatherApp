using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ThirdPartyApiCaller.Models
{
    [DataContract]
    public class GeolocationModel
    {
        [DataMember(Name = "query")]
        public string IP { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "zip")]
        public string ZipCode { get; set; }

        [DataMember(Name = "lat")]
        public string Latitude { get; set; }

        [DataMember(Name = "lon")]
        public string Longitude { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "regionName")]
        public string RegionName { get; set; }
    }
}
