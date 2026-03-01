using Contracts.Common.Interfaces;
using Customers.API.Entities;
using Customers.API.Persistence;
using Customers.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customers.API.Repositories
{
    public class CustomerRepositoryAsync : RepositoryQueryBase<Entities.Customer, int, Persistence.CustomerContext>, ICustomerRepository
    {
        public CustomerRepositoryAsync(CustomerContext context) : base(context)
        {
        }
        public Task<Entities.Customer> GetCustomerByUserNameAsync(string username) =>
         FindByCondition(x => x.UserName.Equals(username)) 
         .SingleOrDefaultAsync();

    }
}
