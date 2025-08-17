using maui_sample.Data;
using maui_sample.Models;
using System.Windows.Input;
using maui_sample.Tests.Mocks;
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
    public ICommand NavigateToBrownianPage { get; }

    private readonly IDialogService _dialog;
    private readonly IDatabaseService _database;

    public MainViewModel(IDialogService dialogService, IDatabaseService databaseService)
    {
        _dialog = dialogService;
        _database = databaseService;

        NewCustomerCommand = new Command(async () => await NewCustomer());
        NavigateToBrownianPage = new Command(async () => await NavigateToBrownianAsync());
        EditCommand = new Command<Customer>(async (client) => await EditCustomer(client));
        DeleteCommand = new Command<Customer>(async (client) => await DeleteCustomer(client));

        Setup();
    }

    private async void Setup()
    {
        // My first time using Preferences.. Lets Try!
        if (Preferences.Get("FirstRun", true))
        {
            Preferences.Set("FirstRun", false);

            await _database.InserirCustomerAsync(RandomCustomersMock.Customer01);
            await _database.InserirCustomerAsync(RandomCustomersMock.Customer02);
            await _database.InserirCustomerAsync(RandomCustomersMock.Customer03);
        }

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

    private async Task NavigateToBrownianAsync()
    {
        await Shell.Current.GoToAsync(nameof(BrownianViewModel));
    }
}
