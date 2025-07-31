using Contracts.Domains;
using System.ComponentModel.DataAnnotations;

namespace Customers.API.Entities
{
    public class Customer: EntityBase<int>
    {
        [Required]
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }
    }
}
