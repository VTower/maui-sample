using SQLite;
using maui_sample.Models;

namespace maui_sample.Data;

public class DatabaseService : IDatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseService()
    {
        // TODO: Add in Helper
        string? path = Path.Combine(Path.GetTempPath(), "btg_demo.db3");

        _database = new SQLiteAsyncConnection(path);

        _database.CreateTableAsync<Customer>().Wait();
    }

    public Task<int> InserirCustomerAsync(Customer Customer)
    {
        return _database.InsertAsync(Customer);
    }

    public Task<List<Customer>> CustomersAsync()
    {
        return _database.Table<Customer>().ToListAsync();
    }

    public Task<int> UpdateCustomerAsync(Customer cliente)
    {
        return _database.UpdateAsync(cliente);
    }

    public Task<int> DeletarCustomerAsync(Customer Customer)
    {
        return _database.DeleteAsync(Customer);
    }
}
