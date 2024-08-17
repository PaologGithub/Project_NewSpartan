using Microsoft.Toolkit.Uwp.UI.Controls;
using Project_NewSpartan.UI.NewSpartanHTML;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Project_NewSpartan.UI
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class NavigationRailFrame : Page
    {
        private WebView _webView;
        private TabViewItem _tab;
        private NavigationParametersArgs _parametersArgs;

        public NavigationRailFrame()
        {
            this.InitializeComponent();

            this.SizeChanged += OnSizeChanged;
            CompositionTarget.Rendering += Update;

            URLInput.KeyDown += URLInput_KeyDown;
            
        }

        private void webView_SourceChanged(WebView sender, Uri args)
        {
            //Update URL
            URLInput.Text = args.ToString();
        }

        private void Update(object sender, object e)
        {
            if (!_webView.canGoBack())
            {
                BackButton.IsEnabled = false;
            } else
            {
                BackButton.IsEnabled = true;
            }
            if (!_webView.canGoForward())
            {
                ForwardButton.IsEnabled = false;
            } else
            {
                ForwardButton.IsEnabled = true;
            }

            if (App.Current.RequestedTheme == ApplicationTheme.Dark)
            {
                NavigationRailFrameGrid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255,31,31,31));
                Line1.Stroke = new SolidColorBrush(Colors.White);
                Line2.Stroke = new SolidColorBrush(Colors.White);
            }
            else
            {
                NavigationRailFrameGrid.Background = new SolidColorBrush(Colors.White);
                Line1.Stroke = new SolidColorBrush(Colors.Black);
                Line2.Stroke = new SolidColorBrush(Colors.Black);
            }
        }

        /* Define the _webView */
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is NavigationParametersArgs navigationParametersArgs)
            {
                _parametersArgs = navigationParametersArgs;
                _webView = navigationParametersArgs.webView;
                _webView.SourceChanged += webView_SourceChanged;
                _tab = navigationParametersArgs.tabViewItem;
            }
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateURLInputWidth(e.NewSize.Width);
        }
        private void UpdateURLInputWidth(double appWidth)
        {
            /// Change URL Input Size
            /// Because the application equals SizeChangedEventArgs.NewSize.Width
            /// And there is 8 buttons of 40 pixels width, and 2 Line(12px), we can use this method to fit the input
            /// To Finish, put all that in a update loop
            URLInput.MaxWidth = URLInput.Width = URLInput.MinWidth = appWidth - ((8*40) + (2*12));

        }

        private void URLInput_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            // Logic to handle when Enter key is pressed in the TextBox
            if (e.Key == VirtualKey.Enter)
            {
                e.Handled = true; // Prevents the default behavior (like beeping)

                string url = URLInput.Text;
                // Check if URI is valid uri
                if (Uri.TryCreate(url, UriKind.Absolute, out Uri result))
                {
                    _webView.goTo(url);
                } else if (Uri.TryCreate($"http://{url}", UriKind.Absolute, out Uri result1))
                {
                    _webView.goTo($"http://{url}");
                } else
                {
                    _webView.goTo($"https://www.google.com/search?q={url}");
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _webView.GoBack();
        }
        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            _webView.GoForward();
        }
        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            _webView.goTo(URLInput.Text);
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            _webView.Refresh();
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            _webView.showDownloads();
        }

        private void FavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            Grid mainGrid = (_tab.Content as Grid);

            //If there is more items than three in MainPage (so there is a Pane Open), close it
            if (mainGrid.Children.Count > 2)
            {
                for (int i = mainGrid.Children.Count - 1; i >= 2; i--)
                {
                    mainGrid.Children.RemoveAt(i);
                }
            } else
            {
                // Metro SettingsPane like Grid
                Grid favoriteGrid = new Grid();
                Grid.SetRow(favoriteGrid, 1);
                favoriteGrid.HorizontalAlignment = HorizontalAlignment.Right;
                favoriteGrid.Background = new SolidColorBrush(Colors.Gray);
                favoriteGrid.MinWidth = 300;

                Frame favoriteFrame = new Frame { Width = 300 };
                favoriteFrame.Navigate(typeof(NewFavoriteControl), _webView);

                favoriteGrid.Children.Add(favoriteFrame);

                mainGrid.Children.Add(favoriteGrid);
            }

        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Grid mainGrid = (_tab.Content as Grid);

            if (mainGrid.Children.Count > 2)
            {
                while(mainGrid.Children.Count > 2)
                {
                    mainGrid.Children.RemoveAt(2);
                }
            } else
            {
                // Metro SettingsPane like Grid
                Grid settingsGrid = new Grid();
                Grid.SetRow(settingsGrid, 2);
                settingsGrid.HorizontalAlignment = HorizontalAlignment.Right;
                settingsGrid.Background = new SolidColorBrush(Colors.Gray);
                settingsGrid.MinWidth = 300;

                Frame settingsFrame = new Frame { Width = 300 };
                settingsFrame.Navigate(typeof(SettingsControl), _parametersArgs);

                settingsGrid.Children.Add(settingsFrame);

                mainGrid.Children.Add(settingsGrid);
            }
        }
    }
}