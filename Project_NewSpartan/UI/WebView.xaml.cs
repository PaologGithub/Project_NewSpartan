﻿using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Project_NewSpartan.UI
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class WebView : Page
    {
        public event TypedEventHandler<WebView, Uri> SourceChanged;
        public WebView()
        {
            this.InitializeComponent();
            WebView2.NavigationCompleted += WebView2_NavigationComplete;
        }

        private void WebView2_NavigationComplete(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            SourceChanged?.Invoke(this, sender.Source);

            if (!args.IsSuccess)
            {
                string message = $"Navigation wasn't successfull. Check the url. Status Code: {args.HttpStatusCode}";
                goTo($"https://paologgithub.github.io/products/NewSpartan/pages/error.html?NEWSPARTAN_URL={sender.Source.AbsoluteUri}&NEWSPARTAN_ERRCODE={args.WebErrorStatus}&NEWSPARTAN_ERRMSG={message}");
            }
        }


        public string getCurrentUrl()
        {
            return WebView2.Source.ToString();
        }

        public string getCurrentTitle()
        {
            return WebView2.CoreWebView2?.DocumentTitle;
        }

        public void GoBack()
        {
            WebView2.GoBack();
        }
        public void GoForward()
        {
            WebView2.GoForward();
        }
        public void Refresh()
        {
            WebView2.Reload();
        }
        public void goTo(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                WebView2.Source = uri;
            } catch (Exception ex)
            {
                goTo($"https://paologgithub.github.io/products/NewSpartan/pages/error.html?NEWSPARTAN_URL={url}&NEWSPARTAN_ERRCODE={ex.GetType()}&NEWSPARTAN_ERRMSG={ex.Message}");
            }
        }

        public void showDownloads()
        {
            WebView2.CoreWebView2.OpenDefaultDownloadDialog();
        }
        public bool canGoBack()
        {
            return (WebView2.CanGoBack);
        }
        public bool canGoForward()
        {
            return (WebView2.CanGoForward);
        }

    }
}
