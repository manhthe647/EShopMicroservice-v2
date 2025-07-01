using Contracts.Common.Interfaces;
using Product.API.Entities;
using Product.API.Persistence;

namespace Product.API.Repositories.Interfaces
{
    public interface IProductRepository: IRepositoryBaseAsync<CardProduct, long, ProductContext>
    {
        Task<List<CardProduct>> GetProducts(); 
    }
}
