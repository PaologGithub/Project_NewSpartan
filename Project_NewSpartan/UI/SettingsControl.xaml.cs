using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Principal;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236

namespace Project_NewSpartan.UI
{
    public sealed partial class SettingsControl : Page
    {
        private WebView _webView;
        private TabViewItem _tab;
        private NavigationParametersArgs _parametersArgs;
        public SettingsControl()
        {
            this.InitializeComponent();
            CompositionTarget.Rendering += Update;

            

        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string userName = await getUserName();
            HelloText.Text = $"Hello, {userName}";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is NavigationParametersArgs navigationParametersArgs)
            {
                _webView = navigationParametersArgs.webView;
                _tab = navigationParametersArgs.tabViewItem;
                Debug.WriteLine(_tab.Header);
            }
        }

        private void Update(object sender, object e)
        {
            if (App.Current.RequestedTheme == ApplicationTheme.Dark)
            {
                SettingsControlGrid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 31, 31, 31));
            }
            else
            {
                SettingsControlGrid.Background = new SolidColorBrush(Windows.UI.Colors.LightGray);
            }
        }

        private void Hyperlink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            ((Window.Current.Content as Frame).Content as MainPage).HandleProtocolActivation("https://www.github.com/PaologGithub/Project_NewSpartan");
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Grid mainGrid = (_tab.Content as Grid);

            // Metro SettingsPane like Grid
            Grid favoriteGrid = new Grid();
            Grid.SetRow(favoriteGrid, 2);
            favoriteGrid.HorizontalAlignment = HorizontalAlignment.Right;
            favoriteGrid.Background = new SolidColorBrush(Colors.Gray);
            favoriteGrid.MinWidth = 300;

            Frame favoriteFrame = new Frame { Width = 300 };
            favoriteFrame.Navigate(typeof(AboutControl), _webView);

            favoriteGrid.Children.Add(favoriteFrame);

            mainGrid.Children.Add(favoriteGrid);
        }

        private async Task<String> getUserName() {
            IReadOnlyList<User> users = await User.FindAllAsync();

            var current = users.Where(p => p.AuthenticationStatus == UserAuthenticationStatus.LocallyAuthenticated &&
                                        p.Type == UserType.LocalUser).FirstOrDefault();


            // user may have username
            var data = await current.GetPropertyAsync(KnownUserProperties.AccountName);
            string displayName = (string)data;

            // or may be authinticated using hotmail 
            if (String.IsNullOrEmpty(displayName) || displayName == " ")
            {

                string a = (string)await current.GetPropertyAsync(KnownUserProperties.FirstName);
                string b = (string)await current.GetPropertyAsync(KnownUserProperties.LastName);
                displayName = string.Format("{0} {1}", a, b);
            }


            // User may not has let us have their infos
            if (String.IsNullOrEmpty(displayName) || displayName == " ") { displayName = WindowsIdentity.GetCurrent().Name.Split("\\")[1]; }

            // Last try
            if (String.IsNullOrEmpty(displayName) || displayName == " ") { displayName = "Unknown User"; }

            return displayName;
        }

        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem menuFlyoutItem)
            {
                string selectedText = menuFlyoutItem.Text;
                string userName = await getUserName();

                HelloText.Text = $"{selectedText}, {userName}";
            }
        }

        // Yeah there is a function for theme changing, but it bugs and work like sh*t
        /*private async void ChangeTheme_Toggled(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["themeSetting"] = ((ToggleSwitch)sender).IsOn ? 0 : 1;
            //Restart app if theme setting and switch value are not equal

            ContentDialog finishSetupDialog = new ContentDialog
            {
                Title = "Information",
                Content = "To apply theme, you need to restart NewSpartan.\nPlease restart Project NewSpartan.",
                PrimaryButtonText = "Restart NewSpartan",
                SecondaryButtonText = "Close NewSpartan",
                CloseButtonText = "Close this popup",
                DefaultButton = ContentDialogButton.Primary
            };
            ContentDialogResult result = await finishSetupDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                await CoreApplication.RequestRestartAsync("Restarting app to finish setup");
            }
            else if (result == ContentDialogResult.Secondary)
            {

                CoreApplication.Exit();
            }
            else
            {
                //Do Nothing
            }

        }

        private void ChangeTheme_Loaded(object sender, RoutedEventArgs e)
        {
            ((ToggleSwitch)sender).IsOn = App.Current.RequestedTheme == ApplicationTheme.Light;
        }*/
    }
}
