using BetterSom.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BetterSom.ViewModels
{
    public class homeSomViewModel
    {   private ObservableCollection<Cijfer> col = new ObservableCollection<Cijfer>();
        private ObservableCollection<Huiswerk> hw = new ObservableCollection<Huiswerk>();
       
        public homeSomViewModel()
        {
            var localData = ApplicationData.Current.LocalSettings;
            Profiel profiel = new Profiel();
            profiel.Naam = localData.Values["naam"].ToString();
            profiel.SchoolNaam = localData.Values["sc"].ToString();
            profiel.Id = localData.Values["iD"].ToString();
            profiel.Tijd = "Goedenmorgen,";
            profiel.huisWerk = hw;
            profiel.Cijfers = col;
            LeerlingProf = profiel;
        }
        private Profiel leerlingProfiel;
        public Profiel LeerlingProf
        {
            get { return leerlingProfiel; }
            set
            {
                leerlingProfiel = value;
            }
        }
    }
}
