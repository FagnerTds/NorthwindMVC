using NorthwindMvc.Models.Entities;

namespace NorthwindMvc.Services
{
    public interface IProductService
    {
        Task CreateProductAsync(Product product);
        Task<List<Category>> GetCategoriesAssync();
        Task<List<Supplier>> GetSuppliersAssync();

        Task<Product> GetByIdAsync(int id);
        Task UpdateAsync(Product product);

    }
}
