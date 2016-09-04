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

    public class ProfielViewModel : NotificationBase<Profiel>
    {
        public ProfielViewModel(Profiel person = null) : base(person) { }

        public string Id
        {
            get { return This.Id; }
            set { SetProperty(This.Id, value, () => This.Id = value); }
        }

        public string Naam
        {
            get { return This.Naam; }
            set { SetProperty(This.Naam, value, () => This.Naam = value); }
        }
        public string Tijd
        {
            get { return This.Tijd; }
            set { SetProperty(This.Tijd, value, () => This.Tijd = value); }
        }
        public string SchoolNaam
        {
            get { return This.SchoolNaam; }
            set { SetProperty(This.SchoolNaam, value, () => This.SchoolNaam = value); }
        }
        public ObservableCollection<Cijfer> Cijfers
        {
            get { return This.Cijfers; }
            set { SetProperty(This.Cijfers, value, () => This.Cijfers = value); }
        }
        public ObservableCollection<Huiswerk> huisWerk
        {
            get { return This.huisWerk; }
            set { SetProperty(This.huisWerk, value, () => This.huisWerk = value); }
        } 

       
      
    }
}
