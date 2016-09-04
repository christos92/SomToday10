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
         

            TimeSpan start = new TimeSpan(6, 0, 0); //6 o'clock
            TimeSpan end = new TimeSpan(12, 0, 0); //12 o'clock
            TimeSpan now = DateTime.Now.TimeOfDay;

            if ((now > start) && (now < end))
            {
                profiel.Tijd = "Goedenmorgen,";
            }

            TimeSpan start1 = new TimeSpan(12, 0, 0); //12 o'clock
            TimeSpan end1 = new TimeSpan(24, 0, 0); //24 o'clock
            TimeSpan now1 = DateTime.Now.TimeOfDay;

            if ((now1 > start1) && (now1 < end1))
            {
                profiel.Tijd = "Goedenmiddag,";
            }

            TimeSpan start2 = new TimeSpan(12, 0, 0); //12 o'clock
            TimeSpan end2 = new TimeSpan(24, 0, 0); //24 o'clock
            TimeSpan now2 = DateTime.Now.TimeOfDay;

            if ((now2 > start2) && (now2 < end2))
            {
                profiel.Tijd = "Goedenavond,";
            }
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
