using IniParser.Model;
using IniParser.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class NewFavoriteControl : Page
    {
        private WebView _webView;
        public ObservableCollection<TileGroup> GroupedItems { get; set; }
        public NewFavoriteControl()
        {
            this.InitializeComponent();
            CompositionTarget.Rendering += Update;

            GroupedItems = new ObservableCollection<TileGroup>();

            GetAndReturnFavorites();
        }

        private void Update(object sender, object e)
        {
            if (App.Current.RequestedTheme == ApplicationTheme.Dark)
            {
                NewFavoriteGrid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 31, 31, 31));
            }
            else
            {
                NewFavoriteGrid.Background = new SolidColorBrush(Windows.UI.Colors.LightGray);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is WebView webView)
            {
                _webView = webView;
            }
        }

        private async void GetAndReturnFavorites()
        {
            try
            {
                IniDataParser parser = new IniDataParser();
                StorageFolder favoriteFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Favorites");
                IReadOnlyList<StorageFolder> folderList = await favoriteFolder.GetFoldersAsync();

                foreach (StorageFolder folder in folderList)
                {
                    TileGroup group = new TileGroup() { Key = folder.Name, Items = new ObservableCollection<TileItem>() };
                    GroupedItems.Add(group);

                    IReadOnlyList<StorageFile> filesList = await folder.GetFilesAsync();

                    foreach (StorageFile file in filesList)
                    {
                        string content = await FileIO.ReadTextAsync(file);
                        IniData data = parser.Parse(content);
                        TileItem tileItem = new TileItem()
                        {
                            GroupName = folder.Name,
                            Name = data["Favorite"]["Name"],
                            Description = data["Favorite"]["Description"],
                            Icon = data["Favorite"]["Icon"]  // Store the Icon URL
                        };
                        tileItem.OnClick += () =>
                        {
                            _webView?.goTo(data["Favorite"]["URL"]);
                        };
                        group.Items.Add(tileItem);
                    }
                }

            }
            catch (Exception ex)
            {
                TileGroup group = new TileGroup { Key = "Error" };
                TileItem item = new TileItem { Name = ex.Message };
                group.Items.Add(item);
                GroupedItems.Add(group);
            }
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is TileItem clickedTile && clickedTile.OnClick != null) 
            {
                clickedTile.OnClick.Invoke();
            }
        }

        private async void AddFavorite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StorageFolder favoriteFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Favorites");
                StorageFolder webSitesFolder = await favoriteFolder.GetFolderAsync("WebSites");
                IniData data = new IniData();
                data["Favorite"]["Name"] = _webView.getCurrentTitle();
                data["Favorite"]["URL"] = _webView.getCurrentUrl();
                data["Favorite"]["Icon"] = new Uri(_webView.getCurrentUrl()).GetLeftPart(UriPartial.Authority) + "/favicon.ico";
                data["Favorite"]["Description"] = "Favorite added by user.";
                StorageFile file = await webSitesFolder.CreateFileAsync(GenerateRandomString(10) + ".ini");
                await FileIO.WriteTextAsync(file, data.ToString());
            } catch(Exception ex) 
            { 
            
            }
        }
        private string GenerateRandomString(int len)
        {
            Random random = new Random();

            int randValue;
            string str = "";
            char letter;
            for (int i = 0; i < len; i++)
            {

                // Generating a random number. 
                randValue = random.Next(0, 26);

                // Generating random character by converting 
                // the random number into character. 
                letter = Convert.ToChar(randValue + 65);

                // Appending the letter to string. 
                str = str + letter;
            }
            return str;
        }
    }

    public class TileItem
    {
        public string Name { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public Action OnClick { get; set; }
    }

    public class TileGroup
    {
        public string Key { get; set; }
        public ObservableCollection<TileItem> Items { get; set;}
    }
}
