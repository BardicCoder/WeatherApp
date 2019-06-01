using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ThirdPartyApiCaller.Models
{
    [DataContract]
    public class Day
    {
        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "temp_max_f")]
        public string MaxTemp { get; set; }

        [DataMember(Name = "temp_min_f")]
        public string MinTemp { get; set; }

        [DataMember(Name = "prob_precip_pct")]
        public string PrecipChance { get; set; }
    }
}
