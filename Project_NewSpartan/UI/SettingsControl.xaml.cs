using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            this.InitializeComponent();
            CompositionTarget.Rendering += Update;

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
            Grid mainGrid = (((Window.Current.Content as Frame).Content as Page).Content as Grid);

            // Metro SettingsPane like Grid
            Grid favoriteGrid = new Grid();
            Grid.SetRow(favoriteGrid, 2);
            favoriteGrid.HorizontalAlignment = HorizontalAlignment.Right;
            favoriteGrid.Background = new SolidColorBrush(Colors.Gray);
            favoriteGrid.MinWidth = 300;

            Frame favoriteFrame = new Frame { Content = new AboutControl(), Width = 300 };

            favoriteGrid.Children.Add(favoriteFrame);

            mainGrid.Children.Add(favoriteGrid);
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
