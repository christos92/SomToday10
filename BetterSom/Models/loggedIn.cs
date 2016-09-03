using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSom.Models
{
    public class Leerlingen
    {
        [JsonProperty(PropertyName = "leerlingId")]
        public int leerlingId { get; set; }
        [JsonProperty(PropertyName = "fullName")]
        public string fullName { get; set; }
    }

    public class loggedIn
    {
        [JsonProperty(PropertyName = "leerlingen")]
        public IList<Leerlingen> leerlingen { get; set; }
        [JsonProperty(PropertyName = "verzorger")]
        public bool verzorger { get; set; }
        [JsonProperty(PropertyName = "error")]
        public string error { get; set; }
    }
}