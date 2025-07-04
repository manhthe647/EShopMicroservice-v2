using AutoMapper;
using Contracts.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;
using Shared.DTOs.Product;
using System.ComponentModel.DataAnnotations;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repository.GetProducts();
            var result = _mapper.Map<List<ProductDto>>(products);
            return Ok(result);
        }

        //[HttpPut(template: "{id:long}")]
        //public async Task<IActionResult> UpdateProduct([Required] long id, [FromBody] UpdateProductDto productDto)
        //{
        //    var product = await _repository.GetProduct(id);
        //    if (product == null)
        //        return NotFound();

        //    var updateProduct = _mapper.Map(productDto, product);
        //    await _repository.UpdateProduct(updateProduct);
        //    await _repository.SaveChangesAsync();
        //    var result = _mapper.Map<ProductDto>(product);
        //    return Ok(result);
        //}

        //[HttpDelete(template: "{id:long}")]
        //public async Task<IActionResult> DeleteProduct([Required] long id)
        //{
        //    var product = await _repository.GetProduct(id);
        //    if (product == null)
        //        return NotFound();

        //    await _repository.DeleteProduct(id);
        //    await _repository.SaveChangesAsync();
        //    return NoContent();
        //}
    }
}
