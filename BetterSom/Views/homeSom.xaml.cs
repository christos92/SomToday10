
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BetterSom.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class homeSom : Page
    {
        public homeSom()
        {
            this.InitializeComponent();
            Loaded += HomeSom_Loaded;
        }
        string username;
        string password;
        public async Task LoadImage(Uri uri)
        {
            BitmapImage bitmapImage = new BitmapImage();

           
               var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("JSESSIONID", JIDD);
                    var data = await httpClient.GetByteArrayAsync(uri);
                  
                    var file = await KnownFolders.PicturesLibrary.CreateFileAsync("myfile.jpg", CreationCollisionOption.ReplaceExisting);
            var stream = await file.OpenAsync(FileAccessMode.ReadWrite);

            using (var outputStream = stream.GetOutputStreamAt(0))
            {
                DataWriter writer = new DataWriter(outputStream);
                writer.WriteBytes(data);
                await writer.StoreAsync();
               await outputStream.FlushAsync();
            }


        }
        string afkorting;
        public string JIDD = "";
        private void HomeSom_Loaded(object sender, RoutedEventArgs e)
        {
            var localData = ApplicationData.Current.LocalSettings;
            afkorting = localData.Values["afkorting"].ToString();
            username = localData.Values["echteNaam"].ToString();
            password = localData.Values["echteWachtwoord"].ToString();
            webView.Source = new Uri("https://"+afkorting+"-elo.somtoday.nl/");
            webView.NavigationCompleted += WebView_NavigationCompleted;
        }
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
        int count = 0;
        private async void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            try
            {      
                if (count == 0)
                {
                    await webView.InvokeScriptAsync("eval", new string[] { "document.getElementsByName('usernameFieldPanel:usernameFieldPanel_body:usernameField')[0].value='" + username+"';" });
                    await webView.InvokeScriptAsync("eval", new string[] { "document.getElementsByName('passwordFieldPanel:passwordFieldPanel_body:passwordField')[0].value='"+password+"';" });
                    await webView.InvokeScriptAsync("eval", new string[] { "document.getElementsByTagName('a')[0].click();" });
                    count++;
                }
            }
            catch (Exception e1)
            {
                MessageDialog mes = new MessageDialog(e1.Message);
                await mes.ShowAsync();
            }
        }
    }
}
