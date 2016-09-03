using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSom.Models
{
    public class Profiel
    {
        public string Id { get; set; }
        public string Naam { get; set; }
        public string SchoolNaam { get; set; }
        public List<Cijfer> Cijfers = new List<Cijfer>();
        public List<Huiswerk> huisWerk = new List<Huiswerk>();
    }
}
