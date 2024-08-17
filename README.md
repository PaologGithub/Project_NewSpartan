# Project_NewSpartan
Project NewSpartan remakes the old Microsoft Edge Browser, named at the time Project Spartan

## Installation
Go to [NewSpartan website](https://paologgithub.github.io/products/NewSpartan) and:
- Certificate:
    - Open Tab "Additional Links"
    - Click on "Editor Certificate"
    - Install the downloaded .cer on Local Computer and local user.
- Installation:
    - Open Tab "Additional Links"
    - Click on "App Installer File"
    - Run the appinstaller

And there you go :,)

## Functionality
Project NewSpartan has many features:
- Complete Navigation
- Tabs System:
    - Open Tab with the "+" button at titlebar.
    - Close tab by hovering over the tab, and then pressing the "x"
- Favorites:
    - Get all favorites by opening the star menu at top.
    - Open a favorite by clicking on its tile.
    - View the favorite's description by hovering over the tile.
    - Favorite your current page by pressing the save button next to "Favorite".

## Including NewSpartan in a custom Windows
Do you want to include NewSpartan in your custom made Windows? No problem !!!

Here’s what you can do::
- Start by [Installing NewSpartan on your Windows](#installation).
- Optionally, set it as [the default browser in Windows](https://support.microsoft.com/windows/change-your-default-browser-in-windows-020c58c6-7d77-797a-b74e-8f07946c5db6).
- Optionally, set a default homepage by changing the Source in `UI/WebView.xaml` at line 20
- If you want, you can [add a custom favorite](#add-a-custom-favorite).

## Add a custom favorite
You want to add a custom favorite. Here is what you can do.

- Navigate to `%LOCALAPPDATA%\Packages\3b32003e-9502-4825-8ecb-a5823aabe484_yhvgyszfzs6g4\LocalState\Favorites`
- Go to the folder you want. (Ex: If you want to add a song, go to `Songs` folder)
- Create a new INI file. Name it like you want, it doesn't matter. (Ex: `5raadw.ini`)
- Create a new favorite from the template.

### Template:
```ini
[Favorite]
Name = Google
URL = https://www.google.com
Icon = https://www.google.com/favicon.ico
Description = Search informations, including webpages, images, videos and more.
```

## Thanks
Thanks for watching this project. If you do anything with this (include it in windows, do a modded version), tell me, I will be happy about it !!!
### Tip:
If you need help with NewSpartan, go into `Favorites > Help > Video Help`.
It's very useful and may assist you.