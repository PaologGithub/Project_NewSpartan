using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Project_NewSpartan.UI;


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Project_NewSpartan
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            MainFrame.Navigate(typeof(UI.WebView));
            TitlebarFrame.Navigate(typeof(UI.TitleBar));
            /// Put the WebView Frame as Parameter of NavigationRailFrame
            NavigationFrame.Navigate(typeof(UI.NavigationRailFrame), MainFrame.Content as UI.WebView);

            this.Loaded += CheckForSetupFile;
        }

        private async void CheckForSetupFile(object sender, RoutedEventArgs e)
        {
            bool setupFileExists = await CheckIfSetupFileExists();
            if (setupFileExists)
            {
                // No need to run setup
                // Continue running application here -> Load Favorites

            } else
            {
                //Setup
                setup_ProjectNewSpartan();
            }
        }

        public async Task<bool> CheckIfSetupFileExists()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            try
            {
                StorageFile setupFile = await localFolder.GetFileAsync("__SETUP__");
                return true;
            } catch (FileNotFoundException)
            {
                return false;
            }
        }

        private async void setup_ProjectNewSpartan()
        {
            // Navigate to setup page to setup Project NewSpartan

            bool navigated = Frame.Navigate(typeof(UI.SetupPage), null);

            if (navigated)
            {
            }
            else
            {
                var dialog = new MessageDialog("Cannot setup, Navigation to UI/SetupPage failed");
                await dialog.ShowAsync();
            }
        }

        public void HandleProtocolActivation(string uri)
        {
            if (uri.StartsWith("newspartan:")) { uri = uri.Substring("newspartan:".Length); };
            // Replace with your logic to handle the URI
            // Example: Navigating a WebView to the URL
            (MainFrame.Content as UI.WebView).goTo(uri);
        }

    }
}
