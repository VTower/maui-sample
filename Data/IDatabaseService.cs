using maui_sample.Models;

namespace maui_sample.Data;

public interface IDatabaseService
{
    Task<int> InserirCustomerAsync(Customer Customer);
    Task<List<Customer>> CustomersAsync();
    Task<int> UpdateCustomerAsync(Customer cliente);
    Task<int> DeletarCustomerAsync(Customer Customer);
}