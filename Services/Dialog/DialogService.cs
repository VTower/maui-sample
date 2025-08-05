namespace maui_sample.Services.Dialog;

public class DialogService : IDialogService
{
    public async Task<bool> ShowConfirmationAsync(string title, string message, string accept, string cancel)
    {
        Page? currentPage = Application.Current?.MainPage;

        if (currentPage != null)
            return await currentPage.DisplayAlert(title, message, accept, cancel);

        // TODO: Problens in dialog message
        return false;
    }
}