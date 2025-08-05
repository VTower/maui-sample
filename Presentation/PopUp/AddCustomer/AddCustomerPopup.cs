using maui_sample.Models;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace maui_sample.ViewModels;

public partial class AddCustomerPopupViewModel : ObservableObject
{
    private string _firstName = string.Empty;
    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }
    private string _lastName = string.Empty;
    public string LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }
    private int _age;
    public int Age
    {
        get => _age;
        set => SetProperty(ref _age, value);
    }
    private string _address = string.Empty;
    public string Address
    {
        get => _address;
        set => SetProperty(ref _address, value);
    }

    // Commands 
    public ICommand SaveCommand { get; }
    public ICommand CloseCommand { get; }

    // Handlers 
    public event Action<Customer>? OnSave;
    public event Action? OnCancel;

    public AddCustomerPopupViewModel()
    {
        CloseCommand = new Command(() =>
                      {
                          // TODO: Add PopUp in main page to spaw PopUp Add Customer cancelled 
                          // Do Nothing..
                          OnCancel?.Invoke();
                      });

        SaveCommand = new Command(() =>
               {
                   Customer cliente = new()
                   {
                       FirstName = FirstName,
                       LastName = LastName,
                       Age = Age,
                       Address = Address
                   };

                   OnSave?.Invoke(cliente);
               });
    }
}
