using SQLite;

namespace maui_sample.Models;

public record Customer
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Address { get; set; } = string.Empty;
}
