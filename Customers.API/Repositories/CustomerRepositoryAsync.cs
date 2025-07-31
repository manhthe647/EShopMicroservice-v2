using Contracts.Common.Interfaces;
using Customers.API.Entities;
using Customers.API.Persistence;
using Customers.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customers.API.Repositories
{
    public class CustomerRepositoryAsync : RepositoryBaseAsync<Entities.Customer, int, Persistence.CustomerContext>, ICustomerRepository
    {
        public CustomerRepositoryAsync(CustomerContext context, IUnitOfWork<CustomerContext> unitOfWork) : base(context, unitOfWork)
        {
        }
        public Task<Customer> GetCustomerByUserNameAsync(string username)
        {
            return FindByCondition(c => c.UserName.Equals(username, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync();
        }
    }
}
