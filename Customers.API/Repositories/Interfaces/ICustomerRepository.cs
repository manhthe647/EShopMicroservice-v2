using Contracts.Common.Interfaces;
using Customers.API.Persistence;

namespace Customers.API.Repositories.Interfaces
{
    public interface ICustomerRepository: IRepositoryBaseAsync<Entities.Customer, int, CustomerContext>
    {
        Task<Entities.Customer> GetCustomerByUserNameAsync(string username);

    }
}
