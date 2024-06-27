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
        public TitleBar()
        {
            this.InitializeComponent();
            CompositionTarget.Rendering += Update;


            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
        }
        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            AppTitleBar.Height = sender.Height;
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
        }
    }
}
