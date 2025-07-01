using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories
{
    public class ProductRepository
       : RepositoryBaseAsync<CardProduct, long, ProductContext>, IProductRepository
    {
        public ProductRepository(ProductContext context, IUnitOfWork<ProductContext> unitOfWork)
            : base(context, unitOfWork)
        {
        }

        public async Task<List<CardProduct>> GetProducts()
        {
            return await FindAll().ToListAsync();
        }
    }
}
