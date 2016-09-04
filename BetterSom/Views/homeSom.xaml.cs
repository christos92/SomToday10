
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
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
using Windows.UI.Xaml.Shapes;

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
      
        private async void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            try
            {
                if (webView.Source.ToString().Contains("pasfoto"))
                {

  

                    RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
                    await renderTargetBitmap.RenderAsync(webView);
                    var file = await KnownFolders.PicturesLibrary.CreateFileAsync("profilepicture.png", CreationCollisionOption.ReplaceExisting);
                    if (file != null)
                    {
                        var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

                        using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                            encoder.SetPixelData(
                                BitmapPixelFormat.Bgra8,
                                BitmapAlphaMode.Ignore,
                                (uint)renderTargetBitmap.PixelWidth,
                                (uint)renderTargetBitmap.PixelHeight, 96d, 96d,
                                pixelBuffer.ToArray());
                         
                            await encoder.FlushAsync();
                         
                       }
                        var filestream = await file.OpenAsync(FileAccessMode.Read);
                        BitmapImage imageBit = new BitmapImage();
                      
                        ImageBrush brush = new ImageBrush();
                        brush.Stretch = Stretch.UniformToFill;
                        await imageBit.SetSourceAsync(filestream);
                        brush.ImageSource = imageBit;
                        Els.Fill = brush;
                    }
                }
                else
                {
                    await webView.InvokeScriptAsync("eval", new string[] { "document.getElementsByName('usernameFieldPanel:usernameFieldPanel_body:usernameField')[0].value='" + username + "';" });
                    await webView.InvokeScriptAsync("eval", new string[] { "document.getElementsByName('passwordFieldPanel:passwordFieldPanel_body:passwordField')[0].value='" + password + "';" });
                    await webView.InvokeScriptAsync("eval", new string[] { "document.getElementsByTagName('a')[0].click();" });
                    webView.Navigate(new Uri("https://merewa-elo.somtoday.nl/pasfoto/pasfoto_leerling.jpg?id=546308480"));
                }

            }
            catch (Exception e1)
            {
                Debug.WriteLine(e1.Message);
            }
        }
    }
}
