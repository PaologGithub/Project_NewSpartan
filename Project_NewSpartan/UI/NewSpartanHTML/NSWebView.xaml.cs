using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Text;
using System.Diagnostics;

using Project_NewSpartan.UI.NewSpartanHTML;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Project_NewSpartan.UI.NewSpartanHTML
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class NSWebView : Page
    {
        public event TypedEventHandler<WebView, Uri> SourceChanged;
        public NSWebView()
        {
            this.InitializeComponent();
            // Implement SourceChanged

            this.goTo("https://www.google.com");
        }

        public string getCurrentUrl()
        {
            //Implement getCurrentUrl
            return "";
        }

        public void GoBack()
        {
            //Implement GoBack
        }

        public void GoForward()
        {
            //Implement GoForward
        }

        public void Refresh()
        {
            //Implement Refresh
        }

        public async void goTo(string url)
        {
            NSHtmlRenderer renderer = new NSHtmlRenderer(url, Content);
            renderer.RenderHTML();

            
        }

        public void showDownloads()
        {
            //Implement Show Downloads
        }

        public bool canGoBack()
        {
            //Implement canGoBack
            return true;
        }

        public bool canGoForward()
        {
            //Implement canGoForward
            return true;
        }
    }
}
