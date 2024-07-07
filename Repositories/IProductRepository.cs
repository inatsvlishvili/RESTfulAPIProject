using RESTfulAPIProject.Models;

namespace RESTfulAPIProject.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<(IEnumerable<Product>, int)> SearchProductsAsync(string searchText, int pageNumber, int pageSize);
    }
}
