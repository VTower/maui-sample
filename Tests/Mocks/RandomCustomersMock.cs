
using maui_sample.Models;

namespace maui_sample.Tests.Mocks;

public static class RandomCustomersMock
{
    public static Customer Customer01 => new()
    {
        FirstName = "FirstNameTest01",
        LastName = "LastNameTest01",
        Age = 37,
        Address = "Rud Exemplo, 123 Bairro 01"
    };

    public static Customer Customer02 => new()
    {
        FirstName = "FirstNameTest02",
        LastName = "LastNameTest02",
        Age = 22,
        Address = "Rud Exemplo, 123 Bairro 02"
    };

    public static Customer Customer03 => new()
    {
        FirstName = "FirstNameTest03",
        LastName = "LastNameTest03",
        Age = 42,
        Address = "Rud Exemplo, 123 Bairro 03"
    };
}