using Customers.API.Repositories.Interfaces;
using Customers.API.Services.Interfaces;

namespace Customers.API.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }
        public async Task<IResult> GetCustomerByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return Results.BadRequest("Username cannot be null or empty.");
            }

            var customer = await _customerRepository.GetCustomerByUserNameAsync(username);
            if (customer == null)
            {
                return Results.NotFound($"Customer with username '{username}' not found.");
            }

            return Results.Ok(customer);
        }
    }
}
