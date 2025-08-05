using maui_sample.Models;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace maui_sample.ViewModels;

public partial class EditCustomerPopUpViewModel : ObservableObject
{
    // TODO: Implemente this to avoid unnecessary behavior
    bool _hasEdited = false;
    private string _firstName;
    public string FirstName
    {
        get => _firstName;
        set
        {
            _hasEdited = true;
            SetProperty(ref _firstName, value);
        }
    }
    
    private string _lastName;
    public string LastName
    {
        get => _lastName;
        set
        {
            _hasEdited = true;
            SetProperty(ref _lastName, value);
        }
    }
    
    private int _age;
    public int Age
    {
        get => _age;
        set
        {
            _hasEdited = true;
            SetProperty(ref _age, value);
        }
    }
    
    private string _address;
    public string Address
    {
        get => _address;
        set
        {
            _hasEdited = true;
            SetProperty(ref _address, value);
        }
    }

    // Commands 
    public ICommand SaveCommand { get; }
    public ICommand CloseCommand { get; }

    // Handlers 
    public event Action<Customer>? OnSave;
    public event Action? OnCancel;

    public EditCustomerPopUpViewModel(Customer client)
    {
        _firstName = client.FirstName;
        _lastName = client.LastName;
        _age = client.Age;
        _address = client.Address;

        // Update just if have changes
        _hasEdited = false;

        CloseCommand = new Command(() =>
                      {
                          // TODO: Add PopUp in main page to spawn PopUp Edit Customer cancelled 
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
