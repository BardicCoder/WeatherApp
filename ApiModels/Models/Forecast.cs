using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ThirdPartyApiCaller.Models
{
    [DataContract]
    public class Forecast
    {
        [DataMember(Name = "Days")]
        public List<Day> Days { get; set; }
    }
}
