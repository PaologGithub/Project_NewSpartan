using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using HtmlParserDotNet;
using Windows.UI.Xaml.Documents;

namespace Project_NewSpartan.UI.NewSpartanHTML
{
    public class NSHtmlRenderer
    {
        private String URL;
        private byte[] HtmlBytes;
        private StackPanel Content;

        public NSHtmlRenderer(String URL, StackPanel Content) {
            this.URL = URL;
            this.Content = Content;
        }

        public void RenderHTML()
        {
            CleanContent();

            // Parse HtmlDOM

            HtmlDocument document = HtmlParser.LoadFromUrl(URL);
            if (document != null)
            {
                Debug.WriteLine(document.ToString());
                // Rendering
                document.DocumentElement.GetElementsByTagName("a").ForEach((node) =>
                {
                    string hrefValue = node.GetAttributeValue("href").FirstOrDefault();

                    if (!string.IsNullOrEmpty(hrefValue) && Uri.IsWellFormedUriString(hrefValue, UriKind.Absolute))
                    {
                        Hyperlink hyperlink = new Hyperlink
                        {
                            NavigateUri = new Uri(hrefValue)
                        };
                        
                        hyperlink.Inlines.Add(new Run{Text=node.InnerHTML});

                        TextBlock textBlock = new TextBlock
                        {
                            Margin = new Thickness(0, 10, 0, 0)
                        };
                        textBlock.Inlines.Add(hyperlink);

                        Content.Children.Add(textBlock);
                    }
                });

                document.DocumentElement.GetElementsByTagName("h1").ForEach((node) =>
                {
                    TextBlock textBlock = new TextBlock
                    {
                        Text = node.InnerHTML,
                        FontSize = 40,
                        Margin = new Thickness(0, 10, 0, 0)
                    };
                    Content.Children.Add(textBlock);
                });

                document.DocumentElement.GetElementsByTagName("p").ForEach((node) =>
                {
                    TextBlock textBlock = new TextBlock
                    {
                        Text = node.InnerHTML,
                        Margin = new Thickness(0, 10, 0, 0)
                    };
                    Content.Children.Add(textBlock);
                });

                document.DocumentElement.GetElementsByTagName("button").ForEach((node) =>
                {
                    Button button = new Button
                    {
                        Content = node.InnerHTML,
                        Margin = new Thickness(0, 10, 0, 0)
                    };
                    Content.Children.Add(button);
                });

                document.DocumentElement.GetElementsByTagName("input").ForEach((node) =>
                {
                    switch (node.GetAttributeValue("type").FirstOrDefault() ?? "text")
                    {
                        case "text":

                            TextBox textBox = new TextBox
                            {
                                PlaceholderText = node.GetAttributeValue("placeholder").FirstOrDefault() ?? "",
                                Margin = new Thickness(0, 10, 0, 0)
                            };
                            Content.Children.Add(textBox);
                            break;

                        case "checkbox":

                            CheckBox checkBox = new CheckBox
                            {
                                Margin = new Thickness(0, 10, 0, 0)
                            };
                            Content.Children.Add(checkBox);
                            break;
                    }
                });
            }
            else
            {
                Content.Children.Add(new TextBlock { Text = "Failed to parse HTML, document is null" });
            }
        }


        private void CleanContent()
        {
            this.Content.Children.Clear();
        }
    }
}
