using System;
using System.Collections.Generic;
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

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236

namespace Project_NewSpartan.UI
{
    public sealed partial class AboutControl : Page
    {
        private WebView _webView;
        
        public AboutControl()
        {
            this.InitializeComponent();
            CompositionTarget.Rendering += Update;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is WebView webView)
            {
                _webView = webView;
            }
        }

        private void Update(object sender, object e)
        {
            if (App.Current.RequestedTheme == ApplicationTheme.Dark)
            {
                AboutControlGrid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 31, 31, 31));
            }
            else
            {
                AboutControlGrid.Background = new SolidColorBrush(Windows.UI.Colors.LightGray);
            }
        }
        private void Hyperlink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            _webView?.goTo("https://www.github.com/PaologGithub/Project_NewSpartan");
        }
    }
}
