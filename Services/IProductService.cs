using CapiWear_API.DTOs;
using System.Collections.Generic;

namespace CapiWear_API.Services
{
    public interface IProductService
    {
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task AddProductAsync(ProductDTO productDto);
        Task UpdateProductAsync(int id, ProductDTO productDto);
        Task DeleteProductAsync(int id);
    }
}