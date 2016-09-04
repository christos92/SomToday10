using BetterSom.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSom.Models
{
    public class Profiel
    {
        public string Id { get; set; }
        public string Naam { get; set; }
        public string Tijd { get; set; }
        public string SchoolNaam { get; set; }
        public ObservableCollection<Cijfer> Cijfers = new ObservableCollection<Cijfer>();
        public ObservableCollection<Huiswerk> huisWerk = new ObservableCollection<Huiswerk>();
    }

}
