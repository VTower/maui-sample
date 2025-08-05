namespace maui_sample.Services.Dialog;

public interface IDialogService
{
    Task<bool> ShowConfirmationAsync(string title, string message, string accept, string cancel);
}