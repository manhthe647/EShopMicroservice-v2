using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Customers.API.Controllers;

public static class CustomersController
{
public static void MapCustomersAPI(this WebApplication app)
{
    app.MapGet("/api/customers/{username}", (string username, Services.Interfaces.ICustomerService customerService) =>
    {
        var customer = customerService.GetCustomerByUsernameAsync(username);
        return Results.Ok(customer);
    });
}

}
