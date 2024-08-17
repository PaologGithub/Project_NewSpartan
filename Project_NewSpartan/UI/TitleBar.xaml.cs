using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class TitleBar : Page
    {
        double TitleBarButtonsWidth = 0;
        public TitleBar()
        {
            this.InitializeComponent();
            CompositionTarget.Rendering += Update;


            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            Window.Current.SetTitleBar(DragRegion);
            AddTabButtonUpper_Click(null, null);

        }
        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            // AppTitleBar.Height = sender.Height + 8
            TitleBarButtonsWidth = sender.SystemOverlayRightInset;
        }

        private void Update(object sender, object e)
        {
            var titlebar = ApplicationView.GetForCurrentView().TitleBar;
            if (App.Current.RequestedTheme == ApplicationTheme.Dark)
            {
                AppTitleBar.Background = new SolidColorBrush(Colors.Black);
                titlebar.ButtonBackgroundColor = Colors.Black;
                titlebar.ButtonHoverBackgroundColor = Colors.DarkGray;
                titlebar.ButtonInactiveBackgroundColor = Colors.Black;
                titlebar.ButtonInactiveForegroundColor = Colors.LightGray;
            }
            else
            {
                AppTitleBar.Background = new SolidColorBrush(Colors.LightGray);
                titlebar.ButtonBackgroundColor = Colors.LightGray;
                titlebar.ButtonHoverBackgroundColor = Colors.Gray;
                titlebar.ButtonInactiveBackgroundColor = Colors.LightGray;
                titlebar.ButtonInactiveForegroundColor = Colors.Gray;
            }

            foreach(TabViewItem tab in Tabs.Items)
            {
                Grid tabContent = (tab.Content as Grid);
                if (tabContent.Children.Count >= 2)
                {
                    string title = ((tabContent.Children[0] as Frame).Content as WebView)?.getCurrentTitle();
                    tab.Header = title;
                }
            }
        }

        private void AddTabButtonUpper_Click(object sender, RoutedEventArgs e)
        {
            TabViewItem tabViewItem = new TabViewItem();
            tabViewItem.Header = "New Tab";

            Grid grid = new Grid();
            tabViewItem.Content = grid;

            RowDefinition rowDefinition1 = new RowDefinition();
            rowDefinition1.Height = new GridLength(0, GridUnitType.Auto);
            grid.RowDefinitions.Add(rowDefinition1);

            RowDefinition rowDefinition2 = new RowDefinition();
            rowDefinition2.Height = new GridLength(1, GridUnitType.Star);
            grid.RowDefinitions.Add(rowDefinition2);

            Frame webViewFrame = new Frame();
            Grid.SetRow(webViewFrame, 1);
            grid.Children.Add(webViewFrame);
            webViewFrame.Navigate(typeof(UI.WebView));
            
            Frame navigationRailFrame = new Frame();
            Grid.SetRow(navigationRailFrame, 0);
            grid.Children.Add(navigationRailFrame);

            NavigationParametersArgs args = new NavigationParametersArgs() { webView = (webViewFrame.Content as WebView), tabViewItem=tabViewItem };
            navigationRailFrame.Navigate(typeof(UI.NavigationRailFrame), args);

            Tabs.Items.Add(tabViewItem);
        }
    }

}
