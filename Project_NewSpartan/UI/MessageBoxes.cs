using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Project_NewSpartan.UI
{
    public class ErrorMessageBox
    {

    }

    public class WarningMessageBox
    {

    }

    public class InfoMessageBox
    {
        async public static void ShowInfoMessageBox(string Title, string Content, string PrimaryButtonText, ContentDialogButton DefaultButton, Action OnPrimaryButton)
        {
            ContentDialog dialog = new ContentDialog { 
                Title = $"Info: {Title}",
                Content = Content,
                PrimaryButtonText = PrimaryButtonText,
                CloseButtonText = "Close this popup",
                DefaultButton = DefaultButton
            };

            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                OnPrimaryButton();
            } else
            {
                // Close the popup
            }
        }

        async public static void ShowInfoMessageBox(string Title, string Content, string PrimaryButtonText, string SecondaryButtonText, ContentDialogButton DefaultButton, Action OnPrimaryButton, Action OnSecondaryButton)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = $"Info: {Title}",
                Content = Content,
                PrimaryButtonText = PrimaryButtonText,
                SecondaryButtonText = SecondaryButtonText,
                CloseButtonText = "Close this popup",
                DefaultButton = DefaultButton
            };

            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                OnPrimaryButton();
            } 
            else if (result == ContentDialogResult.Secondary)
            {
                OnSecondaryButton();
            }
            else
            {
                // Close the popup
            }
        }
    }

    public class SuccessMessageBox
    {

    }
}
