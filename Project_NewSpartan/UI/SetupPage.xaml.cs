using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using IniParser;
using IniParser.Model;
using IniParser.Parser;
using System.Diagnostics;
using System.IO;
using static System.Net.WebRequestMethods;
using Windows.ApplicationModel.Core;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Project_NewSpartan.UI
{
    public sealed partial class SetupPage : Page
    {
        private ObservableCollection<LogItem> Logs;

        public SetupPage()
        {
            this.InitializeComponent();

            Logs = new ObservableCollection<LogItem>();
            LogTreeView.ItemsSource = Logs;

            // Example: Add a log entry
            var rootLog = new LogItem("Log");
            Logs.Add(rootLog);
            setup_Favorite(rootLog);
        }

        private async void setup_Favorite(LogItem rootLog)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            var childLog = new LogItem("Preparing for favorites setup");
            rootLog.Children.Add(childLog);

            checkForNewSpartanRequirement(childLog);
            await Task.Run(() => setupFavoriteFolders(localFolder, childLog)); 
            await Task.Run(() => createFavoritesFiles(localFolder, childLog));
            await Task.Run(() => writeFavoritesFiles(localFolder, childLog));
            await Task.Delay(2000);
            finishSetup(localFolder, childLog);


            

        }
        //WebSites: google.ini
        //Songs: Apple5GSong.ini
        //Help: Rickroll(VideoHelp.ini), MainHelp.ini, howtousegoogle.ini (https://searchengineland.com/guide/how-to-use-google-to-search)

        //Tip: If you are checking the next function, press the arrows on the line to hide the after functions, because they're dumb code.
        private void checkForNewSpartanRequirement(LogItem childLog)
        {
            childLog.Children.Add(new LogItem("Testing for Favorites Requirements: INI PARSING"));
            try
            {
                childLog.Children.Add(new LogItem("Testing IniParser"));
                var parser = new IniDataParser();
                IniData data = parser.Parse("[Test]\ntest=hi\ntest2=ih\n[Test2]\nproject_name=project_newspartan");
                ProgressBar.Value = 10;
                childLog.Children.Add(new LogItem("INIPARSER TEST: [Test]\\test=" + data["Test"]["test"].ToString()));
                childLog.Children.Add(new LogItem("INIPARSER TEST: [Test]\\test=" + data["Test"]["test2"].ToString()));
                childLog.Children.Add(new LogItem("INIPARSER TEST: [Test2]\\project_name=" + data["Test2"]["project_name"].ToString()));

                if (data["Test"]["test"].ToString() == "hi" && data["Test"]["test2"].ToString() == "ih" && data["Test2"]["project_name"].ToString() == "project_newspartan")
                {
                    childLog.Children.Add(new LogItem("IniParser test finished successfully"));


                }
                else
                { 
                    throw new Exception("INIPARSER TEST: The value are not attended value. :,(");

                }
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while creating Help favorite folder. Error: " + ex.Message + ". Continuing", true));
            }
        }
        private async void setupFavoriteFolders(StorageFolder localFolder, LogItem childLog)
        {

            try
            {
                childLog.Children.Add(new LogItem("Creating folder APP/Favorites"));
                await localFolder.CreateFolderAsync("Favorites");
                childLog.Children.Add(new LogItem("APP/Favorites folder created successfully"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while creating folder APP/Favorites. Error: " + ex.Message + ". Continuing", true));
            }

            try
            {
                childLog.Children.Add(new LogItem("Creating WebSites folder inside APP/Favorites folder"));
                StorageFolder favoritesFolder = await localFolder.GetFolderAsync("Favorites");
                await favoritesFolder.CreateFolderAsync("WebSites");
                childLog.Children.Add(new LogItem("Created folder APP/Favorites/WebSites"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while creating WebSites favorite folder. Error: " + ex.Message + ". Continuing", true));
            }

            try
            {
                childLog.Children.Add(new LogItem("Creating Notes folder inside APP/Favorites folder"));
                StorageFolder favoritesFolder = await localFolder.GetFolderAsync("Favorites");
                await favoritesFolder.CreateFolderAsync("Notes");
                childLog.Children.Add(new LogItem("Created folder APP/Favorites/Notes"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while creating Notes favorite folder. Error: " + ex.Message + ". Continuing", true));
            }

            try
            {
                childLog.Children.Add(new LogItem("Creating Songs folder inside APP/Favorites folder"));
                StorageFolder favoritesFolder = await localFolder.GetFolderAsync("Favorites");
                await favoritesFolder.CreateFolderAsync("Songs");
                childLog.Children.Add(new LogItem("Created folder APP/Favorites/Songs"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while creating Songs favorite folder. Error: " + ex.Message + ". Continuing", true));
            }


            try
            {
                childLog.Children.Add(new LogItem("Creating Help folder inside APP/Favorites folder"));
                StorageFolder favoritesFolder = await localFolder.GetFolderAsync("Favorites");
                await favoritesFolder.CreateFolderAsync("Help");
                childLog.Children.Add(new LogItem("Created folder APP/Favorites/Help"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while creating Help favorite folder. Error: " + ex.Message + ". Continuing", true));
            }

        }
        private async void createFavoritesFiles(StorageFolder localFolder, LogItem childLog)
        {
            try
            {
                childLog.Children.Add(new LogItem("Creating google.ini inside APP/Favorites/WebSites folder"));
                StorageFolder favoritesFolder = await localFolder.GetFolderAsync("Favorites");
                StorageFolder websitesFolder = await favoritesFolder.GetFolderAsync("WebSites");
                await websitesFolder.CreateFileAsync("google.ini");
                childLog.Children.Add(new LogItem("Created folder APP/Favorites/WebSites/google.ini"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while creating google.ini. Error: " + ex.Message + ". Continuing", true));
            }

            try
            {
                childLog.Children.Add(new LogItem("Creating Apple5GSong.ini inside APP/Favorites/Songs folder"));
                StorageFolder favoritesFolder = await localFolder.GetFolderAsync("Favorites");
                StorageFolder songsFolder = await favoritesFolder.GetFolderAsync("Songs");
                await songsFolder.CreateFileAsync("Apple5GSong.ini");
                childLog.Children.Add(new LogItem("Created folder APP/Favorites/Songs/Apple5GSong.ini"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while creating Apple5GSong.ini. Error: " + ex.Message + ". Continuing", true));
            }

            try
            {
                childLog.Children.Add(new LogItem("Creating VideoHelp.ini inside APP/Favorites/Help folder"));
                StorageFolder favoritesFolder = await localFolder.GetFolderAsync("Favorites");
                StorageFolder helpFolder = await favoritesFolder.GetFolderAsync("Help");
                await helpFolder.CreateFileAsync("VideoHelp.ini");
                childLog.Children.Add(new LogItem("Created folder APP/Favorites/Help/VideoHelp.ini"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while creating VideoHelp.ini. Error: " + ex.Message + ". Continuing", true));
            }

            try
            {
                childLog.Children.Add(new LogItem("Creating MainHelp.ini inside APP/Favorites/Help folder"));
                StorageFolder favoritesFolder = await localFolder.GetFolderAsync("Favorites");
                StorageFolder helpFolder = await favoritesFolder.GetFolderAsync("Help");
                await helpFolder.CreateFileAsync("MainHelp.ini");
                childLog.Children.Add(new LogItem("Created folder APP/Favorites/Help/MainHelp.ini"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while creating MainHelp.ini. Error: " + ex.Message + ". Continuing", true));
            }

            try
            {
                childLog.Children.Add(new LogItem("Creating HowToUseGoogle.ini inside APP/Favorites/Help folder"));
                StorageFolder favoritesFolder = await localFolder.GetFolderAsync("Favorites");
                StorageFolder helpFolder = await favoritesFolder.GetFolderAsync("Help");
                await helpFolder.CreateFileAsync("HowToUseGoogle.ini");
                childLog.Children.Add(new LogItem("Created folder APP/Favorites/Help/HowToUseGoogle.ini"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while creating HowToUseGoogle.ini. Error: " + ex.Message + ". Continuing", true));
            }

        }
        private async void writeFavoritesFiles(StorageFolder localFolder, LogItem childLog)
        {
            try
            {
                childLog.Children.Add(new LogItem("Writing google.ini inside APP/Favorites/WebSites folder"));
                StorageFolder favoritesFolder = await localFolder.GetFolderAsync("Favorites");
                StorageFolder websitesFolder = await favoritesFolder.GetFolderAsync("WebSites");
                StorageFile googleIni = await websitesFolder.GetFileAsync("google.ini");
                IniData data = new IniData();
                data["Favorite"]["Name"] = "Google";
                data["Favorite"]["URL"] = "https://www.google.com";
                data["Favorite"]["Icon"] = "https://www.google.com/favicon.ico";
                data["Favorite"]["Description"] = "Search the world's information, including webpages, images, videos and more.";
                await FileIO.WriteTextAsync(googleIni, data.ToString());
                childLog.Children.Add(new LogItem("Writed to APP/Favorites/WebSites/google.ini"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while writing google.ini. Error: " + ex.Message + ". Continuing", true));
            }



            try
            {
                childLog.Children.Add(new LogItem("Writing Apple5GSong.ini inside APP/Favorites/Songs folder"));
                StorageFolder favoritesFolder = await localFolder.GetFolderAsync("Favorites");
                StorageFolder songsFolder = await favoritesFolder.GetFolderAsync("Songs");
                StorageFile apple5gsongIni = await songsFolder.GetFileAsync("Apple5GSong.ini");
                IniData data = new IniData();
                data["Favorite"]["Name"] = "Apple 5G Song";
                data["Favorite"]["URL"] = "https://www.youtube.com/watch?v=59T1gJHURMo";
                data["Favorite"]["Icon"] = "https://www.youtube.com/favicon.ico";
                data["Favorite"]["Description"] = "Five G Five G Five G";
                await FileIO.WriteTextAsync(apple5gsongIni, data.ToString());
                childLog.Children.Add(new LogItem("Writed to APP/Favorites/Songs/Apple5GSong.ini"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while writing Apple5GSong.ini. Error: " + ex.Message + ". Continuing", true));
            }


            try
            {
                childLog.Children.Add(new LogItem("Writing VideoHelp.ini inside APP/Favorites/Songs folder"));
                StorageFolder favoritesFolder = await localFolder.GetFolderAsync("Favorites");
                StorageFolder helpFolder = await favoritesFolder.GetFolderAsync("Help");
                StorageFile videoHelpIni = await helpFolder.GetFileAsync("VideoHelp.ini");
                IniData data = new IniData();
                data["Favorite"]["Name"] = "Video Help";
                data["Favorite"]["URL"] = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
                data["Favorite"]["Icon"] = "https://www.youtube.com/favicon.ico";
                data["Favorite"]["Description"] = "Help you in Project NewSpartan by watching a video";
                await FileIO.WriteTextAsync(videoHelpIni, data.ToString());
                childLog.Children.Add(new LogItem("Writed to APP/Favorites/Help/VideoHelp.ini"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while writing Apple5GSong.ini. Error: " + ex.Message + ". Continuing", true));
            }


            try
            {
                childLog.Children.Add(new LogItem("Writing MainHelp.ini inside APP/Favorites/Songs folder"));
                StorageFolder favoritesFolder = await localFolder.GetFolderAsync("Favorites");
                StorageFolder helpFolder = await favoritesFolder.GetFolderAsync("Help");
                StorageFile mainHelpIni = await helpFolder.GetFileAsync("MainHelp.ini");
                IniData data = new IniData();
                data["Favorite"]["Name"] = "Get Help";
                data["Favorite"]["URL"] = "https://github.com/PaologGithub/Project_NewSpartan/issues";
                data["Favorite"]["Icon"] = "https://www.github.com/favicon.ico";
                data["Favorite"]["Description"] = "Help you in Project NewSpartan by watching known issues";
                await FileIO.WriteTextAsync(mainHelpIni, data.ToString());
                childLog.Children.Add(new LogItem("Writed to APP/Favorites/Help/MainHelp.ini"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while writing MainHelp.ini. Error: " + ex.Message + ". Continuing", true));
            }


            try
            {
                childLog.Children.Add(new LogItem("Writing HowToUseGoogle.ini inside APP/Favorites/Songs folder"));
                StorageFolder favoritesFolder = await localFolder.GetFolderAsync("Favorites");
                StorageFolder helpFolder = await favoritesFolder.GetFolderAsync("Help");
                StorageFile howToUseGoogleIni = await helpFolder.GetFileAsync("HowToUseGoogle.ini");
                IniData data = new IniData();
                data["Favorite"]["Name"] = "How to use Google";
                data["Favorite"]["URL"] = "https://searchengineland.com/guide/how-to-use-google-to-search";
                data["Favorite"]["Icon"] = "https://www.google.com/favicon.ico";
                data["Favorite"]["Description"] = "See how can you use google (yeah...)";
                await FileIO.WriteTextAsync(howToUseGoogleIni, data.ToString());
                childLog.Children.Add(new LogItem("Writed to APP/Favorites/Help/HowToUseGoogle.ini"));
            }
            catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while writing HowToUseGoogle.ini. Error: " + ex.Message + ". Continuing", true));
            }

        }
        private async void finishSetup(StorageFolder localFolder, LogItem childLog)
        {
            childLog.Children.Add(new LogItem("Finishing setup"));


            try
            {
                childLog.Children.Add(new LogItem("Creating APP/__SETUP__"));
                await localFolder.CreateFileAsync("__SETUP__");
                childLog.Children.Add(new LogItem("Created APP/__SETUP__"));
            } catch (Exception ex)
            {
                childLog.Children.Add(new LogItem("Error while creating __SETUP__. Error: " + ex.Message, true));
            }


            childLog.Children.Add(new LogItem("Setup Finished !!! "));

            ContentDialog finishSetupDialog = new ContentDialog
            {
                Title = "Information",
                Content = "Project NewSpartan setup has been finished.\nPlease restart Project NewSpartan.",
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
    }
}
