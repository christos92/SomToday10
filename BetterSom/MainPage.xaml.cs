using BetterSom.Models;
using BetterSom.Views;
using Newtonsoft.Json;
using PCLCrypto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BetterSom
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private HttpClient httpClient;
        public MainPage()
        {
            this.InitializeComponent();
            httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 9556000;
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
            Loaded += MainPage_Loaded;
        }
        private ObservableCollection<SchoolModel> schoolList = new ObservableCollection<SchoolModel>();
        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            cob.IsEnabled = false;
            try
            {
                var localData = ApplicationData.Current.LocalSettings;
                if ((string)localData.Values["autoLogin"] == "true")
                {
                    loginAuto();
                }

                HttpResponseMessage response = await httpClient.GetAsync("http://servers.somtoday.nl/");
                string array = await response.Content.ReadAsStringAsync();

                var school = JsonConvert.DeserializeObject<List<scholen>>(array);
                foreach (var scho1 in school)
                {
                    foreach (var scho in scho1.instellingen)
                    { 
                        var schoolModel = new SchoolModel();
                        schoolModel.afkorting = scho.afkorting;
                        schoolModel.brin = scho.brin;
                        schoolModel.naam = scho.naam;
                        schoolList.Add(schoolModel);

                    }

                }

            }
            catch (Exception e1)
            {

            }
            cob.DataContext = schoolList;
            cob.IsEnabled = true;
        }

        string username;
        string password;
        string schoolName;
        string leerlingID;
        string brin;
        private string ToHex(string inputstring)
        {
            string output = "";
            string hexChars = "0123456789abcdef";
            foreach (char hexChar in inputstring)
            {
                int asciiValue = (int)hexChar;
                int index1 = asciiValue % 16;
                int index2 = (asciiValue - index1) / 16;
                output += hexChars[index2];
                output += hexChars[index1];
            }
            return output;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        private async void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
           await login();
        }
        private async Task login()
        {
            var cobSec = cob.SelectedItem as SchoolModel;
            brin = cobSec.brin;
            schoolName = cobSec.afkorting;

            var sha1 = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha1);
            byte[] passwordArray = Encoding.UTF8.GetBytes(ww.Password);
            byte[] hash = sha1.HashData(passwordArray);
            string hashedAndBase64 = Convert.ToBase64String(hash);
            string output = ToHex(hashedAndBase64);
            password = output;
            username = Base64Encode(user.Text);

            HttpResponseMessage response = await httpClient.GetAsync("http://somtoday.nl/" + schoolName + "/services/mobile/v10/Login/CheckMultiLoginB64/" +
            username + "/" + password + "/" + brin);
            string array = await response.Content.ReadAsStringAsync();
            var school = JsonConvert.DeserializeObject<loggedIn>(array);

            if ((string)school.error == "SUCCESS")
            {
                var localData = ApplicationData.Current.LocalSettings;
                foreach (var na in school.leerlingen)
                {


                    localData.Values["naam"] = na.fullName;
                    localData.Values["lNaam"] = username;
                    localData.Values["iD"] = na.leerlingId;
                    localData.Values["pass"] = password;
                    localData.Values["brin"] = brin;
                    localData.Values["sc"] = schoolName;
                    if (remember.IsChecked == true)
                    {

                        localData.Values["autoLogin"] = "true";
                    }
                    else
                    {
                        localData.Values["autoLogin"] = "false";

                    }
                    this.Frame.Navigate(typeof(homeSom));
                }
            }
            else
            {

            }

        }
        async void loginAuto()
        {
            var localData = ApplicationData.Current.LocalSettings;

            username = localData.Values["lNaam"].ToString();

            password = localData.Values["pass"].ToString();
            brin = localData.Values["brin"].ToString();
            schoolName = localData.Values["sc"].ToString();


            HttpResponseMessage response = await httpClient.GetAsync("http://somtoday.nl/" + schoolName + "/services/mobile/v10/Login/CheckMultiLoginB64/" +
            username + "/" + password + "/" + brin);
            string array = await response.Content.ReadAsStringAsync();
            var school = JsonConvert.DeserializeObject<loggedIn>(array);

            if ((string)school.error == "SUCCESS")
            {

                foreach (var na in school.leerlingen)
                {

                    localData.Values["naam"] = na.fullName;
                    localData.Values["iD"] = na.leerlingId;

                    this.Frame.Navigate(typeof(homeSom));
                }
            }
            else
            {

            }

        }

        private void HyperlinkButton_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
    public class SchoolModel
    {
        public string naam { get; set; }
        public string afkorting { get; set; }
        public string brin { get; set; }
    }
}
