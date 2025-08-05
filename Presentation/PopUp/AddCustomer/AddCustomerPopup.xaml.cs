using maui_sample.ViewModels;
using CommunityToolkit.Maui.Views;

namespace maui_sample.Presentation.PopUp;

public partial class AddCustomerPopup : Popup
{
    public AddCustomerPopup()
    {
        InitializeComponent();

        AddCustomerPopupViewModel vm = new();
        vm.OnSave += Close;
        vm.OnCancel += () => Close();

        BindingContext = vm;
    }

    // TODO: Fazer injeção de dependencia
    private void OnlyNumbers(object sender, TextChangedEventArgs e)
    {
        Entry entry = (Entry)sender;

        string onlyCharNumbers = new(entry.Text?.Where(char.IsDigit).ToArray());

        if (entry.Text != onlyCharNumbers)
            entry.Text = onlyCharNumbers;
    }

    private void OnlyLetters(object sender, TextChangedEventArgs e)
    {
        Entry entry = (Entry)sender;

        string onlyCharCaracter = new(entry.Text?.Where(char.IsLetter).ToArray());

        if (entry.Text != onlyCharCaracter)
            entry.Text = onlyCharCaracter;
    }
}
