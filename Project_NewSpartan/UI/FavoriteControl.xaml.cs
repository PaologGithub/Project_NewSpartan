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

using IniParser.Model;
using IniParser.Parser;
using Windows.UI.ViewManagement;
using Windows.UI;

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236

namespace Project_NewSpartan.UI
{
    public sealed partial class FavoriteControl : UserControl
    {
        private ObservableCollection<LogItem> Logs;
        public FavoriteControl()
        {
            this.InitializeComponent();
            CompositionTarget.Rendering += Update;

            Logs = new ObservableCollection<LogItem>();
            FavoriteTreeView.ItemsSource = Logs;

            getRootFolder(Logs);
        }

        private async void getRootFolder(ObservableCollection<LogItem> logs)
        {
            try
            {
                IniDataParser parser = new IniDataParser();
                StorageFolder favoriteFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Favorites");
                IReadOnlyList<StorageFolder> folderList = await favoriteFolder.GetFoldersAsync();

                foreach (StorageFolder folder in folderList)
                {
                    LogItem folderLogItem = new LogItem(folder.Name);
                    logs.Add(folderLogItem);

                    IReadOnlyList<StorageFile> filesList = await folder.GetFilesAsync();

                    foreach(StorageFile file in filesList)
                    {
                        string content = await Windows.Storage.FileIO.ReadTextAsync(file);
                        IniData data = parser.Parse(content);
                        folderLogItem.Children.Add(new LogItem(data["Favorite"]["Name"], false, data["Favorite"]["Description"]));
                    }
                }

                IReadOnlyList<StorageFile> favoriteFilesList = await favoriteFolder.GetFilesAsync();

                foreach( StorageFile file in favoriteFilesList)
                {
                    logs.Add(new LogItem(file.Name));
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                logs.Add(new LogItem($"Error: {ex.Message}"));
            }
        }
        private void Update(object sender, object e)
        {
            if (App.Current.RequestedTheme == ApplicationTheme.Dark)
            {
                FavoriteControlGrid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255,31,31,31));
            }
            else
            {
                FavoriteControlGrid.Background = new SolidColorBrush(Windows.UI.Colors.LightGray);
            }
        }
    }
}
