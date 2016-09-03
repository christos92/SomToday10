using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSom.Models
{

    public class Instellingen
    {
        [JsonProperty(PropertyName = "naam")]
        public string naam { get; set; }
        [JsonProperty(PropertyName = "afkorting")]
        public string afkorting { get; set; }
        [JsonProperty(PropertyName = "brin")]
        public string brin { get; set; }
    }

    public class scholen
    {
        [JsonProperty(PropertyName = "baseURL")]
        public string baseURL { get; set; }
        [JsonProperty(PropertyName = "instellingen")]
        public List<Instellingen> instellingen { get; set; }
    }
}