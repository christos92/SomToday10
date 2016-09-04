using BetterSom.Models;
using BetterSom.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BetterSom.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainHub : Page
    {
        public MainHub()
        {
            this.InitializeComponent();
            this.Loaded += MainHub_Loaded;
            }

        public ObservableCollection<ModelItem> items = new ObservableCollection<ModelItem>();
        private void MainHub_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var home = new ModelItem();
                home.Title = "Rooster";
                home.symb = Symbol.CalendarWeek;
                home.frame = typeof(MainPage);
                items.Add(home);

                var huiswerk = new ModelItem();
                huiswerk.Title = "Huiswerk";
                huiswerk.symb = Symbol.Rename;
                huiswerk.frame = typeof(MainPage);
                items.Add(huiswerk);

                var cijfers = new ModelItem();
                cijfers.Title = "Cijfers";
                cijfers.symb = Symbol.FontSize;
                cijfers.frame = typeof(MainPage);
                items.Add(cijfers);


                var vakken = new ModelItem();
                vakken.Title = "Vakken";
                vakken.symb = Symbol.Library;
                vakken.frame = typeof(MainPage);
                items.Add(vakken);

                var afwezigheid = new ModelItem();
                afwezigheid.Title = "Afwezigheid";
                afwezigheid.symb = Symbol.ContactPresence;
                vakken.frame = typeof(MainPage);
                items.Add(afwezigheid);

                MainHubView.ItemsSource = items;

            }
            catch (Exception e1)
            {
                Debug.WriteLine(e1.Message);
            }
        }
    }
}
