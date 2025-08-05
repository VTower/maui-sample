using maui_sample.Data;
using maui_sample.Models;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using maui_sample.Services.Dialog;
using System.Collections.ObjectModel;
using maui_sample.Presentation.PopUp;
using CommunityToolkit.Mvvm.ComponentModel;

namespace maui_sample.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public ObservableCollection<Customer> Customers { get; } = [];

    // Commands
    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand NewCustomerCommand { get; }

    private readonly IDialogService _dialog;
    private readonly IDatabaseService _database;

    public MainViewModel(IDialogService dialogService, IDatabaseService databaseService)
    {
        _dialog = dialogService;
        _database = databaseService;

        NewCustomerCommand = new Command(async () => await NewCustomer());
        DeleteCommand = new Command<Customer>(async (client) => await DeleteCustomer(client));
        EditCommand = new Command<Customer>(async (client) => await EditCustomer(client));

        Setup();
    }

    private async void Setup()
    {
        // TODO: Change To mock Data
        await _database.InserirCustomerAsync(new Customer
        {
            FirstName = "FirstNameTest",
            LastName = "LastNameTest",
            Age = 37,
            Address = "Rud Exemplo, 123"
        });

        await LoadCustomersAsync();
    }

    private async Task LoadCustomersAsync()
    {
        Customers.Clear();

        List<Customer>? customers = await _database.CustomersAsync();

        foreach (var customer in customers)
            Customers.Add(customer);
    }

    private async Task EditCustomer(Customer client)
    {
        EditCustomerPopUp popup = new(client);
        object? resultado = await Application.Current.MainPage!.ShowPopupAsync(popup);

        if (resultado is Customer pClient)
        {
            client.FirstName = pClient.FirstName;
            client.LastName = pClient.LastName;
            client.Age = pClient.Age;
            client.Address = pClient.Address;

            await _database.UpdateCustomerAsync(client);

            // TODO: That can be avoided manipuling Customers or with INotifyPropertyChanged. But i dont have time anymore =(
            await LoadCustomersAsync();
        }
    }

    private async Task DeleteCustomer(Customer client)
    {
        bool confirm = await _dialog.ShowConfirmationAsync("Confirmação", $"Deseja excluir {client.FirstName}  {client.LastName}?", "Excluir", "Cancelar");

        if (confirm)
        {
            await _database.DeletarCustomerAsync(client);
            Customers.Remove(client);
        }
    }

    private async Task NewCustomer()
    {
        AddCustomerPopup popup = new();
        object? resultado = await Application.Current.MainPage!.ShowPopupAsync(popup);

        if (resultado is Customer client)
        {
            await _database.InserirCustomerAsync(client);
            Customers.Add(client);
        }
    }
}
