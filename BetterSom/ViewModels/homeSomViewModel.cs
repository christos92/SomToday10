using BetterSom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.Storage;
using Windows.UI.Xaml.Navigation;

namespace BetterSom.ViewModels
{
    public class homeSomViewModel : ViewModelBase
    {

        public homeSomViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Naam = "Piet Smit";
            }
            UpdateCurrentData();
            var localData = ApplicationData.Current.LocalSettings;
            schoolname = localData.Values["sc"].ToString();
            baseurl =  "https://somtoday.nl/" + schoolname + "/services/mobile/v10/";

            gebNaam = localData.Values["lNaam"].ToString();
            password = localData.Values["pass"].ToString();
            brin = localData.Values["brin"].ToString();
            Naam = localData.Values["naam"].ToString();
        }
      
      
        #region Lifecycle Handlers

        /// <summary>
        ///
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="suspensionState"></param>
        /// <returns></returns>
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode,
            IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
            
            }
            else
            {     
                UpdateCurrentData();
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// Save state before navigating
        /// </summary>
        /// <param name="suspensionState"></param>
        /// <param name="suspending"></param>
        /// <returns></returns>
        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {           
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        #endregion
        #region Bindable Game Vars
        public string gebNaam { get; set; }
        public string password { get; set; }
        public string brin { get; set; }
        public string Naam { get; set; }

        private Profiel leerlingProfiel { get; set; }
        /// <summary>
        /// Data for the current user
        /// </summary>
        public Profiel LeerlingProf
        {
            get { return leerlingProfiel; }
            set { leerlingProfiel = value; }
        }
   

        #endregion

        #region Game Logic

        #region Shared Logic

        /// <summary>
        /// Updates data related to current user
        /// </summary>
        private async void UpdateCurrentData()
        {
            // Retrieve data
            LeerlingProf = await KrijgProfiel();  
        }
        string schoolname = "";
        string baseurl;
        public async Task<Profiel> KrijgProfiel()
        {
            var localData = ApplicationData.Current.LocalSettings;
            Profiel profiel = new Profiel();
            profiel.Naam = localData.Values["naam"].ToString();
            profiel.SchoolNaam = localData.Values["sc"].ToString();
            profiel.Id = localData.Values["iD"].ToString();
            string gradesurl;
            gradesurl = baseurl + "Cijfers/GetMultiCijfersRecentB64/" + gebNaam + "/" + password + "/" +
                    brin + "/" + profiel.Id;
            Naam = profiel.Naam;
            return profiel;
        }

        #endregion
        #endregion
    }
}